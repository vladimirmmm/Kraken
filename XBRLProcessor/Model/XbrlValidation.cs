using BaseModel;
using LogicalModel.Base;
using LogicalModel.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using XBRLProcessor.Model.Base;
using XBRLProcessor.Model.DefinitionModel;
using XBRLProcessor.Model.DefinitionModel.Filter;
using XBRLProcessor.Models;

namespace XBRLProcessor.Model
{
    public partial class XbrlValidation
    {
        public XbrlTaxonomy Taxonomy = null;

        public List<Arc> Arcs = new List<Arc>();
        public List<XbrlIdentifiable> Identifiables = new List<XbrlIdentifiable>();


        public XbrlIdentifiable AssertionSet { get; set; }

        private List<ValueAssertion> _ValueAssertions = new List<ValueAssertion>();
        public List<ValueAssertion> ValueAssertions { get { return _ValueAssertions; } set { _ValueAssertions = value; } }


        private List<VariableArc> _VariableArcs = new List<VariableArc>();
        public List<VariableArc> VariableArcs { get { return _VariableArcs; } set { _VariableArcs = value; } }

        private List<VariableFilterArc> _VariableFilterArcs = new List<VariableFilterArc>();
        public List<VariableFilterArc> VariableFilterArcs { get { return _VariableFilterArcs; } set { _VariableFilterArcs = value; } }
        
        private List<VariableSetFilterArc> _VariableSetFilterArcs = new List<VariableSetFilterArc>();
        public List<VariableSetFilterArc> VariableSetFilterArcs { get { return _VariableSetFilterArcs; } set { _VariableSetFilterArcs = value; } }
        

        private List<DimensionFilter> _DimensionFilters = new List<DimensionFilter>();
        public List<DimensionFilter> DimensionFilters { get { return _DimensionFilters; } set { _DimensionFilters = value; } }

        private List<TupleFilter> _TupleFilters = new List<TupleFilter>();
        public List<TupleFilter> TupleFilters { get { return _TupleFilters; } set { _TupleFilters = value; } }

        private List<GeneralFilter> _GeneralFilters = new List<GeneralFilter>();
        public List<GeneralFilter> GeneralFilters { get { return _GeneralFilters; } set { _GeneralFilters = value; } }


        private List<ConceptFilter> _ConceptFilters = new List<ConceptFilter>();
        public List<ConceptFilter> ConceptFilters { get { return _ConceptFilters; } set { _ConceptFilters = value; } }

        private List<AspectFilter> _AspectFilters = new List<AspectFilter>();
        public List<AspectFilter> AspectFilters { get { return _AspectFilters; } set { _AspectFilters = value; } }


        private List<FactVariable> _FactVariables = new List<FactVariable>();
        public List<FactVariable> FactVariables { get { return _FactVariables; } set { _FactVariables = value; } }

        private List<Filter> _Filters = new List<Filter>();
        public List<Filter> Filters { get { return _Filters; } set { _Filters = value; } }


        public Hierarchy<XbrlIdentifiable> ValidationRoot = null;
        private XbrlTaxonomyDocument Document;

