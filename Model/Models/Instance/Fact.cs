using LogicalModel.Base;
using LogicalModel.Validation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    [JsonObject(MemberSerialization=MemberSerialization.OptIn)]
    public class InstanceFact : FactBase //, IValueWithTreshold
    {

        private String _FactKey = "";
        [JsonProperty]
        public String FactKey { get { return _FactKey; } set { _FactKey = value; } }

        public string Decimals { get; set; }

        private String _Value = "";
        [JsonProperty]
        public String Value { get { return _Value; } set { _Value = value; } }

        private List<String> _Cells = new List<string>();
        [JsonProperty]
        public List<String> Cells { get { return _Cells; } set { _Cells = value; } }

        public decimal Value_F { 
            get
            {
                if (String.IsNullOrEmpty(_Value)) { return 0; }
                if (_Value.Length > 29 || !Utilities.Strings.IsDigitsOnly(_Value, '.', '-'))
                {
                    Console.WriteLine(String.Format("Invalid Value Detected: {0}", _Value));
                }
                else 
                {
                    return decimal.Parse(_Value);
                }
                return decimal.MaxValue;
            }
        }

        [JsonProperty]
        public String ContextID { get; set; }


        [JsonProperty]
        public Entity Entity { get; set; }
        [JsonIgnore]
        public Unit Unit { get; set; }
        [JsonProperty]
        public String UnitID { get; set; }

        public override string ToString()
        {
            return String.Format("Value: {0}; FactString: {1};", Value, FactString);
        }
        
    }
}
