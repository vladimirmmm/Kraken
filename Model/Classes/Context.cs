using LogicalModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    public class Context:Identifiable
    {
        public List<Dimension> Dimensions = new List<Dimension>();
        public Entity Entity { get; set; }
        public Period Period { get; set; }

        public override string ToString()
        {
            return String.Format(base.ToString());
        }
    }
}
