using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;
using MetroFramework;
using System.Threading;
using System.Drawing;
using System.Diagnostics;
using System.Reflection;

namespace IllTechLibrary.Util
{
    public class MsgDialogs
    {
        public const int LOGGER_LEVEL = 2;

        public static DialogResult ShowMetro(IWin32Window owner, String message)
        { return ShowMetro(owner, message, "Notification", 211); }

        public static DialogResult ShowMetro(IWin32Window owner, String message, int height)
        { return ShowMetro(owner, message, "Notification", height); }

        public static DialogResult ShowMetro(IWin32Window owner, String message, String title)
        { return ShowMetro(owner, message, title, MessageBoxButtons.OK, 211); }

        public static DialogResult ShowMetro(IWin32Window owner, String message, String title, int height)
        { return ShowMetro(owner, message, title, MessageBoxButtons.OK, height); }

        public static DialogResult ShowMetro(IWin32Window owner, String message, String title, MessageBoxButtons buttons)
        { return ShowMetro(owner, message, title, buttons, MessageBoxIcon.None, 211); }

        public static DialogResult ShowMetro(IWin32Window owner, String message, String title, MessageBoxButtons buttons, int height)
        { return ShowMetro(owner, message, title, buttons, MessageBoxIcon.None, height); }

        public static DialogResult ShowMetro(IWin32Window owner, String message, String title, MessageBoxButtons buttons, MessageBoxIcon icon)
        { return ShowMetro(owner, message, title, buttons, icon, MessageBoxDefaultButton.Button1, 211); }

        public static DialogResult ShowMetro(IWin32Window owner, String message, String title, MessageBoxButtons buttons, MessageBoxIcon icon, int height)
        { return ShowMetro(owner, message, title, buttons, icon, MessageBoxDefaultButton.Button1, height); }

        public static DialogResult ShowMetro(IWin32Window owner, String message, String title, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultbutton)
        {
            return ShowMetro(owner, message, title, buttons, icon, defaultbutton, 211);
        }

