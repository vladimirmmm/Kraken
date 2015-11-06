using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class FS
    {
        public static bool FileExists(string path) 
        {
            if (path.Contains("{0}")) 
            {
                var folder = Utilities.Strings.GetFolder(path);
                if (System.IO.Directory.Exists(folder))
                {
                    var filename = Utilities.Strings.GetFileName(path);
                    var pattern = filename.Replace("{0}", "*");
                    return System.IO.Directory.GetFiles(folder, pattern).Any();
                }
                else 
                {
                    return false;
                }
            }
            return System.IO.File.Exists(path);
        }
        public static void EnsurePath(string path) 
        {
            var folder = Strings.GetFolder(path);
            if (!System.IO.Directory.Exists(folder)) 
            {
                System.IO.Directory.CreateDirectory(folder);
            }
        }

        public static void WriteAllText(string path, string content) 
        {
            EnsurePath(path);
            System.IO.File.WriteAllText(path, content);
        }
        public static void AppendAllText(string path, string content)
        {
            EnsurePath(path);
            System.IO.File.AppendAllText(path, content);
        }

        public static string ReadAllText(string path)
        {
            if (System.IO.File.Exists(path))
            {
                return System.IO.File.ReadAllText(path);
            }
            else 
            {
                return "";
            }

        }

        public static void DeleteFile(string filepath, bool logexception=true)
        {
            if (System.IO.File.Exists(filepath))
            {
                try
                {
                    System.IO.File.Delete(filepath);
                }
                catch (Exception ex) 
                {
                    if (logexception)
                    {
                        Logger.WriteLine(String.Format("Can't delete file {0}! {1}", filepath, ex.Message));
                    }
                }
            }
        }

        public static void DeleteFolder(string folderpath)
        {
            if (System.IO.Directory.Exists(folderpath))
            {
                try
                {
                    System.IO.Directory.Delete(folderpath, true);
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(String.Format("Can't delete folder {0}! {1}", folderpath, ex.Message));
                }
            }
        }

        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs, bool overwrite)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, overwrite);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs, overwrite);
                }
            }
        }

    }
}
