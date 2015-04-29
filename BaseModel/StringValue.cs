using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel
{
    public class StringValue
    {
        public String Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}
