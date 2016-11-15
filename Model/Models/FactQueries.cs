using LogicalModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace LogicalModel
{
    public class FactQuery
    {
        public List<int> DictFilterIndexes = new List<int>();
        public List<int> NegativeDictFilterIndexes = new List<int>();

        public IList<int> ToIntervalList(Taxonomy taxonomy, IList<int> facts, bool ensurepartnr = false)
        {
            //cover here
            //var partnr = Cover ? ensurepartnr ? NrOfDictFilters : 0 : 0;
            var partnr = 0;
            var items = taxonomy.SearchFactsGetIndex3(DictFilterIndexes.ToArray(), taxonomy.FactsOfParts, facts, partnr);
            foreach (var negativeindex in NegativeDictFilterIndexes)
            {
                if (taxonomy.FactsOfParts.ContainsKey(negativeindex))
                {
                    items = Utilities.Objects.SortedExcept(items, taxonomy.FactsOfParts[negativeindex]);
                }
            }

            return items;
        }

        public string GetString(Taxonomy taxonomy) 
        {
            var sb = new StringBuilder();
            foreach (var ix in DictFilterIndexes) 
            {
                sb.Append(taxonomy.CounterFactParts[ix] + ", ");
            }
            sb.Append("; !:");
            foreach (var ix in NegativeDictFilterIndexes)
            {
                sb.Append(taxonomy.CounterFactParts[ix] + ", ");
            }
            return sb.ToString();
        }
    }

    public class FactQueryPool 
    {
        public List<List<FactBaseQuery>> Items = new List<List<FactBaseQuery>>();

        public IEnumerable<IList<int>> EnumerateIntervals(Taxonomy taxonomy, int ix, IList<int> data) 
        {
            if (ix == Items.Count) { yield return data; }
            foreach (var qry in Items[ix]) 
            {
                var subresult = qry.ToIntervalList(taxonomy, data);
                EnumerateIntervals(taxonomy, ix + 1, subresult);
            }
        }
    }
}
