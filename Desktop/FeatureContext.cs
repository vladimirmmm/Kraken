using LogicalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop
{
    public class FeatureContext
    {
        public Taxonomy ActiveTaxonomy { get; set; }
        
        private List<Taxonomy> _SelectedTaxonomies = new List<Taxonomy>();
        public List<Taxonomy> SelectedTaxonomies { get { return _SelectedTaxonomies; } set { _SelectedTaxonomies = value; } }

        public Instance ActiveInstance { get; set; }

        private List<Instance> _SelectedInstances = new List<Instance>();
        public List<Instance> SelectedInstances { get { return _SelectedInstances; } set { _SelectedInstances = value; } }

        public Instance ActiveTable { get; set; }

        private List<Table> _SelectedTabless = new List<Table>();
        public List<Table> SelectedTabless { get { return _SelectedTabless; } set { _SelectedTabless = value; } }

    }
}
