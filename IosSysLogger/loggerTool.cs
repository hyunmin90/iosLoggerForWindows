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
           
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = currentPath;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                   | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = "*.*";
            watcher.Changed += new FileSystemEventHandler((source, e) => OnChanged(source, e, form));
            watcher.EnableRaisingEvents = true;

           
            Process process = new Process();
            process.StartInfo.FileName = currentPath+ @"\cmdLogger.exe"; 
            process.StartInfo.Arguments = "";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
         
            //* Set your output and error (asynchronous) handlers
            process.OutputDataReceived += new DataReceivedEventHandler((source, e)=>OutputHandler(source,e,form));
            process.ErrorDataReceived += new DataReceivedEventHandler((source, e) => OutputHandler(source, e, form));
            //* Start process and handlers
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

        }

        private void OnChanged(object source, FileSystemEventArgs e, iosSyslogger form)
        {
            
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
