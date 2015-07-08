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
        static string outdata;
        public void readLog(Form1 form)
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
            process.StartInfo.FileName = currentPath+@"\idevicesyslog.exe"; 
            process.StartInfo.Arguments = "";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;

            //* Set your output and error (asynchronous) handlers
            process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
            process.ErrorDataReceived += new DataReceivedEventHandler(OutputHandler);
            //* Start process and handlers
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

            
        }

        private void OnChanged(object source, FileSystemEventArgs e, Form1 form)
        {
            
        }

        public void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            //*Includes writing to a temporary PATH that need a fix later on *IMPORTANT*
            //Console.WriteLine(outLine.Data); FOR DEBUGGING PURPOSES

            string currentPath = System.Environment.CurrentDirectory;
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(currentPath+@"\syslog.txt", true))
            {
                file.WriteLine(outLine.Data);
            }
            //*Most of the logic for outputing the log should be dealt from this output Handler

        }



    }
}
