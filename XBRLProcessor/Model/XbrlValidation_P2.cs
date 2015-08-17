using BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBRLProcessor.Model.Base;
using XBRLProcessor.Model.DefinitionModel;
using XBRLProcessor.Model.DefinitionModel.Filter;

namespace XBRLProcessor.Model
{
    public partial class XbrlValidation
    {
        public List<LogicalModel.Base.FactGroup> GetGroups()
        {
            var rule_conceptfilters = this.ValidationRoot.Children.Where(i => i.Item is ConceptFilter).Select(s => s.Item as ConceptFilter).ToList();
            var rule_dimensionfilters = this.ValidationRoot.Children.Where(i => i.Item is DimensionFilter).Select(s => s.Item as DimensionFilter).ToList();
            
            var rule_orfilters = this.ValidationRoot.Children.Where(i => i.Item is OrFilter).ToList();
            var rule_andfilters = this.ValidationRoot.Children.Where(i => i.Item is AndFilter).ToList();
            //TODO
            if (rule_orfilters.Count > 0)
            {
                var rule_orfilteritems = this.ValidationRoot.Children.Where(i => i.Item is OrFilter).ToList();

                var mainorfilter = new Hierarchy<XbrlIdentifiable>();
                mainorfilter.Children.AddRange(rule_orfilteritems);
                var facts = GetFacts(mainorfilter);
                if (facts.Count > 0)
                {

                }
            }
            if (rule_andfilters.Count > 0)
            {

            }

            rule_dimensionfilters = rule_dimensionfilters.Where(i => i.Complement == false).ToList();

            var rule_concepts = rule_conceptfilters.Select(i => Mapping.Mappings.ToLogical(i)).ToList();
            var rule_dimensions = rule_dimensionfilters.Select(i => Mapping.Mappings.ToLogicalDimensions(i)).ToList();

            var simple_rule_dimensions = rule_dimensions.Where(i => i.Count() == 1).SelectMany(i => i).Where(i => !i.IsDefaultMemeber).ToList();
            var complex_rule_dimension_groups = rule_dimensions.Where(i => i.Count() > 1).ToList();
            List<IEnumerable<LogicalModel.Dimension>> complex_rule_dimensions = Utilities.MathX.CartesianProduct(complex_rule_dimension_groups).ToList();

            if (complex_rule_dimensions.Count == 0)
            {
                complex_rule_dimensions.Add(new List<LogicalModel.Dimension>());
            }

            var factgroups = new List<LogicalModel.Base.FactGroup>();

            var concept = rule_concepts.SingleOrDefault();
            foreach (var complex_fact_dimensions in complex_rule_dimensions)
            {
                var factgroup = new LogicalModel.Base.FactGroup();
                factgroups.Add(factgroup);
                factgroup.Dimensions.AddRange(complex_fact_dimensions.Where(i => !i.IsDefaultMemeber));
                factgroup.Dimensions.AddRange(simple_rule_dimensions);
                FixDomains(factgroup.Dimensions);
                factgroup.Concept = concept;

            }

            var rule_orfactgroups = new List<LogicalModel.Base.FactGroup>();
            foreach (var rule_orfilter in rule_orfilters) 
            {
                var rulefacts = new List<LogicalModel.Base.FactBase>();

                foreach (var child in rule_orfilter.Children)
                {
                    var f_and = child.Item as AndFilter;
                    if (f_and != null) 
                    {
                        var fact = new LogicalModel.Base.FactBase();
                        var concepts = GetConcepts(child);
               
                        if (concepts.Count == 1)
                        {
                            fact.Concept = concepts.FirstOrDefault();
                        }

                        var dimensions = GetDimensions(child).SelectMany(i => i).ToList();
                        dimensions = dimensions.Where(i => !i.IsDefaultMemeber).ToList();
                        fact.Dimensions.AddRange(dimensions);
                        rulefacts.Add(fact);
                    }
                }
                var allfactgroups = new List<LogicalModel.Base.FactGroup>();
                foreach (var rulefact in rulefacts)
                {
                    var orrule_fg = new LogicalModel.Base.FactGroup();
                    orrule_fg.Concept = rulefact.Concept;
                    orrule_fg.Dimensions = rulefact.Dimensions;
                    foreach (var mainfactgroup in factgroups)
                    {
                        var x_fg = new LogicalModel.Base.FactGroup();
                        x_fg.Concept = mainfactgroup.Concept == null ? orrule_fg.Concept : mainfactgroup.Concept;
                        x_fg.Dimensions.AddRange(mainfactgroup.Dimensions);
                        MergeDimensions(x_fg.Dimensions, orrule_fg.Dimensions);
                        allfactgroups.Add(x_fg);
                        
                    }
                }
             
                return allfactgroups;
            }
            foreach (var fg in factgroups)
            {
                fg.Dimensions = fg.Dimensions.OrderBy(i => i.DomainMemberFullName).ToList();
            }
            return factgroups;
        }

