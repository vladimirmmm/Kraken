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

        public string ToXmlString() 
        {
            var sb = new StringBuilder();
            sb.AppendLine(String.Format("<xbrli:context id=\"{0}\">", this.ID));
            sb.AppendLine(Entity.ToXmlString());
            sb.AppendLine(Period.ToXmlString());
            sb.AppendLine(Scenario.ToXmlString());
            sb.AppendLine("</xbrli:context>");

            return sb.ToString();
        }
    }
}
