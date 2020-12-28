using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using IllTechLibrary;
using IllTechLibrary.Util;

namespace NpcView
{
    public class tSmcModel
    {
        public string FileName;
        public Dictionary<String, tSmcLCTexs> LCTexs;
    }

    public class tSmcLCTexs
    {
        public string Name;
        public string File;
    }

    public class tSmcColision
    {
        public string Name;
        public float[] Params;
    }

    public class SmcFile
    {
        public float[] Offset;
        public string Name = "";
        public List<tSmcModel> Model;
        public string SkeletonFileName = "";
        public string AnimSetFileName = "";
        public float[] BBoX;
        public tSmcColision[] Collision;

        public List<string> Includes;

        public bool ReadFileNew(string FileName)
        {
            Model = new List<tSmcModel>();
            Includes = new List<string>();

            if (FSUtil.FilePathHasInvalidChars(FileName))
            {
                MsgDialogs.LogError(String.Format("Invalid Smc Path: {0}", FileName));
                return false;
            }

            String lines = File.ReadAllText(FileName);

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
                new TokenDefinition(@"}", "RIGHT_BRACK"),
                new TokenDefinition(@"#INCLUDE", "INC")
            };

            TextReader r = new StringReader(lines);

            Lexer l = new Lexer(r, defs);

            String LastSymbol = "";
            int texIndex = 0;

            while (l.Next())
            {
                if ((String)l.Token == "SYMBOL" && l.TokenContents != "TFNM")
                    LastSymbol = l.TokenContents;

                if ((String)l.Token == "INC")
                    LastSymbol = "#INCLUDE";

                if (LastSymbol == "NAME" && l.TokenContents != "TFNM")
                {
                    if ((String)l.Token == "QUOTED-STRING")
                    {
                        Name = l.TokenContents.Replace("\"", "");
                    }
                }

                if (LastSymbol == "SKELETON" && l.TokenContents != "TFNM")
                {
                    if ((String)l.Token == "QUOTED-STRING")
                    {
                        SkeletonFileName = l.TokenContents.Replace("\"", "");
                    }
                }

                if (LastSymbol == "ANIMSET" && l.TokenContents != "TFNM")
                {
                    if ((String)l.Token == "QUOTED-STRING")
                    {
                        AnimSetFileName = l.TokenContents.Replace("\"", "");
                    }
                }

                if (LastSymbol == "ANIMEFFECT" && l.TokenContents != "TFNM")
                {
                    if ((String)l.Token == "QUOTED-STRING")
                    {
                        String AfxFile = l.TokenContents.Replace("\"", "");
                    }
                }

                if (LastSymbol == "ALLFRAMESBBOX")
                {
                    if ((String)l.Token == "QUOTED-STRING")
                    {
                        String BBox = l.TokenContents.Replace("\"", "");
                    }
                }

                if (LastSymbol == "MESH" && l.TokenContents != "TFNM")
                {
                    if ((String)l.Token == "QUOTED-STRING" && l.TokenContents != "}" && l.TokenContents != "{")
                    {
                        tSmcModel nMesh = new tSmcModel();
                        nMesh.LCTexs = new Dictionary<string, tSmcLCTexs>();

                        nMesh.FileName = l.TokenContents.Replace("\"", "");

                        Model.Add(nMesh);

                        texIndex = Model.Count - 1;
                    }
                }

                if (LastSymbol == "#INCLUDE" && l.TokenContents != "TFNM")
                {
                    if((String)l.Token == "QUOTED-STRING")
                    {
                        ReadInc(IllTechLibrary.Settings.Preferences.GetString("CLIENT", "root") + l.TokenContents.Replace("\"", ""));
                    }
                }

                if (LastSymbol == "TEXTURES" && l.TokenContents != "TFNM")
                {
                    tSmcLCTexs nTex = new tSmcLCTexs();

                    if ((String)l.Token == "QUOTED-STRING" && l.TokenContents != "}" && l.TokenContents != "{")
                    {
                        nTex.Name = l.TokenContents.Replace("\"", "");

                        l.Next();

                        while((String)l.Token != "QUOTED-STRING" && l.Next())
                        {
                        }

                        if ((String)l.Token == "QUOTED-STRING" && l.TokenContents != "}" && l.TokenContents != "{")
                        {
                            nTex.File = l.TokenContents.Replace("\"", "");

                            Model[texIndex].LCTexs.Add(Model[texIndex].FileName, nTex);
                        }
                    }
                }
            }

            return true;
        }

        private void ReadInc(string FileName)
        {
            String lines = File.ReadAllText(FileName);

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

            while (l.Next())
            {
                if ((String)l.Token == "SYMBOL" && l.TokenContents != "TFNM")
                    LastSymbol = l.TokenContents;

                if (LastSymbol == "NAME" && l.TokenContents != "TFNM")
                {
                    if ((String)l.Token == "QUOTED-STRING")
                    {
                        Includes.Add(l.TokenContents.Replace("\"", ""));
                    }
                }

                if (LastSymbol == "SKELETON" && l.TokenContents != "TFNM")
                {
                    if ((String)l.Token == "QUOTED-STRING")
                    {
                        String ASkel = l.TokenContents.Replace("\"", "");
                    }
                }


                if (LastSymbol == "ANIMSET" && l.TokenContents != "TFNM")
                {
                    if ((String)l.Token == "QUOTED-STRING")
                    {
                        String AAnim = l.TokenContents.Replace("\"", "");
                    }
                }

                if (LastSymbol == "ANIMEFFECT" && l.TokenContents != "TFNM")
                {
                    if ((String)l.Token == "QUOTED-STRING")
                    {
                        String AfxFile = l.TokenContents.Replace("\"", "");
                    }
                }

                if (LastSymbol == "ALLFRAMESBBOX")
                {
                    if ((String)l.Token == "QUOTED-STRING")
                    {
                        String BBox = l.TokenContents.Replace("\"", "");
                    }
                }

                if (LastSymbol == "MESH" && l.TokenContents != "TFNM")
                {
                    if ((String)l.Token == "QUOTED-STRING" && l.TokenContents != "}" && l.TokenContents != "{")
                    {
                        tSmcModel nMesh = new tSmcModel();
                        nMesh.LCTexs = new Dictionary<string, tSmcLCTexs>();

                        nMesh.FileName = l.TokenContents.Replace("\"", "");

                        Model.Add(nMesh);

                        texIndex = Model.Count - 1;
                    }
                }

                if (LastSymbol == "#INCLUDE" && l.TokenContents != "TFNM")
                {
                    if ((String)l.Token == "QUOTED-STRING")
                    {
                        ReadInc(IllTechLibrary.Settings.Preferences.GetString("CLIENT", "root") + l.TokenContents.Replace("\"", ""));
                    }
                }

                if (LastSymbol == "TEXTURES" && l.TokenContents != "TFNM")
                {
                    tSmcLCTexs nTex = new tSmcLCTexs();

                    if ((String)l.Token == "QUOTED-STRING" && l.TokenContents != "}" && l.TokenContents != "{")
                    {
                        nTex.Name = l.TokenContents.Replace("\"", "");

                        l.Next();

                        while ((String)l.Token != "QUOTED-STRING" && l.Next())
                        {
                        }

                        if ((String)l.Token == "QUOTED-STRING" && l.TokenContents != "}" && l.TokenContents != "{")
                        {
                            nTex.File = l.TokenContents.Replace("\"", "");

                            Model[texIndex].LCTexs.Add(Model[texIndex].FileName, nTex);
                        }
                    }
                }
            }
        }
    }
}