        public List<LogicalModel.Base.FactBase> GetFacts(Hierarchy<XbrlIdentifiable> fv) 
        {
            var facts = new List<LogicalModel.Base.FactBase>();
            var factvariable = fv.Item as FactVariable;
            var or_filters = fv.Where(i => i.Item is OrFilter).ToList();
            var and_filters = fv.Where(i => i.Item is AndFilter).ToList();
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
                facts.Add(new LogicalModel.Base.FactBase());
            }
            var paramfacts = GetRootParamLevelFacts(fv);
            if (paramfacts.Count > 1) 
            {

            }
            //var firstparamfact = paramfacts.FirstOrDefault();
            var multipliedfacts = new List<LogicalModel.Base.FactBase>();
            foreach (var fact in facts) 
            {
                foreach (var paramfact in paramfacts)
                {
                    var itemfact = new LogicalModel.Base.FactBase();
                    itemfact.Dimensions.AddRange(fact.Dimensions);
                    itemfact.Concept = fact.Concept;
                    multipliedfacts.Add(itemfact);

                    if (itemfact.Concept == null)
                    {
                        itemfact.Concept = paramfact.Concept;
                    }
                    MergeDimensions(itemfact.Dimensions, paramfact.Dimensions);
                }

            }
            /*
            if (or_filters.Count == 0)
            {


                var param_concepts = GetConcepts(fv);
                var param_dimensions = GetDimensions(fv).SelectMany(i => i).Where(i => !i.IsDefaultMemeber).ToList();
                var groups = param_dimensions.GroupBy(i => i.DimensionItemWithDomain);

                var simpledimensions = groups.Where(i => i.Count() == 1).SelectMany(i => i).ToList();
                var dimensionsets = new List<IEnumerable<LogicalModel.Dimension>>();

                var multigroups = groups.Where(i => i.Count() > 1).ToList();

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

            }
            foreach (var fact in facts) 
            {
                FixDomains(fact.Dimensions);
            }
            return facts
            */
            foreach (var fact in multipliedfacts) 
            {
                FixDomains(fact.Dimensions);
            }
            return multipliedfacts;
        }

        protected List<LogicalModel.Base.FactBase> GetRootParamLevelFacts(Hierarchy<XbrlIdentifiable> fv) 
        {
            var facts = new List<LogicalModel.Base.FactBase>();
            var orfilters = fv.Children.Where(i => i.Item is OrFilter).ToList();
            for (int i = 0; i < orfilters.Count;i++ )
            {
                fv.Children.Remove(orfilters[i]);
            }


            var param_concepts = GetConcepts(fv);
            var param_dimensions = GetDimensions(fv).SelectMany(i => i).Where(i => !i.IsDefaultMemeber).ToList();
            var groups = param_dimensions.GroupBy(i => i.DimensionItemWithDomain);

            var simpledimensions = groups.Where(i => i.Count() == 1).SelectMany(i => i).ToList();
            var dimensionsets = new List<IEnumerable<LogicalModel.Dimension>>();

            var multigroups = groups.Where(i => i.Count() > 1).ToList();

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
            foreach (var orfilter in orfilters)
            {
                fv.Children.Add(orfilter);
            }
            return facts;
        }

