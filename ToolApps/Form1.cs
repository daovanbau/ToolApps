using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using KAutoHelper;


namespace ToolApps
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

        [DllImport("user32.dll", SetLastError = true)]

        [System.ComponentModel.Browsable(false)]

        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetWindowPos(IntPtr hwnd, long hWndInsertAfter, long x, long y, long cx, long cy, long wFlags);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);
        IntPtr appWin1;

        const int WM_SYSCOMMAND = 274;
        const int SC_MAXIMIZE = 1000;
        int DeviceCount = 2;
        public Form1()
        {
            InitializeComponent();
            setDevices();
            Thread.Sleep(TimeSpan.FromSeconds(5));
        }
        void Auto()
        {
            List<string> devices = new List<string>();
            devices = KAutoHelper.ADBHelper.GetDevices();
            foreach (var device in devices)
            {
                Thread t = new Thread(() =>
                {
                    KAutoHelper.ADBHelper.TapByPercent(device, 68.3, 33.1);
                });
                t.Start();
            }
        }
        void setDevices()
        {
            int x = 0;
            int y = 0;
            for (int i = 0; i < DeviceCount; i++)
            {
                Process proc = Process.Start(new ProcessStartInfo()
                {
                    FileName = "E:\\laptrinh\\tài liệu\\drive-download-20201013T021241Z-001\\LDPlayer-" + (i + 1),
                    WindowStyle = ProcessWindowStyle.Minimized
                });
                while (proc.MainWindowHandle == IntPtr.Zero)
                {
                    if (i == 3)
                    {
                        x = i * 0;
                        y = 3 * 100;
                    }
                    else
                    {
                        x = i * 100;
                        y = i * 0;
                    }
                    Thread.Sleep(100);
                    appWin1 = proc.MainWindowHandle;
                    SetParent(appWin1, this.panel1.Handle);

                    MoveWindow(appWin1, x, y, this.Width / 6, this.Height / 2, true);
                }
                Thread.Sleep(100);
            }
        }
    } 
}
