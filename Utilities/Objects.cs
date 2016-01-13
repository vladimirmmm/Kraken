using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Objects
    {
        public static Type GetEnumerableType(Type type)
        {
            foreach (Type intType in type.GetInterfaces())
            {
                if (intType.IsGenericType
                    && intType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    return intType.GetGenericArguments()[0];
                }
            }
            return null;
        }

        public static String ListToString<T>(IEnumerable<T> items) 
        {
            var sb = new StringBuilder();
            foreach (var item in items) 
            {
                sb.AppendLine(item.ToString());
            }
            return sb.ToString();
        }

        public static List<T> SortedExcept<T>(List<T> m, List<T> n) where T : IComparable<T>
        {
            var result = new List<T>();
            int i = 0, j = 0;
            if (n.Count == 0)
            {
                result.AddRange(m);
                return result;
            }
            while (i < m.Count)
            {
                if (m[i].CompareTo(n[j]) < 0)
                {
                    result.Add(m[i]);
                    i++;
                }
                else if (m[i].CompareTo(n[j]) > 0)
                {
                    j++;
                }
                else
                {
                    i++;
                }
                if (j >= n.Count)
                {
                    for (; i < m.Count; i++)
                    {
                        result.Add(m[i]);
                    }
                    break;
                }
            }
            return result;
        }
        public void test() 
        {
            var li = new List<int>();
            var li2 = new List<int>() { 1215, 15748, 12, 14675, 98000, 123, 12105, 157408, 120, 146075, 980000, 1230 };
            for (int i = 0; i < 1000000; i++) 
            {
                li.Add(i);
            }
            var ref1 = DateTime.Now;
            var result = IntersectSorted(li, li2, null);
            var ref2 = DateTime.Now;
            var result2 = IntersectSorted(li2, li, null);
            var ref3 = DateTime.Now;
            var f1 = ref2.Subtract(ref1).TotalMilliseconds;
            var f2 = ref3.Subtract(ref2).TotalMilliseconds;
            var z = 0;
        }

        public static List<int> IntersectSortedI(List<int> source, List<int> target, Comparer<int> comparer)
        {
            // Set initial capacity to a "full-intersection" size
            // This prevents multiple re-allocations
            var ints = new List<int>(Math.Min(source.Count, target.Count));

            var i = 0;
            var j = 0;

            while (i < source.Count && j < target.Count)
            {
                // Compare only once and let compiler optimize the switch-case
                switch (source[i].CompareTo(target[j]))
                {
                    case -1:
                        i++;

                        // Saves us a JMP instruction
                        continue;
                    case 1:
                        j++;

                        // Saves us a JMP instruction
                        continue;
                    default:
                        ints.Add(source[i++]);
                        j++;

                        // Saves us a JMP instruction
                        continue;
                }
            }

            // Free unused memory (sets capacity to actual count)
            ints.TrimExcess();

            return ints;
        }
        public static List<T> IntersectSorted<T>( List<T> sequence1, List<T> sequence2, IComparer<T> comparer)
        {
            return IntersectSorted(sequence1.AsEnumerable(), sequence2.AsEnumerable(), comparer).ToList();
        }
        public static IEnumerable<T> IntersectSorted<T>( IEnumerable<T> sequence1, IEnumerable<T> sequence2, IComparer<T> comparer)
        {
            if (comparer == null)
            {
                comparer = Comparer<T>.Default;
            }
            using (var cursor1 = sequence1.GetEnumerator())
            using (var cursor2 = sequence2.GetEnumerator())
            {
                if (!cursor1.MoveNext() || !cursor2.MoveNext())
                {
                    yield break;
                }
                var value1 = cursor1.Current;
                var value2 = cursor2.Current;

                while (true)
                {
                    switch (comparer.Compare(value1, value2))
                    {
                        case -1:
                            if (!cursor1.MoveNext())
                            {
                                yield break;
                            }
                            value1 = cursor1.Current;
                            continue;

                        case 1:
                            if (!cursor2.MoveNext())
                            {
                                yield break;
                            }
                            value2 = cursor2.Current;
                            continue;

                        default:
                            yield return value1;
                            if (!cursor1.MoveNext() || !cursor2.MoveNext())
                            {
                                yield break;
                            }
                            value1 = cursor1.Current;
                            value2 = cursor2.Current;
                            // Saves us a JMP instruction
                            continue;
                    }
                    //int comparison = comparer.Compare(value1, value2);
                    //if (comparison < 0)
                    //{
                    //    if (!cursor1.MoveNext())
                    //    {
                    //        yield break;
                    //    }
                    //    value1 = cursor1.Current;
                    //}
                    //else if (comparison > 0)
                    //{
                    //    if (!cursor2.MoveNext())
                    //    {
                    //        yield break;
                    //    }
                    //    value2 = cursor2.Current;
                    //}
                    //else
                    //{
                    //    yield return value1;
                    //    if (!cursor1.MoveNext() || !cursor2.MoveNext())
                    //    {
                    //        yield break;
                    //    }
                    //    value1 = cursor1.Current;
                    //    value2 = cursor2.Current;
                    //}
                }
            }
        }
    }
}
