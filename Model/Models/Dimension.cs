using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    public class Dimension
    {
        public Boolean IsTyped { get; set; }
        private String _DimensionItem = "";
        public String DimensionItem
        {
            get { return _DimensionItem; }
            set
            {
                _DimensionItem = value;
                SetDomainMemberFullName();

            }
        }

        private String _Domain= "";
        public String Domain
        {
            get { return _Domain; }
            set
            {
                _Domain = value;
                SetDomainMemberFullName();

            }
        }


        private String _DomainMember = "";
        public String DomainMember
        {
            get { return _DomainMember; }
            set
            {
                _DomainMember = value;
                SetDomainMemberFullName();
            }
        }

        private string _DomainMemberFullName = "";

        private void SetDomainMemberFullName() 
        {
            var setted = false;
            if (String.IsNullOrEmpty(DomainMember))
            {
                _DomainMemberFullName =  String.Format("[{0}]{1}", DimensionItem, Domain);
                setted = true;
            }
            if (String.IsNullOrEmpty(Domain))
            {
                _DomainMemberFullName = String.Format("[{0}]{1}", DimensionItem, DomainMember);
                setted = true;
            }
            if (!setted)
            {
                _DomainMemberFullName = String.Format("[{0}]{1}:{2}", DimensionItem, Domain, DomainMember);
            }
        }
        public string DomainMemberFullName 
        {
            get 
            {
                return _DomainMemberFullName;
            }
        }
        public string DimensionItemWithDomain
        {
            get { return String.Format("[{0}]{1}", DimensionItem, Domain); }
        }

        public string DimensionItemFullName
        {
            get { return String.Format("{0}", DimensionItem); }
        }

        public override string ToString()
        {
            return String.Format("[{0}]{1}:{2}", DimensionItem, Domain, DomainMember);
        }

        public string ToStringForKey() 
        {
            if (this.IsTyped) 
            {
                return String.Format("[{0}]{1}", this.DimensionItem, this.Domain);
            }
            return String.Format("[{0}]{1}:{2}", this.DimensionItem, this.Domain, this.DomainMember);
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

        public string ToXmlString()
        {
            var sb = new StringBuilder();
            if (!this.IsTyped)
            {
                //<xbrldi:explicitMember dimension="eba_dim:LIQ">eba_LQ:x72</xbrldi:explicitMember>
                sb.AppendLine(String.Format("<xbrldi:explicitMember dimension=\"{0}\">{1}:{2}</xbrldi:explicitMember>",
                    this.DimensionItemFullName, this.Domain, this.DomainMember));
            }
            else 
            {
                /*   <xbrldi:typedMember dimension="eba_dim:INC">
                        <eba_typ:CC>LEI-E57ODZWZ7FF32TWEFA76</eba_typ:CC>
                      </xbrldi:typedMember>*/
                sb.AppendLine(String.Format("<xbrldi:typedMember dimension=\"{0}\"><{1}>{2}</{1}></xbrldi:typedMember>",
                 this.DimensionItemFullName, this.Domain, this.DomainMember));
            }
            return sb.ToString();
        }
    }

    //public class TypeDimension : Dimension
    //{
    //    public override 

    //}
}
