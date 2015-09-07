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
        public string ID 
        {
            get {
                return ValueAssertion != null? ValueAssertion.ID:"";
            }
        }
        public List<Arc> Arcs = new List<Arc>();
        public List<XbrlIdentifiable> Identifiables = new List<XbrlIdentifiable>();


        public ValueAssertion ValueAssertion {get; set;}

        private List<VariableArc> _VariableArcs = new List<VariableArc>();
        public List<VariableArc> VariableArcs { get { return _VariableArcs; } set { _VariableArcs = value; } }

        private List<VariableFilterArc> _VariableFilterArcs = new List<VariableFilterArc>();
        public List<VariableFilterArc> VariableFilterArcs { get { return _VariableFilterArcs; } set { _VariableFilterArcs = value; } }
        
        private List<VariableSetFilterArc> _VariableSetFilterArcs = new List<VariableSetFilterArc>();
        public List<VariableSetFilterArc> VariableSetFilterArcs { get { return _VariableSetFilterArcs; } set { _VariableSetFilterArcs = value; } }
        

        private List<DimensionFilter> _DimensionFilters = new List<DimensionFilter>();
        public List<DimensionFilter> DimensionFilters { get { return _DimensionFilters; } set { _DimensionFilters = value; } }


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

            Identifiables.Add(this.ValueAssertion);
            Identifiables.AddRange(this.DimensionFilters);
            Identifiables.AddRange(this.ConceptFilters);
            Identifiables.AddRange(this.AspectFilters);
            Identifiables.AddRange(this.FactVariables);
            Identifiables.AddRange(this.Filters);

            ValidationRoot = Hierarchy<XbrlIdentifiable>.GetHierarchy(Arcs, Identifiables,
                (i, a) => i.Item.ID == a.From, (i, a) => i.Item.ID == a.To,
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

        public LogicalModel.Validation.ValidationRule GetLogicalRule() 
        {
            var logicalrule = new LogicalModel.Validation.ValidationRule();
            logicalrule.ID = this.ID;
            logicalrule.LabelID = this.ValueAssertion.LabelID;
            logicalrule.OriginalExpression = this.ValueAssertion.Test;
            logicalrule.SetTaxonomy(this.Taxonomy);

            var factvariables = ValidationRoot.Where(i => i.Item is FactVariable).ToList();
 
            var sb = new StringBuilder();
            sb.AppendLine(logicalrule.DisplayText);
            sb.AppendLine(this.ValueAssertion.Test);

            var factgroups = GetGroups();
            if (this.ID.Contains("0671"))
            {
            }
            foreach (var fv in factvariables) 
            {
                var factvariable = fv.Item as FactVariable;
                //var name = fv.Item.ID.Substring(fv.Item.ID.LastIndexOf(".") + 1);
                var name = factvariable.Name;
                var parameter = new LogicalModel.Validation.ValidationParameter(name);
                parameter.BindAsSequence = factvariable.BindAsSequence;
                parameter.FallBackValue = factvariable.FallbackValue;
             
            
                //TODO
                foreach (var factgroup in factgroups) 
                {
                    var parameterfactgroup= factgroup.Copy();
                    parameter.FactGroups.Add(parameterfactgroup.FactString, parameterfactgroup);
                    var facts = GetFacts(fv);
                    parameterfactgroup.Facts = facts;
                    SetFacts(parameterfactgroup);
                    var xfirstfact = parameterfactgroup.Facts.FirstOrDefault();
                    if (xfirstfact != null) 
                    {
                        if (xfirstfact.Dimensions.Count == 0 && xfirstfact.Concept == null) 
                        {

                        }
                    }
                }


                var type = LogicalModel.TypeEnum.Numeric;
                var firstfact = parameter.FactGroups.Values.FirstOrDefault().Facts.FirstOrDefault();
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
            if (this.ValueAssertion.Test.Contains("$ReportingLevel"))
            {
                var p_rl1 = new LogicalModel.Validation.ValidationParameter("ReportingLevel");
                p_rl1.StringValue = this.Taxonomy.EntryDocument.FileName.Contains("_con") ? "con" : "ind";
                p_rl1.Type = LogicalModel.TypeEnum.String;
                p_rl1.IsGeneral = true;
                logicalrule.Parameters.Add(p_rl1);
            }
            if (this.ValueAssertion.Test.Contains("$AccountingStandard"))
            {
                var p_rl2 = new LogicalModel.Validation.ValidationParameter("AccountingStandard");
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

        public void Clear() 
        {
            this.Arcs.Clear();
            this.Identifiables.Clear();
            this.AspectFilters.Clear();
   
            this.VariableArcs.Clear();
            this.VariableFilterArcs.Clear();
            this.VariableSetFilterArcs.Clear();
            this.ConceptFilters.Clear();
            this.DimensionFilters.Clear();
            this.Filters.Clear();
            this.FactVariables.Clear();

            this.ValidationRoot = null;

        }

    }
}
