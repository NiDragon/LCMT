using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IllTechLibrary.SharedStructs;
using System.IO;

namespace LCMT
{
    public partial class ItemRipper : Form
    {
        public class ItemStrValue
        {
            public ItemStrValue(String Name, String Desc)
            {
                this.Name = Name;
                this.Desc = Desc;
            }

            public String Name;
            public String Desc;
        }

        private int itemCount = 0;
        private List<Item> readItems = new List<Item>();
        private List<Item> rippedItems = new List<Item>();

        public ItemRipper()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void OpenFileEp1_Click(object sender, EventArgs e)
        {
            rippedItems.Clear();
            readItems.Clear();
            RipItemList.Items.Clear();

            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "Select Episode One ItemAll.lod";
            ofd.Filter = "itemAll.lod (*.lod)|*.lod|All files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ASCIIEncoding asciiencoding = new ASCIIEncoding();

                try
                {
                    using (BinaryReader b = new BinaryReader(File.Open(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.Read)))
                    {
                        itemCount = b.ReadInt32();

                        while (b.BaseStream.Position < b.BaseStream.Length)
                        {
                            int currentitem = 0;

                            while (currentitem < itemCount)
                            {
                                Item tempitem = new Item();
                                tempitem.a_index = b.ReadInt32();
                                currentitem = tempitem.a_index;
                                tempitem.a_name_usa = asciiencoding.GetString(b.ReadBytes(b.ReadInt32()));
                                tempitem.a_name_thai = tempitem.a_name_usa;
                                tempitem.a_job_flag = b.ReadInt32();
                                tempitem.a_weight = b.ReadInt32();
                                tempitem.a_max_use = b.ReadInt32();
                                tempitem.a_level = b.ReadInt32();
                                tempitem.a_flag = b.ReadInt32();
                                tempitem.a_wearing = b.ReadInt32();
                                tempitem.a_type_idx = b.ReadInt32();
                                tempitem.a_subtype_idx = b.ReadInt32();

                                tempitem.a_need_item0 = b.ReadInt32();
                                tempitem.a_need_item1 = b.ReadInt32();
                                tempitem.a_need_item2 = b.ReadInt32();
                                tempitem.a_need_item3 = b.ReadInt32();
                                tempitem.a_need_item4 = b.ReadInt32();
                                tempitem.a_need_item5 = b.ReadInt32();
                                tempitem.a_need_item6 = b.ReadInt32();
                                tempitem.a_need_item7 = b.ReadInt32();
                                tempitem.a_need_item8 = b.ReadInt32();
                                tempitem.a_need_item9 = b.ReadInt32();

                                tempitem.a_need_item_count0 = b.ReadInt32();
                                tempitem.a_need_item_count1 = b.ReadInt32();
                                tempitem.a_need_item_count2 = b.ReadInt32();
                                tempitem.a_need_item_count3 = b.ReadInt32();
                                tempitem.a_need_item_count4 = b.ReadInt32();
                                tempitem.a_need_item_count5 = b.ReadInt32();
                                tempitem.a_need_item_count6 = b.ReadInt32();
                                tempitem.a_need_item_count7 = b.ReadInt32();
                                tempitem.a_need_item_count8 = b.ReadInt32();
                                tempitem.a_need_item_count9 = b.ReadInt32();

                                tempitem.a_need_sskill = b.ReadInt32();
                                tempitem.a_need_sskill_level = b.ReadInt32();
                                tempitem.a_need_sskill2 = b.ReadInt32();
                                tempitem.a_need_sskill_level2 = b.ReadInt32();
                                tempitem.a_num_0 = b.ReadInt32();
                                tempitem.a_num_1 = b.ReadInt32();
                                tempitem.a_num_2 = b.ReadInt32();
                                tempitem.a_num_3 = b.ReadInt32();
                                tempitem.a_price = b.ReadInt32();
                                tempitem.a_file_smc = asciiencoding.GetString(b.ReadBytes(b.ReadInt32()));
                                tempitem.a_texture_id = b.ReadInt32();
                                tempitem.a_texture_row = b.ReadInt32();
                                tempitem.a_texture_col = b.ReadInt32();
                                tempitem.a_descr_usa = asciiencoding.GetString(b.ReadBytes(b.ReadInt32()));
                                tempitem.a_descr_thai = tempitem.a_descr_usa;
                                tempitem.a_effect_name = asciiencoding.GetString(b.ReadBytes(b.ReadInt32()));
                                tempitem.a_set_0 = b.ReadInt32();
                                tempitem.a_set_1 = b.ReadInt32();
                                tempitem.a_set_2 = b.ReadInt32();
                                tempitem.a_set_3 = b.ReadInt32();

                                readItems.Add(tempitem);
                            }
                        }
                    }

                    RipItemList.Items.AddRange(readItems.Select(p => p.a_index.ToString() + " - " + p.a_name_usa).ToArray());
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid Episode One ItemAll", "Error", MessageBoxButtons.OK);
                }
            }
        }

