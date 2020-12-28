using IllTechLibrary;
using IllTechLibrary.Settings;
using IllTechLibrary.Serialization;
using IllTechLibrary.SharedStructs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace LCMT.MagicTool
{
    public partial class MagicTool : LCToolFrm
    {
        private enum MAGIC_TYPE
        {
            MT_STAT = 0,
            MT_ATTRIBUTE,
            MT_ASSIST,
            MT_ATTACK,
            MT_RECOVER,
            MT_CURE,
            MT_OTHER,
            MT_REDUCE,
            MT_IMMUNE,
            MT_CASTLE_WAR,
            MT_MONEY
        }

        private List<MagicData> m_data = new List<MagicData>();
        private List<MagicLevel> m_levels = new List<MagicLevel>();

        private Deserialize<MagicData> magicDataDesc = new Deserialize<MagicData>("t_magic");
        private Deserialize<MagicLevel> magicLevelDesc = new Deserialize<MagicLevel>("t_magiclevel");

        /// <summary>
        /// The ID of the selected Magic Type
        /// </summary>
        private int SelectedIndex = -1;
        private int SelectedLevel = -1;

        public static String MagicToolID = "MAGIC_TOOL";

        public MagicTool() : base(MagicToolID)
        {
            InitializeComponent();
        }

        public override void OnConnect()
        {
            AddTask(DoLoadAll);
        }

        public override void OnDisconnect()
        {
            m_data.Clear();
            m_levels.Clear();

            ExecuteCleanUp();
        }

        private void DoLoadAll()
        {
            Invoke((MethodInvoker) delegate { ProgSpin.Visible = true; });

            TaskBlock(delegate
            {
                m_data = new Transactions<MagicData>(DataCon).ExecuteQuery(magicDataDesc).OrderBy(p => p.a_index).ToList();
            });

            TaskBlock(delegate
            {
                m_levels = new Transactions<MagicLevel>(DataCon).ExecuteQuery(magicLevelDesc).OrderBy(p => p.a_index).ToList();
            });

            Invoke((MethodInvoker)delegate { BuildLists(); });
        }

        private void ClearUI()
        {
            tb_name.Clear();
            tb_maxLevel.Clear();

            cb_type.SelectedIndex = -1;
            cb_subType.SelectedIndex = -1;
            cb_damageType.SelectedIndex = -1;
            cb_hitType.SelectedIndex = -1;
            cb_atkAttr.SelectedIndex = -1;

            tb_psp.Clear();
            tb_ptp.Clear();
            tb_hsp.Clear();
            tb_htp.Clear();

            chk_toggle.Checked = false;

            tb_power.Clear();
            tb_hitrate.Clear();
        }

        private void BuildLists()
        {
            lb_magic.Items.AddRange(m_data.Select(p => p.a_index + " - " + p.a_name).ToArray());
            ProgSpin.Visible = false;
        }

        private void TypeSelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox ctrl = (ComboBox)sender;

            // No selected index return
            if (ctrl.SelectedIndex == -1)
                return;

            int magicType = int.Parse(ctrl.Items[ctrl.SelectedIndex].ToString().Split(new char[] { '-' })[0]);

            cb_subType.SelectedIndex = -1;
            cb_subType.Items.Clear();

            MAGIC_TYPE type = (MAGIC_TYPE)magicType;

            cb_damageType.SelectedIndex = -1;
            cb_damageType.Items.Clear();

            if(type == MAGIC_TYPE.MT_STAT)
            {
                string[] stat_damage_types = 
                {
                    "0 - Power Only",
                    "1 - Addition",
                    "2 - Rate"
                };

                cb_damageType.Items.AddRange(stat_damage_types);
            }
            else
            {
                string[] other_damage_types =
                {
                    "0 - None",
                    "1 - Attack",
                    "2 - Defense"
                };

                cb_damageType.Items.AddRange(other_damage_types);
            }

            cb_damageType.SelectedIndex = 0;

            switch (type)
            {
                case MAGIC_TYPE.MT_STAT:
                    string[] stat_strings = 
                    {
                        "0 - Attack",
                        "1 - Defense",
                        "2 - Magic",
                        "3 - Resist",
                        "4 - Hitrate",
                        "5 - Avoid",
                        "6 - Critical",
                        "7 - Attack Speed",
                        "8 - Magic Speed",
                        "9 - Move Speed",
                        "10 - Recover HP",
                        "11 - Recover MP",
                        "12 - Max HP",
                        "13 - Max MP",
                        "14 - Deadly",
                        "15 - Magic Hitrate",
                        "16 - Magic Avoid",
                        "17 - Attack Dist",
                        "18 - Attack Melee",
                        "19 - Attack Range",
                        "20 - Hitrate Skill",
                        "21 - Attack 80",
                        "22 - Max HP 450",
                        "23 - Skill Speed",
                        "24 - Valor",
                        "25 - Stat Point All",
                        "26 - Attack Percent",
                        "27 - Defense Percent",
                        "28 - Stat Point All Precent",
                        "29 - Stat Str",
                        "30 - Stat Dex",
                        "31 - Stat Int",
                        "32 - Stat Con",
                        "33 - Stat Hard",
                        "34 - Stat Strong",
                        "35 - Stat Npc Attack",
                        "36 - Stat Npc Magic",
                        "37 - Skill Cooldown",
                        "38 - Decrease Mana Cost"
                    };

                    cb_subType.Items.AddRange(stat_strings);
                    break;
                case MAGIC_TYPE.MT_ATTRIBUTE:
                    string[] attribute_strings =
                    {
                        "0 - None",
                        "1 - Fire",
                        "2 - Water",
                        "3 - Earth",
                        "4 - Wind",
                        "5 - Dark",
                        "6 - Light",
                        "7 - Random"
                    };

                    cb_subType.Items.AddRange(attribute_strings);
                    break;
                case MAGIC_TYPE.MT_ASSIST:
                    string[] assist_strings =
                    {
                        "0 - Poison",
                        "1 - Hold",
                        "2 - Confusion",
                        "3 - Stone",
                        "4 - Silent",
                        "5 - Blood",
                        "6 - Blind",
                        "7 - Stun",
                        "8 - Sleep",
                        "9 - HP",
                        "10 - MP",
                        "11 - Move Speed",
                        "12 - HP Cancel",
                        "13 - MP Cancel",
                        "14 - Dizzy",
                        "15 - Invisible",
                        "16 - Sloth",
                        "17 - Fear",
                        "18 - Fake Death",
                        "19 - Perfect Body",
                        "20 - Frenzy",
                        "21 - Damage Link",
                        "22 - Berserk",
                        "23 - Despair",
                        "24 - Mana Screen",
                        "25 - Bless",
                        "26 - Safe Guard",
                        "27 - Mantle",
                        "28 - Guard",
                        "29 - Charge ATC",
                        "30 - Charge MGC",
                        "31 - Disease",
                        "32 - Curse",
                        "33 - Confusion",
                        "34 - Taming",
                        "35 - Freeze",
                        "36 - Inverse Damage",
                        "37 - HP DoT",
                        "38 - Rebirth",
                        "39 - Darkness Mode",
                        "40 - Aura Darkness",
                        "41 - Aura Weakness",
                        "42 - Aura Illusion",
                        "43 - Mercenary",
                        "44 - Soul Totem Buff",
                        "45 - Soul Totem Attk",
                        "46 - Trap",
                        "47 - Parasite",
                        "48 - Suicide",
                        "49 - Invincibility",
                        "50 - GPS",
                        "51 - Attack Tower",
                        "52 - Artifact GPS",
                        "53 - Totem Item Buff",
                        "54 - Totem Item Attk"
                    };

                    cb_subType.Items.AddRange(assist_strings);
                    break;
                case MAGIC_TYPE.MT_ATTACK:
                    string[] attack_strings =
                    {
                        "0 - Normal",
                        "1 - Critical",
                        "2 - Drain",
                        "3 - One Shot Skill",
                        "4 - Deadly",
                        "5 - Hard",
                    };

                    cb_subType.Items.AddRange(attack_strings);
                    break;
                case MAGIC_TYPE.MT_RECOVER:
                    string [] recover_strings =
                    {
                        "0 - HP",
                        "1 - MP",
                        "2 - STM",
                        "3 - FAITH",
                        "4 - EXP",
                        "5 - SP",
                    };

                    cb_subType.Items.AddRange(recover_strings);
                    break;
                case MAGIC_TYPE.MT_CURE:
                    string[] cure_strings = 
                    {
                        "0 - Poison",
                        "1 - Hold",
                        "2 - Confusion",
                        "3 - Stone",
                        "4 - Silence",
                        "5 - Blood",
                        "6 - Rebirth",
                        "7 - Invisible",
                        "8 - Stun",
                        "9 - Sloth",
                        "10 - Not Help",
                        "11 - Blind",
                        "12 - Disease",
                        "13 - Curse",
                        "14 - All",
                        "15 - Instant Death"
                    };

                    cb_subType.Items.AddRange(cure_strings);
                    break;
                case MAGIC_TYPE.MT_OTHER:
                    string[] other_strings =
                    {
                        "0 - Instant Death",
                        "1 - Skill Cancel",
                        "2 - Tackle",
                        "3 - Tackle2",
                        "4 - Reflex",
                        "5 - Death XP Plus",
                        "6 - Death SP Plus",
                        "7 - Telekinesis",
                        "8 - Tount",
                        "9 - Summon",
                        "10 - Evocation",
                        "11 - Target Free",
                        "12 - Curse",
                        "13 - Peace",
                        "14 - Soul Drain",
                        "15 - Knock Back",
                        "16 - Warp",
                        "17 - Fly",
                        "18 - EXP",
                        "19 - SP",
                        "20 - Item Drop",
                        "21 - Skill",
                        "22 - PK Disposition",
                        "23 - Affinity",
                        "24 - Affinity Quest",
                        "25 - Affinity Monster",
                        "26 - Affinity Item",
                        "27 - Quest EXP",
                        "28 - Guild Party Exp",
                        "29 - Guild Party SP",
                        "30 - Summon Npc"
                    };

                    cb_subType.Items.AddRange(other_strings);
                    break;
                case MAGIC_TYPE.MT_REDUCE:
                    string[] reduce_strings =
                    {
                        "0 - Melee",
                        "1 - Range",
                        "2 - Magic",
                        "3 - Skill",
                    };

                    cb_subType.Items.AddRange(reduce_strings);
                    break;
                case MAGIC_TYPE.MT_IMMUNE:
                    string[] immune_strings =
                    {
                        "0 - Blind",
                    };

                    cb_subType.Items.AddRange(immune_strings);
                    break;
                case MAGIC_TYPE.MT_CASTLE_WAR:
                    string[] war_strings =
                    {
                        "0 - Reduce Melee",
                        "1 - Reduce Range",
                        "2 - Reduce Magic",
                        "3 - Max HP",
                        "4 - Defense",
                        "5 - Resist",
                        "6 - Tower Attack"
                    };

                    cb_subType.Items.AddRange(war_strings);
                    break;
                case MAGIC_TYPE.MT_MONEY:
                    string[] money_strings =
                    {
                        "0 - Money Buy",
                        "1 - Money Sell",
                        "2 - Money Nas",
                    };

                    cb_subType.Items.AddRange(money_strings);
                    break;
                default:
                    break;
            }

            if (cb_subType.Items.Count != 0)
                cb_subType.SelectedIndex = 0;
        }

        private void MagicTool_FormClosing(object sender, FormClosingEventArgs e)
        {
            ExecuteCleanUp();
        }

        private void ExecuteCleanUp()
        {
            m_data.Clear();
            m_levels.Clear();

            ClearUI();

            SelectedIndex = -1;
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MaigcsSelectedIndexChanged(object sender, EventArgs e)
        {
            ClearUI();

            if (lb_magic.SelectedIndex == -1)
                return;

            SelectedIndex = int.Parse(lb_magic.Items[lb_magic.SelectedIndex].ToString().Split(new char[] { '-' })[0]);

            lb_magicLevels.Items.Clear();
            lb_magicLevels.Items.AddRange(m_levels.FindAll(p => p.a_index.Equals(SelectedIndex)).OrderBy(p => p.a_level).Select(p => p.a_level.ToString()).ToArray());
        }

        private void MagicLevelsSelectedIndexChanged(object sender, EventArgs e)
        {
            if (lb_magic.SelectedIndex == -1 || lb_magicLevels.SelectedIndex == -1)
                return;

            SelectedLevel = int.Parse(lb_magicLevels.Items[lb_magicLevels.SelectedIndex].ToString());

            MagicData md = m_data.Find(p => p.a_index.Equals(SelectedIndex));

            MagicLevel ml = m_levels.Find(p => p.a_index == SelectedIndex && p.a_level == SelectedLevel);

            tb_name.Text = md.a_name;
            tb_maxLevel.Text = md.a_maxlevel.ToString();

            cb_type.SelectedIndex = md.a_type;
            cb_subType.SelectedIndex = md.a_subtype;
            cb_damageType.SelectedIndex = md.a_damagetype;
            cb_hitType.SelectedIndex = md.a_hittype;
            cb_atkAttr.SelectedIndex = md.a_attribute;

            tb_psp.Text = md.a_psp.ToString();
            tb_ptp.Text = md.a_ptp.ToString();
            tb_hsp.Text = md.a_hsp.ToString();
            tb_htp.Text = md.a_htp.ToString();

            chk_toggle.Checked = md.a_togle == 1 ? true : false;
            
            tb_power.Text = ml.a_power.ToString();
            tb_hitrate.Text = ml.a_hitrate.ToString();
        }
    }
}
