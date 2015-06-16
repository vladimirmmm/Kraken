using LogicalModel.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    //[JsonObject(MemberSerialization=MemberSerialization.]
    public class InstanceFact:FactBase
    {

        private String _FactString= "";
        public String FactString { get { return _FactString; } set { _FactString = value; } }

        private String _FactKey = "";
        public String FactKey { get { return _FactKey; } set { _FactKey = value; } }

        public int Decimals { get; set; }

        private String _Concept= "";
        public String Concept { get { return _Concept; } set { _Concept = value; } }

        private String _Value = "";
        public String Value { get { return _Value; } set { _Value = value; } }

        private List<String> _Cells = new List<string>();
        public List<String> Cells { get { return _Cells; } set { _Cells = value; } }

        public float Value_F { 
            get
            {
                if (String.IsNullOrEmpty(_Value)) { return 0; }
                return float.Parse(_Value, CultureInfo.InvariantCulture);
            }
            //set {
            //    _Value = String.Format("{0:0.##}", value); 
            //}
        }

        public String ContextID { get; set; }


        public Entity Entity { get; set; }
        public Unit Unit { get; set; }
        public String UnitID { get; set; }

        public override string ToString()
        {
            return String.Format("Value: {0}; FactString: {1};", Value, FactString);
        }
    }
}
