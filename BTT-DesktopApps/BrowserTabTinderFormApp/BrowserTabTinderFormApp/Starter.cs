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

        private void Starter_Load(object sender, EventArgs e)
        {
            Program.webSocketServer.OnSocketConnected += WebSocketServer_OnSocketConnected;
            Program.webSocketServer.OnSocketDisconnected += WebSocketServer_OnSocketDisconnected;
            _ = Program.webSocketServer.Listen();
        }

        private void WebSocketServer_OnSocketDisconnected(object? sender, EventArgs e)
        {
            MessageBox.Show("The connection was closed. The application will now close");
            Application.Exit();
        }

        private void WebSocketServer_OnSocketConnected(object? sender, EventArgs e)
        {
            button1.Enabled = true;
        }
    }
}
