namespace Hajk_std.Web
{
    using System;
    using System.Net;
    using System.IO;
    using System.Text.RegularExpressions;

    public class HttpFetch
    {
        public static string GetDirectoryListingRegexForUrl(string url)
        {
            if (url.Equals("http://localhost/ebook/"))
            {
                return "<a href=\".*\">(?<name>.*)</a>";
            }
            throw new NotSupportedException();
        }
        public static void FetchWebToFolder(string rootUri, string FolderToSaveWeb)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(rootUri);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string html = reader.ReadToEnd();
                    Regex regex = new Regex(GetDirectoryListingRegexForUrl(rootUri));
                    MatchCollection matches = regex.Matches(html);
                    if (matches.Count > 0)
                    {
                        foreach (Match match in matches)
                        {
                            if (match.Success)
                            {
                                Console.WriteLine(match.Groups["name"]);
                            }
                        }
                    }
                }
            }

            Console.ReadLine();
        }
    }
}