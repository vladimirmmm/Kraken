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
        public ConceptQName Concept { get; set; }

        public override string ToString()
        {
            return String.Format("{0} >> {1}", base.ToString(), this.Concept.QName.Content);
        }
        /*
        public override Func<string, bool> GetFunc(FactBaseQuery fbq)
        {
            Func<string, bool> f = null;
            List<Func<string, bool>> functions = new List<Func<string, bool>>();

            if (!Complement)
            {
                fbq.DictFilters = fbq.DictFilters + String.Format("{0}, ", Concept.QName.Content);

                functions.Add((s) =>
                {
            
                    var sconceptpart = s.Substring(0, s.IndexOf(","));
                    return sconceptpart.EndsWith(Concept.QName.Content);
                });

            }
            else
            {
                fbq.FalseFilters = fbq.FalseFilters + String.Format("{0}, ", Concept.QName.Content);
                functions.Add((s) =>
                {
                    var sconceptpart = s.Substring(0, s.IndexOf(","));
                    return !sconceptpart.EndsWith(Concept.QName.Content);
                });
            }



            f = (s) => functions.All(fx => fx(s));

            return f;
        }
        */
        /*
        public override List<FactBaseQuery> GetQueries(Taxonomy taxonomy, int level = 0)
        {
            var queries = new List<FactBaseQuery>();
            var query = new FactBaseQuery();
            var factparts = taxonomy.FactParts;
            var factsofparts = taxonomy.FactsOfParts;


      
            var tag = Concept.QName.Content;
            query.Concept = tag;
            if (!Complement)
            {
                query.DictFilters = query.DictFilters + String.Format("{0} ", tag);
                if (factparts.ContainsKey(tag))
                {
                    query.DictFilterIndexes.Add(factparts[tag]);
                }
            }
            else 
            {
                query.FalseFilters = query.DictFilters + String.Format("{0} ", tag);
                if (factparts.ContainsKey(tag))
                {
                    query.NegativeDictFilterIndexes.Add(factparts[tag]);
                }
            }
            queries.Add(query);
            SetCover(queries);

            return queries;
        }
        */
        public override FactBaseQuery GetQuery(Taxonomy taxonomy, Hierarchy<XbrlIdentifiable> currentfilter, FactBaseQuery parent)
        {
            //Console.WriteLine(String.Format("ConceptFilter.GetQuery( {0} ) ", currentfilter.Item));

            var factparts = taxonomy.FactParts;
            var factsofparts = taxonomy.FactsOfParts;
            var query = new FactBaseQuery();


            var tag = Concept.QName.Content;
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

            return query;
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
