using BaseModel;
using LogicalModel;
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
        public void SetFacts(LogicalModel.Validation.ValidationRule rule) 
        {
            if (rule.ID.Contains("0147")) 
            {

            }
            var tables = rule.Tables.Select(i => Taxonomy.Tables.FirstOrDefault(t => t.ID == i)).ToList();
            IList<int> tableintevallist = null;
            foreach (var table in tables) 
            {
                tableintevallist = tableintevallist == null ? new IntervalList() : tableintevallist;
                tableintevallist = Utilities.Objects.MergeSorted(tableintevallist, table.FactindexList, null);
            }
            var hastableinfo = tableintevallist != null ? tableintevallist.Count > 0 : false;
            IList<int> allfactsintevallist = new IntervalList(0, Taxonomy.FactsManager.FactsOfPages.Count);

            if (hastableinfo) 
            {
                allfactsintevallist = tableintevallist;
            }
            var ruletypeddimension = rule.BaseQuery.DictFilterIndexes.Where(i => Taxonomy.IsTyped(i));

            foreach (var parameter in rule.Parameters)
            {
                parameter.TaxFacts.Clear();
                IList<int> sdata = tableintevallist;
                if (!parameter.IsGeneral)
                {
                    if (parameter.BaseQuery.DictFilterIndexes.Count == 0)
                    {

                        sdata = allfactsintevallist;
                    }
                    parameter.Data = parameter.BaseQuery.ToIntervalList(this.Taxonomy.FactsOfParts, sdata);
                    parameter.TypedDimensions = parameter.BaseQuery.DictFilterIndexes.Where(i => Taxonomy.IsTyped(i)).ToList();
                    parameter.TypedDimensions = parameter.TypedDimensions.Concat(ruletypeddimension).Distinct().ToList();
                    parameter.CoveredParts = parameter.BaseQuery.GetAspects(this.Taxonomy);
                    if (parameter.FallBackValue == "()" 
                        /*&& parameter.TypedDimensions.Count>0 */
                        && !parameter.BindAsSequence)
                    {
                        parameter.BindAsSequence = true;
                    }
                }
            }
            bool hasfacts = false;
            var ix = 0;
            IList<int> data = tableintevallist;
            if (rule.BaseQuery.DictFilterIndexes.Count == 0) 
            {
                // && rule.BaseQuery.Pools.Count == 0
                //var interval = allfactsinteval;
                //var intervallist = new IntervalList();
                //intervallist.AddInterval(interval);
                data = allfactsintevallist;
            }
            rule.TypedDimensions = rule.BaseQuery.DictFilterIndexes.Where(i => Taxonomy.IsTyped(i)).ToList();
            rule.CoveredParts = rule.BaseQuery.GetAspects(this.Taxonomy);
            var singlefactparameters = rule.Parameters.Where(i => !i.IsGeneral && !i.BindAsSequence).ToList();
            var multifactparameters = rule.Parameters.Where(i => !i.IsGeneral && i.BindAsSequence).ToList();
            var mffnspissue = false;
            LogicalModel.Validation.ValidationParameter p_mffnspissue=null;
            //Utilities.Logger.WriteToFile(String.Format("EnumerateIntervals {0} on {1}", rule.BaseQuery, data));

            foreach (var group in rule.BaseQuery.EnumerateIntervals(this.Taxonomy.FactsOfParts, 0, data, false))
            {
                //Utilities.Logger.WriteToFile("Joining rule with parameters...");

                foreach (var parameter in rule.Parameters)
                {
                    if (!parameter.IsGeneral)
                    {
                        var factsq = Utilities.Objects.IntersectSorted(parameter.Data, group, null);
                        //if (hastableinfo) 
                        //{
                        //    factsq = Utilities.Objects.IntersectSorted(factsq, tableintevallist, null);
                        //}
                        var facts = factsq.ToList();

                        //var facts_domainkeys = facts.Select(i=>Taxonomy.DimensionDomainsOfMembers[i])
                        //var facts = parameter.BaseQuery.EnumerateIntervals(this.Taxonomy.FactsOfParts, 0, group,null).SelectMany(i => i).ToList();
                        if (!parameter.BindAsSequence && facts.Count > 1)
                        {
                            mffnspissue = true;
                            p_mffnspissue = parameter;
                        }
                        if (facts.Count > 0) 
                        {
                            hasfacts = true;
                        }
                        parameter.TaxFacts.Add(facts);
                    }

                
                }
                if (mffnspissue)
                {
                    var factcounts = singlefactparameters.Select(i => new Utilities.KeyValue<int, LogicalModel.Validation.ValidationParameter>(i.TaxFacts.LastOrDefault().Count, i)).OrderByDescending(i => i.Key).ToList();
                    var distinctfactcounts = factcounts.Distinct().ToList();
                    if (singlefactparameters.Count == 1)
                    {
                        mffnspissue = false;
                        var p = singlefactparameters.FirstOrDefault();
                        var lasttaxfact = p.TaxFacts.LastOrDefault();
                        p.TaxFacts.Remove(lasttaxfact);
                        foreach (var fact in lasttaxfact)
                        {
                            p.TaxFacts.Add(new List<int>() { fact });
                        }
                    }
                    if ((distinctfactcounts.Count == 2 && distinctfactcounts[1].Key == 1))
                    {
                        mffnspissue = false;
                        var theparameter = factcounts[0].Value;
                        var parameterstocomplete = factcounts.Where(i => i.Key < factcounts[0].Key).Select(i => i.Value);

                        var facts = theparameter.TaxFacts.LastOrDefault();
                        theparameter.TaxFacts.Clear();
                        var run = 0;
                        foreach (var fact in facts)
                        {
                            theparameter.TaxFacts.Add(new List<int>() { fact });

                            foreach (var p in parameterstocomplete)
                            {
                                var firsttaxfact = p.TaxFacts.FirstOrDefault();
                                if (run > 0)
                                {
                                    p.TaxFacts.Add(firsttaxfact);
                                }

                            }
                            run++;
                        }

                   

                    }
                    if (distinctfactcounts.Count == 1 /*&& multifactparameters.Count==0*/)
                    {
                        mffnspissue = false;
                        foreach (var p in singlefactparameters) 
                        {
                            var lasttaxfact = p.TaxFacts.LastOrDefault();
                            p.TaxFacts.Remove(lasttaxfact);
                            foreach (var fact in lasttaxfact)
                            {
                                p.TaxFacts.Add(new List<int>() { fact });
                            }
                        }



                    }
                }
                if (mffnspissue) 
                {
                    Utilities.Logger.WriteLine(String.Format("{0}: Multiple facts found for Non Sequenced parameter {1}", rule.ID, p_mffnspissue.Name));

                }
                ix++;
                //Utilities.Logger.WriteToFile("End joining rule with parameters");

            }
            ValidationRuleHelper.SetParamerterTypes(Taxonomy, rule);
            if (!hasfacts) 
            {
                Utilities.Logger.WriteLine(String.Format("{0}: Rule has no facts!", rule.ID));
            }
            var parameterswithissues = singlefactparameters.Where(i => i.TaxFacts.Any(f=>f.Count>1)).ToList();
            if (parameterswithissues.Count > 0) 
            {
                Utilities.Logger.WriteLine(String.Format("{0}: Rule single factrule has multiple facts!", rule.ID));

            }
        }
        
        public FactBaseQuery GetQuery(Hierarchy<XbrlIdentifiable> item, int level = 0)
        {
            var query = new FactBaseQuery();
            foreach (var child in item.Children) 
            {
                var filter = child.Item as Filter;
                if (filter != null) 
                {
                
                    filter.GetQuery(this.Taxonomy,child, query);
                }

            }
            return query;
        }
       
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
                if (queries.Count > 0)
                {
                    if (queries.Count == 1)
                    {
                        rootquerypool.Add(queries);
                    }
                    else
                    {

                    }
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

        public List<FactBaseQuery> GetRuleBaseQuery(Hierarchy<XbrlIdentifiable> item)
        {
            //root level
            var rootfilters = item.Children.Where(i => i.Item is Filter).ToList();
            var rootquerypool = new List<List<FactBaseQuery>>();

            foreach (var rootfilter in rootfilters)
            {
                var rootfilterItem = rootfilter.Item as Filter;
                var queries = rootfilterItem.GetQueries(this.Taxonomy, 0);
                //rootquerypool.Add(queries);
                if (queries.Count > 0)
                {
                    if (queries.Count == 1)
                    {
                        rootquerypool.Add(queries);
                        item.Children.Remove(rootfilter);
                    }
                    else
                    {

                    }
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

            var rootqueries = CombineQueries(rootquerypool.ToArray());

            var allqueries = CombineQueries(rootqueries, mergedqueries);
            foreach (var qry in allqueries) 
            {
                qry.NrOfDictFilters = qry.DictFilterIndexes.Count;
            }
            return allqueries;
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
                    FactBaseQuery.Merge(fbq, mergedquery);
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
                    FactBaseQuery.Merge(fbq, mergedquery);

                    ix++;
                }
                mergedqueries.Add(mergedquery);
            }
            return mergedqueries;
        }

    }
}
