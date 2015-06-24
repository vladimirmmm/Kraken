using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    public abstract class TaxonomyEngine
    {
        public delegate void TaxonomyEventHandler(object sender, EventArgs e);
        public event TaxonomyEventHandler TaxonomyLoaded;
        public event TaxonomyEventHandler InstanceLoaded;
        
        public virtual bool LoadTaxonomy(string filepath)
        {
            if (TaxonomyLoaded != null)
            {
                TaxonomyLoaded(this, new EventArgs());
            }
            return true;
        }

        public virtual bool LoadInstance(string filepath)
        {
            if (InstanceLoaded != null)
            {
                InstanceLoaded(this, new EventArgs());
            }
            return true;
        }
    }
}
