using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBRLProcessor.Model.Base;

namespace XBRLProcessor.Model.DefinitionModel.Filter
{
    public class Filter : XbrlIdentifiable 
    {
        //all but the filter if true else only the filter
        public bool Complement { get; set; }

    }
    public class OrFilter : Filter
    {
    }
    public class AndFilter : Filter
    {
    }
}
