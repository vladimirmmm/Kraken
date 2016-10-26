using BaseModel;
using LogicalModel;
using Model.InstanceModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBRLProcessor.Model.InstanceModel
{
    public class XbrlContext : Identifiable
    {
        public Entity Entity { get; set; }
        public Period Period { get; set; }
        [JsonIgnore]
        public Scenario Scenario { get; set; }

        public string ToXmlString()
        {
            var sb = new StringBuilder();
            var tab = LogicalModel.Literals.Tab;
            sb.AppendLine(String.Format(tab + "<xbrli:context id=\"{0}\">", this.ID));
            sb.AppendLine(Entity.ToXmlString(tab + tab));
            sb.AppendLine(Period.ToXmlString(tab + tab));
            sb.AppendLine(Scenario.ToXmlString(tab + tab));
            sb.AppendLine(tab + "</xbrli:context>");

            return sb.ToString();
        }
    }
}
