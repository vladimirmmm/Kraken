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
    public class XbrlValidation
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
            foreach (var fv in factvariables) 
            {
                var name = fv.Item.ID.Substring(fv.Item.ID.LastIndexOf(".") + 1);
                var parameter = new LogicalModel.Validation.ValidationParameter(name);
                parameter.BindAsSequence = (fv.Item as FactVariable).BindAsSequence;
                var or_filters = fv.Where(i => i.Item is OrFilter).ToList();
                var and_filters = fv.Where(i => i.Item is AndFilter).ToList();
                var facts = new List<LogicalModel.Base.FactBase>();
                if (this.ID.Contains("0726") && name=="c")
                {
                }
                //TODO
                foreach (var f_or in or_filters) 
                {
                    foreach (var child in f_or.Children) 
                    {
                        var fact = new LogicalModel.Base.FactBase();

                        var f_and = child.Item as AndFilter;
                        if (f_and == null)
                        {

                        }
                        else 
                        {
                            var concepts = GetConcepts(child);
                            if (concepts.Count > 1) 
                            {

                            }
                            if (concepts.Count == 1)
                            {
                                fact.Concept = concepts.FirstOrDefault();
                            }
   
                            var dimensions = GetDimensions(child).SelectMany(i => i).ToList();
                            dimensions = dimensions.Where(i => !i.IsDefaultMemeber).ToList();
                            fact.Dimensions.AddRange(dimensions);
                        }
                        facts.Add(fact);
                    }

                }

                if (or_filters.Count == 0)
                {
                  

                    var param_concepts = GetConcepts(fv);
                    var param_dimensions = GetDimensions(fv).SelectMany(i => i).Where(i => !i.IsDefaultMemeber).ToList();
                    var groups = param_dimensions.GroupBy(i => i.DimensionItemWithDomain);
             
                    var simpledimensions = groups.Where(i=>i.Count()==1).SelectMany(i=>i).ToList();
                    var dimensionsets = new List<IEnumerable<LogicalModel.Dimension>>();

                    var multigroups = groups.Where(i=>i.Count()>1).ToList();
                    
                    var multidimensions = new List<List<LogicalModel.Dimension>>();
                    foreach (var group in multigroups)
                    {
                        multidimensions.Add(group.ToList());
                      
                    }

                    if (multidimensions.Count > 0)
                    {
                        dimensionsets.AddRange(Utilities.MathX.CartesianProduct(multidimensions));

                    }
                    else 
                    {
                        dimensionsets.Add(new List<LogicalModel.Dimension>());

                    }
                    foreach (var dimensionset in dimensionsets)
                    {
                        var fact = new LogicalModel.Base.FactBase();
                        fact.Concept = param_concepts.Count == 1 ? param_concepts.FirstOrDefault() : null;
                        fact.Dimensions.AddRange(dimensionset);
                        fact.Dimensions.AddRange(simpledimensions);
                        facts.Add(fact);
                    }
                    //}
                    //param_dimensions = param_dimensions.Where(i => !i.IsDefaultMemeber).ToList();

                    //var fact = new LogicalModel.Base.FactBase();
                    //fact.Concept = param_concepts.Count == 1 ? param_concepts.FirstOrDefault() : null;
                    //fact.Dimensions.AddRange(param_dimensions);
                    //facts.Add(fact);
                }
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
                var typestring = "DoubleValue";
                var firstfact = facts.FirstOrDefault();
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
                var cellfound = false;
                foreach (var fact in facts)
                {
                    var cellslist = new List<String>();
                    var factkey = fact.GetFactKey();
                    if (Taxonomy.Facts.ContainsKey(factkey))
                    {
                        var cells = Taxonomy.Facts[factkey];
                        cellslist.AddRange(cells);
                        if (cells.Count == 0) 
                        {
                            Console.WriteLine(this.ID + " not found! " + factkey);
                        }
                    }
                    else
                    {

                        var s_facts = Taxonomy.Facts.Keys.AsEnumerable();
                        if (fact.Concept != null)
                        {
                            s_facts = s_facts.Where(i => i.StartsWith(fact.Concept.Content));
                        }
                        foreach (var dimension in fact.Dimensions)
                        {
                            s_facts = s_facts.Where(i => i.Contains(dimension.DomainMemberFullName));
                        }
                        var s_factlist = s_facts.ToList();
                        if (s_factlist.Count > 0)
                        {
                            foreach (var s_fact in s_factlist)
                            {
                                var cells = Taxonomy.Facts[s_fact];
                                if (cells.Count == 0) 
                                {
                                    Console.WriteLine(this.ID + " for parameter " +parameter.Name+ " not found! " + s_fact);

                                }
                                cellslist.AddRange(cells);
                            }
                        }
                        else
                        {
                            Console.WriteLine(this.ID + " fact for parameter " + parameter.Name + " not found! " + factkey);

                        }

                    }
                    if (cellslist.Count == 0)
                    {

                    }
                    else 
                    {
                        cellfound = true;
                    }
                    //sb.AppendLine(fact.GetFactKey());
                    foreach (var cell in cellslist)
                    {                   
                        sb.Append(cell + ", ");
                    }
                    //sb.AppendLine();
                    sb.AppendLine();

                }
                if (!cellfound) 
                {

                }
                sb.AppendLine();

                parameter.Facts.AddRange(facts);
                logicalrule.Parameters.Add(parameter);
              
            }
            var pc = 0;
            foreach (var pv in logicalrule.Parameters) 
            {
                if (pc == 0) 
                {
                    pc = pv.Facts.Count;
                }
                if (pc != pv.Facts.Count)                 
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
