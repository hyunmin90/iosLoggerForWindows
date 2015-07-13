using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace IosSysLogger
{
    class loggerTool
    {
        List<Thread> lstThreads = new List<Thread>();
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
            Program.GlobalData.uuid.RemoveAt(Program.GlobalData.uuid.Count - 1);

            deviceInfoThread(form, tool, "");

            foreach (string uuid in Program.GlobalData.uuid)
            {
                deviceInfoThread(form, tool,uuid);
                LoggingThread(form,tool,uuid);
            }

            foreach (Thread th in lstThreads)
            {
                th.Start();
            }

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
            Program.GlobalData.uuid.Add(outLine.Data);
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

            if (!File.Exists(currentPath + @"\syslog.txt"+uuid))
            {
                File.Create(currentPath + @"\syslog.txt"+ uuid).Close();
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(currentPath+ @"\syslog" + uuid + ".txt", true))
            {
                form.BeginInvoke(new Action(() =>
                {

                    form.TextBoxText = outLine.Data;

                }));
                file.WriteLine(outLine.Data);
            }
            //*Most of the logic for outputing the log should be dealt from this output Handler


            
            
        }



    }
}