        private void OpenFileEp2_Click(object sender, EventArgs e)
        {
            rippedItems.Clear();
            readItems.Clear();
            RipItemList.Items.Clear();

            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "Select Episode Two ItemAll.lod";
            ofd.Filter = "itemAll.lod (*.lod)|*.lod|All files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ASCIIEncoding asciiencoding = new ASCIIEncoding();

                try
                {
                    using (BinaryReader b = new BinaryReader(File.Open(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.Read)))
                    {
                        itemCount = b.ReadInt32();

                        while (b.BaseStream.Position < b.BaseStream.Length)
                        {
                            int currentitem = 0;

                            while (currentitem < itemCount)
                            {
                                Item tempitem = new Item();

                                tempitem.a_index = b.ReadInt32();
                                currentitem = tempitem.a_index;
                                tempitem.a_name_usa = asciiencoding.GetString(b.ReadBytes(b.ReadInt32()));
                                tempitem.a_name_thai = tempitem.a_name_usa;
                                tempitem.a_job_flag = b.ReadInt32();
                                tempitem.a_weight = b.ReadInt32();
                                tempitem.a_max_use = b.ReadInt32();
                                tempitem.a_level = b.ReadInt32();
                                tempitem.a_flag = b.ReadInt32();
                                tempitem.a_wearing = b.ReadInt32();
                                tempitem.a_type_idx = b.ReadInt32();
                                tempitem.a_subtype_idx = b.ReadInt32();

                                tempitem.a_need_item0 = b.ReadInt32();
                                tempitem.a_need_item1 = b.ReadInt32();
                                tempitem.a_need_item2 = b.ReadInt32();
                                tempitem.a_need_item3 = b.ReadInt32();
                                tempitem.a_need_item4 = b.ReadInt32();
                                tempitem.a_need_item5 = b.ReadInt32();
                                tempitem.a_need_item6 = b.ReadInt32();
                                tempitem.a_need_item7 = b.ReadInt32();
                                tempitem.a_need_item8 = b.ReadInt32();
                                tempitem.a_need_item9 = b.ReadInt32();

                                tempitem.a_need_item_count0 = b.ReadInt32();
                                tempitem.a_need_item_count1 = b.ReadInt32();
                                tempitem.a_need_item_count2 = b.ReadInt32();
                                tempitem.a_need_item_count3 = b.ReadInt32();
                                tempitem.a_need_item_count4 = b.ReadInt32();
                                tempitem.a_need_item_count5 = b.ReadInt32();
                                tempitem.a_need_item_count6 = b.ReadInt32();
                                tempitem.a_need_item_count7 = b.ReadInt32();
                                tempitem.a_need_item_count8 = b.ReadInt32();
                                tempitem.a_need_item_count9 = b.ReadInt32();

                                tempitem.a_need_sskill = b.ReadInt32();
                                tempitem.a_need_sskill_level = b.ReadInt32();
                                tempitem.a_need_sskill2 = b.ReadInt32();
                                tempitem.a_need_sskill_level2 = b.ReadInt32();
                                tempitem.a_num_0 = b.ReadInt32();
                                tempitem.a_num_1 = b.ReadInt32();
                                tempitem.a_num_2 = b.ReadInt32();
                                tempitem.a_num_3 = b.ReadInt32();
                                tempitem.a_price = b.ReadInt32();
                                tempitem.a_file_smc = asciiencoding.GetString(b.ReadBytes(b.ReadInt32()));
                                tempitem.a_texture_id = b.ReadInt32();
                                tempitem.a_texture_row = b.ReadInt32();
                                tempitem.a_texture_col = b.ReadInt32();
                                tempitem.a_descr_usa = asciiencoding.GetString(b.ReadBytes(b.ReadInt32()));
                                tempitem.a_descr_thai = tempitem.a_descr_usa;
                                tempitem.a_effect_name = asciiencoding.GetString(b.ReadBytes(b.ReadInt32()));
                                tempitem.a_attack_effect_name = asciiencoding.GetString(b.ReadBytes(b.ReadInt32()));
                                tempitem.a_damage_effect_name = asciiencoding.GetString(b.ReadBytes(b.ReadInt32()));
                                tempitem.a_set_0 = b.ReadInt32();
                                tempitem.a_set_1 = b.ReadInt32();
                                tempitem.a_set_2 = b.ReadInt32();
                                tempitem.a_set_3 = b.ReadInt32();
                                tempitem.a_set_4 = b.ReadInt32();
                                tempitem.a_rare_index_0 = b.ReadInt32();
                                tempitem.a_rare_prob_0 = b.ReadInt32();

                                readItems.Add(tempitem);
                            }
                        }
                    }

                    RipItemList.Items.AddRange(readItems.Select(p => p.a_index.ToString() + " - " + p.a_name_usa).ToArray());
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid Episode Two ItemAll", "Error", MessageBoxButtons.OK);
                }
            }
        }

        private void OpenFileEp3_Click(object sender, EventArgs e)
        {
            rippedItems.Clear();
            readItems.Clear();
            RipItemList.Items.Clear();

            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "Select Episode Two ItemAll.lod";
            ofd.Filter = "itemAll.lod (*.lod)|*.lod|All files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ASCIIEncoding asciiencoding = new ASCIIEncoding();

                try
                {
                    using (BinaryReader b = new BinaryReader(File.Open(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.Read)))
                    {
                        itemCount = b.ReadInt32();

                        while (b.BaseStream.Position < b.BaseStream.Length)
                        {
                            int currentitem = 0;

                            while (currentitem < itemCount)
                            {
                                Item tempitem = new Item();

                                tempitem.a_index = b.ReadInt32();
                                currentitem = tempitem.a_index;
                                tempitem.a_name_usa = asciiencoding.GetString(b.ReadBytes(b.ReadInt32()));
                                tempitem.a_name_thai = tempitem.a_name_usa;
                                tempitem.a_job_flag = b.ReadInt32();
                                tempitem.a_weight = b.ReadInt32();
                                tempitem.a_max_use = b.ReadInt32();
                                tempitem.a_level = b.ReadInt32();
                                tempitem.a_flag = b.ReadInt32();
                                tempitem.a_wearing = b.ReadInt32();
                                tempitem.a_type_idx = b.ReadInt32();
                                tempitem.a_subtype_idx = b.ReadInt32();

                                tempitem.a_need_item0 = b.ReadInt32();
                                tempitem.a_need_item1 = b.ReadInt32();
                                tempitem.a_need_item2 = b.ReadInt32();
                                tempitem.a_need_item3 = b.ReadInt32();
                                tempitem.a_need_item4 = b.ReadInt32();
                                tempitem.a_need_item5 = b.ReadInt32();
                                tempitem.a_need_item6 = b.ReadInt32();
                                tempitem.a_need_item7 = b.ReadInt32();
                                tempitem.a_need_item8 = b.ReadInt32();
                                tempitem.a_need_item9 = b.ReadInt32();

                                tempitem.a_need_item_count0 = b.ReadInt32();
                                tempitem.a_need_item_count1 = b.ReadInt32();
                                tempitem.a_need_item_count2 = b.ReadInt32();
                                tempitem.a_need_item_count3 = b.ReadInt32();
                                tempitem.a_need_item_count4 = b.ReadInt32();
                                tempitem.a_need_item_count5 = b.ReadInt32();
                                tempitem.a_need_item_count6 = b.ReadInt32();
                                tempitem.a_need_item_count7 = b.ReadInt32();
                                tempitem.a_need_item_count8 = b.ReadInt32();
                                tempitem.a_need_item_count9 = b.ReadInt32();

                                tempitem.a_need_sskill = b.ReadInt32();
                                tempitem.a_need_sskill_level = b.ReadInt32();
                                tempitem.a_need_sskill2 = b.ReadInt32();
                                tempitem.a_need_sskill_level2 = b.ReadInt32();
                                tempitem.a_num_0 = b.ReadInt32();
                                tempitem.a_num_1 = b.ReadInt32();
                                tempitem.a_num_2 = b.ReadInt32();
                                tempitem.a_num_3 = b.ReadInt32();
                                tempitem.a_price = b.ReadInt32();
                                tempitem.a_file_smc = asciiencoding.GetString(b.ReadBytes(b.ReadInt32()));
                                tempitem.a_texture_id = b.ReadInt32();
                                tempitem.a_texture_row = b.ReadInt32();
                                tempitem.a_texture_col = b.ReadInt32();
                                tempitem.a_descr_usa = asciiencoding.GetString(b.ReadBytes(b.ReadInt32()));
                                tempitem.a_descr_thai = tempitem.a_descr_usa;
                                tempitem.a_effect_name = asciiencoding.GetString(b.ReadBytes(b.ReadInt32()));
                                tempitem.a_attack_effect_name = asciiencoding.GetString(b.ReadBytes(b.ReadInt32()));
                                tempitem.a_damage_effect_name = asciiencoding.GetString(b.ReadBytes(b.ReadInt32()));
                                tempitem.a_set_0 = b.ReadInt32();
                                tempitem.a_set_1 = b.ReadInt32();
                                tempitem.a_set_2 = b.ReadInt32();
                                tempitem.a_set_3 = b.ReadInt32();
                                tempitem.a_set_4 = b.ReadInt32();
                                tempitem.a_rare_index_0 = b.ReadInt32();
                                tempitem.a_rare_prob_0 = b.ReadInt32();

                                readItems.Add(tempitem);
                            }
                        }
                    }

                    RipItemList.Items.AddRange(readItems.Select(p => p.a_index.ToString() + " - " + p.a_name_usa).ToArray());
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid Episode Two ItemAll", "Error", MessageBoxButtons.OK);
                }
            }
        }

        private void OpenFileEp4_Click(object sender, EventArgs e)
        {
            string ripType = (string)((ToolStripMenuItem)sender).Tag;

            rippedItems.Clear();
            readItems.Clear();
            RipItemList.Items.Clear();

            Dictionary<int, ItemStrValue> strItem = GetNamesDict();

            if (strItem.Count == 0)
                return;

            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "Select Episode Four ItemAll.lod";
            ofd.Filter = "itemAll.lod (*.lod)|*.lod|All files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ASCIIEncoding asciiencoding = new ASCIIEncoding();

                try
                {
                    using (BinaryReader b = new BinaryReader(File.Open(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.Read)))
                    {
                        itemCount = b.ReadInt32();

                        while (b.BaseStream.Position < b.BaseStream.Length)
                        {
                            int currentitem = 0;

                            while (currentitem < itemCount)
                            {
                                Item tempitem = new Item();

                                tempitem.a_index = b.ReadInt32();

                                currentitem = tempitem.a_index;

                                if (strItem.Keys.Contains(tempitem.a_index))
                                {
                                    tempitem.a_name_usa = strItem[tempitem.a_index].Name;
                                    tempitem.a_descr_usa = strItem[tempitem.a_index].Desc;
                                    tempitem.a_name_thai = tempitem.a_name_usa;
                                    tempitem.a_descr_thai = tempitem.a_descr_usa;
                                }
                                else
                                {
                                    tempitem.a_name_usa = "Ripped Item";
                                    tempitem.a_name_thai = tempitem.a_name_usa;

                                    tempitem.a_descr_usa = "No Description";
                                    tempitem.a_descr_thai = tempitem.a_descr_usa;
                                }

                                tempitem.a_job_flag = b.ReadInt32();
                                tempitem.a_weight = b.ReadInt32();
                                tempitem.a_fame = b.ReadInt32();
                                tempitem.a_level = b.ReadInt32();
                                tempitem.a_flag = b.ReadInt64();
                                tempitem.a_wearing = b.ReadInt32();
                                tempitem.a_type_idx = b.ReadInt32();
                                tempitem.a_subtype_idx = b.ReadInt32();

                                tempitem.a_need_item0 = b.ReadInt32();
                                tempitem.a_need_item1 = b.ReadInt32();
                                tempitem.a_need_item2 = b.ReadInt32();
                                tempitem.a_need_item3 = b.ReadInt32();
                                tempitem.a_need_item4 = b.ReadInt32();
                                tempitem.a_need_item5 = b.ReadInt32();
                                tempitem.a_need_item6 = b.ReadInt32();
                                tempitem.a_need_item7 = b.ReadInt32();
                                tempitem.a_need_item8 = b.ReadInt32();
                                tempitem.a_need_item9 = b.ReadInt32();

                                tempitem.a_need_item_count0 = b.ReadInt32();
                                tempitem.a_need_item_count1 = b.ReadInt32();
                                tempitem.a_need_item_count2 = b.ReadInt32();
                                tempitem.a_need_item_count3 = b.ReadInt32();
                                tempitem.a_need_item_count4 = b.ReadInt32();
                                tempitem.a_need_item_count5 = b.ReadInt32();
                                tempitem.a_need_item_count6 = b.ReadInt32();
                                tempitem.a_need_item_count7 = b.ReadInt32();
                                tempitem.a_need_item_count8 = b.ReadInt32();
                                tempitem.a_need_item_count9 = b.ReadInt32();

                                tempitem.a_need_sskill = b.ReadInt32();
                                tempitem.a_need_sskill_level = b.ReadInt32();
                                tempitem.a_need_sskill2 = b.ReadInt32();
                                tempitem.a_need_sskill_level2 = b.ReadInt32();
                                tempitem.a_texture_id = b.ReadInt32();
                                tempitem.a_texture_row = b.ReadInt32();
                                tempitem.a_texture_col = b.ReadInt32();

                                tempitem.a_num_0 = b.ReadInt32();
                                tempitem.a_num_1 = b.ReadInt32();
                                tempitem.a_num_2 = b.ReadInt32();
                                tempitem.a_num_3 = b.ReadInt32();
                                tempitem.a_price = b.ReadInt32();

                                tempitem.a_set_0 = b.ReadInt32();
                                tempitem.a_set_1 = b.ReadInt32();
                                tempitem.a_set_2 = b.ReadInt32();
                                tempitem.a_set_3 = b.ReadInt32();
                                tempitem.a_set_4 = b.ReadInt32();

                                tempitem.a_file_smc = Encoding.ASCII.GetString(b.ReadBytes(64));
                                tempitem.a_file_smc = tempitem.a_file_smc.Replace("\0", "");

                                tempitem.a_effect_name = Encoding.ASCII.GetString(b.ReadBytes(32));
                                tempitem.a_effect_name = tempitem.a_effect_name.Replace("\0", "");

                                tempitem.a_attack_effect_name = Encoding.ASCII.GetString(b.ReadBytes(32));
                                tempitem.a_attack_effect_name = tempitem.a_attack_effect_name.Replace("\0", "");

                                tempitem.a_damage_effect_name = Encoding.ASCII.GetString(b.ReadBytes(32));
                                tempitem.a_damage_effect_name = tempitem.a_damage_effect_name.Replace("\0", "");

                                if (ripType == "playpark" || ripType == "lcgn02")
                                {
                                    b.ReadInt32();
                                }

                                tempitem.a_rare_index_0 = b.ReadInt32();
                                tempitem.a_rare_prob_0 = b.ReadInt32();

                                tempitem.a_rare_index_0 = b.ReadInt32();
                                tempitem.a_rare_index_1 = b.ReadInt32();
                                tempitem.a_rare_index_2 = b.ReadInt32();
                                tempitem.a_rare_index_3 = b.ReadInt32();
                                tempitem.a_rare_index_4 = b.ReadInt32();
                                tempitem.a_rare_index_5 = b.ReadInt32();
                                tempitem.a_rare_index_6 = b.ReadInt32();
                                tempitem.a_rare_index_7 = b.ReadInt32();
                                tempitem.a_rare_index_8 = b.ReadInt32();
                                tempitem.a_rare_index_9 = b.ReadInt32();

                                tempitem.a_rare_prob_0 = b.ReadInt32();
                                tempitem.a_rare_prob_1 = b.ReadInt32();
                                tempitem.a_rare_prob_2 = b.ReadInt32();
                                tempitem.a_rare_prob_3 = b.ReadInt32();
                                tempitem.a_rare_prob_4 = b.ReadInt32();
                                tempitem.a_rare_prob_5 = b.ReadInt32();
                                tempitem.a_rare_prob_6 = b.ReadInt32();
                                tempitem.a_rare_prob_7 = b.ReadInt32();
                                tempitem.a_rare_prob_8 = b.ReadInt32();
                                tempitem.a_rare_prob_9 = b.ReadInt32();

                                tempitem.a_rvr_value = b.ReadInt32();
                                tempitem.a_rvr_grade = b.ReadInt32();

                                // Fortune Shit
                                b.ReadInt32();

                                tempitem.a_castle_war = b.ReadByte();

                                if (ripType == "lcgn02")
                                {
                                    for (int i = 0; i < 11; i++)
                                    {
                                        b.ReadInt32();
                                    }
                                }

                                readItems.Add(tempitem);
                            }
                        }
                    }

                    RipItemList.Items.AddRange(readItems.Select(p => p.a_index.ToString() + " - " + p.a_name_usa).ToArray());
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid Episode Four ItemAll", "Error", MessageBoxButtons.OK);
                }
            }
        }

        public Dictionary<int, ItemStrValue> GetNamesDict()
        {
            Dictionary<int, ItemStrValue> strItem = new Dictionary<int, ItemStrValue>();

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open strItem File *.lod";
            ofd.Filter = "String File(*.lod)|*.lod";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                BinaryReader br = new BinaryReader(ofd.OpenFile());

                int nIdx = br.ReadInt32();
                int lIdx = br.ReadInt32();

                try {
                    while (br.BaseStream.Position != br.BaseStream.Length)
                    {
                        int idx = br.ReadInt32();

                        int len = br.ReadInt32();

                        String name = "";

                        if (len != 0)
                        {
                            name = IllTechLibrary.Core.Encoder.GetString(br.ReadBytes(len));
                        }

                        len = br.ReadInt32();

                        String desc = "";

                        if (len != 0)
                        {
                            desc = IllTechLibrary.Core.Encoder.GetString(br.ReadBytes(len));
                        }

                        strItem.Add(idx, new ItemStrValue(name, desc));
                    }
                } catch (Exception)
                {
                    strItem.Clear();
                    MessageBox.Show("Invalid String File Check Language Settings", "Error", MessageBoxButtons.OK);
                }

                br.Close();
                br.Dispose();
            }

            return strItem;
        }

        public List<Item> GetItems()
        {
            return rippedItems;
        }

        private void ConfirmOkBtn_Click(object sender, EventArgs e)
        {
            char[] listSplit = new char[] { '-' };
            char[] trimChars = new char[] { ' ' };

            for (int i = 0; i < RipItemList.Items.Count; i++)
            {
                if(RipItemList.GetItemChecked(i) == true)
                {
                    int idx = int.Parse(RipItemList.Items[i].ToString().Split(listSplit)[0].TrimEnd(trimChars));

                    int itemIndex = readItems.FindIndex(p => p.a_index == idx);

                    rippedItems.Add(readItems[itemIndex]);
                }
            }

            this.DialogResult = DialogResult.OK;

            if (rippedItems.Count == 0)
                this.DialogResult = DialogResult.Cancel;

            this.Close();
        }
    }
}
