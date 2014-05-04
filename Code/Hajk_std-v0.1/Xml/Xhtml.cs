using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hajk_std.Xml
{
    public static class Xhtml
    {
        #region Datové Složky
        //=============
        //Datove slozky
        //=============


        #endregion

        #region Objekty
        //=============
        //Objekty
        //=============

        #endregion

        #region Konstruktory
        //=============
        //Konstruktory
        //=============

        #endregion

        #region Metody
        //=============
        //Metody
        //=============
        public static string EntityXhtmlToXml(string xhtml)
        {
            string xml = xhtml;
            string[][] tableOfEntities = new string[][]
            {
                new string[] {"&nbsp;"," "},
            };
            
            for (int i = 0; i < tableOfEntities.Length; i++)
			{   

                xml = xml.Replace(tableOfEntities[i][0], tableOfEntities[i][1]);
			}

            return xml;
        }

        #endregion

        #region Soukromé metody
        //=============
        //Soukrome metody
        //=============

        #endregion
				
    }
}
