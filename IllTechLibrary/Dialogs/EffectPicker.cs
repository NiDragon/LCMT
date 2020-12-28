using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IllTechLibrary.Dialogs
{
    public partial class EffectPicker : Form
    {
        private String filename = String.Empty;
        private String selectedAnimation = String.Empty;
        private static List<String> effectNames = new List<String>();

        private BinaryReader datReader = null;

        public EffectPicker(String filename)
        {
            InitializeComponent();
            this.filename = filename;

            Icon = Icon.ExtractAssociatedIcon(Assembly.GetEntryAssembly().Location);
        }

        private String ReadString(BinaryReader br)
        {
            char LastRead = 'Z';
            StringBuilder sb = new StringBuilder();

            while (LastRead >= 0x20)
            {
                char read = Encoding.ASCII.GetChars(br.ReadBytes(1))[0];

                if (LastRead >= 0x20)
                    sb.Append(read);

                LastRead = read;
            }

            br.BaseStream.Position = br.BaseStream.Position - 1;

            return sb.ToString().Replace("\r", "").Replace("\t", "");
        }

        private void Expect_ID(String ID, BinaryReader stream)
        {
            String found = Encoding.ASCII.GetString(stream.ReadBytes(4));
            bool result = found == ID;

            if (!result)
            {
                stream.Close();
                stream.Dispose();
                throw new Exception(String.Format("Expected ID {0} Found {1}", ID, found));
            }
        }

        private String Peek_ID(BinaryReader stream)
        {
            String ret = String.Empty;

            ret = Encoding.ASCII.GetString(stream.ReadBytes(4));
            stream.BaseStream.Position = stream.BaseStream.Position - 4;

            return ret;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            if (filename != "")
            {
                effectNames.Clear();

                datReader = new BinaryReader(File.Open(filename, FileMode.Open));

                #region BEGIN_NOTHING
                // effect begin
                Expect_ID("EFTB", datReader);
                #endregion

                Expect_ID("EPGM", datReader);
                CParticleGroupManagerRead(datReader);

#if IMP_LATER
            Expect_ID("EEFM", datReader);
            CEffectManagerRead(datReader);

            Expect_ID("EEGM", datReader);
            CEffectGroupManagerRead(datReader);

                #region END_NOTHING
            //effect end
            Expect_ID("EFTF", datReader);
                #endregion
#endif

                datReader.Close();
                datReader.Dispose();
            }

            foreach (String a in effectNames)
            {
                effectListBox.Items.Add(a);
            }
        }

        enum CParticlesCommonProcessType
        {
            PCPT_NONE = 0,
            PCPT_DYNAMIC_STATE,
            PCPT_FORCE,
            PCPT_POINT_GOAL,
            PCPT_CONTROL,
            PCPT_VELOCITY
        }

        CParticlesCommonProcessType CreateProcess(int pcpt)
        {
            switch (pcpt)
            {
                case 0: return CParticlesCommonProcessType.PCPT_NONE;
                case 1: return CParticlesCommonProcessType.PCPT_DYNAMIC_STATE;
                case 2: return CParticlesCommonProcessType.PCPT_FORCE;
                case 3: return CParticlesCommonProcessType.PCPT_POINT_GOAL;
                case 4: return CParticlesCommonProcessType.PCPT_CONTROL;
                case 5: return CParticlesCommonProcessType.PCPT_VELOCITY;
            }

            return CParticlesCommonProcessType.PCPT_NONE;
        }

        enum CParticlesEmitter
        {
            PET_NONE = 0,
            PET_SPHERE,
            PET_CONE,
            PET_CYLINDER
        }

        CParticlesEmitter CreateEmitter(int pet)
        {
            switch (pet)
            {
                case 0: return CParticlesEmitter.PET_NONE;
                case 1: return CParticlesEmitter.PET_SPHERE;
                case 2: return CParticlesEmitter.PET_CONE;
                case 3: return CParticlesEmitter.PET_CYLINDER;
            }

            return CParticlesEmitter.PET_NONE; ;
        }

        enum CParticlesAbsorption
        {
            PAT_NONE = 0,
            PAT_DEFAULT,
            PAT_SPHERE
        }

        CParticlesAbsorption CreateAbsorption(int pat)
        {
            switch (pat)
            {
                case 0: return CParticlesAbsorption.PAT_NONE;
                case 1: return CParticlesAbsorption.PAT_DEFAULT;
                case 2: return CParticlesAbsorption.PAT_SPHERE;
            }

            return CParticlesAbsorption.PAT_NONE;
        }

        enum CEffectType
        {
            ET_NOTHING = 0,
            ET_TERRAIN,
            ET_LIGHT,
            ET_PARTICLE,
            ET_SKA,
            ET_MDL,
            ET_TRACE,
            ET_SOUND,
            ET_SPLINEBILLBOARD,
            ET_ORBIT,
            ET_SHOCKWAVE,
            ET_SPLINEPATH,
            ET_CAMERA,
            ET_ENTITY
        }

        CEffectType CEffectFromType(int et)
        {
            switch (et)
            {
                case 0: return CEffectType.ET_NOTHING;
                case 1: return CEffectType.ET_TERRAIN;
                case 2: return CEffectType.ET_LIGHT;
                case 3: return CEffectType.ET_PARTICLE;
                case 4: return CEffectType.ET_SKA;
                case 5: return CEffectType.ET_MDL;
                case 6: return CEffectType.ET_TRACE;
                case 7: return CEffectType.ET_SOUND;
                case 8: return CEffectType.ET_SPLINEBILLBOARD;
                case 9: return CEffectType.ET_ORBIT;
                case 10: return CEffectType.ET_SHOCKWAVE;
                case 11: return CEffectType.ET_SPLINEPATH;
                case 12: return CEffectType.ET_CAMERA;
                case 13: return CEffectType.ET_ENTITY;
            }

            return CEffectType.ET_NOTHING;
        }

        private void CParticleGroupManagerRead(BinaryReader br)
        {
            Expect_ID("PGMG", br);

            byte ubVer = br.ReadByte();

            int dwSize = br.ReadInt32();

            for (int i = 0; i < dwSize; ++i)
            {
                ReadParticleGroup(br);
            }
        }

        private void ReadParticleGroup(BinaryReader br)
        {
            Expect_ID("PTGR", br);

            byte ubVer = br.ReadByte();

            String effectName = ReadString(br);

            effectNames.Add(effectName);

            // Literal New Line Crap
            br.ReadInt16();
            
            Expect_ID("DFNM", br);

            String Texture = Encoding.ASCII.GetString(br.ReadBytes(br.ReadInt32()));

            int eRenderType = br.ReadInt32();
            int eBlendType = br.ReadInt32();

            int mexWidth = br.ReadInt32();

            int mexHeight = br.ReadInt32();

            int col = br.ReadInt32();
            int row = br.ReadInt32();
            int count = br.ReadInt32();

            for (int i = 0; i < count; ++i)
            {
                int dwTemp = br.ReadInt32();

                switch (CreateProcess(dwTemp))
                {
                    case CParticlesCommonProcessType.PCPT_NONE:
                        break;
                    case CParticlesCommonProcessType.PCPT_DYNAMIC_STATE:
                        ReadProcessDynamic(br);
                        break;
                    case CParticlesCommonProcessType.PCPT_FORCE:
                        ReadProcessForce(br);
                        break;
                    case CParticlesCommonProcessType.PCPT_POINT_GOAL:
                        ReadProcessPointGoal(br);
                        break;
                    case CParticlesCommonProcessType.PCPT_CONTROL:
                        ReadProcessControl(br);
                        break;
                    case CParticlesCommonProcessType.PCPT_VELOCITY:
                        ReadProcessVelocity(br);
                        break;
                }
            }

            int exist = br.ReadInt32();

            if (exist > 0)
            {
                int dwTemp = br.ReadInt32();

                switch (CreateEmitter(dwTemp))
                {
                    case CParticlesEmitter.PET_SPHERE:
                        ReadParticlesEmitterSphere(br);
                        break;
                    case CParticlesEmitter.PET_CONE:
                        ReadParticlesEmitterCone(br);
                        break;
                    case CParticlesEmitter.PET_CYLINDER:
                        ReadParticlesEmitterCylinder(br);
                        break;
                }
            }

            exist = br.ReadInt32();

            if (exist > 0)
            {
                int dwTemp = br.ReadInt32();

                switch (CreateAbsorption(dwTemp))
                {
                    case CParticlesAbsorption.PAT_DEFAULT:
                        ReadParticleAbsorptionDefault(br);
                        break;
                    case CParticlesAbsorption.PAT_SPHERE:
                        ReadParticleAbsorptionSphere(br);
                        break;
                }
            }
        }

        private void CEffectManagerRead(BinaryReader br)
        {
            Expect_ID("EFMG", br);

            byte ubVer = br.ReadByte();

            int dwSize = br.ReadInt32();

            for(int i = 0; i < dwSize; i++)
            {
                int dwType = br.ReadInt32();

                switch (CEffectFromType(dwType))
                {
                    case CEffectType.ET_NOTHING:
                        break;
                    case CEffectType.ET_TERRAIN:
                        break;
                    case CEffectType.ET_LIGHT:
                        break;
                    case CEffectType.ET_PARTICLE:
                        break;
                    case CEffectType.ET_SKA:
                        break;
                    case CEffectType.ET_MDL:
                        break;
                    case CEffectType.ET_TRACE:
                        break;
                    case CEffectType.ET_SOUND:
                        break;
                    case CEffectType.ET_SPLINEBILLBOARD:
                        break;
                    case CEffectType.ET_ORBIT:
                        break;
                    case CEffectType.ET_SHOCKWAVE:
                        break;
                    case CEffectType.ET_SPLINEPATH:
                        break;
                    case CEffectType.ET_CAMERA:
                        break;
                    case CEffectType.ET_ENTITY:
                        break;
                }
            }
        }

        private void CEffectGroupManagerRead(BinaryReader br)
        {
        }

        private void ReadParticlesEmitterCylinder(BinaryReader br)
        {
            ReadParticlesEmitter(br);

            Expect_ID("PECY", br);

            byte ubVer = br.ReadByte();

            if(ubVer == 2)
            {
                br.ReadSingle();
                br.ReadSingle();

                br.ReadInt32();
                br.ReadInt32();
                br.ReadInt32();

                ReadCylinderDoubleSpace(br);
            }
            else
            {
                br.ReadSingle();
                br.ReadSingle();

                br.ReadInt32();
                br.ReadInt32();
                
                ReadCylinderDoubleSpace(br);
            }
        }

        private void ReadParticlesEmitterCone(BinaryReader br)
        {
            ReadParticlesEmitter(br);

            Expect_ID("PECN", br);

            byte ubVer = br.ReadByte();

            br.ReadSingle();
            br.ReadSingle();
            br.ReadInt32();

            br.ReadInt32();

            ReadConeDoubleSpace(br);
        }

        private void ReadParticleAbsorptionDefault(BinaryReader br)
        {
            ReadParticlesAbsorption(br);

            Expect_ID("PADF", br);

            byte ubVer = br.ReadByte();
        }

        private void ReadCylinderDoubleSpace(BinaryReader br)
        {
            Expect_ID("CDSP", br);

            byte ubVer = br.ReadByte();

            if (ubVer == 1)
            {

                br.ReadSingle();

                br.ReadSingle();

                br.ReadSingle();

                br.ReadSingle();

                br.ReadSingle();
                br.ReadSingle();
            }
        }

        private void ReadConeDoubleSpace(BinaryReader br)
        {
            Expect_ID("CDSP", br);

            byte ubVer = br.ReadByte();

            if (ubVer == 2)
            {

                br.ReadSingle();

                br.ReadSingle();

                br.ReadSingle();

                br.ReadSingle();

                br.ReadSingle();
                br.ReadSingle();
                br.ReadSingle();

                br.ReadSingle();

                br.ReadSingle();
            }
            //old version
            else if (ubVer == 1)
            {
                br.ReadSingle();

                br.ReadSingle();

                br.ReadSingle();

                br.ReadSingle();

                br.ReadSingle();
                br.ReadSingle();
                br.ReadSingle();
            }
        }

        private void ReadParticlesEmitterSphere(BinaryReader br)
        {
            ReadParticlesEmitter(br);

            Expect_ID("PEMS", br);

            byte ubVer = br.ReadByte();

            if (ubVer == 1)
            {
                ReadForce(br);
                br.ReadSingle();
                ReadSphereDoubleSpace(br);
            }
        }

        private void ReadProcessControl(BinaryReader br)
        {
            Expect_ID("PTPC", br);

            byte ubVer = br.ReadByte();

            if (ubVer == 2)
            {
                br.ReadInt32();

                br.ReadInt32();

                br.ReadSingle();

                br.ReadSingle();

                br.ReadInt32();
            }
            else if (ubVer == 1)
            {

                br.ReadInt32();

                br.ReadInt32();

                br.ReadSingle();

                br.ReadSingle();
            }
        }

        private void ReadProcessPointGoal(BinaryReader br)
        {
            Expect_ID("PPPG", br);

            byte ubVer = br.ReadByte();

            if (ubVer == 2)
            {
                float m_fLerpRatio = br.ReadSingle();

                float m_fLerpSpeed = br.ReadSingle();

                float m_fSpeed = br.ReadSingle();

                float m_vPointGoal1 = br.ReadSingle();
            
                float m_vPointGoal2 = br.ReadSingle();

                float m_vPointGoal3 = br.ReadSingle();

                String strTagName = Encoding.ASCII.GetString(br.ReadBytes(br.ReadInt32()));
            }
            //old version
            else if (ubVer == 1)
            {
                float m_fLerpRatio = br.ReadSingle();

                float m_fLerpSpeed = br.ReadSingle();

                float m_fSpeed = br.ReadSingle();

                float m_vPointGoal1 = br.ReadSingle();

                float m_vPointGoal2 = br.ReadSingle();

                float m_vPointGoal3 = br.ReadSingle();
            }
        }
        
        private void ReadProcessForce(BinaryReader br)
        {
            Expect_ID("PTPF", br);

            byte ubVer = br.ReadByte();

            if(ubVer == 2)
            {
                int dwCount = br.ReadInt32();

                for(int i = 0; i < dwCount; ++i)
                {
                    ReadForce(br);
                }
            }
            else if(ubVer == 1)
            {
                int dwCount = br.ReadInt32();

                for (int i = 0; i < dwCount; ++i)
                {
                    ReadForce(br);
                }

                br.ReadSingle();
            }
        }

        private void ReadProcessVelocity(BinaryReader br)
        {
            Expect_ID("PTPF", br);

            byte ubVer = br.ReadByte();

            // Velocity Dir
            br.ReadSingle();
            br.ReadSingle();
            br.ReadSingle();

            // Speed
            br.ReadSingle();
        }

        private void ReadParticlesAbsorption(BinaryReader br)
        {
            Expect_ID("PTAB", br);

            byte ubVer = br.ReadByte();

            br.ReadInt32();

            br.ReadInt32();
        }

        private void ReadParticlesAbsorptionSphere(BinaryReader br)
        {
            Expect_ID("PASP", br);

            // ubVer
            br.ReadByte();

            ReadParticleAbsorptionSphere(br);
        }

        private void ReadParticleAbsorptionSphere(BinaryReader br)
        {
            ReadParticlesAbsorption(br);

            Expect_ID("PASP", br);

            // ubVer
            br.ReadByte();

            ReadSphereDoubleSpace(br);
        }

        private void ReadSphereDoubleSpace(BinaryReader br)
        {
            Expect_ID("SDSP", br);

            // ubVer
            br.ReadByte();

            // Radius Information
            br.ReadSingle();
            br.ReadSingle();

            // Center of Sphere Double Space
            br.ReadSingle();
            br.ReadSingle();
            br.ReadSingle();
        }

        private void ReadForce(BinaryReader br)
        {
            Expect_ID("FOCE", br);

            // ubVer
            br.ReadByte();

            // Type
            br.ReadInt32();

            // Power
            br.ReadSingle();

            // Position
            br.ReadSingle();
            br.ReadSingle();
            br.ReadSingle();

            // Direction
            br.ReadSingle();
            br.ReadSingle();
            br.ReadSingle();
        }

        private void ReadParticlePrototype(BinaryReader br)
        {
            Expect_ID("PTPT", br);

            // ubVer
            br.ReadByte();

            br.ReadSingle();
            br.ReadSingle();
            br.ReadSingle();
            br.ReadSingle();
            br.ReadSingle();
            br.ReadSingle();

            br.ReadByte();
            br.ReadByte();

            br.ReadSingle();
            br.ReadSingle();

            br.ReadInt32();
            br.ReadInt32();

            br.ReadSingle();
            br.ReadSingle();
            br.ReadSingle();
            br.ReadSingle();
            br.ReadSingle();
            br.ReadSingle();
            br.ReadSingle();
            br.ReadSingle();
        }

        private void ReadParticlesEmitter(BinaryReader br)
        {
            Expect_ID("PTEM", br);

            // ubVer
            byte ver = br.ReadByte();

            switch(ver)
            {
                case 2:
                    br.ReadInt32();
                    br.ReadSingle();
                    ReadParticlePrototype(br);
                    br.ReadInt32();
                    break;
                case 1:
                    br.ReadInt32();
                    br.ReadSingle();
                    ReadParticlePrototype(br);
                    break;
            }

        }

        enum ReadFromStreamType
        {
            COLOR,
            UBYTE,
            CParticleTexPos,
            CParticleSize,
            FLOAT,
            FLOAT3D,
        }

        private void ReadCount(BinaryReader br, ReadFromStreamType rfst)
        {
            int bDynamic = br.ReadInt32();

            if (bDynamic > 0)
            {
                int count = br.ReadInt32();

                for (int i = 0; i < count; i++)
                {
                    float key = br.ReadSingle();

                    switch (rfst)
                    {
                        case ReadFromStreamType.COLOR:
                            br.ReadInt32();
                            break;
                        case ReadFromStreamType.UBYTE:
                            br.ReadByte();
                            break;
                        case ReadFromStreamType.CParticleTexPos:
                            br.ReadByte();
                            br.ReadByte();
                            break;
                        case ReadFromStreamType.CParticleSize:
                            br.ReadSingle();
                            br.ReadSingle();
                            break;
                        case ReadFromStreamType.FLOAT:
                            br.ReadSingle();
                            break;
                        case ReadFromStreamType.FLOAT3D:
                            br.ReadSingle();
                            br.ReadSingle();
                            br.ReadSingle();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void ReadProcessDynamic(BinaryReader br)
        {
            Expect_ID("PPDS", br);

            byte ubVer = br.ReadByte();

            switch (ubVer)
            {
                case 5:
                    // Timing Info
                    br.ReadSingle();
                    br.ReadSingle();
                    br.ReadSingle();

                    ReadCount(br, ReadFromStreamType.COLOR);

                    // Alpha
                    ReadCount(br, ReadFromStreamType.UBYTE);

                    // Something Pos
                    ReadCount(br, ReadFromStreamType.CParticleTexPos);

                    // Something Size?
                    ReadCount(br, ReadFromStreamType.CParticleSize);

                    // Mass
                    ReadCount(br, ReadFromStreamType.FLOAT);

                    // Position
                    ReadCount(br, ReadFromStreamType.FLOAT3D);

                    // Angle
                    ReadCount(br, ReadFromStreamType.FLOAT3D);
                    break;
                case 4:
                    // Timing Info
                    br.ReadSingle();
                    br.ReadSingle();
                    br.ReadSingle();

                    // Color
                    ReadCount(br, ReadFromStreamType.COLOR);

                    // Alpha
                    ReadCount(br, ReadFromStreamType.UBYTE);

                    // Something Pos
                    ReadCount(br, ReadFromStreamType.CParticleTexPos);

                    // Something Size?
                    ReadCount(br, ReadFromStreamType.CParticleSize);

                    // Mass
                    ReadCount(br, ReadFromStreamType.FLOAT);

                    // Position
                    ReadCount(br, ReadFromStreamType.FLOAT3D);

                    // Dynamic Angle?
                    int adynamic = br.ReadInt32();

                    if(adynamic > 0)
                    {
                        int wordCounts = br.ReadInt32();

                        for(int i = 0; i < wordCounts; i++)
                        {
                            // Keyframe?
                            br.ReadSingle();

                            // Quad3D Trasformation?
                            br.ReadSingle();
                            br.ReadSingle();
                            br.ReadSingle();
                        }
                    }

                    break;
                case 3:
                    // Timing Info
                    br.ReadSingle();
                    br.ReadSingle();
                    br.ReadSingle();

                    // Color
                    ReadCount(br, ReadFromStreamType.COLOR);

                    // Alpha
                    ReadCount(br, ReadFromStreamType.UBYTE);

                    // Something Pos
                    ReadCount(br, ReadFromStreamType.CParticleTexPos);

                    // Something Size?
                    ReadCount(br, ReadFromStreamType.CParticleSize);

                    // Mass
                    ReadCount(br, ReadFromStreamType.FLOAT);

                    // Position
                    ReadCount(br, ReadFromStreamType.FLOAT3D);
                    break;
                default:
                    throw new Exception(String.Format("Looks like we have to implement this shit case {0} ReadProcessDynamic.", ubVer));
            }
        }

        public String GetName()
        {
            return selectedAnimation;
        }

        private void OnSelectEffect(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            selectedAnimation = effectListBox.Items[effectListBox.SelectedIndex].ToString();
            this.Close();
        }

        private void OnSearchTextChanged(object sender, EventArgs e)
        {
            if (effectListBox.Items.Count == 0)
                return;


            if (search_text.Text == String.Empty)
            {
                effectListBox.Items.Clear();
                effectListBox.Items.AddRange(effectNames.ToArray());
                return;
            }

            List<String> items = effectNames.FindAll(p => p.Contains(search_text.Text));

            if (items.Count != 0)
            {
                effectListBox.Items.Clear();
                effectListBox.Items.AddRange(items.ToArray());

                effectListBox.SelectedIndex = 0;
            }
        }
    }
}
