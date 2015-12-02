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
        //public string ID 
        //{
        //    get {
        //        return ValueAssertion != null? ValueAssertion.ID:"";
        //    }
        //}
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

        public LogicalModel.Validation.ValidationRule GetLogicalRule(Hierarchy<XbrlIdentifiable> hrule) 
        {
            var logicalrule = new LogicalModel.Validation.ValidationRule();
            var valueassertion = hrule.Item as ValueAssertion;
            logicalrule.ID = valueassertion.ID;
            logicalrule.LabelID = valueassertion.LabelID;
            logicalrule.OriginalExpression = valueassertion.Test;
            logicalrule.SetTaxonomy(this.Taxonomy);

            var factvariables = hrule.Where(i => i.Item is FactVariable);
 
            var sb = new StringBuilder();
            sb.AppendLine(logicalrule.DisplayText);
            sb.AppendLine(valueassertion.Test);

            //
            if (valueassertion.ID.Contains("eba_v0648_m"))
            {
            }
            var factgroups = GetGroups(hrule);
            if (factgroups.Count == 1 &&  factgroups.FirstOrDefault().Concept!=null && factgroups.FirstOrDefault().GetFactKey().IndexOf("[") < 0) 
            {
                var factkey = factgroups.FirstOrDefault().GetFactKey();
                var factsofconcept = Taxonomy.Facts.Where(i => i.Key.StartsWith(factkey)).ToList();
                if (factsofconcept.Count > 0) 
                {
                    factgroups.Clear();
                }
                foreach (var factkvp in factsofconcept)
                {
                    var factgroup = new LogicalModel.Base.FactGroup();
                    factgroup.SetFromString(factkvp.Key);
                    factgroups.Add(factgroup);

                    var fb = new LogicalModel.Base.FactBase();
                    fb.SetFromString(factkvp.Key);
                    factgroup.Facts.Add(fb);
                }

            }
       
            foreach (var fv in factvariables) 
            {
                var factvariable = fv.Item as FactVariable;
                //var name = fv.Item.ID.Substring(fv.Item.ID.LastIndexOf(".") + 1);
                var name = factvariable.Name;
                var parameter = new LogicalModel.Validation.ValidationParameter(name, logicalrule.ID);
                parameter.BindAsSequence = factvariable.BindAsSequence;
                parameter.FallBackValue = factvariable.FallbackValue;
             
            
                //TODO
                foreach (var factgroup in factgroups) 
                {
                    var parameterfactgroup= factgroup.Copy();
                    parameter.FactGroups.Add(parameterfactgroup.FactString, parameterfactgroup);
                    var facts = GetFacts(fv);
                    ////original
                    //parameterfactgroup.Facts = facts;
                    //SetFacts(parameterfactgroup);
                    parameterfactgroup.SetFacts(facts);
                    
                    var xfirstfact = parameterfactgroup.FullFacts.FirstOrDefault();
                    if (xfirstfact != null) 
                    {
                        if (xfirstfact.Dimensions.Count == 0 && xfirstfact.Concept == null) 
                        {

                        }
                    }
                }


                var type = LogicalModel.TypeEnum.Numeric;
                var firstfact = parameter.FactGroups.Values.FirstOrDefault().FullFacts.FirstOrDefault();
                if (firstfact != null && firstfact.Concept != null)
                {
                    //if (firstfact.Concept.ID.StartsWith("ei"))
                    if (firstfact.Concept.Name.StartsWith("ei"))
                    {
                        type = LogicalModel.TypeEnum.String;
                    }
                }
                parameter.Type = type;

                var sequence = parameter.BindAsSequence ? "Sequence":"";
                sb.AppendLine("parameter: " + name + " " + sequence);

                if (LogicalModel.Settings.Current.CheckValidationCells)
                {
                    sb.AppendLine(CheckCells(parameter));
                }
                logicalrule.Parameters.Add(parameter);
              
            }
            var pc = 0;
            foreach (var pv in logicalrule.Parameters) 
            {
                if (pc == 0) 
                {
                    pc = pv.FactGroups.Count;
                }
                if (pc != pv.FactGroups.Count)                 
                {
                    if (!pv.BindAsSequence) 
                    {
                        sb.AppendLine("Sequenced");

                    }
                    sb.AppendLine("XnotX");
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


            Utilities.FS.AppendAllText(Taxonomy.TaxonomyTestPath, sb.ToString());
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
