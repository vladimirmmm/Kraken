using BaseModel;
using LogicalModel;
using LogicalModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBRLProcessor.Model.Base;

namespace XBRLProcessor.Model.DefinitionModel.Filter
{
    public abstract class Filter : XbrlIdentifiable 
    {
        //all but the filter if true else only the filter
        public bool Complement { get; set; }
        public bool Cover { get; set; }

        public override string ToString()
        {
            return base.ToString() + (Complement ? " [C]" : "") + (Cover ? " [cover]" : "");
        }


        public abstract Func<string, bool> GetFunc(FactBaseQuery fbq);
        public virtual List<FactBaseQuery> GetQueries(Taxonomy taxonomy, int level) 
        {
            var queries = new List<FactBaseQuery>();
            return queries;
        }

        public virtual FactBaseQuery GetQuery(Taxonomy taxonomy, int level)
        {
            var queries = new FactBaseQuery();
            return null;
        }

        public virtual FactBaseQuery GetQuery(Taxonomy taxonomy,Hierarchy<XbrlIdentifiable> currentfilter, FactBaseQuery parent)
        {
            var queries = new FactBaseQuery();
            return null;
        }

        public virtual void SetCover(List<FactBaseQuery> queries) 
        {
            foreach (var query in queries) 
            {
                query.Cover = this.Cover;
            }
        }
        public virtual void SetCover(FactBaseQuery query)
        {

            query.Cover = this.Cover;

        }
    }

    public class OrFilter : Filter
    {

        public override FactBaseQuery GetQuery(Taxonomy taxonomy, Hierarchy<XbrlIdentifiable> currentfilter, FactBaseQuery parent)
        {
            var query = new FactPoolQuery();
            foreach (var child in currentfilter.Children)
            {
                var filter = child.Item as Filter;
                if (filter != null)
                {
                    var childquery = filter.GetQuery(taxonomy, child, query);
                    query.ChildQueries.Add(childquery);
                }
            }
            parent.ChildQueries.Add(query);
            return query;
        }

        public override Func<string, bool> GetFunc(FactBaseQuery fbq)
        {
            throw new NotImplementedException();
        }
    }
    
    public class AndFilter : Filter
    {
        public override FactBaseQuery GetQuery(Taxonomy taxonomy, Hierarchy<XbrlIdentifiable> currentfilter, FactBaseQuery parent)
        {
            var query = new FactBaseQuery();
            foreach (var child in currentfilter.Children)
            {
                var filter = child.Item as Filter;
                if (filter != null)
                {
                    var childquery = filter.GetQuery(taxonomy, child, query);
                    //FactBaseQuery.Merge(childquery, query);
                }
            }
            return query;
        }

        public override Func<string, bool> GetFunc(FactBaseQuery fbq)
        {
            throw new NotImplementedException();
        }
    }
}
