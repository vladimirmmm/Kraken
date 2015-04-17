using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    public class Dimension
    {
        public Dimension Parent = null;
        public XbrlValue Value { get; set; }
        public XbrlValue QName { get; set; }
        public XbrlValue ScenarioItem { get; set; }
        public List<Dimension> Dimensions = new List<Dimension>();

    }
}
