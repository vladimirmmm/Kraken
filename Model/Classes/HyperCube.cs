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
    }
}
