using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IllTechLibrary;
using IllTechLibrary.Serialization;
using IllTechLibrary.Settings;
using IllTechLibrary.SharedStructs.Strings;
using IllTechLibrary.Util;

namespace LCMT.StringTool
{
    public partial class StringFrm : LCToolFrm
    {
        public static String StringToolID = "STRING_TOOL";

        private List<ClientStrings> m_strings = new List<ClientStrings>();
        private Deserialize<ClientStrings> m_desc = new Deserialize<ClientStrings>("t_string");

        private int selectedIdx = -1;

        private double loadAllTime;

        private BackgroundWorker loadAll = new BackgroundWorker();

        private String LocalString
        {
            get { return String.Format("a_string_{0}", Core.LangCode); }
        }

        public StringFrm() : base(StringToolID)
        {
            InitializeComponent();
        }

        public override void OnConnect()
        {
            m_strings = new List<ClientStrings>();
            m_desc = new Deserialize<ClientStrings>("t_string");

            DoLoadAll();
        }

        private void DoLoadAll()
        {
            lb_strings.Items.Clear();

            ProgressWheel.Visible = true;

            loadAll.DoWork += OnLoadAll;
            loadAll.RunWorkerCompleted += OnLoadAllComplete;
            loadAll.RunWorkerAsync();
        }

        private void OnLoadAll(object sender, DoWorkEventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            Transactions<ClientStrings> trans = new Transactions<ClientStrings>(DataCon);

            m_strings = trans.ExecuteQuery(m_desc).OrderBy(p => p.a_index).ToList();

            TimeSpan time = sw.Elapsed;
            sw.Stop();

            loadAllTime = time.TotalMilliseconds;
        }

        private void OnLoadAllComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            DoRebuildList();
            ProgressWheel.Visible = false;

            GeneralStatsLabel.Text = $"Stats: Total Strings - {m_strings.Count}, Load Time: {loadAllTime} ms";
        }

        public override void OnDisconnect()
        {
            m_strings.Clear();

            tb_search.Clear();
            tb_selected.Clear();

            lb_strings.Items.Clear();

            selectedIdx = -1;
        }

        private void DoRebuildList()
        {
            lb_strings.Items.Clear();

            lb_strings.Items.AddRange(m_strings.Select(p => p.a_index + " - " + (string)m_desc.SetData(p)[LocalString]).ToArray());

            /*foreach (ClientStrings a in m_strings)
            {
                lb_strings.Items.Add(String.Format("{0} - {1}", a.a_index, (String)m_desc.SetData(a)[LocalString]));
            }*/
        }

