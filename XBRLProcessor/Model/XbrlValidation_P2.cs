using BaseModel;
using LogicalModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using XBRLProcessor.Model.Base;
using XBRLProcessor.Model.DefinitionModel;
using XBRLProcessor.Model.DefinitionModel.Filter;

namespace XBRLProcessor.Model
{
    public partial class XbrlValidation
    {
        public List<LogicalModel.Base.FactGroup> GetGroups(Hierarchy<XbrlIdentifiable> hrule, LogicalModel.Validation.ValidationRule rule)
        {
            if (hrule.Item.ID.Contains("v0784_m")) 
            {

            }
            var rule_conceptfilters = hrule.Children.Where(i => i.Item is ConceptFilter).Select(s => s.Item as ConceptFilter).ToList();
            var rule_dimensionfilters = hrule.Children.Where(i => i.Item is DimensionFilter).Select(s => s.Item as DimensionFilter).ToList();

            var rule_orfilters = hrule.Children.Where(i => i.Item is OrFilter).ToList();
            var rule_andfilters = hrule.Children.Where(i => i.Item is AndFilter).ToList();
            //TODO
            if (rule_orfilters.Count > 0)
            {
                var rule_orfilteritems = hrule.Children.Where(i => i.Item is OrFilter).ToList();

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
            var rule_concepts = rule_conceptfilters.Select(i => Mapping.Mappings.ToLogical(i)).ToList();

            //Load Typed domains
            //var typeddimensionfilters = rule_dimensionfilters.Where(i => i is TypedDimensionFilter).Select(i=>i as TypedDimensionFilter).ToList();
            //foreach (var typeddimensionfilter in typeddimensionfilters) 
            //{
                
            //}
            var NotFunctions = new List<Func<LogicalModel.Base.FactBase, bool>>();
            //rule_dimensionfilters = rule_dimensionfilters.Where(i=>i.Dimension.QName)
            var complementedrulefilters = rule_dimensionfilters.Where(i => i.Complement).ToList();
            foreach (var complementedrulefilter in complementedrulefilters)
            {
                var explicitdimfilter = complementedrulefilter as ExplicitDimensionFilter;
                var typeddimfilter = complementedrulefilter as TypedDimensionFilter;
                if (explicitdimfilter != null)
                {

                    var dimensiondomain = explicitdimfilter.Members.FirstOrDefault().QName.Domain;

                    dimensiondomain = dimensiondomain.IndexOf("_") > -1 ? dimensiondomain.Substring(dimensiondomain.LastIndexOf("_") + 1) : dimensiondomain;

                    var dimensiondomainitems = this.Taxonomy.Hierarchies.Where(i => i.Item.Name == dimensiondomain).SelectMany(i => i.Children).Select(i=>i.Item.Content).Distinct().ToList();
                    dimensiondomainitems = dimensiondomainitems.Except(explicitdimfilter.Members.Select(i => i.QName.Content)).ToList();
                    explicitdimfilter.Members.Clear();
                    foreach (var item in dimensiondomainitems) 
                    {
                        var member = new DimensionMember();
                        member.QName = new QName();
                        member.QName.Content = item;
                        explicitdimfilter.Members.Add(member);
                    }
                    explicitdimfilter.Complement = false;

                }

                if (typeddimfilter != null)
                {
                    NotFunctions.Add((f) => f.Dimensions.Any(i => i.DimensionItem == typeddimfilter.Dimension.QName.Content));
                }

            }

            rule_dimensionfilters = rule_dimensionfilters.Where(i => !i.Complement).ToList();
         

            var rule_dimensions = rule_dimensionfilters.Select(i => Mapping.Mappings.ToLogicalDimensions(i)).ToList();

            //set typed dimension domains
            foreach (var dimensionlist in rule_dimensions) 
            {
                foreach (var dim in dimensionlist) 
                {
                    if (dim.IsTyped) 
                    {
                        dim.Domain = Taxonomy.FindDimensionDomainString(dim.DimensionItem);
                        if (string.IsNullOrEmpty(dim.Domain))
                        {
                            Logger.WriteLine(String.Format("Domain not found for dimension {0}", dim.DimensionItem));
                        }
                    }
                }
            }
         

            var simple_rule_dimensions = rule_dimensions.Where(i => i.Count() == 1).SelectMany(i => i).Where(i => !i.IsDefaultMemeber).ToList();
            var complex_rule_dimension_groups = rule_dimensions.Where(i => i.Count() > 1).ToList();
            List<IEnumerable<LogicalModel.Dimension>> complex_rule_dimensions = Utilities.MathX.CartesianProduct(complex_rule_dimension_groups).ToList();

            if (complex_rule_dimensions.Count == 0)
            {
                complex_rule_dimensions.Add(new List<LogicalModel.Dimension>());
            }

            //getting the concept for the rule
            var factgroups = new List<LogicalModel.Base.FactGroup>();
            var conceptsfound = new List<LogicalModel.Concept>();
            foreach (var conceptrule in rule_concepts) 
            {
                if (this.Taxonomy.Concepts.ContainsKey(conceptrule.Content))
                {
                    var existingconcept = this.Taxonomy.Concepts[conceptrule.Content];
                    conceptsfound.Add(existingconcept);
                }
                else 
                {
                    var nsm = Utilities.Xml.GetTaxonomyNamespaceManager(Document.XmlDocument);
                    var ns = nsm.LookupNamespace(conceptrule.Namespace);
                    var nsdocument = this.Taxonomy.TaxonomyDocuments.FirstOrDefault(i=>i.TargetNamespace==ns);
                    if (nsdocument!=null)
                    {
                        var nsprefix  = nsdocument.TargetNamespacePrefix;
                        var lookupconcept = new LogicalModel.Base.QualifiedName();
                        lookupconcept.Namespace = nsprefix;
                        lookupconcept.Name = conceptrule.Name;
                        if (this.Taxonomy.Concepts.ContainsKey(lookupconcept.Content))
                        {
                            var existingconcept = this.Taxonomy.Concepts[lookupconcept.Content];
                            conceptsfound.Add(existingconcept);
                        }
                    }
            
                }
            }
            LogicalModel.Concept concept = conceptsfound.SingleOrDefault();
            //

            foreach (var complex_fact_dimensions in complex_rule_dimensions)
            {
                var factgroup = new LogicalModel.Base.FactGroup();
                factgroups.Add(factgroup);
                factgroup.Dimensions.AddRange(complex_fact_dimensions.Where(i => !i.IsDefaultMemeber));
                factgroup.Dimensions.AddRange(simple_rule_dimensions);
                factgroup.Not = (f) => NotFunctions.Any(nf => nf(f));
                FixDomains(factgroup.Dimensions);
                factgroup.Concept = concept;

            }

            var rule_orfactgroups = new List<LogicalModel.Base.FactGroup>();

            var ruleset =new List<List<LogicalModel.Base.FactBase>>();

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
                ruleset.Add(rulefacts);
            }

            var allfactgroups = new List<LogicalModel.Base.FactGroup>();

            var factcombinations = Utilities.MathX.CartesianProductList(ruleset);

            foreach (var factcombination in factcombinations)
            {
                var x_fg = new LogicalModel.Base.FactGroup();

                foreach (var rulefact in factcombination)
                {
                    foreach (var mainfactgroup in factgroups)
                    {
                        LogicalModel.Base.FactBase.MergeFact(x_fg, mainfactgroup);

                    }
                    LogicalModel.Base.FactBase.MergeFact(x_fg, rulefact);

                }

                allfactgroups.Add(x_fg);

            }

            var result = new List<LogicalModel.Base.FactGroup>();
            if (allfactgroups.Count > 0)
            {
                result= allfactgroups;
            }
            else
            {
                foreach (var fg in factgroups)
                {
                    fg.Dimensions = fg.Dimensions.OrderBy(i => i.DomainMemberFullName).ToList();
                }
                result= factgroups;
            }

            return result;
        }

