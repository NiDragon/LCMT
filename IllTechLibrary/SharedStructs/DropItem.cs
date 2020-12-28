using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IllTechLibrary.Serialization;

using IllTechLibrary.Attributes;
using IllTechLibrary.Util;

namespace IllTechLibrary.SharedStructs
{
    public class DropItem : SSClass
    {
        public DropItem()
        {
        }

        public DropItem(List<Object> MembData) : base(MembData) { }

        public int a_index;

        // Icon data
        public SByte a_texture_id;
        public SByte a_texture_row;
        public SByte a_texture_col;

        // For price updates
        public Int64 a_price;

        // For display
        [Locale("usa")]
        public String a_name_usa;
        [Locale("usa")]
        public String a_descr_usa;

        [Locale("thai")]
        public String a_name_thai;
        [Locale("thai")]
        public String a_descr_thai;
    }
}