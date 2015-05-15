using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    public class Fact
    {

        private String _FactString= "";
        public String FactString { get { return _FactString; } set { _FactString = value; } }

        public int Decimals { get; set; }

        private String _Concept= "";
        public String Concept { get { return _Concept; } set { _Concept = value; } }

        private String _Value = "";
        public String Value { get { return _Value; } set { _Value = value; } }

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
