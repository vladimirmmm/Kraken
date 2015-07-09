using LogicalModel;
using LogicalModel.Base;
using Model.DefinitionModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.InstanceModel
{
    public class Context:Identifiable
    {
        public Entity Entity { get; set; }
        public Period Period { get; set; }
        [JsonIgnore]
        public Scenario Scenario { get; set; }
    }
}
