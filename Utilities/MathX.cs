using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class MathX
    {

        public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(IEnumerable<IEnumerable<T>> sequences)
        {
            IEnumerable<IEnumerable<T>> emptyProduct = new[] { Enumerable.Empty<T>() };
            var result = sequences.Aggregate(
                emptyProduct,
                (accumulator, sequence) =>
                    from accseq in accumulator
                    from item in sequence
                    select accseq.Concat(new[] { item })
                );
            if (result.Count() == 1 && result.FirstOrDefault().Count() == 0) 
            {
                result = new List<IEnumerable<T>>();
            }
            return result;
        }

        public static List<List<T>> CartesianProductList<T>(IEnumerable<IEnumerable<T>> sequences)
        {
            var result = new List<List<T>>();
            var cr = CartesianProduct(sequences);
            foreach (var item in cr) 
            {
                var itemlist = item.ToList();
                if (itemlist.Count > 0)
                {
                    result.Add(itemlist);
                }
            }
            return result;
        }

        private void test() 
        {
            var a = new List<int>() { 10, 11 };
            var b = new List<int>() { 100, 101,102 };
            var c = new List<int>() { 8, 9 };

            var all = new List<List<int>>();
            all.Add(a);
            all.Add(b);
            all.Add(c);

            var items = CartesianProduct(all);
            foreach (var item in items)
            {
                var s ="";
                foreach (var subitem in item)
                {
                    s += String.Format("{0},", subitem);
                }
                Logger.WriteLine(s);
            }
            int z = 0;
        }
    }
}
