using IllTechLibrary;
using IllTechLibrary.Enums;
using IllTechLibrary.Localization;
using IllTechLibrary.Settings;
using IllTechLibrary.Util;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LCMT
{
    /// <summary>
    /// Main base class for tool windows
    /// </summary>
#if CUSTOM_MDI
    public class LCToolFrm : MetroFramework.Forms.MetroForm
#else
    public class LCToolFrm : Form
#endif
    {
        private static FieldInfo[] versionprops = typeof(ToolVersions).GetFields(BindingFlags.NonPublic | BindingFlags.Static);

        private CancellationTokenSource taskToken = new CancellationTokenSource();
        private ConcurrentBag<Task> bag = new ConcurrentBag<Task>();

        private string m_toolID = string.Empty;
        private FormDataSync m_sync = new FormDataSync();

        private Stopwatch m_profile = new Stopwatch();

        /// <summary>
        /// Required for designer support.
        /// </summary>
        public LCToolFrm()
        {
            m_toolID = string.Empty;
        }

        /// <summary>
        /// Main constructor for a LCToolFrm instance.
        /// </summary>
        /// <param name="ToolID"></param>
        public LCToolFrm(string ToolID)
        {
            m_toolID = ToolID;
        }

        /// <summary>
        /// Attach to the mouse down of ever child control.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            Translate(StringTable.UIStrings);

            base.OnShown(e);

            ParallelOptions po = new ParallelOptions();

            po.MaxDegreeOfParallelism = Environment.ProcessorCount;

            if (!DesignMode)
            {
                Parallel.ForEach(ControlUtil.GetAll(this), po, (thisControl) =>
                {
                    thisControl.MouseDown += I_MouseDown;
                });
            }
        }

        /// <summary>
        /// Override for window activation
        /// </summary>
        /// <param name="sender">the control that got the message</param>
        /// <param name="e">the mouse message</param>
        private void I_MouseDown(object sender, MouseEventArgs e)
        {
            if (MdiParent.ActiveMdiChild == this)
                return;

            BringToFront();
        }

        /// <summary>
        /// Eventh method to handle common LCToolFrm initilization code.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            // Call the inherited forms Load callback
            // This should execute first due to initilization code
            base.OnLoad(e);

            if(m_toolID != string.Empty)
            {
                Preferences.SetWindow(m_toolID, this);

                string tid = m_toolID.Replace("_", "").ToLower();
                FieldInfo fi = versionprops.Where(p => p.Name.ToLower().Equals(tid)).FirstOrDefault();

                if (fi != null)
                {
                    Text = $"{Text} - {fi.GetValue(null).ToString()}";
                }
                else
                {
                    Text = $"{Text} - (Version Not Found!)";
                }
            }
        }

        /// <summary>
        /// Eventh method to handle common LCToolFrm cleanup code.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            // if our token is null we already did this code in OnDisconnect
            if (taskToken != null)
            {
                // Block input to the window
                Enabled = false;

                // cancel all pending tasks
                CancelTasks();

                // wait for tasks to respond
                while (HasPendingTasks() == EPendingTaskState.Active)
                {
                    Application.DoEvents();
                }

                taskToken.Dispose();
                taskToken = null;
            }

            // This is a valid tool window
            if(m_toolID != string.Empty)
                Preferences.WriteWindow(m_toolID, this);

            // Call the inherited forms OnFormClosing callback
            // This should call last due to most clean up being in disconnect
            base.OnClosing(e);
        }

        /// <summary>
        /// Return a string representing this tools unique identifer.
        /// </summary>
        /// <returns></returns>
        public string GetToolID()
        {
            return m_toolID;
        }

        /// <summary>
        /// A virtual method to be overriden by any inheriting class.
        /// </summary>
        public virtual void OnConnect()
        {
            if(taskToken == null)
                taskToken = new CancellationTokenSource();
        }

        /// <summary>
        /// A virtual method to be overriden by any inheriting class.
        /// </summary>
        public virtual void OnDisconnect()
        {
            // Prevent an odd condition where the window might close first 
            // Then try to disconnect on another thread (require lock to make thread safe?)
            if (taskToken != null)
            {
                // Block input to the window
                Enabled = false;

                // cancel all pending tasks
                CancelTasks();

                // wait for tasks to respond
                while (HasPendingTasks() == EPendingTaskState.Active)
                {
                    Application.DoEvents();
                }

                taskToken.Dispose();
                taskToken = null;

                Enabled = true;
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0201)
            {
                Point target = new Point((m.WParam.ToInt32() << 8) & 0xFF, (m.WParam.ToInt32() << 0) & 0xFF);

                Control ctrl = GetChildAtPoint(target);

                if (ctrl != null && ActiveForm != this)
                    BringToFront();
            }

            try
            {
                base.WndProc(ref m);
            }
            catch (ObjectDisposedException)
            {

            }
        }

        /// <summary>
        /// Get Database Data Connection.
        /// </summary>
        /// <returns></returns>
        public IllSQL DataCon
        {
            get
            {
                if (MultiFrm.GetInstance().GetDataDb().IsLocked() && !MultiFrm.GetInstance().GetPostDb().IsLocked())
                {
                    MultiFrm.GetInstance().GetPostDb().SwitchDB(IllSQL.GetToolDB("DataDB"));
                    return MultiFrm.GetInstance().GetPostDb();
                }

                return MultiFrm.GetInstance().GetDataDb();
            }
        }

        /// <summary>
        /// Get Database Auth Connection.
        /// </summary>
        /// <returns></returns>
        public IllSQL AuthCon
        {
            get
            {
                if (MultiFrm.GetInstance().GetAuthDb().IsLocked() && !MultiFrm.GetInstance().GetPostDb().IsLocked())
                {
                    MultiFrm.GetInstance().GetPostDb().SwitchDB(IllSQL.GetToolDB("AuthDB"));
                    return MultiFrm.GetInstance().GetPostDb();
                }


                return MultiFrm.GetInstance().GetAuthDb();
            }
        }

        /// <summary>
        /// Get Database Char Connection.
        /// </summary>
        /// <returns></returns>
        public IllSQL CharCon
        {
            get
            {
                if(MultiFrm.GetInstance().GetCharDb().IsLocked() && !MultiFrm.GetInstance().GetPostDb().IsLocked())
                {
                    MultiFrm.GetInstance().GetPostDb().SwitchDB(IllSQL.GetToolDB("CharDB"));
                    return MultiFrm.GetInstance().GetPostDb();
                }

                return MultiFrm.GetInstance().GetCharDb();
            }
        }

        /// <summary>
        /// Get Database Post Connection.
        /// </summary>
        /// <returns></returns>
        public IllSQL PostCon
        {
            get { return MultiFrm.GetInstance().GetPostDb(); }
        }

        /// <summary>
        /// Task Token property to Signal Cancelation to Tasks.
        /// </summary>
        public CancellationToken Token
        {
            get { return taskToken.Token; }
        }

        /// <summary>
        /// Create and start an async task no argument
        /// </summary>
        /// <param name="action">the action whichs supports cancelation</param>
        /// <param name="callback">callback method optional</param>
