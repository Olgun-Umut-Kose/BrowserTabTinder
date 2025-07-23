namespace BrowserTabTinderFormApp
{
    internal static class Program
    {
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_RESTORE = 0xF120;
        public const int SC_SIZE = 0xF000;
        public const int SC_MOVE = 0xF010;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Starter());
        }
    }
}