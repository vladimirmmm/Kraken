using LogicalModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.Dimensions
{
    public class DimensionMember : QualifiedName, ILabeled
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

        public DimensionDomain Domain { get; set; }

        public Boolean IsDefaultMember
        {
            get;
            set;
        }

        public bool CanHaveAnyValue 
        {
            get { return this.Name == "*"; }
        }
    }

    public class TypedDimensionMember : DimensionMember 
    {
        public override string FullName
        {
            get
            {
                return String.Format("{0}:{1}", Domain.Namespace, Domain.Name);
            }

        }
    }
}
