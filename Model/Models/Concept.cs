using BaseModel;
using LogicalModel.Base;
using LogicalModel.Dimensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    public class Concept :QualifiedName
    {
        public QualifiedName Domain { get; set; }
        
        private String _HierarchyRole = "";
        public String HierarchyRole { get { return _HierarchyRole; } set { _HierarchyRole = value; } }
        //public List<Dimension> PossibleDimensions = new List<Dimension>();
    }
}