        public string CheckCells(LogicalModel.Validation.ValidationParameter parameter) 
        {
            var cellfound = false;
            var sb = new StringBuilder();
            var sequence = parameter.BindAsSequence ? "Sequence" : "";
            sb.AppendLine("parameter: " + parameter.Name + " " + sequence);
            var c_sb = new StringBuilder();
            var log = false;
            foreach (var factgroup in parameter.FactGroups) 
            {
                foreach (var fact in factgroup.Facts)
                {
                    var cellslist = new List<List<String>>();
                    var factkey = fact.GetFactKey();
                    if (Taxonomy.Facts.ContainsKey(factkey))
                    {
                        var cells = Taxonomy.Facts[factkey];
                        if (this.ID.Contains("0602"))
                        {
                            cellslist.Add(cells.Select(i => i + " {" + factkey + "} ").ToList());
                        }
                        else
                        {
                            cellslist.Add(cells);
                        }
                        if (cells.Count == 0)
                        {
                            if (log)
                            {
                                c_sb.AppendLine(this.ID + " fact found but no cells! " + factkey);
                            }
                        }
                    }
                    else
                    {

                        var s_facts = Taxonomy.Facts.Keys.AsEnumerable();
                        if (fact.Concept != null)
                        {
                            //s_facts = s_facts.Where(i => i.StartsWith(fact.Concept.Content));
                            if (Taxonomy.FactsOfConcepts.ContainsKey(fact.Concept.Content))
                            {
                                s_facts = Taxonomy.FactsOfConcepts[fact.Concept.Content];
                            }
                            else
                            {
                                s_facts = new List<string>();
                            }
                        }
                        foreach (var dimension in fact.Dimensions)
                        {
                            s_facts = s_facts.Where(i => i.Contains(dimension.DomainMemberFullName));
                        }
                       // var s_factlist = s_facts.ToList();
                        var hasanyfact = false;

                        foreach (var s_fact in s_facts)
                        {
                            hasanyfact = true;
                            var cells = Taxonomy.Facts[s_fact];
                            if (cells.Count == 0)
                            {
                                if (log)
                                {
                                    c_sb.AppendLine(this.ID + " for parameter " + parameter.Name + " no cells were found! " + s_fact);
                                }
                            }
                            if (this.ID.Contains("0602"))
                            {
                                cellslist.Add(cells.Select(i => i + " {" + factkey + "} ").ToList());
                            }
                            else
                            {
                                cellslist.Add(cells);
                            }
                        }
                       
                        if (!hasanyfact)
                        {
                            if (log)
                            {
                                c_sb.AppendLine(this.ID + " fact for parameter " + parameter.Name + " not found! " + factkey);
                            }
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
                    foreach (var cells in cellslist)
                    {
                        foreach (var cell in cells)
                        {
                            sb.Append(cell + ", ");
                        }
                        sb.AppendLine();

                    }
                }
            }
            if (!cellfound)
            {
                if (log)
                {
                    c_sb.AppendLine("None of the Fact Groups can be found for " + this.ID + " - " + parameter.Name);
                }
            }
            if (c_sb.Length > 0)
            {
                Console.WriteLine(c_sb.ToString());
            }
            return sb.ToString();
        }
        
        public void SetFacts(LogicalModel.Base.FactGroup factgroup) 
        {
            foreach (var fact in factgroup.Facts) 
            {
                MergeDimensions(fact.Dimensions, factgroup.Dimensions);
                if (fact.Concept == null) 
                {
                    fact.Concept = factgroup.Concept;
                }
                fact.Dimensions = fact.Dimensions.OrderBy(i => i.DomainMemberFullName).ToList();
            }
   
        }

        private void FixDomains(List<LogicalModel.Dimension> dimensionlist) 
        {
            foreach (var dimension in dimensionlist) 
            {
                if (String.IsNullOrEmpty(dimension.Domain))
                {
                    //OGR issue
                    dimension.Domain = Taxonomy.FindDimensionDomain(dimension.DimensionItem);
                }
            }
        }

        private void MergeDimensions(List<LogicalModel.Dimension> target, List<LogicalModel.Dimension> items)
        {
            foreach (var item in items)
            {
                var existing = target.FirstOrDefault(i => i.Domain == item.Domain && i.DimensionItem == item.DimensionItem);
                if (existing == null)
                {
                    target.Add(item);
                }
            }
        }
    }
}
