using IllTechLibrary.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IllTechLibrary
{
    public class smcObject
    {
        public string Name;
        public string Texture;

        public smcObject()
        {
        }

        public smcObject(string Name, string Texture)
        {
            this.Name = Name;
            this.Texture = Texture;
        }
    }

    public struct smcMesh
    {
        public String SmcFile;
        public String FileName;
        public List<smcObject> Object;

        public smcMesh(String FileName)
        {
            this.SmcFile = String.Empty;
            this.FileName = FileName;
            this.Object = new List<smcObject>();
        }
    }

    public class SMCReader
    {
        public static List<smcMesh> ReadFileChecked(String RootPath, String FileName, out String SmcName)
        {
            List<smcMesh> ret = new List<smcMesh>();

            String lines = File.ReadAllText(FileName);
            SmcName = String.Empty;

            var defs = new TokenDefinition[]
            {
                // Thanks to [steven levithan][2] for this great quoted string
                // regex
                new TokenDefinition(@"([""'])(?:\\\1|.)*?\1", "QUOTED-STRING"),
                // Thanks to http://www.regular-expressions.info/floatingpoint.html
                new TokenDefinition(@"[-+]?\d*\.\d+([eE][-+]?\d+)?", "FLOAT"),
                new TokenDefinition(@"[-+]?\d+", "INT"),
                new TokenDefinition(@"#t", "TRUE"),
                new TokenDefinition(@"#f", "FALSE"),
                new TokenDefinition(@"[*<>\?\-+/A-Za-z->!]+", "SYMBOL"),
                new TokenDefinition(@"\.", "DOT"),
                new TokenDefinition(@"\(", "LEFT"),
                new TokenDefinition(@"\)", "RIGHT"),
                new TokenDefinition(@"\s", "SPACE"),
                new TokenDefinition(@",", "TICKMARK"),
                new TokenDefinition(@";", "SEMICOLON"),
                new TokenDefinition(@"{", "LEFT_BRACK"),
                new TokenDefinition(@"}", "RIGHT_BRACK")
            };

            TextReader r = new StringReader(lines);

            Lexer l = new Lexer(r, defs);

            String LastSymbol = "";
            int texIndex = 0;

            smcMesh mesh = new smcMesh();

            try
            {
                while (l.Next())
                {
                    if ((String)l.Token == "SYMBOL" && l.TokenContents != "TFNM")
                        LastSymbol = l.TokenContents;

                    if (LastSymbol == "NAME" && l.TokenContents != "TFNM")
                    {
                        if ((String)l.Token == "QUOTED-STRING")
                        {
                            SmcName = l.TokenContents.Replace("\"", "");
                        }
                    }

                    if (LastSymbol == "MESH" && l.TokenContents != "TFNM")
                    {
                        if ((String)l.Token == "QUOTED-STRING" && l.TokenContents != "}" && l.TokenContents != "{")
                        {
                            if (mesh.Object != null)
                                ret.Add(mesh);

                            String sPath = l.TokenContents;

                            if(sPath.Contains("\""))
                            {
                                MsgDialogs.LogError($"Invalid Mesh Path : {l.TokenContents} in {FileName}");
                                sPath = sPath.Replace("\"", "");
                            }

                            mesh = new smcMesh(sPath);

                            String meshPath = RootPath + sPath;

                            if (!File.Exists(meshPath))
                                MsgDialogs.LogError($"Mesh Not Found: {meshPath} in {FileName}");
                        }
                    }

                    if (LastSymbol == "TEXTURES" && l.TokenContents != "TFNM")
                    {
                        if ((String)l.Token == "QUOTED-STRING" && l.TokenContents != "}" && l.TokenContents != "{")
                        {
                            String texName = l.TokenContents;

                            if (texName.Contains("\""))
                            {
                                MsgDialogs.LogError($"Invalid Texture Path : {l.TokenContents} in {FileName}");
                                texName = texName.Replace("\"", "");
                            }

                            if (texIndex == 0)
                            {
                                // Create new SMC object
                                mesh.Object.Add(new smcObject());
                                mesh.Object.Last().Name = texName;
                                texIndex++;
                            }
                            else
                            {
                                // Append texture name to object
                                mesh.Object.Last().Texture = texName;
                                texIndex = 0;

                                String texPath = RootPath + texName;

                                if (!File.Exists(texPath))
                                    MsgDialogs.LogError(String.Format("Texture Not Found: {0} in {1}", texPath, FileName));
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show(String.Format("Failed to parse smc file: {0}", FileName), "Error", MessageBoxButtons.OK);
            }

            if (mesh.Object != null)
                ret.Add(mesh);

            return ret;
        }

        public static List<smcMesh> ReadFile(String FileName, out String SmcName)
        {
            List<smcMesh> ret = new List<smcMesh>();

            String lines = File.ReadAllText(FileName);
            SmcName = String.Empty;

            var defs = new TokenDefinition[]
            {
                // Thanks to [steven levithan][2] for this great quoted string
                // regex
                new TokenDefinition(@"([""'])(?:\\\1|.)*?\1", "QUOTED-STRING"),
                // Thanks to http://www.regular-expressions.info/floatingpoint.html
                new TokenDefinition(@"[-+]?\d*\.\d+([eE][-+]?\d+)?", "FLOAT"),
                new TokenDefinition(@"[-+]?\d+", "INT"),
                new TokenDefinition(@"#t", "TRUE"),
                new TokenDefinition(@"#f", "FALSE"),
                new TokenDefinition(@"[*<>\?\-+/A-Za-z->!]+", "SYMBOL"),
                new TokenDefinition(@"\.", "DOT"),
                new TokenDefinition(@"\(", "LEFT"),
                new TokenDefinition(@"\)", "RIGHT"),
                new TokenDefinition(@"\s", "SPACE"),
                new TokenDefinition(@",", "TICKMARK"),
                new TokenDefinition(@";", "SEMICOLON"),
                new TokenDefinition(@"{", "LEFT_BRACK"),
                new TokenDefinition(@"}", "RIGHT_BRACK")
            };

            TextReader r = new StringReader(lines);

            Lexer l = new Lexer(r, defs);

            String LastSymbol = "";
            int texIndex = 0;

            smcMesh mesh = new smcMesh();

            try
            {
                while (l.Next())
                {
                    if ((String)l.Token == "SYMBOL" && l.TokenContents != "TFNM")
                        LastSymbol = l.TokenContents;

                    if (LastSymbol == "NAME" && l.TokenContents != "TFNM")
                    {
                        if ((String)l.Token == "QUOTED-STRING")
                        {
                            SmcName = l.TokenContents.Replace("\"", "");
                        }
                    }

                    if (LastSymbol == "MESH" && l.TokenContents != "TFNM")
                    {
                        if ((String)l.Token == "QUOTED-STRING" && l.TokenContents != "}" && l.TokenContents != "{")
                        {
                            if (mesh.Object != null)
                                ret.Add(mesh);

                            mesh = new smcMesh(l.TokenContents.Replace("\"", ""));
                        }
                    }

                    if (LastSymbol == "TEXTURES" && l.TokenContents != "TFNM")
                    {
                        if ((String)l.Token == "QUOTED-STRING" && l.TokenContents != "}" && l.TokenContents != "{")
                        {
                            if (texIndex == 0)
                            {
                                mesh.Object.Add(new smcObject());
                                mesh.Object.Last().Name = l.TokenContents.Replace("\"", "");
                                texIndex++;
                            }
                            else
                            {
                                mesh.Object.Last().Texture = l.TokenContents.Replace("\"", "");
                                texIndex = 0;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show(String.Format("Failed to parse smc file: {0}", FileName), "Error", MessageBoxButtons.OK);
            }

            if (mesh.Object != null)
                ret.Add(mesh);

            return ret;
        }

        public static List<smcMesh> ReadFile(String FileName)
        {
            string[] strArray1 = Path.GetDirectoryName(FileName).Split(new char[1]
            {
        '\\'
            });
            string str = "";
            bool flag = true;
            for (int index = 0; index < Enumerable.Count<string>(strArray1); ++index)
            {
                if (strArray1[index].ToUpper() == "DATA")
                    flag = false;
                if (flag)
                    str = str + strArray1[index] + "\\";
            }
            List<string> list1 = Enumerable.ToList(File.ReadAllLines(FileName));
            for (int index = Enumerable.Count(list1) - 1; index >= 0; --index)
            {
                list1[index] = list1[index].Trim();
                list1[index] = list1[index].Replace("TFNM", "");
                if (list1[index].Contains("{") || list1[index].Contains("}") || (list1[index].Contains(",") || list1[index].Contains("NAME")) || (list1[index].Contains("COLISION") || list1[index].Contains("TEXTURES") || (list1[index].Contains("ANIM") || list1[index].Contains("SKELETON"))) || list1[index].Contains("_TAG"))
                    list1.RemoveAt(index);
            }
            int index1 = -1;
            List<smcMesh> list2 = new List<smcMesh>();
            for (int index2 = 0; index2 < Enumerable.Count(list1); ++index2)
            {
                try
                {
                    if (list1[index2].Substring(0, 4) == "MESH")
                    {
                        ++index1;
                        string[] strArray2 = list1[index2].Split(new char[1]
            {
              '"'
            });
                        list2.Add(new smcMesh(str + strArray2[1]));
                    }
                    else
                    {
                        string[] strArray2 = list1[index2].Split(new char[1]
            {
              '"'
            });
                        list2[index1].Object.Add(new smcObject(strArray2[1], str + strArray2[3]));
                    }
                }
                catch
                {
                }
            }
            return list2;
        }
    }
}
