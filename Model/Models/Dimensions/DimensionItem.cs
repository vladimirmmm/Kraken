using LogicalModel.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.Dimensions
{
    public class DimensionItem : QualifiedName, ILabeled
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

        private List<DimensionDomain> _Domains = new List<DimensionDomain>();
        [JsonProperty]
        public List<DimensionDomain> Domains 
        {
            get { return _Domains; }
            set { _Domains = value; }
        }


        internal void SetReferences()
        {
            foreach (var domain in Domains)
            {
                domain.DimensionItem = this;
                domain.SetReferences();
            }
        }


    }
}
