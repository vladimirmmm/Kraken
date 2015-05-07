using Model.DefinitionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBRLProcessor.Model.Base;
using XBRLProcessor.Model.StringEnums;

namespace XBRLProcessor.Model.DefinitionModel.Filter
{
    public class DimensionFilter:XbrlIdentifiable 
    {
        public DimensionQName Dimension { get; set; }

    }
    public class ExplicitDimensionFilter : DimensionFilter
    {
        public DimensionMember Member { get; set; }
    }

    public class TypedDimensionFilter : DimensionFilter
    {
        private String _Test = "";
        public String Test { get { return _Test; } set { _Test = value; } }
    }

    public class DimensionQName 
    {
        private QName _QName = new QName("");
        public QName QName { get { return _QName; } set { _QName = value; } }

        private String _QNameExpression = "";
        public String QNameExpression { get { return _QNameExpression; } set { _QNameExpression = value; } }
    }

    public class DimensionMember
    {
        private QName _Variable = new QName("");
        public QName Variable { get { return _Variable; } set { _Variable = value; } }

        private QName _QName = new QName("");
        public QName QName { get { return _QName; } set { _QName = value; } }

        private String _QNameExpression = "";
        public String QNameExpression { get { return _QNameExpression; } set { _QNameExpression = value; } }

        private String _LinkRole = "";
        public String LinkRole { get { return _LinkRole; } set { _LinkRole = value; } }

        private String _ArcRole = "";
        public String ArcRole { get { return _ArcRole; } set { _ArcRole = value; } }


        public FilterAxis Axis { get; set; }
    }
}
