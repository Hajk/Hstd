using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace Hajk_std.Xml
{
    public static class Xml
    {
        public static string FormatXml2(XmlDocument doc)
        {

            XmlTextWriter xmlWriter;
            StringWriter textWriter;

            // Format the Xml document with indentation and save it to a string.
            textWriter = new StringWriter();
            xmlWriter = new XmlTextWriter(textWriter);
            xmlWriter.Formatting = Formatting.Indented;
            doc.Save(xmlWriter);

            return textWriter.ToString();

        }

        public static string FormatXml(XmlDocument doc)
        {

            string rtn = string.Empty;
            using (StringWriter textWriter = new StringWriter())
            {
                using (XmlTextWriter xmlWriter = new XmlTextWriter(textWriter))
                {
                    xmlWriter.Formatting = Formatting.Indented;
                    doc.Save(xmlWriter);
                }
                rtn = textWriter.ToString();
            }
            return rtn;
        }

        public static string IndentXMLString(String xml)
        {
            XmlDocument xmldoc = new XmlDocument();

            xmldoc.LoadXml(xml);

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            settings.OmitXmlDeclaration = true;
            settings.NewLineOnAttributes = false;


            StringBuilder output = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(output, settings);
            xmldoc.WriteContentTo(writer);
            writer.Flush();
            return output.ToString();
        }
    }
}
