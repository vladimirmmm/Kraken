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
     
        public string CheckCells(LogicalModel.Validation.ValidationParameter parameter) 
        {
            var cellfound = false;
            var sb = new StringBuilder();
            var sequence = parameter.BindAsSequence ? "Sequence" : "";
            sb.AppendLine("parameter: " + parameter.Name + " " + sequence);
            var c_sb = new StringBuilder();
            var log = false;
            foreach (var factgroup in parameter.TaxFacts) 
            {
                foreach (var factid in factgroup)
                {
                    var cellslist = new List<List<String>>();
                    var factkey = Taxonomy.FactsIndex[factid];
                    var fact = FactBase.GetFactFrom(factkey);
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
        
        public List<FactBaseQuery> GetRuleQuery(Hierarchy<XbrlIdentifiable> item)
        {
            //root level
            var rootfilters = item.Children.Where(i => i.Item is Filter).ToList();
            var rootquerypool = new List<List<FactBaseQuery>>();

            foreach (var rootfilter in rootfilters)
            {
                var rootfilterItem = rootfilter.Item as Filter;
                var queries = rootfilterItem.GetQueries(this.Taxonomy, 0);
                //rootquerypool.Add(queries);

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

        public List<string> GetFacts(FactBaseQuery fbq, List<int> IdList, Dictionary<int,bool> IdDict, List<int> factids)
        {

    
            //factids.AddRange(GetFactIDs(fbq, IdList));
            var factsofrule = GetFactsByIds(GetFactIDs(fbq, IdList, IdDict));
            var facts = fbq.ToQueryable(factsofrule.AsQueryable());
         
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
        
        public IEnumerable<int> GetFactIDs(FactBaseQuery fbq, List<int> IdList, Dictionary<int,bool> IdDict)
        {
            IEnumerable<int> ids = new List<int>();


            var rulefactquery = fbq;
            var source = IdList == null ? Taxonomy.FactsIndex.Keys.ToList() : IdList;

            var dimparts = rulefactquery.GetDimensions().Distinct().ToList();
            ids = source;

            if (dimparts.Count > 0)
            {
                //var dimboxes = new List<List<int>>();
                var dimboxes = new List<string>();
                var mincount = 1000000;
                var minix = 0;
                for (int i = 0; i < dimparts.Count; i++)
                {
                    var dimkey = dimparts[i];
                    if (Taxonomy.FactsOfDimensions.ContainsKey(dimkey))
                    {
                        var dimbox = Taxonomy.FactsOfDimensions[dimkey];
                        if (dimbox.Count < mincount) 
                        {
                            mincount = dimbox.Count;
                            minix = i;
                        }
                        dimboxes.Add(dimkey);
                    }
                    else
                    {
                        dimboxes = new List<string>();
                        return ids;
                    }
                }
                IEnumerable<int> dimidlist = Taxonomy.FactsOfDimensions[dimboxes[minix]];
                dimboxes.RemoveAt(minix);
                for (int i = 0; i < dimboxes.Count; i++)
                {
                    var dimbox = Taxonomy.FactsOfDimensions[dimboxes[i]];
                    if (dimidlist.Count() * 3 < dimbox.Count)
                    {
                        dimidlist = Utilities.Objects.IntersectSorted(dimidlist, Taxonomy.FactsOfDimensionsD[dimboxes[i]], null);
                    }
                    else
                    {
                        dimidlist = Utilities.Objects.IntersectSorted(dimidlist, dimbox, null);
                    }
                }
                //foreach (var dim in dimparts)

                if (source.Count < mincount)
                {                
                    ids = Utilities.Objects.IntersectSorted(ids, dimidlist, null);
                }
                else
                {
                    if (ids == IdList)
                    {
                        ids = Utilities.Objects.IntersectSorted(dimidlist, IdDict, null);

                    }
                    else
                    {
                        ids = Utilities.Objects.IntersectSorted(dimidlist, ids, null);
                    }
                }


            }
            if (fbq.ChildQueries.Count > 0)
            {
                var idlist = ids.ToList();
                if (idlist.Count > 100)
                {
                    var childids = new List<int>();
                    foreach (var childquery in fbq.ChildQueries)
                    {
                        childids.AddRange(GetFactIDs(childquery, idlist, IdDict));
                    }

                    return childids;
                }
                else 
                {
                    return idlist;
                }
            }
            return ids;
        }

        public List<String> GetFactsByIds(IEnumerable<int> ids) 
        {
            var factlist = new List<String>();//ids.Count());
            foreach (var id in ids) 
            {
                factlist.Add(Taxonomy.FactsIndex[id]);
            }
            return factlist;
        }

        public void FixRule(Hierarchy<XbrlIdentifiable> rule) 
        {
            var conceptfilters = rule.Where(i => i.Item is ConceptNameFilter).Select(i => i.Item as ConceptNameFilter).ToList();
            foreach (var conceptfilter in conceptfilters) 
            {
                var conceptval = conceptfilter.Concept.QName.Content;
                var conceptns = conceptfilter.Concept.QName.Domain;
                var conceptname = conceptfilter.Concept.QName.Value;
                var conceptsfound = new List<LogicalModel.Concept>();

                if (this.Taxonomy.Concepts.ContainsKey(conceptval))
                {
                    //var existingconcept = this.Taxonomy.Concepts[conceptval];
                    //conceptsfound.Add(existingconcept);
                }
                else
                {
                    var nsm = Utilities.Xml.GetTaxonomyNamespaceManager(Document.XmlDocument);
                    var ns = nsm.LookupNamespace(conceptns);
                    var nsdocument = this.Taxonomy.TaxonomyDocuments.FirstOrDefault(i => i.TargetNamespace == ns);
                    if (nsdocument != null)
                    {
                        var nsprefix = nsdocument.TargetNamespacePrefix;
                        var lookupconcept = new LogicalModel.Base.QualifiedName();
                        lookupconcept.Namespace = nsprefix;
                        lookupconcept.Name = conceptname;
                        if (this.Taxonomy.Concepts.ContainsKey(lookupconcept.Content))
                        {
                            var existingconcept = this.Taxonomy.Concepts[lookupconcept.Content];
                            conceptfilter.Concept.QName.Content = existingconcept.Content;
                            //conceptsfound.Add(existingconcept);
                        }
                    }

                }
            }
        }
        
        public List<String> GetFactsByIdListCollection(IEnumerable<IEnumerable<int>> ids)
        {
            var factlist = new List<String>();//ids.Count());
            foreach (var id in ids)
            {
                factlist.AddRange(GetFactsByIds(id));
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
            target.ChildQueries = source.ChildQueries;
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
