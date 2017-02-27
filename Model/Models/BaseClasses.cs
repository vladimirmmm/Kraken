﻿using BaseModel;
using LogicalModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace LogicalModel.Base
{
    public class nString
    {
        public string Value = "";
        public nString() { }
        public nString(string val)
        {
            this.Value = val;
        }

        public static string operator /(nString item1, String item2)
        {
            return item1.Value;
        }

    }
    /*
    public class IndexDictionaryCollection : Dictionary<int, IndexDictionary2>
    {
        public Func<int, int> DomainIndexAccessor = (i) => -1;

        public HashSet<int> GetFacts(int key)
        {
            if (!this.ContainsKey(key))
            {
                var domainkey = DomainIndexAccessor(key);
                if (this.ContainsKey(domainkey))
                {
                    IndexDictionary2 item = base[domainkey];
                    return item.ContainsKey(key) ? item[key] : new HashSet<int>();

                }
                return new HashSet<int>();
            }
            else
            {
                var itemx = new HashSet<int>();
                foreach (var id in base[key])
                {
                    itemx.UnionWith(id.Value);
                }
                return itemx;
            }


        }
    }
    */
    /*
    public class IndexDictionary2 : Dictionary<int, HashSet<int>> 
    {
        int maxitemnr = 0;

        public IndexDictionary2() { }
        public IndexDictionary2(int maxnr) 
        {
            this.maxitemnr = maxnr;
        }
        List<int> loaded = new List<int>();
        public Func<int, string> AccessHashset = (i) => "";
        public Action<int,string> SaveHashSet = (k,s) => { };
        public void Add(int key, int value) 
        {
            if (!this.ContainsKey(key)) 
            {
                this.Add(key, new HashSet<int>());
                LoadKey(key);
            }
            this[key].Add(value);
        }
        public void LoadKey(int key)
        {
            loaded.Add(key);
            if (loaded.Count > maxitemnr && maxitemnr!=0)
            {
                Unload(loaded.FirstOrDefault());
            }
        }
        public new HashSet<int> this[int key]
        {
            get
            {
                if (base[key] == null) 
                {
                    Console.WriteLine("IndexDictionary Loading " + key);

                    var str = AccessHashset(key);
                    var items = str.Split(new string[] { Literals.NewLine  },StringSplitOptions.RemoveEmptyEntries);
                    var h = new HashSet<int>();
                    foreach (var item in items) 
                    {
                        h.Add(Utilities.Converters.FastParse(item));
                    }
                    base[key] = h;
                    LoadKey(key);
                  
                }
                return base[key];
            }
            set 
            {
                base[key] = value;
            }
        }

        public void Unload(int key) 
        {
            Console.WriteLine("IndexDictionary Unloading " + key);
            var sb = new StringBuilder();
            foreach (int item in this[key]) 
            {
                sb.AppendLine(item.ToString());
            }
            SaveHashSet(key, sb.ToString());
            loaded.Remove(key);
            this[key] = null;

        }

    }
    */
    /*
    public class FactDictionary : Dictionary<int[], List<int>>
    {
        public FactDictionary(IEqualityComparer<int[]> comparer)
            : base(comparer)
        {

        }
        public void AddKvp(KeyValuePair<int[], List<int>> kvp)
        {
            if (!this.ContainsKey(kvp.Key))
            {
                AddItem(kvp.Key, kvp.Value);
            }
        }
        public void AddItem(int[] key, List<int> value)
        {
            if (!this.ContainsKey(key))
            {
                this.Add(key, value);
            }
        }


        public void LoadValues(Func<int[],List<int>> loader)
        {
            foreach(var key in this.Keys)
            {
                this[key]=loader(key);
            }

        }

        public void Unload() 
        {
            foreach (var key in this.Keys)
            {
                this[key] = null;
            }
            IsLoaded = false;
        }

        private bool _IsLoaded = false;
        public bool IsLoaded { get; set; }
    }
    */
    /*
    public class FactDictionary2 : Dictionary<int, List<int>>
    {
        public FactDictionary2()
            : base()
        {

        }
        public void AddKvp(KeyValuePair<int, List<int>> kvp)
        {
            if (!this.ContainsKey(kvp.Key))
            {
                AddItem(kvp.Key, kvp.Value);
            }
        }
        public void AddItem(int key, List<int> value)
        {
            if (!this.ContainsKey(key))
            {
                this.Add(key, value);
            }
        }


        public void LoadValues(Func<int, List<int>> loader)
        {
            foreach (var key in this.Keys)
            {
                this[key] = loader(key);
            }

        }

        public void Unload()
        {
            foreach (var key in this.Keys)
            {
                this[key] = null;
            }
            IsLoaded = false;
        }

        private bool _IsLoaded = false;
        public bool IsLoaded { get; set; }
    }

    */

    public class QualifiedItem : QualifiedName 
    {
        private SimpleLabel _Label = null;
        [JsonProperty]
        public SimpleLabel Label
        {
            get
            {
                return _Label;
            }
            set
            {
                _Label = value;
            }
        }

        public string LabelContent
        {
            get 
            {
                if (_Label != null) 
                {
                    return Label.Content;
                }
                return "";
            }
        }

        public string LabelCode
        {
            get
            {
                if (_Label != null)
                {
                    return Label.Code;
                }
                return "";
            }
        }

        private string _Role = "";
        [JsonProperty]
        [DefaultValue("")]
        public string Role
        {
            get
            {
                return _Role;
            }
            set
            {
                _Role = value;
            }
        }
    }
    /*
      public class QualifiedItem : QualifiedName, ILabeled 
    {
        private Label _Label = null;
        [JsonProperty]
        public Label Label
        {
            get
            {
                return _Label;
            }
            set
            {
                _Label = value;
            }
        }

        public string LabelContent
        {
            get 
            {
                if (_Label != null) 
                {
                    return Label.Content;
                }
                return "";
            }
        }

        public string LabelCode
        {
            get
            {
                if (_Label != null)
                {
                    return Label.Code;
                }
                return "";
            }
        }

        private string _LabelID = "";
        public string LabelID
        {
            get
            {
                return _LabelID;
            }
            set
            {
                _LabelID = value;
            }
        }

        private string _Role = "";
        [JsonProperty]
        public string Role
        {
            get
            {
                return _Role;
            }
            set
            {
                _Role = value;
            }
        }
    }
    
     */
    /*
    public class FactFilter 
    {
        public Boolean Positive = true;
        public string Representation = "";
        public Func<string,int[], bool> Filter = (keystring,key) => true;
        public int Level = 0; 
        //0 - dimensiondomian
        //1 - dimensiondomainmember

        public FactFilter() 
        {
      
        }

        public FactFilter(Boolean Positive, string Representation, int Level, Func<string, int[], bool> Filter)
        {
            this.Positive = Positive;
            this.Representation = Representation;
            this.Level = Level;
            this.Filter = Filter;
        }

        public override string ToString()
        {
            return String.Format("[{1}] {0}{2}", Positive ? "" : "!", Level, Representation);
        }
    }
    /*
    /*
    public class FactBaseQuery2
    {
        public List<FactBaseQuery2> ChildQueries = new List<FactBaseQuery2>();
        public List<FactFilter> Filters = new List<FactFilter>();
        public Func<string,int[], bool> _Filter = (s,k) => true;

        public Func<string, int[], bool> GetFilter() 
        {
            Func<string, int[], bool> filter = null;
            var filters = new List<FactFilter>();
            if (ChildQueries.Count > 0)
            {
                var results = new List<KeyValue<string, int>>();
                foreach (var childquery in ChildQueries)
                {
                    filters.AddRange(childquery.Filters);
                }
            }
            return filter;
        }

        public Boolean Filter(string factstring, int[] factintkey) 
        {
            var result = true;
            result = _Filter(factstring, factintkey);
            return result;
        }

        public List<String> ToList(IQueryable<String> queryable)
        {
            return queryable.Where(i => Filter(i,new int[]{})).ToList();
        }
        public List<int> ToIndexList(IQueryable<int> queryable)
        {
            int[] keys =new int[]{};
            return queryable.Where(i => Filter("", keys)).ToList();
        }
        public List<int[]> ToIntKeyList(IQueryable<int[]> queryable)
        {
            return queryable.Where(i => Filter("", i)).ToList();
        }

    }
    */
    public class FactBaseQuery 
    {
        public string FalseFilters = "";
        public string TrueFilters = "";
        public string DictFilters = "";
        public List<int> DictFilterIndexes = new List<int>();
        public List<int> NegativeDictFilterIndexes = new List<int>();
        public bool Cover = false;
        public string Concept = "";
        public Func<string, bool> Filter = (s) => true;
        public int NrOfDictFilters = 0;
        protected FactBaseQuery Parent = null;
        protected List<FactBaseQuery> ChildQueries = new List<FactBaseQuery>();
        private List<FactPoolQuery> _Pools = new List<FactPoolQuery>();
        public List<FactPoolQuery> Pools 
        {
            get 
            {
                if (_Pools.Count == 0)
                {
                    _Pools= ChildQueries.Where(i => i is FactPoolQuery).Select(i => i as FactPoolQuery).ToList();
                    
                }
                return _Pools;
            }
        }

        public FactBaseQuery() 
        {
           
        }

        public void AddChildQuery(FactBaseQuery qry) 
        {
            if (qry != this)
            {
                qry.Parent = this;
                if (!ChildQueries.Contains(qry))
                {
                    ChildQueries.Add(qry);
                }
            }
        }
     
        public FactBaseQuery(List<FactBaseQuery> queries)
        {
            this.ChildQueries.AddRange(queries);
        }
        public void AddIndex(int ix, bool isnegative) 
        {
            if (isnegative) 
            {
                this.NegativeDictFilterIndexes.Add(ix);
            }
            else
            {
                this.DictFilterIndexes.Add(ix);
            }

        }
        public bool IsEmpty() 
        {
            return this.DictFilterIndexes.Count == 0 && this.NegativeDictFilterIndexes.Count == 0;
        }

        public bool HasFilters() 
        {
            var s = GetCompareString();
            return !String.IsNullOrEmpty(s.Trim());
        }
        public bool HasDictFilter(string dictfilter) 
        {
            if (DictFilters.Contains(dictfilter)) 
            {
                return true;
            }
            foreach (var q in ChildQueries) 
            {
                if (q.HasDictFilter(dictfilter)) 
                {
                    return true;
                }
            }
            return false;
        }
        public List<String> ToList(IQueryable<String> queryable) 
        {
            return queryable.Where(i => Filter(i)).ToList();
        }

        public string GetConcept() 
        {
            //rec todo
            if (!String.IsNullOrEmpty(Concept)) { return Concept; }
            foreach (var childquery in ChildQueries)
            {
                var concept = childquery.GetConcept();
                if (!String.IsNullOrEmpty(concept)) 
                {
                    return concept;
                }
            }
            return "";
        }
        public List<int> GetAspects(Taxonomy taxonomy) 
        {
            var aspects = new List<int>();
            aspects.AddRange(this.DictFilterIndexes);
            //TODO: make rescursive
            foreach (var pool in this.Pools) 
            {
                aspects.AddRange(pool.DictFilterIndexes);
            }
            aspects = aspects.Distinct().ToList();
            aspects = aspects.Select(i => taxonomy.DimensionDomainsOfMembers.ContainsKey(i) ? taxonomy.DimensionDomainsOfMembers[i] : i).ToList();
            return aspects;
        }

        public List<string> GetDimensions()
        {
            var filterparts = new List<string>();
            if (ChildQueries.Count > 0)
            {
                var commonchildfilters = new List<string>();

                var isfilteradded = false;
                foreach (var childquery in ChildQueries)
                {
                    var dimensions = childquery.GetDimensions();
                    if (!isfilteradded)
                    {
                        isfilteradded = true;
                        commonchildfilters.AddRange(dimensions);
                    }
                    else
                    {
                        commonchildfilters = commonchildfilters.Intersect(dimensions).ToList();
                    }
                }
                filterparts.AddRange(commonchildfilters);
            }

            filterparts.AddRange(DictFilters.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries));
            

            //var conceptfilterparts = filterparts.Where(i => i.IndexOf("]") == -1).ToList();
            //var dimparts = filterparts.Where(i => i.IndexOf("]") > -1).ToList();
            return filterparts.ToList();

        }
        public static void BaseMerge(FactBaseQuery source, FactBaseQuery target)
        {
            target.DictFilterIndexes.AddRange(source.DictFilterIndexes);
            target.NegativeDictFilterIndexes.AddRange(source.NegativeDictFilterIndexes);

        }
        public static void Merge(FactBaseQuery source, FactBaseQuery target)
        {
            target.TrueFilters = target.TrueFilters + source.TrueFilters;
            target.FalseFilters = target.FalseFilters + source.FalseFilters;
            target.DictFilters = target.DictFilters + source.DictFilters;
            target.DictFilterIndexes.AddRange(source.DictFilterIndexes);
            target.NegativeDictFilterIndexes.AddRange(source.NegativeDictFilterIndexes);
            target.ChildQueries = source.ChildQueries;
            var originalfilter = target.Filter;
            target.Filter = null;
            target.Cover = source.Cover;
            target.Filter = (s) => originalfilter(s) && source.Filter(s);

        }
        public static FactBaseQuery GetCommonQuery(params FactBaseQuery[] queries) 
        {
            if (queries.Length==0){return null;}
            //if (queries.Length==1)
            //{
            //    return queries[0];
            //}
            var result = queries[0];

            for (int i=1;i<queries.Length;i++) 
            {
                result = FactBaseQuery.GetCommon(result, queries[i]);
            }

            return result;
        }

        public static FactBaseQuery GetCommon(FactBaseQuery a, FactBaseQuery b) 
        {
            var common_dfs = a.DictFilterIndexes.Intersect(b.DictFilterIndexes).ToList();
            var common_ndfs = a.NegativeDictFilterIndexes.Intersect(b.NegativeDictFilterIndexes).ToList();
            var qry = new FactBaseQuery();
            qry.DictFilterIndexes = common_dfs;
            qry.NegativeDictFilterIndexes = common_ndfs;
            foreach (var achildquery in a.ChildQueries) 
            {
                var acs = achildquery.GetCompareString();
                var bchildquery = b.ChildQueries.FirstOrDefault(i => i.GetCompareString() == acs);
                if (bchildquery != null) 
                {
                    qry.ChildQueries.Add(achildquery.Copy());
                }
            }
            return qry;
            
        }
        public FactBaseQuery Copy() 
        {
            var result = new FactBaseQuery();
            result.DictFilterIndexes = this.DictFilterIndexes.ToArray().ToList();
            result.NegativeDictFilterIndexes = this.NegativeDictFilterIndexes.ToArray().ToList();
            foreach (var childquery in ChildQueries) 
            {
                result.AddChildQuery(childquery.Copy());
            }
            return result;
        }
        public static void RemoveQuery(FactBaseQuery from,FactBaseQuery item)
        {
            from.DictFilterIndexes = from.DictFilterIndexes.Except(item.DictFilterIndexes).ToList();
            from.NegativeDictFilterIndexes = from.NegativeDictFilterIndexes.Except(item.NegativeDictFilterIndexes).ToList();
            var childqueries = from.ChildQueries.ToArray().ToList();
            foreach (var childquery in childqueries) 
            {
                var itemcs = childquery.GetCompareString();

                var bchildquery = item.ChildQueries.FirstOrDefault(i => i.GetCompareString() == itemcs);
                if (bchildquery != null) 
                {
                    from.ChildQueries.Remove(childquery);
                }
 
            }
        }
        public static void MergeQueries(FactBaseQuery target, FactBaseQuery source) 
        {
            target.DictFilterIndexes = target.DictFilterIndexes.Concat(source.DictFilterIndexes).Distinct().ToList();
            target.NegativeDictFilterIndexes = target.NegativeDictFilterIndexes.Concat(source.NegativeDictFilterIndexes).Distinct().ToList();
            foreach (var sourcechild in source.ChildQueries) 
            {
                var sourcechildcs = sourcechild.GetCompareString();
                var targetchild = target.ChildQueries.FirstOrDefault(i => i.GetCompareString() == sourcechildcs);
                if (targetchild == null) 
                {
                    target.ChildQueries.Add(sourcechild);
                }
            }
        }
        public IEnumerable<String> ToQueryable(IEnumerable<String> queryable)
        {
            var result = queryable.Where(i => Filter(i));
            if (ChildQueries.Count > 0)
            {
                var items = new List<string>();
                foreach (var childquery in ChildQueries) 
                {
                    items.AddRange(childquery.ToQueryable(result));
                }
                return items;
            }
            return result;
        }
        
        public List<KeyValue<string, int>> ToList(List<KeyValue<string, int>> queryable)
        {
            //var mainitems = new List<KeyValue<string, int>>();
            var items = new List<KeyValue<string, int>>();

            var queryablecount = queryable.Count();
            var sb = new StringBuilder();
            for (int i = 0; i < queryablecount; i++) 
            {
                var str = queryable[i].Key;

                if (Filter(str))
                {
                    items.Add(queryable[i]);
                }
                else 
                {
                    var intkey = TaxonomyEngine.CurrentEngine.CurrentTaxonomy.FactsManager.GetFactKey(queryable[i].Value);
                    var x = TaxonomyEngine.CurrentEngine.CurrentTaxonomy.GetFactStringKey(intkey);
                    sb.AppendLine(x);
                }
            
            }
            if (items.Count == 0)
            {

            }
            if (ChildQueries.Count > 0)
            {
                var results = new List<KeyValue<string, int>>(); 
                foreach (var childquery in ChildQueries)
                {
                    results.AddRange(childquery.ToList(items));
                }
                return results;
            }
            return items;
        }

        public IList<int> ToList(Taxonomy taxonomy, IList<int> facts,bool ensurepartnr=false)
        {
            //cover here
            var partnr = Cover ? ensurepartnr ? NrOfDictFilters : 0 : 0;
            var items = taxonomy.SearchFactsGetIndex3(DictFilterIndexes.ToArray(), taxonomy.FactsOfParts, facts, partnr);  
            foreach (var negativeindex in NegativeDictFilterIndexes)
            {
                if (taxonomy.FactsOfParts.ContainsKey(negativeindex))
                {
                    items = Utilities.Objects.SortedExcept(items, taxonomy.FactsOfParts[negativeindex]);
                }
            }
            if (ChildQueries.Count > 0)
            {
                var result = new List<int>();

                foreach (var childquery in ChildQueries)
                {
                    result.AddRange(childquery.ToList(taxonomy, items));
                }
                return result;
            }

            return items;
        }

        public IList<int> ToIntervalList(FactsPartsDictionary FactsOfParts, IList<int> facts, bool ensurepartnr = false)
        {
            //cover here
            var items = FactsOfParts.SearchFactsIndexByKey(DictFilterIndexes.ToArray(), facts);
            foreach (var negativeindex in NegativeDictFilterIndexes)
            {
                if (FactsOfParts.ContainsKey(negativeindex))
                {
                    items = Utilities.Objects.SortedExcept(items, FactsOfParts[negativeindex]);
                }
            }
            if (ChildQueries.Count > 0)
            {
                var result = new IntervalList();

                foreach (var childquery in ChildQueries)
                {
                    result.AddRange(childquery.ToIntervalList(FactsOfParts, items));
                }
                return result;
            }

            return items;
        }
        private int _DictFilterCount = -1;
        public int GetDictFilterCount()
        {

            int limit = 100;
            int counter = 0;
            var current = this;
            var result = 0;
            while (current != null && counter < limit)
            {
                result += current.DictFilterIndexes.Count;
                counter++;
                current = current.Parent;
            }
            _DictFilterCount = result;

            return _DictFilterCount;
        }
        public IEnumerable<FactBaseQueryResult> EnumerateResults(FactsPartsDictionary FactsOfParts, int ix, IList<int> data,FactBaseQuery currentquery,bool skipinitialfiltering=false)
        {
            var items = data;
            //CurrentLength = GetDictFilterCount();
            if (ix == 0)
            {
                currentquery = new FactBaseQuery();
                currentquery.DictFilterIndexes = this.DictFilterIndexes;
                if (DictFilterIndexes.Count > 0 && !skipinitialfiltering)
                {
                    items = FactsOfParts.SearchFactsIndexByKey(DictFilterIndexes.ToArray(), data);
                }
                foreach (var negativeindex in NegativeDictFilterIndexes)
                {
                    if (FactsOfParts.ContainsKey(negativeindex))
                    {
                        items = Utilities.Objects.SortedExcept(items, FactsOfParts[negativeindex]);
                    }
                }
            }

            if (Pools.Count == 0)
            {
                var result = new FactBaseQueryResult();
                result.Items = items;
                result.Query = currentquery;
                yield return result;
                yield break;
            }

            if (ix == Pools.Count)
            {
                var result = new FactBaseQueryResult();
                result.Items = data;
                result.Query = currentquery;
                yield return result;
            }
            else
            {
                foreach (var qry in Pools[ix].ChildQueries)
                {
                    var subresult = qry.ToIntervalList(FactsOfParts, items);
                    var subquery = new FactBaseQuery();
                    FactBaseQuery.BaseMerge(currentquery, subquery);
                    FactBaseQuery.BaseMerge(qry, subquery);
                    foreach (var x in EnumerateResults(FactsOfParts, ix + 1, subresult, subquery))
                    {
                        yield return x;
                    }
                }
            }
        }
        

        public IEnumerable<IList<int>> EnumerateIntervals(FactsPartsDictionary FactsOfParts, int ix, IList<int> data,bool ensureKeyCount)
        {
            var items = data;
            //CurrentLength = GetDictFilterCount();
            if (ix == 0) 
            {
                if (DictFilterIndexes.Count > 0)
                {
                    items = FactsOfParts.SearchFactsIndexByKey(DictFilterIndexes.ToArray(), data);
                }
                foreach (var negativeindex in NegativeDictFilterIndexes)
                {
                    if (FactsOfParts.ContainsKey(negativeindex))
                    {
                        items = Utilities.Objects.SortedExcept(items, FactsOfParts[negativeindex]);
                    }
                }
            }
       
            if (Pools.Count == 0) 
            {
                yield return items;
                yield break;
            }

            if (ix == Pools.Count)
            {
                yield return data;
            }
            else
            {
                foreach (var qry in Pools[ix].ChildQueries)
                {
                    var subresult = qry.ToIntervalList(FactsOfParts, items);
                    foreach (var x in EnumerateIntervals(FactsOfParts, ix + 1, subresult, ensureKeyCount))
                    {
                        yield return x;
                    }
                }
            }
        }
        
        public string GetString(Taxonomy taxonomy,string pad="")
        {
            var sb = new StringBuilder();
            foreach (var ix in DictFilterIndexes)
            {
                sb.Append(taxonomy.CounterFactParts[ix] + ", ");
            }
            sb.Append("; ");
            if (NegativeDictFilterIndexes.Count > 0)
            {
                sb.Append("!:");

                foreach (var ix in NegativeDictFilterIndexes)
                {
                    sb.Append(taxonomy.CounterFactParts[ix] + ", ");
                }
                sb.AppendLine(";");

            }
            foreach (var child in ChildQueries)
            {
                sb.AppendLine(child.GetString(taxonomy, Literals.Tab + pad));
            }
            return sb.ToString();
        }
        public string GetCompareString(string pad = "")
        {
            var sb = new StringBuilder();
            if (DictFilterIndexes.Count > 0)
            {
                sb.Append("[");
                foreach (var ix in DictFilterIndexes)
                {
                    sb.Append(ix + ", ");
                }
                sb.Append("]");
            }
            if (NegativeDictFilterIndexes.Count > 0)
            {
                sb.Append("![");

                foreach (var ix in NegativeDictFilterIndexes)
                {
                    sb.Append(ix + ", ");
                }
                sb.AppendLine("]");

            }
            foreach (var child in ChildQueries)
            {
                sb.AppendLine(child.GetCompareString(Literals.Tab + pad));
            }
            return sb.ToString();
        }

        public static FactBaseQuery GetQuery(string filterstring, Dictionary<string, int> factparts) 
        {
            var factstrings = filterstring.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            //TODO
            IList<int> idlist = new List<int>();
            var keys = new List<int>();
            var qry = new LogicalModel.Base.FactBaseQuery();
            foreach (var fs in factstrings)
            {
                var ors = fs.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                if (ors.Length == 1)
                {
                    var oritem = ors.FirstOrDefault();
                    var isnegative = false;
                    if (oritem.StartsWith("!"))
                    {
                        isnegative = true;
                        oritem = oritem.Substring(1);
                    }
                    oritem = oritem.Trim();
                    if (factparts.ContainsKey(oritem))
                    {
                        qry.AddIndex(factparts[oritem], isnegative);
                    }
                    else
                    {
                        var partkeys = factparts.Keys.Where(i => i.IndexOf(oritem, StringComparison.InvariantCultureIgnoreCase) > -1).ToList();
                        if (partkeys.Count > 0)
                        {
                            if (isnegative)
                            {
                                foreach (var partkey in partkeys)
                                {
                                    var partqry = new LogicalModel.Base.FactBaseQuery();
                                    qry.AddIndex(factparts[partkey], isnegative);
                                }
                            }
                            else
                            {
                                var qrypool = new LogicalModel.Base.FactPoolQuery();
                                foreach (var partkey in partkeys)
                                {
                                    var partqry = new LogicalModel.Base.FactBaseQuery();
                                    partqry.AddIndex(factparts[partkey], isnegative);
                                    qrypool.AddChildQuery(partqry);
                                }
                                qry.AddChildQuery(qrypool);
                            }


                        }
                        else
                        {
                            qry.DictFilterIndexes.Add(-1);
                        }

                    }
                }
                else
                {
                    var qrypool = new LogicalModel.Base.FactPoolQuery();
                    var isnegative = false;

                    for (int i = 0; i < ors.Length; i++)
                    {
                        var oritem = ors[i];
                        if (oritem.StartsWith("!"))
                        {
                            isnegative = true;
                            oritem = oritem.Substring(1);
                        }
                        oritem = oritem.Trim();
                        LogicalModel.Base.FactBaseQuery subquery = null;
                        if (factparts.ContainsKey(oritem))
                        {
                            subquery = new LogicalModel.Base.FactBaseQuery();
                            subquery.AddIndex(factparts[oritem], isnegative);
                        }
                        else
                        {

                            var partkeys = factparts.Keys.Where(k => k.IndexOf(oritem, StringComparison.InvariantCultureIgnoreCase) > -1).ToList();
                            if (partkeys.Count > 0 && !String.IsNullOrEmpty(oritem))
                            {
                                if (isnegative)
                                {
                                    subquery = new LogicalModel.Base.FactBaseQuery();

                                    foreach (var partkey in partkeys)
                                    {
                                        var partqry = new LogicalModel.Base.FactBaseQuery();
                                        subquery.AddIndex(factparts[partkey], isnegative);
                                    }
                                }
                                else
                                {
                                    subquery = new LogicalModel.Base.FactPoolQuery();
                                    foreach (var partkey in partkeys)
                                    {
                                        var partqry = new LogicalModel.Base.FactBaseQuery();
                                        partqry.AddIndex(factparts[partkey], isnegative);
                                        subquery.AddChildQuery(partqry);
                                    }
                                }



                            }
                            else
                            {
                                qry.DictFilterIndexes.Add(-1);
                            }
                        }
                        qrypool.AddChildQuery(subquery);
                    }
                    qry.AddChildQuery(qrypool);
                }




            }
            if (!qry.HasFilters()) { return null; } else 
            {
                return qry;
            }
        }

        public override string ToString()
        {
            return GetString(TaxonomyEngine.CurrentEngine.CurrentTaxonomy);
            //var thisstr = String.Format("DictFilters: {0}\n FalseFilters: {2}\n TrueFilters: {1}\n", DictFilters, TrueFilters, FalseFilters);
            //var sb = new StringBuilder();
            //sb.Append(thisstr);
            //foreach (var child in ChildQueries) 
            //{
            //    sb.Append(">" + child.ToString());
            //}
            //return sb.ToString();
        }
    }

    public class FactPoolQuery:FactBaseQuery
    {

        //public IEnumerable<IList<int>> EnumerateIntervals(Taxonomy taxonomy, int ix, IList<int> data)
        //{
        //    if (ix == Items.Count) { yield return data; }
        //    foreach (var qry in Items[ix])
        //    {
        //        var subresult = qry.ToIntervalList(taxonomy, data);
        //        EnumerateIntervals(taxonomy, ix + 1, subresult);
        //    }
        //}
    }

    public class FactBaseQueryResult 
    {
        public FactBaseQuery Query = null;
        public IList<int> Items = null;
    }

    public class FactBase 
    {

        [JsonIgnore]
        public Concept Concept { get; set; }

        private List<Dimension> _Dimensions = new List<Dimension>();
        [JsonIgnore]
        public List<Dimension> Dimensions { get { return _Dimensions; } set { _Dimensions = value; } }

        internal string _FactString = "";
        
        [JsonProperty]
        public virtual string FactString
        {
            get 
            {
                if (String.IsNullOrEmpty(_FactString))
                {
                    _FactString = GetFactString();
                }
                return _FactString;
            }
            set
            {
                _FactString = value;
                SetFromString(value);
            }
        }
        
        public static FactBase GetFactFrom(string factstring)
        {
            var fact = new FactBase();
            fact.FactString = factstring;
            return fact;
        }
        public static void MergeFact(FactBase target, FactBase source)
        {
            if (target.Concept == null) 
            {
                target.Concept = source.Concept;
            }
            Dimension.MergeDimensions(target.Dimensions, source.Dimensions);
        }
        
        public void SetFactString()
        {
            Dimensions = Dimensions.OrderBy(i => i.DomainMemberFullName, StringComparer.Ordinal).ToList();
            _FactString = "";
        }
        
        public string GetFactString() 
        {
            var sb = new StringBuilder();
            if (Concept != null)
            {
                sb.Append(Concept + ",");//>
            }
            var lastdimns = "";
            foreach (var dimension in Dimensions)
            {
                var dimstr = dimension.DomainMemberFullName.Trim();
                sb.Append(Format(dimstr, ref lastdimns) + ",");
            }
            return sb.ToString();
        }

        public void ClearFactKey()
        {
            _FactKey = "";
        }
        private string _FactKey = "";
        
        public string GetFactKey()
        {
            if (this.Dimensions.Count == 0 && this.Concept == null)
            {
                LoadObjects();
            }
            if (String.IsNullOrEmpty(_FactKey))
            {
                var sb = new StringBuilder();
                if (Concept != null)
                {
                    sb.Append(Concept + ",");
                }
                //var lastdimns = "";
                foreach (var dimension in Dimensions)
                {
                    var dimstr = dimension.ToStringForKey().Trim();
                    sb.Append(dimstr + ",");
                    //sb.Append(Format(dimstr, ref lastdimns) + ",");
                }
                _FactKey = sb.ToString();
                //ClearObjects();
            }

            return _FactKey;

        }

        private void test() 
        {
            var fact = new LogicalModel.Base.FactBase();
            //fact.SetFromString("eba_met:mi235,[eba_dim:BAS]eba_BA:x9,[eba_dim:LEC]eba_typ:LE,[eba_dim:MCY]eba_MC:x27,[eba_dim:TRI]eba_TR:x6,");
            fact.SetFromString("eba_met:mi235,[eba_dim:BAS]eba_BA:x9,[*:LEC]eba_typ:LE:1234,[*:MCY]eba_MC:x27,[*:TRI]eba_TR:x6,");
            var k1 = fact.GetFactKey();
            var s1 = fact.GetFactString();
            fact.SetFromString("eba_met:mi235,[eba_dim:BAS]eba_BA:x9,[eba_dim:LEC]eba_typ:LE:1234,[eba_dim:MCY]eba_MC:x27,[eba_dim:TRI]eba_TR:x6,");
            var k2 = fact.GetFactKey();
            var s2 = fact.GetFactString();
        }

        private static string Format(string item, ref string lastdimns)
        {
            var dimns = item.TextBetween("[", ":");
            if (lastdimns != dimns)
            {
                lastdimns = dimns;
            }
            else
            {
                //item = item.Replace(dimns, "*");

            }
            return item;

        }
        public void SetFromStringStable(string item)
        {
            this.Dimensions.Clear();
            this.Concept = null;
            var parts = item.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            //var parts = Utilities.Strings.FactSplit(item, ',', 8);
            var toskip = 0;
            if (parts.Length > 0)
            {
                if (parts[0].IndexOf("[") == -1)
                {
                    toskip = 1;
                    var concept = new Concept();
                    concept.Content = parts[0];
                    this.Concept = concept;
                }
            }
            var dimparts = parts.Skip(toskip).ToList();
            foreach (var dimpart in dimparts)
            {
                var dimitem = Utilities.Strings.TextBetween(dimpart, "[", "]");
                var domainpart = dimpart.Substring(dimitem.Length + 2);

                var domain = domainpart;
                var member = "";
                var dim = new Dimension();

                if (domainpart.Contains(":"))
                {
                    var domainparts = domainpart.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                    switch (domainparts.Length)
                    {
                        case 2:
                            dim.IsTyped = Taxonomy.IsTyped(domain);
                            if (dim.IsTyped)
                            {
                                domain = domainpart;
                            }
                            else
                            {
                                domain = domainparts[0];
                                member = domainparts[1];
                            }
                            break;
                        case 3:
                            domain = String.Format("{0}:{1}", domainparts[0], domainparts[1]);
                            member = domainparts[2];
                            dim.IsTyped = true;
                            break;
                        default:
                            break;
                    }

                }
                dim.DimensionItem = dimitem;
                dim.Domain = domain;
                dim.DomainMember = member;
                this.Dimensions.Add(dim);
            }
            this._FactString = item;
            this._FactKey = "";
        }
        public void SetFromString(string item)
        {
            this.Dimensions.Clear();
            this.Concept = null;
            //var parts = item.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var parts = Utilities.Strings.FactSplit(item, ',', 8);
            var toskip = 0;
            if (parts.Count > 0)
            {
                if (parts[0].IndexOf("[") == -1)
                {
                    toskip = 1;
                    var concept = new Concept();
                    concept.Content = parts[0];
                    this.Concept = concept;
                }
            }
            var dimparts = parts.Skip(toskip).ToList();
            foreach (var dimpart in dimparts)
            {
                var dimitem = Utilities.Strings.TextBetween(dimpart, "[", "]");
                var domainpart = dimpart.Substring(dimitem.Length + 2);

                var domain = domainpart;
                var member = "";
                var dim = new Dimension();

                if (domainpart.Contains(":"))
                {
                    var domainparts = domainpart.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                    switch (domainparts.Length)
                    {
                        case 2:
                            dim.IsTyped = Taxonomy.IsTyped(domain);
                            if (dim.IsTyped)
                            {
                                domain = domainpart;
                            }
                            else
                            {
                                domain = domainparts[0];
                                member = domainparts[1];
                            }
                            break;
                        case 3:
                            domain = String.Format("{0}:{1}", domainparts[0], domainparts[1]);
                            member = domainparts[2];
                            dim.IsTyped = true;
                            break;
                        default:
                            break;
                    }
                    
                }
                dim.DimensionItem = dimitem;
                dim.Domain = domain;
                dim.DomainMember = member;
                this.Dimensions.Add(dim);
            }
            this._FactString = item;
            this._FactKey = "";
        }


        public void SetFromStringOld(string item) 
        {
            this.Dimensions.Clear();
            this.Concept = null;
            var parts = item.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var toskip = 0;
            if (parts.Length > 0) 
            {
                if (parts[0].IndexOf("[")==-1) 
                {
                    toskip = 1;
                    var concept = new Concept();
                    concept.Content = parts[0];
                    this.Concept = concept;
                }
            }
            var dimparts = parts.Skip(toskip).ToList();
            var lastdimns = "";
            foreach (var dimpart in dimparts)
            {
                var dimitem = Utilities.Strings.TextBetween(dimpart, "[", "]");
                var domainpart = dimpart.Substring(dimitem.Length + 2);

                var dimitemns = dimitem.Remove(dimitem.IndexOf(":"));
                if (dimitemns == "*")
                {
                    dimitem = dimitem.Replace("*", lastdimns);
                }
                else 
                {
                    lastdimns = dimitemns;
                }
                var domain = domainpart;
                var member = "";
                var dim = new Dimension();

                if (domainpart.Contains(":"))
                {
                    var domainparts = domainpart.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                    switch (domainparts.Length){
                        case 2:
                            dim.IsTyped = Taxonomy.IsTyped(domain);
                            if (dim.IsTyped)
                            {
                                domain = domainpart;
                            }
                            else 
                            {
                                domain = domainparts[0];
                                member = domainparts[1];
                            }
                            break;
                        case 3:
                            domain = String.Format("{0}:{1}", domainparts[0], domainparts[1]);
                            member = domainparts[2];
                            dim.IsTyped = true;
                            break;
                        default:
                            break;
                    }
                
                }
                dim.DimensionItem = dimitem;
                dim.Domain = domain;
                dim.DomainMember = member;
                this.Dimensions.Add(dim);
            }
            this._FactString = item;
            this._FactKey = "";
        }

        public bool HasTypedDimension() 
        {
            foreach (var dimension in Dimensions) 
            {
                if (dimension.IsTyped) 
                {
                    return true;
                }
            }
            return false;
        }
        
        public void SetTyped() 
        {
            foreach (var dim in Dimensions) {
                dim.SetTyped();
            }
        }
        
        public override string ToString()
        {
            return GetFactString();
        }



        public void ClearObjects()
        {
            var fs = this.FactString;
            this.Concept = null;
            this.Dimensions.Clear();
            
        }

        public void LoadObjects() 
        {
            SetFromString(_FactString);
        }

    }
    [JsonObject(MemberSerialization.OptIn)]
    //public class FactGroup : FactBase 
    public class FactGroup : FactBase 
    {
        private List<FactBase> _Facts = new List<FactBase>();
        [JsonProperty]
        public List<FactBase> Facts { get { return _Facts; } set { _Facts = value; } }
        public List<FactBase> FullFacts {
            get { return GetFullFacts(); } 
           // set { _Facts = value; } 
        }
        public Func<FactBase, bool> Not = (f) => false;
        public void SetFacts(List<FactBase> facts) 
        {
            _Facts = facts;
        }

        public void AddFact(FactBase fact) 
        {
            _Facts.Add(fact);
        }

        protected List<FactBase> GetFullFacts() 
        {
            var newfacts = new List<FactBase>();
            if (this.Dimensions.Count == 0 && this.Concept == null)
            {
                LoadObjects();
            }
            foreach (var fact in _Facts)
            {
                if (fact.Dimensions.Count == 0 && fact.Concept == null)
                {
                    fact.LoadObjects();
                }
                var newfact = new FactBase();
                newfacts.Add(newfact);
                newfact.Dimensions.AddRange(fact.Dimensions);
                newfact.Concept=fact.Concept;

                Dimension.MergeDimensions(newfact.Dimensions, Dimensions);
                if (newfact.Concept == null)
                {
                    newfact.Concept = Concept;
                }
                newfact.Dimensions = newfact.Dimensions.OrderBy(i => i.DomainMemberFullName, StringComparer.Ordinal).ToList();
            }
            return newfacts;
        }


        [JsonIgnore]
        public override string FactString
        {
            get
            {
                if (String.IsNullOrEmpty(_FactString))
                {
                    _FactString = GetFactString();
                }
                return _FactString;
            }
            set
            {
                _FactString = value;
                SetFromString(value);
            }
        }

        public FactGroup Copy() 
        {
            var item = new FactGroup();
            item.Concept = this.Concept;
            item.Dimensions.AddRange(this.Dimensions);
            return item;
        }
    }



    public class Link
    {
        private string _Type = "";
        public string Type { get { return _Type; } set { _Type = value; } }

        private string _Href = "";
        public string Href { get { return _Href; } set { _Href = value; } }
    }

    public interface ILink
    {
        string Href { get; set; }
    }

    public interface ILabeled
    {
        Label Label { get; set; }
        string LabelContent { get; }
        string LabelCode { get; }
        string LabelID { get; set; }
    }

    public class IdentifiablewithLabel : Identifiable, ILabeled 
    {

        private Label _Label;
        public Label Label { get { return _Label; } set { _Label = value; } }


        public string LabelContent
        {
            get { return _Label != null ? _Label.Content : ""; }
        }

        public string LabelCode
        {
            get { return _Label != null ? _Label.Code : ""; }
        }

        public string _LabelID = "";
        public string LabelID { get { return _LabelID; } set { _LabelID = value; } }
        public string ParentRole { get; set; }
        public string HRef { get; set; }

        public override string ToString()
        {
            return String.Format("{0}: {1} - {2}",this.GetType().Name, this.ID,this.LabelID);
        }
    }
    
    public class Element
    {
        private string _ID = "";
        public string ID { get { return _ID; } set { _ID = value; } }

        private string _Name = "";
        public string Name { get { return _Name; } set { _Name = value; } }

        private string _Type = "";
        public string Type { get { return _Type; } set { _Type = value; } }

        private string _SubstitutionGroup = "";
        public string SubstitutionGroup { get { return _SubstitutionGroup; } set { _SubstitutionGroup = value; } }

        private string _PeriodType = "";
        public string PeriodType { get { return _PeriodType; } set { _PeriodType = value; } }

        public Boolean Nullable { get; set; }

        private Boolean _IsDefaultMember = false;
        public Boolean IsDefaultMember { get { return _IsDefaultMember; } set { _IsDefaultMember = value; } }


        private string _Domain = "";
        [DefaultValue("")]
        public string Domain { get { return _Domain; } set { _Domain = value; } }

        private string _Hierarchy = "";
        [DefaultValue("")]
        public string Hierarchy { get { return _Hierarchy; } set { _Hierarchy = value; } }


        private string _Namespace = "";
        public string Namespace { get { return _Namespace; } set { _Namespace = value; } }

        private string _NamespaceURI = "";
        public string NamespaceURI { get { return _NamespaceURI; } set { _NamespaceURI = value; } }

        private string _TypedDomainRef = "";
        public string TypedDomainRef { get { return _TypedDomainRef; } set { _TypedDomainRef = value; } }

        private string _LinkRole = "";
        [DefaultValue("")]
        public string LinkRole { get { return _LinkRole; } set { _LinkRole = value; } }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public DateTime? CreationDate { get; set; }

        public string Key { get { return String.Format("{0}:{1}",this.Namespace,this.ID); } }

        private string _FileName = "";
        public string FileName { get { return _FileName; } set { _FileName = value; } }

        public override string ToString()
        {
            return String.Format("{0} - {1}", this.GetType().Name, this.ID);
        }
    }
    [JsonObject(MemberSerialization=MemberSerialization.OptIn)]
    public class QualifiedName:Identifiable
    {
        private string _Content = "";
        [JsonProperty]
        public string Content
        {
            get { return _Content; }
            set
            {
                _Content = value;
                var six = _Content.IndexOf(":");
                if (six > -1)
                {
                    _Namespace = _Content.Remove(six);
                    _Name = _Content.Substring(six + 1);
                    ID = _Name;
                    SetFullName();

                }
            }
        }

        private string _Namespace = "";
        [JsonIgnore]
        public string Namespace
        {
            get { return _Namespace; }
            set
            {
                _Namespace = value;
                SetFullName();
            }
        }

        private string _Name = "";
        [JsonIgnore]
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                SetFullName();
            }
        }

        private void SetFullName() 
        {
            _FullName = String.Format("{0}:{1}", _Namespace, _Name);
            _Content = _FullName;
        }
        private string _FullName = "";
        public virtual string FullName 
        {
            get 
            {
                return _FullName;
            }

        }

        public QualifiedName()
        {

        }
        public QualifiedName(string content)
        {
            this.Content = content;
        }

        public override string ToString()
        {
            return String.Format("{0}", this.Content);
        }

        public void testc()
        {
            var files = System.IO.Directory.GetFiles(@"C:\My\Tasks\97532\20150606_19");
            foreach (var file in files) 
            {
                var content = System.IO.File.ReadAllText(file);
                System.IO.File.WriteAllText(file, content);
            }
        }
    }

    public class testy 
    {
        public void test ()
        {
            var s1 ="retek";
            var s2 ="retek";
            var l1 = new List<string>();
            l1.Add(s1);
            l1.Add(s2);
            s1 = "xfg";
            s2 = "ppf";
        }
    }
}
