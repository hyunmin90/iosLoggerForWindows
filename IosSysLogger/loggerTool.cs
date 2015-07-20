using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Linq;

namespace IosSysLogger
{
    class loggerTool
    {
        List<Thread> lstThreads = new List<Thread>();
        Dictionary<string, Process> crProcess = new Dictionary<string, Process>();
        List<String> WriteToTxt = new List<String>();
        Dictionary<string, string> uuidToName = new Dictionary<string, string>();
        public void readLog(iosSyslogger form,string uuid)
        {

           
            string currentPath = System.Environment.CurrentDirectory;                      
            Process process = new Process();
            
            process.StartInfo.FileName = currentPath+ @"\cmdLogger.exe"; 
            process.StartInfo.Arguments = "-u"+" "+uuid;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            if (uuid != null || process != null || crProcess != null)
            {
                try
                {
                    crProcess.Add(uuid, process);
                }
                catch (Exception ex)
                {
                    return;
                }//Keep gettign null point exception here 
            }
            //* Set output and error (asynchronous) handlers
            process.OutputDataReceived += new DataReceivedEventHandler((source, e)=>OutputHandler(source,e,form,uuid));
            process.ErrorDataReceived += new DataReceivedEventHandler((source, e) => OutputHandler(source, e, form,uuid));
            //* Start process and handlers
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

        }
        public void readDeviceinfo(iosSyslogger form, string uuid)
        {
            string currentPath = System.Environment.CurrentDirectory;
            

            Process process = new Process();
            
            process.StartInfo.FileName = currentPath + @"\iOSdeviceinfo.exe";
            process.StartInfo.Arguments = "-u" + " "+uuid;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;

            process.OutputDataReceived += new DataReceivedEventHandler((source, e) => deviceInfoHandler(source, e, form,uuid));

            //* Start process and handlers
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
        }

        public void readDeviceUUID(iosSyslogger form, loggerTool tool)
        {
            string currentPath = System.Environment.CurrentDirectory;

            Process process = new Process();
            process.StartInfo.FileName = currentPath + @"\deviceid.exe";
            process.StartInfo.Arguments = "-l";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;

            process.OutputDataReceived += new DataReceivedEventHandler((source, e) => uuidinfoHandler(source, e, form));

            //* Start process and handlers
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
           
            if (Program.GlobalData.usbRemoved == true)
            {
                
                foreach (string uuidt in Program.GlobalData.uuid)
                {
                    Process test = new Process();
                    if (!(crProcess.TryGetValue(uuidt, out test)))
                    {
                        continue;
                    }
                    

                    if (!Program.GlobalData.tempuuid.Contains(uuidt)&&uuidt!=null&& crProcess[uuidt]!=null)
                    {
                        crProcess[uuidt].Kill();
                        crProcess.Remove(uuidt);
                    }

                }

                Program.GlobalData.uuid.Clear();
                Program.GlobalData.uuid.AddRange(Program.GlobalData.tempuuid);
                form.BeginInvoke(new Action(() =>
                {
                    form.clearDeviceName();
                }));
                foreach (string uuidstring in Program.GlobalData.uuid)
                {
                    Thread loggingThread = new Thread(() => tool.readDeviceinfo(form, uuidstring));
                    loggingThread.IsBackground = true;
                    loggingThread.Start();
                }
                Program.GlobalData.usbRemoved = false;
                return;
            }


            else if (Program.GlobalData.usbInserted == true)
            {
                if (Program.GlobalData.uuid[Program.GlobalData.uuid.Count - 1] == null)
                    return;
                else
                {
                    deviceInfoThread(form, tool, Program.GlobalData.uuid[Program.GlobalData.uuid.Count - 1]);
                    LoggingThread(form, tool, Program.GlobalData.uuid[Program.GlobalData.uuid.Count - 1]);
                    lstThreads[lstThreads.Count - 2].Start();
                    lstThreads[lstThreads.Count - 1].Start();
                    Program.GlobalData.notInit = true;
                    return;

                }
                
            }
            else if (Program.GlobalData.usbInserted!=true)
            {
                if (Program.GlobalData.usbInserted == true) return;
                foreach (string uuid in Program.GlobalData.uuid)
                {
                    deviceInfoThread(form, tool, uuid);
                    LoggingThread(form, tool, uuid);
                }

                foreach (Thread th in lstThreads)
                {
                    th.Start();
                }
                return;
            }
            else
                return;
        }
        public void LoggingThread(iosSyslogger form, loggerTool tool,string uuid)
        {
            Thread loggingThread = new Thread(() => tool.readLog(form,uuid));
            loggingThread.IsBackground = true;
            lstThreads.Add(loggingThread);
        }
        public void deviceInfoThread(iosSyslogger form, loggerTool tool, string uuid)
        {
            Thread deviceInfoThread = new Thread(() => tool.readDeviceinfo(form,uuid));
            deviceInfoThread.IsBackground = true;
            lstThreads.Add(deviceInfoThread);
        }


        private void uuidinfoHandler(object sendingProcess, DataReceivedEventArgs outLine, iosSyslogger form)
        {
            if (outLine.Data == null) return;
            if (Program.GlobalData.usbRemoved == true)
            {
                int index = Program.GlobalData.uuid.FindIndex(x => x.Contains(outLine.Data));
                Program.GlobalData.tempuuid.Add(Program.GlobalData.uuid[index]);
                Program.GlobalData.uuid.RemoveAt(index);
            }
            else
            {
                if (outLine.Data == null) return;
                else if (Program.GlobalData.uuid.Contains(outLine.Data)) return;
                else if (!Program.GlobalData.uuid.Contains(outLine.Data))
                {
                    Program.GlobalData.uuid.Add(outLine.Data);
                }
            }
        }

        private void deviceInfoHandler(object sendingProcess, DataReceivedEventArgs outLine, iosSyslogger form,string uuid)
        {
            form.BeginInvoke(new Action(() =>
            {
                if(outLine.Data==null) return;
                if (outLine.Data.Contains("DeviceName:"))
                {
                    string[] words = outLine.Data.Split(' ');
                    words = words.Where(w => w != words[0]).ToArray();
                    string deviceName = "";
                    for (int i = 0; i < words.Length; i++)
                    {
                        if (i < words.Length - 1)
                            deviceName += words[i] + "-";
                        else
                            deviceName += words[i];
                    }
                    uuidToName.Add(uuid, deviceName);
                }
                form.insertDeviceName = outLine.Data;
            }));
        }
        
        public void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine, iosSyslogger form, string uuid)
        {
            string currentPath = System.Environment.CurrentDirectory;
            bool exit = false;
            if (exit == true) return;

            try
            {
                form.BeginInvoke(new Action(() =>
                {
                    if (uuidToName.ContainsKey(uuid)==false) return;
                    form.insertToDataSource(outLine.Data,uuidToName[uuid]);
                }));


               using (System.IO.StreamWriter file = new System.IO.StreamWriter(currentPath + @"\syslog" + uuid + ".txt", true))
               {
                 file.WriteLine(outLine.Data);
               }

            }
            catch 
            {
                return ;
            }
            //*Most of the logic for outputing the log should be dealt from this output Handler
        }

    }
}
