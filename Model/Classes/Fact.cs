using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    public class Fact
    {
        public List<Table> Tables = new List<Table>();
        public XbrlValue Concept { get; set; }
        public int Decimals { get; set; }

        public Object Value { get; set; }
        public float Value_N { get; set; }
        public float Value_T { get; set; }

        public Context Context { get; set; }
        public String ContextID { get; set; }


        public Unit Unit { get; set; }
        public String UnitID { get; set; }

        public override string ToString()
        {
            return String.Format("Concept: {0}; Context: {1}; Val:{2};", Concept, Context, Value);
        }
    }
}
