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

    public class IntervalComparer2 : IComparer<Interval>
    {
        public int Compare(Interval x, Interval y)
        {
            return x.Start - y.Start;
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
        public void AddInterval(Interval interval)
        {
            _Count = -1;
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
        public static IntervalComparer2 ic2 = new IntervalComparer2();
        public void AddX(int value)
        {
            _Count = -1;
            if (LastInterval == null)
            {
                AddNewInterval(value, 0);
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
        public void Add(int value)
        {
            AddX(value);
        }
        public void AddY(int value) 
        {
            _Count = -1;
            if (LastInterval == null)
            {
                AddNewInterval(value, 0);

            }
            else 
            {
               

                for (int i = this.Intervals.Count - 1; i > -1; i--) 
                {
                    var prev = i - 1 > 0 ? this.Intervals[i - 1] : null;
                    var next = i + 1 < this.Intervals.Count ? this.Intervals[i + 1] : null;
                    var interval = this.Intervals[i];
                    if (interval.IsNext(value))
                    {
                        if (next == null || next.Start > interval.End + 1) 
                        {
                            interval.End++;
                            return;
                        }
                    }
                    if (interval.IsInThis(value))
                    {
                        return;
                    }
                    if (interval.IsPrev(value))
                    {
                        if (prev == null || prev.End < interval.Start - 1)
                        {
                            interval.Start--;
                            return;
                        }
                    }
                    if (next == null && value > interval.End) 
                    {
                        AddNewInterval(value, Intervals.Count);
                    }
                    if (prev == null && value < interval.Start)
                    {
                        AddNewInterval(value, 0);
                    }
                }
            }
        }

        public void AddOld(int value)
        {
            _Count = -1;
            if (LastInterval == null)
            {
                AddNewInterval(value, 0);

            }

            if (value == LastInterval.End + 1)
            {
                LastInterval.End++;
            }
            else
            {
                if (value > LastInterval.End)
                {
                    AddNewInterval(value, Intervals.Count);
                }
                else
                {
                    var ix = 0;
                    foreach (var interval in Intervals)
                    {
                        if (interval.Start - 1 == value)
                        {
                            interval.Start--;
                            break;
                        }
                        //already exists
                        if (interval.Start >= value && value <= interval.End)
                        {
                            return;
                        }
                        //next of the current interval
                        if (value > interval.End)
                        {
                            if (value == interval.End + 1)
                            {
                                if (Intervals[ix + 1].Start == value) { break; }

                                LastInterval.End++;
                            }
                            else
                            {
                                if (Intervals[ix + 1].Start > value)
                                {
                                    AddNewInterval(value, ix + 1);

                                    break;
                                }

                            }
                        }

                        if (value < interval.Start)
                        {
                            break;
                        }
                        ix++;
                    }
                }
            }
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
    }

    public class TestInterval
    {
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
    }
}
