using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    public class Dimension
    {
        public String Value { get; set; }
        public String Domain { get; set; }
        public String DomainMember { get; set; }

        public string Content 
        {
            get { return String.Format("{0}:{1}", Domain, Value); }
        }

        public override string ToString()
        {
            return String.Format("[{0}]{1}:{2}", Value, Domain, DomainMember);
        }
    }
}