        private System.Xml.XmlNamespaceManager NsManager = null;
        public void SetNamespaceManager(System.Xml.XmlDocument doc) 
        {
            NsManager = Utilities.Xml.GetTaxonomyNamespaceManager(doc);
        }
        public void LoadValidationHierarchy() 
        {

            Arcs.Clear();
            Arcs.AddRange(this.VariableArcs);
            Arcs.AddRange(this.VariableFilterArcs);
            Arcs.AddRange(this.VariableSetFilterArcs); //.Where(i=>i.Complement=false));
            if (this.AssertionSet != null)
            {
                Identifiables.Add(this.AssertionSet);
            }
            Identifiables.AddRange(this.ValueAssertions);
            Identifiables.AddRange(this.DimensionFilters);
            Identifiables.AddRange(this.TupleFilters);
            Identifiables.AddRange(this.GeneralFilters);
            Identifiables.AddRange(this.ConceptFilters);
            Identifiables.AddRange(this.AspectFilters);
            Identifiables.AddRange(this.FactVariables);
            Identifiables.AddRange(this.Filters);
            foreach (var identifiable in Identifiables)
            {
                  var df = identifiable as DimensionFilter;
                  if (df != null)
                  {
                      var nsuri = NsManager.LookupNamespace(df.Dimension.QName.Domain);
                      df.Dimension.QName.Domain = Taxonomy.FindNamespacePrefix(nsuri, df.Dimension.QName.Domain);
                      var exdf = df as ExplicitDimensionFilter;
                      if (exdf != null)
                      {
                          foreach (var m in exdf.Members)
                          {
                              nsuri = NsManager.LookupNamespace(m.QName.Domain);
                              m.QName.Domain = Taxonomy.FindNamespacePrefix(nsuri, m.QName.Domain);
                          }
                      }
                      var tydf = df as TypedDimensionFilter;
                      if (tydf != null)
                      {
                          //tydf.
                      }
                  }

            }
            ValidationRoot = Hierarchy<XbrlIdentifiable>.GetHierarchy(Arcs, Identifiables,
                (i, a) => i.Item.LabelID == a.From, (i, a) => i.Item.LabelID == a.To,
                (i, a) => {
                    if (i.Item is Filter && a is ComplementArc) 
                    {
                        var filter = i.Item as Filter;
                        var arc = a as ComplementArc;
                        filter.Complement = arc.Complement;
                    }
                    if (a is VariableArc) 
                    {
                        var varc = a as VariableArc;
                        var variable = i.Item as Variable;
                        variable.Name = varc.Name;
                    }
                    if (a is VariableFilterArc && i.Item is Filter) 
                    {
                        var vfarc = a as VariableFilterArc;
                        var ac = i.Item as Filter;
                        ac.Cover = vfarc.Cover;

                    }
                });

           
        }
       
