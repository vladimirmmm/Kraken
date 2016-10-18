using LogicalModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.Models
{
    public class FactPage2
    {
        public string Key = "";
        public bool IsDirty = false;
        //public FactDictionary Facts = new FactDictionary(new Utilities.IntArrayEqualityComparer());
        public FactDictionary2 Facts = new FactDictionary2();
        public FactPage2()
        {

        }
        public FactPage2(string key)
        {
            this.Key = key;
        }

    }

    public class FactPage 
    {
        public string Key = "";
        public bool IsDirty = false;
        //public FactDictionary Facts = new FactDictionary(new Utilities.IntArrayEqualityComparer());
        public FactDictionary Facts = new FactDictionary(new Utilities.IntArrayEqualityComparer());
        public FactPage() 
        {

        }
        public FactPage(string key)
        {
            this.Key = key;
        }
      
    }
    public class FactKeyManager2
    {
        public int maxdictnr = 10;
        public bool SaveToFile = true;
        public string DataFolder = "";
        public FactDictionary2 LastPage = null;
        public string LastPageKey = "";
        public FactDictionaryCollection FactsOfPages = new FactDictionaryCollection();
        public List<Utilities.KeyValue<string, FactPage2>> LoadedFactsOfPages = new List<Utilities.KeyValue<string, FactPage2>>();
        public HashSet<string> ExistingFiles = new HashSet<string>();
        public Action<FactDictionary2> ManageLoadedFacts = (f) => { };
        public Func<int[], int> GetFactIndex = (key) => -1;
        public Func<int> GetNewFactIndex = () => -1;
        public Func<int, int[]> GetFactKey = (index) => new int[] { -1 };
        public string GetPagePath(string pagekey)
        {
            return DataFolder + pagekey + ".dat";
        }

        public FactKeyManager2() 
        {
            GetFactIndex = FactsOfPages.Index;
            GetFactKey = FactsOfPages.Key;
        }

        public bool FactKeyExists(int[] factkey)
        {
            return FactsOfPages.ContainsKey(factkey);
        }
        public Utilities.KeyValue<int, List<int>> GetFact(int[] factkey)
        {
            return FactsOfPages.GetKvp(factkey);
        }
        public void EnsureFact(int[] factkey)
        {
            FactsOfPages.Save(factkey);
        }



        public void Load()
        {
            FactsOfPages.Clear();
            FactsOfPages.LoadPages();
        }

        public void SaveAll()
        {
             FactsOfPages.SavePages();
        }
        public IEnumerable<int> IndexesAsEnumerable()
        {
            return FactsOfPages.FactIndexes;
        }
        public IEnumerable<int[]> KeysAsEnumerable()
        {
            return FactsOfPages.FactKeysAsEnumerable;
        }
        public IEnumerable<Utilities.KeyValue<int[],List<int>>> FactsAsEnumerable()
        {
            return FactsOfPages.FactsAsEnumerable;
        }




        public void Clear()
        {
            this.ExistingFiles.Clear();
            this.FactsOfPages.Clear();
            this.LoadedFactsOfPages.Clear();
        }

        public int Count 
        {
            get { return FactsOfPages.Count; }
        }

    }


    public class FactKeyManager
    {
        public int maxdictnr = 10;
        public bool SaveToFile = true;
        public string DataFolder = "";
        public FactDictionary LastPage = null;
        public string LastPageKey="";
        public Dictionary<string, FactPage> FactsOfPages = new Dictionary<string, FactPage>();
        public List<Utilities.KeyValue<string, FactPage>> LoadedFactsOfPages = new List<Utilities.KeyValue<string, FactPage>>();
        public HashSet<string> ExistingFiles = new HashSet<string>();
        public Action<FactDictionary> ManageLoadedFacts = (f) => { };

        public string GetPagePath(string pagekey) 
        {
            return DataFolder + pagekey+".dat";
        }

        public bool FactKeyExists(int[] factkey)
        {
            var pagekey = GetPageKey(factkey);
            if (!PageExists(pagekey))
            {
                return false;
            }
            var page = GetPage(pagekey);
            return page.Facts.ContainsKey(factkey);
        }
        public Utilities.KeyValue<int[], List<int>> GetFact(int[] factkey) 
        {
            var pagekey = GetPageKey(factkey);
            var result = new Utilities.KeyValue<int[], List<int>>();
            if (!PageExists(pagekey))
            {
                //return new KeyValuePair<int[], List<string>>();
            }
            else 
            {
                var page = GetPage(pagekey);
                if (page.Facts.ContainsKey(factkey)) 
                {
                    result.Key = factkey;
                    result.Value = page.Facts[factkey];
                }
            }
            return result;
        }
        public void EnsureFact(int[] factkey) 
        {
            var pagekey = GetPageKey(factkey);
            if (!FactKeyExists(factkey)) 
            {
                var page = GetPage(pagekey);
                page.Facts.AddItem(factkey, new List<int>());
            }
        }

        public string GetPageKey(int[] factkey) 
        {
            //var limit = 1;
            //var ix = factkey.Length > limit ? limit : factkey.Length;
            //var pb = new StringBuilder();
            //for (int i = 0; i < ix; i++) 
            //{
            //    pb.Append(factkey[i]);
            //    pb.Append("-");
            //}
            return factkey[0].ToString();
        }

        public bool PageExists(string pagekey)
        {
            var path = GetPagePath(pagekey);
            if (ExistingFiles.Contains(path)) { return true; }
            if (Utilities.FS.FileExists(path))
            {
                if (!ExistingFiles.Contains(path))
                {
                    ExistingFiles.Add(path);
                }
                return true;
            }
            return false;
        }

   

        public FactPage GetPage(string pagekey) 
        {
            if (!FactsOfPages.ContainsKey(pagekey)) 
            {
                FactsOfPages.Add(pagekey, null);
            }
            var page = FactsOfPages[pagekey];
            if (page == null) 
            {
                if (!PageExists(pagekey)) 
                {
                    CreatePage(pagekey);
                }
                FactsOfPages[pagekey] = LoadPage(pagekey);
            }
            return FactsOfPages[pagekey];
        }
        
        public void CreatePage(string pagekey)
        {
            var path = GetPagePath(pagekey);
            Utilities.FS.WriteAllText(path, "");
            if (!ExistingFiles.Contains(path)) 
            {
                ExistingFiles.Add(path);
            }
        }

        public void SavePage(string pagekey) 
        {
            var page = GetPage(pagekey);
            var sb = new StringBuilder();
            foreach (var item in page.Facts) 
            {
                sb.AppendLine(GetFactDictionaryItemString(item));
            }
            if (SaveToFile)
            {
                Utilities.FS.WriteAllText(GetPagePath(pagekey), sb.ToString());
            }
            sb.Clear();
        }
        
        public FactPage LoadPage(string pagekey)
        {
            Console.WriteLine("Loading " + pagekey);

            var pagecontent = Utilities.FS.ReadAllText(GetPagePath(pagekey));
            var page = new FactPage(pagekey);
            var factitems = pagecontent.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var factitem in factitems)
            {
                var kvp = GetFactDictionaryItem(factitem);
                page.Facts.AddKvp(kvp);
            }
            ManageLoadedFacts(page.Facts);
            if (maxdictnr < LoadedFactsOfPages.Count - 1)
            {
                var pagekeytounload = LoadedFactsOfPages.FirstOrDefault().Key;
                UnloadPage(pagekeytounload);
            }
            LoadedFactsOfPages.Add(new Utilities.KeyValue<string,FactPage>( pagekey, page));
            return page;
        }

        public void UnloadPage(string pagekey) 
        {
            Console.WriteLine("Unloading " + pagekey);
            var page = GetPage(pagekey);
            SavePage(pagekey);
            LoadedFactsOfPages.Remove(LoadedFactsOfPages.FirstOrDefault(i=>i.Key==pagekey));
            
            FactsOfPages[pagekey] = null;

            //FactsOfPages[pagekey].Facts.Unload();
        }

        public void Unload(int percent) 
        {
            var nr = (percent *  LoadedFactsOfPages.Count)/100 ;
            for (int i = 0; i < nr; i++) 
            {
                UnloadPage(LoadedFactsOfPages.FirstOrDefault().Key);
            }
        }
        public KeyValuePair<int[],List<int>> GetFactDictionaryItem(string factkeystring) 
        {
         
            var sp_pipe =  Literals.PipeSeparator[0];
            var sp_coma = Literals.Coma[0];
            //var parts = factkeystring.Split(sp_pipe, StringSplitOptions.None);
            //var keys = parts[0].Split(sp_coma, StringSplitOptions.RemoveEmptyEntries);
            var s_ix = 0;
            List<string> keyparts = new List<string>(20);
            List<string> values = new List<string>(20);
            List<string> container= keyparts;
            var diff=0;
            var val="";
            for (int i = 0; i < factkeystring.Length; i++) 
            {
                if (factkeystring[i] == sp_coma) 
                {
                    diff= i - s_ix;
                    val = factkeystring.Substring(s_ix, diff).Trim();
                    if (!String.IsNullOrEmpty(val))
                    {
                        container.Add(val);
                    }
                    s_ix = i + 1;
                }
                if (factkeystring[i] == sp_pipe)
                {
                    container = values;
                }
            }
            //var parts = Utilities.Strings.FactSplit(factkeystring, Literals.PipeSeparator[0], 4);


            //var keyparts = Utilities.Strings.FactSplit(parts[0], Literals.Coma[0], 1);
            var intkeys = new int[keyparts.Count];

            for (int i = 0; i < keyparts.Count; i++)
            {
                intkeys[i] = Utilities.Converters.FastParse(keyparts[i]);
            }

            //var values = parts[1].Split(sp_coma, StringSplitOptions.RemoveEmptyEntries).Select(i => Utilities.Converters.FastParse(i));
            //var values = Utilities.Strings.FactSplit(parts[1],Literals.Coma[0], 1).Select(i => Utilities.Converters.FastParse(i));
            //var cells = new List<int>(values.Select(i => Utilities.Converters.FastParse(i)));
            var cells = new List<int>(values.Count);
            cells.AddRange(values.Select(i => Utilities.Converters.FastParse(i)));
            var kvp = new KeyValuePair<int[], List<int>>(intkeys,cells);
            return kvp;
        }
        public string GetFactDictionaryItemString(KeyValuePair<int[], List<int>> kvp) 
        {
            var sb = new StringBuilder();
            foreach (var keypart in kvp.Key) 
            {
                sb.Append(keypart);
                sb.Append(Literals.Coma);
            }
            sb.Append(Literals.PipeSeparator);
            foreach (var cell in kvp.Value) 
            {
                sb.Append(cell);
                sb.Append(Literals.Coma);
            }
            return sb.ToString();
        }

        public void Load() 
        {
            Clear();
            var files = System.IO.Directory.GetFiles(DataFolder, "*.dat");
            foreach (var file in files) 
            {
                var fn = Utilities.Strings.GetFileNameWithoutExtension(file);
                FactsOfPages.Add(fn, null);
                if (!ExistingFiles.Contains(file))
                {
                    ExistingFiles.Add(file);
                }
            }
        }

        public void SaveAll()
        {
            foreach (var pagekey in LoadedFactsOfPages) 
            {
                SavePage(pagekey.Key);
            }
        }

        public IEnumerable<KeyValuePair<int[],List<int>>> AsEnumerable()
        {
            var keylist = FactsOfPages.Keys.ToList();
            foreach (string pagekey in keylist)
            {
                var page = GetPage(pagekey);
                var items = page.Facts.ToList();
                foreach (var item in items) 
                {
                    yield return item;
                }
                //yield return item + "roxxors";
            }
        }




        internal void Clear()
        {
            this.ExistingFiles.Clear();
            this.FactsOfPages.Clear();
            this.LoadedFactsOfPages.Clear();
        }
    }

    public class testx 
    {
        public void test()
        {
            var m = new FactKeyManager();

            var facts = new List<int[]>
            {
                new int[]{ 1 },
                new int[]{ 1, 2 },
                new int[]{ 1, 2, 3, 4 },
                new int[]{ 1, 200, 3, 4 },
                new int[]{ 1, 200, 3, 5 },
                new int[]{ 1, 200, 14, 60 },
                new int[]{ 1000, 3000, 14, 60 },
                new int[]{ 1000, 3000, 700, 60 },
                new int[]{ 500, 3000, 700, 61 },
                new int[]{ 500, 3000, 700, 61,15454 },
                new int[]{ 500, 3000, 700, 61,15454,15 },
                new int[]{ 500, 250, 700, 61, 15 },
                new int[]{ 600, 250, 700, 61, 15 },

           
            };
            m.maxdictnr = 5;
            m.DataFolder = @"C:\My\Developement\test\";

            var ix = 0;
            foreach (var fact in facts)
            {
                var f = m.GetFact(fact);
                if (f.Key == null)
                {
                    m.EnsureFact(fact);
                }
                f = m.GetFact(fact);
                f.Value.Add( ix);
                f.Value.Add( ix);
                ix++;
            }
            m.SaveAll();

            var x = m.GetPageKey(new int[] { 1, 2, 3, 4, 5 });
            var y = m.GetPageKey(new int[] { 1, 2, 3, 4 });
            var z = m.GetPageKey(new int[] { 1 });
        }
    }
}
