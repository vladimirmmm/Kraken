using LogicalModel;
using LogicalModel.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.InstanceModel
{
    public class Scenario
    {
        private List<Dimension> _Dimensions = new List<Dimension>();
        [JsonIgnore]
        public List<Dimension> Dimensions { get { return _Dimensions; } set { _Dimensions=value; } }

        private string _FactString = "";
        [JsonProperty]
        public string FactString
        {
            get
            {
                if (String.IsNullOrEmpty(_FactString))
                {
                    _FactString = GetFactString();
                }
                return _FactString;
            }
            set
            {
                _FactString = value;
                SetFromString(value);
            }
        }

        public string GetFactString()
        {
            var sb = new StringBuilder();
            foreach (var dimension in Dimensions)
            {
                sb.Append(dimension.DomainMemberFullName + ",");//|
            }
            return sb.ToString();
        }

        public void SetFromString(string item)
        {
            this.Dimensions.Clear();
            var parts = item.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var toskip = 0;
            var dimparts = parts.Skip(toskip).ToList();
            foreach (var dimpart in dimparts)
            {
                var dimitem = Utilities.Strings.TextBetween(dimpart, "[", "]");
                var domainpart = dimpart.Substring(dimitem.Length + 2);
                var domain = domainpart;
                var member = "";
                var dim = new Dimension();

                if (domainpart.Contains(":"))
                {
                    var domainparts = domainpart.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                    if (domainparts.Length == 2)
                    {
                        domain = domainparts[0];
                        member = domainparts[1];
                    }
                    if (domainparts.Length == 3)
                    {
                        domain = String.Format("{0}:{1}", domainparts[0], domainparts[1]);
                        member = domainparts[2];
                        dim.IsTyped = true;
                    }
                }
                dim.DimensionItem = dimitem;
                dim.Domain = domain;
                dim.DomainMember = member;
                this.Dimensions.Add(dim);
            }
        }

        public string ToXmlString() 
        {
            var sb = new StringBuilder();
            sb.AppendLine("<scenario>");
            foreach (var dimension in Dimensions) 
            {
                sb.AppendLine(dimension.ToXmlString());
            }
            sb.AppendLine("</scenario>");
            return sb.ToString();
        }
    }
}
