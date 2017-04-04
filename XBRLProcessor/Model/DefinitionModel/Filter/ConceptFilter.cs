using BaseModel;
using LogicalModel;
using LogicalModel.Base;
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
        private List<ConceptQName> _Concepts = new List<ConceptQName>();
        public List<ConceptQName> Concepts { get { return _Concepts; } set { _Concepts = value; } }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(String.Format("{0} >> ", base.ToString()));
            foreach (var concept in _Concepts) 
            {
                sb.Append(concept.QName.Content + ",");
            }
            return sb.ToString();
        }

        public override FactBaseQuery GetQuery(Taxonomy taxonomy, Hierarchy<XbrlIdentifiable> currentfilter, FactBaseQuery parent)
        {
            //Console.WriteLine(String.Format("ConceptFilter.GetQuery( {0} ) ", currentfilter.Item));

            var factparts = taxonomy.FactParts;
            var factsofparts = taxonomy.FactsOfParts;
            //var query = new FactBaseQuery();
            FactBaseQuery result=null;
            var queries = new List<FactBaseQuery>();
            if (Concepts.Count == 1) 
            {

            }
            foreach (var concept in Concepts)
            {
                var query = new FactBaseQuery();
                queries.Add(query);
                var tag = concept.QName.Content;
                query.Concept = tag;
                if (!Complement)
                {
                    query.DictFilters = parent.DictFilters + String.Format("{0} ", tag);
                    if (factparts.ContainsKey(tag))
                    {
                        query.DictFilterIndexes.Add(factparts[tag]);
                    }
                }
                else
                {
                    query.FalseFilters = parent.DictFilters + String.Format("{0} ", tag);
                    if (factparts.ContainsKey(tag))
                    {
                        query.NegativeDictFilterIndexes.Add(factparts[tag]);
                    }
                }
                SetCover(query);

            }
            if (queries.Count == 1)
            {
                result = queries.FirstOrDefault();
            }
            if (queries.Count > 1)
            {
                result = new FactPoolQuery();
                foreach(var query in queries)
                {
                    result.AddChildQuery(query);
                }
            }
            return result;
        }
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
