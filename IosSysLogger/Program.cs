using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace IosSysLogger
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {

            string currentPath = System.Environment.CurrentDirectory;

            if (!File.Exists(currentPath + @"\syslog.txt"))
            {
                File.Create(currentPath + @"\syslog.txt").Close();
            }
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            iosSyslogger LoggerWindow = new iosSyslogger();
            loggerTool tool = new loggerTool();
       

           
            //Check if syslog file exist. If it does not exist, create one 

            Thread loggingThread = new Thread(() => tool.readLog(LoggerWindow));
            loggingThread.IsBackground = true;

            Thread deviceInfoThread = new Thread(() => tool.readDeviceinfo(LoggerWindow));
            deviceInfoThread.IsBackground = true;

            deviceInfoThread.Start(); //Threading the device info process in background.
            loggingThread.Start(); //Threading the syslog process in background.

            Application.Run(LoggerWindow);
            
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
        }

        static void OnProcessExit(object sender, EventArgs e)
        { //Temporary way of killing background process. Need this fixed.**IMPORTANT**
            foreach (Process proc in Process.GetProcessesByName("cmdLogger"))
            {
                proc.Kill();
            }
            foreach (Process proc in Process.GetProcessesByName("iOSdeviceinfo"))
            {
                proc.Kill();
            }


        }
    }
}
