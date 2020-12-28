using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using IllTechLibrary.DataFiles;
using IllTechLibrary.SharedStructs;
using IllTechLibrary.Serialization;
using IllTechLibrary.Util;

namespace IllTechLibrary.Controls
{
    public struct WearItemDesc
    {
        public WearItemDesc(Item RowItem, WearInvenData InvData, int Upgrade, int WearPos)
        {
            this.RowItem = RowItem;
            this.Upgrade = Upgrade;
            this.WearPos = WearPos;
            this.InvData = InvData;

            RareOpt = false;
            Origin = false;

            if (BitHelpers.IsBitSet(RowItem.a_flag, 19)) // Origin
            {
                Origin = true;

                Options = new int[] { RowItem.a_rare_index_1 ,
                RowItem.a_rare_index_2 ,
                RowItem.a_rare_index_3 ,
                RowItem.a_rare_index_4 ,
                RowItem.a_rare_index_5 ,
                RowItem.a_rare_index_6 };
            }
            else if (BitHelpers.IsBitSet(RowItem.a_flag, 15)) // Rare Option
            {
                RareOpt = true;

                Options = new int[] { InvData.a_item_option0,
                                      InvData.a_item_option1};
            }
            else // Default to regular seals
            {
                this.Options = new int[] { InvData.a_item_option0 ,
                InvData.a_item_option1,
                InvData.a_item_option2,
                InvData.a_item_option3,
                InvData.a_item_option4 };
            }
        }

        public Item RowItem;
        public int Upgrade;
        public int WearPos;
        public WearInvenData InvData;

        public int[] Options;
        public bool RareOpt, Origin;
    }

    public partial class CharEquipScreen : UserControl
    {
        private List<WearItemDesc> m_items = new List<WearItemDesc>();

        private ToolTip m_toolTip = new ToolTip();
        private ContextMenu m_context = new ContextMenu();
        private List<Descriptor> m_desc = new List<Descriptor>();

        private List<Option> m_opts = new List<Option>();
        private List<RareOption> m_rareOpts = new List<RareOption>();

        private class Descriptor
        {
            public Descriptor(string Name, string Desc, Rectangle Rect)
            {
                this.Name = Name;
                this.Desc = Desc;
                this.Rect = Rect;
            }

            public string Name;
            public string Desc;
            public Rectangle Rect;
        }

        private Dictionary<int, Point> m_spriteLoc = new Dictionary<int, Point>();

        public CharEquipScreen()
        {
            InitializeComponent();

            Size = new Size(246, 155);

            // Body
            m_spriteLoc.Add(0, new Point(107, 9));
            m_spriteLoc.Add(1, new Point(107, 44));
            m_spriteLoc.Add(2, new Point(72, 97));
            m_spriteLoc.Add(3, new Point(107, 79));
            m_spriteLoc.Add(4, new Point(142, 62));
            m_spriteLoc.Add(5, new Point(72, 62));
            m_spriteLoc.Add(6, new Point(107, 114));
            
            // Acc
            m_spriteLoc.Add(7, new Point(31, 9));
            m_spriteLoc.Add(8, new Point(31, 44));
            m_spriteLoc.Add(9, new Point(31, 79));

            // Pet
            m_spriteLoc.Add(10, new Point(183, 9));

            // Backwing
            m_spriteLoc.Add(11, new Point(142, 27));

            //m_spriteLoc.Add(30, new Point());

            m_context.MenuItems.Add("Delete Item");
        }

        public int lastInside = -1;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            int idx = m_desc.FindIndex(p => p.Rect.Contains(e.Location));

