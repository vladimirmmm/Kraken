using BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    public class InstanceContext:Identifiable
    {
        public Entity Entity { get; set; }
        public Period Period { get; set; }
        public List<Dimension> FactParts = new List<Dimension>();

        public string ToXmlString()
        {
            var sb = new StringBuilder();
            var tab = Literals.Tab;
            sb.AppendLine(String.Format(tab + "<xbrli:context id=\"{0}\">", this.ID));
            sb.AppendLine(Entity.ToXmlString(tab + tab));
            sb.AppendLine(Period.ToXmlString(tab + tab));
            //sb.AppendLine(Scenario.ToXmlString(tab + tab));
            sb.AppendLine(tab + "</xbrli:context>");

            return sb.ToString();
        }
    }
}
