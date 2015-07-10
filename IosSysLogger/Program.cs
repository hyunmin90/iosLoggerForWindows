using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

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
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 LoggerWindow = new Form1();
            loggerTool tool = new loggerTool();
            Thread loggingThread = new Thread(() => tool.readLog(LoggerWindow));
            loggingThread.IsBackground = true;
            
            loggingThread.Start(); //Multi Threading the syslog process in background.

            Application.Run(LoggerWindow);
            
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
        }

        static void OnProcessExit(object sender, EventArgs e)
        { //Temporary way of killing background process. Need this fixed.**IMPORTANT**
            foreach (Process proc in Process.GetProcessesByName("idevicesyslog"))
            {
                proc.Kill();
            }


        }
    }
}
