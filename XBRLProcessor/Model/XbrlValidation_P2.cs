using BaseModel;
using LogicalModel;
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
using XBRLProcessor.Models;

namespace XBRLProcessor.Model
{
    public partial class XbrlValidation
    {
        public void SetFacts(LogicalModel.Validation.ValidationRule rule) 
        {
            //foreach (var parameter in rule.Parameters) 
            //{
            //    parameter.Data = parameter.BaseQuery.ToIntervalList(this.Taxonomy,null);
            //}
            bool hasfacts = false;
            var ix = 0;
            IList<int> data = null;
            if (rule.BaseQuery.DictFilterIndexes.Count == 0) 
            {
                // && rule.BaseQuery.Pools.Count == 0
                var interval = new Interval(0, Taxonomy.FactsManager.FactsOfPages.Count);
                var intervallist = new IntervalList();
                intervallist.AddInterval(interval);
                data = intervallist;
            }
            foreach (var group in rule.BaseQuery.EnumerateIntervals(this.Taxonomy, 0, data))
            {
                foreach (var parameter in rule.Parameters)
                {
                    if (!parameter.IsGeneral)
                    {
                        //var facts = Utilities.Objects.IntersectSorted(parameter.Data, group, null);
                        var facts = parameter.BaseQuery.EnumerateIntervals(Taxonomy, 0, group).SelectMany(i => i).ToList();
                        if (!parameter.BindAsSequence && facts.Count > 1)
                        {
                            Utilities.Logger.WriteLine(String.Format("{0}: Multiple facts found for Non Sequenced parameter {1}", rule.ID, parameter.Name));

                        }
                        if (facts.Count > 0) 
                        {
                            hasfacts = true;
                        }
                        parameter.TaxFacts.Add(facts);
                    }
                }
                ix++;
            }
            if (!hasfacts) 
            {
                Utilities.Logger.WriteLine(String.Format("{0}: Rule has no facts!", rule.ID));
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

        public FactQueryPool GetQueryPool(Hierarchy<XbrlIdentifiable> item) 
        {
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
                        var levelqueries = levelfilteritem.GetQueries(this.Taxonomy, 0);
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
            return null;
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

        public LogicalModel.Validation.ValidationRule GetLogicalRule_Old(Hierarchy<XbrlIdentifiable> hrule, XbrlTaxonomyDocument document)
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

            var sb = new StringBuilder();
            sb.AppendLine(logicalrule.DisplayText);
            sb.AppendLine(valueassertion.Test);
            var rawval = document.FileName + "\r\n" + document.LocalPath + "\r\n" + logicalrule.DisplayText + "\r\n" + logicalrule.OriginalExpression + "\r\n" + hrule.ToHierarchyString(i => i.ToString()) + "\r\n";

            Utilities.FS.AppendAllText(Taxonomy.TaxonomyValidationFolder + "Validations_XML.txt", rawval);


            var factvariables = tmp_rule.Where(i => i.Item is FactVariable);
            foreach (var fv in factvariables)
            {
                tmp_rule.Remove(fv);
            }
            if (logicalrule.ID == "s2md_BV34-1")
            {
                //var rulebasequeryX = GetRuleQuery(tmp_rule).FirstOrDefault();

            }
            var rulebasequery = GetRuleBaseQuery(tmp_rule).FirstOrDefault();

            var rulefactqueries = GetFactQuery(tmp_rule);
            IList<int> rulefactIds = new IntervalList();

            //var rulebasequery = GetRuleQuery(tmp_rule).FirstOrDefault();


            var rbds = new List<string>();
            bool RuleHasDictFilter = false;
            if (rulebasequery != null)
            {
                RuleHasDictFilter = rulebasequery.DictFilterIndexes.Count > 0;
                rulefactIds = rulebasequery.ToIntervalList(Taxonomy, null);

            }
            else
            {
                rulebasequery = new LogicalModel.Base.FactBaseQuery();
            }
            var additionalfacts = new IntervalList();// List<int>();
            foreach (var rulefactquery in rulefactqueries)
            {
                rulefactquery.NrOfDictFilters += rulebasequery.NrOfDictFilters;
                //RemoveCommon(rulefactquery, rulebasequery);

                //var factindexes = rulefactquery.ToIntervalList(Taxonomy, rulebasequery == null ? null : rulefactIds);
                //additionalfacts.AddRange(factindexes);
            }

            if (additionalfacts.Count > 0)
            {
                rulefactIds = additionalfacts;
            }


            //


            foreach (var fv in factvariables)
            {
                var factvariable = fv.Item as FactVariable;

                var parameterfactbasequery = GetRuleBaseQuery(fv).FirstOrDefault();
                var parameterfactqueries = GetFactQuery(fv, 1);

                var parameterfacts = parameterfactbasequery == null ? rulefactIds : parameterfactbasequery.ToIntervalList(Taxonomy, RuleHasDictFilter ? rulefactIds : null);

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
                var rulefactdictcount = rulefactqueries.Select(i => i.DictFilterIndexes.Count).ToList();

                if (parameter.BindAsSequence)
                {
                    mergedqueries = SoftCombineQueries(rulefactqueries, new List<LogicalModel.Base.FactBaseQuery>() { parameterfactquery });
                    //mergedqueries = new List<LogicalModel.Base.FactBaseQuery>() { parameterfactquery };

                }
                else
                {
                    if (parameterfactqueries.Count > 1)
                    {
                        //Utilities.Logger.WriteLine(String.Format("Rule {0} parameter {1} has multiple queries, but it is not sequenced.", logicalrule.ID, parameter.Name));
                    }
                    mergedqueries = SoftCombineQueries(rulefactqueries, parameterfactqueries);
                    //mergedqueries = parameterfactqueries;
                }
                if (mergedqueries.Count == 0)
                {
                    mergedqueries.Add(new LogicalModel.Base.FactBaseQuery());
                }
                //TODO
                var multiplefactsfornonseqparameter = 0;
                var qix = 0;
                parameter.TaxFacts.Capacity = mergedqueries.Count;

                var isnonsequenced = !parameter.BindAsSequence;

                foreach (var fbq in mergedqueries)
                {
                    if (fbq.HasDictFilter("find:filingIndicator"))
                    {
                        parameter.IsGeneral = true;
                        parameter.StringValue = "filingindicators";
                    }
                    //var datafactids = new List<int>();
                    //var facts = GetFacts(fbq, parameterfacts, parameterfactdict, datafactids);

                    var facts = fbq.ToList(Taxonomy, parameterfacts, true);

                    var ok = true;

                    if (isnonsequenced && facts.Count > 1)
                    {
                        multiplefactsfornonseqparameter++;
                        ok = false;
                        //like concepts for exampl
                        if (parameterfactqueries.Count == 0)
                        {
                            multiplefactsfornonseqparameter = 0;
                            foreach (var fact in facts)
                            {
                                parameter.TaxFacts.Add(new List<int>() { fact });
                            }
                        }
                        else
                        {
                            multiplefactsfornonseqparameter = 0;
                            foreach (var fact in facts)
                            {
                                parameter.TaxFacts.Add(new List<int>() { fact });
                            }
                        }

                    }
                    if (ok)
                    {

                        parameter.TaxFacts.Add(facts.Select(i => i).ToList());

                    }


                    qix++;
                }


                if (multiplefactsfornonseqparameter > 0)
                {
                    Utilities.Logger.WriteLine(String.Format("Rule {0} non-sequenced parameter {1} has multiple facts", logicalrule.ID, parameter.Name));

                }
                if (parameter.TaxFacts.Count == 0 && !parameter.IsGeneral)
                {
                    Utilities.Logger.WriteLine(String.Format("Rule {0} parameter {1} has no facts", logicalrule.ID, parameter.Name));

                }

                var type = LogicalModel.TypeEnum.Numeric;
                var firsttaxfact = parameter.TaxFacts.FirstOrDefault(i => i.Count > 0);
                if (!parameter.IsGeneral)
                {
                    if (firsttaxfact == null)
                    {
                        Utilities.Logger.WriteLine(String.Format("Rule {0} parameter {1} has no valid facts", logicalrule.ID, parameter.Name));
                    }
                    else
                    {
                        var firstfactid = firsttaxfact.FirstOrDefault();
                        var firstfactstring = Taxonomy.GetFactStringKey(Taxonomy.FactsManager.GetFactKey(firstfactid));
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
                }
                else
                {
                    parameter.Type = LogicalModel.TypeEnum.String;
                }
                var sequence = parameter.BindAsSequence ? "Sequence" : "";
                sb.AppendLine("parameter: " + name + " " + sequence);

                if (LogicalModel.Settings.Current.CheckValidationCells)
                {
                    sb.AppendLine(CheckCells(parameter));
                }
                logicalrule.Parameters.Add(parameter);

            }

            var groupcounts = logicalrule.Parameters.Select(i => i.TaxFacts.Count).Distinct().ToList();
            if (groupcounts.Count > 1)
            {
                Utilities.Logger.WriteLine(String.Format("Rule {0} has fact group issues!", logicalrule.ID));
            }
            /*
            //remove invalid facts
            var firstparameter = logicalrule.Parameters.FirstOrDefault();
            //var xx = Taxonomy.GetFactStringKeys(firstparameter.TaxFacts);

            var taxfactstoremove = new List<int>();

            for (int i = 0; i < firstparameter.TaxFacts.Count; i++)
            {
                var factlist = new List<List<int>>();
                factlist.Add(firstparameter.TaxFacts[i]);
                for (int j = 1; j < logicalrule.Parameters.Count; j++)
                {
                    var parameter = logicalrule.Parameters[j];
                    if (parameter.TaxFacts.Count != firstparameter.TaxFacts.Count) 
                    {

                    }
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

            */
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


    }
}
