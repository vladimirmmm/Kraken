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
        public QualifiedName Measure { get; set; }

        public String ItemType { get; set; }
        public String Name { get; set; }

        public override string ToString()
        {
            return String.Format("{0} - Measure: ", base.ToString(), Measure);
        }

        public string NSLink { get; set; }
    }
}
