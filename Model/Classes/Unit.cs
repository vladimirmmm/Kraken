using LogicalModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    public class Unit:Identifiable
    {
        public XbrlValue Measure { get; set; }

        public override string ToString()
        {
            return String.Format("{0} - Measure: ", base.ToString(), Measure);
        }
    }
}
