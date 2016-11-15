﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class ArrayEqualityComparer:IEqualityComparer<int[]>{

        public bool Equals(int[] x, int[] y)
        {
            if (x.Length != y.Length) { return false; }
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != y[i])
                {
                    return false;
                }
            }
            return true;
        }

        public int GetHashCode(int[] obj)
        {
            return obj.GetHashCode();
        }
    }

    public class Objects
    {
        public static string GetFullException(Exception ex) 
        {
            var sb = new StringBuilder();
            sb.AppendLine(ex.Message);
            sb.AppendLine(ex.StackTrace);
            if (ex.InnerException != null)
            {
                sb.AppendLine("Inner Exception:");
                sb.AppendLine(ex.InnerException.Message);
                sb.AppendLine(ex.InnerException.StackTrace);
            }
            return sb.ToString();
        }
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

        public static List<T> SortedExcept_Old<T>(List<T> m, List<T> n) where T : IComparable<T>
        {
            if (m.Count > 2 && n.Count > 2)
            {
                var startm = m[0];
                var startn = n[0];
                var endm = m[m.Count - 1];
                var endn = n[n.Count - 1];
                // a > b: a compareto b > 0  
                if (startn.CompareTo(endm) > 0 || endn.CompareTo(startm)<0) 
                {
                    return m;
                }
            }
            
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

        public static IntervalList SortedExcept(IntervalList sequence1, IntervalList sequence2, IComparer<int> comparer)
        {
            var result = new IntervalList();
            var smaller = sequence1;
            var bigger = sequence2;
            var bix = 0;
            var six = 0;
            if (bigger.Intervals.Count == 0) { return smaller; }

            Interval s = smaller.Intervals.FirstOrDefault();
            Interval b = bigger.Intervals.FirstOrDefault();
            Interval snext = smaller.Intervals.FirstOrDefault();
            Interval bnext = bigger.Intervals.FirstOrDefault();

      
            while (snext !=null)
            {

                s = snext;
                b = bnext;

            
                var r = b==null?-1: s.Compare(b);
                if (r == 0)
                {
                    var except = s.Except(b);
                    var hasany = except != null && except.Count > 0;
                    if (!hasany)
                    {
                        six++;
                        snext = six < smaller.Intervals.Count ? smaller.Intervals[six] : null;
                        continue;
                    }
                    else 
                    {
                   
                        if (except.Count == 2)
                        {
                            result.Intervals.Add(except[0]);
                            snext = except[1];
                            continue;
                        }
                        else
                        {
                            snext = except[0];
                           
                        }
                        s = snext;
                    
                    }

                    if (s.End > b.End)
                    {
                        bix++;
                    }
                    else
                    {
                        result.Intervals.Add(s);
                        six++;
                        snext = six < smaller.Intervals.Count ? smaller.Intervals[six] : null;

                    }
                    

                }
                if (r < 0)
                {
                    result.Intervals.Add(s);
                    six++;
                    snext = six < smaller.Intervals.Count ? smaller.Intervals[six] : null;

                }
                if (r > 0)
                {
                    bix++;
                }
                bnext = bix < bigger.Intervals.Count ? bigger.Intervals[bix] : null;
            }

            return result;
        }

        public static IList<int> SortedExcept(IList<int> m, IList<int> n)
        {
            if (m.GetType() == typeof(IntervalList) && n.GetType() == typeof(IntervalList))
            {
                return SortedExcept((IntervalList)m, (IntervalList)n, null);
            }
            if (m.Count > 2 && n.Count > 2)
            {
                var startm = m[0];
                var startn = n[0];
                var endm = m[m.Count - 1];
                var endn = n[n.Count - 1];
                // a > b: a compareto b > 0  
                if (startn.CompareTo(endm) > 0 || endn.CompareTo(startm) < 0)
                {
                    return m.ToList();
                }
            }
            var mcount = m.Count;
            var ncount = n.Count;
            var result = new List<int>();
            int i = 0, j = 0;
            if (ncount == 0)
            {
                result.AddRange(m);
                return result;
            }
            if (mcount == 0)
            {
                return m.ToList();
            }
            var mi = m[i];
            var nj = n[j];
            while (i < mcount)
            {
                if (mi.CompareTo(nj) < 0)
                {
                    result.Add(mi);
                    i++;
                    mi = i >= mcount ? mi : m[i];
                }
                else if (mi.CompareTo(nj) > 0)
                {
                    j++;
                    //nj = n[j];
                    nj = j >= ncount ? nj : n[j];

                }
                else
                {
                    i++;
                    mi = i >= mcount ? mi : m[i];
                }
                if (j >= ncount)
                {
                    for (; i < mcount; i++)
                    {
                        mi = m[i];
                        result.Add(mi);

                    }
                    break;
                }
            }
            return result;
        }
        public static List<T> SortedExcept<T>(List<T> m, List<T> n) where T : IComparable<T>
        {
            if (m.Count > 2 && n.Count > 2)
            {
                var startm = m[0];
                var startn = n[0];
                var endm = m[m.Count - 1];
                var endn = n[n.Count - 1];
                // a > b: a compareto b > 0  
                if (startn.CompareTo(endm) > 0 || endn.CompareTo(startm) < 0)
                {
                    return m;
                }
            }
            var mcount = m.Count;
            var ncount = n.Count;
            var result = new List<T>();
            int i = 0, j = 0;
            if (ncount == 0)
            {
                result.AddRange(m);
                return result;
            }
            if (mcount == 0)
            {
                return m;
            }
            var mi = m[i];
            var nj = n[j];
            while (i < mcount)
            {
                if (mi.CompareTo(nj) < 0)
                {
                    result.Add(mi);
                    i++;
                    mi = i >= mcount ? mi : m[i];
                }
                else if (mi.CompareTo(nj) > 0)
                {
                    j++;
                    //nj = n[j];
                    nj = j >= ncount ? nj : n[j];

                }
                else
                {
                    i++;
                    mi = i >= mcount ? mi : m[i];
                }
                if (j >= ncount)
                {
                    for (; i < mcount; i++)
                    {
                        mi = m[i];
                        result.Add(mi);
                        
                    }
                    break;
                }
            }
            return result;
        }
        
        
        public void test() 
        {
            Random rnd = new Random();
            var li = new List<int>();
            var li2 = new List<int>() { 1215, 15748, 12, 14675, 98000, 123, 12105, 157408, 120, 146075, 980000, 1230 };
            for (int i = 0; i < 1000000; i++) 
            {
                li.Add(i);
            }
            for (int i = 0; i < 100000; i++)
            {
                var r = rnd.Next(0, 1000000 - 1);
                while (li2.Contains(r)) 
                {
                    r = rnd.Next(0, 1000000 - 1);
                }

                li2.Add(r);
            }
            li2 = li2.OrderBy(i => i).ToList();
            var dict = li.ToDictionary(i => i,e=>true);
            var dict2 = li2.ToDictionary(i => i, e => true);
            var ref1 = DateTime.Now;
            for (int i = 0; i < 100; i++)
            {
                var result = IntersectSorted(li, li2, null);
            }
            var ref2 = DateTime.Now;
            for (int i = 0; i < 100; i++)
            {
                var result2 = IntersectSorted(li2, li, null);
            }
            var ref3 = DateTime.Now;
            var f1 = ref2.Subtract(ref1).TotalMilliseconds;
            var f2 = ref3.Subtract(ref2).TotalMilliseconds;
            var z = 0;
            ref1 = DateTime.Now;
            for (int i = 0; i < 100; i++)
            {
                var result = IntersectSorted(dict, dict2, null);
            }
            ref2 = DateTime.Now;
            for (int i = 0; i < 100; i++)
            {
                var result2 = IntersectSorted(dict2, dict, null);
            }
            ref3 = DateTime.Now;
            f1 = ref2.Subtract(ref1).TotalMilliseconds;
            f2 = ref3.Subtract(ref2).TotalMilliseconds;
            z = 1;
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
        
        public static List<int> IntersectSorted(Dictionary<int,bool> sequence1, Dictionary<int,bool> sequence2, Comparer<int> comparer)
        {
            var results = new List<int>();
            foreach (var item in sequence1) 
            {
                if (sequence2.ContainsKey(item.Key)) 
                {
                    results.Add(item.Key);
                }

            }
            return results;
        }
        public static List<int> IntersectSorted(List<int> sequence1, HashSet<int> sequence2, Comparer<int> comparer)
        {
            var results = new List<int>();//sequence1.Count());
            int item;
            for (int i = 0; i < sequence1.Count; i++)
            {
                item = sequence1[i];
                if (sequence2.Contains(item))
                {
                    results.Add(item);
                }

            }
            //results.TrimExcess();
            return results;
        }

        public static List<string> SearchDictionary<T>(Dictionary<string, T> dict, String key) 
        {
            return dict.Keys.Where(i => i.Contains(key)).ToList();
        }

        public static List<T> IntersectSorted<T>(IEnumerable<T> sequence1, HashSet<T> sequence2, IComparer<T> comparer)
        {
            var results = new List<T>();//sequence1.Count());
            foreach (var item in sequence1)
            {
                if (sequence2.Contains(item))
                {
                    results.Add(item);
                }

            }
            //results.TrimExcess();
            return results;
        }
        //public static List<T> IntersectSorted<T>( List<T> sequence1, List<T> sequence2, IComparer<T> comparer)
        //{
        //    return IntersectSorted(sequence1.AsEnumerable(), sequence2.AsEnumerable(), comparer).ToList();
        //}
        private static bool IsContinous(List<int> items)
        {
            if (items[items.Count - 1] - items[0] == items.Count - 1) 
            {
                return true;
            }
            return false;
        }
        private static bool IsTheSame(List<int> items1, List<int> items2)
        {
            if (items1.Count != items2.Count) { return false; }
            if (items1.Count == 0) { return false; }
            if (items1[0]==items2[0])
            {
                return IsContinous(items1) && IsContinous(items2);
            }
            return false;
        }
        private static bool IsContinous(IList<int> items)
        {
            if (items[items.Count - 1] - items[0] == items.Count - 1)
            {
                return true;
            }
            return false;
        }
        private static bool IsTheSame(IList<int> items1, IList<int> items2)
        {
            if (items1.Count != items2.Count) { return false; }
            if (items1.Count == 0) { return false; }
            if (items1[0] == items2[0])
            {
                return IsContinous(items1) && IsContinous(items2);
            }
            return false;
        }

        public static IntervalList MergeSorted(IntervalList sequence1, IntervalList sequence2, IComparer<int> comparer)
        {
            var result = new IntervalList();
            var smaller = sequence1.Intervals.Count < sequence2.Intervals.Count ? sequence1 : sequence2;
            var bigger = smaller == sequence1 ? sequence2 : sequence1;
            var bix = 0;
            var six = 0;
            if (IsTheSame(sequence1, sequence2))
            {
                return sequence1;
            }
       
            var sc = smaller.Intervals.Count;
            var bc = bigger.Intervals.Count;

            Interval s = smaller.Intervals.FirstOrDefault();
            Interval b = bigger.Intervals.FirstOrDefault();
            int r = 0;
            while (s!=null || b!=null)
            {

                if (s != null && b != null)
                {
                    r = s.Compare(b);
                }
                else 
                {
                    r = s == null ? 1 : -1;
                }
                if (r == 0)
                {
                    var interval = s.Merge(b);
              
                    result.AddInterval(interval);
                    

                    if (interval.End >= b.End)
                    {
                        bigger.SetNextInterval(ref b, ref bix);
                    }
                    if (interval.End >= s.End)
                    {
                        smaller.SetNextInterval(ref s, ref six);

                    }

                }
                if (r < 0)
                {
                    result.AddInterval(s);

                    smaller.SetNextInterval(ref s, ref six);


                }
                if (r > 0)
                {
                    result.AddInterval(b);

                    bigger.SetNextInterval(ref b, ref bix);

                }
            }

            return result;
        }


        public static IntervalList IntersectSorted(IntervalList sequence1, IntervalList sequence2, IComparer<int> comparer)
        {
            var result = new IntervalList();
            var smaller = sequence1.Intervals.Count < sequence2.Intervals.Count ? sequence1 : sequence2;
            var bigger = smaller == sequence1 ? sequence2 : sequence1;
            var bix = 0;
            var six = 0;
            if (IsTheSame(sequence1, sequence2)) 
            {
                return sequence1;
            }
            Interval s = null;
            Interval b = null;
            var sc = smaller.Intervals.Count;
            var bc = bigger.Intervals.Count;
            var bchanged = true;
            var schanged = true;
            //if (sc>0 && bc>sc*100)
            //{
            //    //var vix = bigger.Intervals.BinarySearch(smaller.FirstInterval, IntervalList.ic2);
            //    //bix = (vix < 0 ? ~vix : vix) - 1;
            //    //if (bix < 0 || bix == bigger.Intervals.Count)
            //    //{
            //    //    bix = 0;
            //    //}
            //}
            while (six < sc && bix < bc) 
            {
                if (schanged)
                {
                    s = smaller.Intervals[six];
                    schanged = false;
                }
                if (bchanged)
                {
                    b = bigger.Intervals[bix];
                    bchanged = false;
                }
                var r = s.Compare(b);
                if (r == 0) 
                {
                    result.Intervals.Add(s.Intersect(b));
                    if (s.End > b.End)
                    {
                        bix++;
                        bchanged = true;
                    }
                    else 
                    {
                        six++;
                        schanged = true;

                    }

                }
                if (r < 0) 
                {
                    six++;
                    schanged = true;

                }
                if (r > 0) 
                {
                    bix++;
                    bchanged=true;
                }
            }       

            return result;
        }

        public static IList<int> IntersectSorted(IList<int> sequence1, IList<int> sequence2, IComparer<int> comparer)
        {
            if (sequence1.GetType() == typeof(IntervalList) && sequence2.GetType() == typeof(IntervalList)) 
            {
                return IntersectSorted((IntervalList)sequence1, (IntervalList)sequence2, null);
            }
            if (IsTheSame(sequence1, sequence2)) { 
                return sequence1.ToList();
            }
            var smaller = sequence1.Count < sequence2.Count ? sequence1 : sequence2;
            var bigger = smaller == sequence1 ? sequence2 : sequence1;
            List<int> r = new List<int>(smaller.Count);

            //if (smaller.Count*10 < bigger.Count)
            //{
            //    var secix = 0;
            //    var seccount = bigger.Count;
            //    foreach (var item in smaller)
            //    {
            //        var ix = bigger.BinarySearch(secix, seccount, item, null);
            //        if (ix >= 0)
            //        {
            //            r.Add(item);
            //            secix = ix;
            //            seccount = bigger.Count - secix;
            //        }
            //    }
            //}
            //else 
            //{
            //    return IntersectSorted((IEnumerable<int>)smaller, (IEnumerable<int>)bigger, null).ToList();
            //}
            return IntersectSorted((IEnumerable<int>)smaller, (IEnumerable<int>)bigger, null).ToList();
            
     
            //return r;
        }
        public static List<T> IntersectSorted<T>(IEnumerable<T> sequence1, List<HashSet<T>> sequence2, IComparer<T> comparer)
        {
            var results = new List<T>();//sequence1.Count());

            foreach (HashSet<T> item in sequence2)
            {

                results.AddRange( IntersectSorted(sequence1, item,comparer));
            }
            return results;
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
    public class IntArrayComparer : IComparer<int[]>
    { 
    
        int IComparer<int[]>.Compare(int[] x, int[] y)
        {
            var minlength = Math.Min(x.Length, y.Length);
            var val=0;
            for (int i = 0; i < minlength; i++) 
            {
                val = x[i] - y[i];
                if (val != 0) { return val; }
            }
            return x.Length - y.Length;
        }
    }
    public class IntArrayEqualityComparer : IEqualityComparer<int[]>
    {
        public bool Equals(int[] x, int[] y)
        {
            if (x.Length != y.Length)
            {
                return false;
            }
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != y[i])
                {
                    return false;
                }
            }
            return true;
        }

        public int GetHashCode(int[] obj)
        {
            int result = 17;
            for (int i = 0; i < obj.Length; i++)
            {
                unchecked
                {
                    result = result * 23 + obj[i];
                }
            }
            return result;
        }
    }

    public class Testr 
    {
        public void emoryest()
        {
            var cnt = 1500000;
            //var stringlist = new List< List<string>>(cnt);
            var dict = new Dictionary<int[], bool>(cnt);
            var dict2 = new Dictionary<int[], List<String>>(cnt, new IntArrayEqualityComparer());
            var dict3 = new SortedList<int[], List<String>>(cnt, new IntArrayComparer());
            var idarray = new int[] { 12, 23542, 2353, 235, 7543, 54572, 234, 46577,123,2545,24566,3464667 };
            for (int i = 0; i < cnt; i++)
            {
                idarray[0] = idarray[0] + 1;
                idarray[1] = idarray[1] + 1;
                idarray[2] = idarray[2] + 1;
                idarray[3] = idarray[3] + 1;
                idarray[4] = idarray[4] + 1;
                idarray[5] = idarray[5] + 1;
                var cellist = new List<string>(){"eba_tC_66.00.w<"+i.ToString()+"|"+(390+i).ToString()+"|160>"};
                dict2.Add(idarray.ToList().ToArray(), cellist);
                //dict3.Add(idarray.ToList().ToArray(), cellist);
            }
        }
   
    }



}
