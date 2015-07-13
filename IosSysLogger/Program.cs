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
        /// 
        
        [STAThread]

        static void Main()
        {
            string currentPath = System.Environment.CurrentDirectory;

            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            iosSyslogger LoggerWindow = new iosSyslogger();
            loggerTool tool = new loggerTool();

            Thread deviceUUIDThread = new Thread(() => tool.readDeviceUUID(LoggerWindow, tool));
            deviceUUIDThread.Start();

            
            Application.Run(LoggerWindow);
            
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
        }
        public static class GlobalData
        {
            public static List<string> uuid = new List<string>();
        };

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
