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
        int[] Key(int index);
        IEnumerable<int[]> Keys { get; }
        int Save(int[] key);
        int Save(int[] key, System.Collections.Generic.List<int> value);
        System.Collections.Generic.List<int> this[int index] { get; }
        System.Collections.Generic.List<int> this[int[] key] { get; }
    }

    public class testxf
    {
        public void testx() 
        {
            Dictionary<ushort[], int> HashKeys = new Dictionary<ushort[], int>(new Utilities.Int16ArrayEqualityComparer());
            int i1 = 0;
            int i2 = 17000000;
            int total = 16000000;
            int col = 0;
            Random r = new Random();
            for (int i = 0; i < total; i++)
            {
                UInt16 k1 = (UInt16)((r.Next(0,UInt16.MaxValue)) % UInt16.MaxValue);
                UInt16 k2 = (UInt16)((r.Next(0, UInt16.MaxValue)) % UInt16.MaxValue);
                var key = new ushort[] { k1, k2 };

                if (!HashKeys.ContainsKey(key))
                {
                    HashKeys.Add(key, i);

                }
                else
                {
                    HashKeys[key] = -1;
                    col++;
                }
            }
            int z = 0;
        }
        public void test0()
        {
            Dictionary<UInt16,Dictionary<UInt16,int>> HashKeys = new Dictionary<UInt16,Dictionary<UInt16,int>>();
            int i1 = 0;
            int i2 = 17000000;
            int total = 16000000;
            for (int i = 0; i < total; i++)
            {
                UInt16 k1 = (UInt16)((i1 + i) % UInt16.MaxValue);
                UInt16 k2 = (UInt16)((i2 + i) % UInt16.MaxValue);
                if (!HashKeys.ContainsKey(k1)) 
                {
                    HashKeys.Add(k1, new Dictionary<UInt16, int>());
                }
                var subdict = HashKeys[k1];
                if (!subdict.ContainsKey(k2))
                {
                    subdict[k2] = i;
                }
                else 
                {
                    Console.WriteLine("collision");
                }
            }
            int z = 0;
        }
        public void test1()
        {
            Dictionary<int[], int> HashKeys = new Dictionary<int[], int>(new Utilities.IntArrayEqualityComparer());
            int i1 = 0;
            int i2 = 17000000;
            int total = 16000000;
            for (int i = 0; i < total; i++) 
            {
                HashKeys.Add(new int[] { i1 + i, i2 + i }, i);
            }
            int z = 0;
        }

        public void test2()
        {
            Dictionary<int, int> HashKeys = new Dictionary<int, int>();
            int i1 = 0;
            int i2 = 17000000;
            int total = 16000000;
            var r = new Random();
            var col = 0;
            for (int i = 0; i < total; i++)
            {
                var key = r.Next(0, int.MaxValue);
                if (!HashKeys.ContainsKey(key))
                {
                    HashKeys.Add(key, i);
                }
                else { col++; }
            }
            int z = 0;
        }

        public void test3()
        {
            List<Tuple<int, int, int>> HashKeys =new List<Tuple<int, int, int>>();
            int i1 = 0;
            int i2 = 17000000;
            int total = 16000000;
            for (int i = 0; i < total; i++)
            {
                HashKeys.Add(new Tuple<int, int, int>(i1+i,i2+i,i));
            }
            int z = 0;
        }

        public void test4()
        {
            List<Tuple<int, int>>[] HashKeys = new List<Tuple<int, int>>[Int16.MaxValue];
            int i1 = 0;
            int i2 = 17000000;
            int total = 16000000;
            for (int i = 0; i < total; i++)
            {
                var k1 = (i1 + i) % Int16.MaxValue;
                var k2 = i2 + i;
                var dict = HashKeys[k1];
                if (dict == null) 
                {
                    HashKeys[k1] = new List<Tuple<int, int>>();
                }
                HashKeys[k1].Add(new Tuple<int,int>(k2, i));
            }
            foreach (var item in HashKeys) 
            {
                
            }
            int z = 0;
        }
    }
}
