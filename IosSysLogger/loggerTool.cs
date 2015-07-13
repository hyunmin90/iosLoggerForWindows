using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace IosSysLogger
{
    class loggerTool
    {
       
        public void readLog(iosSyslogger form)
        {
            
            string currentPath = System.Environment.CurrentDirectory;
           
           

           
            Process process = new Process();
            process.StartInfo.FileName = currentPath+ @"\cmdLogger.exe"; 
            process.StartInfo.Arguments = "";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
         
            //* Set output and error (asynchronous) handlers
            process.OutputDataReceived += new DataReceivedEventHandler((source, e)=>OutputHandler(source,e,form));
            process.ErrorDataReceived += new DataReceivedEventHandler((source, e) => OutputHandler(source, e, form));
            //* Start process and handlers
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

        }
        public void readDeviceinfo(iosSyslogger form)
        {
            string currentPath = System.Environment.CurrentDirectory;
            
            Process process = new Process();
            process.StartInfo.FileName = currentPath + @"\iOSdeviceinfo.exe";
            process.StartInfo.Arguments = "";
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


        private void deviceInfoHandler(object sendingProcess, DataReceivedEventArgs outLine, iosSyslogger form)
        {
            form.BeginInvoke(new Action(() =>
            {
                form.DeviceNameText = outLine.Data;
            }));
        }

        public void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine, iosSyslogger form)
        {
            //*Includes writing to a temporary PATH that need a fix later on *IMPORTANT*
            //Console.WriteLine(outLine.Data); FOR DEBUGGING PURPOSES

            string currentPath = System.Environment.CurrentDirectory;


            using (System.IO.StreamWriter file = new System.IO.StreamWriter(currentPath+@"\syslog.txt", true))
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
