using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IllTechLibrary.DataFiles
{
    public class IconCache
    {
        private class CacheMap
        {
            public CacheMap(String Name, Bitmap Src)
            {
                this.Name = Name;
                this.Image = Src;
            }

            public String Name;
            public Bitmap Image;
        }

        private static List<CacheMap> bitMapCache = new List<CacheMap>();

        public static object GetSkillIcon(int Id, int Row, int Col)
        {
            String fileName = String.Format("Images\\SkillBtn{0}.png", Id);

            int idx = bitMapCache.FindIndex(p => p.Name == fileName);

            Bitmap src = null;

            if (idx == -1)
            {
                if (!File.Exists(fileName))
                    return SystemIcons.Error.ToBitmap();

                src = Image.FromFile(fileName) as Bitmap;

                bitMapCache.Add(new CacheMap(fileName, src));

                idx = bitMapCache.Count - 1;
            }
            else
            {
                src = bitMapCache[idx].Image;
            }

            Rectangle cropRect = new Rectangle(new Point(32 * Col, 32 * Row), new Size(32, 32));
            Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

            Graphics g = Graphics.FromImage(target);

            lock (src)
            {
                g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
                    cropRect,
                    GraphicsUnit.Pixel);
            }

            g.Dispose();

            return target;
        }

        public static object GetItemIcon(int Id, int Row, int Col)
        {
            String fileName = String.Format("Images\\ItemBtn{0}.png", Id);

            int idx = bitMapCache.FindIndex(p => p.Name == fileName);

            Bitmap src = null;

            if (idx == -1)
            {
                if (!File.Exists(fileName))
                    return SystemIcons.Error.ToBitmap();

                src = Image.FromFile(fileName) as Bitmap;

                bitMapCache.Add(new CacheMap(fileName, src));

                idx = bitMapCache.Count-1;
            }
            else
            {
                src = bitMapCache[idx].Image;
            }

            Rectangle cropRect = new Rectangle(32 * Col, 32 * Row, 32, 32);
            Image target = new Bitmap(cropRect.Width, cropRect.Height);

            Graphics g = Graphics.FromImage(target);

            lock (src)
            {
                g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
                    cropRect,
                    GraphicsUnit.Pixel);
            }

            g.Dispose();

            return target;
        }

        public static object GetIconTexture(int Id)
        {
            String fileName = String.Format("Images\\ItemBtn{0}.png", Id);

            int idx = bitMapCache.FindIndex(p => p.Name == fileName);

            if (idx == -1)
            {
                if (!File.Exists(fileName))
                    return SystemIcons.Error.ToBitmap();
               
                bitMapCache.Add(new CacheMap(fileName, Image.FromFile(fileName) as Bitmap));

                idx = bitMapCache.Count - 1;

                return bitMapCache[idx].Image;
            }
            else
            {
                return bitMapCache[idx].Image;
            }
        }

        public static Rectangle GetIconRect(int Row, int Col)
        {
            return new Rectangle(32 * Col, 32 * Row, 32, 32);
        }
    }
}
