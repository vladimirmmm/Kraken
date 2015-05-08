using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBRLProcessor.Model.Base;
using XBRLProcessor.Model.DefinitionModel.Formula;

namespace Model.DefinitionModel
{
    //TODO
    public class AspectNode : XbrlIdentifiable
    {

        private DimensionAspect _DimensionAspect = null;
        public DimensionAspect DimensionAspect { get { return _DimensionAspect; } set { _DimensionAspect = value; } }
    }
}
