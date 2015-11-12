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
        private String _Domain = "";
        private String _DomainMember = "";
  

        public String DimensionItem
        {
            get { return _DimensionItem; }
            set
            {
                _DimensionItem = value;
            }
        }

        public String Domain
        {
            get { return _Domain; }
            set
            {
                _Domain = value;

            }
        }

        public String DomainMember
        {
            get { return _DomainMember; }
            set
            {
                _DomainMember = value;
            }
        }

        public String DomainAndMember
        {
            get { return String.Format("{0}:{1}",Domain,DomainMember); }
            set
            {
                var parts = value.Split(new string[]{":"},StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2)
                {
                    _Domain = parts[0];
                    _DomainMember = parts[1];
                }
                else 
                {
                }
            }
        }
  
        public string DomainMemberFullName 
        {
            get 
            {
                var doimainmember = DomainMember;
                if (string.IsNullOrEmpty(doimainmember))
                {
                    return String.Format("[{0}]{1}", DimensionItem, Domain);
                }
                return String.Format("[{0}]{1}:{2}", DimensionItem, Domain, doimainmember);
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
        public string ToStringForKey() {
            return ToStringForKey("");
        }
        public void SetTyped() 
        {
            if (this.Domain.IndexOf(":") > -1)
            {
                this.IsTyped = Taxonomy.IsTyped(this.Domain);
                return;
            }
            if (this.Domain.IndexOf(":") > -1 && !String.IsNullOrEmpty(this.DomainMember))
            {
                this.IsTyped = true;
            }
        }
        public string ToStringForKey(string lastnamespace) 
        {
            var item = "";
            SetTyped();
            //var dimensionitem = lastnamespace == "" ? this.DimensionItem : this.DimensionItem.Replace(lastnamespace, "*");
            var dimensionitem = this.DimensionItem;

            if (this.IsTyped )
            {
           
                item = String.Format("[{0}]{1}", this.DimensionItem, this.Domain);

            }
            else 
            {
                item = String.Format("[{0}]{1}:{2}", this.DimensionItem, this.Domain, this.DomainMember);

            }
          
            return item.Trim();
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

        public static void SetDimensions(BaseModel.Hierarchy<LayoutItem> item)
        {
            var current = item.Parent;
            while (current != null)
            {
                MergeDimensions(item.Item.Dimensions, current.Item.Dimensions);

                if (current.Item.Concept == null || current.Item.Concept.Content == item.Item.Concept.Content)
                {
                }

                current = current.Parent;
            }
            item.Item.Dimensions = item.Item.Dimensions.Where(i => !i.IsDefaultMemeber).ToList();
        }
        public static void SetDimensions(List<BaseModel.Hierarchy<LayoutItem>> items)
        {
            foreach (var item in items)
            {
                SetDimensions(item);
            }
        }
        public static void MergeDimensions(List<Dimension> target, List<Dimension> items)
        {
            foreach (var item in items)
            {
                var existing = target.FirstOrDefault(i => i.Domain == item.Domain && i.DimensionItem == item.DimensionItem);
                if (existing == null)
                {
                    target.Add(item);
                }
            }
        }

        public static List<Dimension> GetDimensions(List<Dimension> target, List<Dimension> items)
        {
            var dimensions = new List<Dimension>();
            foreach (var item in items)
            {
                var existing = target.FirstOrDefault(i => i.Domain == item.Domain && i.DimensionItem == item.DimensionItem);
                if (existing != null)
                {
                    dimensions.Add(existing);
                }
            }
            return dimensions;
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
