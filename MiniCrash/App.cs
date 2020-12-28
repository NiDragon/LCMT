using System;
using System.Threading;
using System.Windows.Forms;

namespace MiniCrash
{
    static class App
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string[] arguments = Environment.GetCommandLineArgs();

            if (arguments.Length > 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                MainFrm fm = new MainFrm();

                fm.Show();

                while(fm.DoGetAlive())
                {
                    Application.DoEvents();
                    Thread.Sleep(100);
                }
            }
        }
    }
}
