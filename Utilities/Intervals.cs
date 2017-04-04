using Newtonsoft.Json;
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
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Interval:IComparable<Interval>
    {
        private int _Start = 0;
        private int _End = 0;
        //private int _End = -1;
        [JsonProperty]
        public int End { get { return _End; } set { _End = value; } }
        [JsonProperty]
        public int Start { get { return _Start; } set { _Start = value; } }

        public Interval()
        {

        }
        public Interval(int start)
        {
            _Start = start;
            _End = start;
        }
        public Interval(int start, int end)
        {
            _Start = start;
            _End = end;
        }

        public IEnumerable<int> AsEnumerable()
        {
            //if (Start == End) { yield return Start; }
            for (int i = _Start; i <= _End; i++)
            {
                yield return i;
            }
        }

        public int Count
        {
            get { return _Start == -1 ? 0 : _End - _Start + 1; }
        }

        public string Content()
        {
            if (_End == _Start)
            {
                return _Start.ToString();
            }
            else
            {
                return string.Format("{0}..{1}", _Start, _End);
            }
        }
        public Interval Intersect(Interval interval) 
        {
            var minend = Math.Min(interval.End, _End);
            var maxstart = Math.Max(interval.Start, _Start);
            if (maxstart <= minend)
            {
                return new Interval(maxstart, minend);
            }
            return null;
        }
        public Interval Merge(Interval interval)
        {
            var minstart = Math.Min(interval.Start, _Start);
            var maxend = Math.Max(interval.End, _End);
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
                result.Add(this.Copy());
                return result;
            }
            var x = intersection.Start - _Start;
            var y = this.End - intersection.End;
            if (x > 0) 
            {
                var i1 = new Interval(_Start, intersection.Start - 1);
                result.Add(i1);

            }
            if (y > 0) 
            {
                var i2=new Interval(intersection.End + 1, _End);
                result.Add(i2);

            }
            return result;

        }
        public int CompareX(Interval interval) 
        {
            //var minend = Math.Min(interval.End, this.End);
            //var maxstart = Math.Max(interval.Start, this.Start);
            var diff1 = _End - interval.Start;
            var diff2 = _Start - interval.End;
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
            var diff = _End - interval.Start;
            if (diff < 0)
            {
                return -1;
            }
            else
            {
                diff = _Start - interval.End;
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
            if (_End < interval.Start)
            {
                return _End - interval.Start;
            }
            if (_Start > interval.End)
            {
                return _Start - interval.End;
            }
            return 0;
        }
        public bool IsNext(int value)
        {
            return _End + 1 == value;
        }
        public bool IsPrev(int value)
        {
            return _Start - 1 == value;
        }
        public bool IsInThis(int value) 
        {
            return _Start >= value && value <= _End;
        }
        public static Interval GetInstanceFromString(string content)
        {
            if (string.IsNullOrEmpty(content)) { return null; }
            var ix = content.IndexOf("..",StringComparison.Ordinal);
            Interval interval = null;
            if (ix == -1)
            {
                interval = new Interval(Utilities.Converters.FastParse(content));
            }
            else 
            {
                var start = content.Substring(0, ix);
                ix = ix + 2;
                var end = content.Substring(ix, content.Length - ix);
                interval = new Interval(Utilities.Converters.FastParse(start), Utilities.Converters.FastParse(end));
            }
            //var parts = content.Split(new string[] { ".." }, StringSplitOptions.RemoveEmptyEntries);
            //var interval = new Interval(Utilities.Converters.FastParse(parts[0]));
            //if (parts.Length == 2)
            //{
            //    interval.End = Utilities.Converters.FastParse(parts[1]);
            //}
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

        public Interval Copy()
        {
            return new Interval(_Start, _End);
        }
    }
 
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


        public IEnumerable<int> GetEnumerator()
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

        public void Clear()
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

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class IntervalList : IList<int>
    {
        public IntervalList() 
        {

        }
        public IntervalList(int value)
        {
            var interval = new Interval(value);
            AddIntervalToEnd(interval);
     
        }
        public IntervalList(int start,int end)
        {
            var interval = new Interval(start,end);
            AddIntervalToEnd(interval);
      
        }
        public IntervalList(Interval interval)
        {
     
            AddIntervalToEnd(interval);
        }
        private List<Interval> _Intervals = new List<Interval>();
        [JsonProperty]
        public List<Interval> Intervals { get { return _Intervals; } set { _Intervals = value; } }
        protected Dictionary<int, Interval> StartValues = new Dictionary<int, Interval>();

        public void TrimExcess() 
        {
            this.Intervals.TrimExcess();
        }
        public void Sort() { }

        private Interval _LastInterval = null;
        public Interval LastInterval
        {
            get 
            {
                if (_LastInterval == null)
                {
                    _LastInterval = Intervals.LastOrDefault();
                }
                return _LastInterval;
            }
        }

        public Interval FirstInterval
        {
            get { return Intervals.FirstOrDefault(); }
        }
        private void AddNewInterval(int value, int ix)
        {
            _Count = -1;
            var interval = new Interval(value);
            if (ix == this.Intervals.Count) 
            {
                _LastInterval = interval;
            }
            this.Intervals.Insert(ix, interval);
        }
        public void AddIntervalToEnd(Interval interval) 
        {
            _LastInterval = interval;
            this.Intervals.Add(interval);
        }
        private void AddNewInterval(int value)
        {
            _Count = -1;
            var interval = new Interval(value);
            AddIntervalToEnd(interval);
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
            AddIntervalToEnd(interval);
        }
        public void AddRange_O(IEnumerable<int> values)
        {

            foreach (var value in values)
            {
                this.Add(value);
            }
            _Count = -1;
            this.Normalize();
        }
        public void AddRange(IEnumerable<int> values)
        {
            var t = this;
            var ivalues = values as IntervalList;
            if (ivalues != null && ivalues.Intervals.Count > 25)
            {
                t.AddRange(ivalues);
                return;
            }
            foreach (var value in values)
            {
                this.Add(value);
            }
            _Count = -1;
        }
        public void AddRange_X(IEnumerable<int> values)
        {
            var t = this.Copy();
            var t2 = this.Copy();
            //var t = this;
            //var sb = new StringBuilder();
            //sb.AppendLine(t.GetString());
            //sb.AppendLine();

            var ivalues = values as IntervalList;
            if (ivalues != null)
            {
                t2.AddRange(ivalues);
                //return;
            }

            //_Count = -1;
            foreach (var value in values)
            {
                this.Add(value);
            }
            _Count = -1;

            if (this.Count != t2.Count)
            {
                //    var bigger = this.Count > t.Count ? this : t;
                //    var smaller = bigger == this ? t : this;
                //    var diff = Utilities.Objects.SortedExcept(bigger, smaller);
            }

        }
        public void AddRange(IntervalList values)
        {
            var merged = Utilities.Objects.MergeSorted_New(this, values, null);
            //merged.Normalize();
            this.Clear();
            this.Intervals = merged.Intervals;
            //foreach (var interval in merged.Intervals)
            //{
            //    this.Intervals.Add(interval);
            //}
            //_Count = -1;

        }
        public static IntervalComparer ic = new IntervalComparer();
        public static IntervalStartComparer startcomparer = new IntervalStartComparer();
        public static IntervalEndComparer endcomparer = new IntervalEndComparer();
        private int LastAdded = -1;
        public void AddX(int value)
        {

            if (LastAdded == value ){
                return;
            }
            LastAdded = value;
            _Count = -1;
            if (LastInterval == null)
            {
                AddNewInterval(value, 0);
                return;

            }
            //if (value == LastInterval.End)
            //{
            //    return;
            //}
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
            if (FirstInterval.IsPrev(value))
            {
                FirstInterval.Start--;
                return;
            }
            if (value < FirstInterval.Start) 
            {
                AddNewInterval(value,0);
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
            _LastInterval = null;
            _Count = -1;
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
        //public int SearchByStart(Interval interval, int startix)
        //{
        //    var ix=0;
        //    var c = this.Intervals.Count;

        //    var tix = this.Intervals.BinarySearch(startix, c - startix, interval, IntervalList.startcomparer);
        //    if (tix < 0) 
        //    {
        //        ix = ~tix;
        //    }
        //}
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

        public IntervalList Copy()
        {
            var il = new IntervalList();
            foreach (var i in this.Intervals) 
            {
                il.Intervals.Add(i.Copy());
            }
            return il;
        }
        public bool IsConsistent() 
        {
            var max = -1;
            foreach (var i in Intervals) 
            {
                if (i.End == 172) 
                {

                }
                if (i.Start < max) 
                {
                    return false;
                }
                if (i.End > max) 
                {
                    max = i.End;
                }
            }
            return true;
        }

        public void Normalize()
        {
            var ic1 = this.Intervals.Count;
            if (this.Intervals.Count > 1)
            {
                var il = new IntervalList();
                foreach (var i in this.AsEnumerable())
                {
                    il.Add(i);
                }

                this.Intervals = il.Intervals;
            }
            _Count = -1;
            if (this.Intervals.Count != ic1) 
            {

            }
            //foreach (var interval in this.Intervals)
            //{ 
            //    il.AddInterval(interval);
            //}
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
            var ls = new IntervalList();
            ls.Intervals = new List<Interval>() { 
                new Interval(6337,6384)
            };
            var lb = new IntervalList();
            lb.Intervals = new List<Interval>() { 
                new Interval(131 , 147),
new Interval(149 , 169),
new Interval(158 , 165),
new Interval(196 , 198),
new Interval(206 , 206),
new Interval(232 , 232),
new Interval(238 , 240),
new Interval(609 , 672),
new Interval(801 , 928),
new Interval(993 , 1056),
new Interval(1121 , 1184),
new Interval(1313 , 1440),
new Interval(1921 , 2400),
new Interval(2737 , 3072),
new Interval(3193 , 3312),
new Interval(3433 , 3552),
new Interval(3793 , 4032),
new Interval(4065 , 4096),
new Interval(4577 , 5056),
new Interval(5121 , 5184),
new Interval(5217 , 5248),
new Interval(5281 , 5312),
new Interval(5345 , 5376),
new Interval(5385 , 5392),
new Interval(5409 , 5424),
new Interval(5433 , 5440),
new Interval(5449 , 5456),
new Interval(5473 , 5488),
new Interval(5513 , 5536),
new Interval(5545 , 5552),
new Interval(5569 , 5584),
new Interval(5593 , 5600),
new Interval(5609 , 5616),
new Interval(5633 , 5648),
new Interval(5673 , 5696),
new Interval(5705 , 5712),
new Interval(5729 , 5744),
new Interval(5753 , 5760),
new Interval(5769 , 5776),
new Interval(5793 , 5808),
new Interval(5833 , 5856),
new Interval(5865 , 5872),
new Interval(5889 , 5904),
new Interval(5913 , 5920),
new Interval(5929 , 5936),
new Interval(5953 , 5968),
new Interval(5993 , 6016),
new Interval(6033 , 6048),
new Interval(6081 , 6112),
new Interval(6129 , 6144),
new Interval(6161 , 6176),
new Interval(6209 , 6240),
new Interval(6289 , 6336),
new Interval(6361 , 6384),
new Interval(6433 , 6480),
new Interval(6505 , 6528),
new Interval(6553 , 6576),
new Interval(6625 , 6672),
new Interval(6745 , 6816),
new Interval(6865 , 6912),
new Interval(7033 , 7152),
new Interval(7169 , 7184),
new Interval(7193 , 7200),
new Interval(7209 , 7216),
new Interval(7225 , 7232),
new Interval(7233 , 7400),
new Interval(7401 , 7554),
new Interval(7555 , 12416),
new Interval(12417 , 13045),
new Interval(13046 , 13572),
new Interval(13573 , 22572),
new Interval(29573 , 34072),
new Interval(37573 , 39822),
new Interval(52823 , 109072),
new Interval(109323 , 109353),
new Interval(109354 , 109359),
new Interval(109392 , 110177),
new Interval(110178 , 110188),
new Interval(110225 , 110640),
new Interval(110677 , 110794),
new Interval(110807 , 110853),
new Interval(110950 , 111262),
new Interval(111275 , 111679),
new Interval(111692 , 111937),
new Interval(111950 , 111988),
new Interval(118085 , 118108),
new Interval(118111 , 118130),
new Interval(118131 , 118170),
new Interval(118175 , 118178),

            };
            var result3= Utilities.Objects.SortedExcept(ls, lb, null);

        }
        public void mergetest() 
        {
            IntervalList list1 = new IntervalList();
            list1.Intervals=new List<Interval>(){ 
                new Interval(10,12),
                new Interval(20,50),
                new Interval(60,70),
                new Interval(71,80),
                new Interval(100,120),
                new Interval(180,210)
            };

            IntervalList list2 = new IntervalList();
            list2.Intervals = new List<Interval>(){ 
                new Interval(11,15),
                new Interval(16,20),
                new Interval(21,25),
                new Interval(30,130),
                new Interval(200,202),
                new Interval(205,208)
            };

            IntervalList result = Utilities.Objects.MergeSorted_New(list1, list2, null);

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
            var list3 = new IntervalList();
            list3.Intervals.Add(new Interval(0, 5));
            list3.Intervals.Add(new Interval(7, 9));
            list3.Intervals.Add(new Interval(11));
            list3.Intervals.Add(new Interval(12));

            var list4 = new IntervalList();
            list4.Intervals.Add(new Interval(-1, 5));
            list4.Intervals.Add(new Interval(6, 8));


            var list5 = new IntervalList();
            list5.Intervals.Add(new Interval(-1, 6));
            list5.Intervals.Add(new Interval(7, 9));

            var list6 = new IntervalList();
            list6.Intervals.Add(new Interval(0, 5));
            list6.Intervals.Add(new Interval(7, 9));

            var list7 = new IntervalList();
            list7.Intervals.Add(new Interval(0, 6));
            list7.Intervals.Add(new Interval(11, 12));

            var result = Utilities.Objects.MergeSorted(list1, list2, null);
            var result2 = Utilities.Objects.MergeSorted(list2, list1, null);

            var results = new List<IntervalList>();
            results.Add(Utilities.Objects.MergeSorted(list3, list4, null));
            results.Add(Utilities.Objects.MergeSorted(list3, list5, null));
            results.Add(Utilities.Objects.MergeSorted(list3, list6, null));
            results.Add(Utilities.Objects.MergeSorted(list3, list7, null));

        }
    }
}
