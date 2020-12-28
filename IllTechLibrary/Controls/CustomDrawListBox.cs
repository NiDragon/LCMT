using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IllTechLibrary.Controls
{
    public struct ItemListItem
    {
        public int a_index;
        //public Bitmap Icon;
        public String Text;

        public int id;
        public int row, col;
    }

    public partial class CustomDrawListBox : ListBox
    {
        public CustomDrawListBox()
        {
            DrawMode = DrawMode.Normal; // We're using custom drawing.
            ItemHeight = 33; // Set the item height to 40.

            DoubleBuffered = false;
        }

        public ItemListItem[] listItems;

        public void SetImages(ItemListItem[] listItems)
        {
            this.listItems = listItems;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            // Make sure we're not trying to draw something that isn't there.
            if (e.Index >= this.Items.Count || e.Index <= -1)
                return;

            // Draw the background color depending on 
            // if the item is selected or not.
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                // The item is selected.
                // We want a blue background color.
                e.Graphics.FillRectangle(Brushes.Gainsboro, e.Bounds);
            }
            else
            {
                // The item is NOT selected.
                // We want a white background color.
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            }

            ItemListItem actualItem = listItems[e.Index];

            Bitmap bmp = (Bitmap)DataFiles.IconCache.GetIconTexture(actualItem.id);
            Rectangle Dimensions = DataFiles.IconCache.GetIconRect(actualItem.row, actualItem.col);

            // Draw the icon
            if (/*actualItem.Icon*/ bmp == null)
            {
                e.Graphics.DrawIcon(SystemIcons.Error, new Rectangle(e.Bounds.X, e.Bounds.Y, 32, 32));
            }
            else
            {
                //e.Graphics.DrawImage(actualItem.Icon, e.Bounds.X, e.Bounds.Y, 32, 32);
                e.Graphics.DrawImage(bmp, new Rectangle(e.Bounds.X, e.Bounds.Y, 32, 32), Dimensions, GraphicsUnit.Pixel);
            }

            // Draw the item.
            string text = actualItem.Text;
            SizeF stringSize = e.Graphics.MeasureString(text, Font);

            e.Graphics.DrawString(text, Font, Brushes.Black,
                new PointF(5 + 32, e.Bounds.Y + (e.Bounds.Height - stringSize.Height) / 2));
        }

        public void SetSelectedByIndex(int Index)
        {
            int idx = listItems.ToList().FindIndex(p => p.a_index.Equals(Index));
            SelectedIndex = idx;
        }

        public int GetSelectedItemIndex()
        {
            if (SelectedIndex != -1)
            {
                return listItems[SelectedIndex].a_index;
            }
            else
            {
                return -1;
            }
        }
    }
}
