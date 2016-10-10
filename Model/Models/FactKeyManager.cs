using LogicalModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.Models
{
    public class FactKeyManager
    {
        public int maxdictnr = 10;
        public string DataFolder = "";
        public FactDictionary LastPage = null;
        public string LastPageKey="";
        //public Dictionary<string, List<int[]>> FactsOfPages = new Dictionary<string, List<int[]>>();
        public Dictionary<string, FactDictionary> FactsOfPages = new Dictionary<string, FactDictionary>();
        public Dictionary<string, FactDictionary> LoadedFactsOfPages = new Dictionary<string, FactDictionary>();
        
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
            return page.ContainsKey(factkey);
        }
        public Utilities.KeyValue<int[], List<string>> GetFact(int[] factkey) 
        {
            var pagekey = GetPageKey(factkey);
            var result = new Utilities.KeyValue<int[], List<string>>();
            if (!PageExists(pagekey))
            {
                //return new KeyValuePair<int[], List<string>>();
            }
            else 
            {
                var page = GetPage(pagekey);
                if (page.ContainsKey(factkey)) 
                {
                    result.Key = factkey;
                    result.Value = page[factkey];
                }
            }
            return result;
        }
        public void EnsureFact(int[] factkey) 
        {
            var pagekey = GetPageKey(factkey);
            if (!FactKeyExists(factkey)) 
            {
                var dict = GetPage(pagekey);
                dict.Add(factkey, new List<string>());
            }
        }

        public string GetPageKey(int[] factkey) 
        {
            var ix = factkey.Length / 2 + 1;
            var pb = new StringBuilder();
            for (int i = 0; i < ix; i++) 
            {
                pb.Append(factkey[i]);
                pb.Append("-");
            }
            return pb.ToString();
        }

        public bool PageExists(string pagekey)
        {

            if (Utilities.FS.FileExists(GetPagePath(pagekey)))
            {
                return true;
            }
            return false;
        }

   

        public FactDictionary GetPage(string pagekey) 
        {
            if (!FactsOfPages.ContainsKey(pagekey)) 
            {
                FactsOfPages.Add(pagekey, null);
            }
            var dict = FactsOfPages[pagekey];
            if (dict == null) 
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
            Utilities.FS.WriteAllText(GetPagePath(pagekey), "");
        }

        public void SavePage(string pagekey) 
        {
            var dict = GetPage(pagekey);
            var sb = new StringBuilder();
            foreach (var item in dict) 
            {
                sb.AppendLine(GetFactDictionaryItemString(item));
            }
            Utilities.FS.WriteAllText(GetPagePath(pagekey), sb.ToString());
            sb.Clear();
        }
        public FactDictionary LoadPage(string pagekey)
        {
            Console.WriteLine("Loading " + pagekey);

            var page = Utilities.FS.ReadAllText(GetPagePath(pagekey));
            var dict = new FactDictionary();
            var factitems = page.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var factitem in factitems)
            {
                var kvp = GetFactDictionaryItem(factitem);
                dict.AddKvp(kvp);
            }
            if (maxdictnr < LoadedFactsOfPages.Count - 1)
            {
                var pagekeytounload = LoadedFactsOfPages.FirstOrDefault().Key;
                UnloadPage(pagekeytounload);
            }
            LoadedFactsOfPages.Add(pagekey, dict);
            return dict;
        }

        public void UnloadPage(string pagekey) 
        {
            Console.WriteLine("Unloading " + pagekey);
            var page = GetPage(pagekey);
            SavePage(pagekey);
            LoadedFactsOfPages.Remove(pagekey);
            FactsOfPages[pagekey] = null;

        }
        public KeyValuePair<int[],List<string>> GetFactDictionaryItem(string factkeystring) 
        {
         
            var sp_pipe = new string[] { Literals.PipeSeparator };
            var sp_coma = new string[] { Literals.Coma };
            var parts = factkeystring.Split(sp_pipe, StringSplitOptions.None);
            var keys = parts[0].Split(sp_coma, StringSplitOptions.RemoveEmptyEntries);
            var intkeys = new int[keys.Length];

            for (int i = 0; i < keys.Length; i++)
            {
                intkeys[i] = Utilities.Converters.FastParse(keys[i]);
            }

            var values = parts[1].Split(sp_coma, StringSplitOptions.RemoveEmptyEntries);
            var cells = new List<string>(values);
            var kvp = new KeyValuePair<int[], List<string>>(intkeys,cells);
            return kvp;
        }
        public string GetFactDictionaryItemString(KeyValuePair<int[], List<string>> kvp) 
        {
            var sb = new StringBuilder();
            foreach (var keypart in kvp.Key) 
            {
                sb.Append(keypart);
                sb.Append(",");
            }
            sb.Append(" | ");
            foreach (var cell in kvp.Value) 
            {
                sb.Append(cell);
                sb.Append(",");
            }
            return sb.ToString();
        }

        public void Load() 
        {
            var files = System.IO.Directory.GetFiles(DataFolder, "*.dat");
            foreach (var file in files) 
            {
                var fn = Utilities.Strings.GetFileName(file);
                FactsOfPages.Add(fn, null);
            }
        }

        public void SaveAll()
        {
            foreach (var pagekey in LoadedFactsOfPages.Keys) 
            {
                SavePage(pagekey);
            }
        }

        public IEnumerable<KeyValuePair<int[],List<string>>> AsEnumerable()
        {
            foreach (string pagekey in FactsOfPages.Keys)
            {
                var page = GetPage(pagekey);
                foreach (var item in page) 
                {
                    yield return item;
                }
                //yield return item + "roxxors";
            }
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
                f.Value.Add("retek " + ix.ToString());
                f.Value.Add("repa " + ix.ToString());
                ix++;
            }
            m.SaveAll();

            var x = m.GetPageKey(new int[] { 1, 2, 3, 4, 5 });
            var y = m.GetPageKey(new int[] { 1, 2, 3, 4 });
            var z = m.GetPageKey(new int[] { 1 });
        }
    }
}