        public List<LogicalModel.Base.FactBase> GetFacts(Hierarchy<XbrlIdentifiable> fv ) 
        {
            var facts = new List<LogicalModel.Base.FactBase>();
            var factvariable = fv.Item as FactVariable;
            var or_filters = fv.Where(i => i.Item is OrFilter);
            var and_filters = fv.Where(i => i.Item is AndFilter);
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
                    LogicalModel.Dimension.MergeDimensions(itemfact.Dimensions, paramfact.Dimensions);
                }

            }
            
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
            foreach (var factgroup in parameter.FactGroups.Values) 
            {
                foreach (var fact in factgroup.FullFacts)
                {
                    var cellslist = new List<List<String>>();
                    var factkey = fact.GetFactKey();
                    if (Taxonomy.Facts.ContainsKey(factkey))
                    {
                        var cells = Taxonomy.Facts[factkey];
                        if (parameter.RuleID.Contains("0602"))
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
                                c_sb.AppendLine(parameter.RuleID + " fact found but no cells! " + factkey);
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
                                    c_sb.AppendLine(parameter.RuleID + " for parameter " + parameter.Name + " no cells were found! " + s_fact);
                                }
                            }
                            if (parameter.RuleID.Contains("0602"))
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
                                c_sb.AppendLine(parameter.RuleID + " fact for parameter " + parameter.Name + " not found! " + factkey);
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
                    c_sb.AppendLine("None of the Fact Groups can be found for " + parameter.RuleID + " - " + parameter.Name);
                }
            }
            if (c_sb.Length > 0)
            {
                Logger.WriteLine(c_sb.ToString());
            }
            return sb.ToString();
        }
        
        public void SetFacts(LogicalModel.Base.FactGroup factgroup) 
        {
            foreach (var fact in factgroup.FullFacts) 
            {
                LogicalModel.Dimension.MergeDimensions(fact.Dimensions, factgroup.Dimensions);
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
                    dimension.Domain = Taxonomy.FindDimensionDomainString(dimension.DimensionItem);
                }
            }
        }
        public List<FactBaseQuery> GetRuleQuery(Hierarchy<XbrlIdentifiable> item)
        {
            //root level
            var rootfilters = item.Children.Where(i => i.Item is Filter).ToList();
            var rootquerypool = new List<List<FactBaseQuery>>();

            foreach (var rootfilter in rootfilters)
            {
                var rootfilterItem = rootfilter.Item as Filter;
                var queries = rootfilterItem.GetQueries(this.Taxonomy, 0);
                if (queries.Count == 1)
                {
                    rootquerypool.Add(queries);
                }

            }
            if (rootquerypool.Count == 108)
            {

            }
            var rootqueries = CombineQueries(rootquerypool.ToArray());

            var allqueries = rootqueries;

            return allqueries;
        }
        public List<FactBaseQuery> GetFactQuery(Hierarchy<XbrlIdentifiable> item,int level=0)
        {
            //or queries
            var orfilters = item.Where(i => i.Item is OrFilter).ToList();
            var querycombinationpool = new List<List<FactBaseQuery>>();
            foreach (var orfilter in orfilters)
            {

                var orfilteritem = orfilter.Item as OrFilter;
                var andfilters = orfilter.Where(i => i.Item is AndFilter).ToList();
                var orquerycombinationpool = new List<List<FactBaseQuery>>();
                var queries = new List<FactBaseQuery>();
                foreach (var andfilter in andfilters)
                {
                    var andfilteritem = andfilter.Item as AndFilter;

                    var levelquerypool = new List<List<FactBaseQuery>>();

                    foreach (var filteritem in andfilter.Children)
                    {
                        var levelfilteritem = filteritem.Item as Filter;
                        var levelqueries = levelfilteritem.GetQueries(this.Taxonomy, level);
                        //queries.AddRange(levelqueries);
                        levelquerypool.Add(levelqueries);
                    }
                    var andqueries = CombineQueries(levelquerypool.ToArray());
                    queries.AddRange(andqueries);
                    //orquerycombinationpool.Add(andqueries);

                }

                //List<FactBaseQuery> queries = CombineQueries(orquerycombinationpool.ToArray());

                item.Children.Remove(orfilter);
                querycombinationpool.Add(queries);
            }
            var mergedqueries = CombineQueries(querycombinationpool.ToArray());

            //root level
            var rootfilters = item.Where(i => i.Item is Filter).ToList();
            var rootquerypool = new List<List<FactBaseQuery>>();

            foreach (var rootfilter in rootfilters)
            {
                var rootfilterItem  = rootfilter.Item as Filter;
                rootquerypool.Add(rootfilterItem.GetQueries(this.Taxonomy, level));

            }
            if (rootquerypool.Count == 108) 
            { 

            }
            var rootqueries = CombineQueries(rootquerypool.ToArray());

            var allqueries = CombineQueries(rootqueries, mergedqueries);

            return allqueries;
        }

        public List<string> GetFacts(FactBaseQuery fbq, List<int> IdList, List<int> factids)
        {

            var rulefactquery = fbq;
            //factids.AddRange(GetFactIDs(fbq, IdList));
            var factsofrule = GetFactsByIds(GetFactIDs(fbq, IdList));
            var facts = rulefactquery.ToQueryable(factsofrule.AsQueryable());
         
            foreach (var fact in facts) 
            {
                factids.Add(Taxonomy.FactKeyIndex[fact]);
            }
            return facts.ToList();
        }
        public List<int> GetFactIDsByDict(FactBaseQuery fbq, List<int> IdList)
        {
            List<int> ids = null;


            var rulefactquery = fbq;
            var dimparts = rulefactquery.GetDimensions().Distinct().ToList();
            var source = IdList == null ? Taxonomy.FactsIndex.Keys.ToList() : IdList.ToList();

            if (dimparts.Count > 0)
            {
                ids = source;

                foreach (var dim in dimparts)
                {
                    if (Taxonomy.FactsOfDimensions.ContainsKey(dim))
                    {
                        ids = Utilities.Objects.IntersectSorted(ids, Taxonomy.FactsOfDimensions[dim], null).AsQueryable().ToList();
                    }
                }
            }
            else
            {
                return source;
            }
            return ids;
        }
        public List<int> GetFactIDs(FactBaseQuery fbq, List<int> IdList)
        {
            List<int> ids = null;


            var rulefactquery = fbq;
            var source = IdList == null ? Taxonomy.FactsIndex.Keys.ToList() : IdList;

            var dimparts = rulefactquery.GetDimensions().Distinct().ToList();
            if (dimparts.Count > 0)
            {
                ids = source;
                //foreach (var dim in dimparts)
                for (int i = 0; i < dimparts.Count; i++)
                {
                    var dim = dimparts[i];
                    if (Taxonomy.FactsOfDimensions.ContainsKey(dim))
                    {
                        ids = Utilities.Objects.IntersectSorted(ids, Taxonomy.FactsOfDimensions[dim], null).AsQueryable().ToList();
                    }
                    else 
                    {
                        ids.Clear();
                    }

                }
            }
            else 
            {
                return source;
            }
            return ids;
        }

        public List<String> GetFactsByIds(List<int> ids) 
        {
            var factlist = new List<String>();
            foreach (var id in ids) 
            {
                factlist.Add(Taxonomy.FactsIndex[id]);
            }
            return factlist;
        }

        public void Merge(FactBaseQuery source, FactBaseQuery target)
        {
            if (target.TrueFilters.Length > 150) 
            { 

            }
            target.TrueFilters = target.TrueFilters + source.TrueFilters ;
            target.FalseFilters = target.FalseFilters + source.FalseFilters;
            target.DictFilters = target.DictFilters + source.DictFilters;
            var originalfilter = target.Filter;
            target.Filter = null;
            target.Filter = (s) => originalfilter(s) && source.Filter(s);

        }

        public List<FactBaseQuery> CombineQueries(params List<FactBaseQuery>[] querypool) 
        {
            var nonempty = querypool.Where(i => i.Count > 0);

            var combinedqueries = MathX.CartesianProduct(nonempty);

            var mergedqueries = new List<FactBaseQuery>();
            foreach (var combination in combinedqueries)
            {
                var mergedquery = new FactBaseQuery();
                foreach (var fbq in combination)
                {
                    Merge(fbq, mergedquery);
                }
                mergedqueries.Add(mergedquery);
            }
            return mergedqueries;
        }

    }
}
