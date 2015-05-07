using LogicalModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.Dimensions
{
    public class DimensionDomain : QualifiedName, ILabeled
    {
        private Label _Label;
        Label ILabeled.Label { get { return _Label; } set { _Label = value; } }

        public string LabelContent
        {
            get { return _Label != null ? _Label.Content : ""; }
        }

        public string LabelCode
        {
            get { return _Label != null ? _Label.Code : ""; }
        }

        public string _LabelID = "";
        public string LabelID { get { return _LabelID; } set { _LabelID = value; } }

        private List<DimensionMember> _DomainMembers = new List<DimensionMember>();
        public List<DimensionMember> DomainMembers
        {
            get { return _DomainMembers; }
            set { _DomainMembers = value; }
        }

        public DimensionItem DimensionItem { get; set; }


        internal void SetReferences()
        {
            foreach (var member in DomainMembers)
            {
                member.Domain = this;
            }
        }

        public override bool Equals(object obj)
        {
            var item = obj as DimensionDomain;
            if (item != null)
            {
                return this.Content == item.Content;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.Content.GetHashCode();
        }
    }
}
