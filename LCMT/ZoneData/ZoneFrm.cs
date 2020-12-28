using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using IllTechLibrary;

namespace LCMT.ZoneData
{
    public partial class ZoneFrm : LCToolFrm
    {
        class stInfo
        {
            public int nZoneType;
            /* Number of sub zones */
            public int nExtraCnt;
            /* Zone name */
            public int nString;
            public string wldFileName;
            public string texName1;
            public string texName2;
            public float fLoadingStep;
            public float fTer_Lodmul;
        }

        class stExtraInfo
        {
            /* Sub zone name strings */
            public int[] nString = new int[30];
        }

        private List<stInfo> zoneList;
        private List<stExtraInfo> extraList;
        private Dictionary<int, string> m_stringTable = new Dictionary<int, string>();

        public static String ZoneToolID = "ZONE_TOOL";

        private String lastSaveFile = String.Empty;

        public ZoneFrm() : base(ZoneToolID)
        {
            InitializeComponent();
        }

        private string GetString(int strID)
        {
            if (strID == 0)
                return "";

            if(m_stringTable.Count == 0)
            {
                OpenFileDialog ofd = new OpenFileDialog();

                ofd.Title = "Select String Table (strClient_us.lod)";
                ofd.Filter = "Client String Files (*.lod)|*.lod";

                if (ofd.ShowDialog() != DialogResult.OK)
                    return "";

                BinaryReader br = new BinaryReader(File.Open(ofd.FileName, FileMode.Open));

                int nRow = br.ReadInt32();
                int nMax = br.ReadInt32();

                while(br.BaseStream.Position != br.BaseStream.Length)
                {
                    int nidx = br.ReadInt32();

                    int len = br.ReadInt32();

                    if(len > 0)
                    {
                        string text = Core.Encoder.GetString(br.ReadBytes(len));

                        m_stringTable.Add(nidx, text);
                    }
                }

                br.Close();
                br.Dispose();
            }

            string value;

            bool ret = m_stringTable.TryGetValue(strID, out value);

            return ret == true ? value : "NAME NOT FOUND";
        }

        private void RebuildLists()
        {
            lb_zoneList.Items.Clear();
            lb_zoneExtra.Items.Clear();

            for(int i = 0; i < zoneList.Count; i++)
            {
                lb_zoneList.Items.Add(GetString(zoneList[i].nString));
            }
        }

        private void OnLoadZoneData(string zonefile)
        {
            if (!File.Exists(zonefile))
                return;

            BinaryReader br = new BinaryReader(File.Open(zonefile, FileMode.Open));

            int zoneCount = br.ReadInt32();

            zoneList = new List<stInfo>();

            for(int i = 0; i < zoneCount; i++)
            {
                zoneList.Add(new stInfo());

                zoneList[i].nZoneType = br.ReadInt32();
                zoneList[i].nExtraCnt = br.ReadInt32();
                zoneList[i].nString = br.ReadInt32();

                zoneList[i].wldFileName = Core.Encoder.GetString(br.ReadBytes(128));
                zoneList[i].texName1 = Core.Encoder.GetString(br.ReadBytes(64));
                zoneList[i].texName2 = Core.Encoder.GetString(br.ReadBytes(64));

                zoneList[i].fLoadingStep = br.ReadSingle();
                zoneList[i].fTer_Lodmul = br.ReadSingle();
            }

            int zoneExtraCount = br.ReadInt32();
            extraList = new List<stExtraInfo>();

            for(int i = 0; i < zoneExtraCount; i++)
            {
                extraList.Add(new stExtraInfo());

                for (int j = 0; j < 30; j++)
                {
                    extraList[i].nString[j] = br.ReadInt32();
                }
            }

            br.Close();
            br.Dispose();

            RebuildLists();
        }

        private void OnOpenZoneData(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "ZoneData Binary (*.bin)|*.bin";
            ofd.Title = "Open ZoneData File";

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                OnLoadZoneData(ofd.FileName);
            }
        }

        private void OnZoneListSelectionChange(object sender, EventArgs e)
        {
            ListBox lb = (ListBox)sender;

            if (lb.SelectedIndex == -1)
                return;

            int idx = lb.SelectedIndex;

            tb_zoneType.Text = zoneList[idx].nZoneType.ToString();
            tb_zoneFile.Text = zoneList[idx].wldFileName;
            tb_texName1.Text = zoneList[idx].texName1;
            tb_texName2.Text = zoneList[idx].texName2;
            tb_loadSteps.Text = zoneList[idx].fLoadingStep.ToString();
            tb_loadMul.Text = zoneList[idx].fTer_Lodmul.ToString();

            lb_zoneExtra.Items.Clear();

            for (int j = 0; j < zoneList[idx].nExtraCnt; ++j)
            {
                String test;

                if (extraList[idx].nString[j] == 0)
                {
                    test = "NONE";
                }
                else
                {
                    test = GetString(extraList[idx].nString[j]);
                }

                lb_zoneExtra.Items.Add(test);
            }
        }

