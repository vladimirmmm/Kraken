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
    public class FactLookupValue 
    {
        public int Index = -1;
        private List<int> _CellIndexes = new List<int>();
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
        public FactLookupValue(int index)
        {
            this.Index = index;
        }
        public FactLookupValue(int index, List<int> cellIndexes)
        {
            this.Index = index;
            this.CellIndexes = cellIndexes;
        }
    }
    public class FactDictionaryCollection : LogicalModel.Models.IFactDictionary
    {
        public List<FactDictionary3> Pages = new List<FactDictionary3>();
        public FactDictionary3 LastPage = null;
        public List<int> LoadedPages = new List<int>();
        public Func<string> Folder = () => @"C:\Users\vladimir.balacescu\Desktop\f\";
        public static string Tag = "Facts_";
        public string FileNameFormat = Tag+"{0}.dat";
        public string FileNamePattern = Tag+"*.dat";
        public int NrItemsPerPage=500000;
        public int NrLoadedPages = 5;
        public FactDictionaryCollection() 
        {

        }

        public IEnumerable<KeyValuePair<int[], FactLookupValue>> Facts 
        {
            get 
            {
                foreach (var page in Pages)
                {
           
                    foreach (var key in page.Keys)
                    {
                        yield return page.GetItem(key);
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
        }

        public FactDictionary3 CreatePage() 
        {
            var page = new FactDictionary3();
            page.ID = this.Pages.Count;
            page.NrMaxItems = this.NrItemsPerPage;
            page.IndexStartAt = page.ID * page.NrMaxItems;
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
        public void LoadPage(FactDictionary3 page)
        {
            Log("Loading page " + page.ID);
            var filepath = GetFilePath(page.ID);
            var lines = System.IO.File.ReadAllLines(filepath);
            foreach (var line in lines)
            {
                page.Save(line);
            }
            if (NrLoadedPages!=0 && LoadedPages.Count > NrLoadedPages )
            {
                Unload(0);
            }
            LoadedPages.Add(page.ID);
        

        }
        protected void Log(string item) 
        {
            var debug = true;
            if (debug)
            {
                Console.WriteLine(item);
            }
        }
 

        public void CheckPages(Func<FactDictionary3,bool> action) 
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


        public bool ContainsKey(int[] key)
        {
            var result = false;
            CheckPages((p) => 
            { 
                result = p.ContainsKey(key); return result; 
            });
            return result;
        }

        public int Index(int[] key)
        {
            var result = -1;
            CheckPages((p) => 
            { 
                result = p.ContainsKey(key) ? p.Index(key) : -1; return p.ContainsKey(key); 
            });
            return result;
        }

        public KeyValuePair<int[], FactLookupValue> GetItem(int[] key)
        {
            var result = new KeyValuePair<int[], FactLookupValue>(key,new FactLookupValue());
            CheckPages((p) => 
            { 
                result = p.ContainsKey(key) ? p.GetItem(key) : result; 
                return p.ContainsKey(key); 
            });
            return result;
        }
        public Utilities.KeyValue<int, List<int>> GetKvp(int[] key)
        {
            var result = new Utilities.KeyValue<int, List<int>>(-1, new List<int>());
            CheckPages((p) => { 
                result = p.ContainsKey(key) ? p.GetKvp(key) : result; return p.ContainsKey(key); 
            });
            return result;
        }
        public int[] Key(int index)
        {
            if (LastPage.ContainsIndex(index)) 
            {
                return LastPage.Key(index);
            }
            int pid = index / NrItemsPerPage;
            return this.Pages[pid].Key(index);
        
        }

 

        public List<int> this[int index]
        {
            get 
            {
                var result = new List<int>();
                CheckPages((p) =>
                {
                    //result = p.ContainsIndex(index) ? p[index] : result; return p.ContainsIndex(index);

                    var contains = p.ContainsIndex(index);
                    if (contains)
                    {
                        result = p.GetItem(index);
                    }
                    return contains;
                });
                return result;
            }
        }



        public List<int> this[int[] key]
        {
            get 
            {
                var result = new List<int>();
                CheckPages((p) => 
                { 
                    result = p.ContainsKey(key) ? p[key] : result; return p.ContainsKey(key); 
                });
                return result;
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
                    foreach (var item in page.Keys)
                    {
                        yield return new Utilities.KeyValue<int[], List<int>>(item, page[item]);
                    }
                }
            }
        }
        public IEnumerable<int> FactIndexes
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
                        yield return page.Index(item);
                    }
                }
            }
        }

        public void Save(int[] key)
        {
            Save(key, new List<int>());
        }

        public void Save(int[] key, List<int> value)
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
            lastpage.Save(key, value);
        }
        public void Unload(int ix)
        {
            var pageix = LoadedPages[ix];
            var page = this.Pages[pageix];
            Unload(page);
        }
        private void Unload(FactDictionary3 page)
        {


            Log("UnLoading page " + page.ID);
            if (page.IsDirty)
            {
                Save(page);
            }
            LoadedPages.Remove(page.ID);
            page.Clear();
        }
        public void Save(FactDictionary3 page)
        {
            Task.Run(
                () =>
                {
                    var path = GetFilePath(page.ID);
                    Utilities.FS.WriteAllText(path, page.Content());
                    page.IsDirty = false;
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
            
        }
        public void Clear() 
        {
            var pages = Pages.ToList();
            foreach (var page in pages)
            {
                Unload(page);
            }
            this.Pages.Clear();

        }




    }

    public class FactDictionary3 : LogicalModel.Models.IFactDictionary 
    {
        public int ID = 0;
        public int NrMaxItems = -1;
        public int IndexStartAt = 0;
        protected Dictionary<int[], FactLookupValue> a = new Dictionary<int[], FactLookupValue>(new Utilities.IntArrayEqualityComparer());
        protected Dictionary<int,int[]> b = new Dictionary<int,int[]>();
        public bool IsDirty = false;
        private bool _IsLoaded = false;
        public bool IsLoaded 
        {
            get 
            {
                return _IsLoaded;
            }
        }
        public void UnLoad() 
        {
            this._IsLoaded = false;
        }
        public void Load()
        {
            this._IsLoaded = true;
        }
        public FactDictionary3() 
        {
        }
        public int Count { get { return a.Count; } }

        public IEnumerable<int[]> Keys 
        {
            get
            {
                return b.Values;
            }
        }
        public void Save(string content)
        {
            var sp_pipe = Literals.PipeSeparator[0];
            var sp_coma = Literals.Coma[0];
            var s_ix = 0;
            List<string> keyparts = new List<string>(20);
            List<string> values = new List<string>(20);
            List<string> container = keyparts;
            var diff = 0;
            var val = "";
            var index = -2;
            for (int i = 0; i < content.Length; i++)
            {
                char c = content[i];
                if (c == sp_coma)
                {
                    diff = i - s_ix;
                    val = content.Substring(s_ix, diff).Trim();
                    if (!String.IsNullOrEmpty(val))
                    {
                        container.Add(val);
                    }
                    s_ix = i + 1;
                }
                if (c == sp_pipe)
                {
                    diff = i - s_ix;
                    val = content.Substring(s_ix, diff).Trim();
                    if (index != -2)
                    {
                        index = Utilities.Converters.FastParse(val);
                    }
                    else
                    {
                        index = -1;
                    }
                    s_ix = i + 1;
                    container = values;
                }
            }
            var intkeys = new int[keyparts.Count];

            for (int i = 0; i < keyparts.Count; i++)
            {
                intkeys[i] = Utilities.Converters.FastParse(keyparts[i]);
            }

            var cells = new List<int>(values.Count);
            cells.AddRange(values.Select(i => Utilities.Converters.FastParse(i)));
            Save(intkeys, index, cells);
            //var kvp = new KeyValuePair<int, List<int>>(intkeys[0], cells);
            //return kvp;
            _IsLoaded = true;
        }
        public string Content() 
        {
            var sb = new StringBuilder();
            foreach (var item in a) 
            {
                sb.AppendLine(Content(item));
            }
            return sb.ToString();
        }
        public string Content(KeyValuePair<int[],FactLookupValue> kvp) 
        {
            var sb = new StringBuilder();
            foreach (var cell in kvp.Key)
            {
                sb.Append(cell);
                sb.Append(Literals.Coma);
            }
            sb.Append(Literals.PipeSeparator);
            sb.Append(kvp.Value.Index);
            sb.Append(Literals.PipeSeparator);
            foreach (var cell in kvp.Value.CellIndexes)
            {
                sb.Append(cell);
                sb.Append(Literals.Coma);
            }
            return sb.ToString();
        }
        public void Save(int[] key)
        {
            Save(key, -1, new List<int>());
        }
        public void Save(int[] key, List<int> value)
        {
            Save(key, -1, value);
        }
        public void Save(int[] key, int index, List<int> value) 
        {
            if (!a.ContainsKey(key))
            {
                var flv= new FactLookupValue(index == -1 ? (NrMaxItems * this.ID) + a.Count : index, value);
                a.Add(key, flv);
                b.Add(flv.Index, key);
                _IsLoaded = true;
            }
            else 
            {
                a[key].CellIndexes=value;
            }
            IsDirty = true;
        }
        public int GetPageIndex(int ix) 
        {
            var index = ix - IndexStartAt;
            return index;
        }
        private int GetRealIndex(int ix) 
        {
            var index = IndexStartAt + ix;
            return index;
        }
        public bool ContainsKey(int[] key) 
        {
            return a.ContainsKey(key);
        }
        public bool ContainsIndex(int index)
        {
            return b.ContainsKey(index);
        }
        public bool ContainsPageIndex(int pageindex)
        {
            return pageindex > -1 && pageindex < b.Count;
        }
        public List<int> this[int index] 
        {
            get 
            {
                index = GetPageIndex(index);
                if (ContainsPageIndex(index))
                {
                    var item = b[index];
                    return a[item].CellIndexes;
                }
                return null;
            }
        }
        public List<int> this[int[] key]
        {
            get
            {
                return a[key].CellIndexes;
            }
        }

        public int[] Key(int index)
        {
            return b[index];
            //var pageindex = GetPageIndex(index);
            //if (ContainsPageIndex(pageindex))
            //{
            //    var item = b[pageindex];
            //    return item;
            //}
            //return new int[0];
        }
        public int[] GetKeyByPageIndex(int pageindex) 
        {
            return b[pageindex];
        }
        public int Index(int[] key)
        {
            return (a[key].Index);
        }

        public KeyValuePair<int[], FactLookupValue> GetItem(int[] key)
        {
            return new KeyValuePair<int[], FactLookupValue>(key, a[key]);

        }

        public Utilities.KeyValue<int, List<int>> GetKvp(int[] key)
        {
            var item = GetItem(key);
            return new Utilities.KeyValue<int, List<int>>(Index(key), item.Value.CellIndexes);

        }

        public void Clear()
        {
            a.Clear();
            b.Clear();
            _IsLoaded = false;
        }



        internal List<int> GetItem(int pindex)
        {
            return a[b[pindex]].CellIndexes;
        }
    }
    public class HashSetList : ICollection<int> 
    {
        public List<HashSet<int>> Value = new List<HashSet<int>>();



        public void Add(int item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(int item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(int[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get
            {
                var c = 0;
                foreach (var item in Value)
                {
                    c += item.Count;
                }
                return c;
            }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(int item)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<int> IndexesAsEnumerable() 
        {
            foreach (var item in Value) 
            {
                foreach (var i in item) 
                {
                    yield return i;
                }
            }
        }
        public IEnumerator<int> GetEnumerator()
        {
            return this.IndexesAsEnumerable().OrderBy(i=>i).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
    public class FactsPartsDictionary : SharedDictionary<int, HashSet<int>> 
    {
        public void AddIfNotExists(int key, int index) 
        {
            if (!this.ContainsKey(key)) 
            {
                this.Add(key, new HashSet<int>());
            }
            this[key].Add(index);
        }

        public void MoveTo(FactsPartsDictionary target) 
        {
            foreach (var item in this) 
            {
                if (!target.ContainsKey(item.Key)) 
                {
                    target.Add(item.Key, new HashSet<int>());
                }
                target[item.Key].UnionWith(this[item.Key]);
                this[item.Key].Clear();
            }
        }
        //public Dictionary<int, HashSet<int>> FactsDomains = new Dictionary<int, HashSet<int>>();
        //public HashSet<int> AddFactPart(int factpart, int index) 
        //{
        //    if (!this.ContainsKey(factpart)) 
        //    {
        //        this.Add(factpart, new HashSet<int>());
        //    }
        //    var hashset = this[factpart];
        //    hashset.Add(index);
        //    return hashset;
        //}
        //public void AddPart(int domainpart,int factpart, int index) 
        //{
        //    var hashset = AddFactPart(factpart,index);
        //    if (!FactsDomains.ContainsKey(domainpart))
        //    {
        //        FactsDomains.Add(domainpart,new HashSet<int>());
        //    }
        //    var h2 = FactsDomains[domainpart];
        //    if (!h2.Contains(factpart)) 
        //    {
        //        h2.Add(factpart);
        //    }
           
        //}
        //public HashSetList FactsOfDomainX(int key)
        //{
        //    var result = new HashSetList();
        //    var parts = FactsDomains[key];
        //    foreach (var part in parts) 
        //    {
        //        result.Value.Add(this[part]); 
        //    }
        //    return result;
        //}


        //public bool ContainsDomainKey(int key) 
        //{
        //    return this.FactsDomains.ContainsKey(key);
        //}

        //public override string ToString()
        //{
        //    var sb = new StringBuilder();
        //    sb.AppendLine("Facts Of Domains");
        //    foreach (var item in FactsDomains) 
        //    {
        //        sb.AppendLine(String.Format("{0}: {1}", item.Key, item.Value.Count));
        //    }
        //    sb.AppendLine("Facts Of Parts");
        //    foreach (var item in this)
        //    {
        //        sb.AppendLine(String.Format("{0}: {1}", item.Key, item.Value.Count));
        //    }
        //    return sb.ToString();
        //}
    }
    public class RelationDictionary
    {
        
        public static FactsPartsDictionary GetFactsOfParts(Taxonomy taxonomy) 
        {
            var instance = new FactsPartsDictionary();
            instance.DeSerializeItems = (lines) => {
                var values = new HashSet<int>();
                foreach (var line in lines) 
                {
                    values.Add(Utilities.Converters.FastParse(line));
                }
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
            System.IO.File.WriteAllText(GetFilePath(key), sb.ToString());

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