        public static DialogResult ShowMetro(IWin32Window owner, String message, String title, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultbutton, int height)
        {
            DialogResult _result = DialogResult.None;

            if (owner != null)
            {
                Form _owner = (owner as Form == null) ? ((UserControl)owner).ParentForm : (Form)owner;

                //int _minWidth = 500;
                //int _minHeight = 350;

                //if (_owner.Size.Width < _minWidth ||
                //    _owner.Size.Height < _minHeight)
                //{
                //    if (_owner.Size.Width < _minWidth && _owner.Size.Height < _minHeight) {
                //            _owner.Size = new Size(_minWidth, _minHeight);
                //    }
                //    else
                //    {
                //        if (_owner.Size.Width < _minWidth) _owner.Size = new Size(_minWidth, _owner.Size.Height);
                //        else _owner.Size = new Size(_owner.Size.Width, _minHeight);
                //    }

                //    int x = Convert.ToInt32(Math.Ceiling((decimal)(Screen.PrimaryScreen.WorkingArea.Size.Width / 2) - (_owner.Size.Width / 2)));
                //    int y = Convert.ToInt32(Math.Ceiling((decimal)(Screen.PrimaryScreen.WorkingArea.Size.Height / 2) - (_owner.Size.Height / 2)));
                //    _owner.Location = new Point(x, y);
                //}

                switch (icon)
                {
                    case MessageBoxIcon.Error:
                        SystemSounds.Hand.Play(); break;
                    case MessageBoxIcon.Exclamation:
                        SystemSounds.Exclamation.Play(); break;
                    case MessageBoxIcon.Question:
                        SystemSounds.Beep.Play(); break;
                    default:
                        SystemSounds.Asterisk.Play(); break;
                }

                MetroMessageBoxControl _control = new MetroMessageBoxControl();
                _control.BackColor = _owner.BackColor;
                _control.Properties.Buttons = buttons;
                _control.Properties.DefaultButton = defaultbutton;
                _control.Properties.Icon = icon;
                _control.Properties.Message = message.Replace("\\n", Environment.NewLine);                
                _control.Properties.Title = title;
                _control.Padding = new Padding(0, 0, 0, 0);
                _control.ControlBox = false;
                _control.ShowInTaskbar = true;
                _control.TopMost = false;
                _control.Icon = Icon.ExtractAssociatedIcon(Assembly.GetEntryAssembly().Location);
                _control.Text = title;
                //_owner.Controls.Add(_control);
                //if (_owner is IMetroForm)
                //{
                //    //if (((MetroForm)_owner).DisplayHeader)
                //    //{
                //    //    _offset += 30;
                //    //}
                //    _control.Theme = ((MetroForm)_owner).Theme;
                //    _control.Style = ((MetroForm)_owner).Style;
                //}

                _control.Size = new Size(_owner.Size.Width, height);
                _control.Location = new Point(_owner.Location.X, _owner.Location.Y + (_owner.Height - _control.Height) / 2);
                _control.ArrangeApperance();
                int _overlaySizes = Convert.ToInt32(Math.Floor(_control.Size.Height * 0.28));
                //_control.OverlayPanelTop.Size = new Size(_control.Size.Width, _overlaySizes - 30);
                //_control.OverlayPanelBottom.Size = new Size(_control.Size.Width, _overlaySizes);

                _control.ShowDialog();
                _control.BringToFront();
                _control.SetDefaultButton();

                Action<MetroMessageBoxControl> _delegate = new Action<MetroMessageBoxControl>(ModalState);
                IAsyncResult _asyncresult = _delegate.BeginInvoke(_control, null, _delegate);
                bool _cancelled = false;

                try
                {
                    while (!_asyncresult.IsCompleted)
                    { Thread.Sleep(10); Application.DoEvents(); }
                }
                catch
                {
                    _cancelled = true;

                    if (!_asyncresult.IsCompleted)
                    {
                        try { _asyncresult = null; }
                        catch { }
                    }

                    _delegate = null;
                }

                if (!_cancelled)
                {
                    _result = _control.Result;
                    //_owner.Controls.Remove(_control);
                    _control.Dispose(); _control = null;
                }

            }

            return _result;
        }

        private static void ModalState(MetroMessageBoxControl control)
        {
            while (control.Visible) { }
        }

        private static SpinLock DialogLock = new SpinLock();

        public static DialogResult ShowNoLog(String Caption, String Text, String Buttons, MsgTypes MsgType)
        {
            bool lockTry = false;
            DialogLock.TryEnter(ref lockTry);

            if (!lockTry)
                return DialogResult.None;

            DialogResult dr = DialogResult.None;

            MessageBoxButtons btns = MessageBoxButtons.OK;
            MessageBoxIcon icon = MessageBoxIcon.None;

            if (Buttons.ToLower().Contains("yesno"))
            {
                if (Buttons.ToLower().Contains("cancel"))
                {
                    btns = MessageBoxButtons.YesNoCancel;
                }
                else
                {
                    btns = MessageBoxButtons.YesNo;
                }
            }

            if (Buttons.ToLower().Contains("ok"))
            {
                btns = MessageBoxButtons.OK;
            }

            switch (MsgType)
            {
                case MsgTypes.ERROR:
                    icon = MessageBoxIcon.Error;
                    break;
                case MsgTypes.WARNING:
                    icon = MessageBoxIcon.Warning;
                    break;
                case MsgTypes.INFO:
                    icon = MessageBoxIcon.Information;
                    break;
            }

            if (Application.OpenForms.Count > 0)
            {
                dr = ShowMetro(Application.OpenForms[0], Text, Caption, btns, icon, MessageBoxDefaultButton.Button1);
                DialogLock.Exit();
                return dr;
            }
            else
            {
                dr = MessageBox.Show(Text, Caption, btns, icon, MessageBoxDefaultButton.Button1);
                DialogLock.Exit();
                return dr;
            }
        }