        private void OnDoDeleteSubZone(object sender, EventArgs e)
        {
            int zoneIdx = lb_zoneList.SelectedIndex;

            if (zoneIdx == -1)
                return;

            int idx = lb_zoneExtra.SelectedIndex;

            if (idx == -1)
                return;

            extraList[zoneIdx].nString[idx] = 0;
            lb_zoneExtra.Items[idx] = "NONE";
        }

        private void OnSelectExtraName(object sender, EventArgs e)
        {
            int zoneIdx = lb_zoneList.SelectedIndex;

            if (zoneIdx == -1)
                return;

            int idx = lb_zoneExtra.SelectedIndex;

            if (idx == -1)
                return;

            StringSelect ss = new StringSelect(m_stringTable);

            ss.ShowDialog();

            string name = ss.Selected();

            if (name == "NONE")
                return;

            extraList[zoneIdx].nString[idx] = int.Parse(name);
            lb_zoneExtra.Items[idx] = GetString(int.Parse(name));
        }

        private void OnDoAddSubZone(object sender, EventArgs e)
        {
            int zoneIdx = lb_zoneList.SelectedIndex;

            if (zoneIdx == -1)
                return;

            if (zoneList[zoneIdx].nExtraCnt == 30)
            {
                MessageBox.Show("No Zone Extra Slots Availiable Limit 30.");
                return;
            }

            zoneList[zoneIdx].nExtraCnt += 1;

            extraList[zoneIdx].nString[zoneList[zoneIdx].nExtraCnt - 1] = 0;

            OnZoneListSelectionChange(lb_zoneList, null);
        }

        private void OnDoAddZone(object sender, EventArgs e)
        {
            stInfo st = new stInfo();

            st.nZoneType = 0;
            st.nString = 0;
            st.nExtraCnt = 0;
            st.fTer_Lodmul = 1.0f;
            st.fLoadingStep = 13.0f;
            st.wldFileName = "";
            st.texName1 = "";
            st.texName2 = "";

            zoneList.Add(st);

            stExtraInfo extra = new stExtraInfo();

            for (int i = 0; i < 30; i++)
                extra.nString[i] = 0;

            extraList.Add(extra);

            RebuildLists();

            lb_zoneList.SelectedIndex = lb_zoneList.Items.Count - 1;
        }

        private void OnSelectZoneName(object sender, EventArgs e)
        {
            int zoneIdx = lb_zoneList.SelectedIndex;

            if (zoneIdx == -1)
                return;

            StringSelect ss = new StringSelect(m_stringTable);

            ss.ShowDialog();

            string id = ss.Selected();

            if (id == "NONE")
                return;

            zoneList[zoneIdx].nString = int.Parse(id);
            lb_zoneList.Items[zoneIdx] = GetString(int.Parse(id));
        }

        private void OnDoDelete(object sender, EventArgs e)
        {
            int zoneIdx = lb_zoneList.SelectedIndex;

            if (zoneIdx == -1)
                return;

            zoneList.RemoveAt(zoneIdx);
            extraList.RemoveAt(zoneIdx);

            RebuildLists();

            if (lb_zoneList.Items.Count != 0)
                lb_zoneList.SelectedIndex = 0;
        }

