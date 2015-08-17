using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    public class Period
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? Instant { get; set; }
        public Boolean Forever { get; set; }



        public override string ToString()
        {
            return String.Format("{0:yyyy-MM-dd}", Instant);
        }

        public string ToXmlString()
        {
            var sb = new StringBuilder();
            if (Instant.HasValue)
            {
                /*<xbrli:period>
			        <xbrli:instant>2015-06-30</xbrli:instant>
		        </xbrli:period>*/
                sb.AppendLine("<xbrli:period>");
                sb.AppendLine(String.Format("<xbrli:instant>{0:yyyy-MM-dd}</xbrli:instant>", Instant));
                sb.AppendLine("</xbrli:period>");
            }
            return sb.ToString();
        }
    }
}
