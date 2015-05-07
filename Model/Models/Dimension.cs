using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    public class Dimension
    {
        public String DimensionItem { get; set; }
        public String Domain { get; set; }
        public String DomainMember { get; set; }

        public string DomainMemberFullName 
        {
            get { return String.Format("[{0}]{1}:{2}", DimensionItem, Domain, DomainMember); }
        }

        public string DimensionItemFullName
        {
            get { return String.Format("{0}", DimensionItem); }
        }

        public override string ToString()
        {
            return String.Format("[{0}]{1}:{2}", DimensionItem, Domain, DomainMember);
        }

        public override bool Equals(object obj)
        {
            var item = obj as Dimension;
            if (item != null)
            {
                return this.DomainMemberFullName == item.DomainMemberFullName;
            }
            return false;
        }

        public bool IsDefaultMemeber 
        {
            get { return DomainMember == "x0"; }
        }

        public override int GetHashCode()
        {
            return this.DomainMemberFullName.GetHashCode();
        }
    }
}
