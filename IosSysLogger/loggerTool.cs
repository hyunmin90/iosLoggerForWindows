using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace IosSysLogger
{
    class loggerTool
    {
        List<Thread> lstThreads = new List<Thread>();
        Dictionary<string, Process> crProcess = new Dictionary<string, Process>();
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

            process.OutputDataReceived += new DataReceivedEventHandler((source, e) => deviceInfoHandler(source, e, form));

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
            //Program.GlobalData.uuid.RemoveAt(Program.GlobalData.uuid.Count - 1);

            if (Program.GlobalData.usbRemoved == true)
            {
                foreach (string uuidt in Program.GlobalData.uuid)
                {
                    if (!Program.GlobalData.tempuuid.Contains(uuidt))
                    {
                        //MessageBox.Show("Gotcha");
                        //MessageBox.Show(uuidt);
                        crProcess[uuidt].Kill();
                        crProcess.Remove(uuidt);
                    }

                }

                Program.GlobalData.uuid.Clear();
                Program.GlobalData.uuid.AddRange(Program.GlobalData.tempuuid);
                form.BeginInvoke(new Action(() =>
                {
                    form.clearCheckbox();
                }));
                foreach (string uuidstring in Program.GlobalData.uuid)
                {
                    Thread loggingThread = new Thread(() => tool.readDeviceinfo(form, uuidstring));
                    loggingThread.IsBackground = true;
                    loggingThread.Start();
                }
                Program.GlobalData.usbRemoved = false;
                //foreach (string uuidname in Program.GlobalData.uuid)
                //{
                //   MessageBox.Show(uuidname);
                //}
                //return;
                return;
            }

            //deviceInfoThread(form, tool, "");

            else if (Program.GlobalData.usbInserted == true)
            {
                //MessageBox.Show("Inserted");
                deviceInfoThread(form, tool, Program.GlobalData.uuid[Program.GlobalData.uuid.Count - 1]);
                LoggingThread(form, tool, Program.GlobalData.uuid[Program.GlobalData.uuid.Count - 1]);
                lstThreads[lstThreads.Count - 2].Start();
                lstThreads[lstThreads.Count - 1].Start();
                //Program.GlobalData.usbInserted = false;
                Program.GlobalData.notInit = true;
                return;
            }
            else if (Program.GlobalData.usbInserted!=true)
            {
                if (Program.GlobalData.usbInserted == true) return;
                //MessageBox.Show("Inserted should not be here");
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
                { //MessageBox.Show(outLine.Data.ToString());
                    Program.GlobalData.uuid.Add(outLine.Data);
                }
            }
        }

        private void deviceInfoHandler(object sendingProcess, DataReceivedEventArgs outLine, iosSyslogger form)
        {
            form.BeginInvoke(new Action(() =>
            {
                form.DeviceNameText = outLine.Data;
            }));
        }
        
        public void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine, iosSyslogger form, string uuid)
        {
            //*Includes writing to a temporary PATH that need a fix later on *IMPORTANT*
            //Console.WriteLine(outLine.Data); FOR DEBUGGING PURPOSES
            string currentPath = System.Environment.CurrentDirectory;
            bool exit = false;
            if (exit == true) return;

            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(currentPath + @"\syslog" + uuid + ".txt", true))
                {
                    form.BeginInvoke(new Action(() =>
                    {

                        form.TextBoxText = outLine.Data;

                    }));
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
