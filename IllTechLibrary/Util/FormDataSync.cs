using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IllTechLibrary.Util
{
    /// <summary>
    /// Class for reporting and syncing data with a form.
    /// </summary>
    public class FormDataSync
    {
        private struct DataControlObject
        {
            public Control member;
            public string initialText;

            public DataControlObject(Control member, string initialText)
            {
                this.member = member;
                this.initialText = initialText;
            }
        }

        /// <summary>
        /// Id to tag controls as they move about the system
        /// </summary>
        private int s_id = 0;

        /// <summary>
        /// State for keeping track of reloading of window data
        /// </summary>
        private bool m_reloading = false;

        /// <summary>
        /// Colors for various states
        /// </summary>
        private readonly Color different = Color.PaleVioletRed;
        private readonly Color original = Color.FromKnownColor(KnownColor.Window);
        private readonly Color updated = Color.LightGreen;

        /// <summary>
        /// Tracking object for inital states
        /// </summary>
        private List<DataControlObject> m_initalValues = new List<DataControlObject>();
        
        /// <summary>
        /// Tracking object for changed states
        /// </summary>
        private List<DataControlObject> m_changedValues = new List<DataControlObject>();

        #region ControlRegisters
        /// <summary>
        /// Track all textbox objects under the parent control
        /// </summary>
        /// <param name="parent">Parent to search for controls to track</param>
        public void RegisterTextBoxes(Control parent)
        {
            foreach (Control item in ControlUtil.GetControlsOfType<TextBox>(parent))
            {
                item.Tag = s_id++;
                m_initalValues.Add(new DataControlObject(item, item.Text));
                item.TextChanged += ControlTextChanged;
            }

            SetLoading(true);
        }

        /// <summary>
        /// Remove all tracked text objects
        /// </summary>
        public void RemoveTextBoxes()
        {
            foreach(DataControlObject item in m_changedValues)
            {
                item.member.TextChanged -= ControlTextChanged;
            }

            m_initalValues.Clear();
            m_changedValues.Clear();
        }
        #endregion

        #region ControlEvents
        /// <summary>
        /// State object to tell the callback if we have unsaved data
        /// and if its safe to switch data or not
        /// </summary>
        private class OnFormChangeEventArgs : EventArgs
        {
            public bool LockState;

            public OnFormChangeEventArgs(bool LockState)
            {
                this.LockState = LockState;
            }
        }

        /// <summary>
        /// This is really just for future ideas not sure if it will be used
        /// </summary>
        private void NotifyChange()
        {
            if (HasChanges())
            {
                // Tell the parent we need security around changed values
                OnDataChange?.Invoke(this, new OnFormChangeEventArgs(true));
            }
            else
            {
                // Tell the parent we can undo any security around changed values
                OnDataChange?.Invoke(this, new OnFormChangeEventArgs(false));
            }
        }

        /// <summary>
        /// Just manually clear out all change data
        /// </summary>
        public void Clear()
        {
            m_reloading = true;

            for(int i = 0; i < m_initalValues.Count; i++)
            {
                m_initalValues[i].member.Text = string.Empty;
                m_initalValues[i].member.BackColor = original;
            }

            m_changedValues.Clear();

            m_reloading = false;
        }

        /// <summary>
        /// Change control color based on text value changed
        /// </summary>
        /// <param name="sender">Control with text to watch</param>
        /// <param name="e">Event arguement</param>
        private void ControlTextChanged(object sender, EventArgs e)
        {
            Control textControl = (Control)sender;

            if(m_reloading)
            {
                m_initalValues.RemoveAll(p => p.member.Tag.Equals(textControl.Tag));
                m_changedValues.RemoveAll(p => p.member.Tag.Equals(textControl.Tag));

                textControl.BackColor = original;

                m_initalValues.Add(new DataControlObject(textControl, textControl.Text));
            }
            else
            {
                int initIdx = m_initalValues.FindIndex(p => p.member.Tag.Equals(textControl.Tag));
                int changedIdx = m_changedValues.FindIndex(p => p.member.Tag.Equals(textControl.Tag));

                // This is a new change
                if(changedIdx == -1)
                {
                    textControl.BackColor = different;
                    m_changedValues.Add(new DataControlObject(textControl, textControl.Text));
                }
                else
                {
                    // Remove change if new value reverts to old value
                    if(m_initalValues[initIdx].initialText == textControl.Text)
                    {
                        textControl.BackColor = original;
                        m_changedValues.RemoveAt(changedIdx);
                    }
                    else
                    {
                        // Replace the changed value this might be helpful in the future
                        // for showing changes in a dialog we can display
                        m_changedValues[changedIdx] = new DataControlObject(textControl, textControl.Text);
                    }
                }
            }

            NotifyChange();
        }
        #endregion

        /// <summary>
        /// Only on a save operation
        /// </summary>
        public void CommitChanges()
        {
            foreach(DataControlObject item in m_changedValues)
            {
                int idx = m_initalValues.FindIndex(p => p.member.Tag.Equals(item.member.Tag));

                // Set background color add new instance
                item.member.BackColor = updated;
                m_initalValues[idx] = new DataControlObject(item.member, item.member.Text);
            }

            m_changedValues.Clear();
        }

        /// <summary>
        /// Update all initial values only called on SetLoading(false)
        /// </summary>
        private void CommitValues()
        {
            for (int i = 0; i < m_initalValues.Count; i++)
            {
                m_initalValues[i] = new DataControlObject(m_initalValues[i].member, m_initalValues[i].member.Text);
            }
        }

        /// <summary>
        /// Set the window reloading state
        /// </summary>
        /// <param name="state">True if window is reloading data</param>
        public void SetLoading(bool state)
        {
            // Update initial values to match
            if (state == false)
                CommitValues();

            m_reloading = state;
        }

        /// <summary>
        /// Prompt user to save changes if any are found
        /// </summary>
        /// <returns>true if changes are detected</returns>
        public bool HasChanges()
        {
            return m_changedValues.Count > 0;
        }

        /// <summary>
        /// Event handler for alerting the parent this data has changed
        /// </summary>
        public EventHandler OnDataChange;
    }
}
