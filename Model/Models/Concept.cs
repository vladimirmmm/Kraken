using BaseModel;
using LogicalModel.Base;
using LogicalModel.Dimensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Concept : QualifiedName
    {
        [JsonProperty]
        public QualifiedName Domain { get; set; }
        
        private String _HierarchyRole = "";
        public String HierarchyRole { get { return _HierarchyRole; } set { _HierarchyRole = value; } }
        //public List<Dimension> PossibleDimensions = new List<Dimension>();
    }
}
