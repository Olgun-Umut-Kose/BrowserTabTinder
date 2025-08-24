namespace BrowserTabTinderFormApp
{
    public partial class OverlayForm : Form
    {

        private IOverlay? Current = null;

        public OverlayForm()
        {
            InitializeComponent();
            
        }




        public void ShowOverlay(Form parentForm, IOverlay top, Color? backcolor = null, double opacity = 0.2)
        {
            if (Current != null) return;
            this.Opacity = opacity;
            this.BackColor = backcolor ?? Color.Black;
            this.Size = parentForm.Size;
            this.Location = parentForm.Location;




            this.Show(parentForm);
            Current = top;
            top.ShowOverlay(parentForm);
        }

        private void OverlayForm_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible) { Current?.Hide(); Current = null; }
        }
    }
}
