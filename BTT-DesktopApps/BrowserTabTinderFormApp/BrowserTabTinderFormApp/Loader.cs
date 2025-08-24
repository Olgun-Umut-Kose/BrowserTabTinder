using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrowserTabTinderFormApp
{
    public partial class Loader : Form,IOverlay
    {

        private string[] loadingStrings = { "(☞ﾟヮﾟ)☞", "☜(ﾟヮﾟ☜)" };

        private byte currentLoadIndex = 0;

        [DllImport("user32.dll", SetLastError = true)]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        const int GWL_EXSTYLE = -20;
        const int WS_EX_LAYERED = 0x80000;
        const int WS_EX_TRANSPARENT = 0x20;

        public Loader()
        {
            InitializeComponent();
            this.TransparencyKey = BackColor;
            int exStyle = GetWindowLong(this.Handle, GWL_EXSTYLE);
            SetWindowLong(this.Handle, GWL_EXSTYLE, exStyle | WS_EX_LAYERED);

        }

        public void ShowOverlay(Form parent)
        {
            this.Size = parent.Size;
            this.Location = parent.Location;

            this.Show(parent);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (currentLoadIndex == 0)
            {
                currentLoadIndex = 1;

            }
            else
            {
                currentLoadIndex = 0;

            }
            label1.Text = loadingStrings[currentLoadIndex];
        }
    }
}
