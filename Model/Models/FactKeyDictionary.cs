using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    public class FactKeyWithCells 
    {
        public int[] FactKey = new int[0];
        public List<int> CellIndexes = new List<int>(1);

        public FactKeyWithCells() 
        {

        }

        public FactKeyWithCells(int[] key)
        {
            this.FactKey = key;
        }
        public FactKeyWithCells(int[] key,List<int> CellIndexes)
        {
            this.FactKey = key;
            this.CellIndexes = CellIndexes;
        }
    }

    public enum PeristenceEnum 
    {
        Initial,
        Loading,
        Loaded,
        Saving,
        Unloading,
        Unloaded,
    
    }

    public class FactKeyDictionary : LogicalModel.Models.IFactDictionary
    {
        public int ID = 0;
        public int NrMaxItems = -1;
        public int IndexStartAt = 0;
        public int IndexEndAt = 0;
        public Dictionary<int, FactKeyWithCells> LookupOfIndexes = new Dictionary<int, FactKeyWithCells>();
        public bool IsDirty = false;
        public PeristenceEnum PeristenceState = PeristenceEnum.Initial;
        //public bool IsPersisting = false;
        //private bool _IsLoaded = false;

        public object Locker = new Object();
        public bool IsLoaded
        {
            get
            {
                return PeristenceState == PeristenceEnum.Loaded || PeristenceState == PeristenceEnum.Initial;
            }
        }
        public FactKeyDictionary()
        {
        }
        public int Count { get { return LookupOfIndexes.Count; } }

        public IEnumerable<int[]> Keys
        {
            get
            {
                return LookupOfIndexes.Values.Select(i=>i.FactKey);
            }
        }
        public Utilities.KeyValue<int, FactKeyWithCells> Save(string content)
        {
            var kv = new Utilities.KeyValue<int, FactKeyWithCells>();
            var sp_pipe = Literals.PipeSeparator[0];
            var sp_coma = Literals.Coma[0];
            /*
            var parts = content.Split(sp_pipe);
            
            var index = Utilities.Converters.FastParse(parts[0]);
            var intkeys = parts[1].Split(sp_coma).Where(i=>!String.IsNullOrEmpty(i)).Select(i => Utilities.Converters.FastParse(i)).ToArray();
            var cells = parts[2].Split(sp_coma).Where(i => !String.IsNullOrEmpty(i)).Select(i => Utilities.Converters.FastParse(i)).ToList();
            */
            var parts = SplitY(content);
            var index = parts[0][0];
            var intkeys = parts[1].ToArray();
            var cells = parts[2];

            Save(intkeys, index,cells);
            kv.Key = index;
            //if (index == 2117299) 
            //{
            //}
            kv.Value = new FactKeyWithCells(intkeys, cells);
            //var kvp = new KeyValuePair<int, List<int>>(intkeys[0], cells);
            //return kvp;
      
            return kv;
        }
        public string Content()
        {
            var sb = new StringBuilder();
            foreach (var item in LookupOfIndexes)
            {
                //if (item.Key == 2117299) 
                //{

                //}
                sb.AppendLine(Content(item));
            }
            return sb.ToString();
        }
        public static string Content(KeyValuePair<int, FactKeyWithCells> kvp)
        {
            var sb = new StringBuilder();
            sb.Append(NrToStr(kvp.Key));
            sb.Append(Literals.PipeSeparator);
            //foreach (var item in kvp.Value.FactKey)
            for (int i=0;i< kvp.Value.FactKey.Length;i++)
            {
                sb.Append(NrToStr(kvp.Value.FactKey[i]));
                if (!(i == kvp.Value.FactKey.Length - 1))
                {
                    sb.Append(Literals.Coma);
                }
            }
            sb.Append(Literals.PipeSeparator);
            for (int i = 0; i < kvp.Value.CellIndexes.Count; i++)
            {
                sb.Append(NrToStr(kvp.Value.CellIndexes[i]));
                if (!(i == kvp.Value.CellIndexes.Count - 1))
                {
                    sb.Append(Literals.Coma);
                }
            }
            //foreach (var item in kvp.Value.CellIndexes)
            //{
            //    sb.Append(NrToStr(item));
            //    sb.Append(Literals.Coma);
            //}
            return sb.ToString();
        }
        public static void Testz() 
        {
            var d = Utilities.Converters.FastParse("   12");
            var d2 = Utilities.Converters.FastParse("123  ");
            var items = new List<string>() 
            {
                "300000    |23647     ,459       ,1668      ,2429      ,12707     ,14646     ,15292     ,17646     ,21076     ,|",
                "5         |23340     ,16150     ,21076     ,21276     ,|6         ,",
                "         4|         2,         1|         9,         2",
            };
            foreach (var item in items) 
            {
                var z = SplitX(item);
            }
        }
        public static List<List<int>> SplitY(string item)
        {
            var result = new List<List<int>>();
            var pad = 10;
            var previx = 0;
            var ix = pad;
            var sp_pipe = Literals.PipeSeparator[0];
            var sp_coma = Literals.Coma[0];
            List<int> container = new List<int>();
            result.Add(container);
            while (ix < item.Length)
            {
                var c = item[ix];
                container.Add(Utilities.Converters.FastParse(item.Substring(previx, pad)));
                previx = ix + 1;
                ix = previx + pad;
                if (c == sp_pipe)
                {
                    container = new List<int>();
                    result.Add(container);
                }
                //if (c == sp_coma)
                //{

                //}
            }
            var val = item.Substring(previx).Trim();
            if (!String.IsNullOrEmpty(val))
            {
                container.Add(Utilities.Converters.FastParse(val));
            }
            return result;
        }
        public static List<List<string>> SplitX(string item) 
        {
            var result = new List<List<string>>();
            var pad = 10;
            var previx = 0;
            var ix = pad;
            var sp_pipe = Literals.PipeSeparator[0];
            var sp_coma = Literals.Coma[0];
            List<String> container = new List<string>();
            result.Add(container);
            while (ix < item.Length) 
            {
                var c = item[ix];
                container.Add(item.Substring(previx,pad));
                previx = ix + 1;
                ix = previx + pad;
                if (c == sp_pipe) 
                {
                    container = new List<string>();
                    result.Add(container);
                }
                //if (c == sp_coma)
                //{

                //}
            }
            var val = item.Substring(previx).Trim();
            if (!String.IsNullOrEmpty(val))
            {
                container.Add(val);
            }
            return result;
        }
        public static string NrToStr(int nr) 
        {
            return nr.ToString().PadRight(10, ' ');
        }


        public int Save(int[] key, int index,List<int> CellIndexes)
        {
            if (!LookupOfIndexes.ContainsKey(index))
            {

                LookupOfIndexes.Add(index, new FactKeyWithCells(key, CellIndexes));
            }
            IsDirty = true;
            return index;
        }

        //public int AddtoDictionary(int[] key, int index, List<int> CellIndexes)
        //{
        //    if (!LookupOfIndexes.ContainsKey(index))
        //    {
        //        if (index == 2117299)
        //        {
        //        }
        //        LookupOfIndexes.Add(index, new FactKeyWithCells(key, CellIndexes));
        //        _IsLoaded = true;
        //    }
        //    IsDirty = true;
        //    return index;
        //}
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

        public bool ContainsIndex(int index)
        {
            return LookupOfIndexes.ContainsKey(index);
        }
        public bool ContainsPageIndex(int pageindex)
        {
            return pageindex > -1 && pageindex < LookupOfIndexes.Count;
        }
        public int[] this[int index]
        {
            get
            {
                var pindex = GetPageIndex(index);
                if (ContainsPageIndex(pindex))
                {
                    var item = LookupOfIndexes[index];
                    return item.FactKey;
                }
                return null;
            }
        }

        public int[] Key(int index)
        {
            return LookupOfIndexes[index].FactKey;
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
            return LookupOfIndexes[pageindex].FactKey;
        }


        public void Clear(Action<string> Log)
        {
            Log("Clearing page" + this.ID.ToString());
            LookupOfIndexes.Clear();
       

        }

        //protected void Log(string item)
        //{
        //    var debug = true;
        //    if (debug)
        //    {
        //        Console.WriteLine(Utilities.Converters.DateTimeToString(DateTime.Now, Utilities.Converters.DateTimeFormat) + ": " + item);
        //    }
        //}

        public bool ContainsKey(int[] key)
        {
            throw new NotImplementedException();
        }

        public int Index(int[] key)
        {
            throw new NotImplementedException();
        }

        public int Save(int[] key, List<int> value)
        {
            throw new NotImplementedException();
        }

        List<int> Models.IFactDictionary.this[int index]
        {
            get { throw new NotImplementedException(); }
        }

        public List<int> this[int[] key]
        {
            get { throw new NotImplementedException(); }
        }


        public int Save(int[] key)
        {
            throw new NotImplementedException();
        }

        public bool IsPersisting { get { return this.PeristenceState == PeristenceEnum.Saving; } }
    }
    
}