        private void OnSaveAs(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Title = "Save zone_data.bin";
            sfd.Filter = "Zone Binary File (*.bin)|*.bin";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                BinaryWriter bw = new BinaryWriter(sfd.OpenFile());

                bw.Write(zoneList.Count);

                byte[] buffer = new byte[128];
                byte[] bufferTex = new byte[64];

                for (int i = 0; i < zoneList.Count; i++)
                {
                    bw.Write(zoneList[i].nZoneType);
                    bw.Write(zoneList[i].nExtraCnt);
                    bw.Write(zoneList[i].nString);

                    byte[] tmp = Core.Encoder.GetBytes(zoneList[i].wldFileName);

                    Array.Clear(buffer, 0, 128);
                    Array.Copy(tmp, buffer, zoneList[i].wldFileName.Length);

                    bw.Write(buffer);

                    /* First texture */
                    tmp = Core.Encoder.GetBytes(zoneList[i].texName1);

                    Array.Clear(bufferTex, 0, 64);
                    Array.Copy(tmp, bufferTex, zoneList[i].texName1.Length);

                    bw.Write(bufferTex);

                    /* Second texture */
                    tmp = Core.Encoder.GetBytes(zoneList[i].texName2);

                    Array.Clear(bufferTex, 0, 64);
                    Array.Copy(tmp, bufferTex, zoneList[i].texName2.Length);

                    bw.Write(bufferTex);

                    bw.Write(zoneList[i].fLoadingStep);
                    bw.Write(zoneList[i].fTer_Lodmul);
                }

                bw.Write(extraList.Count);

                for (int i = 0; i < extraList.Count; i++)
                {
                    for (int j = 0; j < 30; j++)
                    {
                        bw.Write(extraList[i].nString[j]);
                    }
                }

                bw.Close();
                bw.Dispose();

                lastSaveFile = sfd.FileName;
            }
        }

        private void OnSave(object sender, EventArgs e)
        {
            if(lastSaveFile == String.Empty)
            {
                OnSaveAs(sender, e);
            }

            BinaryWriter bw = new BinaryWriter(File.Open(lastSaveFile, FileMode.Create));

            bw.Write(zoneList.Count);

            byte[] buffer = new byte[128];
            byte[] bufferTex = new byte[64];

            for(int i = 0; i < zoneList.Count; i++)
            {
                bw.Write(zoneList[i].nZoneType);
                bw.Write(zoneList[i].nExtraCnt);
                bw.Write(zoneList[i].nString);

                byte[] tmp = Core.Encoder.GetBytes(zoneList[i].wldFileName);

                Array.Clear(buffer, 0, 128);
                Array.Copy(tmp, buffer, zoneList[i].wldFileName.Length);
            
                bw.Write(buffer);

                /* First texture */
                tmp = Core.Encoder.GetBytes(zoneList[i].texName1);

                Array.Clear(bufferTex, 0, 64);
                Array.Copy(tmp, bufferTex, zoneList[i].texName1.Length);

                bw.Write(bufferTex);

                /* Second texture */
                tmp = Core.Encoder.GetBytes(zoneList[i].texName2);

                Array.Clear(bufferTex, 0, 64);
                Array.Copy(tmp, bufferTex, zoneList[i].texName2.Length);

                bw.Write(bufferTex);

                bw.Write(zoneList[i].fLoadingStep);
                bw.Write(zoneList[i].fTer_Lodmul);
            }

            bw.Write(extraList.Count);

            for(int i = 0; i < extraList.Count; i++)
            {
                for(int j = 0; j < 30; j++)
                {
                    bw.Write(extraList[i].nString[j]);
                }
            }

            bw.Close();
            bw.Dispose();
        }

        private void OnExit(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OnDoUpdate(object sender, EventArgs e)
        {
            int zoneIdx = lb_zoneList.SelectedIndex;

            if (zoneIdx == -1)
                return;

            zoneList[zoneIdx].nZoneType = int.Parse(tb_zoneType.Text);
            zoneList[zoneIdx].wldFileName = tb_zoneFile.Text;
            zoneList[zoneIdx].texName1 = tb_texName1.Text;
            zoneList[zoneIdx].texName2 = tb_texName2.Text;

            zoneList[zoneIdx].fLoadingStep = float.Parse(tb_loadSteps.Text);
            zoneList[zoneIdx].fTer_Lodmul = float.Parse(tb_loadMul.Text);
        }

        private void OnFindZone(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "Select .wld file";
            ofd.Filter = "Last Chaos World File (*.wld)|*.wld";

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                int len = ofd.FileName.LastIndexOf("Data\\World");
                int extra = "Data\\World".Length;
                String trim = ofd.FileName.Substring(len+extra + 1, ofd.FileName.Length - len - extra - 1);

                tb_zoneFile.Text = trim;
            }
        }

        private void OnFindTex1Btn(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "Select .tex file";
            ofd.Filter = "Last Chaos Texture (*.tex)|*.tex";

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                tb_texName1.Text = Path.GetFileName(ofd.FileName);
            }
        }

        private void OnFindTex2Btn(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "Select .tex file";
            ofd.Filter = "Last Chaos Texture (*.tex)|*.tex";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tb_texName2.Text = Path.GetFileName(ofd.FileName);
            }
        }

        private void OnZoneListKeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                OnSelectZoneName(null, null);
            }
        }

        private void OnZoneExtraKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                OnSelectExtraName(null, null);
            }
        }

        private void OnZoneListDblClick(object sender, MouseEventArgs e)
        {
            int index = this.lb_zoneList.IndexFromPoint(e.Location);

            if (index != ListBox.NoMatches)
            {
                OnSelectZoneName(null, null);
            }
        }

        private void OnZoneExtraDblClick(object sender, MouseEventArgs e)
        {
            int index = this.lb_zoneExtra.IndexFromPoint(e.Location);

            if (index != ListBox.NoMatches)
            {
                OnSelectExtraName(null, null);
            }
        }

        private void OnReloadStrings(object sender, EventArgs e)
        {
            if (m_stringTable.Count == 0)
                return;

            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "Select String Table (strClient_us.lod)";
            ofd.Filter = "Client String Files (*.lod)|*.lod";
            ofd.CheckFileExists = true;

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            m_stringTable.Clear();

            BinaryReader br = new BinaryReader(File.Open(ofd.FileName, FileMode.Open));

            int nRow = br.ReadInt32();
            int nMax = br.ReadInt32();

            while (br.BaseStream.Position != br.BaseStream.Length)
            {
                int nidx = br.ReadInt32();

                int len = br.ReadInt32();

                if (len > 0)
                {
                    string text = Core.Encoder.GetString(br.ReadBytes(len));

                    m_stringTable.Add(nidx, text);
                }
            }

            br.Close();
            br.Dispose();

            RebuildLists();
        }

        public override void OnConnect()
        {
        }

        public override void OnDisconnect()
        {
        }
    }
}
