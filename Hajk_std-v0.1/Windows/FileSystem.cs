using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hajk_std.Windows
{
    /// <summary>
    /// Filesystem
    /// </summary>
    public static class FileSystem
    {

        //Metody
        /// <summary>
        /// Copy directory structure recursively
        /// </summary>
        /// <param name="Src"></param>
        /// <param name="Dst"></param>
        public static bool CopyDirectory(string Src, string Dst)
        {
            //pokud je cílová složka stejná jako zdrojová, není třeba kopírovat
            const string testingFolder = "test";
            if (Path.Combine(Src, testingFolder) == Path.Combine(Dst, testingFolder)) { return false; }

            String[] Files;

            if (Dst[Dst.Length - 1] != Path.DirectorySeparatorChar)
                Dst += Path.DirectorySeparatorChar;
            if (!Directory.Exists(Dst)) Directory.CreateDirectory(Dst);
            Files = Directory.GetFileSystemEntries(Src);
            foreach (string Element in Files)
            {
                // Sub directories

                if (Directory.Exists(Element))
                    CopyDirectory(Element, Dst + Path.GetFileName(Element));
                // Files in directory

                else
                    System.IO.File.Copy(Element, Dst + Path.GetFileName(Element), true);
            }

            return true;
        }
        public static void MoveDirectory(string Src, string Dst)
        {
            bool result = CopyDirectory(Src, Dst);
            if (result)
            {
                Directory.Delete(Src, true);
            }
        }
        public static void RenameDirectory(string Src, string Dst)
        {
            // vyčištění případné budoucí složky
            CleanDirectory(Dst, true);

            // Přejmenování přesunem
            MoveDirectory(Src, Dst);

        }
        /// <summary>
        /// Vytvoří složku v appData s nastavením aplikace
        /// </summary>
        /// <param name="autorName">autor software</param>
        /// <param name="applicationName">jméno aplikace</param>
        /// <returns>cesta ke složce s nastavením</returns>
        public static string createUserSettingsDirectory(string autorName, string applicationName)
        {
            string metodResult = string.Empty;

            string appData = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string autorDirectoryPath = Path.Combine(appData, autorName);
            string settingsDirectoryPath = Path.Combine(autorDirectoryPath, applicationName);

            try
            {
                if (!Directory.Exists(autorDirectoryPath))     //pokud složka neexistuje
                {
                    //vytvoření adresáře autora
                    DirectoryInfo createautorDirectoryFolderResult = Directory.CreateDirectory(autorDirectoryPath);
                    if (createautorDirectoryFolderResult.Exists)
                    {

                        if (!Directory.Exists(autorDirectoryPath))    //pokud složka neexistuje
                        {
                            //vytvoření adresáře aplikace
                            DirectoryInfo createSettingsDirectoryFolderResult = Directory.CreateDirectory(settingsDirectoryPath);

                        }

                    }
                }

                metodResult = settingsDirectoryPath;
            }
            catch
            {
                metodResult = "chyba";
            }

            return metodResult;
        }
        /// <summary>
        /// Smaže soubor z disku
        /// </summary>
        /// <param name="filePath">cesta k souboru</param>
        public static void RemoveFile(string filePath)
        {
            try
            {
                System.IO.File.Delete(filePath);
            }
            catch (Exception e)
            {
                Debug.WriteLine(string.Format("Soubor s umístěním {0} nelze smazat. Chybová hláška:\n{1}", filePath, e.Message));
            }
        }
        /// <summary>
        /// přejmenuje soubor
        /// </summary>
        /// <param name="oldFilePath">cesta ke starému souboru</param>
        /// <param name="newFilePath">cesta k novému souboru</param>
        public static void RenameFile(string oldFilePath, string newFilePath)
        {
            try
            {
                System.IO.File.Copy(oldFilePath, newFilePath);
            }
            catch (Exception e)
            {
                Debug.Write("Chyba v metodě RenameFile třídy FileSystem");
            }

            System.IO.File.Delete(oldFilePath);

        }
        public static void ActionOnEachFileInFolder(string folderPath, Action<FileInfo, Dictionary<string, string>> Action, Dictionary<string, string> properties, bool vcetnePodslozek)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
            FileInfo[] files = directoryInfo.GetFiles();
            foreach (FileInfo file in files)
            {
                ActionOnFile(file, Action, properties);
            }

            if (vcetnePodslozek)
            {
                DirectoryInfo[] directories = directoryInfo.GetDirectories();
                foreach (DirectoryInfo directory in directories)
                {
                    ActionOnEachFileInFolder(directory.FullName, Action, properties, vcetnePodslozek);
                }
            }
        }
        public static void ActionOnFile(FileInfo file, Action<FileInfo, Dictionary<string, string>> Action, Dictionary<string, string> properties)
        {
            Action(file, properties);
        }
        public static void ActionOnFile(string filePath, Action<FileInfo, Dictionary<string, string>> Action, Dictionary<string, string> properties)
        {
            FileInfo file = new FileInfo(filePath);
            Action(file, properties);
        }
        public static string ReadFile(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return string.Empty;
        }
        public static void WriteFile(string content, string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write(content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public static void CleanDirectory(string directoryPath, bool recursive)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
            FileInfo[] files = directoryInfo.GetFiles();

            foreach (FileInfo file in files)
            {
                file.Delete();
            }

            if (recursive)
            {
                DirectoryInfo[] directories = directoryInfo.GetDirectories();
                foreach (DirectoryInfo directory in directories)
                {
                    CleanDirectory(directory.FullName, recursive);
                }
            }

            directoryInfo.Delete(true);
        }
        public static void ExplorerOpenFolder(string path)
        {
            Process.Start(path); 
        }
        public static bool FileExist(string uri)
        {
            FileInfo fileInfo = new FileInfo(uri);

            if (fileInfo.Exists)
            {
                return true;
            }

            // pouze pokud předchozí podmínka nevrátí true
            return false;
        }
        
        //Soukrome metody
    }

}      







