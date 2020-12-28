using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IllTechLibrary.SharedStructs;
using IllTechLibrary.DataFiles;
using IllTechLibrary.Serialization;
using IllTechLibrary.Util;

namespace IllTechLibrary.Controls
{
    public partial class StashInventory : UserControl
    {
        private class Descriptor
        {
            public string Name;
            public string Desc;

            public int[] Options;
            public bool RareOpt, Origin;
        }

        private int ScrollPos = 0;
        private bool Scrolling = false;
        private int ToolTipIndex = 0;
        private int lastInside = -1;

        private ulong StashCash = 0;
        
        // Persistent
        private List<Item> m_itemData = new List<Item>();
        private List<Option> m_opts = new List<Option>();
        private List<RareOption> m_rareOpts = new List<RareOption>();

        // Generated
        private ToolTip m_toolTip = new ToolTip();
        private List<StashData> m_items = new List<StashData>();
        private List<Descriptor> m_desc = new List<Descriptor>();

        private Point LastMouseDown = new Point();

        private double perc = 0;
        private Rectangle BarRect;

        public StashInventory()
        {
            InitializeComponent();

            Size = Properties.Resources.StashBack.Size;
            MaximumSize = Size;
            MinimumSize = Size;

            CalculateScrollBar();

            DoubleBuffered = true;
        }

