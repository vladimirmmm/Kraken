using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBRLProcessor.Model.Base;
using XBRLProcessor.Model.StringEnums;

namespace Model.DefinitionModel
{
    public class TableNode:XbrlIdentifiable
    {
        public AspectModel _AspectModel = AspectModel.Dimensional;
        public AspectModel AspectModel { get { return _AspectModel; } set { _AspectModel = value; } }

    }
}
