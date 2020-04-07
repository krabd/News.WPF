using System.Diagnostics;
using System.Runtime.InteropServices;

namespace News.Utils.Helpers
{
    public static class ControlPCHelper
    {
        [DllImport("user32")]
        private static extern bool ExitWindowsEx(uint uFlags, uint dwReason);

        public static void ShutdownPC()
        {
            Process.Start("shutdown", "/s /t 0");
        }

        public static void LogOut()
        {
            ExitWindowsEx(0, 0);
        }
    }
}
