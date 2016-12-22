using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace LogicalModel.Validation
{
    public partial class ValidationRuleHelper
    {
        public static void ExecuteExplicitFiltering(Taxonomy taxonomy, ValidationRule rule)
        {
            if (rule.ID.Contains("de_sprv_vcrs_0030"))
            {

            }
            var tables = rule.Tables.Select(i => taxonomy.Tables.FirstOrDefault(t => t.ID == i)).ToList();
            IList<int> tableintevallist = null;
            foreach (var table in tables)
            {
                tableintevallist = tableintevallist == null ? new IntervalList() : tableintevallist;
                tableintevallist = Utilities.Objects.MergeSorted(tableintevallist, table.FactindexList, null);
            }
            var hastableinfo = tableintevallist != null ? tableintevallist.Count > 0 : false;
            IList<int> allfactsintevallist = new IntervalList(0, taxonomy.FactsManager.FactsOfPages.Count);

            if (hastableinfo)
            {
                allfactsintevallist = tableintevallist;
            }
            var ruletypeddimension = rule.BaseQuery.DictFilterIndexes.Where(i => taxonomy.IsTyped(i));

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
                    parameter.Data = parameter.BaseQuery.ToIntervalList(taxonomy.FactsOfParts, sdata);
                    parameter.TypedDimensions = parameter.BaseQuery.DictFilterIndexes.Where(i => taxonomy.IsTyped(i)).ToList();
                    parameter.TypedDimensions = parameter.TypedDimensions.Concat(ruletypeddimension).Distinct().ToList();
                    parameter.CoveredParts = parameter.BaseQuery.GetAspects(taxonomy);
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
                data = allfactsintevallist;
            }
            rule.TypedDimensions = rule.BaseQuery.DictFilterIndexes.Where(i => taxonomy.IsTyped(i)).ToList();
            rule.CoveredParts = rule.BaseQuery.GetAspects(taxonomy);

            var singlefactparameters = rule.Parameters.Where(i => !i.IsGeneral && !i.BindAsSequence).ToList();
            var multifactparameters = rule.Parameters.Where(i => !i.IsGeneral && i.BindAsSequence).ToList();
            //Utilities.Logger.WriteToFile(String.Format("EnumerateIntervals {0} on {1}", rule.BaseQuery, data));

            foreach (var group in rule.BaseQuery.EnumerateIntervals(taxonomy.FactsOfParts, 0, data, false))
            {
                //Utilities.Logger.WriteToFile("Joining rule with parameters...");
                foreach (var parameter in rule.Parameters)
                {
                    if (!parameter.IsGeneral)
                    {
                        var factsq = Utilities.Objects.IntersectSorted(parameter.Data, group, null);
                        var facts = factsq.ToList();
                        if (facts.Count > 0)
                        {
                            hasfacts = true;
                        }
                        parameter.TaxFacts.Add(facts);
                    }
                }
        
                ix++;
                //Utilities.Logger.WriteToFile("End joining rule with parameters");
            }
            ValidationRuleHelper.SetParamerterTypes(taxonomy, rule);
            if (!hasfacts)
            {
                Utilities.Logger.WriteLine(String.Format("{0}: Rule has no facts!", rule.ID));
            }
       
        }

        public static List<ValidationRuleResult> ExecuteImplicitFiltering(Instance instance, List<ValidationRuleResult> ruleresults)
        {
            var taxonomy = instance.Taxonomy;
            var results = new List<ValidationRuleResult>();
            foreach (var result in ruleresults)
            {
                var uncoveredmainaspects = GetUncoveredDomains(instance, result);
                var expanded = ExpandResult(instance, result, uncoveredmainaspects);
                results.AddRange(expanded);
            }
            return results;
        }
        public static List<int> GetUncoveredDomains(Instance instance, ValidationRuleResult ruleresult) 
        {
            var result = new List<int>();
            var taxonomy = instance.Taxonomy;
            var rule = taxonomy.ValidationRules.FirstOrDefault(i => i.ID == ruleresult.ID);
            var ruleparameterdictionary = rule.Parameters.ToDictionary(i => i.Name);
            var ruleresultparameterdictionary = ruleresult.Parameters.ToDictionary(i => i.Name);
            var factparameterids = ruleparameterdictionary.Where(i=>!i.Value.IsGeneral).Select(i=>i.Key).ToList();
            var mainaspectsofparameters = new List<List<int>>();
            foreach (var parmaeterid in factparameterids) 
            {
                var rulep = ruleparameterdictionary[parmaeterid];
                var p = ruleresultparameterdictionary[parmaeterid];
                var mainaspects = new HashSet<int>();
                foreach (var fact in p.InstanceFacts) 
                {
                    foreach (var part in fact.TaxonomyKey)
                    {
                        var mainaspectid = taxonomy.DimensionDomainsOfMembers.ContainsKey(part) ? taxonomy.DimensionDomainsOfMembers[part] : part;

                        if (!mainaspects.Contains(mainaspectid)) 
                        {
                            mainaspects.Add(mainaspectid);
                        }
                    }
                }

                var aspectsofparameter = mainaspects.Except(rulep.CoveredParts).ToList();
                //aspectsofparameter = aspectsofparameter.Except(rule.CoveredParts).ToList();
                mainaspectsofparameters.Add(aspectsofparameter);

            }
            List<int> commonmainaspects = null;
            foreach (var parameteraspects in mainaspectsofparameters) 
            {
                if (commonmainaspects == null)
                {
                    commonmainaspects = parameteraspects.ToList();
                }
                else 
                {
                    commonmainaspects = commonmainaspects.Intersect(parameteraspects).ToList();
                }
            }

            result = commonmainaspects;

            return result;
        }

        public static List<ValidationRuleResult> ExpandResult(Instance instance, ValidationRuleResult ruleresult, List<int> groupaspects)
        {
            var result = new List<ValidationRuleResult>();
            var taxonomy = instance.Taxonomy;
            var rule = taxonomy.ValidationRules.FirstOrDefault(i => i.ID == ruleresult.ID);
            var ruleparameterdictionary = rule.Parameters.ToDictionary(i => i.Name);
            var ruleresultparameterdictionary = ruleresult.Parameters.ToDictionary(i => i.Name);
            var factparameterids = ruleparameterdictionary.Where(i => !i.Value.IsGeneral).Select(i => i.Key).ToList();
            
            //get the projection
            var projections = new Dictionary<int[],Dictionary<string,List<InstanceFact>>>(new IntArrayEqualityComparer());
            foreach (var parmaeterid in factparameterids)
            {
                var rulep = ruleparameterdictionary[parmaeterid];
                var p = ruleresultparameterdictionary[parmaeterid];

                foreach (var fact in p.InstanceFacts) 
                {
                    var key = GetPartsOfMainParts(instance, fact, groupaspects);
                    var keyarray = key.ToArray();
                    if (!projections.ContainsKey(keyarray)) 
                    {
                        projections.Add(keyarray,new Dictionary<string, List<InstanceFact>>());
                    }
                    var paramdict = projections[keyarray];
                    if (!paramdict.ContainsKey(parmaeterid)) 
                    {
                        paramdict.Add(parmaeterid, new List<InstanceFact>());
                    }
                    paramdict[parmaeterid].Add(fact);
                }
            }

            //doing the implicit filtering
            var ic = new IntArrayComparer();
            if (projections.Count > 0)
            {
                foreach (var projectionkey in projections)
                {
                    var projectedresult = ruleresult.Copy();
                    result.Add(projectedresult);

                    foreach (var p in projectedresult.Parameters)
                    {
                        //var pfacts = ruleresult.Parameters.FirstOrDefault(i => i.Name == p.Name).InstanceFacts;
                        //var facts = pfacts.Where(i => ic.Contains(i.InstanceKey, projectionkey));
                        var paramdict = projections[projectionkey.Key];
                        if (paramdict.ContainsKey(p.Name))
                        {
                            var facts = paramdict[p.Name];
                            p.FactIDs = facts.Select(i => String.Format("I:{0}", i.IX)).ToList();
                            p.InstanceFacts = facts.ToList();
                        }
                        else 
                        {
                            p.FactIDs.Clear();
                            p.InstanceFacts.Clear();
                            p.Value = ruleparameterdictionary[p.Name].FallBackValue;
                        }
                    }
                    SetCells(instance, projectedresult);
                }
            }
            else
            {
                result.Add(ruleresult);
            }
            return result;
        }

        public static List<int> GetPartsOfMainParts(Instance instance, InstanceFact fact, List<int> mainparts)
        {
            var taxonomy = instance.Taxonomy;
            var parts = new List<int>();
            for (int i = 0; i < fact.TaxonomyKey.Length; i++)
            {
                var aspectid = fact.TaxonomyKey[i];
                var mainaspectid = taxonomy.DimensionDomainsOfMembers.ContainsKey(aspectid) ? taxonomy.DimensionDomainsOfMembers[aspectid] : aspectid;
                if (mainparts.Contains(mainaspectid)) 
                {
                    parts.Add(fact.InstanceKey[i]);
                }

            }
            return parts;
        }
    }
}
