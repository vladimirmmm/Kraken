using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XBRLProcessor.Model.DefinitionModel.Formula
{

    public class ExplicitDimension:RuleDimension
    {
        private string _Dimension = "";
        public string Dimension { get { return _Dimension; } set { _Dimension = value; } }

        private List<Member> _Members = new List<Member>();
        public List<Member> Members { get { return _Members; } set { _Members = value; } }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0}[", _Dimension);

            for (int i = 0; i < Members.Count; i++)
            {
                sb.Append(Members[i].QName);
                if (i < Members.Count - 1)
                {
                    sb.Append("|");
                }
            }
            sb.Append("]");

            return sb.ToString();
        }
    }
}
