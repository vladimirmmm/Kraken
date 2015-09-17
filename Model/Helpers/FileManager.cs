using BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace LogicalModel.Helpers
{
    public class FileManager
    {
        protected Taxonomy Taxonomy = null;
        public static Dictionary<string, bool> DownloadedFiles = new Dictionary<string, bool>();
        public static object DownloadedFileslocker = new Object();

        public static void Clear()
        {
            lock(DownloadedFileslocker)
            {
                DownloadedFiles.Clear();
            }
        }
        public static void AddDocument(string localpath) 
        {
            if (!DownloadedFiles.ContainsKey(localpath))
            {
                lock (DownloadedFileslocker)
                {
                    if (!DownloadedFiles.ContainsKey(localpath))
                    {
                        DownloadedFiles.Add(localpath, true);
                    }
                }
            }
        }
        public void SetTaxonomy(Taxonomy Taxonomy) 
        {
            this.Taxonomy = Taxonomy;
        }
        public string GetFile(Document parentdocument, string filepath, bool downloadifexists = false) 
        {
            var sourcepath = filepath;
            var markuppath = filepath;
            if (parentdocument != null)
            {
                if (Utilities.Strings.IsRelativePath(markuppath))
                {
                    sourcepath = Utilities.Strings.ResolveRelativePath(parentdocument.SourceFolder, markuppath);
                }
            }

            var localpath = Utilities.Strings.GetLocalPath(TaxonomyEngine.LocalFolder, sourcepath);

            //SetLocalPath(localpath);
            //SetSourcePath(sourcepath);
            try
            {
                if (!DownloadedFiles.ContainsKey(localpath))
                {
                    Utilities.Strings.CopyToLocal(sourcepath, localpath, downloadifexists);
                }
                return localpath; 
             

            }
            catch (Exception ex)
            {
                Logger.WriteLine(String.Format("Can't get source file {0}. Error: {1}", sourcepath, ex));
        
            }
            return "";
        }
        public void GetFiles(string[] files, bool downloadifexists = false)
        {

        }
    }
}
