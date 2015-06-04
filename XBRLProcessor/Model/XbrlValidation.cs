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
                });

            int z = 0;
        }

        public LogicalModel.Validation.ValidationRule GetLogicalRule() 
        {
            var logicalrule = new LogicalModel.Validation.ValidationRule();
            logicalrule.ID = this.ID;
            var label = new LogicalModel.Label();
            var labelkey = LogicalModel.Label.GetKey("val", this.ValueAssertion.LabelID);
            logicalrule.Label = Taxonomy.FindLabel(labelkey);
            var factvariables = ValidationRoot.Where(i => i.Item is FactVariable).ToList();
            var rule_conceptfilters = this.ValidationRoot.Children.Where(i => i.Item is ConceptFilter).Select(s=>s.Item as ConceptFilter).ToList();
            var rule_dimensionfilters = this.ValidationRoot.Children.Where(i => i.Item is DimensionFilter).Select(s=>s.Item as DimensionFilter).ToList();
            rule_dimensionfilters = rule_dimensionfilters.Where(i => i.Complement == false).ToList();
            var rule_concepts = rule_conceptfilters.Select(i => Mapping.Mappings.ToLogical(i)).ToList();
            var rule_dimensions_x = rule_dimensionfilters.Select(i => Mapping.Mappings.ToLogicalDimensions(i)).ToList();
            var rule_dimensions = rule_dimensionfilters.Select(i => Mapping.Mappings.ToLogicalDimensions(i)).SelectMany(i => i).ToList();
            rule_dimensions=rule_dimensions.Where(i=>!i.IsDefaultMemeber).ToList();
            if (rule_concepts.Count>1)
            {

            }
            var sb = new StringBuilder();
            sb.AppendLine(logicalrule.Label.Content);
            sb.AppendLine(this.ValueAssertion.Test);

            var factgroups = GetGroups();
            
            foreach (var fv in factvariables) 
            {
                var name = fv.Item.ID.Substring(fv.Item.ID.LastIndexOf(".") + 1);
                var parameter = new LogicalModel.Validation.ValidationParameter(name);
                parameter.BindAsSequence = (fv.Item as FactVariable).BindAsSequence;
        
                if (this.ID.Contains("0726") && name=="c")
                {
                }
            
                //TODO
                foreach (var factgroup in factgroups) 
                {
                    var parameterfactgroup= factgroup.Copy();
                    parameter.FactGroups.Add(parameterfactgroup);
                    var facts = GetFacts(fv);
                    parameterfactgroup.Facts = facts;
                    SetFacts(parameterfactgroup);
                }








                /*
                var factstoAdd = new List<LogicalModel.Base.FactBase>();
                foreach (var fact in facts) 
                {
                    if (fact.Concept == null) 
                    {
                        fact.Concept = rule_concepts.FirstOrDefault();
                    }
                    foreach (var dimlist in rule_dimensions_x)
                    {
                        var usabledims = dimlist.Where(i=>!i.IsDefaultMemeber).ToList();
                        if (usabledims.Count > 0)
                        {
                            if (usabledims.Count == 1)
                            {
                                var dim = usabledims.FirstOrDefault();
                                AddDimensionIfNotExists(dim, fact);
                                foreach (var x_fact in factstoAdd) 
                                {
                                    AddDimensionIfNotExists(dim, x_fact);
                                }

                            }
                            else 
                            {
                                int ix = 0;
                                var factdims = fact.Dimensions.ToList();
                                foreach (var dim in usabledims) 
                                {
                                    if (ix == 0)
                                    {
                                        AddDimensionIfNotExists(dim, fact);

                                    }
                                    else 
                                    {
                                        var f = new LogicalModel.Base.FactBase();
                                        f.Dimensions.AddRange(factdims);
                                        f.Concept = fact.Concept;               
                                        AddDimensionIfNotExists(dim, f);
                                        factstoAdd.Add(f);
                                    }
                                    ix++;
                                }
                            }
                        }
                    }                  
                    fact.Dimensions=fact.Dimensions.OrderBy(i=>i.DomainMemberFullName).ToList();
                }

                foreach (var fact in factstoAdd) 
                {
                    fact.Dimensions = fact.Dimensions.OrderBy(i => i.DomainMemberFullName).ToList();
                    facts.Add(fact);
                }
                 */
                //Fixin the domains of Typed Dimensions
                /*
                foreach (var fact in facts) 
                {
                    foreach (var dim in fact.Dimensions)
                    {
                        if (String.IsNullOrEmpty(dim.Domain)) 
                        {
                            //OGR issue
                            dim.Domain = Taxonomy.FindDimensionDomain(dim.DimensionItem);
                        }
                    }
                }
                */
                var typestring = "DoubleValue";
                var firstfact = factgroups.FirstOrDefault().Facts.FirstOrDefault();
                if (firstfact != null && firstfact.Concept != null) 
                {
                    if (firstfact.Concept.ID.StartsWith("ei")) 
                    {
                        typestring = "StringValue";
                    }
                }
                parameter.TypeString = typestring;



                var sequence = parameter.BindAsSequence ? "Sequence":"";
                sb.AppendLine("parameter: " + name + " " + sequence);


                sb.AppendLine(CheckCells(parameter));

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
            var p_rl = new LogicalModel.Validation.ValidationParameter("ReportingLevel");
            p_rl.StringValue = this.Taxonomy.EntryDocument.FileName.Contains("_con") ? "con" : "ind";
            p_rl.TypeString = "StringValue";
            logicalrule.Parameters.Add(p_rl);
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

    }
}
