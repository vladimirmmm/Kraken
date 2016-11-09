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
    public class FactKeyDictionary : LogicalModel.Models.IFactDictionary
    {
        public int ID = 0;
        public int NrMaxItems = -1;
        public int IndexStartAt = 0;
        public int IndexEndAt = 0;
        public Dictionary<int, FactKeyWithCells> LookupOfIndexes = new Dictionary<int, FactKeyWithCells>();
        public bool IsDirty = false;
        public bool IsPersisting = false;
        private bool _IsLoaded = false;
        public object Locker = new Object();
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
            var parts = content.Split(sp_pipe);
            var index = Utilities.Converters.FastParse(parts[0]);
            var intkeys = parts[1].Split(sp_coma).Where(i=>!String.IsNullOrEmpty(i)).Select(i => Utilities.Converters.FastParse(i)).ToArray();
            var cells = parts[2].Split(sp_coma).Where(i => !String.IsNullOrEmpty(i)).Select(i => Utilities.Converters.FastParse(i)).ToList();

            
            Save(intkeys, index,cells);
            kv.Key = index;
            //if (index == 2117299) 
            //{
            //}
            kv.Value = new FactKeyWithCells(intkeys, cells);
            //var kvp = new KeyValuePair<int, List<int>>(intkeys[0], cells);
            //return kvp;
            _IsLoaded = true;
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
            sb.Append(kvp.Key);
            sb.Append(Literals.PipeSeparator);
            foreach (var item in kvp.Value.FactKey)
            {
                sb.Append(item);
                sb.Append(Literals.Coma);
            }
            sb.Append(Literals.PipeSeparator);
            foreach (var item in kvp.Value.CellIndexes)
            {
                sb.Append(item);
                sb.Append(Literals.Coma);
            }
            return sb.ToString();
        }

        public int Save(int[] key, int index,List<int> CellIndexes)
        {
            if (!LookupOfIndexes.ContainsKey(index))
            {
                //if (index == 2117299) 
                //{ 
                //}
                LookupOfIndexes.Add(index, new FactKeyWithCells(key, CellIndexes));
                _IsLoaded = true;
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


        public void Clear()
        {
            Log("Clearing page" + this.ID.ToString());
            LookupOfIndexes.Clear();
            _IsLoaded = false;
       

        }

        protected void Log(string item)
        {
            var debug = true;
            if (debug)
            {
                Console.WriteLine(Utilities.Converters.DateTimeToString(DateTime.Now, Utilities.Converters.DateTimeFormat) + ": " + item);
            }
        }

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
    }
    
}
