using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace IosSysLogger
{
    class loggerTool
    {
        static string outdata;

        public void readLog()
        {
            Process process = new Process();
            process.StartInfo.FileName = @"C:\Users\Administrator\Downloads\imobiledevice-1.2.0-r3\idevicesyslog.exe"; ;
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
        static void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            //*Includes writing to a temporary PATH that need a fix later on *IMPORTANT*
            //Console.WriteLine(outLine.Data); FOR DEBUGGING PURPOSES
           
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Administrator\Desktop\syslog.txt", true))
            {
                file.WriteLine(outLine.Data);
                
            }
            //*Most of the logic for outputing the log should be dealt from this output Handler

        }



    }
}
