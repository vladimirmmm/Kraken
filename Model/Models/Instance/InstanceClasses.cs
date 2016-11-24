using BaseModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    public class FilingIndicator : Identifiable
    {
        public bool Filed { get; set; }
    }
    public class InstanceDictionaryConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var ifd = value as InstanceFactDictionary;
            var sb = ifd.Serialize();
            writer.WriteRawValue(sb.ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(InstanceFactDictionary);
        }
    }
    public class InstanceFactDictionary
    {
        protected Instance Instance = null;
        private Dictionary<int, InstanceFact> _FactsByIndex = new Dictionary<int, InstanceFact>();
        [JsonProperty]
        public Dictionary<int, InstanceFact> FactsByIndex { get { return _FactsByIndex; } set { _FactsByIndex = value; } }

        private Dictionary<int, int> _InstIndexToTaxIndex = new Dictionary<int, int>();
        [JsonProperty]
        public Dictionary<int, int> InstIndexToTaxIndex { get { return _InstIndexToTaxIndex; } set { _InstIndexToTaxIndex = value; } }


        private Dictionary<int[], List<int>> _FactsByTaxonomyKey = new Dictionary<int[], List<int>>(new Utilities.IntArrayEqualityComparer());
        [JsonProperty]
        public Dictionary<int[], List<int>> FactsByTaxonomyKey { get { return _FactsByTaxonomyKey; } set { _FactsByTaxonomyKey = value; } }

        private Dictionary<int[], int> _FactsByInstanceKey = new Dictionary<int[], int>(new Utilities.IntArrayEqualityComparer());
        [JsonProperty]
        public Dictionary<int[], int> FactsByInstanceKey { get { return _FactsByInstanceKey; } set { _FactsByInstanceKey = value; } }

        public void AddFact(InstanceFact fact)
        {
            var ix = FactsByIndex.Count;
            fact.IX = ix;
            FactsByIndex.Add(ix, fact);
            //InstIndexToTaxIndex.Add(ix, this.Instance.Taxonomy.FactsManager.GetFactIndex(fact.TaxonomyKey));
            var instancecontext = Instance.Contexts.Items[fact.ContextID];
            var partcount = instancecontext.DimensionIds.Count + 1;
            var taxfactkeylist = new List<int>(partcount);
            var instfactkeylist = new List<int>(partcount);
            var conceptid = Instance.GetPartID(fact.Concept.Content);
            taxfactkeylist.Add(conceptid);
            instfactkeylist.Add(conceptid);
            if (instancecontext.DimensionIds != null)
            {

                foreach (var id in instancecontext.DimensionIds)
                {
                    instfactkeylist.Add(id);
                    var taxid = id;
                    if (Instance.CounterTypedFactMembers.ContainsKey(id)) 
                    {
                        taxid = Instance.CounterTypedFactMembers[id];
                    }
                    taxfactkeylist.Add(taxid);
                }

            }
            var taxfactkey = taxfactkeylist.ToArray();
            var instfactkey = instfactkeylist.ToArray();
            if (!this.FactsByInstanceKey.ContainsKey(instfactkey)) 
            {
                this.FactsByInstanceKey.Add(instfactkey, ix);
            }
            if (!this.FactsByTaxonomyKey.ContainsKey(taxfactkey))
            {
                this.FactsByTaxonomyKey.Add(taxfactkey, new List<int>());
            }
            this.FactsByTaxonomyKey[taxfactkey].Add(ix);
            fact.InstanceKey = instfactkey;
            fact.TaxonomyKey = taxfactkey;
        }

        public bool ContainsKey(string stringkey)
        {
            var key = Instance.GetFactIntKey(stringkey);
            return ContainsKey(key);
        }
        public bool ContainsKey(int[] key)
        {
            return FactsByTaxonomyKey.ContainsKey(key) ? true : FactsByInstanceKey.ContainsKey(key);
        }
        
        public List<InstanceFact> GetFacts(string stringkey)
        {
            var key = Instance.GetFactIntKey(stringkey);
            return GetFacts(key);

        }
        public List<InstanceFact> GetFacts(int[] key)
        {
            if (this.FactsByTaxonomyKey.ContainsKey(key))
            {
                var ixs = this.FactsByTaxonomyKey[key];
                var factlist = ixs.Select(i => FactsByIndex[i]).ToList();
                return factlist;
            }
            else
            {

                if (this.FactsByInstanceKey.ContainsKey(key))
                {
                    var ix = this.FactsByInstanceKey[key];
                    return new List<InstanceFact>(){ this.FactsByIndex[ix]};
                }
            }
            return new List<InstanceFact>();

        }

        public InstanceFact GetFact(string stringkey)
        {
            var key = Instance.GetFactIntKey(stringkey);
            return GetFact(key);

        }
        public InstanceFact GetFact(int[] key)
        {
            if (this.FactsByTaxonomyKey.ContainsKey(key))
            {
                var ixs = this.FactsByTaxonomyKey[key];
                var ix = ixs.FirstOrDefault();
                return this.FactsByIndex[ix];
            }
            else
            {

                if (this.FactsByInstanceKey.ContainsKey(key))
                {
                    var ix = this.FactsByInstanceKey[key];
                    return this.FactsByIndex[ix];
                }
            }
            return null;

        }
        public StringBuilder Serialize()
        {
            var sb = new StringBuilder();
            sb.AppendLine("{");
            sb.AppendLine("\"FactsByIndex\": {");
            var ix = 0;
            foreach (var item in FactsByIndex)
            {
                sb.Append("\"" + item.Key + "\":");
                sb.Append("{ \"Content\": \"");
                sb.Append(item.Value.Content);
                sb.Append("\"}");
                ix++;

                if (ix != FactsByIndex.Count)
                {
                    sb.AppendLine(",");
                }
                item.Value.Content = "";
            }
            sb.AppendLine("},");


            sb.AppendLine("\"FactsByTaxonomyKey\": {");
            ix = 0;
            foreach (var item in FactsByTaxonomyKey)
            {
                var key = Utilities.Strings.ArrayToString(item.Key, ",");
                sb.Append("\"" + key + "\":");
                sb.Append("[");
                var subitems = Utilities.Strings.ListToString(item.Value, ",");
                sb.Append(subitems);
                sb.Append("]");

                ix++;
                if (ix != FactsByTaxonomyKey.Count) 
                {
                    sb.AppendLine(",");

                }
            }
            sb.AppendLine("},");


   
            sb.AppendLine("\"FactsByInstanceKey\": {");
            ix = 0;
            foreach (var item in FactsByInstanceKey)
            {
                var key = Utilities.Strings.ArrayToString(item.Key, ",");
                sb.Append("\"" + key + "\":");
                sb.Append(item.Value);
                ix++;
                if (ix != FactsByInstanceKey.Count)
                {
                    sb.AppendLine(",");


                }
            }
            sb.AppendLine("}");

            sb.AppendLine("}");

            return sb;
        }

        public void Clear()
        {
            this.FactsByIndex.Clear();
            this.FactsByInstanceKey.Clear();
            this.FactsByTaxonomyKey.Clear();
        }

        public void SetInstance(Instance instance)
        {
            this.Instance = instance;
        }
    }
}
   