#pragma warning disable
        public Task AddTask(Action action, Action callback = null)
        {
            Task T;

            bag.Add(T = Task.Run(async () => {
                action();
                if (!taskToken.IsCancellationRequested)
                {
                    callback?.Invoke();
                }
            }, Token));

            return T;
        }

        /// <summary>
        /// Create and start an async task.
        /// </summary>
        /// <param name="action">the action which supports cancelation</param>
        /// <param name="arg">method arguement optional</param>
        /// <param name="callback">callback method optional</param>
        public Task AddTask(Action<object> action, object arg = null, Action callback = null)
        {
            Task T;

            bag.Add(T = Task.Run(async () => {
                action(arg);
                if (!taskToken.IsCancellationRequested)
                {
                    callback?.Invoke();
                }
            }, Token));

            return T;
        }

        /// <summary>
        /// Nice way to wait on a handfull of tasks
        /// </summary>
        /// <param name="tasks">Task list to wait on</param>
        /// <returns>if we have any tasks</returns>
        public void AwaitTasks(List<Task> tasks)
        {
            Task.WaitAll(tasks.ToArray());
        }

        /// <summary>
        /// Method to define interruptible blocks of code
        /// </summary>
        /// <param name="action">function body</param>
        public ETaskBlockResult TaskBlock(Action action)
        {
            if (!IsPendingCancel())
            {
                action();
            }
            else
            {
                return ETaskBlockResult.Canceled;
            }

            return ETaskBlockResult.Complete;
        }
