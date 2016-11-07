using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    public struct FactLookupValue2
    {
        //public int Index = -1;
        //public List<int> CellIndexes = new List<int>();

        //public FactLookupValue2() 
        //{

        //}
        //public FactLookupValue2(int index)
        //{
        //    this.Index = index;
        //}
        //public FactLookupValue2(int index, List<int> cellIndexes)
        //{
        //    this.Index = index;
        //    this.CellIndexes = cellIndexes;
        //}
    }
    public class FactLookupValue 
    {
        public int KeyCount = -1;
        private List<int> _CellIndexes = new List<int>(1);
        public List<int> CellIndexes 
        {
            get { return _CellIndexes; }
            set 
            {  
                _CellIndexes=value; 
            }
        }

        public FactLookupValue() 
        {

        }
        public FactLookupValue(int keycount)
        {
            this.KeyCount = keycount;
        }
        public FactLookupValue(int keycount, List<int> cellIndexes)
        {
            this.KeyCount = keycount;
            this.CellIndexes = cellIndexes;
        }

        public string Content()
        {
            return string.Format("{0}|{1}", this.KeyCount, Utilities.Strings.EnumerableToString(this.CellIndexes, ","));
        }

        public static FactLookupValue GetInstanceFromString(string content) 
        {
            var parts = content.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
            var cellindexes = parts[1].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(i => Utilities.Converters.FastParse(i)).ToList();
            var key = Utilities.Converters.FastParse(parts[0]);
                     
            var result = new FactLookupValue(key,cellindexes);
            return result;
        }
    }
    public class FactDictionaryCollection : LogicalModel.Models.IFactDictionary
    {
        public List<FactKeyDictionary> Pages = new List<FactKeyDictionary>();
        public Dictionary<int, int> FactKeyCountOfIndexes = new Dictionary<int, int>();
        public Dictionary<int[], int> HashKeys = new Dictionary<int[], int>(new Utilities.IntArrayEqualityComparer());

        public FactKeyDictionary LastPage = null;
        public List<int> LoadedPages = new List<int>();
        public Func<string> Folder = () => @"C:\Users\vladimir.balacescu\Desktop\f\";
        public static string Tag = "Facts_";
        public string FileNameFormat = Tag+"{0}.dat";
        public string FileNamePattern = Tag+"*.dat";
        public int NrItemsPerPage=100000;
        public int NrLoadedPages = 10;
        public FactDictionaryCollection() 
        {

        }

        public IEnumerable<KeyValuePair<int[], FactKeyWithCells>> Facts 
        {
            get 
            {
                foreach (var page in Pages)
                {
           
                    foreach (var key in page.LookupOfIndexes)
                    {
                        yield return new KeyValuePair<int[], FactKeyWithCells>(key.Value.FactKey, key.Value);
                    }
                    //yield return item + "roxxors";
                }
            }
        }

        public string GetFilePath(int ix) 
        {
            var filename = string.Format(FileNameFormat, ix);
            return Folder() + filename;
        }

        public void LoadPages() 
        {
            var files = System.IO.Directory.GetFiles(Folder(), FileNamePattern);
            foreach (var file in files) 
            {

                var page = CreatePage();
                //var filename = Utilities.Strings.GetFileName(file);
                //var ix = Utilities.Converters.FastParse(Utilities.Strings.TextBetween(filename, Tag, "."));
                //page.ID = ix;
                LoadPage(page);

            }
            Utilities.FS.DictionaryFromFile(GetHashKeyFilePath(), this.HashKeys);
            //Utilities.FS.DictionaryFromFile(GetLookupOfIndexesFilePath(), this.FactKeyCountOfIndexes, (i) => Utilities.Converters.FastParse(i), (s) => FactLookupValue.GetInstanceFromString(s));


        }
        public string GetHashKeyFilePath()
        {
            return Folder() + "HashKeys.dat";
        }
        public string GetLookupOfIndexesFilePath()
        {
            return Folder() + "LookupOfIndexes.dat";
        }
        public FactKeyDictionary CreatePage() 
        {
            var page = new FactKeyDictionary();
            page.ID = this.Pages.Count;
            page.NrMaxItems = this.NrItemsPerPage;
            page.IndexStartAt = page.ID * page.NrMaxItems;
            page.IndexEndAt = page.IndexStartAt + page.NrMaxItems;
            this.Pages.Add(page);
            this.LoadedPages.Add(page.ID);
            LastPage = page;
            return page;
        }
        
        public void LoadPage(int ix) 
        {
            var page = this.Pages[ix];
            LoadPage(page);
        }
        public void LoadPage(FactKeyDictionary page)
        {
            Log("Loading page " + page.ID);
            var filepath = GetFilePath(page.ID);
            var lines = System.IO.File.ReadAllLines(filepath);
            lock (Locker)
            {
                foreach (var line in lines)
                {
                    var kv = page.Save(line);
                    //if (!KeyCountOfIndex.ContainsKey(kv.Key))
                    //{
                    //    KeyCountOfIndex.Add(kv.Key,  kv.Value.Length);
                    //}
                }
                page.IsDirty = false;
            }
            if (NrLoadedPages!=0 && LoadedPages.Count > NrLoadedPages )
            {
                Unload(0);
            }
            LoadedPages.Add(page.ID);
            LastPage = page;
        

        }
        protected void Log(string item) 
        {
            var debug = true;
            if (debug)
            {
                Console.WriteLine(item);
            }
        }
 

        public void CheckPages(Func<FactKeyDictionary,bool> action) 
        {
            for (int i = LoadedPages.Count-1; i > -1; i--) 
            {
                if (action(Pages[LoadedPages[i]])) { return; }

            }
            //foreach (var pageix in LoadedPages)
            //{
            //    if (action(Pages[pageix])) { return; }
            //}
            var notloadedpages = Pages.Where(i => !i.IsLoaded);
            foreach (var page in notloadedpages)
            {
        
                LoadPage(page);
                if (action(page)) { return; }

            }
        }

        public bool ContainsIndex(int index)
        {
            var result = false;
            CheckPages((p) => 
            { 
                result = p.ContainsIndex(index); return result; 
            });
            return result;
        }

        public int GetIndexByHashKeys(int[] hashkeys)
        {
            if (this.HashKeys.ContainsKey(hashkeys))
            {
                return this.HashKeys[hashkeys];
            }
            else { return -1; }
        }
        public int[] GetHashKeys(int[] array)
        {
            var result = new int[2];
            result[0] = GetHashKey1(array);
            result[1] = GetHashKey2(array);
            return result;
        }
        public int GetHashKey1(int[] array)
        {
            int hc = array.Length;
            for (int i = 0; i < array.Length; ++i)
            {
                hc = unchecked(hc * 314159 + array[i]);
            }
            return hc;
        }
        public int GetHashKey2(int[] array)
        {
            //98047
            int hc = array.Length;
            for (int i = 0; i < array.Length; ++i)
            {
                hc = unchecked(hc * 98047 - array[i]);
            }
            return hc;

        } 

        public bool ContainsKey(int[] key)
        {
            var hashkey = GetHashKeys(key);
            return this.HashKeys.ContainsKey(hashkey);
        }

        public int Index(int[] key)
        {
            var hashkey = GetHashKeys(key);
            if (this.HashKeys.ContainsKey(hashkey)) 
            {
                var ix = this.HashKeys[hashkey];
                return ix;
            }
            return -1;
        }

        //public KeyValuePair<int[], FactLookupValue> GetItem(int[] key)
        //{
        //    var result = new KeyValuePair<int[], FactLookupValue>(key, new FactLookupValue());
        //    CheckPages((p) =>
        //    {
        //        result = p.ContainsKey(key) ? p.GetItem(key) : result;
        //        return p.ContainsKey(key);
        //    });
        //    return result;
        //}
        public Utilities.KeyValue<int, List<int>> GetKvp(int[] key)
        {
            var result = new Utilities.KeyValue<int, List<int>>(-1, new List<int>());
            var hashkey = GetHashKeys(key);
            var ix = GetIndexByHashKeys(hashkey);
            result.Key = ix;
            result.Value = GetFactKeyWithCells(ix).CellIndexes;
            return result;
        }
        public int[] Key(int index)
        {
            return GetFactKeyWithCells(index).FactKey;
        
        }

        public FactKeyWithCells GetFactKeyWithCells(int index) 
        {
            if (index >= LastPage.IndexStartAt && index < LastPage.IndexEndAt)
            {
                return LastPage.LookupOfIndexes[index];

            }

            int pid = index / NrItemsPerPage;
            LastPage = this.Pages[pid];
            if (!LastPage.IsLoaded)
            {
                LoadPage(LastPage);
            }
            return LastPage.LookupOfIndexes[index];
        }

 

        public List<int> this[int index]
        {
            get 
            {
                return GetFactKeyWithCells(index).CellIndexes;
            }
        }



        public List<int> this[int[] key]
        {
            get 
            {
                var hashkey  = GetHashKeys(key);
                var index = GetIndexByHashKeys(hashkey);
                return GetFactKeyWithCells(index).CellIndexes;
            }
        }
        public int Count { 
            get 
            {
                if (this.Pages.Count == 0) { return 0; }
                return (this.Pages.Count - 1) * NrItemsPerPage + this.Pages.LastOrDefault().Count;
            } 
        }
        public IEnumerable<int[]> Keys
        {
            get
            {
                var result = new List<int[]>();
                CheckPages((p) => { result.Union(p.Keys); return false; });
                return result;
            }
        }
        public IEnumerable<int[]> FactKeysAsEnumerable
        {
            get
            {
                foreach (var page in Pages)
                {
                    if (!page.IsLoaded)
                    {
                        LoadPage(page);
                    }
                    foreach (var item in page.Keys)
                    {
                        yield return item;
                    }
                }
            }
        }
        public IEnumerable<Utilities.KeyValue<int[], List<int>>> FactsAsEnumerable
        {
            get
            {
                foreach (var page in Pages)
                {
                    if (!page.IsLoaded)
                    {
                        LoadPage(page);
                    }
                    foreach (var item in page.LookupOfIndexes)
                    {
                        yield return new Utilities.KeyValue<int[], List<int>>(item.Value.FactKey, item.Value.CellIndexes);
                    }
                }
            }
        }
        public IEnumerable<int> FactIndexes
        {
            get
            {
                return this.FactKeyCountOfIndexes.Keys;
            }
        }

        public int Save(int[] key)
        {
            return Save(key, new List<int>(1));
        }

        public int Save(int[] key, List<int> value)
        {
            var lastpage = Pages.LastOrDefault();
            if (lastpage == null)
            {
                lastpage = CreatePage();
            }
            if (lastpage.Count == NrItemsPerPage)
            {
                Save(lastpage);
                lastpage = CreatePage();

            }
            if (NrLoadedPages != 0 && LoadedPages.Count > NrLoadedPages)
            {
                Unload(0);

            }
            var ix = FactKeyCountOfIndexes.Count;
            lastpage.Save(key, ix,value);
            if (!this.FactKeyCountOfIndexes.ContainsKey(ix))
            {
                FactKeyCountOfIndexes.Add(ix, key.Length);

            }
            return ix;
        }
        public void Unload(int ix)
        {
            var pageix = LoadedPages[ix];
            var page = this.Pages[pageix];
            Unload(page);
        }
        private void Unload(FactKeyDictionary page)
        {


            Log("UnLoading page " + page.ID);
            if (page.IsDirty)
            {
                Save(page);
            }
            LoadedPages.Remove(page.ID);
        }
        public object Locker = new Object();
        public void Save(FactKeyDictionary page)
        {
            Task.Run(
                () =>
                {
                    lock (Locker)
                    {
                        var path = GetFilePath(page.ID);
                        Utilities.FS.WriteAllText(path, page.Content());
                        page.IsDirty = false;
                        page.Clear();
                    }
                });
        }
        public void SavePages(bool clear=false) 
        {
            var pages = Pages.ToList();

            foreach (var page in pages) 
            {
                Save(page);
                if (clear)
                {
                    Unload(page);
                }
            }
            Utilities.FS.DictionaryToFile(GetHashKeyFilePath(), this.HashKeys);
            //Utilities.FS.DictionaryToFile(GetLookupOfIndexesFilePath(), pages,(i)=>i.ToString(), (f)=>f.ToString());
            
        }


        public void Clear() 
        {
            var pages = Pages.ToList();
            foreach (var page in pages)
            {
                Unload(page);
            }
            this.Pages.Clear();
            FactKeyCountOfIndexes.Clear();
            HashKeys.Clear();
        }






    }

   
    public class FactsPartsDictionary : SharedDictionary<int, List<int>> 
    {
        public int GetTotalCount() 
        {
            var count = 0;
            foreach (var item in this) 
            {
                count += item.Value.Count;
            }
            return count;
        }
        public void AddIfNotExists(int key, int index) 
        {
            if (!this.ContainsKey(key)) 
            {
                this.Add(key, new List<int>());
            }
            this[key].Add(index);
        }

        public void MoveTo(FactsPartsDictionary target) 
        {
            foreach (var item in this) 
            {
                if (!target.ContainsKey(item.Key)) 
                {
                    target.Add(item.Key, new List<int>());
                }
                target[item.Key].AddRange(this[item.Key]);
             
                target[item.Key] = target[item.Key].Distinct().ToList();
                target[item.Key].TrimExcess();
                this[item.Key].Clear();
            }
        }
        
    }
    
    public class RelationDictionary
    {
        
        public static FactsPartsDictionary GetFactsOfParts(Taxonomy taxonomy) 
        {
            var instance = new FactsPartsDictionary();
            instance.DeSerializeItems = (lines) => {
                var values = new List<int>();
                foreach (var line in lines) 
                {
                    values.Add(Utilities.Converters.FastParse(line));
                }
                values.TrimExcess();

                return values;
            };
            instance.SerializeItem = (itemcontainer) =>
            {
                var sb = new StringBuilder();
                foreach(var item in itemcontainer)
                {
                    sb.AppendLine(item.ToString());
                }
                return sb;
            };
            instance.Folder = () => taxonomy.TaxonomyFactsFolder;
            instance.FileSearchPattern = "FactsOfParts_*.dat";
            instance.GetKey = (file) => 
            {
                return Utilities.Converters.FastParse(Utilities.Strings.TextBetween(file, "FactsOfParts_", ".dat"));
            };
    
            return instance;
        }
    }

    public class SharedDictionary<TKey, TValue> : Dictionary<TKey, TValue> where TValue : IEnumerable, new()
    {
        public Func<IEnumerable<string>, TValue> DeSerializeItems = (s) => default(TValue);
        public Func<TValue, StringBuilder> SerializeItem = (key) => new StringBuilder();
        public Action<string> DeSerializer = null;
        public Action Serializer = null; 
        public Func<string> Folder = () => "";
        public Boolean SaveOnUnload = true;
        public string FileSearchPattern = "*";

        public string FileNamePattern 
        {
            get { return FileSearchPattern.Replace("*", "{0}"); }
        }
        public Func<string, TKey> GetKey = (filepath) => 
        {
            return default(TKey);
        };
    
        public Func<TKey, string> FileName = (k) => "";

        public int MaxItemsLoaded = 0;

        protected List<string> GetFiles() 
        {
            var files = System.IO.Directory.GetFiles(Folder(), FileSearchPattern);
            return files.ToList();

        }
        public string GetFilePath(TKey key) 
        {
            var filepath = Folder() + String.Format(FileNamePattern, key);
            return filepath;
        }
        public void Load() 
        {
            var files = GetFiles();
            foreach(var file in files)
            {
                if (this.DeSerializer != null)
                {
                    this.DeSerializer(file);
                }
                else 
                {
                    LoadFile(file);
                }
            }
        }
        public void LoadFile(string file) 
        {
            var lines = System.IO.File.ReadLines(file);
            var id = GetKey(file);
            var h = DeSerializeItems(lines);
            this.Add(id, h);
        }
        public Boolean IsLoaded(TKey key) 
        {
            return this[key] != null;
        }
        public void Load(TKey key)
        {
            if (!IsLoaded(key)) 
            {
                LoadFile(GetFilePath(key));
            }
        }
        public void Save() 
        {
            foreach (var key in this.Keys) 
            {
                Save(key);
            }
        }
        public void Save(TKey key)
        {
            var sb = SerializeItem(this[key]);

            Utilities.FS.WriteAllText(GetFilePath(key), sb.ToString());

        }
        public void UnLoad(bool save=true) 
        {
            foreach (var key in this.Keys)
            {
                UnLoad(key);
            }
        }
        public void UnLoad(TKey key,bool save=true)
        {
            if (SaveOnUnload)
            {
                Save(key);
            }
            this[key] = default(TValue);
        }

    }

    public class Indexes
    {
        public OrderedDictionary Facts = new OrderedDictionary();
        public Dictionary<int[], int> a = new Dictionary<int[], int>();
        public List<int[]> b = new List<int[]>();

        public void x() 
        {
            //Facts.Add()
        }

    }
   
    public class textx 
    {
        private int totlitems = 8000000;

        public void test9()
        {
            var dict = new Dictionary<int[], List<int>>();
            var dict2 = new Dictionary<int, int[]>();
            var dict3 = new List<int[]>();
            for (int i = 0; i < totlitems; i++)
            {
                var keys = new int[12];
                for (int j = 0; j < keys.Length; j++)
                {
                    keys[j] = i % (j + 1);
                }
                dict.Add(keys, new List<int>(1) { i });
                //dict2.Add(i, keys);
                dict3.Add(keys);
            }
        }
        public void test0()
        {
            var dict = new SortedList<int[], List<int>>(new Utilities.IntArrayComparer());
            int half = totlitems / 2;
            int[] thekey = null;
            var d = DateTime.Now;
            DateTime cd;
            for (int i = 0; i < totlitems; i++)
            {
                var keys = new int[12];
                keys[0] = i;
                for (int j = 1; j < keys.Length; j++)
                {
                    keys[j] = i % (j + 1);
                }
                dict.Add(keys, new List<int>(1) { i });
                if (i == half)
                {
                    thekey = keys;
                }
            }

            Console.WriteLine(DateTime.Now.Subtract(d).TotalMilliseconds); d = DateTime.Now;
            var item1 = dict.Values[half];
            var s = 0;
            for (int i = 0; i < 5000; i++)
            {
                s = dict.IndexOfKey(thekey);
            }
            Console.WriteLine(s);
            Console.WriteLine(DateTime.Now.Subtract(d).TotalMilliseconds); d = DateTime.Now;
            for (int i = 0; i < 5000; i++)
            {
                var item2 = dict[thekey];
            }
            Console.WriteLine(DateTime.Now.Subtract(d).TotalMilliseconds); d = DateTime.Now;

        }

        public void test3()
        {
            var dict = new FactDictionaryCollection();
            int half = totlitems / 2;
            int[] thekey = null;
            var d = DateTime.Now;
            DateTime cd;
            for (int i = 0; i < totlitems; i++)
            {
                var keys = new int[12];
                keys[0] = i;
                for (int j = 1; j < keys.Length; j++)
                {
                    keys[j] = i % (j + 1);
                }
                dict.Save(keys, new List<int>(1) { i });
                if (i == half)
                {
                    thekey = keys;
                }
            }

            Console.WriteLine(DateTime.Now.Subtract(d).TotalMilliseconds); d = DateTime.Now;
            var item1 = dict[half];
            var s = 0;
            for (int i = 0; i < 5000; i++)
            {
                s = dict.Index(thekey);
            }
            Console.WriteLine(s);
            Console.WriteLine(DateTime.Now.Subtract(d).TotalMilliseconds); d = DateTime.Now;
            for (int i = 0; i < 5000; i++)
            {
                var item2 = dict[thekey];
            }
            Console.WriteLine(DateTime.Now.Subtract(d).TotalMilliseconds); d = DateTime.Now;

        }
        public void test1() 
        {
            //var x = new List<List<int>>();
            //for (int i = 0; i < 50; i++) 
            //{
            //    x.Add(new List<int>() { 0, 1 });
            //}
            //var y = Utilities.MathX.CartesianProduct(x);
            //var z = y.ToList();
            var dict = new OrderedDictionary();
            int half = totlitems/2;
            int[] thekey=null;
            var d = DateTime.Now;
            DateTime cd;
            for (int i = 0; i < totlitems; i++) 
            {
                var keys = new int[12];
                for (int j = 0; j < keys.Length; j++) 
                {
                    keys[j] = i % (j+1);
                }
                dict.Add(keys, new List<int>(){i});
                if (i == half) 
                {
                    thekey=keys;
                }
            }

            Console.WriteLine(DateTime.Now.Subtract(d).TotalMilliseconds); d = DateTime.Now;
            var item1 = dict[half];
            for (int i = 0; i < 5000; i++)
            {
                var item2 = dict[thekey];
            }
            Console.WriteLine(DateTime.Now.Subtract(d).TotalMilliseconds); d = DateTime.Now;
            for (int i = 0; i < 5000;i++ )
            {
                var item2 = dict[thekey];
            }
            Console.WriteLine(DateTime.Now.Subtract(d).TotalMilliseconds); d = DateTime.Now;

        }
        public void test2()
        {
            var dict = new Dictionary<int[],List<int>>();
            var ix = new List<int[]>();
            int half = totlitems / 2;
            int[] thekey = null;
            var d = DateTime.Now;
            DateTime cd;
            for (int i = 0; i < totlitems; i++)
            {
                var keys = new int[12];
                for (int j = 0; j < keys.Length; j++)
                {
                    keys[j] = i % (j + 1);
                }
                dict.Add(keys, new List<int>(1) { i });
                if (i == half)
                {
                    thekey = keys;
                }
                ix.Add(keys);
            }

            Console.WriteLine(DateTime.Now.Subtract(d).TotalMilliseconds); d = DateTime.Now;
            for (int i = 0; i < 5000; i++)
            {
                var item2 = dict[thekey];
            }
            Console.WriteLine(DateTime.Now.Subtract(d).TotalMilliseconds); d = DateTime.Now;

        } 
    }
}
