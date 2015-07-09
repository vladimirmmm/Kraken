using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    public class TaxonomyEventArgs : EventArgs 
    {
        public string FilePath = "";
        public TaxonomyEventArgs() 
        {

        }
        public TaxonomyEventArgs(string filepath)
        {
            this.FilePath = filepath;
        }
    }
    public abstract class TaxonomyEngine
    {
        public delegate void TaxonomyEventHandler(object sender, TaxonomyEventArgs e);
        public event TaxonomyEventHandler TaxonomyLoad;
        public event TaxonomyEventHandler TaxonomyLoadFailed;
        public event TaxonomyEventHandler TaxonomyLoaded;
        public event TaxonomyEventHandler InstanceLoad;
        public event TaxonomyEventHandler InstanceLoadFailed;
        public event TaxonomyEventHandler InstanceLoaded;

        public virtual bool LoadInstance(string filepath) 
        {
            return true;
        }
        public virtual bool LoadTaxonomy(string filepath)
        {
            return true;

        }

        public virtual bool Trigger_TaxonomyLoad(string filepath)
        {
            if (TaxonomyLoad != null)
            {
                TaxonomyLoad(this, new TaxonomyEventArgs(filepath));
            }
            return true;
        }
        public virtual bool Trigger_TaxonomyLoadFailed(string filepath)
        {
            if (TaxonomyLoadFailed != null)
            {
                TaxonomyLoadFailed(this, new TaxonomyEventArgs(filepath));
            }
            return true;
        }
        public virtual bool Trigger_TaxonomyLoaded(string filepath)
        {
            if (TaxonomyLoaded != null)
            {
                TaxonomyLoaded(this, new TaxonomyEventArgs(filepath));
            }
            return true;
        }

        public virtual bool Trigger_InstanceLoad(string filepath)
        {
            if (InstanceLoad != null)
            {
                InstanceLoad(this, new TaxonomyEventArgs(filepath));
            }
            return true;
        }
        public virtual bool Trigger_InstanceLoadFailed(string filepath)
        {
            if (InstanceLoadFailed != null)
            {
                InstanceLoadFailed(this, new TaxonomyEventArgs(filepath));
            }
            return true;
        }
        public virtual bool Trigger_InstanceLoaded(string filepath)
        {
            if (InstanceLoaded != null)
            {
                InstanceLoaded(this, new TaxonomyEventArgs(filepath));
            }
            return true;
        }
    }
}
