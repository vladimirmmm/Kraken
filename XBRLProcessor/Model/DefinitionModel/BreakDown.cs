using XBRLProcessor.Model.StringEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XBRLProcessor.Model.Base;

namespace Model.DefinitionModel
{

    public class BreakDown : XbrlIdentifiable
    {
        private ParentChildOrder _ParentChildOrder = ParentChildOrder.ChildFirst;
        public ParentChildOrder ParentChildOrder { get { return _ParentChildOrder; } set { _ParentChildOrder = value; } }
    }
}
