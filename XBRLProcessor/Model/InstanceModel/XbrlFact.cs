using LogicalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.InstanceModel
{
    public class XbrlFact
    {
        public String Concept { get; set; }
        public String Decimals { get; set; }
        public String ContextRef { get; set; }
        public String UnitRef { get; set; }
        private String _Value = "";
        public String Value { get { return _Value;} set { _Value=value;} }
    }
}
