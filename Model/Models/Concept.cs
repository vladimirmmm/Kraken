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
        //public List<Dimension> PossibleDimensions = new List<Dimension>();
    }
}
