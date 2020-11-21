using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;

namespace HarithaHRMS
{
    class FocusedApp
    {


        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        public static string GetActiveWindowTitle()
        {
            var handle = GetForegroundWindow();
            string fileName = "";
            string name = "";
            uint pid = 0;
            GetWindowThreadProcessId(handle, out pid);

            Process p = Process.GetProcessById((int)pid);
            var processname = p.ProcessName;

            switch (processname)
            {
                case "explorer": //metro processes
                case "WWAHost":
                    name = GetTitle(handle);
                    return name;
                default:
                    break;
            }
            string wmiQuery = string.Format("SELECT ProcessId, ExecutablePath FROM Win32_Process WHERE ProcessId LIKE '{0}'", pid.ToString());
            var pro = new ManagementObjectSearcher(wmiQuery).Get().Cast<ManagementObject>().FirstOrDefault();
            fileName = (string)pro["ExecutablePath"];
            // Get the file version
            FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(fileName);
            // Get the file description
            name = myFileVersionInfo.FileDescription;
            if (name == "")
                name = GetTitle(handle);

            return name;
        }

        public static string GetTitle(IntPtr handle)
        {
            string windowText = "";
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                windowText = Buff.ToString();
            }
            return windowText;
        }


    }
}
