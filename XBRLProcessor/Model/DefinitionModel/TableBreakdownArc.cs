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

    public class TableBreakDownArc : Arc
    {
        private Axis _Axis = Axis.X;
        public Axis Axis { get { return _Axis; } set { _Axis = value; } }

    }
}
