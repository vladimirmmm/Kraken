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

        public static IEnumerable<T> IntersectSorted<T>( IEnumerable<T> sequence1,
    IEnumerable<T> sequence2,
    IComparer<T> comparer)
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
                    int comparison = comparer.Compare(value1, value2);
                    if (comparison < 0)
                    {
                        if (!cursor1.MoveNext())
                        {
                            yield break;
                        }
                        value1 = cursor1.Current;
                    }
                    else if (comparison > 0)
                    {
                        if (!cursor2.MoveNext())
                        {
                            yield break;
                        }
                        value2 = cursor2.Current;
                    }
                    else
                    {
                        yield return value1;
                        if (!cursor1.MoveNext() || !cursor2.MoveNext())
                        {
                            yield break;
                        }
                        value1 = cursor1.Current;
                        value2 = cursor2.Current;
                    }
                }
            }
        }
    }
}
