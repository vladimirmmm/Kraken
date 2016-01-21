using BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private List<FilterContainer> _Filters = new List<FilterContainer>();
        public List<FilterContainer> Filters { get { return _Filters; } set { _Filters = value; } }


        public Hierarchy<XbrlIdentifiable> ValidationRoot = null;
        private XbrlTaxonomyDocument Document;

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
                });

           
        }

        public LogicalModel.Validation.ValidationRule GetLogicalRule_Tmp(Hierarchy<XbrlIdentifiable> hrule, XbrlTaxonomyDocument document)
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
            var factvariables = tmp_rule.Where(i => i.Item is FactVariable);
            foreach (var fv in factvariables)
            {
                tmp_rule.Remove(fv);
            }
            var rulefactqueries = GetFactQuery(tmp_rule);
            var rulefactIds = new List<int>();
            var rulebasequery = GetRuleQuery(tmp_rule).FirstOrDefault();
            var rbds = new List<string>();
            if (rulebasequery != null)
            {
                rbds = rulebasequery.GetDimensions().Select(i=>i+", ").ToList();
                rulefactIds.AddRange(GetFactIDsByDict(rulebasequery, null));

            }

            if (rulefactIds.Count == 0)
            {
                rulefactIds = Taxonomy.FactsIndex.Keys.ToList();
            }

            var sb = new StringBuilder();
            sb.AppendLine(logicalrule.DisplayText);
            sb.AppendLine(valueassertion.Test);
            var rawval = logicalrule.DisplayText + "\r\n" + logicalrule.OriginalExpression + "\r\n" + hrule.ToHierarchyString(i => i.ToString()) + "\r\n";

            Utilities.FS.AppendAllText(Taxonomy.TaxonomyValidationFolder + "Validations_XML.txt", rawval);

            //
            if (valueassertion.ID.Contains("0647"))
            {
            }

            foreach (var fv in factvariables)
            {
                var factvariable = fv.Item as FactVariable;
                var parameterfactqueries = GetFactQuery(fv, 1);

                var parameterfactbasequery = GetRuleQuery(fv).FirstOrDefault();
                var parameterfacts = parameterfactbasequery == null ? rulefactIds.ToArray().ToList() : GetFactIDsByDict(parameterfactbasequery, rulefactIds);
                var issorted = false;
                var parameterfactquery = new LogicalModel.Base.FactBaseQuery();
                foreach (var pfbq in parameterfactqueries) 
                {
                    parameterfactquery.ChildQueries.Add(pfbq);
                }


                var name = factvariable.Name;
                var parameter = new LogicalModel.Validation.ValidationParameter(name, logicalrule.ID);
                parameter.BindAsSequence = factvariable.BindAsSequence;
                parameter.FallBackValue = factvariable.FallbackValue;

                var mergedqueries = new List<LogicalModel.Base.FactBaseQuery>();
                if (parameter.BindAsSequence) {
                    mergedqueries = CombineQueries(rulefactqueries, new List<LogicalModel.Base.FactBaseQuery>() { parameterfactquery });

                }
                else
                {
                    if (parameterfactqueries.Count > 1) 
                    {
                        //Utilities.Logger.WriteLine(String.Format("Rule {0} parameter {1} has multiple queries, but it is not sequenced.", logicalrule.ID, parameter.Name));
                    }
                    mergedqueries = CombineQueries(rulefactqueries, parameterfactqueries);
                }
                //TODO
                var multiplefactsfornonseqparameter = 0;
                var qix = 0;
                parameter.TaxFacts.Capacity = mergedqueries.Count;
                var bsize = 500;
                var isnonsequenced = !parameter.BindAsSequence;
                var parameterfactdict = new HashSet<int>(parameterfacts);
                foreach (var fbq in mergedqueries)
                {

                    foreach (var rbs in rbds)
                    {
                        fbq.DictFilters = fbq.DictFilters.Replace(rbs, "");
                    }

                    //var datafactids = new List<int>();
                    //var facts = GetFacts(fbq, parameterfacts, parameterfactdict, datafactids);
                    
                    var facts = GetFactsKV(fbq, parameterfacts, parameterfactdict);

                    var ok = true;

                    if (isnonsequenced && facts.Count > 1 )
                    {
                        multiplefactsfornonseqparameter++;
                        ok = false;
                        //like concepts for exampl
                        if (parameterfactqueries.Count == 0)
                        {
                            multiplefactsfornonseqparameter = 0;
                            foreach (var fact in facts)
                            {
                                parameter.TaxFacts.Add(new List<int>() { fact.Value });
                            }
                        }
                        else 
                        {
                            multiplefactsfornonseqparameter = 0;
                            foreach (var fact in facts)
                            {
                                parameter.TaxFacts.Add(new List<int>() { fact.Value });
                            }
                        }

                    }
                    if (ok)
                    {

                        parameter.TaxFacts.Add(facts.Select(i=>i.Value).ToList());
                        if (qix > 1 && qix % bsize == 0)
                        {
                            //if (!issorted)
                            //{
                            //    issorted = true;
                            //    parameterfacts = parameterfacts.OrderBy(i => i).ToList();

                            //}
                            //factids = factids.OrderBy(i => i).ToList();
                            //parameterfacts = Utilities.Objects.SortedExcept(parameterfacts, factids);
                            //factids.Clear();
                            //Utilities.Logger.WriteLine(String.Format("Remaining: {0}",mergedqueries.Count- mergedqueries.IndexOf(fbq)));
                        }
                    }
            

                    qix++;
                }


                if (multiplefactsfornonseqparameter > 0)
                {
                    Utilities.Logger.WriteLine(String.Format("Rule {0} non-sequenced parameter {1} has multiple facts", logicalrule.ID, parameter.Name));

                }
                if (parameter.TaxFacts.Count == 0)
                {
                    Utilities.Logger.WriteLine(String.Format("Rule {0} parameter {1} has no facts", logicalrule.ID, parameter.Name));

                }

                var type = LogicalModel.TypeEnum.Numeric;
                var firsttaxfact = parameter.TaxFacts.FirstOrDefault(i => i.Count > 0);
                if (firsttaxfact == null)
                {
                    Utilities.Logger.WriteLine(String.Format("Rule {0} parameter {1} has no valid facts", logicalrule.ID, parameter.Name));
                }
                else
                {
                    var firstfactid = firsttaxfact.FirstOrDefault();
                    var firstfactstring = Taxonomy.GetFactStringKey(Taxonomy.FactsIndex[firstfactid]);
                    var firstfact = LogicalModel.Base.FactBase.GetFactFrom(firstfactstring);

                    if (firstfact.Concept != null
                        && (firstfact.Concept.Name.StartsWith("ei") || firstfact.Concept.Name.StartsWith("si")))
                    {
                        type = LogicalModel.TypeEnum.String;
                    }
                    if (firstfact.Concept != null
                        && (firstfact.Concept.Name.StartsWith("di")))
                    {
                        type = LogicalModel.TypeEnum.Date;
                    }
                    parameter.Type = type;
                }
                var sequence = parameter.BindAsSequence ? "Sequence" : "";
                sb.AppendLine("parameter: " + name + " " + sequence);

                if (LogicalModel.Settings.Current.CheckValidationCells)
                {
                    sb.AppendLine(CheckCells(parameter));
                }
                logicalrule.Parameters.Add(parameter);

            }

            //remove invalid facts
            var firstparameter = logicalrule.Parameters.FirstOrDefault();
            var taxfactstoremove = new List<int>();
            for (int i = 0; i < firstparameter.TaxFacts.Count; i++)
            {
                var factlist = new List<List<int>>();
                factlist.Add(firstparameter.TaxFacts[i]);
                for (int j = 1; j < logicalrule.Parameters.Count; j++)
                {
                    var parameter = logicalrule.Parameters[j];
                    if (i >= parameter.TaxFacts.Count)
                    {

                    }
                    else
                    {
                        factlist.Add(parameter.TaxFacts[i]);

                    }
                }
                if (factlist.All(f => f.Count == 0))
                {
                    taxfactstoremove.Add(i);
                }
            }
            taxfactstoremove = taxfactstoremove.OrderByDescending(i => i).ToList();
            foreach (var taxfactid in taxfactstoremove)
            {
                foreach (var parameter in logicalrule.Parameters)
                {
                    if (parameter.TaxFacts.Count > taxfactid)
                    {
                        parameter.TaxFacts.RemoveAt(taxfactid);
                    }
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


            return logicalrule;
        }


        private void AddDimensionIfNotExists(LogicalModel.Dimension dimension, LogicalModel.Base.FactBase fact)
        {
            var existingdim = fact.Dimensions.FirstOrDefault(i => i.DimensionItem == dimension.DimensionItem);
            if (existingdim == null && !dimension.IsDefaultMemeber)
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
