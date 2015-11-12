using LogicalModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    public class InstanceUnit:Identifiable
    {
        public QualifiedName Measure { get; set; }

        public String ItemType { get; set; }
        public String Name { get; set; }

        public override string ToString()
        {
            return String.Format("{0} - Measure: {1}", base.ToString(), Measure);
        }

        public string NSLink { get; set; }

        public string ToXmlString()
        {
            var sb = new StringBuilder();
            /*
             <xbrli:unit id="U_1">
		<xbrli:measure>iso4217:EUR</xbrli:measure>
	</xbrli:unit>
             */
            sb.AppendLine(String.Format("<xbrli:unit id=\"{0}\">", this.ID));
            sb.AppendLine(String.Format("<xbrli:measure>{0}</xbrli:measure>", this.Measure));
            sb.AppendLine("</xbrli:unit>");

            return sb.ToString();
        }
    }
}
