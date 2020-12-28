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
    public class Item
    {
        public Item()
        {
        }

        public Item(List<Object> MembData)
        {
            int lastIndex = 0;

            List<FieldInfo> info = this.GetType().GetFields().ToList();

            try
            {
                for (int i = 0; i < info.Count(); i++)
                {
                    lastIndex = i;

                    if (Attribute.IsDefined(info[i], typeof(LocaleAttribute)))
                    {
                        if (((LocaleAttribute)Attribute.GetCustomAttribute(info[i],
                        typeof(LocaleAttribute))) != Core.LangCode)
                        {
                            info.RemoveAt(i);
                            i--;
                            continue;
                        }
                    }

                    info[i].SetValue(this, MembData[i]);
                }
            }
            catch (Exception e)
            {
                String message = e.Message;
                MsgDialogs.Show("Exception!", String.Format("{0}\nEntry Name: {1}", e.Message, info[lastIndex].Name), "ok", IllTechLibrary.Util.MsgDialogs.MsgTypes.ERROR);
            }
        }

        public int a_index;
        public int a_enable;
        public int a_job_flag;
        public int a_weight;
        public int a_fame;
        public int a_level;
        public int a_level2;
        public Int64 a_flag;
        public int a_wearing;
        public int a_type_idx;
        public int a_subtype_idx;

        // Crafting
        public int a_need_item0;
        public int a_need_item1;
        public int a_need_item2;
        public int a_need_item3;
        public int a_need_item4;
        public int a_need_item5;
        public int a_need_item6;
        public int a_need_item7;
        public int a_need_item8;
        public int a_need_item9;
        public int a_need_item_count0;
        public int a_need_item_count1;
        public int a_need_item_count2;
        public int a_need_item_count3;
        public int a_need_item_count4;
        public int a_need_item_count5;
        public int a_need_item_count6;
        public int a_need_item_count7;
        public int a_need_item_count8;
        public int a_need_item_count9;
        public int a_need_sskill;
        public int a_need_sskill_level;
        public int a_need_sskill2;
        public int a_need_sskill_level2;

        // Texture info
        public int a_texture_id;
        public int a_texture_row;
        public int a_texture_col;

        // magic numbers
        public int a_num_0;
        public int a_num_1;
        public int a_num_2;
        public int a_num_3;
        public int a_num_4;

        // Price
        public int a_price;

        public int a_max_use;

        // Set Items
        public int a_set_0;
        public int a_set_1;
        public int a_set_2;
        public int a_set_3;
        public int a_set_4;

        // Apperance
        public String a_file_smc;
        public String a_effect_name;
        public String a_attack_effect_name;
        public String a_damage_effect_name;

        // Localization Strings
        [Locale("usa")]
        public String a_name_usa;
        [Locale("thai")]
        public String a_name_thai;
        [Locale("usa")]
        public String a_descr_usa;
        [Locale("thai")]
        public String a_descr_thai;
        [Locale("jpn")]
        public String a_name_jpn;
        [Locale("jpn")]
        public String a_descr_jpn;
        [Locale("chn")]
        public string a_name_chn;
        [Locale("chn")]
        public string a_descr_chn;

        // Rare Option
        public int a_rare_index_0;
        public int a_rare_index_1;
        public int a_rare_index_2;
        public int a_rare_index_3;
        public int a_rare_index_4;
        public int a_rare_index_5;
        public int a_rare_index_6;
        public int a_rare_index_7;
        public int a_rare_index_8;
        public int a_rare_index_9;
        public int a_rare_prob_0;
        public int a_rare_prob_1;
        public int a_rare_prob_2;
        public int a_rare_prob_3;
        public int a_rare_prob_4;
        public int a_rare_prob_5;
        public int a_rare_prob_6;
        public int a_rare_prob_7;
        public int a_rare_prob_8;
        public int a_rare_prob_9;

        // Origin Values
        public int a_origin_variation1;
        public int a_origin_variation2;
        public int a_origin_variation3;
        public int a_origin_variation4;
        public int a_origin_variation5;
        public int a_origin_variation6;

        // RVR Values
        public int a_rvr_value;
        public int a_rvr_grade;

        // Durability??
        public uint a_durability;

        // WAR NIGGA
        public int a_castle_war;
    }

    public class ItemFortuneLOD
    {
        public ItemFortuneLOD()
        {
        }

        public ItemFortuneLOD(List<Object> MembData)
        {
            FieldInfo[] info = this.GetType().GetFields();

            try
            {
                for (int i = 0; i < info.Count(); i++)
                {
                    info[i].SetValue(this, MembData[i]);
                }
            }
            catch (Exception e)
            {
                String message = e.Message;
                MsgDialogs.Show("Exception!", e.Message, "ok", MsgDialogs.MsgTypes.ERROR);
            }
        }

        public int a_item_idx;
        public int a_skill_index;
        public int a_skill_level;
        public int a_string_index;
        public int a_prob;
    }
}
