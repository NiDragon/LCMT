using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using IllTechLibrary;
using IllTechLibrary.Serialization;
using IllTechLibrary.Dialogs;
using IllTechLibrary.DataFiles;
using IllTechLibrary.SharedStructs;
using IllTechLibrary.Util;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LCMT.SkillTool
{
    public partial class SkillFrm : LCToolFrm
    {
        public const string SkillToolID = "SKILL_TOOL";

        // Editor Data
        private List<SkillData> m_skills = new List<SkillData>();
        private List<SkillLevel> m_skillLevels = new List<SkillLevel>();

        // Reference Data
        private List<MagicData> m_magic = new List<MagicData>();
        private List<MagicLevel> m_magicLevels = new List<MagicLevel>();

        private String LocaleName { get { return "a_name_" + Core.LangCode; } }
        private String LocaleDesc { get { return "a_client_description_" + Core.LangCode; } }
        private String LocaleTooltip { get { return "a_client_tooltip_" + Core.LangCode; } }

        public SkillFrm() : base(SkillToolID)
        {
            InitializeComponent();
        }

        public override void OnConnect()
        {
            AddTask(DoLoadAll);
        }

        public override void OnDisconnect()
        {
            CleanUp();

            GeneralStatsLabel.Text = $"Stats: ";
        }

        private void DoLoadAll()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            Invoke((MethodInvoker)delegate 
            {
                ClearAll(); ProgSpin.Visible = true;
            });

            TaskBlock(delegate() { 
                Deserialize<SkillData> skillDesc = new Deserialize<SkillData>("t_skill");
                m_skills = new Transactions<SkillData>(DataCon).ExecuteQuery(skillDesc).OrderBy(p => p.a_index).ToList();
            });

            TaskBlock(delegate () {
                Deserialize<SkillLevel> skillLevelDesc = new Deserialize<SkillLevel>("t_skilllevel");
                m_skillLevels = new Transactions<SkillLevel>(DataCon).ExecuteQuery(skillLevelDesc).OrderBy(p => p.a_index).ToList();
            });

            TaskBlock(delegate () {
                Deserialize<MagicData> magicDesc = new Deserialize<MagicData>("t_magic");
                m_magic = new Transactions<MagicData>(DataCon).ExecuteQuery(magicDesc).OrderBy(p => p.a_index).ToList();
            });

            TaskBlock(delegate () {
                Deserialize<MagicLevel> magicLevelDesc = new Deserialize<MagicLevel>("t_magiclevel");
                m_magicLevels = new Transactions<MagicLevel>(DataCon).ExecuteQuery(magicLevelDesc).OrderBy(p => p.a_index).ToList();
            });

            TimeSpan time = sw.Elapsed;
            sw.Stop();

            loadAllTime = time.TotalMilliseconds;

            Invoke((MethodInvoker)delegate
            {
                BuildSkillList();
                ProgSpin.Visible = false;
            });
        }

        double loadAllTime;

        private void BuildSkillList()
        {
            lb_skills.Items.Clear();
            lb_magics.Items.Clear();

            Deserialize<SkillData> skillDesc = new Deserialize<SkillData>("t_skill");
            lb_skills.Items.AddRange(m_skills.Select(p => p.a_index + " - " + skillDesc.SetData(p)[LocaleName]).ToArray());

            lb_magics.Items.AddRange(m_magic.Select(p => p.a_index.ToString() + " - " + p.a_name + " (" + String.Join(", ", m_magicLevels.FindAll(l => l.a_index.Equals(p.a_index)).OrderBy(k => k.a_level).Select(g => g.a_level.ToString()).ToArray()) + ")").ToArray());

            GeneralStatsLabel.Text = $"Stats: Total Skills - {m_skills.Count}, Load Time: {loadAllTime} ms";
        }

        private void ClearAll()
        {
            tb_search.Text = String.Empty;

            m_skills.Clear();
            m_skillLevels.Clear();
            m_magic.Clear();
            m_magicLevels.Clear();

            lb_skills.Items.Clear();
            lb_skillLevels.Items.Clear();

            lb_magics.Items.Clear();
        }

        private int SelectedSkill = -1;

        private void SelectedSkillChanged(object sender, EventArgs e)
        {
            int selectedIndex = lb_skills.SelectedIndex;

            if(selectedIndex != -1)
            {
                int skillID = int.Parse(lb_skills.Items[selectedIndex].ToString().Split(new char[] { '-' })[0]);

                BuildLevelList(skillID);

                SelectedSkill = skillID;
            }
            else
            {
                SelectedSkill = -1;
            }

            SelectedLevel = -1;
            SetLevelFields(SelectedLevel);

            SetSkillFields(SelectedSkill);
            SetAnimationFields(SelectedSkill);
        }

        private void SetSkillFields(int selectedSkill)
        {
            int idx = m_skills.FindIndex(p => p.a_index.Equals(selectedSkill));

            if(idx != -1)
            {
                Deserialize<SkillData> skillDesc = new Deserialize<SkillData>("t_skill");
                skillDesc.SetData(m_skills[idx]);

                // Basic Info
                tb_index.Text = m_skills[idx].a_index.ToString();

                cb_Job.SelectedIndex = cb_Job.FindString(m_skills[idx].a_job.ToString());

                cb_Job2.SelectedIndex = cb_Job2.FindString(m_skills[idx].a_job2.ToString());

                tb_name.Text = skillDesc[LocaleName].ToString();

                cb_type.SelectedIndex = cb_type.FindString(m_skills[idx].a_type.ToString());

                tb_flag.Text = m_skills[idx].a_flag.ToString();

                tb_maxLevel.Text = m_skills[idx].a_maxLevel.ToString();

                pb_Icon.Image = (Image)IconCache.GetSkillIcon(m_skills[idx].a_client_icon_texid,
                    m_skills[idx].a_client_icon_row,
                    m_skills[idx].a_client_icon_col);

                pb_Icon.Tag = $"{m_skills[idx].a_client_icon_texid},{m_skills[idx].a_client_icon_row},{m_skills[idx].a_client_icon_col}";

                // Target Info
                tb_applyRange.Text = m_skills[idx].a_appRange.ToString();
                tb_fireRange.Text = m_skills[idx].a_fireRange.ToString();
                tb_fireRange2.Text = m_skills[idx].a_fireRange2.ToString();

                cb_targetType.SelectedIndex = cb_targetType.FindString(m_skills[idx].a_targetType.ToString());

                num_targets.Value = m_skills[idx].a_targetNum;

                tb_useState.Text = m_skills[idx].a_useState.ToString();

                cb_weaponType0.SelectedIndex = cb_weaponType0.FindString(m_skills[idx].a_useWeaponType0.ToString());
                cb_weaponType1.SelectedIndex = cb_weaponType1.FindString(m_skills[idx].a_useWeaponType1.ToString());

                tb_applyState.Text = m_skills[idx].a_appState.ToString();

                tb_summon.Text = m_skills[idx].a_summon_idx.ToString();

                // Description
                tb_tooltip.Text = skillDesc[LocaleTooltip].ToString();
                tb_desc.Text = skillDesc[LocaleDesc].ToString();

                // Magic Info
                tb_magicIndex1.Text = m_skills[idx].a_useMagicIndex1.ToString();
                tb_magicLevel1.Text = m_skills[idx].a_useMagicLevel1.ToString();

                tb_magicIndex2.Text = m_skills[idx].a_useMagicIndex2.ToString();
                tb_magicLevel2.Text = m_skills[idx].a_useMagicLevel2.ToString();

                tb_magicIndex3.Text = m_skills[idx].a_useMagicIndex3.ToString();
                tb_magicLevel3.Text = m_skills[idx].a_useMagicLevel3.ToString();

                tb_fireTime.Text = m_skills[idx].a_fireTime.ToString();
                tb_readyTime.Text = m_skills[idx].a_readyTime.ToString();
                tb_stillTime.Text = m_skills[idx].a_stillTime.ToString();
                tb_reuseTime.Text = m_skills[idx].a_reuseTime.ToString();
            }
            else
            {
                // Clear the active skill
                // Basic Info
                tb_index.Text = "";

                cb_Job.SelectedIndex = 0;

                cb_Job2.SelectedIndex = 0;

                tb_name.Text = "";

                cb_type.SelectedIndex = 0;

                tb_flag.Text = "";

                tb_maxLevel.Text = "";

                pb_Icon.Image = (Image)IconCache.GetSkillIcon(0,
                    0,
                    0);

                // Target Info
                tb_applyRange.Text = "";
                tb_fireRange.Text = "";
                tb_fireRange2.Text = "";

                cb_targetType.SelectedIndex = 0;

                num_targets.Value = 0;

                tb_useState.Text = "";

                cb_weaponType0.SelectedIndex = 0;
                cb_weaponType1.SelectedIndex = 0;

                tb_applyState.Text = "";

                tb_summon.Text = "";

                // Description
                tb_tooltip.Text = "";
                tb_desc.Text = "";

                // Magic Info
                tb_magicIndex1.Text = "";
                tb_magicLevel1.Text = "";

                tb_magicIndex2.Text = "";
                tb_magicLevel2.Text = "";

                tb_magicIndex3.Text = "";
                tb_magicLevel3.Text = "";

                tb_fireTime.Text = "";
                tb_readyTime.Text = "";
                tb_stillTime.Text = "";
                tb_reuseTime.Text = "";
            }
        }

        private void SetAnimationFields(int selectedSkill)
        {
            int idx = m_skills.FindIndex(p => p.a_index.Equals(selectedSkill));

            if(idx != -1)
            {
                // Populate animation tab

                // First Weapon
                tb_readyanim1.Text = m_skills[idx].a_cd_ra;
                tb_readyeffect1.Text = m_skills[idx].a_cd_re;

                tb_stillanim1.Text = m_skills[idx].a_cd_sa;

                tb_fireanim1.Text = m_skills[idx].a_cd_fa;
                tb_fireeffect1_1.Text = m_skills[idx].a_cd_fe0;
                tb_fireeffect1_2.Text = m_skills[idx].a_cd_fe1;
                tb_fireeffect1_3.Text = m_skills[idx].a_cd_fe2;

                // Projectile info one
                tb_firetype1.Text = m_skills[idx].a_cd_fot.ToString();
                tb_firespeed1.Text = m_skills[idx].a_cd_fos.ToString();

                tb_fireX1.Text = m_skills[idx].a_cd_ox.ToString();
                tb_fireZ1.Text = m_skills[idx].a_cd_oz.ToString();
                tb_fireH1.Text = m_skills[idx].a_cd_oh.ToString();

                tb_firecoord1.Text = m_skills[idx].a_cd_oc.ToString();

                tb_delaycount1.Text = m_skills[idx].a_cd_fdc.ToString();

                tb_firedelay1_1.Text = m_skills[idx].a_cd_fd0.ToString();
                tb_firedelay1_2.Text = m_skills[idx].a_cd_fd1.ToString();
                tb_firedelay1_3.Text = m_skills[idx].a_cd_fd2.ToString();
                tb_firedelay1_4.Text = m_skills[idx].a_cd_fd3.ToString();

                tb_firedest1.Text = m_skills[idx].a_cd_dd.ToString();

                // Second Weapon
                tb_readyanim2.Text = m_skills[idx].a_cd_ra2;
                tb_readyeffect2.Text = m_skills[idx].a_cd_re2;

                tb_stillanim2.Text = m_skills[idx].a_cd_sa2;

                tb_fireanim2.Text = m_skills[idx].a_cd_fa2;
                tb_fireeffect2_1.Text = m_skills[idx].a_cd_fe3;
                tb_fireeffect2_2.Text = m_skills[idx].a_cd_fe4;
                tb_fireeffect2_3.Text = m_skills[idx].a_cd_fe5;

                // Projectile info two
                tb_firetype2.Text = m_skills[idx].a_cd_fot2.ToString();
                tb_firespeed2.Text = m_skills[idx].a_cd_fos2.ToString();

                tb_fireX2.Text = m_skills[idx].a_cd_ox2.ToString();
                tb_fireZ2.Text = m_skills[idx].a_cd_oz2.ToString();
                tb_fireH2.Text = m_skills[idx].a_cd_oh2.ToString();

                tb_firecoord2.Text = m_skills[idx].a_cd_oc2.ToString();

                tb_delaycount2.Text = m_skills[idx].a_cd_fdc2.ToString();

                tb_firedelay2_1.Text = m_skills[idx].a_cd_fd4.ToString();
                tb_firedelay2_2.Text = m_skills[idx].a_cd_fd5.ToString();
                tb_firedelay2_3.Text = m_skills[idx].a_cd_fd6.ToString();
                tb_firedelay2_4.Text = m_skills[idx].a_cd_fd7.ToString();

                tb_firedest2.Text = m_skills[idx].a_cd_dd2.ToString();

                tb_afterEffect.Text = m_skills[idx].a_cd_fe_after;
            }
            else
            {
                // Clear animation tab

                // First Weapon
                tb_readyanim1.Text = "";
                tb_readyeffect1.Text = "";

                tb_stillanim1.Text = "";

                tb_fireanim1.Text = "";
                tb_fireeffect1_1.Text = "";
                tb_fireeffect1_2.Text = "";
                tb_fireeffect1_3.Text = "";

                // Projectile info one
                tb_firetype1.Text = "";
                tb_firespeed1.Text = "";

                tb_fireX1.Text = "";
                tb_fireZ1.Text = "";
                tb_fireH1.Text = "";

                tb_firecoord1.Text = "";

                tb_delaycount1.Text = "";

                tb_firedelay1_1.Text = "";
                tb_firedelay1_2.Text = "";
                tb_firedelay1_3.Text = "";
                tb_firedelay1_4.Text = "";

                tb_firedest1.Text = "";

                // Second Weapon
                tb_readyanim2.Text = "";
                tb_readyeffect2.Text = "";

                tb_stillanim2.Text = "";

                tb_fireanim2.Text = "";
                tb_fireeffect2_1.Text = "";
                tb_fireeffect2_2.Text = "";
                tb_fireeffect2_3.Text = "";

                // Projectile info two
                tb_firetype2.Text = "";
                tb_firespeed2.Text = "";

                tb_fireX2.Text = "";
                tb_fireZ2.Text = "";
                tb_fireH2.Text = "";

                tb_firecoord2.Text = "";

                tb_delaycount2.Text = "";

                tb_firedelay2_1.Text = "";
                tb_firedelay2_2.Text = "";
                tb_firedelay2_3.Text = "";
                tb_firedelay2_4.Text = "";

                tb_firedest2.Text = "";
            }
        }

        private void PopulateJob2(int a_job)
        {
            cb_Job2.Items.Clear();

            switch (a_job)
            {
                case 0:
                    cb_Job2.Items.AddRange(new string[] { "0 - None", "1 - Warmaster", "2 - Highlander" });
                    break;
                case 1:
                    cb_Job2.Items.AddRange(new string[] { "0 - None", "1 - Royal Knight", "2 - Temple Knight" });
                    break;
                case 2:
                    cb_Job2.Items.AddRange(new string[] { "0 - None", "1 - Archer", "2 - Cleric" });
                    break;
                case 3:
                    cb_Job2.Items.AddRange(new string[] { "0 - None", "1 - Wizard", "2 - Witch" });
                    break;
                case 4:
                    cb_Job2.Items.AddRange(new string[] { "0 - None", "1 - Assasin", "2 - Ranger" });
                    break;
                case 5:
                    cb_Job2.Items.AddRange(new string[] { "0 - None", "1 - Elementalist", "2 - Specialist" });
                    break;
                case 6:
                    cb_Job2.Items.AddRange(new string[] { "0 - None" });
                    break;
                case 7:
                    cb_Job2.Items.AddRange(new string[] { "0 - None", "1 - Assasin", "2 - Ranger" });
                    break;
                case 8:
                    cb_Job2.Items.AddRange(new string[] { "0 - None", "1 - Wizard", "2 - Witch" });
                    break;
                case 10:
                    cb_Job2.Items.AddRange(new string[] { "0 - Hatchling", "1 - Adult", "2 - Mount" });
                    break;
                case 11:
                    cb_Job2.Items.AddRange(new string[] { "1 - Baby", "2 - Juvinial", "3 - Adult" });
                    break;
                case -1:
                case 99:
                case 999:
                default:
                    cb_Job2.Items.Add("0 - None");
                    break;
            }
        }

        private void BuildLevelList(int index)
        {
            lb_skillLevels.Items.Clear();

            List<SkillLevel> levels = m_skillLevels.FindAll(p => p.a_index.Equals(index)).OrderBy(p => p.a_level).ToList();

            lb_skillLevels.Items.AddRange(levels.Select(p => p.a_level.ToString()).ToArray());
        }

        private void DoSearch(object sender, EventArgs e)
        {
            lb_skills.SelectedIndex = -1;

            if(tb_search.Text != String.Empty)
            {
                Deserialize<SkillData> skillDesc = new Deserialize<SkillData>("t_skill");

                List<SkillData> searched = m_skills.FindAll(p => skillDesc.SetData(p)[LocaleName].ToString().ToLower().Contains(tb_search.Text));

                if (searched.Count != 0)
                {
                    lb_skills.Items.Clear();
                    lb_skills.Items.AddRange(searched.Select(p => p.a_index + " - " + skillDesc.SetData(p)[LocaleName]).ToArray());
                }
            }
            else
            {
                BuildSkillList();
            }
        }

        private int SelectedLevel = -1;

        private void SelectedLevelChanged(object sender, EventArgs e)
        {
            int idx = lb_skillLevels.SelectedIndex;

            if (SelectedSkill != -1 && idx != -1)
            {
                sbyte level = sbyte.Parse(lb_skillLevels.Items[idx].ToString());
                SelectedLevel = m_skillLevels.FindIndex(p => p.a_index.Equals(SelectedSkill) && p.a_level.Equals(level));
            }
            else
            {
                SelectedLevel = -1;
            }

            SetLevelFields(SelectedLevel);
        }

        private void SetLevelFields(int selectedLevel)
        {
            if(selectedLevel != -1)
            {
                // Base Cast Info
                tb_needHP.Text = m_skillLevels[selectedLevel].a_needHP.ToString();
                tb_needMP.Text = m_skillLevels[selectedLevel].a_needMP.ToString();
                tb_needGP.Text = m_skillLevels[selectedLevel].a_needGP.ToString();
                tb_duration.Text = m_skillLevels[selectedLevel].a_durtime.ToString();
                tb_power.Text = m_skillLevels[selectedLevel].a_dummypower.ToString();
                tb_useCount.Text = m_skillLevels[selectedLevel].a_use_count.ToString();
                tb_targetNum.Text = m_skillLevels[selectedLevel].a_targetNum.ToString();

                // Magic Info
                tb_castMagic1.Text = m_skillLevels[selectedLevel].a_magicIndex1.ToString();
                tb_castMagic2.Text = m_skillLevels[selectedLevel].a_magicIndex2.ToString();
                tb_castMagic3.Text = m_skillLevels[selectedLevel].a_magicIndex3.ToString();

                tb_castLevel1.Text = m_skillLevels[selectedLevel].a_magicLevel1.ToString();
                tb_castLevel2.Text = m_skillLevels[selectedLevel].a_magicLevel2.ToString();
                tb_castLevel3.Text = m_skillLevels[selectedLevel].a_magicLevel3.ToString();

                tb_appIndex1.Text = m_skillLevels[selectedLevel].a_appMagicIndex1.ToString();
                tb_appIndex2.Text = m_skillLevels[selectedLevel].a_appMagicIndex2.ToString();
                tb_appIndex3.Text = m_skillLevels[selectedLevel].a_appMagicIndex3.ToString();

                tb_appLevel1.Text = m_skillLevels[selectedLevel].a_appMagicLevel1.ToString();
                tb_appLevel2.Text = m_skillLevels[selectedLevel].a_appMagicLevel2.ToString();
                tb_appLevel3.Text = m_skillLevels[selectedLevel].a_appMagicLevel3.ToString();

                // Cast Items
                tb_needItem1.Text = m_skillLevels[selectedLevel].a_needItemIndex1.ToString();
                tb_needItemCount1.Text = m_skillLevels[selectedLevel].a_needItemCount1.ToString();

                tb_needItem2.Text = m_skillLevels[selectedLevel].a_needItemIndex2.ToString();
                tb_needItemCount2.Text = m_skillLevels[selectedLevel].a_needItemCount2.ToString();

                // Learn Info
                tb_learnLevel.Text = m_skillLevels[selectedLevel].a_learnLevel.ToString();
                tb_needSP.Text = m_skillLevels[selectedLevel].a_learnSP.ToString();

                tb_needStr.Text = m_skillLevels[selectedLevel].a_learnstr.ToString();
                tb_needDex.Text = m_skillLevels[selectedLevel].a_learndex.ToString();
                tb_needInt.Text = m_skillLevels[selectedLevel].a_learnint.ToString();
                tb_needCon.Text = m_skillLevels[selectedLevel].a_learncon.ToString();

                tb_learnItem1.Text = m_skillLevels[selectedLevel].a_learnItemIndex1.ToString();
                tb_learnItem2.Text = m_skillLevels[selectedLevel].a_learnItemIndex2.ToString();
                tb_learnItem3.Text = m_skillLevels[selectedLevel].a_learnItemIndex3.ToString();

                tb_learnitemCount1.Text = m_skillLevels[selectedLevel].a_learnItemCount1.ToString();
                tb_learnitemCount2.Text = m_skillLevels[selectedLevel].a_learnItemCount2.ToString();
                tb_learnitemCount3.Text = m_skillLevels[selectedLevel].a_learnItemCount3.ToString();

                tb_needSkill1.Text = m_skillLevels[selectedLevel].a_learnSkillIndex1.ToString();
                tb_needSkill2.Text = m_skillLevels[selectedLevel].a_learnSkillIndex2.ToString();
                tb_needSkill3.Text = m_skillLevels[selectedLevel].a_learnSkillIndex3.ToString();

                tb_learnSkillLevel1.Text = m_skillLevels[selectedLevel].a_learnSkillLevel1.ToString();
                tb_learnSkillLevel2.Text = m_skillLevels[selectedLevel].a_learnSkillLevel2.ToString();
                tb_learnSkillLevel3.Text = m_skillLevels[selectedLevel].a_learnSkillLevel3.ToString();
            }
            else
            {
                // Base Cast Info
                tb_needHP.Text = "";
                tb_needMP.Text = "";
                tb_needGP.Text = "";
                tb_duration.Text = "";
                tb_power.Text = "";
                tb_useCount.Text = "";
                tb_targetNum.Text = "";

                // Magic Info
                tb_castMagic1.Text = "";
                tb_castMagic2.Text = "";
                tb_castMagic3.Text = "";

                tb_castLevel1.Text = "";
                tb_castLevel2.Text = "";
                tb_castLevel3.Text = "";

                tb_appIndex1.Text = "";
                tb_appIndex2.Text = "";
                tb_appIndex3.Text = "";

                tb_appLevel1.Text = "";
                tb_appLevel2.Text = "";
                tb_appLevel3.Text = "";

                // Cast Items
                tb_needItem1.Text = "";
                tb_needItemCount1.Text = "";

                tb_needItem2.Text = "";
                tb_needItemCount2.Text = "";

                // Learn Info
                tb_learnLevel.Text = "";
                tb_needSP.Text = "";

                tb_needStr.Text = "";
                tb_needDex.Text = "";
                tb_needInt.Text = "";
                tb_needCon.Text = "";

                tb_learnItem1.Text = "";
                tb_learnItem2.Text = "";
                tb_learnItem3.Text = "";

                tb_learnitemCount1.Text = "";
                tb_learnitemCount2.Text = "";
                tb_learnitemCount3.Text = "";

                tb_needSkill1.Text = "";
                tb_needSkill2.Text = "";
                tb_needSkill3.Text = "";

                tb_learnSkillLevel1.Text = "";
                tb_learnSkillLevel2.Text = "";
                tb_learnSkillLevel3.Text = "";
            }
        }

        private void OnExit(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CleanUp()
        {
            lb_skills.SelectedIndex = -1;
            lb_skillLevels.SelectedIndex = -1;

            m_skills.Clear();
            m_skillLevels.Clear();
            m_magic.Clear();
            m_magicLevels.Clear();

            SelectedSkill = -1;
            SelectedLevel = -1;

            lb_skills.Items.Clear();
            lb_skillLevels.Items.Clear();
        }

        private void SkillFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CleanUp();
        }

        private void DoSelectIcon(object sender, EventArgs e)
        {
            int selected = lb_skills.SelectedIndex;

            if (selected == -1 || SelectedSkill == -1)
                return;

            IconPickerDlg ipd = new IconPickerDlg(IconPickerDlg.FileType.SkillBtn);

            if(ipd.ShowDialog() == DialogResult.OK)
            {
                //int skillIndex =  m_skills.FindIndex(p => p.a_index.Equals(SelectedSkill));

                IconPickerDlg.IconInfo info = ipd.GetInfo();

                /*m_skills[skillIndex].a_client_icon_texid = info.id;
                m_skills[skillIndex].a_client_icon_row = info.row;
                m_skills[skillIndex].a_client_icon_col = info.col;*/

                pb_Icon.Tag = $"{info.id},{info.row},{info.col}";

                pb_Icon.Image = (Image)IconCache.GetSkillIcon(info.id, info.row, info.col);
            }
        }

        private void SelectedJobChanged(object sender, EventArgs e)
        {
            int jobid = -1;

            if (cb_Job.SelectedIndex != 0)
            {
                 jobid = int.Parse(cb_Job.Text.Split(new char[] { '-' })[0]);
            }

            PopulateJob2(jobid);

            cb_Job2.SelectedIndex = 0;
        }

        private void NewSkillPress(object sender, EventArgs e)
        {
            Deserialize<SkillData> desc = new Deserialize<SkillData>("t_skill");

            desc.SetData(new SkillData());

            desc["a_index"] = m_skills.Max(p => p.a_index) + 1;
            desc[LocaleName] = "New Skill";
            desc[LocaleTooltip] = "New Tooltip";
            desc[LocaleDesc] = "No Description";

            m_skills.Add(desc.Serialize());

            new Transactions<SkillData>(DataCon).ExecuteQuery(desc, QUERY_TYPE.INSERT);

            BuildSkillList();

            lb_skills.SelectedIndex = lb_skills.Items.Count - 1;
        }

        private void BtnRemoveSkill_Click(object sender, EventArgs e)
        {
            if(SelectedSkill != -1 && lb_skills.SelectedIndex != -1)
            {
                int idx = m_skills.FindIndex(p => p.a_index.Equals(SelectedSkill));

                if(idx != -1)
                {
                    Deserialize<SkillData> desc = new Deserialize<SkillData>("t_skill");

                    desc.SetData(m_skills[idx]);

                    if (MsgDialogs.ShowNoLog("Warning!",
                        $"You are about to delete the skill \'{desc[LocaleName]}\' are you sure?",
                        "YesNo",
                        MsgDialogs.MsgTypes.WARNING) == DialogResult.Yes)
                    {
                        desc.SetKey("a_index");
                        new Transactions<SkillData>(DataCon).ExecuteQuery(desc, QUERY_TYPE.DELETE);

                        lb_skills.SelectedIndex = -1;
                        m_skills.RemoveAt(idx);
                        BuildSkillList();
                    }
                }
            }
        }

        private void NewSkillLevelPress(object sender, EventArgs e)
        {
            if(lb_skills.SelectedIndex != -1)
            {
                Deserialize<SkillLevel> desc = new Deserialize<SkillLevel>("t_skilllevel");

                desc.SetData(new SkillLevel());

                List<SkillLevel> levels = m_skillLevels.FindAll(p => p.a_index.Equals(SelectedSkill));

                desc["a_index"] = SelectedSkill;

                if (levels.Count != 0)
                {
                    desc["a_level"] = (sbyte)(levels.Max(p => p.a_level) + 1);
                }
                else
                {
                    desc["a_level"] = (sbyte)1;
                }

                new Transactions<SkillLevel>(DataCon).ExecuteQuery(desc, QUERY_TYPE.INSERT);

                m_skillLevels.Add(desc.Serialize());

                lb_skillLevels.SelectedIndex = -1;

                BuildLevelList(SelectedSkill);

                lb_skillLevels.SelectedIndex = lb_skillLevels.Items.Count - 1;
            }
        }

        private void BtnDeleteLevel_Click(object sender, EventArgs e)
        {
            if(lb_skills.SelectedIndex != -1)
            {
                List<SkillLevel> levels = m_skillLevels.FindAll(p => p.a_index.Equals(SelectedSkill)).OrderBy(p => p.a_index).ToList();

                if(levels.Count != 0)
                {
                    SkillLevel obj = levels.Last();

                    if (MsgDialogs.ShowNoLog("Warning!",
                        "This operation can only delete the last level to keep sequential order do you wish to continue?",
                        "YesNo", MsgDialogs.MsgTypes.WARNING) == DialogResult.Yes)
                    {
                        Deserialize<SkillLevel> desc = new Deserialize<SkillLevel>("t_skilllevel");

                        desc.SetConditions($"where a_index = {obj.a_index} AND a_level = {obj.a_level}");

                        desc.SetData(obj);

                        new Transactions<SkillLevel>(DataCon).ExecuteQuery(desc, QUERY_TYPE.DELETE);

                        m_skillLevels.RemoveAt(m_skillLevels.FindIndex(p => p.a_index.Equals(obj.a_index) && p.a_level.Equals(obj.a_level)));

                        lb_skillLevels.SelectedIndex = -1;
                        BuildLevelList(SelectedSkill);
                    }
                }
            }
        }

        private void BtnUpdateLevel_Click(object sender, EventArgs e)
        {
            if(lb_skills.SelectedIndex != -1 && lb_skillLevels.SelectedIndex != -1)
            {
                int idx = m_skillLevels.FindIndex(p => p.a_index.Equals(SelectedSkill) && p.a_level.Equals(SelectedLevel));

                if(idx != -1)
                {
                    // Basic
                    m_skillLevels[idx].a_needHP = int.Parse(tb_needHP.Text);
                    m_skillLevels[idx].a_needMP = int.Parse(tb_needMP.Text);
                    m_skillLevels[idx].a_needGP = int.Parse(tb_needGP.Text);
                    m_skillLevels[idx].a_durtime = int.Parse(tb_duration.Text);
                    m_skillLevels[idx].a_dummypower = int.Parse(tb_power.Text);
                    m_skillLevels[idx].a_use_count = int.Parse(tb_useCount.Text);
                    m_skillLevels[idx].a_targetNum = int.Parse(tb_targetNum.Text);

                    // Magic
                    m_skillLevels[idx].a_magicIndex1 = int.Parse(tb_castMagic1.Text);
                    m_skillLevels[idx].a_magicIndex2 = int.Parse(tb_castMagic2.Text);
                    m_skillLevels[idx].a_magicIndex3 = int.Parse(tb_castMagic3.Text);
                    m_skillLevels[idx].a_magicLevel1 = sbyte.Parse(tb_castLevel1.Text);
                    m_skillLevels[idx].a_magicLevel2 = sbyte.Parse(tb_castLevel2.Text);
                    m_skillLevels[idx].a_magicLevel3 = sbyte.Parse(tb_castLevel3.Text);

                    m_skillLevels[idx].a_appMagicIndex1 = int.Parse(tb_appIndex1.Text);
                    m_skillLevels[idx].a_appMagicIndex2 = int.Parse(tb_appIndex2.Text);
                    m_skillLevels[idx].a_appMagicIndex3 = int.Parse(tb_appIndex3.Text);
                    m_skillLevels[idx].a_appMagicLevel1 = sbyte.Parse(tb_appLevel1.Text);
                    m_skillLevels[idx].a_appMagicLevel2 = sbyte.Parse(tb_appLevel2.Text);
                    m_skillLevels[idx].a_appMagicLevel3 = sbyte.Parse(tb_appLevel3.Text);

                    m_skillLevels[idx].a_needItemIndex1 = int.Parse(tb_needItem1.Text);
                    m_skillLevels[idx].a_needItemCount1 = int.Parse(tb_needItemCount1.Text);
                    m_skillLevels[idx].a_needItemIndex2 = int.Parse(tb_needItem2.Text);
                    m_skillLevels[idx].a_needItemCount2 = int.Parse(tb_needItemCount2.Text);

                    // Learn
                    m_skillLevels[idx].a_learnLevel = int.Parse(tb_learnLevel.Text);
                    m_skillLevels[idx].a_learnSP = int.Parse(tb_needSP.Text);

                    m_skillLevels[idx].a_learnstr = int.Parse(tb_needStr.Text);
                    m_skillLevels[idx].a_learndex = int.Parse(tb_needDex.Text);
                    m_skillLevels[idx].a_learnint = int.Parse(tb_needInt.Text);
                    m_skillLevels[idx].a_learncon = int.Parse(tb_needCon.Text);

                    m_skillLevels[idx].a_learnItemIndex1 = int.Parse(tb_learnItem1.Text);
                    m_skillLevels[idx].a_learnItemIndex2 = int.Parse(tb_learnItem2.Text);
                    m_skillLevels[idx].a_learnItemIndex3 = int.Parse(tb_learnItem3.Text);

                    m_skillLevels[idx].a_learnItemCount1 = int.Parse(tb_learnitemCount1.Text);
                    m_skillLevels[idx].a_learnItemCount2 = int.Parse(tb_learnitemCount2.Text);
                    m_skillLevels[idx].a_learnItemCount3 = int.Parse(tb_learnitemCount3.Text);

                    m_skillLevels[idx].a_learnSkillIndex1 = int.Parse(tb_needSkill1.Text);
                    m_skillLevels[idx].a_learnSkillIndex2 = int.Parse(tb_needSkill2.Text);
                    m_skillLevels[idx].a_learnSkillIndex3 = int.Parse(tb_needSkill3.Text);

                    m_skillLevels[idx].a_learnSkillLevel1 = sbyte.Parse(tb_learnSkillLevel1.Text);
                    m_skillLevels[idx].a_learnSkillLevel2 = sbyte.Parse(tb_learnSkillLevel2.Text);
                    m_skillLevels[idx].a_learnSkillLevel3 = sbyte.Parse(tb_learnSkillLevel3.Text);

                    Deserialize<SkillLevel> desc = new Deserialize<SkillLevel>("t_skilllevel");

                    desc.SetData(m_skillLevels[idx]);

                    desc.SetConditions($"where a_index = {m_skillLevels[idx].a_index} AND a_level = {m_skillLevels[idx].a_level}");

                    new Transactions<SkillLevel>(DataCon).ExecuteQuery(desc, QUERY_TYPE.UPDATE);
                }
            }
        }

        private void BtnUpdateSkill_Click(object sender, EventArgs e)
        {
            if(lb_skills.SelectedIndex != -1 && SelectedSkill != -1)
            {
                int idx = m_skills.FindIndex(p => p.a_index.Equals(SelectedSkill));

                if(idx != -1)
                {
                    // Basic Information On The Skill
                    if (!cb_Job.Items[cb_Job.SelectedIndex].ToString().Contains("-1"))
                    {
                        m_skills[idx].a_job = int.Parse(cb_Job.Items[cb_Job.SelectedIndex].ToString().Split(new char[] { '-' })[0]);
                    }
                    else
                    {
                        m_skills[idx].a_job = -1;
                    }

                    m_skills[idx].a_job2 = int.Parse(cb_Job2.Items[cb_Job2.SelectedIndex].ToString().Split(new char[] { '-' })[0]);

                    int[] iconInfo = pb_Icon.Tag.ToString().Split(new char[] { ',' }).Select(p => int.Parse(p)).ToArray();

                    m_skills[idx].a_client_icon_texid = iconInfo[0];
                    m_skills[idx].a_client_icon_row = iconInfo[1];
                    m_skills[idx].a_client_icon_col = iconInfo[2];

                    m_skills[idx].a_type = int.Parse(cb_type.Items[cb_type.SelectedIndex].ToString().Split(new char[] { '-' })[0]);

                    m_skills[idx].a_flag = int.Parse(tb_flag.Text);

                    m_skills[idx].a_maxLevel = sbyte.Parse(tb_maxLevel.Text);

                    m_skills[idx].a_appRange = float.Parse(tb_applyRange.Text);
                    m_skills[idx].a_fireRange = float.Parse(tb_fireRange.Text);
                    m_skills[idx].a_fireRange2 = float.Parse(tb_fireRange2.Text);

                    m_skills[idx].a_targetType = int.Parse(cb_targetType.Items[cb_targetType.SelectedIndex].ToString().Split(new char[] { '-' })[0]);

                    m_skills[idx].a_targetNum = Convert.ToInt32(num_targets.Value);

                    m_skills[idx].a_useState = int.Parse(tb_useState.Text);

                    m_skills[idx].a_fireTime = int.Parse(tb_fireTime.Text);
                    m_skills[idx].a_readyTime = int.Parse(tb_readyTime.Text);
                    m_skills[idx].a_stillTime = int.Parse(tb_stillTime.Text);
                    m_skills[idx].a_reuseTime = int.Parse(tb_reuseTime.Text);

                    if (!cb_weaponType0.Items[cb_weaponType0.SelectedIndex].ToString().Contains("-1"))
                    {
                        m_skills[idx].a_useWeaponType0 = int.Parse(cb_weaponType0.Items[cb_weaponType0.SelectedIndex].ToString().Split(new char[] { '-' })[0]);
                    }
                    else
                    {
                        m_skills[idx].a_useWeaponType0 = -1;
                    }

                    if (!cb_weaponType1.Items[cb_weaponType1.SelectedIndex].ToString().Contains("-1"))
                    {
                        m_skills[idx].a_useWeaponType1 = int.Parse(cb_weaponType1.Items[cb_weaponType1.SelectedIndex].ToString().Split(new char[] { '-' })[0]);
                    }
                    else
                    {
                        m_skills[idx].a_useWeaponType1 = -1;
                    }

                    m_skills[idx].a_appState = int.Parse(tb_applyState.Text);

                    m_skills[idx].a_summon_idx = int.Parse(tb_summon.Text);

                    m_skills[idx].a_useMagicIndex1 = int.Parse(tb_magicIndex1.Text);
                    m_skills[idx].a_useMagicIndex2 = int.Parse(tb_magicIndex2.Text);
                    m_skills[idx].a_useMagicIndex3 = int.Parse(tb_magicIndex3.Text);

                    m_skills[idx].a_useMagicLevel1 = int.Parse(tb_magicLevel1.Text);
                    m_skills[idx].a_useMagicLevel2 = int.Parse(tb_magicLevel2.Text);
                    m_skills[idx].a_useMagicLevel3 = int.Parse(tb_magicLevel3.Text);

                    // Animation Shit Here
                    m_skills[idx].a_cd_ra = tb_readyanim1.Text;
                    m_skills[idx].a_cd_ra2 = tb_readyanim2.Text;

                    m_skills[idx].a_cd_re = tb_readyeffect1.Text;
                    m_skills[idx].a_cd_re2 = tb_readyeffect2.Text;

                    m_skills[idx].a_cd_sa = tb_stillanim1.Text;
                    m_skills[idx].a_cd_sa2 = tb_stillanim2.Text;

                    m_skills[idx].a_cd_fa = tb_fireanim1.Text;
                    m_skills[idx].a_cd_fa2 = tb_fireanim2.Text;

                    m_skills[idx].a_cd_fe0 = tb_fireeffect1_1.Text;
                    m_skills[idx].a_cd_fe1 = tb_fireeffect1_2.Text;
                    m_skills[idx].a_cd_fe2 = tb_fireeffect1_3.Text;
                    m_skills[idx].a_cd_fe3 = tb_fireeffect2_1.Text;
                    m_skills[idx].a_cd_fe4 = tb_fireeffect2_2.Text;
                    m_skills[idx].a_cd_fe5 = tb_fireeffect2_3.Text;

                    m_skills[idx].a_cd_fe_after = tb_afterEffect.Text;
                    // End Left Boxes

                    // Start Right Boxes Top
                    m_skills[idx].a_cd_fot = sbyte.Parse(tb_firetype1.Text);
                    m_skills[idx].a_cd_fos = float.Parse(tb_firespeed1.Text);
                    m_skills[idx].a_cd_ox = float.Parse(tb_fireX1.Text);
                    m_skills[idx].a_cd_oz = float.Parse(tb_fireZ1.Text);
                    m_skills[idx].a_cd_oh = float.Parse(tb_fireH1.Text);
                    m_skills[idx].a_cd_dd = float.Parse(tb_firedest1.Text);
                    m_skills[idx].a_cd_oc = float.Parse(tb_firecoord1.Text);
                    m_skills[idx].a_cd_fdc = sbyte.Parse(tb_delaycount1.Text);

                    m_skills[idx].a_cd_fd0 = float.Parse(tb_firedelay1_1.Text);
                    m_skills[idx].a_cd_fd1 = float.Parse(tb_firedelay1_2.Text);
                    m_skills[idx].a_cd_fd2 = float.Parse(tb_firedelay1_3.Text);
                    m_skills[idx].a_cd_fd3 = float.Parse(tb_firedelay1_4.Text);

                    // Start Right Boxes Bottom                    
                    m_skills[idx].a_cd_fot2 = sbyte.Parse(tb_firetype2.Text);
                    m_skills[idx].a_cd_fos2 = float.Parse(tb_firespeed2.Text);
                    m_skills[idx].a_cd_ox2 = float.Parse(tb_fireX2.Text);
                    m_skills[idx].a_cd_oz2 = float.Parse(tb_fireZ2.Text);
                    m_skills[idx].a_cd_oh2 = float.Parse(tb_fireH2.Text);
                    m_skills[idx].a_cd_dd2 = float.Parse(tb_firedest2.Text);
                    m_skills[idx].a_cd_oc2 = float.Parse(tb_firecoord2.Text);
                    m_skills[idx].a_cd_fdc2 = sbyte.Parse(tb_delaycount2.Text);

                    m_skills[idx].a_cd_fd4 = float.Parse(tb_firedelay2_1.Text);
                    m_skills[idx].a_cd_fd5 = float.Parse(tb_firedelay2_2.Text);
                    m_skills[idx].a_cd_fd6 = float.Parse(tb_firedelay2_3.Text);
                    m_skills[idx].a_cd_fd7 = float.Parse(tb_firedelay2_4.Text);

                    // This is for the locale stuff
                    Deserialize<SkillData> desc = new Deserialize<SkillData>("t_skill");

                    desc.SetData(m_skills[idx]);

                    desc[LocaleName] = tb_name.Text;
                    desc[LocaleTooltip] = tb_tooltip.Text;
                    desc[LocaleDesc] = tb_desc.Text;

                    m_skills[idx] = desc.Serialize();
                    // End this locale stuff

                    desc.SetKey("a_index");

                    new Transactions<SkillData>(DataCon).ExecuteQuery(desc, QUERY_TYPE.UPDATE);

                    int lastSelected = m_skills[idx].a_index;

                    lb_skills.Items[lb_skills.SelectedIndex] = (m_skills[idx].a_index + " - " + desc[LocaleName]);
                }
            }
        }

        private void skillStringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(m_skills.Count != 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();

                sfd.Title = "Save Skill String (*.lod)";
                sfd.Filter = "LastChaos Lod (*.lod)|*.lod";

                if(sfd.ShowDialog() == DialogResult.OK)
                {
                    BinaryWriter bw = new BinaryWriter(File.Open(sfd.FileName, FileMode.Create));

                    bw.Write(m_skills.Count);
                    bw.Write(m_skills.Last().a_index);

                    Deserialize<SkillData> desc = new Deserialize<SkillData>("t_skill");

                    foreach(SkillData a in m_skills)
                    {
                        desc.SetData(a);

                        bw.Write(a.a_index);

                        bw.Write(Core.Encoder.GetByteCount(desc[LocaleName].ToString()));
                        bw.Write(Core.Encoder.GetBytes(desc[LocaleName].ToString()));

                        bw.Write(Core.Encoder.GetByteCount(desc[LocaleDesc].ToString()));
                        bw.Write(Core.Encoder.GetBytes(desc[LocaleDesc].ToString()));

                        bw.Write(Core.Encoder.GetByteCount(desc[LocaleTooltip].ToString()));
                        bw.Write(Core.Encoder.GetBytes(desc[LocaleTooltip].ToString()));
                    }

                    bw.Close();
                    bw.Dispose();

                    MsgDialogs.ShowNoLog("Save Complete!", "Finished Saving Skills String Lod.", "OK", MsgDialogs.MsgTypes.INFO);
                }
            }
        }

        private int CalculateSum(BinaryWriter file, int fileSize)
        {
            byte[] buffer = new byte[4];
            int fileSum = 0;

            file.Seek(0, SeekOrigin.Begin);

            for (int i = 0; i < fileSize; i += 20)
            {
                file.Seek(i, SeekOrigin.Begin);

                file.BaseStream.Read(buffer, 0, 4);

                fileSum += BitConverter.ToInt32(buffer, 0);
            }

            return fileSum;
        }

        private void EncodeFile(BinaryWriter file)
        {
            // end pointer.
            int fileSize = (int)file.BaseStream.Length;

            int encryptNumber = CalculateSum(file, fileSize);

            // go to end in file.	
            file.Seek(0, SeekOrigin.End);
            file.Write(encryptNumber);
        }

        private void WriteString(BinaryWriter bw, string text)
        {
            bw.Write(Encoding.ASCII.GetByteCount(text));
            bw.Write(Encoding.ASCII.GetBytes(text));
        }

        private void skillBinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_skills.Count != 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();

                sfd.Title = "Save Skill Lod (*.lod)";
                sfd.Filter = "LastChaos Lod (*.lod)|*.lod";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    BinaryWriter bw = new BinaryWriter(File.Open(sfd.FileName, FileMode.Create));

                    List<SkillData> useSkill = m_skills.FindAll(p => !p.a_job.Equals(-1));

                    // Was last skill index dunno why...
                    bw.Write(useSkill.Max(p => p.a_index));
                    //bw.Write(useSkill.Count);

                    foreach (SkillData sd in useSkill)
                    {
                        bw.Write(sd.a_index);

                        // Class & Level
                        bw.Write(sd.a_job);
                        bw.Write(sd.a_job2);
                        bw.Write(sd.a_apet_index);
                        bw.Write((sbyte)sd.a_type);
                        bw.Write(sd.a_flag);
                        bw.Write(sd.a_sorcerer_flag);
                        bw.Write(sd.a_maxLevel);

                        // Range Apply
                        bw.Write(sd.a_appRange);
                        bw.Write(sd.a_fireRange);
                        bw.Write(sd.a_fireRange2);

                        // Target
                        bw.Write((sbyte)sd.a_targetType);

                        // State Magic
                        bw.Write(sd.a_useState);
                        bw.Write(sd.a_useWeaponType0);
                        bw.Write(sd.a_useWeaponType1);
                        bw.Write(sd.a_useMagicIndex1);
                        bw.Write((sbyte)sd.a_useMagicLevel1);
                        bw.Write(sd.a_useMagicIndex2);
                        bw.Write((sbyte)sd.a_useMagicLevel2);
                        bw.Write(sd.a_useMagicIndex3);
                        bw.Write((sbyte)sd.a_useMagicLevel3);
                        bw.Write(sd.a_soul_consum);

                        // App State
                        bw.Write(sd.a_appState);

                        // Fire stuff
                        bw.Write(sd.a_readyTime);
                        bw.Write(sd.a_stillTime);
                        bw.Write(sd.a_fireTime);
                        bw.Write(sd.a_reuseTime);

                        // First Weapon Stuff
                        WriteString(bw, sd.a_cd_ra);
                        WriteString(bw, sd.a_cd_re);
                        WriteString(bw, sd.a_cd_sa);
                        WriteString(bw, sd.a_cd_fa);
                        WriteString(bw, sd.a_cd_fe0);
                        WriteString(bw, sd.a_cd_fe1);
                        WriteString(bw, sd.a_cd_fe2);

                        bw.Write(sd.a_cd_fot);
                        bw.Write(sd.a_cd_fos);
                        bw.Write(sd.a_cd_ox);
                        bw.Write(sd.a_cd_oz);
                        bw.Write(sd.a_cd_oh);
                        bw.Write((sbyte)sd.a_cd_oc);
                        bw.Write((sbyte)sd.a_cd_fdc);
                        bw.Write(sd.a_cd_fd0);
                        bw.Write(sd.a_cd_fd1);
                        bw.Write(sd.a_cd_fd2);
                        bw.Write(sd.a_cd_fd3);
                        bw.Write(sd.a_cd_dd);

                        // Second Weapon Stuff
                        WriteString(bw, sd.a_cd_ra2);
                        WriteString(bw, sd.a_cd_re2);
                        WriteString(bw, sd.a_cd_sa2);
                        WriteString(bw, sd.a_cd_fa2);
                        WriteString(bw, sd.a_cd_fe3);
                        WriteString(bw, sd.a_cd_fe4);
                        WriteString(bw, sd.a_cd_fe5);

                        bw.Write(sd.a_cd_fot2);
                        bw.Write(sd.a_cd_fos2);
                        bw.Write(sd.a_cd_ox2);
                        bw.Write(sd.a_cd_oz2);
                        bw.Write(sd.a_cd_oh2);
                        bw.Write((sbyte)sd.a_cd_oc2);
                        bw.Write((sbyte)sd.a_cd_fdc2);
                        bw.Write(sd.a_cd_fd4);
                        bw.Write(sd.a_cd_fd5);
                        bw.Write(sd.a_cd_fd6);
                        bw.Write(sd.a_cd_fd7);
                        bw.Write(sd.a_cd_dd2);

                        // After Effect
                        WriteString(bw, sd.a_cd_fe_after);

                        // Icon Data
                        bw.Write(sd.a_client_icon_texid);
                        bw.Write(sd.a_client_icon_row);
                        bw.Write(sd.a_client_icon_col);

                        List<SkillLevel> foundLevels = m_skillLevels.FindAll(p => p.a_index.Equals(sd.a_index)).OrderBy(p => p.a_level).ToList();

                        foreach(SkillLevel sl in foundLevels)
                        {
                            bw.Write(sl.a_needHP);
                            bw.Write(sl.a_needMP);
                            bw.Write(sl.a_needGP);
                            bw.Write(sl.a_durtime);
                            bw.Write(sl.a_dummypower);
                            bw.Write(sl.a_needItemIndex1);
                            bw.Write(sl.a_needItemCount1);
                            bw.Write(sl.a_needItemIndex2);
                            bw.Write(sl.a_needItemCount2);
                            bw.Write(sl.a_learnLevel);
                            bw.Write(sl.a_learnSP);

                            bw.Write(sl.a_learnSkillIndex1);
                            bw.Write(sl.a_learnSkillLevel1);
                            bw.Write(sl.a_learnSkillIndex2);
                            bw.Write(sl.a_learnSkillLevel2);
                            bw.Write(sl.a_learnSkillIndex3);
                            bw.Write(sl.a_learnSkillLevel3);

                            bw.Write(sl.a_learnItemIndex1);
                            bw.Write(sl.a_learnItemCount1);
                            bw.Write(sl.a_learnItemIndex2);
                            bw.Write(sl.a_learnItemCount2);
                            bw.Write(sl.a_learnItemIndex3);
                            bw.Write(sl.a_learnItemCount3);

                            bw.Write(sl.a_learnstr);
                            bw.Write(sl.a_learndex);
                            bw.Write(sl.a_learnint);
                            bw.Write(sl.a_learncon);

                            bw.Write(sl.a_appMagicIndex1);
                            bw.Write(sl.a_appMagicLevel1);
                            bw.Write(sl.a_appMagicIndex2);
                            bw.Write(sl.a_appMagicLevel2);
                            bw.Write(sl.a_appMagicIndex3);
                            bw.Write(sl.a_appMagicLevel3);

                            bw.Write(sl.a_magicIndex1);
                            bw.Write(sl.a_magicLevel1);
                            bw.Write(sl.a_magicIndex2);
                            bw.Write(sl.a_magicLevel2);
                            bw.Write(sl.a_magicIndex3);
                            bw.Write(sl.a_magicLevel3);

                            bw.Write(sl.a_learnGP);

                            // Attribute WTF?
                            WriteAttribute(bw, sl);

                            bw.Write(sl.a_targetNum);
                        }
                    }

                    bw.Write(-9999);
                    bw.Flush();

                    EncodeFile(bw);

                    bw.Close();
                    bw.Dispose();

                    MsgDialogs.ShowNoLog("Save Complete!", "Finished Saving Skills Lod.", "OK", MsgDialogs.MsgTypes.INFO);
                }
            }
        }

        private void WriteAttribute(BinaryWriter bw, SkillLevel sl)
        {
            int[] magicIndex = new int[3];
            sbyte[] magicLevel = new sbyte[3];

            magicIndex[0] = sl.a_magicIndex1;
            magicIndex[1] = sl.a_magicIndex2;
            magicIndex[2] = sl.a_magicIndex3;
            magicLevel[0] = sl.a_magicLevel1;
            magicLevel[1] = sl.a_magicLevel2;
            magicLevel[2] = sl.a_magicLevel3;

            sbyte AttackType = 0;
            sbyte AttackPow = 0;
            sbyte DefenseType = 0;
            sbyte DefensePow = 0;

            for(int i = 0; i < 3; i++)
            {
                int idx = m_magic.FindIndex(p => p.a_type.Equals((sbyte)1) && p.a_index.Equals(magicIndex[i]));
                int sIdx = m_magicLevels.FindIndex(p => p.a_index.Equals(magicIndex[i]) && p.a_level.Equals(magicLevel[i]));

                if (idx != -1)
                {
                    sbyte damageType = m_magic[idx].a_damagetype;

                    if (damageType == 1)
                    {
                        AttackType = m_magic[idx].a_subtype;
                    }
                    else if (damageType == 2)
                    {
                        DefenseType = m_magic[idx].a_subtype;
                    }


                    if (sIdx != -1)
                    {
                        if (damageType == 1)
                        {
                            AttackPow = (sbyte)m_magicLevels[sIdx].a_power;
                        }
                        else if (damageType == 2)
                        {
                            DefensePow = (sbyte)m_magicLevels[sIdx].a_power;
                        }
                    }
                }
            }

            bw.Write(AttackType);
            bw.Write(AttackPow);
            bw.Write(DefenseType);
            bw.Write(DefensePow);
        }

        private void BtnFlagBuild_Click(object sender, EventArgs e)
        {
            if (lb_skills.SelectedIndex != -1 && SelectedSkill != -1)
            {
                int idx = m_skills.FindIndex(p => p.a_index.Equals(SelectedSkill));

                if (idx != -1) {
                    SkillFlagBuilder sfb = new SkillFlagBuilder(m_skills[idx].a_flag);

                    if(sfb.ShowDialog() == DialogResult.OK)
                    {
                        tb_flag.Text = sfb.GetFlag();
                    }
                }
            }
        }

        private void BtnUseStateBuild_Click(object sender, EventArgs e)
        {
            if (lb_skills.SelectedIndex != -1 && SelectedSkill != -1)
            {
                int idx = m_skills.FindIndex(p => p.a_index.Equals(SelectedSkill));

                if (idx != -1)
                {
                    SkillUseFlagBuilder sfb = new SkillUseFlagBuilder(m_skills[idx].a_useState);

                    if (sfb.ShowDialog() == DialogResult.OK)
                    {
                        tb_useState.Text = sfb.GetFlag();
                    }
                }
            }
        }

        private void BtnApplyStateBuild_Click(object sender, EventArgs e)
        {
            if (lb_skills.SelectedIndex != -1 && SelectedSkill != -1)
            {
                int idx = m_skills.FindIndex(p => p.a_index.Equals(SelectedSkill));

                if (idx != -1)
                {
                    SkillUseFlagBuilder sfb = new SkillUseFlagBuilder(m_skills[idx].a_appState);

                    if (sfb.ShowDialog() == DialogResult.OK)
                    {
                        tb_applyState.Text = sfb.GetFlag();
                    }
                }
            }
        }

        private void fixStringsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(m_skills.Count != 0)
            {
                OpenFileDialog ofd = new OpenFileDialog();

                ofd.Title = "Open SkillString (*.lod)";
                ofd.Filter = "LastChaos Lod (*.lod)|*.lod";

                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    BinaryReader br = new BinaryReader(File.Open(ofd.FileName, FileMode.Open));

                    int count = br.ReadInt32();
                    int lastidx = br.ReadInt32();

                    Deserialize<SkillData> desc = new Deserialize<SkillData>("t_skill");

                    desc.SetKey("a_index");

                    while(br.BaseStream.Position != br.BaseStream.Length)
                    {
                        int skillID = br.ReadInt32();

                        int idx = m_skills.FindIndex(p => p.a_index.Equals(skillID));

                        if(idx != -1)
                        {
                            desc.SetData(m_skills[idx]);

                            desc[LocaleName] = Core.Encoder.GetString(br.ReadBytes(br.ReadInt32()));
                            desc[LocaleDesc] = Core.Encoder.GetString(br.ReadBytes(br.ReadInt32()));
                            desc[LocaleTooltip] = Core.Encoder.GetString(br.ReadBytes(br.ReadInt32()));

                            m_skills[idx] = desc.Serialize();
                        }
                        else
                        {
                            br.ReadBytes(br.ReadInt32());
                            br.ReadBytes(br.ReadInt32());
                            br.ReadBytes(br.ReadInt32());
                        }
                    }

                    new Transactions<SkillData>(DataCon).ExecuteQuery(desc, m_skills, QUERY_TYPE.UPDATE);

                    lb_skills.SelectedIndex = -1;
                    BuildSkillList();

                    MsgDialogs.ShowNoLog("Import Complete!", "Finished Importing String File To Database.", "ok", MsgDialogs.MsgTypes.INFO);
                }
            }
        }
    }
}
