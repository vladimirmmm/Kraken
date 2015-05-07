using LogicalModel.Dimensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    public enum HyperCubeRelation 
    {
        All=1,
        NotAll=2,
    }
    public class HyperCube
    {
        private List<Concept> _Concepts = new List<Concept>();
        public List<Concept> Concepts 
        {
            get { return _Concepts; }
            set { _Concepts = value; }
        }

        private List<DimensionItem> _DimensionItems = new List<DimensionItem>();
        public List<DimensionItem> DimensionItems
        {
            get { return _DimensionItems; }
            set { _DimensionItems = value; }
        }

        public HyperCubeRelation Relation { get; set; }

        internal void SetReferences()
        {
            foreach (var dimitem in DimensionItems) 
            {
                dimitem.SetReferences();
            }
        }

        public bool HasDimension(Dimension dimension) 
        {
            var dimensionitem = DimensionItems.FirstOrDefault(i => i.FullName == dimension.DimensionItem);
            if (dimensionitem != null) 
            {
                var domain = dimensionitem.Domains.FirstOrDefault(i => i.ID == dimension.Domain);
                if (domain != null) 
                {
                    return domain.DomainMembers.Any(i => i.Name == dimension.DomainMember);
                }
            }
            return false;
        }

        public List<Dimension> NotIn(List<Dimension> dimensions, List<Dimension> zdimensions)
        {
            var result = new List<Dimension>();
            foreach (var dim in this.DimensionItems) 
            {
                var existing = dimensions.FirstOrDefault(i => i.DimensionItem == dim.FullName);
                if (existing == null) 
                {
                    var zdimension = zdimensions.FirstOrDefault(i => i.DimensionItemFullName == dim.FullName);
                    if (zdimension == null)
                    {
                        //check for default member
                        var hasdefaultmember = false;
                        foreach (var domain in dim.Domains)
                        {
                            var defaultmember = domain.DomainMembers.Where(i => i.IsDefaultMember).FirstOrDefault();
                            if (defaultmember != null)
                            {
                                hasdefaultmember = true;
                                break;
                            }
                        }
                        if (!hasdefaultmember)
                        {
                            //
                            var dimension = new Dimension();
                            dimension.DimensionItem = dim.Name;
                            dimension.Domain = dim.Namespace;
                            result.Add(dimension);
                        }
                    }
                }
            }
            return result;

        }

        public string ToFullString() 
        {
            var sb = new StringBuilder();
            foreach (var concept in Concepts)
            {
                sb.Append(concept + " ");

            }
            sb.AppendLine();
            foreach (var dimitem in DimensionItems) 
            {
                sb.AppendLine("" + dimitem.Name);

                foreach (var domain in dimitem.Domains)
                {
                    sb.AppendLine("     " + domain.Name);

                    foreach (var member in domain.DomainMembers)
                    {
                        sb.AppendLine("         " + member.Name);
                    }
                }
            }
            sb.AppendLine();
            return sb.ToString();
        }

    }
}
