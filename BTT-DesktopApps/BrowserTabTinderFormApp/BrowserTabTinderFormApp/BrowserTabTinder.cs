using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
    public partial class BrowserTabTinder : Form
    {

        JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            },
        };

        private string basetitle = "BrowserTabTinder | Process -";

        private OverlayForm overlayForm = new OverlayForm();

        private Loader loader = new Loader();

        private TabData _currentTabData;

        public TabData CurrentTabData
        {
            get { return _currentTabData; }
            set
            {
                _currentTabData = value;

                pictureBox1.Image = _currentTabData.ImageB64.FromBase64();
                label1.Text = _currentTabData.Url;
                this.Text = $"{basetitle} {_currentTabData.CompletedTabCount}/{_currentTabData.TabCount} | {_currentTabData.Title}";

            }
        }

        public BrowserTabTinder()
        {

            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            

        }

        private async void BrowserTabTinder_Load(object sender, EventArgs e)
        {


            await CallWithLoader(async () =>
            {
                var tabdataMessagetask = SendAndRecv<TabData>();
                await ProcessTab(await tabdataMessagetask);
            });



        }

        private async Task ProcessTab(WebSocketMessage<TabData> tabdataMessage)
        {

            if (tabdataMessage == null)
            {
                return;
            }

            if (tabdataMessage.Command.Equals("end", StringComparison.CurrentCultureIgnoreCase))
            {
                MessageBox.Show("No tabs left. The program will now close.");
                Application.Exit();
                return;
            }


            CurrentTabData = tabdataMessage.Message;
        }

        private async Task<WebSocketMessage<T>> SendAndRecv<T>(string command = "next")
        {

            await Program.webSocketServer.SendStringMessage(JsonConvert.SerializeObject(new WebSocketMessage<string> { Command = command }, serializerSettings));
            string jsonmassage = await Program.webSocketServer.RecvStringMessage();
            var message = JsonConvert.DeserializeObject<WebSocketMessage<T>>(jsonmassage);



            return message;




        }



        private void BrowserTabTinder_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FullScreenImage fullScreenImage = new FullScreenImage(pictureBox1.Image);
            fullScreenImage.Show();

        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Program.WM_SYSCOMMAND)
            {

                int cmd = (m.WParam.ToInt32() & 0xFFF0);
                // block topbar double click 
                if (cmd == Program.SC_RESTORE && this.WindowState == FormWindowState.Maximized)
                    return;

                if (cmd == Program.SC_SIZE || cmd == Program.SC_MOVE)
                    return;

            }
            base.WndProc(ref m);
        }

        private async void button3_Click(object sender, EventArgs e)
        {


            await CallWithLoader(async () =>
            {
                await Program.webSocketServer.SendStringMessage(JsonConvert.SerializeObject(new WebSocketMessage<string> { Command = "delete" }, serializerSettings));
                var tabdataMessagetask = SendAndRecv<TabData>();
                await ProcessTab(await tabdataMessagetask);
            });
        }

        private async void button4_Click(object sender, EventArgs e)
        {


            await CallWithLoader(async () =>
            {
                var tabdataMessagetask = SendAndRecv<TabData>("reload");
                await ProcessTab(await tabdataMessagetask);
            });


        }




        private void ShowOverlay()
        {

            if (overlayForm == null || overlayForm.IsDisposed)
            {
                overlayForm = new OverlayForm();
            }
            overlayForm.ShowOverlay(this, loader);


        }

        private void HideOverlay()
        {
            TopMost = true;
            if (overlayForm != null && !overlayForm.IsDisposed)
            {
                overlayForm.Hide();

            }
            TopMost = false;
        }

        private async Task CallWithLoader(Func<Task> action)
        {
            ShowOverlay();
            await action.Invoke();
            HideOverlay();
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            await Program.webSocketServer.SendStringMessage(JsonConvert.SerializeObject(new WebSocketMessage<string> { Command = "focus" }, serializerSettings));


        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await CallWithLoader(async () =>
            {
                var tabdataMessagetask = SendAndRecv<TabData>();
                await ProcessTab(await tabdataMessagetask);
            });
        }
    }
}