            if (idx != -1 && idx != lastInside)
            {
                m_toolTip.ToolTipTitle = m_desc[idx].Name;
                m_toolTip.Show(m_desc[idx].Desc == "" ? "No Description" : m_desc[idx].Desc, this, e.Location);

                lastInside = idx;
            }
            else
            {
                if (idx == -1)
                {
                    m_toolTip.Hide(this);
                    lastInside = -1;
                }
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            Point Rel = PointToClient(MousePosition);

            if (!DisplayRectangle.Contains(Rel))
            {
                lastInside = -1;
                m_toolTip.Hide(this);
            }

            base.OnMouseLeave(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (DesignMode)
            {
                base.OnPaint(e);
                return;
            }

            Graphics g = e.Graphics;

            g.Clear(BackColor);

            if(BackgroundImage != null)
                g.DrawImage(BackgroundImage, e.ClipRectangle);

            Deserialize<Item> itemDesc = new Deserialize<Item>("t_item");

            foreach (WearItemDesc item in m_items)
            {
                if (!m_spriteLoc.ContainsKey(item.WearPos))
                    continue;

                Point offset = m_spriteLoc[item.WearPos];

                Bitmap img = (Bitmap)IconCache.GetIconTexture(item.RowItem.a_texture_id);
                Rectangle srcRect = IconCache.GetIconRect(item.RowItem.a_texture_row, item.RowItem.a_texture_col);

                /*g.DrawImage((Bitmap)IconCache.GetItemIcon(item.RowItem.a_texture_id,
                    item.RowItem.a_texture_row,
                    item.RowItem.a_texture_col),
                    offset);*/

                g.DrawImage(img,
                    new Rectangle(offset.X, offset.Y, 32, 32),
                    srcRect,
                    GraphicsUnit.Pixel);

                itemDesc.SetData(item.RowItem);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                int idx = m_desc.FindIndex(p => p.Rect.Contains(e.Location));

                if (idx != -1)
                {
                    m_context.MenuItems[0].Tag = m_items[idx];
                    m_context.Show(this, e.Location);
                }
            }

            base.OnMouseDown(e);
        }

        public void RemoveItem(WearItemDesc item)
        {
            int idx = m_items.FindIndex(p => p.WearPos.Equals(item.WearPos));

            if(idx != -1)
            {
                m_context.MenuItems[0].Tag = null;

                m_items.RemoveAt(idx);
                m_desc.RemoveAt(idx);

                Invalidate();
            }
        }

        public void SetDeleteHandler(EventHandler handle)
        {
            m_context.MenuItems[0].Click += handle;
        }

        public void SetItems(List<WearItemDesc> items)
        {
            m_context.MenuItems[0].Tag = null;

            lastInside = -1;

            m_items.Clear();
            m_items.AddRange(items);

            m_desc.Clear();

            CreateToolTips();

            Invalidate();
        }

        private bool OptionEnabled(int levels, int flag)
        {
            return BitHelpers.IsBitSet(levels, flag);
        }

        private void CreateToolTips()
        {
            Deserialize<Item> itemDesc = new Deserialize<Item>("t_item");

            foreach (WearItemDesc desc in m_items)
            {
                if (!m_spriteLoc.ContainsKey(desc.WearPos))
                    continue;

                Point offset = m_spriteLoc[desc.WearPos];

                itemDesc.SetData(desc.RowItem);

                if (m_desc.Count != m_items.Count)
                {
                    string fullName = itemDesc["a_name_" + Core.LangCode].ToString();

                    string optionNames = string.Empty;

                    List<int> validOpts = desc.Options.Where(p => p != 0).ToList();

                    if (validOpts.Count != 0)
                    {
                        try
                        {
                            Deserialize<Option> descOpt = new Deserialize<Option>("t_option");

                            foreach (int a in validOpts)
                            {
                                short number = Convert.ToInt16(a);
                                byte upper = (byte)(number >> 8);
                                byte lower = (byte)(number & 0xff);

                                Option valid = m_opts.Find(p => p.a_type == upper);

                                if (desc.RareOpt) // Rare Option
                                {
                                    RareOption op = m_rareOpts.Find(p => p.a_index == number);

                                    if (op != null)
                                    {
                                        Deserialize<RareOption> rop = new Deserialize<RareOption>("t_rareoption");

                                        rop.SetData(op);

                                        fullName = $"{rop["a_prefix_" + Core.LangCode].ToString()} {itemDesc["a_name_" + Core.LangCode].ToString()}";

                                        List<Option> dataOps = new List<Option>();
                                        List<int> dataLevels = new List<int>();

                                        for (int k = 0; k < 9; k++)
                                        {
                                            if (OptionEnabled(validOpts[1], k))
                                            {
                                                dataOps.Add(m_opts.Find(p => p.a_type.Equals(Convert.ToInt32(rop[$"a_option_index{k}"]))));
                                                dataLevels.Add(Convert.ToInt32(rop[$"a_option_level{k}"]));
                                            }
                                        }

                                        for (int x = 0; x < dataOps.Count; x++)
                                        {
                                            if (dataOps[x] != null)
                                            {
                                                string[] strengths = dataOps[x].a_level.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                                                descOpt.SetData(dataOps[x]);

                                                optionNames += $"{descOpt["a_name_" + Core.LangCode].ToString()}: {strengths[dataLevels[x]]}{Environment.NewLine}";
                                            }
                                        }
                                    }

                                    break;
                                }
                                else if (desc.Origin) // Origin Seals
                                {
                                    List<Option> dataOps = new List<Option>();
                                    List<int> power = new List<int>();

                                    for (int k = 0; k < desc.Options.Count(); k++)
                                    {
                                        int level = Convert.ToInt32(itemDesc[$"a_rare_prob_{k + 1}"]);

                                        dataOps.Add(m_opts.Find(p => p.a_type.Equals(desc.Options[k])));

                                        if (dataOps[k] != null && level > 0)
                                        {
                                            power.Add(int.Parse(dataOps[k].a_level.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[level - 1]));
                                        }
                                        else
                                        {
                                            power.Add(1);
                                        }
                                    }

                                    for (int x = 0; x < dataOps.Count; x++)
                                    {
                                        if (dataOps[x] != null)
                                        {
                                            descOpt.SetData(dataOps[x]);
                                            optionNames += $"{descOpt["a_name_" + Core.LangCode].ToString()}: {power[x]}{Environment.NewLine}";
                                        }
                                    }

                                    break;
                                }
                                else if (valid != null) // Normal Options
                                {
                                    string[] strengths = valid.a_level.Split(new char[] { ' ' });

                                    descOpt.SetData(valid);

                                    optionNames += $"{descOpt["a_name_" + Core.LangCode].ToString()}: {strengths[lower]}{Environment.NewLine}";
                                }
                            }
                        }
                        catch (Exception exc) { MsgDialogs.Show("Error in CharEquipScreen.cs", $"Could Not Generate Tool Tips Error: {exc.Message}", "OK", MsgDialogs.MsgTypes.ERROR); }
                    }
                    else
                    {
                        optionNames = "";
                    }

                    m_desc.Add(new Descriptor(fullName + $" [{itemDesc["a_index"].ToString()}]" + (desc.Upgrade > 0 ? $" +{desc.Upgrade}" : ""),
                        optionNames + itemDesc["a_descr_" + Core.LangCode].ToString(),
                        new Rectangle(offset, new Size(32, 32))));
                }
            }
        }

        public void Clear()
        {
            lastInside = -1;

            m_items.Clear();

            m_desc.Clear();

            Invalidate();
        }

        public void SetOptions(List<Option> opts)
        {
            m_opts = opts;
        }

        public void SetRareOptions(List<RareOption> rareOpts)
        {
            m_rareOpts = rareOpts;
        }
    }
}
