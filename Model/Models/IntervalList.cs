using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{

    public class Interval
    {
        public int Start = -1;
        private int _End = -1;
        public int End { get { return _End == -1 ? Start : _End; } set { _End = value; } }

        public Interval() 
        {

        }
        public Interval(int start)
        {
            this.Start = start;
        }
        public Interval(int start, int end)
        {
            this.Start = start;
            this.End = end;
        }

        public IEnumerable<int> AsEnumerable() 
        {
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
    }

    public class IntervalList : List<int> 
    {
        protected List<Interval> Intervals = new List<Interval>();
        protected Dictionary<int, Interval> StartValues = new Dictionary<int, Interval>();
        public Interval LastInterval 
        {
            get { return Intervals.LastOrDefault(); }
        }

        public Interval FirstInterval
        {
            get { return Intervals.FirstOrDefault(); }
        }
        private void AddNewInterval(int value,int ix) 
        {
            var interval = new Interval(value);
            this.Intervals.Insert(ix, interval);
        }
        public new void Add(int value) 
        {
            if (LastInterval == null) 
            {
                AddNewInterval(value,0);

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
                        if (interval.Start>=value && value <= interval.End) 
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
                                if (Intervals[ix + 1].Start > value) {
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
        public new int Count 
        {
            get 
            {
                var c = 0;
                foreach (var interval in Intervals) 
                {
                    c += interval.Count;
                }
                return c;
            }
        }
        public string GetString() 
        {
            var sb = new StringBuilder();
            foreach(var interval in Intervals)
            {
                sb.AppendLine(String.Format("{0} - {1}",interval.Start,interval.End));
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
    }
    public class TestInterval
    {
        public void Test()
        {
             IntervalList list = new IntervalList();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(1);
            list.Add(4);
            list.Add(10);
            list.Add(9);
            list.Add(7);
            list.Add(11);
            list.Add(4);
            list.Add(21);
            list.Add(22);
            list.Add(23);
            list.Add(22);
            list.Add(18);
            var s = list.GetString();
        }
    }
}
