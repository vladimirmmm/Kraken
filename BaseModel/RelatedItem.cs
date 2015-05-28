using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel
{
    public class RelatedItem<TRelated,TRelator> 
        where TRelated:class
        where TRelator:class
    {
        public List<Relation<TRelated, TRelator>> Relations = new List<Relation<TRelated, TRelator>>();

        public List<TRelated> Children 
        {
            get { return this.Relations.Select(i => i.Target).ToList(); }
        }
    }

    public class Relation<TRelated,TRelator> 
        where TRelated:class
        where TRelator:class
    {
        public TRelated Parent;
        public TRelated Target;

        public int Order;
    }
}
