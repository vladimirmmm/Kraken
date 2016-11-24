using LogicalModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace LogicalModel.Models
{
    
    public class FactKeyManager2
    {
        public int maxdictnr = 10;
        public bool SaveToFile = true;
        public string DataFolder = "";
        public FactDictionaryCollection FactsOfPages = new FactDictionaryCollection();


        //public Action<FactDictionary2> ManageLoadedFacts = (f) => { };
        public Func<int[], int> GetFactIndex = (key) => -1;
        public Func<int> GetNewFactIndex = () => -1;
        public Func<int, int[]> GetFactKey = (index) => new int[] { -1 };
        public string GetPagePath(string pagekey)
        {
            return DataFolder + pagekey + ".dat";
        }

        public FactKeyManager2() 
        {
            GetFactIndex = FactsOfPages.Index;
            GetFactKey = FactsOfPages.Key;
        }

        public bool FactKeyExists(int[] factkey)
        {
            return FactsOfPages.ContainsKey(factkey);
        }
        public Utilities.KeyValue<int, List<int>> GetFact(int[] factkey)
        {
            return FactsOfPages.GetKvp(factkey);
        }
        public Utilities.KeyValue<int, List<int>> GetFact(int factindex)
        {
            var factkey = FactsOfPages.Key(factindex);
            return FactsOfPages.GetKvp(factkey);
        }
        public FactKeyWithCells GetFactKeywithCells(int factindex)
        {
            return FactsOfPages.GetFactKeyWithCells(factindex);
        }

  
        public int EnsureFact(int[] factkey,int[] hashkeys)
        {
            var ix = FactsOfPages.Save(factkey);
            //FactsOfPages.HashKeys.Add(hashkeys, ix);
            FactsOfPages.HashKeys.Add(new Tintint(hashkeys[0], hashkeys[1]), ix);
            return ix;
        }

        

        public void Load()
        {
            FactsOfPages.Clear();
 
            FactsOfPages.LoadPages();


        }
   
        public void SaveAll()
        {
             FactsOfPages.SavePages();
        }
        public IEnumerable<int> IndexesAsEnumerable()
        {
            return FactsOfPages.FactIndexes;
        }
        public IEnumerable<int[]> KeysAsEnumerable()
        {
            return FactsOfPages.FactKeysAsEnumerable;
        }
        public IEnumerable<Utilities.KeyValue<int[],List<int>>> FactsAsEnumerable()
        {
            return FactsOfPages.FactsAsEnumerable;
        }




        public void Clear()
        {
            this.FactsOfPages.Clear();
        }

        public int Count 
        {
            get { return FactsOfPages.Count; }
        }


        public int HasFact(int[] key)
        {
            var hashkey = FactsOfPages.GetHashKeys(key);
            return this.FactsOfPages.GetIndexByHashKeys(hashkey);
        }

        public int[] GetHashKeys(int[] key)
        {
            return FactsOfPages.GetHashKeys(key);
        }

        public int GetFactIndexByHashKey(int[] hashkeys)
        {
            return FactsOfPages.GetIndexByHashKeys(hashkeys);
        }
    }


  
    public class testx 
    {
       
    }
}
