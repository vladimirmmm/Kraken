using LogicalModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBRLProcessor.Model.Base;

namespace XBRLProcessor.Model.DefinitionModel.Filter
{
    public class AspectFilter : Filter
    {

        public override Func<string, bool> GetFunc(FactBaseQuery fbq)
        {
            throw new NotImplementedException();
        }
    }
    public class AspectCoverFilter : AspectFilter
    {
        private String _Aspect = "";
        public String Aspect { get { return _Aspect; } set { _Aspect = value; } }

        public bool Cover { get; set; }
    }
}
