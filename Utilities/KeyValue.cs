using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class KeyValue<TKey,TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public KeyValue() 
        { 

        }
        public KeyValue(TKey key)
        {
            this.Key = Key;
        }

        public KeyValue(TKey key, TValue value)
        {
            this.Key = key;
            this.Value = value;
        }
        public override bool Equals(object obj)
        {
            var item = (KeyValue<TKey,TValue>)obj;

            if (item == null)
            {
                return false;
            }

            return this.Key.Equals(item.Key);
        }

        public override int GetHashCode()
        {
            return this.Key.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("{0} >> {1}", this.Key, this.Value);
        }
    }
    public class KeyValue
    {
        public string Key { get; set; }
        public object Value { get; set; }
        public KeyValue()
        {

        }
        public KeyValue(string key) 
        {
            this.Key = key;
        }

        public KeyValue(string key, object value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}
