using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

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
            return string.Format("{0}|{1}", this.KeyCount, Utilities.Strings.ListToString(this.CellIndexes, ","));
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
        //public Dictionary<int, int> FactKeyCountOfIndexes = new Dictionary<int, int>();
        //public Dictionary<int[], int> HashKeys = new Dictionary<int[], int>(new Utilities.IntArrayEqualityComparer());

        public Dictionary<int, int> FactKeyCountOfIndexes = new Dictionary<int, int>();
        public Dictionary<Tintint, int> HashKeys = new Dictionary<Tintint, int>(new Utilities.TintintEqualityComparer());
        
        //public SortedList<int[], int> HashKeys = new SortedList<int[], int>(100000,new Utilities.IntArrayComparer());

        public FactKeyDictionary LastPage = null;
        public List<int> LoadedPages = new List<int>();
        public Func<string> Folder = () => @"C:\Users\vladimir.balacescu\Desktop\f\";
        public static string Tag = "Facts_";
        public string FileNameFormat = Tag+"{0}.dat";
        public string FileNamePattern = Tag+"*.dat";
        public int NrItemsPerPage=100000;
        public int NrLoadedPages = 15;
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

                var page = CreatePage(PeristenceEnum.Unloaded);
                //var filename = Utilities.Strings.GetFileName(file);
                //var ix = Utilities.Converters.FastParse(Utilities.Strings.TextBetween(filename, Tag, "."));
                //page.ID = ix;
               
                //LoadPage(page);

            }
            Utilities.FS.DictionaryFromFile(GetHashKeyFilePath(), this.HashKeys);
            Utilities.FS.DictionaryFromFile(GetKeyCountFilePath(), this.FactKeyCountOfIndexes);
            //Utilities.FS.DictionaryFromFile(GetLookupOfIndexesFilePath(), this.FactKeyCountOfIndexes, (i) => Utilities.Converters.FastParse(i), (s) => FactLookupValue.GetInstanceFromString(s));


        }
        public string GetHashKeyFilePath()
        {
            return Folder() + "HashKeys.dat";
        }
        public string GetKeyCountFilePath()
        {
            return Folder() + "KeyCount.dat";
        }
        public FactKeyDictionary CreatePage() 
        {
            Log("Creating Page " + this.Pages.Count);
            var page = new FactKeyDictionary();
            page.ID = this.Pages.Count;
            page.NrMaxItems = this.NrItemsPerPage;
            page.IndexStartAt = page.ID * page.NrMaxItems;
            page.IndexEndAt = page.IndexStartAt + page.NrMaxItems;
            this.Pages.Add(page);
            AddIDToLoadedPages(page.ID);
            LastPage = page;
            return page;
        }

        public FactKeyDictionary CreatePage(PeristenceEnum peristence)
        {
            var page = new FactKeyDictionary();
            page.ID = this.Pages.Count;
            page.NrMaxItems = this.NrItemsPerPage;
            page.IndexStartAt = page.ID * page.NrMaxItems;
            page.IndexEndAt = page.IndexStartAt + page.NrMaxItems;
            this.Pages.Add(page);
            page.PeristenceState = peristence;
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
            Log("Loading page" + page.ID);
            //Utilities.Logger.WriteLine("Loading page" + page.ID);
            page.PeristenceState = PeristenceEnum.Loading;
            var filepath = GetFilePath(page.ID);

            lock (page.Locker)
            {
                // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(filepath))
                {

                    while (sr.Peek() >= 0)
                    {
                        String line = sr.ReadLine();
                        var kv = page.Save(line);


                    }
                    page.IsDirty = false;

                }
                // Read the stream to a string, and write the string to the console.


                page.PeristenceState = PeristenceEnum.Loaded;


                if (NrLoadedPages != 0 && LoadedPages.Count > NrLoadedPages)
                {
                    var ix = 0;
                    if (LoadedPages[ix] == page.ID)
                    {
                        ix++;
                    }
                    Unload(ix);
                }
                AddIDToLoadedPages(page.ID);
                LastPage = page;
            }

        }

        private void RemoveIDFromLoadedPages(int pageid) 
        {
            if (pageid == 8)
            {
            }
            Log(String.Format("Removing page{0} from LoadedPages", pageid));
            LoadedPages.Remove(pageid);
        }
        private void AddIDToLoadedPages(int pageid) 
        {
            if (pageid == 8)
            { 
            } 
            LoadedPages.Add(pageid);
            Log(String.Format("page{0} was added to LoadedPages", pageid));
        }

        private StringBuilder sb_log = new StringBuilder();
        protected void Log(string item) 
        {
            var debug = true;
            if (debug)
            {
                var msg = Utilities.Converters.DateTimeToString(DateTime.Now, Utilities.Converters.DateTimeFormat) + ":" + item;
                sb_log.AppendLine(msg);
                Console.WriteLine(msg);
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
            if (this.HashKeys.ContainsKey(new Tintint(hashkeys[0], hashkeys[1])))
            {
                return this.HashKeys[new Tintint(hashkeys[0], hashkeys[1])];
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
            var hashkeys = GetHashKeys(key);
            return this.HashKeys.ContainsKey(new Tintint(hashkeys[0], hashkeys[1]));
        }

        public int Index(int[] key)
        {
            var hashkeys = GetHashKeys(key);
            if (this.HashKeys.ContainsKey(new Tintint(hashkeys[0], hashkeys[1]))) 
            {
                var ix = this.HashKeys[new Tintint(hashkeys[0], hashkeys[1])];
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
        public FactKeyDictionary GetPageByFactIndex(int factindex) 
        {
            if (factindex >= LastPage.IndexStartAt && factindex < LastPage.IndexEndAt)
            {
                return LastPage;

            }

            int pid = factindex / NrItemsPerPage;
            return this.Pages[pid];
        }
        public FactKeyWithCells GetFactKeyWithCells(int index) 
        {
            var result = new FactKeyWithCells();
            FactKeyDictionary page= null;
            if (index >= LastPage.IndexStartAt && index < LastPage.IndexEndAt)
            {
                page = LastPage;

            }
            else
            {

                int pid = index / NrItemsPerPage;
                page = this.Pages[pid];
                LastPage = page;
            }
            if (!page.IsLoaded)
            {
                //if (page.ID == 21) 
                //{ 
                //}
                LoadPage(page);
            }
            if (page.LookupOfIndexes.ContainsKey(index)) 
            {
                result = page.LookupOfIndexes[index];

            }
            return result;
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
                var lastpage = this.Pages.LastOrDefault();
                if (!lastpage.IsLoaded) 
                {
                    LoadPage(lastpage);
                }
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
        protected bool LimitReached()
        {
            return NrLoadedPages != 0 && LoadedPages.Count > NrLoadedPages; 
        }
        public int Save(int[] key, List<int> value)
        {
            var lastpage = Pages.LastOrDefault();
            if (lastpage == null)
            {
                lastpage = CreatePage();
            }
            var limitreached = LimitReached();
            if (lastpage.Count == NrItemsPerPage)
            {
                if (lastpage.IsDirty)
                {
                    SaveToFS(lastpage, false);
                }
                lastpage = CreatePage();

            }
            if (limitreached)
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
            var pageid= LoadedPages[ix];
       
            var page = this.Pages[pageid];
            while (page.IsPersisting)
            { 
                pageid=pageid+1;
                if (LastPage.ID == pageid) 
                {
                    pageid = 0;
                }
                page = this.Pages[pageid % this.Pages.Count];
            }
            if (page.PeristenceState != PeristenceEnum.Unloading) 
            { 
                Unload(page); 
            }
        }
        private void Unload(FactKeyDictionary page)
        {
            page.PeristenceState = PeristenceEnum.Unloading;
            if (page.ID == 11) 
            {

            }
            Log("UnLoading page" + page.ID);

            lock (page.Locker)
            {
                if (page.IsDirty)
                {
                    SaveToFS(page);
                }
                else
                {
                    page.Clear(Log);
                }
                RemoveIDFromLoadedPages(page.ID);
                page.PeristenceState = PeristenceEnum.Unloaded;
            }



        }
        //public object Locker = new Object();
        public void SaveToFS(FactKeyDictionary page, Boolean unload = true)
        {

            Task.Run(
                () =>
                {
                    lock (page.Locker)
                    {
                        var state = page.PeristenceState;
                        page.PeristenceState = PeristenceEnum.Saving;
                        var msg = String.Format("Saveing page{0} to FS", page.ID);
                        Log(msg);
                        //Utilities.Logger.WriteLine(msg);      

                        var path = GetFilePath(page.ID);
                        Utilities.FS.EnsurePath(path);
                        //Utilities.FS.WriteAllText(path, page.Content());
                        using (System.IO.StreamWriter fsw = new System.IO.StreamWriter(path, false))
                        {
   
                            foreach (var item in page.LookupOfIndexes)
                            {
                                fsw.WriteLine(FactKeyDictionary.Content(item));
                            }
                            page.PeristenceState = state;

                        }

                        Log(String.Format("page{0} was saved to FS", page.ID));

                        page.IsDirty = false;
                        if (unload)
                        {
                            page.Clear(Log);
                            RemoveIDFromLoadedPages(page.ID);
                            page.PeristenceState = PeristenceEnum.Unloaded;
                        }


                    }
                });
        }
        
        public void SavePages(bool clear=false) 
        {
            var pages = Pages.ToList();

            foreach (var page in pages) 
            {
                if (page.IsDirty)
                {
                    SaveToFS(page);
                }
                if (clear)
                {
                    Unload(page);
                }
            }
            Utilities.FS.DictionaryToFile(GetHashKeyFilePath(), this.HashKeys);
            Utilities.FS.DictionaryToFile(GetKeyCountFilePath(), this.FactKeyCountOfIndexes);
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







        internal void AddCellToFact(int index, int cellix)
        {
            var page = GetPageByFactIndex(index);
            if (page.IsPersisting)
            {
                lock (page.Locker)
                {
                    if (!page.IsLoaded) 
                    {
                        LoadPage(page);
                    }
                    page.LookupOfIndexes[index].CellIndexes.Add(cellix);
                    page.IsDirty = true;
                }
            }
            else
            {
                var loadrequired = false;
                if (!page.IsLoaded)
                {
                    loadrequired = true;
                    LoadPage(page);
                }
                if (!page.LookupOfIndexes.ContainsKey(index))
                {
                    var msg = String.Format("Page {0}[{4}] has no key {1}; IsLoaded {2} LoadRequired: {3}", page.ID, index, page.IsLoaded, loadrequired, page.LookupOfIndexes.Count);
                    Utilities.Logger.WriteLine(msg);
                    Utilities.Logger.WriteLine(sb_log.ToString());
                    Log(msg);

                }
                page.LookupOfIndexes[index].CellIndexes.Add(cellix);
                page.IsDirty = true;

            }
        }
    }

   
    public class FactsPartsDictionary : SharedDictionary<int, IntervalList> 
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
                this.Add(key, new IntervalList());
            }
            this[key].Add(index);
        }

        public void MoveTo(FactsPartsDictionary target) 
        {
            foreach (var item in this) 
            {
                //if (item.Key == 11529) 
                //{ 

                //}
                if (!target.ContainsKey(item.Key)) 
                {
                    target.Add(item.Key, new IntervalList());
                }
                var targetitem = target[item.Key];
                //IntervalList t = targetitem.Copy();
                //t.AddRange(this[item.Key]);
                //if (!t.IsConsistent()) 
                //{

                //}
                targetitem.AddRange(this[item.Key]);
             
                //target[item.Key] = target[item.Key].Distinct().ToList();

                targetitem.TrimExcess();
                this[item.Key].Clear();
            }
        }

        public IList<int> SearchFactsIndexByKey(int[] factkey, IList<int> facts)
        {
            var memberkeys = factkey.ToList();
            var memberfactspool = new List<IList<int>>();
            foreach (var memberkey in memberkeys)
            {
                if (this.ContainsKey(memberkey))
                {
                    memberfactspool.Add(this[memberkey]);
                }
                else 
                {
                    //Utilities.Logger.WriteLine(String.Format("SearchFactsIndexByKey: memberkey {0} was not found!", memberkey));
                    return new IntervalList();
                }
            }
            var partcount = memberfactspool.Count;
            if (facts != null)
            {
                memberfactspool.Add(facts);

            }
            memberfactspool = memberfactspool.OrderBy(i => i.Count).ToList();
            var result = memberfactspool.FirstOrDefault();
            //if ( facts!=null && (result==null || result.Count > facts.Count)) 
            //{
            //    result = facts;
            //}
            for (int i = 1; i < memberfactspool.Count; i++)
            {
                result = Utilities.Objects.IntersectSorted(result, memberfactspool[i], null);
            }
            if (result == null)
            {
                result = new List<int>();
            }
            return result;
        }
        
    }
    
    public class RelationDictionary
    {
        
        public static FactsPartsDictionary GetFactsOfParts(Taxonomy taxonomy) 
        {
            var instance = new FactsPartsDictionary();
            instance.DeSerializeItems = (lines) => {
                var values = new IntervalList();
                foreach (var line in lines) 
                {
                    values.AddInterval(Interval.GetInstanceFromString(line));
                }
                values.TrimExcess();

                return values;
            };
            instance.SerializeItem = (itemcontainer) =>
            {
                var sb = new StringBuilder();
                foreach(var item in itemcontainer.Intervals)
                {
                    sb.AppendLine(item.Content());
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
        private int totlitems = 1000000;

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
        public void test90()
        {
            var dict = new List<FactKeyWithCells>();
            for (int i = 0; i < totlitems; i++)
            {
                var keys = new int[12];
                for (int j = 0; j < keys.Length; j++)
                {
                    keys[j] = i % (j + 1);
                }
                var fkc = new FactKeyWithCells();
                fkc.FactKey = keys;
                fkc.CellIndexes = new List<int>(1) { i };
                dict.Add(fkc);
                //dict2.Add(i, keys);
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
