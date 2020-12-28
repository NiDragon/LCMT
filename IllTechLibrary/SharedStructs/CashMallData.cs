using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using IllTechLibrary.Attributes;
using IllTechLibrary.Util;

namespace IllTechLibrary.SharedStructs
{
    public class CatalogData : SSClass
    {
        public CatalogData()
        {
        }

        public CatalogData(List<Object> MembData) : base(MembData) { }

        public uint a_ctid;
        public string a_ctname;
        public int a_category;
        public uint a_type;
        public int a_subtype;
        public uint a_cash;
        public uint a_mileage;
        public byte a_enable;

        public uint a_icon;

        [Locale("thai")]
        public string a_ctname_tld;
        [Locale("thai")]
        public string a_ctdesc_tld;

        [Locale("usa")]
        public string a_ctname_usa;
        [Locale("usa")]
        public string a_ctdesc_usa;
    }

    public class CashMallData : SSClass
    {
        public CashMallData()
        {
        }

        public CashMallData(List<Object> MembData) : base(MembData) { }

        public int a_index;

        public int a_user_idx;
        public int a_server;

        public int a_ctid;
        public int a_serial;

        public DateTime a_pdate;
        public byte a_confirm;
        public int a_use_char_idx;
        public DateTime a_use_date;

        public string a_ip;
    }

    public class GiftData : SSClass
    {
        public GiftData()
        {
        }

        public GiftData(List<Object> MembData) : base(MembData) { }

        public int a_index;

        public int a_send_user_idx;
        public string a_send_char_name;
        public string a_send_msg;

        public int a_server;

        public int a_ctid;

        public DateTime a_send_date;

        public int a_use_char_idx;
        public string a_recv_char_name;

        public DateTime a_use_date;
    }
}
