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

namespace ToolApps
{
    public partial class childForm : Form
    {
        public childForm()
        {
            InitializeComponent();


        }
        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetWindowPos(IntPtr hwnd, long hWndInsertAfter, long x, long y, long cx, long cy, long wFlags);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);
        IntPtr appWin1;
        private void childForm_Load(object sender, EventArgs e)
        {      
           
                    openEmulation();
             
          
             
        }

        void openEmulation()
        {

        
            ProcessStartInfo ps1 = new ProcessStartInfo("E:\\ChangZhi\\LDPlayer\\dnmultiplayer.exe");
            ps1.WindowStyle = ProcessWindowStyle.Minimized;
            
            Process p1 = Process.Start(ps1);
            while (p1.MainWindowHandle == IntPtr.Zero)
            {
                Thread.Sleep(1000); // Allow the process to open it's window
                /*Thread t = new Thread(() =>
                {*/
                    appWin1 = p1.MainWindowHandle;
                /*});
                t.Start(); */ 
            }
            
            // Put it into this form
            SetParent(appWin1, this.Handle);
            // Move the window to overlay it on this window
            MoveWindow(appWin1, 0, 0, this.Width-10, this.Height-50, true);
            

        } 
        
    }
}
