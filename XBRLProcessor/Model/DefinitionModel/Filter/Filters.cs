using BaseModel;
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

        public override string ToString()
        {
            return base.ToString() + (Complement ? " [C]" : "");
        }


        public abstract Func<string, bool> GetFunc(FactBaseQuery fbq);
        public virtual List<FactBaseQuery> GetQueries() 
        {
            var queries = new List<FactBaseQuery>();
            return queries;
        }
    }
    public class FilterContainer : XbrlIdentifiable 
    {

    }
    public class OrFilter : FilterContainer
    {
   
    }
    public class AndFilter : FilterContainer
    {
   
    }
}
