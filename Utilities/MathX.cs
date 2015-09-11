using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class MathX
    {
        //static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<IEnumerable<T>> sequences)
        public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(IEnumerable<IEnumerable<T>> sequences)
        {
            IEnumerable<IEnumerable<T>> emptyProduct = new[] { Enumerable.Empty<T>() };
            return sequences.Aggregate(
                emptyProduct,
                (accumulator, sequence) =>
                    from accseq in accumulator
                    from item in sequence
                    select accseq.Concat(new[] { item })
                );
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
