using MiniCrash.CrashHandler;
using System;
using System.Windows.Forms;
using System.Windows.Shell;

namespace MiniCrash
{
    internal partial class MainFrm : Form
    {
        private ProcessDumper m_dump;
        private TaskbarItemInfo m_tbarInfo = new TaskbarItemInfo();

        internal MainFrm()
        {
            InitializeComponent();

            string[] arguments = Environment.GetCommandLineArgs();

            m_dump = new ProcessDumper(int.Parse(arguments[1]));

            if (m_dump != null)
            {
                Text = $"Crash Report - {m_dump.GetFileName()} {m_dump.GetVersion()}";
            }

            labelHeader.Text = $"Looks Like {m_dump.GetProcessName()} Crashed :(";

            string message = string.Empty;

            for (int i = 2; i < arguments.Length; i++)
            {
                message += arguments[i] + " ";
            }

            errorMsg.Text = message;

            System.Media.SystemSounds.Asterisk.Play();

            m_tbarInfo.ProgressState = TaskbarItemProgressState.Normal;
        }

        internal bool DoGetAlive()
        {
            if(m_dump != null && m_dump.IsAlive() && Visible)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnCancelDump(object sender, EventArgs e)
        {
            Close();
        }

        private void OnBeginDump(object sender, EventArgs e)
        {
            if(m_dump != null)
            {
                if (m_dump.CreateDump())
                {
                    dumpProgress.Value = 33;
                    m_tbarInfo.ProgressValue = 33;

                    string nl = Environment.NewLine;

                    if (m_dump.CreateFinal($"{errorMsg.Text}{nl}{nl}----- More Info -----{nl}{nl}{descBox.Text}"))
                    {
                        dumpProgress.Value = 100;
                        m_tbarInfo.ProgressValue = 100;

                        MessageBox.Show(this, "Finished Saving Dump!", "Dump Complete!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(this, "Process Already Terminated!", "Error Saving Dump!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(this, "Process Already Terminated!", "Error Saving Dump!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            Close();
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (m_dump != null)
                    m_dump.DoProcessExit();
            }
            catch (Exception)
            {
                // Just ignore this it already died.
            }
        }
    }
}