        public LogicalModel.Validation.ValidationRule GetLogicalRule(Hierarchy<XbrlIdentifiable> hrule, XbrlTaxonomyDocument document)
        {
            this.Document = document;

            var tmp_rule = hrule.Copy();
            FixRule(tmp_rule);
            var logicalrule = new LogicalModel.Validation.ValidationRule();
            var valueassertion = tmp_rule.Item as ValueAssertion;
            logicalrule.ID = valueassertion.ID;
            Utilities.Logger.WriteLine("Getting rule for " + logicalrule.ID);
            logicalrule.LabelID = valueassertion.LabelID;
            logicalrule.OriginalExpression = valueassertion.Test.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ");
            logicalrule.SetTaxonomy(this.Taxonomy);

            var sb = new StringBuilder();
            sb.AppendLine(logicalrule.DisplayText);
            sb.AppendLine(valueassertion.Test);
            var rawval = document.FileName + "\r\n" + document.LocalPath + "\r\n" + logicalrule.DisplayText + "\r\n" + logicalrule.OriginalExpression + "\r\n" + hrule.ToHierarchyString(i => i.ToString()) + "\r\n";
            logicalrule.RawInfo = rawval;
            Utilities.FS.AppendAllText(Taxonomy.TaxonomyValidationFolder + "Validations_XML.txt", rawval);


            var factvariables = tmp_rule.Where(i => i.Item is FactVariable);
            foreach (var fv in factvariables)
            {
                tmp_rule.Remove(fv);
            }
            if (logicalrule.ID.Contains("de_sprv_vdbl_0080"))
            {
                //var rulebasequeryX = GetRuleQuery(tmp_rule).FirstOrDefault();

            }
            logicalrule.BaseQuery = GetQuery(tmp_rule);

            logicalrule.BaseQuery.GetString(Taxonomy);

            foreach (var fv in factvariables)
            {
                var factvariable = fv.Item as FactVariable;
                var name = factvariable.Name;
                var parameter = new LogicalModel.Validation.ValidationParameter(name, logicalrule.ID);
                logicalrule.Parameters.Add(parameter);
                parameter.BindAsSequence = factvariable.BindAsSequence;
                parameter.FallBackValue = factvariable.FallbackValue;
        

                parameter.BaseQuery = GetQuery(fv);
                parameter.Concept = parameter.BaseQuery.GetConcept();
                if (String.IsNullOrEmpty(parameter.Concept)) 
                {
                    parameter.Concept = logicalrule.BaseQuery.GetConcept();
                }
                if (parameter.BaseQuery.HasDictFilter("find:filingIndicator"))
                {
                    parameter.IsGeneral = true;
                    parameter.StringValue = "filingindicators";
                }
                ValidationRuleHelper.SetParamerterTypes(Taxonomy, logicalrule);
         

               
            }
            var factparameterqueries= logicalrule.Parameters.Where(i=>!i.IsGeneral).Select(i=>i.BaseQuery).ToArray();
            var commconparameterquery = FactBaseQuery.GetCommonQuery(factparameterqueries);
            if (commconparameterquery!=null && commconparameterquery.HasFilters()) 
            {
                FactBaseQuery.MergeQueries(logicalrule.BaseQuery, commconparameterquery);
                foreach (var pquery in factparameterqueries) 
                {
                    FactBaseQuery.RemoveQuery(pquery, commconparameterquery);
                }
            }

            if (valueassertion.Test.Contains("$ReportingLevel"))
            {
                var p_rl1 = new LogicalModel.Validation.ValidationParameter("ReportingLevel", logicalrule.ID);
                p_rl1.StringValue = this.Taxonomy.EntryDocument.FileName.Contains("_con") ? "con" : "ind";
                p_rl1.Type = LogicalModel.TypeEnum.String;
                p_rl1.IsGeneral = true;
                logicalrule.Parameters.Add(p_rl1);
            }
            if (valueassertion.Test.Contains("$AccountingStandard"))
            {
                var p_rl2 = new LogicalModel.Validation.ValidationParameter("AccountingStandard", logicalrule.ID);
                p_rl2.StringValue = this.Taxonomy.EntryDocument.FileName.Contains("GAAP") ? "GAAP" : "IFRS";
                p_rl2.Type = LogicalModel.TypeEnum.String;
                p_rl2.IsGeneral = true;
                logicalrule.Parameters.Add(p_rl2);
            }
            logicalrule.SetTaxonomy(this.Taxonomy);
            ValidationRuleHelper.ExecuteExplicitFiltering(this.Taxonomy, logicalrule);
            ValidationRuleHelper.ExecuteImplicitFiltering(this.Taxonomy, logicalrule);
            ValidationRuleHelper.ExecuteMatching(this.Taxonomy, logicalrule);
            ValidationRuleHelper.CheckConsistency(this.Taxonomy, logicalrule);

            //SetFacts(logicalrule);

            return logicalrule;
        }

     
        private void AddDimensionIfNotExists(LogicalModel.Dimension dimension, LogicalModel.Base.FactBase fact)
        {
            var existingdim = fact.Dimensions.FirstOrDefault(i => i.DimensionItem == dimension.DimensionItem);
            if (existingdim == null && !dimension.IsDefaultMember)
            {
                fact.Dimensions.Add(dimension);
            }
        } 
     

        public List<LogicalModel.Concept> GetConcepts(Hierarchy<XbrlIdentifiable> item) 
        {
            var items= item.Where(i => i.Item is ConceptFilter).Select(s => Mapping.Mappings.ToLogical(s.Item as ConceptFilter)).ToList();
            return items;
        }

        public List<List<LogicalModel.Dimension>> GetDimensions(Hierarchy<XbrlIdentifiable> item)
        {
            var items = item.Where(i => i.Item is DimensionFilter).Select(s => Mapping.Mappings.ToLogicalDimensions(s.Item as DimensionFilter)).ToList();
            return items;
        }

        public void ClearObjects() 
        {
            this.Arcs.Clear();
            this.ValueAssertions.Clear();
            this.Identifiables.Clear();
            this.AspectFilters.Clear();
            this.GeneralFilters.Clear();
            this.TupleFilters.Clear();
            this.VariableArcs.Clear();
            this.VariableFilterArcs.Clear();
            this.VariableSetFilterArcs.Clear();
            this.ConceptFilters.Clear();
            this.DimensionFilters.Clear();
            this.Filters.Clear();
            this.FactVariables.Clear();
            this.AssertionSet = null;

            this.ValidationRoot = null;

        }

    }
}
