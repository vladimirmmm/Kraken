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
    public class ConceptFilter : Filter 
    {

    }
    public class ConceptNameFilter : ConceptFilter
    {
        public ConceptQName Concept { get; set; }
    }

    public class ConceptPeriodTypeFilter : ConceptFilter
    {
        public FilterPeriod PeriodType { get; set; }
    }

    public class ConceptBalanceTypeFilter : ConceptFilter
    {
        public FilterBalance Balance { get; set; }
    }

    public class ConceptCustomAttributeFilter : ConceptFilter
    {
        public ConceptQName Attribute { get; set; }

        private String _Value = "";
        public String Value { get { return _Value; } set { _Value = value; } }
    }

    public class ConceptDataTypeFilter : ConceptFilter
    {
        public ConceptQName Type { get; set; }
        public Boolean Strict { get; set; }

    }

    public class ConceptSubstitutionGroupFilter : ConceptFilter
    {
        public ConceptQName SubstitutionGroup { get; set; }
        public Boolean Strict { get; set; }

    }

    public class ConceptQName 
    {
        private QName _QName = new QName("");
        public QName QName { get { return _QName; } set { _QName = value; } }

        private String _QNameExpression = "";
        public String QNameExpression { get { return _QNameExpression; } set { _QNameExpression = value; } }
    }
}
