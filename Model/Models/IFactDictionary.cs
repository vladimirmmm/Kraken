using System;
using System.Collections.Generic;
namespace LogicalModel.Models
{
    interface IFactDictionary
    {
        int Count {get;}
        bool ContainsIndex(int index);
        bool ContainsKey(int[] key);
        int Index(int[] key);
        System.Collections.Generic.KeyValuePair<int[], FactLookupValue> GetItem(int[] key);
        int[] Key(int index);
        IEnumerable<int[]> Keys { get; }
        int Save(int[] key);
        int Save(int[] key, System.Collections.Generic.List<int> value);
        System.Collections.Generic.List<int> this[int index] { get; }
        System.Collections.Generic.List<int> this[int[] key] { get; }
    }
}
