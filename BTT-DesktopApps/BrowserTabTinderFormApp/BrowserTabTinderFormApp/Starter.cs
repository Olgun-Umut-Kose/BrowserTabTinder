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
    public partial class Starter : Form
    {
        public Starter()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BrowserTabTinder browserTabTinder = new BrowserTabTinder();
            browserTabTinder.Show();
            this.Hide();    
        }
    }
}
