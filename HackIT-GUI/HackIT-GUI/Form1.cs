using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HackIT_GUI
{
    public partial class Form1 : Form
    {
        // Form move section (recreate move handle when using flat design)
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        Timer t = new Timer();


        public Form1()
        {
            InitializeComponent();

            t.Interval = 1000;
            t.Enabled = true;
            t.Tick += new System.EventHandler(OnTimerEvent);
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Events section //

        // Form move event (recreate move handle when using flat design)
        private void pnlMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }

            Data.fetchRecord();

        }

        private void OnTimerEvent(object sender, System.EventArgs e)
        {
            //Data.fetchRecord();
        }
    }
}
