using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Management;

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
            var watcher = new ManagementEventWatcher();
            var query = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 2"); //Checking USB device insertion 
            watcher.EventArrived += new EventArrivedEventHandler((source, e) => watcher_USBInserted(source, e, LoggerWindow, tool));
            watcher.Query = query;
            watcher.Start();

            WqlEventQuery removeQuery = new WqlEventQuery("SELECT * FROM __InstanceDeletionEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_USBHub'");
            ManagementEventWatcher removeWatcher = new ManagementEventWatcher(removeQuery);
            removeWatcher.EventArrived += new EventArrivedEventHandler((source, e) => DeviceRemovedEvent(source, e, LoggerWindow, tool));
            removeWatcher.Start();

            Thread deviceUUIDThread = new Thread(() => tool.readDeviceUUID(LoggerWindow, tool));
            deviceUUIDThread.Start();

            Application.Run(LoggerWindow);

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
        }
        public static class GlobalData
        {
            public static List<string> uuid = new List<string>();
            public static List<string> tempuuid = new List<string>();
            public static int uuidCnt = 0;
            public static bool notInit = false;
            public static bool usbInserted = false;
            public static bool usbRemoved = false;
        }
        static void watcher_USBInserted(object sender, EventArgs e,iosSyslogger window, loggerTool tool)
        {
            //MessageBox.Show("New USB detected!");
            if (GlobalData.usbInserted == true) //If this is true, new iOS Device has already been processed 
            {
                return;
            }
            else
            {
                GlobalData.usbInserted = true;
                tool.readDeviceUUID(window, tool);
                
            }
        }
        static void DeviceRemovedEvent(object sender, EventArrivedEventArgs e, iosSyslogger window, loggerTool tool)
        {
            GlobalData.usbInserted = false;
            if (GlobalData.usbRemoved == true)
                return;
            else
            {
               // MessageBox.Show("Removed");
                window.BeginInvoke(new Action(() =>
                {
                    window.clearDevicenameList();
                    window.clearDeviceName();
                }));
                GlobalData.usbRemoved = true;
                tool.readDeviceUUID(window, tool);
                //GlobalData.usbRemoved = false;
            }
            
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
