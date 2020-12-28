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
    public struct InventoryItemDesc
    {
        public InventoryItemDesc(Item RowItem, InventoryRowData Item, int Col)
        {
            Deserialize<InventoryRowData> rt = new Deserialize<InventoryRowData>("t_inven00");

            rt.SetData(Item);

            this.RowItem = RowItem;
            this.Upgrade = (int)rt[$"a_plus{Col}"];
            this.Count = Convert.ToInt32(rt[$"a_count{Col}"]);
            this.Row = Item.a_row_idx;
            this.Col = Col;
            this.Options = new int[10];
            this.InvRow = Item;
            this.Tab = Row / 5;
            ExpandedInventory = Item.a_tab_idx != 0;

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

                Options = new int[] { Convert.ToInt32(rt[$"a_item{Col}_option0"]),
                                      Convert.ToInt32(rt[$"a_item{Col}_option1"])};
            }
            else // Default to regular seals
            {
                this.Options = new int[] { (short)rt[$"a_item{Col}_option0"] ,
                (short)rt[$"a_item{Col}_option1"],
                (short)rt[$"a_item{Col}_option2"],
                (short)rt[$"a_item{Col}_option3"],
                (short)rt[$"a_item{Col}_option4"] };
            }
        }

        public Item RowItem;
        public InventoryRowData InvRow;

        public int Upgrade;

        public int Tab;
        public int Count;
        public int Row, Col;
        public bool ExpandedInventory;

        public int[] Options;
        public bool RareOpt, Origin;
    }

    public partial class CharInventory : UserControl
    {
        private class Descriptor
        {
            public Descriptor(int Tab, bool Expand, string Name, string Desc, Rectangle Rect)
            {
                this.Tab = Tab;
                this.Name = Name;
                this.Desc = Desc;
                this.Rect = Rect;

                this.Expand = Expand;
            }

            public int Tab;
            public bool Expand;
            public string Name;
            public string Desc;
            public Rectangle Rect;
        }

        private List<InventoryItemDesc> m_items = new List<InventoryItemDesc>();

        private ToolTip m_toolTip = new ToolTip();
        private ContextMenu m_context = new ContextMenu();
        private List<Descriptor> m_desc = new List<Descriptor>();

        private List<Option> m_opts = new List<Option>();
        private List<RareOption> m_rareOpts = new List<RareOption>();

        private int lastInside = -1;
        private int tabPage = 0;
        private bool tabSpecial = false;

        public CharInventory()
        {
            InitializeComponent();

            m_context.MenuItems.Add("Delete Item");

            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;

            /*Size = new Size(212, 187);
            MaximumSize = Size;
            MinimumSize = Size;*/

            tab1Button.Click += SetTabPageEvent;
            tab2Button.Click += SetTabPageEvent;
            tab3Button.Click += SetTabPageEvent;
            tab4Button.Click += SetTabPageEvent;

            special1Button.Click += SetSpecialTabPageEvent;
            special2Button.Click += SetSpecialTabPageEvent;

            special1Button.FlatAppearance.MouseOverBackColor = Color.Transparent;
            special1Button.FlatAppearance.MouseDownBackColor = Color.Transparent;
            special2Button.FlatAppearance.MouseOverBackColor = Color.Transparent;
            special2Button.FlatAppearance.MouseDownBackColor = Color.Transparent;
        }

        private void ResetTabButtons()
        {
            tab1Button.BackgroundImage = tab2Button.BackgroundImage = 
                tab3Button.BackgroundImage = 
                tab4Button.BackgroundImage = 
                Properties.Resources.invtab_off;

            special1Button.BackgroundImage =
                special2Button.BackgroundImage =
                Properties.Resources.invtab_ex_off;

            tabSpecial = false;
        }

        private void SetTabPageEvent(object sender, EventArgs e)
        {
            ResetTabButtons();

            ((Button)sender).BackgroundImage = Properties.Resources.invtab_on;
            tabPage = Convert.ToInt32(((Button)sender).Tag);

            Invalidate();
        }

        private void SetSpecialTabPageEvent(object sender, EventArgs e)
        {
            ResetTabButtons();

            tabSpecial = true;

            ((Button)sender).BackgroundImage = Properties.Resources.invtab_ex_on;
            tabPage = Convert.ToInt32(((Button)sender).Tag);

            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            bool IsExpandInventory = false;

            if (tabSpecial)
            {
                IsExpandInventory = true;
            }

            int idx = m_desc.FindIndex(p => p.Rect.Contains(e.Location) && p.Tab.Equals(tabPage) && p.Expand == IsExpandInventory);

            if (idx != -1 && idx != lastInside)
            {
                m_toolTip.ToolTipTitle = m_desc[idx].Name;
                m_toolTip.Show(m_desc[idx].Desc == "" ? $"No Description" : $"{m_desc[idx].Desc}", this, e.Location);

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

            if(!DisplayRectangle.Contains(Rel))
            {
                lastInside = -1;
                m_toolTip.Hide(this);
            }

            base.OnMouseLeave(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            base.OnPaint(e);

            Rectangle invBox = new Rectangle(e.ClipRectangle.X + 22, e.ClipRectangle.Y, 190, 187);

            // Drawbackground image
            g.DrawImage(Properties.Resources.Inventroy, invBox);

            bool IsExpandInventory = false;

            if(tabSpecial)
            {
                IsExpandInventory = true;
            }

            // This is now all inventroy items draw only what this page needs
            List<InventoryItemDesc> toDraw = m_items.FindAll(p => p.Tab.Equals(tabPage) &&  p.ExpandedInventory == IsExpandInventory).OrderBy(p => p.Row).ThenBy(p => p.Col).ToList();

            int rowCount = 0;
            int lastRow = toDraw.Count != 0 ? toDraw.First().Row : 0;

            foreach (InventoryItemDesc desc in toDraw)
            {
                if (desc.Tab != tabPage)
                    continue;

                if (desc.Row != lastRow)
                {
                    rowCount++;
                    lastRow = desc.Row;
                }

                Point offset = new Point(desc.Col * 32 + (desc.Col * 4) + 7, rowCount * 32 + (rowCount * 4) + 6);

                Bitmap img = (Bitmap)IconCache.GetIconTexture(desc.RowItem.a_texture_id);
                Rectangle srcRect = IconCache.GetIconRect(desc.RowItem.a_texture_row, desc.RowItem.a_texture_col);

                g.DrawImage(img,
                    new Rectangle(invBox.X + offset.X, offset.Y, 32, 32),
                    srcRect,
                    GraphicsUnit.Pixel);

                SizeF textOffset = g.MeasureString(desc.Count.ToString(), Font);

                g.DrawString(desc.Count == 1 ? "" : desc.Count.ToString(),
                    Font, Brushes.White, new PointF((offset.X + 32 + 22) - textOffset.Width,
                    (offset.Y + 32) - textOffset.Height));
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int idx = m_desc.FindIndex(p => p.Rect.Contains(e.Location) && p.Tab.Equals(tabPage));

                if (idx != -1)
                {
                    m_context.MenuItems[0].Tag = m_items[idx];
                    m_context.Show(this, e.Location);
                }
            }

            base.OnMouseDown(e);
        }

        public void RemoveItem(InventoryItemDesc item)
        {
            int idx = m_items.FindIndex(p => p.Row.Equals(item.Row) && p.Col.Equals(item.Col));

            if (idx != -1)
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

        public void SetItems(List<InventoryItemDesc> items)
        {
            m_context.MenuItems[0].Tag = null;

            tabPage = 0;

            lastInside = -1;

            m_items.Clear();
            m_items.AddRange(items);

            m_desc.Clear();

            CreateToolTips();

            ResetTabButtons();
            tab1Button.BackgroundImage = Properties.Resources.invtab_on;

            Invalidate();
        }

        private bool OptionEnabled(int levels, int flag)
        {
            return BitHelpers.IsBitSet(levels, flag);
        }

        private void CreateToolTips()
        {
            Deserialize<Item> itemDesc = new Deserialize<Item>("t_item");

            foreach (InventoryItemDesc desc in m_items)
            {
                int rowCount = desc.Row - (desc.Tab * 5);

                Point offset = new Point(desc.Col * 32 + (desc.Col * 4) + 7 + 22, rowCount * 32 + (rowCount * 4) + 6);

                itemDesc.SetData(desc.RowItem);

                Deserialize<InventoryRowData> ripDesc = new Deserialize<InventoryRowData>("t_inven00");

                ripDesc.SetData(desc.InvRow);

                if (m_desc.Count != m_items.Count)
                {
                    string fullName = itemDesc["a_name_" + Core.LangCode].ToString();

                    string optionNames = string.Empty;

                    List<int> validOpts = desc.Options.Where(p => p != 0).ToList();

                    if (validOpts.Count != 0)
                    {
                        Deserialize<Option> descOpt = new Deserialize<Option>("t_option");

                        /*try
                        {*/
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
                        /*}
                        catch (Exception exc)
                        {
                            MsgDialogs.Show("Error in CharInventory.cs", $"Could Not Generate Tool Tips Error: {exc.Message}", "OK", MsgDialogs.MsgTypes.ERROR);
                        }*/
                    }
                    else
                    {
                        optionNames = "";
                    }

                    m_desc.Add(new Descriptor(desc.Tab, desc.ExpandedInventory, fullName + $" [{itemDesc["a_index"].ToString()}]" + (desc.Upgrade > 0 ? $" +{desc.Upgrade}" : ""),
                        optionNames + itemDesc["a_descr_" + Core.LangCode].ToString(),
                        new Rectangle(offset, new Size(32, 32))));
                }
            }
        }

        public void Clear()
        {
            tabPage = 0;

            lastInside = -1;

            m_items.Clear();

            m_desc.Clear();

            ResetTabButtons();

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