#pragma warning restore

        /// <summary>
        /// Alert all tasks to cancel.
        /// </summary>
        public EPendingTaskState HasPendingTasks()
        {
            if (bag.Where(p => p.IsCompleted.Equals(false)).Count() != 0)
            {
                return EPendingTaskState.Active;
            }

            return EPendingTaskState.Finished;
        }

        /// <summary>
        /// Returns the state of task cancelation.
        /// </summary>
        /// <returns></returns>
        private bool IsPendingCancel()
        {
            if (taskToken == null)
                return false;

            return taskToken.IsCancellationRequested;
        }

        /// <summary>
        /// Wrapper around the cancel token to make it more readable
        /// </summary>
        private void CancelTasks()
        {
            taskToken?.Cancel();
        }
        
        /// <summary>
        /// Signal tracker to stop watching contents
        /// </summary>
        public void BeginChangeContent()
        {
            m_sync.Clear();
            m_sync.SetLoading(true);
        }

        /// <summary>
        /// Signal the tracker we want to start watching again
        /// </summary>
        public void EndChangeContent()
        {
            m_sync.SetLoading(false);
        }

        /// <summary>
        /// Register controls of certain type with the tracker
        /// </summary>
        public void TrackControls<TControlType>(Control parent)
        {
            Type t = typeof(TControlType);

            if(t == typeof(TextBox))
            {
                m_sync.RegisterTextBoxes(parent);
            }
        }

        /// <summary>
        /// Removes controls of certain type from the tracker
        /// </summary>
        /// <typeparam name="TControlType">The type of control to unfollow</typeparam>
        public void UntrackControls<TControlType>()
        {
            Type t = typeof(TControlType);

            if(t == typeof(TextBox))
            {
                m_sync.RemoveTextBoxes();
            }
        }

        /// <summary>
        /// Update commited changes to the form indicator
        /// </summary>
        public void CommitChanges()
        {
            m_sync.CommitChanges();
        }

        /// <summary>
        /// Get a bool denoting any changes in the form contents
        /// </summary>
        /// <returns>true if changes exist</returns>
        public bool FormHasChanges()
        {
            return m_sync.HasChanges();
        }

        /// <summary>
        /// Store the previous set event for unsetting
        /// </summary>
        private EventHandler m_DataChanged = null;

        /// <summary>
        /// Add a method for subscribing the data changed event 
        /// with removing previous callback automatically
        /// </summary>
        /// <param name="eventHandler"></param>
        public void SetOnDataChanged(EventHandler eventHandler)
        {
            if(m_DataChanged != null)
            {
                m_sync.OnDataChange -= m_DataChanged;
            }
            else
            {
                m_DataChanged = eventHandler;
                m_sync.OnDataChange += eventHandler;
            }
        }

        /// <summary>
        /// Begin a simple profile timer operation
        /// </summary>
        public void BeginProfile()
        {
            // Stop the profiler from being restarted by another thread
            while (m_profile.IsRunning)
                Thread.Sleep(10);

            m_profile.Reset();
            m_profile.Start();
        }

        /// <summary>
        /// Stop the profile timer
        /// </summary>
        /// <param name="report">true should display message dialog</param>
        /// <returns>Elapsed time in milliseconds</returns>
        public long StopProfile(bool report = false)
        {
            m_profile.Stop();

            if (report)
                MsgDialogs.ShowNoLog("Profile Operation", $"The operate completed in {m_profile.ElapsedMilliseconds}ms", "ok", MsgDialogs.MsgTypes.INFO);

            return m_profile.ElapsedMilliseconds;
        }

        /// <summary>
        /// Translate Code
        /// </summary>
        /// <param name="st"></param>
        private void Translate(StringTable st)
        {
            if (!DesignMode)
            {
                ToolStrip ts = ControlUtil.GetToolStrip(this);
                if (ts != null)
                    RecurseItems(st, ts.Items.OfType<ToolStripMenuItem>());

                IEnumerable<Control> controls = ControlUtil.GetAll(this);

                foreach (Control at in controls)
                {
                    if (st.HasKey(at.Name))
                        at.Text = st.Get(at.Name);
                }
            }
        }

        /// <summary>
        /// Translate Menu
        /// </summary>
        /// <param name="st"></param>
        /// <param name="col"></param>
        private void RecurseItems(StringTable st, IEnumerable<ToolStripMenuItem> col)
        {
            foreach (ToolStripMenuItem item in col)
            {
                if (st.HasKey(item.Name))
                {
                    item.Text = st.Get(item.Name);
                }

                RecurseItems(st, item.DropDownItems.OfType<ToolStripMenuItem>());
            }
        }
    }
}
