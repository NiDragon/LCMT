using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Windows.Forms;
using IllTechLibrary.Settings;
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

            ProcessCommandLine();

            // Form Visual Styles
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MultiFrm main = new MultiFrm();

            Application.Run(main);
        }

        static void ProcessCommandLine()
        {
            string[] cmdline = Environment.GetCommandLineArgs();

            for(int i = 1; i < cmdline.Length; i++)
            {
                try
                {
                    string[] arg;

                    if (cmdline[i].Contains("="))
                    {
                        arg = cmdline[i].Split('=');
                    }
                    else
                    {
                        arg = new string[] { cmdline[i], "" };
                    }

                    switch(arg[0])
                    {
                        case "-allow-unsafe":
                            Preferences.g_allowUnsafe = bool.Parse(arg[1]);
                            break;
                        default:
                            MsgDialogs.LogError($"Unknown Command Line {cmdline[i]}");
                            break;
                    }
                }
                catch (Exception)
                {
                    // Malformed commandline move on
                }
            }
        }
    }
}
