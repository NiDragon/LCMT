using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Windows.Forms;
using IllTechLibrary.Util;

namespace LCMT
{
    static class App
    {
        internal static void FirstChanceHandler(object sender, FirstChanceExceptionEventArgs e)
        {
#if _DEBUG
            if (e.Exception.Message.Contains("Thread was"))
                return;

            MsgDialogs.LogError(e.Exception.Message + "\n" + e.Exception.StackTrace);
#endif
        }

        internal static void ThreadAcceptionHandler(object sender, ThreadExceptionEventArgs e)
        {
            MsgDialogs.LogError($"{e.Exception.Message}\n{e.Exception.StackTrace}");

            if (File.Exists("MiniCrash.exe"))
            {
                ProcessStartInfo pi = new ProcessStartInfo("MiniCrash.exe",
                    Process.GetCurrentProcess().Id.ToString() + " "
                    + e.Exception.Message 
                    + Environment.NewLine 
                    + e.Exception.StackTrace);

                Process.Start(pi).WaitForExit();
            }
        }

        internal static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            MsgDialogs.LogError($"{((Exception)e.ExceptionObject).Message}\n{((Exception)e.ExceptionObject).StackTrace}");

            if (File.Exists("MiniCrash.exe"))
            {
                ProcessStartInfo pi = new ProcessStartInfo("MiniCrash.exe",
                    Process.GetCurrentProcess().Id.ToString() + " "
                    + ((Exception)e.ExceptionObject).Message 
                    + Environment.NewLine 
                    + ((Exception)e.ExceptionObject).StackTrace);

                Process.Start(pi).WaitForExit();
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // If not debugging foward all errors to application handlers
            if (!Debugger.IsAttached)
            {
                // Application Level Error Handles
                AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;
                AppDomain.CurrentDomain.FirstChanceException += FirstChanceHandler;

                Application.ThreadException += ThreadAcceptionHandler;
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            }

            // Form Visual Styles
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

#if AUTH_SERVICE
            AuthForm auth = new AuthForm();

            auth.ShowDialog();
            
            if (auth.GetModule() != null)
                Application.Run((Form)auth.GetModule());
#else
            MultiFrm main = new MultiFrm();

            Application.Run(main);
#endif
        }
    }
}
