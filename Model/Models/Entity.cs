using BaseModel;
using LogicalModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    public class Entity:Identifiable
    {
        public String Scheme { get; set; }


        public string ToXmlString()
        {
            /*
            <xbrli:entity>
			    <xbrli:identifier scheme="http://standard.iso.org/iso/17442">529900U5XAEYOT68AC40</xbrli:identifier>
		    </xbrli:entity>
             */
            var sb = new StringBuilder();
            sb.AppendLine("<xbrli:entity>");
            sb.AppendLine(String.Format("<xbrli:identifier scheme=\"{0}>{1}</xbrli:identifier>", Scheme, ID));
            sb.AppendLine("</xbrli:entity>");

            return sb.ToString();
        }
    }
}