        private void OnOpen(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open *.lod";
            ofd.Filter = "String File(*.lod)|*.lod";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Dictionary<int, String> strings = new Dictionary<int, string>();

                BinaryReader br = new BinaryReader(ofd.OpenFile());

                int nIdx = br.ReadInt32();
                int lIdx = br.ReadInt32();

                while (br.BaseStream.Position != br.BaseStream.Length)
                {
                    int idx = br.ReadInt32();

                    int len = br.ReadInt32();

                    String str = "";

                    if (len != 0)
                    {
                        str = Core.Encoder.GetString(br.ReadBytes(len));
                    }

                    strings.Add(idx, str);

                    if (idx + 1 == nIdx)
                        break;
                }

                br.Close();
                br.Dispose();

                DoRebuildList();

                tb_selected.Text = String.Empty;
            }
        }

        private void OnAddClick(object sender, EventArgs e)
        {
            if (m_strings.Count <= 0)
            {
                return;
            }
            else
            {
                m_desc.SetValue("a_index", m_strings.Last().a_index + 1);
                m_desc.SetValue(LocalString, "New String");

                m_strings.Add(m_desc.Serialize());

                new Transactions<ClientStrings>(DataCon).ExecuteQuery(m_desc, QUERY_TYPE.INSERT);
            }

            DoRebuildList();

            lb_strings.SelectedIndex = lb_strings.Items.Count - 1;
        }

        private string GetString(int idx)
        {
            try
            {
                ClientStrings c = m_strings.Find(p => p.a_index.Equals(idx));

                if(c != null)
                {
                    return (String)m_desc.SetData(c)[LocalString];
                }

                return String.Empty;
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        private void OnSelectionChanged(object sender, EventArgs e)
        {
            if (lb_strings.SelectedIndex == -1)
            {
                selectedIdx = -1;
                return;
            }

            selectedIdx = int.Parse(lb_strings.Items[lb_strings.SelectedIndex].ToString().Split('-')[0]);

            tb_selected.Text = GetString(selectedIdx);
        }

        private void OnUpdate(object sender, EventArgs e)
        {
            if (selectedIdx != -1 || m_strings.Count <= 0)
            {
                int foundIdx = m_strings.FindIndex(p => p.a_index.Equals(selectedIdx));

                if (foundIdx != -1)
                {
                    m_desc.SetData(m_strings[foundIdx])[LocalString] = tb_selected.Text;
                    m_strings[foundIdx] = m_desc.Serialize();
                    lb_strings.Items[lb_strings.SelectedIndex] = String.Format("{0} - {1}", selectedIdx, tb_selected.Text);

                    m_desc.SetKey("a_index");

                    new Transactions<ClientStrings>(DataCon).ExecuteQuery(m_desc, QUERY_TYPE.UPDATE);
                }
            }
        }

        private void OnDelete(object sender, EventArgs e)
        {
            if (selectedIdx != -1 || m_strings.Count <= 0)
            {
                int foundIdx = m_strings.FindIndex(p => p.a_index.Equals(selectedIdx));

                if (foundIdx != -1)
                {
                    m_desc.SetData(m_strings[foundIdx]);
                    m_desc.SetKey("a_index");

                    new Transactions<ClientStrings>(DataCon).ExecuteQuery(m_desc, QUERY_TYPE.DELETE);

                    m_strings.RemoveAt(foundIdx);
                    selectedIdx = -1;

                    DoRebuildList();

                    tb_selected.Text = String.Empty;
                }
            }
        }

        private void OnSave(object sender, EventArgs e)
        {
            if (m_strings.Count <= 0)
                return;

            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Title = "Save Client String Lod";
            sfd.Filter = "Client String File(*.lod)|*.lod";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (m_strings != null)
                {
                    BinaryWriter bw = new BinaryWriter(sfd.OpenFile());

                    bw.Write(m_strings.Last().a_index + 1);
                    bw.Write(m_strings.Last().a_index + 1);

                    foreach (ClientStrings a in m_strings)
                    {
                        m_desc.SetData(a);

                        String val = (String)m_desc[LocalString];

                        bw.Write(a.a_index);
                        bw.Write(Core.Encoder.GetByteCount(val));
                        bw.Write(Core.Encoder.GetBytes(val));
                    }

                    bw.Close();
                    bw.Dispose();

                    MessageBox.Show("Save Complete!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void OnExit(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OnSearchChanged(object sender, EventArgs e)
        {
            if (m_strings.Count <= 0)
                return;

            if (tb_search.Text == String.Empty)
            {
                lb_strings.Items.Clear();
                lb_strings.Items.AddRange(m_strings.Select(p => p.a_index + " - " + (String)m_desc.SetData(p)[LocalString]).ToArray());
                lb_strings.SelectedIndex = -1;
            }
            else
            {
                List<ClientStrings> itCollection = new List<ClientStrings>();

                itCollection = m_strings.FindAll(p => ((String)m_desc.SetData(p)[LocalString]).ToLower().Contains(tb_search.Text.ToLower()) || p.a_index.ToString().Equals(tb_search.Text));

                if (itCollection.Count != 0)
                {
                    lb_strings.Items.Clear();
                    lb_strings.Items.AddRange(itCollection.Select(p => p.a_index + " - " + (String)m_desc.SetData(p)[LocalString]).ToArray());
                    lb_strings.SelectedIndex = -1;
                }
            }
        }
    }
}