﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    [JsonObject(MemberSerialization=MemberSerialization.OptIn)]
    public class Dimension
    {
        public Boolean IsTyped { get; set; }
        private String _DimensionItem = "";
        private String _Domain = "";
        private String _DomainMember = "";

        private int _MapID = -1;
        private int _DomMapID = -1;

        [JsonProperty]
        public int MapID { get { return _MapID; } set { _MapID = value; } }
        [JsonIgnore]
        public int DomMapID { get { return _DomMapID; } set { _DomMapID = value; } }

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
        public string DimensionDomain 
        {
            get
            {
                return String.Format("[{0}]{1}", DimensionItem, Domain);
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
            if (this.IsTyped) { return; }
            var ix = this.Domain.IndexOf(":", StringComparison.Ordinal);
            if (ix > -1)
            {
                this.IsTyped = Taxonomy.IsTyped(this.Domain);
                return;
            }
            if (ix > -1 && !String.IsNullOrEmpty(this.DomainMember))
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

        public bool IsDefaultMember 
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
                if (item.Item.Concept == null) 
                {
                    item.Item.Concept = current.Item.Concept;
                }
                //if (current.Item.Concept == null || current.Item.Concept.Content == item.Item.Concept.Content)
                //{
                //}

                current = current.Parent;
            }
            item.Item.Dimensions = item.Item.Dimensions.Where(i => !i.IsDefaultMember).ToList();
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
                else 
                {
                    var defdim = new Dimension();
                    defdim.DimensionItem = item.DimensionItem;
                    defdim.Domain = item.Domain;
                    defdim.DomainMember = Literals.DefaultMember;
                    dimensions.Add(defdim);

                }
            }
            return dimensions;
        }

        public string ToXmlString(string prefix)
        {
            var sb = new StringBuilder();
            if (!this.IsTyped)
            {
                //<xbrldi:explicitMember dimension="eba_dim:LIQ">eba_LQ:x72</xbrldi:explicitMember>
                sb.AppendLine(prefix + String.Format("<xbrldi:explicitMember dimension=\"{0}\">{1}:{2}</xbrldi:explicitMember>",
                    this.DimensionItemFullName, this.Domain, this.DomainMember));
            }
            else 
            {
                /*   <xbrldi:typedMember dimension="eba_dim:INC">
                        <eba_typ:CC>LEI-E57ODZWZ7FF32TWEFA76</eba_typ:CC>
                      </xbrldi:typedMember>*/
                sb.AppendLine(prefix + String.Format("<xbrldi:typedMember dimension=\"{0}\"><{1}>{2}</{1}></xbrldi:typedMember>",
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
