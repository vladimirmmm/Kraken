﻿using BaseModel;
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

        public static void SaveToJson<T>(IEnumerable<T> items, string pathformat, int itemsperpart=20) 
        {
            GC.Collect();

            int pageCount = (items.Count() + itemsperpart - 1) / itemsperpart;
            for (int i = 0; i < pageCount; i++) 
            {
                var startix = i * itemsperpart;
                var rules = items.Skip(startix).Take(itemsperpart).ToList();
                var json = Utilities.Converters.ToJson(rules);
                var filepath = String.Format(pathformat, i);
                Utilities.FS.WriteAllText(filepath, json);
                json = null;
            }
        }

        public static void SetFromJson<T>(ICollection<T> target, string pathformat) 
        {
            var folder = Utilities.Strings.GetFolder(pathformat);
            var filename = Utilities.Strings.GetFileName(pathformat);
            var searchpattern = filename.Replace("{0}", "*");
            var files = System.IO.Directory.GetFiles(folder, searchpattern);
            target.Clear();
            foreach (var file in files) 
            {

                var items = new List<T>();
                var json = System.IO.File.ReadAllText(file);
                items = Utilities.Converters.JsonTo<List<T>>(json);
                foreach (var item in items) 
                {
                    if (item is KeyValuePair<int,List<string>>)
                    {
                        var kvp = (KeyValuePair<int, List<string>>)((Object)item) ;
                        kvp.Value.Capacity = kvp.Value.Count;
                    }
                    target.Add(item);
                }
            }
        }

        public static void SetFromJson<T>(ICollection<T> target, string pathformat, Func<String, ICollection<T>> Deserialize)
        {
            var folder = Utilities.Strings.GetFolder(pathformat);
            var filename = Utilities.Strings.GetFileName(pathformat);
            var searchpattern = filename.Replace("{0}", "*");
            var files = System.IO.Directory.GetFiles(folder, searchpattern);
            //target.Clear();
            foreach (var file in files)
            {

                ICollection<T> items = null;
                var json = System.IO.File.ReadAllText(file);
                items = Deserialize(json);
                //items = Utilities.Converters.JsonTo<List<T>>(json);
                json = null;
                //foreach (var item in items)
                //{

                //    //BeforeAdd(item);
                //    target.Add(item);
                //}
            }
        }

    }
}
