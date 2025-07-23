using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrowserTabTinderFormApp
{
    public partial class FullScreenImage : Form
    {
        public FullScreenImage(Image image)
        {
            InitializeComponent();
            pictureBox1.Image = image;

        }

        private void FullScreenImage_Load(object sender, EventArgs e)
        {

        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Program.WM_SYSCOMMAND)
            {
                int cmd = (m.WParam.ToInt32() & 0xFFF0);
                if (cmd == Program.SC_RESTORE || cmd == Program.SC_SIZE || cmd == Program.SC_MOVE)
                    return;
            }
            base.WndProc(ref m);

        }
    }
}
