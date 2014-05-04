using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hajk_std.Text
{
    public static class Convert
    {
        public static string CzechLiteral(string text)
        {
            Dictionary<string, string> replacements = new Dictionary<string, string>()
            {
                {"á","a"},
                {"č", "c"},
                {"c’", "c"},
                {"ď", "d"},
                {"é", "e"},
                {"ě", "e"},
                {"í", "i"},
                {"ň", "n"},
                {"ó", "o"},
                {"ö", "o"},
                {"ř", "r"},
                {"š", "s"},
                {"ť", "t"},
                {"t’", "t"},
                {"ú", "u"},
                {"ů", "u"},
                {"ý", "y"},
                {"ž", "z"},
            };

            foreach (KeyValuePair<string, string> replacement in replacements)
	        {
                // Convert Lower case
                string findLower = replacement.Key;
                string replaceLower = replacement.Value;
                text = text.Replace(findLower, replaceLower);
                
                // Convert Upper case
                string findUpper = replacement.Key.ToUpper();
                string replaceUpper = replacement.Value.ToUpper();
                text = text.Replace(findUpper, replaceUpper); 
	        }

            return text;
            
        }
    }
}