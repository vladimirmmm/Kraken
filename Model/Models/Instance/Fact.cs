using LogicalModel.Base;
using LogicalModel.Validation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace LogicalModel
{
    public class IstanceFactPart 
    {
        public int _ID = -1;
        public int ID {get{return _ID;} set{_ID=value;}}
        public String Member { get; set;}
    }
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class InstanceFact : FactBase //, IValueWithTreshold
    {

        private String _FactKey = "";
        [JsonIgnore]
        public String FactKey { get { return _FactKey; } set { _FactKey = value; } }
        
        private string _FactStringX = null;
        [JsonIgnore]
        public override string FactString { get { return _FactStringX; } set { _FactStringX = value; } }

        private int[] _InstanceKey = new int[0];
        [JsonIgnore]
        public int[] InstanceKey { get { return _InstanceKey; } set { _InstanceKey = value; } }

        private int[] _TaxonomyKey = new int[0];
        [JsonIgnore]
        public int[] TaxonomyKey { get { return _TaxonomyKey; } set { _TaxonomyKey = value; } }


        [JsonProperty]
        public string Content { get; set; }

        public void SetContent() 
        {
            var sb = new StringBuilder();
            /*
             * \"Decimals\": \"0\",   
             * \"Value\": \"-444.99\",    
             * \"IX\": 98508,    
             * \"Cells\": [      \"eba_tC_14.00<00000|11358|210>\"    ],  
             * \"ContextID\": \"CT_64398\",   
             * \"UnitID\": \"monetaryItemType\"  }
             */
            //sb.Append(IX); sb.Append("@");
            sb.Append(ContextID); sb.Append("@");
            sb.Append(UnitID); sb.Append("@");
            sb.Append(Decimals); sb.Append("@");
            sb.Append(Value); sb.Append("@");
            sb.Append(Utilities.Strings.EnumerableToString(Cells, ", ")); sb.Append("@");

            Content = sb.ToString();
        }

        [JsonIgnore]
        [DefaultValue("")]
        public string Decimals { get; set; }

        private String _Value = "";
        [JsonIgnore]
        public String Value { get { return _Value; } set { _Value = value; } }

        private int _IX = -1;
        [JsonIgnore]
        public int IX { get { return _IX; } set { _IX = value; } }

        private List<IstanceFactPart> _Parts = new List<IstanceFactPart>();
        public List<IstanceFactPart> Parts { get { return _Parts; } set { _Parts=value; } }

        private List<String> _Cells = new List<string>();
        [JsonIgnore]
        public List<String> Cells { get { return _Cells; } set { _Cells = value; } }

        public decimal Value_F { 
            get
            {
                if (String.IsNullOrEmpty(_Value)) { return 0; }
                if (_Value.Length > 29 || !Utilities.Strings.IsDigitsOnly(_Value, '.', '-'))
                {
                    var cells = Utilities.Strings.ArrayToString(Cells.ToArray());
                    Logger.WriteLine(String.Format("Invalid Value Detected: {0} Cells: {1}", _Value, cells));
                }
                else 
                {
                    //decimal.Parse("500,85", new NumberFormatInfo() { NumberDecimalSeparator = "," });
                    return decimal.Parse(_Value, new NumberFormatInfo() { NumberDecimalSeparator = "." });
                }
                return decimal.MaxValue;
            }
        }

        [JsonIgnore]
        public String ContextID { get; set; }

        [JsonIgnore]
        public String ID { get; set; }

        //[JsonProperty]
        //public Period Period { get; set; }

        //[JsonProperty]
        //public Entity Entity { get; set; }
        //[JsonIgnore]
        //public InstanceUnit Unit { get; set; }
        [JsonIgnore]
        public String UnitID { get; set; }

        public override string ToString()
        {
            return String.Format("Value: {0}; FactString: {1};", Value, FactString);
        }

        public string ToXmlString()
        {
            var sb = new StringBuilder();
            /*
           <eba_met:mi53 decimals="0" contextRef="CT_85" unitRef="U_1">0.00</eba_met:mi53>
	
             */
            var decimalformat = "decimals=\"{0}\"";
            var decimals = String.Format(decimalformat, this.Decimals);

            var unitref = "";
            if (!String.IsNullOrEmpty(UnitID))
            {
                unitref = String.Format("unitRef=\"{0}\"", UnitID);
            }
            sb.Append(String.Format(Literals.Tab +  "<{0} {2} contextRef=\"{3}\" {4}>{1}</{0}>",
                this.Concept.Content, this.Value, decimals, ContextID, unitref));

            return sb.ToString();
        }
        
    }
}
