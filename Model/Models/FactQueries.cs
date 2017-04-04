using LogicalModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace LogicalModel.Base
{
    public class FactBaseQuery
    {
        public string ID = "";
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
                    _Pools = ChildQueries.Where(i => i is FactPoolQuery).Select(i => i as FactPoolQuery).ToList();

                }
                return _Pools;
            }
        }
        public int ChildCount { get { return ChildQueries.Count; } }
        public FactBaseQuery()
        {
            this.ID = Guid.NewGuid().ToString();
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
            this.ID = Guid.NewGuid().ToString();
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
            if (queries.Length == 0) { return null; }

            var result = queries[0];

            for (int i = 1; i < queries.Length; i++)
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
        public static void RemoveQuery(FactBaseQuery from, FactBaseQuery item)
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

        public IList<int> ToList(Taxonomy taxonomy, IList<int> facts, bool ensurepartnr = false)
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
        public IEnumerable<FactBaseQueryResult> EnumerateResults(FactsPartsDictionary FactsOfParts, int ix, IList<int> data, FactBaseQuery currentquery, bool skipinitialfiltering = false)
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


        public IEnumerable<IList<int>> EnumerateIntervals(FactsPartsDictionary FactsOfParts, int ix, IList<int> data, bool ensureKeyCount)
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
                var currentpool = Pools[ix];

                //if (currentpool.DictFilterIndexes.Count > 0)
                //{
                //    items = FactsOfParts.SearchFactsIndexByKey(currentpool.DictFilterIndexes.ToArray(), items);
                //}
                //foreach (var negativeindex in currentpool.NegativeDictFilterIndexes)
                //{
                //    if (FactsOfParts.ContainsKey(negativeindex))
                //    {
                //        items = Utilities.Objects.SortedExcept(items, FactsOfParts[negativeindex]);
                //    }
                //}
                //if (currentpool.ChildQueries.Count == 0) 
                //{
                //    yield return items;
                //}
                foreach (var qry in currentpool.ChildQueries)
                {
                    var subresult = qry.ToIntervalList(FactsOfParts, items);
                    foreach (var x in EnumerateIntervals(FactsOfParts, ix + 1, subresult, ensureKeyCount))
                    {
                        yield return x;
                    }
                }
            }
        }

        public string GetString(Taxonomy taxonomy, string pad = "")
        {
            var sb = new StringBuilder();
            sb.AppendLine(pad+this.GetType().Name + " " + this.ID);
            if (DictFilterIndexes.Count > 0)
            {
                sb.Append(pad);
                foreach (var ix in DictFilterIndexes)
                {
                    sb.Append(taxonomy.CounterFactParts[ix] + ", ");
                }
                sb.AppendLine("");

            }
            if (NegativeDictFilterIndexes.Count > 0)
            {
                sb.Append(pad+"! ");

                foreach (var ix in NegativeDictFilterIndexes)
                {
                    sb.Append(taxonomy.CounterFactParts[ix] + ", ");
                }
                sb.AppendLine("");

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
            var qry = new FactBaseQuery();
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
                                    var partqry = new FactBaseQuery();
                                    qry.AddIndex(factparts[partkey], isnegative);
                                }
                            }
                            else
                            {
                                var qrypool = new FactPoolQuery();
                                foreach (var partkey in partkeys)
                                {
                                    var partqry = new FactBaseQuery();
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
                    var qrypool = new FactPoolQuery();
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
                        FactBaseQuery subquery = null;
                        if (factparts.ContainsKey(oritem))
                        {
                            subquery = new FactBaseQuery();
                            subquery.AddIndex(factparts[oritem], isnegative);
                        }
                        else
                        {

                            var partkeys = factparts.Keys.Where(k => k.IndexOf(oritem, StringComparison.InvariantCultureIgnoreCase) > -1).ToList();
                            if (partkeys.Count > 0 && !String.IsNullOrEmpty(oritem))
                            {
                                if (isnegative)
                                {
                                    subquery = new FactBaseQuery();

                                    foreach (var partkey in partkeys)
                                    {
                                        var partqry = new FactBaseQuery();
                                        subquery.AddIndex(factparts[partkey], isnegative);
                                    }
                                }
                                else
                                {
                                    subquery = new FactPoolQuery();
                                    foreach (var partkey in partkeys)
                                    {
                                        var partqry = new FactBaseQuery();
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
            if (!qry.HasFilters()) { return null; }
            else
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

        internal void ClearParent()
        {
            this.Parent = null;
        }
    }

    public class FactPoolQuery : FactBaseQuery
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
        public void Remove(FactBaseQuery childquery)
        {
            this.ChildQueries.Remove(childquery);
            childquery.ClearParent();
        }
    }

    public class FactBaseQueryResult
    {
        public FactBaseQuery Query = null;
        public IList<int> Items = null;
    }
}
