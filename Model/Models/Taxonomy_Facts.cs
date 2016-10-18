using BaseModel;
using LogicalModel.Base;
using LogicalModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    public partial class Taxonomy :DocumentCollection
    {
        public FactKeyManager2 FactsManager = new FactKeyManager2();

        public void ManageLoadedFacts(FactDictionary facts) 
        {
            foreach (var fact in facts) 
            {
                SetRelatedDictionaries(fact.Key);
            }
        }
        public void ManageLoadedFacts(FactDictionary2 facts)
        {
            foreach (var fact in facts)
            {
                SetRelatedDictionaries(fact.Key);
            }
        }
        public void SetRelatedDictionaries(int[] key) 
        {
            //LoadFactToFactsOfParts(key);
        }
        public void SetRelatedDictionaries(int index)
        {
            var key = FactsManager.GetFactKey(index);
            SetRelatedDictionaries(key);
            //LoadFactToFactsOfParts(key);
        }

        public void AddFactKey(int[] key)
        {
            FactsManager.EnsureFact(key);
            SetRelatedDictionaries(key);


        }
        public void AddFactKey(List<int> key)
        {
            AddFactKey(key.ToArray());


        }
        public void LoadFactToFactsOfParts(int[] key)
        {
            foreach (var part in key)
            {
                var factix = FactsManager.GetFactIndex(key);
                var dimensiondomainpart = GetDimensionDomainPart(part);
                if (dimensiondomainpart != -1)
                {
                    FactsOfParts.AddIfNotExists(dimensiondomainpart, factix);
                }
                FactsOfParts.AddIfNotExists(part, factix);
            }
        }
        public IQueryable<int[]> GetKeysFactsAsQuearyable()
        {
            return FactKeysAsEnumerable().AsQueryable();
        }
        public IQueryable<Utilities.KeyValue<int[],List<int>>> FactsAsQuearyable()
        {
            return FactsAsEnumerable().AsQueryable();
        }
        public IEnumerable<int[]> FactKeysAsEnumerable()
        {
            return FactsManager.KeysAsEnumerable();
        }
        public IEnumerable<Utilities.KeyValue<int[], List<int>>> FactsAsEnumerable()
        {
            return FactsManager.FactsAsEnumerable();
        }
        public bool HasFact(string factkey)
        {
            var ids = GetFactIntKey(factkey).ToArray();

            return HasFact(ids);
        }
        public bool HasFact(int[] factkey)
        {
            return FactsManager.FactKeyExists(factkey);
        }

        public void ClearFacts()
        {
            this.FactsManager.Clear();
            this.Facts.Clear();
        }
        public bool HasFact(IEnumerable<int> factkey)
        {
            return HasFact(factkey.ToArray());
        }

        public List<string> GetCellsOfFact(string factkey)
        {
            if (factkey.Contains(":"))
            {
                factkey = GetFactStringKeyFromStringKey(factkey);
            }
            var ids = GetFactIntKey(factkey).ToArray();
            return GetCellsOfFact(ids);
        }
        public List<string> GetCellsOfFact(int[] factintkey)
        {
            var cellixs = FactsManager.GetFact(factintkey).Value;
            var cells = new List<string>();
            foreach (var cellix in cellixs)
            {
                var cellstr = CellIndexDictionary.ContainsKey(cellix) ? CellIndexDictionary[cellix] : "?";
                cells.Add(cellstr);
            }
            return cells;
        }
        public void AddCellToFact(int index, int cellix, StringBuilder sb)
        {
            FactsManager.FactsOfPages[index].Add(cellix);
        }
        public void AddCellToFact(int[] key, int cellix,StringBuilder sb ) 
        {
            if (sb != null) 
            {
                sb.AppendLine(this.CellIndexDictionary[cellix] + "" + GetFactStringKey(key));
            }
            FactsManager.FactsOfPages[key].Add(cellix);
     
        }
        public List<string> GetCellsOfFact(IEnumerable<int> factkey)
        {

            return GetCellsOfFact(factkey.ToArray());
        }
    }
    /*
    public partial class Taxonomy : DocumentCollection
    {
        public void AddFactKey(int[] key)
        {
            Facts.Add(key, new List<String>());
            //Facts.Add(key, new List<Int64>());
            EnsureFactIndex(key);
            LoadFactToFactsOfParts(key);


        }
        public void AddFactKey(List<int> key)
        {
            AddFactKey(key.ToArray());


        }

        public IQueryable<KeyValuePair<int[], List<string>>> GetFactsAsQuearyable()
        {
            return this.Facts.AsQueryable();
        }

        public IEnumerable<KeyValuePair<int[], List<string>>> FactsAsEnumerable()
        {
            return this.Facts.AsEnumerable();
        }

        public bool HasFact(string factkey)
        {
            var ids = GetFactIntKey(factkey).ToArray();

            return this.Facts.ContainsKey(ids);
        }
        public bool HasFact(int[] factkey)
        {
            return this.Facts.ContainsKey(factkey);
        }

        public void ClearFacts()
        {
            this.Facts.Clear();
        }
        public bool HasFact(IEnumerable<int> factkey)
        {
            return this.Facts.ContainsKey(factkey.ToArray());
        }

        public List<string> GetCellsOfFact(string factkey)
        {
            if (factkey.Contains(":"))
            {
                factkey = GetFactStringKeyFromStringKey(factkey);
            }
            var ids = GetFactIntKey(factkey).ToArray();
            return GetCellsOfFact(ids);
        }
        public List<string> GetCellsOfFact(int[] factintkey)
        {

            return this.Facts[factintkey];
        }
        public List<string> GetCellsOfFact(IEnumerable<int> factkey)
        {

            return GetCellsOfFact(factkey.ToArray());
        }
    }
     * */
}
