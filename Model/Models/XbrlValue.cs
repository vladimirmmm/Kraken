using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    public class XbrlValue
    {
        public String FullValue { get; set; }
        public String Namespace { get; set; }
        public String Value { get; set; }

        public XbrlValue(string FullValue) 
        {
            this.FullValue = FullValue;
        }

        public override string ToString()
        {
            return String.Format("{0}:{1}", Namespace, Value);
        }

    }
}