        private void CalculateScrollBar()
        {
            ScrollPos = 0;

            // 75 / 5 = 15 rows
            double rowCount = m_items.Count / 5.0;

            perc = rowCount * 0.01;

            if (m_items.Count < 20)
            {
                perc = 1;
                BarRect = new Rectangle(194, 35 + ScrollPos, 9, (int)(123.0 * perc));
            }
            else
            {
                BarRect = new Rectangle(194, 35 + ScrollPos, 9, (int)(Math.Floor(123.0 * perc)));
            }

            if (BarRect.Height < 10)
            {
                BarRect.Height = 9;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if(BarRect.Contains(e.Location))
            {
                LastMouseDown = e.Location;
                Scrolling = true;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            Scrolling = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if(Scrolling)
            {
                int topClamp = (123 - (int)(Math.Ceiling(123.0 * perc)));

                ScrollPos += (e.Location.Y - LastMouseDown.Y);

                if (ScrollPos < 0)
                {
                    ScrollPos = 0;
                }
                else if(ScrollPos > topClamp)
                {
                    ScrollPos = topClamp;
                }

                BarRect.Y = 35 + ScrollPos;

                Invalidate();

                LastMouseDown = e.Location;
            }

            int realX = e.X - 13;
            int realY = e.Y - 28;

            int col = (realX - (realX / 32) * 3) / 32;
            int row = (realY - (realY / 32) * 3) / 32;

            int idx = ToolTipIndex + (row * 5 + col);

            if (col >= 5 || row >= 4 || (e.X - 13) < 0 || (e.Y - 28) < 0)
                idx = -1;

            if (m_items.Count == 0 || idx < 0 || idx > m_items.Count - 1)
                idx = -1;

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

            if (!DisplayRectangle.Contains(Rel))
            {
                lastInside = -1;
                m_toolTip.Hide(this);
            }

            base.OnMouseLeave(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            int topClamp = (123 - (int)(Math.Ceiling(123.0 * perc)));

            ScrollPos += (int)(-e.Delta/120);

            if (ScrollPos < 0)
            {
                ScrollPos = 0;
            }
            else if (ScrollPos > topClamp)
            {
                ScrollPos = topClamp;
            }

            BarRect.Y = 35 + ScrollPos;

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.Clear(BackColor);

            g.DrawImage(Properties.Resources.StashBack, new Point(0, 0));

            // Total Items Draw
            SizeF strCount = g.MeasureString($"{m_items.Count}/300", Font);
            g.DrawString($"{m_items.Count}/300", Font, Brushes.White, 107 - strCount.Width / 2, 11 - strCount.Height / 2);
            
            // Draw Scroll Bar
            g.DrawImage(Properties.Resources.StashBar, BarRect);

            // Draw Item Icons
            int scrollTotal = 0;
            if(m_items.Count != 0) scrollTotal = 123 - (int)Math.Ceiling(123.0 * perc);
            int startIndex = 0;

            if (scrollTotal != 0 && ScrollPos != 0) startIndex = (int)(Math.Floor(m_items.Count/5.0) * ((double)ScrollPos / scrollTotal));

            int itemCount = 20;

            if (m_items.Count == 0 || m_items.Count < 20)
                startIndex = 0;

            if (m_items.Count < 20)
            {
                itemCount = m_items.Count;
            }
            else if(m_items.Count < (startIndex*5) + 20)
            {
                itemCount = m_items.Count - startIndex*5;
            }

            ToolTipIndex = startIndex * 5;

            List<StashData> renderStash = m_items.GetRange(startIndex*5, itemCount);

            int row = 0;
            int col = 0;

            Point offset = new Point(13, 28);

            foreach(StashData item in renderStash)
            {
                int idx = m_itemData.FindIndex(p => p.a_index.Equals(item.a_item_idx));

                if (idx == -1)
                    continue;

                if (col + 1 > 5)
                {
                    col = 0;
                    row++;
                }

                if(row + 1 > 4)
                {
                    break;
                }

                Point loc = new Point(offset.X + (col * 32 + (col * 3)), offset.Y + (row * 32 + (row * 3)));

                g.DrawImage((Image)IconCache.GetIconTexture(m_itemData[idx].a_texture_id),
                    new Rectangle(loc.X, loc.Y, 32, 32),
                    IconCache.GetIconRect(m_itemData[idx].a_texture_row,
                    m_itemData[idx].a_texture_col),
                    GraphicsUnit.Pixel);

                SizeF textOffset = g.MeasureString(item.a_count.ToString(), Font);

                g.DrawString(item.a_count == 1 ? "" : item.a_count.ToString(),
                    Font, Brushes.White, new PointF((loc.X + 32) - textOffset.Width,
                    (loc.Y + 32) - textOffset.Height));

                col++;
            }

            // Cash Draw
            SizeF strSize = g.MeasureString(StashCash.ToString("N0"), Font);

            g.DrawString(StashCash.ToString("N0"), Font, Brushes.White, new PointF(184 - strSize.Width, 170));
        }

        public void SetItems(ulong cash, List<Item> itemData, List<StashData> items)
        {
            StashCash = cash;

            m_items.Clear();
            m_desc.Clear();

            m_toolTip.Hide(this);
            lastInside = -1;

            m_items = items;
            m_itemData = itemData;

            CreateToolTips();

            ScrollPos = 0;
            CalculateScrollBar();

            Invalidate();
        }

        private List<StashData> GuildToStash(List<GuildStashData> items)
        {
            List<StashData> tStash = new List<StashData>();

            for (int i = 0; i < items.Count; i++)
            {
                StashData tmp = new StashData();

                tStash.Add(tmp);

                tStash[i].a_count = items[i].a_count;
                tStash[i].a_flag = items[i].a_flag;
                tStash[i].a_index = items[i].a_index;
                tStash[i].a_item_idx = items[i].a_item_idx;
                tStash[i].a_item_option0 = items[i].a_item_option0;
                tStash[i].a_item_option1 = items[i].a_item_option1;
                tStash[i].a_item_option2 = items[i].a_item_option2;
                tStash[i].a_item_option3 = items[i].a_item_option3;
                tStash[i].a_item_option4 = items[i].a_item_option4;
                tStash[i].a_item_origin_var0 = items[i].a_item_origin_0;
                tStash[i].a_item_origin_var1 = items[i].a_item_origin_1;
                tStash[i].a_item_origin_var2 = items[i].a_item_origin_2;
                tStash[i].a_item_origin_var3 = items[i].a_item_origin_3;
                tStash[i].a_item_origin_var4 = items[i].a_item_origin_4;
                tStash[i].a_item_origin_var5 = items[i].a_item_origin_5;
                tStash[i].a_max_dur = (ushort)items[i].a_max_dur;
                tStash[i].a_now_dur = (ushort)items[i].a_now_dur;
                tStash[i].a_plus = items[i].a_plus;
                tStash[i].a_serial = items[i].a_serial;
                tStash[i].a_socket = items[i].a_socket;
                tStash[i].a_used = items[i].a_used;
                tStash[i].a_used_2 = items[i].a_used_2;
            }

            return tStash;
        }

        public void SetItems(ulong cash, List<Item> itemData, List<GuildStashData> items)
        {
            StashCash = cash;

            m_items.Clear();
            m_desc.Clear();

            m_toolTip.Hide(this);
            lastInside = -1;

            m_items = GuildToStash(items);
            m_itemData = itemData;

            CreateToolTips();

            ScrollPos = 0;
            CalculateScrollBar();

            Invalidate();
        }

        public void Clear()
        {
            StashCash = 0;

            m_toolTip.Hide(this);
            lastInside = -1;

            m_items.Clear();
            m_desc.Clear();

            ScrollPos = 0;
            CalculateScrollBar();

            Invalidate();
        }

        private bool OptionEnabled(int levels, int flag)
        {
            return BitHelpers.IsBitSet(levels, flag);
        }

        private void CalculateSeals(StashData item, Descriptor desc)
        {
            bool RareOpt = false;
            bool Origin = false;

            int[] Options;

            Item it = m_itemData.Find(p => p.a_index.Equals(item.a_item_idx));

            if (BitHelpers.IsBitSet(it.a_flag, 19)) // Origin
            {
                Origin = true;

                Options = new int[] { it.a_rare_index_1 ,
                it.a_rare_index_2 ,
                it.a_rare_index_3 ,
                it.a_rare_index_4 ,
                it.a_rare_index_5 ,
                it.a_rare_index_6 };
            }
            else if (BitHelpers.IsBitSet(it.a_flag, 15)) // Rare Option
            {
                RareOpt = true;

                Options = new int[] { Convert.ToInt32(item.a_item_option0),
                                      Convert.ToInt32(item.a_item_option1)};
            }
            else // Default to regular seals
            {
                Options = new int[] { Convert.ToInt32(item.a_item_option0),
                Convert.ToInt32(item.a_item_option1),
                Convert.ToInt32(item.a_item_option2),
                Convert.ToInt32(item.a_item_option3),
                Convert.ToInt32(item.a_item_option4)};
            }

            desc.Options = Options;

            desc.RareOpt = RareOpt;
            desc.Origin = Origin;
        }

        private void CreateToolTips()
        {
            Deserialize<Item> itemDesc = new Deserialize<Item>("t_item");

            foreach (StashData desc in m_items)
            {
                Descriptor descr = new Descriptor();

                CalculateSeals(desc, descr);

                itemDesc.SetData(m_itemData.Find(p => p.a_index.Equals(desc.a_item_idx)));

                Deserialize<StashData> ripDesc = new Deserialize<StashData>("t_stash00");

                ripDesc.SetData(desc);

                if (m_desc.Count != m_items.Count)
                {
                    string fullName = itemDesc["a_name_" + Core.LangCode].ToString();

                    string optionNames = string.Empty;

                    List<int> validOpts = descr.Options.Where(p => p != 0).ToList();

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

                                if (descr.RareOpt) // Rare Option
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
                                else if (descr.Origin) // Origin Seals
                                {
                                    List<Option> dataOps = new List<Option>();
                                    List<int> power = new List<int>();

                                    for (int k = 0; k < descr.Options.Count(); k++)
                                    {
                                        int level = Convert.ToInt32(itemDesc[$"a_rare_prob_{k + 1}"]);

                                        dataOps.Add(m_opts.Find(p => p.a_type.Equals(descr.Options[k])));

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
                        catch (Exception exc)
                        {
                            MsgDialogs.Show("Error in StashInventory.cs", $"Could Not Generate Tool Tips Error: {exc.Message}", "OK", MsgDialogs.MsgTypes.ERROR);
                        }
                    }
                    else
                    {
                        optionNames = "";
                    }

                    descr.Name = fullName + $" [{itemDesc["a_index"].ToString()}]" + (desc.a_plus > 0 ? $" +{desc.a_plus}" : "");
                    descr.Desc = optionNames + itemDesc["a_descr_" + Core.LangCode].ToString();

                    m_desc.Add(descr);
                }
            }
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
