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

        public void RemoveCommon(FactBaseQuery querytoremovefrom, FactBaseQuery query) 
        {
            var common_dfs = query.DictFilterIndexes.Intersect(querytoremovefrom.DictFilterIndexes).ToList();
            var common_ndfs = query.NegativeDictFilterIndexes.Intersect(querytoremovefrom.NegativeDictFilterIndexes).ToList();
            foreach (var common_df in common_dfs) 
            {
                querytoremovefrom.DictFilterIndexes.Remove(common_df);
            }
            foreach (var common_ndf in common_ndfs)
            {
                querytoremovefrom.NegativeDictFilterIndexes.Remove(common_ndf);
            }
            querytoremovefrom.DictFilters = "";
            foreach (var df in querytoremovefrom.DictFilterIndexes) 
            {
                querytoremovefrom.DictFilters += Taxonomy.CounterFactParts[df]+ ",";
            }
            querytoremovefrom.FalseFilters = "";
            foreach (var ndf in querytoremovefrom.NegativeDictFilterIndexes)
            {
                querytoremovefrom.FalseFilters += Taxonomy.CounterFactParts[ndf] + ",";
            }
            //querytoremovefrom.NrOfDictFilters = querytoremovefrom.DictFilterIndexes.Count;
        }

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
                    var factkey = Taxonomy.FactsManager.GetFactKey(factid);
                    var fact = FactBase.GetFactFrom(Taxonomy.GetFactStringKey(factkey));
                    if (Taxonomy.HasFact(factkey))
                    {
                        var cells = Taxonomy.GetCellsOfFact(factkey);
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

                        var s_facts = Taxonomy.FactKeysAsEnumerable().Select(i=>i).AsEnumerable();
                        if (fact.Concept != null)
                        {
                            //s_facts = s_facts.Where(i => i.StartsWith(fact.Concept.Content));
                            //if (Taxonomy.FactsOfDimensions.ContainsKey(fact.Concept.Content))
                            var ix = Taxonomy.FactParts[fact.Concept.Content];
                            if (Taxonomy.FactsOfParts.ContainsKey(ix))
                            {
                                s_facts = Taxonomy.FactsOfParts[ix].Select(
                                    i => Taxonomy.FactsManager.GetFactKey(i));
                            }
                            else
                            {
                                s_facts = new List<int[]>();
                            }
                        }
                        foreach (var dimension in fact.Dimensions)
                        {
                            s_facts = s_facts.Where(i => Taxonomy.GetFactStringKey(i).Contains(dimension.DomainMemberFullName));
                        }
                       // var s_factlist = s_facts.ToList();
                        var hasanyfact = false;

                        foreach (var s_fact in s_facts)
                        {
                            hasanyfact = true;
                            var cells = Taxonomy.GetCellsOfFact(s_fact);
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

            var rootqueries = CombineQueries(rootquerypool.ToArray());

            var allqueries = rootqueries;
            foreach (var qry in allqueries)
            {
                qry.NrOfDictFilters = qry.DictFilterIndexes.Count;
            }
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
            foreach (var qry in allqueries) 
            {
                qry.NrOfDictFilters = qry.DictFilterIndexes.Count;
            }
            return allqueries;
        }

        public List<string> GetFacts(FactBaseQuery fbq, List<int> IdList, HashSet<int> IdDict, List<int> factids)
        {

    
            //factids.AddRange(GetFactIDs(fbq, IdList));
            var factsofrule = GetFactsByIds(GetFactIDs(fbq, IdList, IdDict));
            var facts = fbq.ToQueryable(factsofrule);
         
            foreach (var fact in facts) 
            {
                var key = Taxonomy.GetFactIntKey(fact).ToArray();
                //Taxonomy.Facts[key]
                factids.Add(Taxonomy.FactsManager.GetFactIndex(key));
            }
            return facts.ToList();
        }

        public List<KeyValue<string,int>> GetFactsKV(FactBaseQuery fbq, List<int> IdList, HashSet<int> IdDict)
        {


            //factids.AddRange(GetFactIDs(fbq, IdList));
            var factsofrule = GetFactsKVByIds(GetFactIDs(fbq, IdList, IdDict));
            var facts = fbq.ToList(factsofrule);
 
            //foreach (var fact in facts)
            //{
            //    var key = Taxonomy.GetFactIntKey(fact).ToArray();
            //    //Taxonomy.Facts[key]
            //    factids.Add(Taxonomy.FactKeyIndex[key]);
            //}
            return facts.ToList();
        }
        
        public List<int> GetFactIDsByDict(FactBaseQuery fbq, List<int> IdList)
        {
            IEnumerable<int> ids = null;


            var rulefactquery = fbq;
            var dimparts = rulefactquery.GetDimensions().Distinct().ToList();
            var source = IdList == null ? Taxonomy.FactIndexEnumerable() : IdList;

            if (dimparts.Count > 0)
            {
                ids = source;
                var firstdimpartix = Taxonomy.FactParts[dimparts[0]];
                ids = Taxonomy.FactsOfParts[firstdimpartix];
                for (int i = 1; i < dimparts.Count; i++) 
                {
                    var ix = Taxonomy.FactParts[dimparts[i]];
                    if (Taxonomy.FactsOfParts.ContainsKey(ix))
                    {
                        ids = Utilities.Objects.IntersectSorted(ids, Taxonomy.FactsOfParts[ix], null).AsQueryable().ToList();
                    }
                }
      
            }
            else
            {
                return source.ToList();
            }
            return ids.ToList();
        }
        
        public IEnumerable<int> GetFactIDs(FactBaseQuery fbq, List<int> IdList, HashSet<int> IdDict)
        {
            IEnumerable<int> ids = new List<int>();


            var rulefactquery = fbq;
            var source = IdList == null ? Taxonomy.FactIndexEnumerable().ToList() : IdList;

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
                    //FP if (Taxonomy.FactsOfDimensions.ContainsKey(dimkey))
                    var ix = Taxonomy.FactParts[dimkey];
                    if (Taxonomy.FactsOfParts.ContainsKey(ix))
                    {
                        var dimbox = Taxonomy.FactsOfParts[ix];
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
                //IEnumerable<int> dimidlist = Taxonomy.FactsOfDimensions[dimboxes[minix]];
                IEnumerable<int> dimidlist = null;
                //FP var mindimset = Taxonomy.FactsOfDimensions[dimboxes[minix]];
                var dix = Taxonomy.FactParts[dimboxes[minix]];
                var mindimset = Taxonomy.FactsOfParts[dix];
                if (IdList.Count < mindimset.Count)
                {
                    dimidlist = Utilities.Objects.IntersectSorted(IdList, mindimset, null);
                }
                else 
                {
                    //dimidlist = Utilities.Objects.IntersectSorted(mindimset, IdList, null);
                    dimidlist = Utilities.Objects.IntersectSorted(mindimset, IdDict, null);
                    //dimidlist = mindimset;

                }

                dimboxes.RemoveAt(minix);
                for (int i = 0; i < dimboxes.Count; i++)
                {
                    //FP var dimbox = Taxonomy.FactsOfDimensions[dimboxes[i]];
                    var dix2 = Taxonomy.FactParts[dimboxes[i]];
                    var dimbox = Taxonomy.FactsOfParts[dix2];
                    //var dimidlistcount = i == 0 ? mincount : dimidlist.Count();
                    //if (dimidlistcount * 3 < dimbox.Count)
                    //{
                    //    dimidlist = Utilities.Objects.IntersectSorted(dimidlist, Taxonomy.FactsOfDimensions[dimboxes[i]], null);
                    //}
                    //else
                    //{
                    //    dimidlist = Utilities.Objects.IntersectSorted(dimidlist, dimbox, null);
                    //}
              
                    dimidlist = Utilities.Objects.IntersectSorted(dimidlist, dimbox, null);
                    //dimidlist = dimidlist.Intersect(dimbox);

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
                        if (dimidlist.Count() <100 )
                        {
                            ids = dimidlist;
                        }
                        else
                        {
                            ids = Utilities.Objects.IntersectSorted(dimidlist, IdDict, null);
                        }
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
                var key = Taxonomy.GetFactStringKey(Taxonomy.FactsManager.GetFactKey(id));
                factlist.Add(key);
            }
            return factlist;
        }

        public List<KeyValue<string,int>> GetFactsKVByIds(IEnumerable<int> ids)
        {
            var factlist = new List<KeyValue<string, int>>();//ids.Count());
            foreach (var id in ids)
            {
                var key = Taxonomy.GetFactStringKey(Taxonomy.FactsManager.GetFactKey(id));
                factlist.Add(new KeyValue<string, int>(key, id));
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
            target.TrueFilters = target.TrueFilters + source.TrueFilters ;
            target.FalseFilters = target.FalseFilters + source.FalseFilters;
            target.DictFilters = target.DictFilters + source.DictFilters;
            target.DictFilterIndexes.AddRange(source.DictFilterIndexes);
            target.NegativeDictFilterIndexes.AddRange(source.NegativeDictFilterIndexes);
            target.ChildQueries = source.ChildQueries;
            var originalfilter = target.Filter;
            target.Filter = null;
            target.Cover = source.Cover;
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

        public List<FactBaseQuery> SoftCombineQueries(params List<FactBaseQuery>[] querypool)
        {
            var nonempty = querypool.Where(i => i.Count > 0);

            var combinedqueries = MathX.CartesianProduct(nonempty);

            var mergedqueries = new List<FactBaseQuery>();
            foreach (var combination in combinedqueries)
            {
                var mergedquery = new FactBaseQuery();
                var combinationlist = combination.ToList();
                var ix = 0;
                foreach (var fbq in combinationlist)
                {
                    //Merge(fbq, mergedquery);
                    mergedquery.NrOfDictFilters += fbq.NrOfDictFilters;
                    //if (ix == combinationlist.Count - 1) 
                    //{
                    //    Merge(fbq, mergedquery);
                    //}
                    Merge(fbq, mergedquery);

                    ix++;
                }
                mergedqueries.Add(mergedquery);
            }
            return mergedqueries;
        }

    }
}