        public static DialogResult Show(String Caption, String Text, String Buttons, MsgTypes MsgType)
        {
            bool lockTry = false;
            DialogLock.TryEnter(ref lockTry);

            if (!lockTry)
                return DialogResult.None;

            DialogResult dr = DialogResult.None;

            MessageBoxButtons btns = MessageBoxButtons.OK;
            MessageBoxIcon icon = MessageBoxIcon.None;

            if (Buttons.ToLower().Contains("yesno"))
            {
                if (Buttons.ToLower().Contains("cancel"))
                {
                    btns = MessageBoxButtons.YesNoCancel;
                }
                else
                {
                    btns = MessageBoxButtons.YesNo;
                }
            }

            if (Buttons.ToLower().Contains("ok"))
            {
                btns = MessageBoxButtons.OK;
            }

            switch (MsgType)
            {
                case MsgTypes.ERROR:
                    icon = MessageBoxIcon.Error;
                    if (LOGGER_LEVEL >= 0)
                        LogError(Text);
                    break;
                case MsgTypes.WARNING:
                    icon = MessageBoxIcon.Warning;
                    if (LOGGER_LEVEL >= 1)
                        LogWarning(Text);
                    break;
                case MsgTypes.INFO:
                    icon = MessageBoxIcon.Information;
                    if (LOGGER_LEVEL >= 2)
                        LogInfo(Text);
                    break;
            }

            if (Application.OpenForms.Count > 0)
            {
                dr = ShowMetro(Application.OpenForms[0], Text, Caption, btns, icon, MessageBoxDefaultButton.Button1);
                DialogLock.Exit();
                return dr;
            }
            else
            {
                dr =  MessageBox.Show(Text, Caption, btns, icon, MessageBoxDefaultButton.Button1);
                DialogLock.Exit();
                return dr;
            }
        }

        public static void LogInfo(string Text)
        {
            if (!File.Exists("IllTech.log"))
            {
                FileStream f = File.Create("IllTech.log");
                f.Close();
                f.Dispose();
            }

            TextWriter tw = new StreamWriter(File.Open("IllTech.log", FileMode.Append, FileAccess.Write));

            tw.WriteLine(String.Format("[INFO] {0}@{1} :: {2}", DateTime.Now.ToString("MM/dd/yyyy"), DateTime.Now.ToString("hh:mm:ss tt"), Text));
            tw.Flush();

            tw.Close();
            tw.Dispose();
        }

        public static void LogWarning(string Text)
        {
            if (!File.Exists("IllTech.log"))
            {
                FileStream f = File.Create("IllTech.log");
                f.Close();
                f.Dispose();
            }

            TextWriter tw = new StreamWriter(File.Open("IllTech.log", FileMode.Append, FileAccess.Write));

            tw.WriteLine(String.Format("[WARNING] {0}@{1} :: {2}", DateTime.Now.ToString("MM/dd/yyyy"), DateTime.Now.ToString("hh:mm:ss tt"), Text));
            tw.Flush();

            tw.Close();
            tw.Dispose();
        }

        public static void LogError(string Text)
        {
            if (!File.Exists("IllTech.log"))
            {
                FileStream f = File.Create("IllTech.log");
                f.Close();
                f.Dispose();
            }

            TextWriter tw = new StreamWriter(File.Open("IllTech.log", FileMode.Append, FileAccess.Write));

            tw.WriteLine(String.Format("[ERROR] {0}@{1} :: {2}", DateTime.Now.ToString("MM/dd/yyyy"), DateTime.Now.ToString("hh:mm:ss tt"), Text));
            tw.Flush();

            tw.Close();
            tw.Dispose();
        }

        public enum MsgTypes
        {
            ERROR,
            WARNING,
            INFO,
        }
    }
}
