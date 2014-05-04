using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Hstd.Conversion
{
    public class Encoding
    {
        private static void RemoveBOM_Action(FileInfo file, Dictionary<string, string> properties)
        {
            // Properties se nepoužijí
            properties = null;
            string[] extensions = new string[] 
            {
                ".txt",
                ".xml",
                ".html",
                ".xhtml",
                ".css",
                ".htm",
                ".shtml",
                ".php",
                ".tpl",
                ".js",
                ".aspx",
                ".opf",
                ".ncx",
            };

            // Ochrana pro přepsování pouze textových souborů
            foreach (string ex in extensions)
            {
                if (file.Extension == ex)
                {
                    string text;

                    using (StreamReader reader = file.OpenText())
                    {
                        text = reader.ReadToEnd();
                    }

                    // Odstranění BOM
                    text.Trim().Trim(new char[] { '\uFEFF', '\u200B' });

                    // Uložení zpět
                    System.Text.UTF8Encoding utf8WithoutBom = new System.Text.UTF8Encoding(false);
                    using (StreamWriter writer = new StreamWriter(file.FullName, false, utf8WithoutBom))
                    {
                        writer.Write(text);
                    }
                }
                else
                {
                    string jaja = "";
                }
            }

            

        }
    }
}