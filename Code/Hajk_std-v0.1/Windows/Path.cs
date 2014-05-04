using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Hajk_std.Windows
{
    public static class PathFU
    {
        public static string GetOnlyDirectoryName(string directory)
        {
            string name = string.Empty;
            string[] parsedDirectory = new string[50];
            const char separator = '\\';
            parsedDirectory = directory.Split(separator);
            name = parsedDirectory.Last();
            return name;
        }

        /// <summary>
        /// Zjistí a vrácí absolutní cestu souboru z relativní od určeného umístění
        /// </summary>
        /// <param name="relativePath">Relativní cesta k cílovému souboru</param>
        /// <param name="referencePath">Výchozí cesta</param>
        /// <returns>absolutní cesta k cílovému souboru</returns>
        public static string GetAbsolutePath(string relativePath, string referencePath)
        {
            if (referencePath.EndsWith("\\") == false)
            {
                referencePath += "\\";
            }

            return Path.GetFullPath(referencePath + relativePath);
        }
    }
}
