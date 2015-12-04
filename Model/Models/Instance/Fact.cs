using LogicalModel.Base;
using LogicalModel.Validation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

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

        [JsonProperty]
        public String ContextID { get; set; }


        [JsonProperty]
        public Entity Entity { get; set; }
        [JsonIgnore]
        public InstanceUnit Unit { get; set; }
        [JsonProperty]
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
            sb.AppendLine(String.Format("<{0} {2} contextRef=\"{3}\" {4}>{1}<{0}>",
                this.Concept.Content, this.Value, decimals, ContextID, unitref));

            return sb.ToString();
        }
        
    }
}
