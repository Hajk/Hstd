using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Hajk_std.Windows
{
    public class MakeFolder
    {
        #region Datové Složky
        //=============
        //Datove slozky
        //=============
        internal string initialFolder { get; private set; }
        internal bool isInitialized { get; private set; }

        #endregion

        #region Objekty
        //=============
        //Objekty
        //=============
        private List<List<string>> recent = new List<List<string>>(10);
        private List<int> recentLevel = new List<int>();


        #endregion

        #region Konstruktory
        //=============
        //Konstruktory
        //=============
        public MakeFolder(string initialFolder)
        {
            this.isInitialized = true;
            this.initialFolder = initialFolder;
        }

        #endregion

        #region Metody
        //=============
        //Metody
        //=============

        public void RunInstructions(string makeInstructions)
        {
            using ( StringReader reader = new StringReader(makeInstructions) )
            {
                string line;
                while ( ( line = reader.ReadLine() ) != null )
                {
                    ParseInstruction(line);
                }
            }
        }

        #endregion

        #region Soukromé metody
        //=============
        //Soukrome metody
        //=============

        private void ParseInstruction(string InstructionsLine)
        {
            if ( InstructionsLine.Trim() == "" )
            {
                return;
            }

            string line = InstructionsLine.Trim();
            int indesOfSpace = line.IndexOf(' ');
            string instruct = line.Substring( 0, indesOfSpace );
            string parameters = line.Substring(indesOfSpace + 1);

            string[] instructionsLogic = new string[2] { instruct, parameters };

            if ( InstructionsLine.Trim().Substring(0, 2) == "//" )  
            {
                //pokud bude na začátku řádku // tak se jedná o poznámku a může se přeskočit
                return;
            }

            switch ( instructionsLogic[0] )
            {
                case ">>>>>>":
                    CreateFolder(instructionsLogic[1], 5);
                    break;

                case ">>>>>":
                    CreateFolder(instructionsLogic[1], 4);
                    break;

                case ">>>>":
                    CreateFolder(instructionsLogic[1], 3);
                    break;

                case ">>>":
                    CreateFolder(instructionsLogic[1], 2);
                    break;

                case ">>":
                    CreateFolder(instructionsLogic[1], 1);
                    break;

                case ">":
                    CreateFolder(instructionsLogic[1], 0);
                    break;  

                case "init":
                    this.initialFolder = instructionsLogic[1];
                    this.isInitialized = true;
                    break;

                default:
                    break;
            }
           
        }

        private void CreateFolder(string foldername, int level)
        {
            if ( isInitialized )
            {
                if ( level != 0 )
                {

                    string parrentFolderPath = recent[level - 1].Last<string>();
                    string folderPath = Path.Combine(parrentFolderPath, foldername);
                    Directory.CreateDirectory(folderPath);

                    if ( this.recentLevel.Contains<int>(level) == false )
                    {
                        this.recentLevel.Add(level);
                        this.recent.Add( new List<string>());

                        this.recent[level].Add(folderPath);

                    }
                    else
                    {
                        this.recent[level].Add(folderPath);
                    }

                }
                else
                {
                    string folderpath = Path.Combine(initialFolder, foldername);
                    Directory.CreateDirectory(folderpath);

                    if ( this.recentLevel.Contains<int>(level) == false )
                    {
                        this.recentLevel.Add(level);
                        this.recent.Add( new List<string>() );

                        this.recent[level].Add(folderpath);

                    }
                    else
                    {
                        this.recent[level].Add(folderpath);
                    }
                }
            }
            
        }

        //private void CreateSubFolder(string foldername)
        //{
        //    string folderpath = Path.Combine(initialFolder, foldername);
        //    System.IO.Directory.CreateDirectory(folderpath);
        //    this.recentFolderPath = folderpath;
        //}

        #endregion
				
    }
}
