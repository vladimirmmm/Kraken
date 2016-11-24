using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class IntervalComparer : IComparer<Interval>
    {
        public int Compare(Interval x, Interval y)
        {
            var minend = Math.Min(x.End, y.End);
            var maxstart = Math.Max(x.Start, y.Start);
            var result = 0;
            if (x.End < y.Start - 1)
            {
                result = x.End - y.Start;
            }
            if (x.Start > y.End + 1)
            {
                result = x.Start - y.End;
            }
            return result;
        }
    }

    public class IntervalStartComparer : IComparer<Interval>
    {
        public int Compare(Interval x, Interval y)
        {
            return x.Start - y.Start;
        }
    }
    public class IntervalStartComparer<TLookup> : IComparer<Interval>
    {
        public int Compare(Interval x, Interval y)
        {
            return x.Start - y.Start;
        }
    }

    public class IntervalEndComparer : IComparer<Interval>
    {
        public int Compare(Interval x, Interval y)
        {
            return x.End - y.End;
        }
    }
    public class IntervalLookup<TLookup> : Interval 
    {
        public TLookup Value;
        public IntervalLookup() { }
        public IntervalLookup(int start) :base(start)
        { 
        }
        public IntervalLookup(int start,int end)
            : base(start,end)
        {
        }
    }
    public class Interval:IComparable<Interval>
    {
        public int Start = -1;
        public int End = -1;
        //private int _End = -1;

        //public int End { get { return _End == -1 ? Start : _End; } set { _End = value; } }

        public Interval()
        {

        }
        public Interval(int start)
        {
            this.Start = start;
            this.End = start;
        }
        public Interval(int start, int end)
        {
            this.Start = start;
            this.End = end;
        }

        public IEnumerable<int> AsEnumerable()
        {
            //if (Start == End) { yield return Start; }
            for (int i = Start; i <= End; i++)
            {
                yield return i;
            }
        }

        public int Count
        {
            get { return Start == -1 ? 0 : End - Start + 1; }
        }

        public string Content()
        {
            if (End == Start)
            {
                return Start.ToString();
            }
            else
            {
                return string.Format("{0}..{1}", Start, End);
            }
        }
        public Interval Intersect(Interval interval) 
        {
            var minend = Math.Min(interval.End, this.End);
            var maxstart = Math.Max(interval.Start, this.Start);
            if (maxstart <= minend)
            {
                return new Interval(maxstart, minend);
            }
            return null;
        }
        public Interval Merge(Interval interval)
        {
            var minstart = Math.Min(interval.Start, this.Start);
            var maxend = Math.Max(interval.End, this.End);
            if (minstart <= maxend)
            {
                return new Interval(minstart, maxend);
            }
            return null;
        }
        public List<Interval> Except(Interval interval)
        {
            var result = new List<Interval>();
            var intersection = Intersect(interval);
            if (intersection == null) 
            { 
                result.Add(this);
                return result;
            }
            var x = intersection.Start - this.Start;
            var y = this.End - intersection.End;
            if (x > 0) 
            {
                var i1 = new Interval(this.Start, intersection.Start - 1);
                result.Add(i1);

            }
            if (y > 0) 
            {
                var i2=new Interval(intersection.End + 1, this.End);
                result.Add(i2);

            }
            return result;

        }
        public int CompareX(Interval interval) 
        {
            //var minend = Math.Min(interval.End, this.End);
            //var maxstart = Math.Max(interval.Start, this.Start);
            var diff1 = this.End - interval.Start;
            var diff2 = this.Start - interval.End;
            return (diff1 + diff2) - Math.Max(diff1, diff2);
            //if (diff < 0)
            //{
            //    return -1;
            //}
            //else 
            //{
            //    diff = this.Start - interval.End;
            //    if (diff > 0) 
            //    {
            //        return 1;
            //    }
            //    return 0;
            //}
      
        }
        public int CompareOld0(Interval interval)
        {
            //var minend = Math.Min(interval.End, this.End);
            //var maxstart = Math.Max(interval.Start, this.Start);
            var diff = this.End - interval.Start;
            if (diff < 0)
            {
                return -1;
            }
            else
            {
                diff = this.Start - interval.End;
                if (diff > 0)
                {
                    return 1;
                }
                return 0;
            }

        }
        public int Compare(Interval interval)
        {
            //var minend = Math.Min(interval.End, this.End);
            //var maxstart = Math.Max(interval.Start, this.Start);
            if (this.End < interval.Start)
            {
                return this.End - interval.Start;
            }
            if (this.Start > interval.End)
            {
                return this.Start - interval.End;
            }
            return 0;
        }
        public bool IsNext(int value)
        {
            return this.End + 1 == value;
        }
        public bool IsPrev(int value)
        {
            return this.Start - 1 == value;
        }
        public bool IsInThis(int value) 
        {
            return this.Start >= value && value <= this.End;
        }
        public static Interval GetInstanceFromString(string content)
        {
            if (string.IsNullOrEmpty(content)) { return null; }
            var parts = content.Split(new string[] { ".." }, StringSplitOptions.RemoveEmptyEntries);
            var interval = new Interval(Utilities.Converters.FastParse(parts[0]));
            if (parts.Length == 2)
            {
                interval.End = Utilities.Converters.FastParse(parts[1]);
            }
            return interval;
        }
        public override string ToString()
        {
            return Content();
        }





        public int CompareTo(Interval other)
        {
            return this.Compare(other);
        }
    }
    //public class IntervalDictionary : IDictionary<int, int> 
    //{
    //    public IntervalList _Keys = new IntervalList();
    //    public List<int> _Values = new List<int>();
    //    public void Add(int key, int value)
    //    {
    //        _Keys.AddX(key)
    //    }

    //    public bool ContainsKey(int key)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public ICollection<int> Keys
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    public bool Remove(int key)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public bool TryGetValue(int key, out int value)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public ICollection<int> Values
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    public int this[int key]
    //    {
    //        get
    //        {
    //            throw new NotImplementedException();
    //        }
    //        set
    //        {
    //            throw new NotImplementedException();
    //        }
    //    }

    //    public void Add(KeyValuePair<int, int> item)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Clear()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public bool Contains(KeyValuePair<int, int> item)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void CopyTo(KeyValuePair<int, int>[] array, int arrayIndex)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public int Count
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    public bool IsReadOnly
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    public bool Remove(KeyValuePair<int, int> item)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IEnumerator<KeyValuePair<int, int>> GetEnumerator()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    public class IntervalListLookup<TLookup> : IList<int>
    {
        public List<IntervalLookup<TLookup>> Intervals = new List<IntervalLookup<TLookup>>();

        public void TrimExcess()
        {
            this.Intervals.TrimExcess();
        }
        public void Sort() { }

        public Interval LastInterval
        {
            get { return Intervals.LastOrDefault(); }
        }

        public Interval FirstInterval
        {
            get { return Intervals.FirstOrDefault(); }
        }
        private void AddNewInterval(int value, int ix)
        {
            _Count = -1;
            var interval = new IntervalLookup<TLookup>(value);
            this.Intervals.Insert(ix, interval);
        }
        private void AddNewInterval(int value)
        {
            _Count = -1;
            var interval = new IntervalLookup<TLookup>(value);
            this.Intervals.Add(interval);
        }
        public void AddInterval(IntervalLookup<TLookup> interval)
        {
            _Count = -1;
            if (LastInterval != null)
            {
                if (LastInterval.End <= interval.End && LastInterval.End >= interval.Start)
                {
                    LastInterval.End = interval.End;
                    return;
                }
            }
            this.Intervals.Add(interval);
        }
        public void AddRange(IEnumerable<int> values)
        {
            _Count = -1;
            foreach (var value in values)
            {
                this.Add(value);
            }
        }
        public static IntervalComparer ic = new IntervalComparer();
        public static IntervalStartComparer<TLookup> startcomparer = new IntervalStartComparer<TLookup>();

        public static IntervalEndComparer endcomparer = new IntervalEndComparer();

        public void AddX(int value)
        {
            _Count = -1;
            if (LastInterval == null)
            {
                AddNewInterval(value, 0);
                return;

            }
            if (LastInterval.IsNext(value))
            {
                LastInterval.End++;
                return;
            }
            if (value > LastInterval.End)
            {
                AddNewInterval(value);
                return;
            }
            var vi = Intervals.BinarySearch(new IntervalLookup<TLookup>(value), ic);

            if (vi > -1 && vi < Intervals.Count)
            {
                var i = Intervals[vi];
                var prev = vi - 1 > -1 ? Intervals[vi - 1] : null;
                var next = vi + 1 < Intervals.Count ? Intervals[vi + 1] : null;


                if (i.Start - 1 == value)
                {
                    if (prev == null || prev.End < value)
                    {
                        i.Start--;
                    }
                    return;
                }
                if (i.End + 1 == value)
                {
                    if (next == null || next.Start > value)
                    {
                        i.End++;
                    }
                    return;
                }
                if (value < i.Start) { AddNewInterval(value, vi); return; }
                if (value > i.End) { AddNewInterval(value, vi + 1); return; }


            }
            else
            {
                vi = ~vi;
                AddNewInterval(value, vi);
            }

        }

        public Interval GetInterval(int ix)
        {
            if (ix > -1 && ix < this.Intervals.Count)
            {
                return this.Intervals[ix];
            }
            return null;
        }
        public void SetNextInterval(ref Interval target, ref int index)
        {
            index++;
            target = GetInterval(index);

        }


        public void Add(int value)
        {
            AddX(value);
        }

        public static IntervalList GetIntervals(IEnumerable<int> items)
        {
            var result = new IntervalList();
            foreach (var item in items)
            {
                result.Add(item);
            }
            return result;
        }


        public new IEnumerable<int> GetEnumerator()
        {
            return this.AsEnumerable();
        }
        public IEnumerable<int> AsEnumerable()
        {
            foreach (var interval in Intervals)
            {
                foreach (var value in interval.AsEnumerable())
                {
                    yield return value;
                }
            }
        }
        private int _Count = -1;
        public int Count
        {
            get
            {
                if (_Count == -1)
                {
                    var c = 0;
                    foreach (var interval in Intervals)
                    {
                        c += interval.Count;
                    }
                    _Count = c;
                }
                return _Count;
            }
        }
        public string GetString()
        {
            var sb = new StringBuilder();
            foreach (var interval in Intervals)
            {
                sb.AppendLine(String.Format("{0} - {1}", interval.Start, interval.End));
            }
            return sb.ToString();
        }

        public string GetFullString()
        {
            var sb = new StringBuilder();
            foreach (var interval in Intervals)
            {
                sb.AppendLine(String.Format("{0} - {1}", interval.Start, interval.End));
            }
            foreach (var value in this.AsEnumerable())
            {
                sb.AppendLine(value.ToString());
            }
            return sb.ToString();
        }

        public new void Clear()
        {
            Intervals.Clear();
        }

        public int IndexOf(int item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, int item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public int this[int index]
        {

            get
            {
                if (index == 0) { return Intervals[0].Start; }
                if (index == this.Count - 1) { return LastInterval.End; }
                var counter = 0;
                foreach (var interval in Intervals)
                {
                    if (index < counter + interval.Count)
                    {
                        return interval.Start + (index - counter);
                    }
                    counter += interval.Count;
                }
                return -1;

            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int SearchByStartIndexBefore(IntervalLookup<TLookup> interval, int startix)
        {
            var ix = startix;
            var c = this.Intervals.Count;

            var tix = this.Intervals.BinarySearch(startix, c - startix, interval, IntervalListLookup<TLookup>.startcomparer);
            if (tix > c)
            {
                ix = c - 1;
            }
            if (tix < 0)
            {
                ix = ~tix;
                ix = ix > 0 ? ix - 1 : ix;
            }
            if (ix < startix)
            {
                ix = startix;
            }
            return ix;
        }
        public bool Contains(int item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(int[] array, int arrayIndex)
        {
            this.AsEnumerable().ToArray().CopyTo(array, arrayIndex);
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(int item)
        {
            throw new NotImplementedException();
        }

        IEnumerator<int> IEnumerable<int>.GetEnumerator()
        {
            return this.AsEnumerable().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.AsEnumerable().GetEnumerator();
        }

        public override string ToString()
        {
            var intterval = this.FirstInterval == null ? "" : String.Format("{0}..{1}", this.FirstInterval.Start, this.LastInterval.End);
            return string.Format("{0}/{1} [{2}]", this.Count, this.Intervals.Count, intterval);
        }
    }


    public class IntervalList : IList<int>
    {
        public List<Interval> Intervals = new List<Interval>();
        protected Dictionary<int, Interval> StartValues = new Dictionary<int, Interval>();

        public void TrimExcess() 
        {
            this.Intervals.TrimExcess();
        }
        public void Sort() { }

        public Interval LastInterval
        {
            get { return Intervals.LastOrDefault(); }
        }

        public Interval FirstInterval
        {
            get { return Intervals.FirstOrDefault(); }
        }
        private void AddNewInterval(int value, int ix)
        {
            _Count = -1;
            var interval = new Interval(value);
            this.Intervals.Insert(ix, interval);
        }
        private void AddNewInterval(int value)
        {
            _Count = -1;
            var interval = new Interval(value);
            this.Intervals.Add(interval);
        }
        public void AddInterval(Interval interval)
        {
            _Count = -1;
            if (LastInterval != null) 
            {
                if (LastInterval.End <= interval.End && LastInterval.End >= interval.Start) 
                {
                    LastInterval.End = interval.End;
                    return;
                }
            }
            this.Intervals.Add(interval);
        }
        public void AddRange(IEnumerable<int> values)
        {
            _Count = -1;
            foreach (var value in values)
            {
                this.Add(value);
            }
        }
        public static IntervalComparer ic = new IntervalComparer();
        public static IntervalStartComparer startcomparer = new IntervalStartComparer();
        public static IntervalEndComparer endcomparer = new IntervalEndComparer();
        
        public void AddX(int value)
        {
            _Count = -1;
            if (LastInterval == null)
            {
                AddNewInterval(value, 0);
                return;

            }
            if (LastInterval.IsNext(value)) 
            {
                LastInterval.End++;
                return;
            }
            if (value > LastInterval.End)
            {
                AddNewInterval(value);
                return;
            }
            var vi = Intervals.BinarySearch(new Interval(value), ic);
           
            if (vi > -1 && vi < Intervals.Count)
            {
                var i = Intervals[vi];
                var prev = vi - 1 > -1 ? Intervals[vi - 1] : null;
                var next = vi + 1 < Intervals.Count ? Intervals[vi + 1] : null;


                if (i.Start - 1 == value)
                {
                    if (prev == null || prev.End < value)
                    {
                        i.Start--;
                    }
                    return;
                }
                if (i.End + 1 == value)
                {
                    if (next == null || next.Start > value)
                    {
                        i.End++;
                    }
                    return;
                }
                if (value < i.Start) { AddNewInterval(value, vi); return; }
                if (value > i.End) { AddNewInterval(value, vi + 1); return; }


            }
            else 
            {
                vi = ~vi;
                AddNewInterval(value, vi);
            }

        }

        public Interval GetInterval(int ix) 
        {
            if (ix > -1 && ix < this.Intervals.Count) 
            {
                return this.Intervals[ix];
            }
            return null;
        }
        public void SetNextInterval(ref Interval target, ref int index)
        {
            index++;
            target=GetInterval(index);
 
        }


        public void Add(int value)
        {
            AddX(value);
        }
        
        public static IntervalList GetIntervals(IEnumerable<int> items)
        {
            var result = new IntervalList();
            foreach (var item in items)
            {
                result.Add(item);
            }
            return result;
        }


        public new IEnumerable<int> GetEnumerator()
        {
            return this.AsEnumerable();
        }
        public IEnumerable<int> AsEnumerable()
        {
            foreach (var interval in Intervals)
            {
                foreach (var value in interval.AsEnumerable())
                {
                    yield return value;
                }
            }
        }
        private int _Count = -1;
        public int Count
        {
            get
            {
                if (_Count == -1)
                {
                    var c = 0;
                    foreach (var interval in Intervals)
                    {
                        c += interval.Count;
                    }
                    _Count = c;
                }
                return _Count;
            }
        }
        public string GetString()
        {
            var sb = new StringBuilder();
            foreach (var interval in Intervals)
            {
                sb.AppendLine(String.Format("{0} - {1}", interval.Start, interval.End));
            }
            return sb.ToString();
        }

        public string GetFullString()
        {
            var sb = new StringBuilder();
            foreach (var interval in Intervals)
            {
                sb.AppendLine(String.Format("{0} - {1}", interval.Start, interval.End));
            }
            foreach (var value in this.AsEnumerable())
            {
                sb.AppendLine(value.ToString());
            }
            return sb.ToString();
        }

        public new void Clear()
        {
            Intervals.Clear();
        }

        public int IndexOf(int item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, int item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public int this[int index]
        {

            get
            {
                if (index == 0) { return Intervals[0].Start; }
                if (index == this.Count - 1) { return LastInterval.End; }
                var counter = 0;
                foreach (var interval in Intervals)
                {
                    if (index < counter + interval.Count)
                    {
                        return interval.Start + (index - counter);
                    }
                    counter += interval.Count;
                }
                return -1;

            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int SearchByStartIndexBefore(Interval interval, int startix) 
        {
            var ix = startix;
            var c = this.Intervals.Count;

            var tix = this.Intervals.BinarySearch(startix, c - startix, interval, IntervalList.startcomparer);
            if (tix > c)
            {
                ix = c - 1;
            }
            if (tix < 0)
            {
                ix = ~tix;
                ix = ix > 0 ? ix - 1 : ix;
            }
            if (ix < startix) 
            {
                ix = startix;
            }
            return ix;
        }
        public bool Contains(int item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(int[] array, int arrayIndex)
        {
            this.AsEnumerable().ToArray().CopyTo(array, arrayIndex);
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(int item)
        {
            throw new NotImplementedException();
        }

        IEnumerator<int> IEnumerable<int>.GetEnumerator()
        {
            return this.AsEnumerable().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.AsEnumerable().GetEnumerator();
        }

        public override string ToString()
        {
            var intterval = this.FirstInterval == null ? "" : String.Format("{0}..{1}", this.FirstInterval.Start, this.LastInterval.End);
            return string.Format("{0}/{1} [{2}]", this.Count, this.Intervals.Count, intterval);
        }
    }

    public class TestInterval
    {
        public void Testb()
        {
            IntervalList list1 = new IntervalList();
            list1.AddInterval(new Interval(0, 16286069));

            IntervalList list2 = new IntervalList();

            list2.AddInterval(new Interval(48639, 204038));
            list2.AddInterval(new Interval(212919, 354998));
            list2.AddInterval(new Interval(363879, 505958));
            list2.AddInterval(new Interval(514839, 656918));
            list2.AddInterval(new Interval(665799, 807878));
            list2.AddInterval(new Interval(816759, 958838));
            list2.AddInterval(new Interval(967719, 1109798));
            list2.AddInterval(new Interval(1122739, 2744818));
            list2.AddInterval(new Interval(2768499, 4390578));
            list2.AddInterval(new Interval(4402419, 6024498));
            list2.AddInterval(new Interval(6036339, 7658418));
            list2.AddInterval(new Interval(7682099, 9304178));
            list2.AddInterval(new Interval(9316019, 10938098));
            list2.AddInterval(new Interval(10949939, 12572018));
            list2.AddInterval(new Interval(12595699, 14217778));
            list2.AddInterval(new Interval(14229619, 15851698));
            list2.AddInterval(new Interval(15863539, 15996738));
            list2.AddInterval(new Interval(16132899, 16133306));
            list2.AddInterval(new Interval(16133331, 16133738));
            list2.AddInterval(new Interval(16133763, 16134170));
            list2.AddInterval(new Interval(16134195, 16137674));
            list2.AddInterval(new Interval(16137699, 16138106));
            list2.AddInterval(new Interval(16138131, 16138538));
            list2.AddInterval(new Interval(16138563, 16140866));
            list2.AddInterval(new Interval(16141851, 16157690));

            var x = Utilities.Objects.SortedExcept(list1, list2,null);

        }
        public void Test()
        {
            IntervalList list = new IntervalList();
    
            list.Add(3);
            list.Add(4);
            list.Add(1);
            list.Add(4);
            list.Add(10);
            list.Add(-1);
            list.Add(9);
            list.Add(7);
            list.Add(11);
            list.Add(4);
            list.Add(21);
            list.Add(22);
            list.Add(24);
            list.Add(23);
            list.Add(22);
            list.Add(24);
            list.Add(18);
            list.Add(2);
            list.Add(1);
            var ix1 = list[4];
            var ix2 = list[0];
            var ix3 = list[1];
            var ix4 = list[3];
            var ix5 = list[list.Count - 1];
            var ix6 = list[list.Count - 2];
            Interval sx = null;
            var ix = 2;
            list.SetNextInterval(ref sx,ref ix);
            var s = list.GetString();
        }

        public void test2() 
        {
            IntervalList list1 = new IntervalList();
            list1.Add(1);
            list1.Add(2);
            list1.Add(3);

            list1.Add(5);
            list1.Add(6);
            list1.Add(7);
            list1.Add(8);
            list1.Add(9);

            list1.Add(11);

            IntervalList list2 = new IntervalList();
            list2.Add(3);
            list2.Add(4);
            list2.Add(5);

            list2.Add(7);
            list2.Add(8);

            var result = Utilities.Objects.IntersectSorted(list2, list1, null);
         
        }

        public void test3()
        {
            IntervalList list1 = new IntervalList();
            list1.Add(-2);
            list1.Add(-1);

            list1.Add(1);
            list1.Add(2);
            list1.Add(3);

            list1.Add(5);
            list1.Add(6);
            list1.Add(7);
          
            list1.Add(9);
            list1.Add(11);
            list1.Add(12);

            list1.Add(20);
            list1.Add(21);

            IntervalList list2 = new IntervalList();
            list2.Add(3);
            list2.Add(4);
            list2.Add(5);

            list2.Add(7);
            list2.Add(8);
            list2.Add(9);

            var result = Utilities.Objects.SortedExcept(list1, list2, null);
            var result2 = Utilities.Objects.SortedExcept(list2, list1, null);

        }

        public void test4()
        {
            IntervalList list1 = new IntervalList();
            list1.Add(-2);
            list1.Add(-1);

            list1.Add(1);
            list1.Add(2);
            list1.Add(3);

            list1.Add(5);
            list1.Add(6);
            list1.Add(7);

            list1.Add(9);
            list1.Add(11);
            list1.Add(12);

            list1.Add(20);
            list1.Add(21);

            IntervalList list2 = new IntervalList();
            list2.Add(3);
            list2.Add(4);
            list2.Add(5);

            list2.Add(7);
            list2.Add(8);
            list2.Add(9);

            var result = Utilities.Objects.MergeSorted(list1, list2, null);
            var result2 = Utilities.Objects.MergeSorted(list2, list1, null);

        }
    }
}
