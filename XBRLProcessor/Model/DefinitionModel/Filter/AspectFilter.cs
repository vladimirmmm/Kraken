using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBRLProcessor.Model.Base;

namespace XBRLProcessor.Model.DefinitionModel.Filter
{
    public class AspectFilter : XbrlIdentifiable
    {

    }
    public class AspectCoverFilter : AspectFilter
    {
        private String _Aspect = "";
        public String Aspect { get { return _Aspect; } set { _Aspect = value; } }
    }
}
