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
        public static void CheckConsistency(Taxonomy taxonomy, ValidationRule rule)
        {
            var sb = new StringBuilder();
            var inconsistent = false;
            sb.AppendLine(rule.RawInfo);
            foreach (var p in rule.Parameters) 
            {
                var tfcs = "";
                var tfc = p.TaxFacts.Count;
                var tfall = p.TaxFacts.SelectMany(i => i).Count();
                if (tfall > tfc && !p.BindAsSequence)
                {

                }
                tfcs=String.Format("{0}: {2}/{1}", p.Name, tfc, tfall);
                sb.AppendLine(p.ToString()+" TFCS: " + tfcs);
                foreach (var factgroup in p.TaxFacts) 
                {
                    if (!p.BindAsSequence && factgroup.Count > 1) 
                    {
                        inconsistent = true;
                        sb.AppendLine(taxonomy.GetFactStringKeys(p.TaxFacts));

                        break;
                    }
                }
  
            }
            sb.AppendLine("___________");
            if (rule.ID == "eba_v0204_m") 
            {

            }
            if (inconsistent) 
            {
                Logger.WriteLine(String.Format("Can't resolve rule {0}", rule.ID));
                var validationfolder = TaxonomyEngine.LogFolder+"Validation\\";
                var logfilename = Utilities.Strings.GetFileName(Logger.LogPath);
                var outputpath = validationfolder + "Issues_" + logfilename;
                Utilities.FS.AppendAllText(outputpath, sb.ToString());
            }

        }
        public static void ExecuteExplicitFiltering(Taxonomy taxonomy, ValidationRule rule)
        {
            if (rule.ID.Contains("eba_v0336_m"))
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
                        IntervalList facts = (IntervalList)Utilities.Objects.IntersectSorted(parameter.Data, group, null);
                        //var facts = factsq.ToList();
                        if (facts.Count > 0)
                        {
                            hasfacts = true;
                        }
                        else 
                        {

                        }
                        parameter.TaxFacts.Add(facts.ToList());
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
        public static void ExecuteMatching(Taxonomy taxonomy, ValidationRule rule)
        {
            var factparameters = rule.Parameters.Where(i => !i.IsGeneral).ToList();
            var paramtaxfactdict = new Dictionary<ValidationParameter, List<List<int>>>();
            var p1 = factparameters.FirstOrDefault();
            if ((Object)p1 == null) 
            {
                return;
            }
            var otherfactparameters = factparameters.Except(new List<ValidationParameter>() { p1 }).ToList();
            var canmatch = true;  
            foreach (var op in otherfactparameters) 
            {
                var c = op.TaxFacts.SelectMany(i => i).Count();
                if (c != 1) 
                {
                    canmatch = false;
                }
            }
            foreach (var p in factparameters) 
            {
                paramtaxfactdict.Add(p, new List<List<int>>());
            }
            if (!p1.BindAsSequence)
            {
                var groupcount = p1.TaxFacts.Count;
                var totalcount = p1.TaxFacts.SelectMany(i => i).Count();
                if (totalcount > groupcount && canmatch)
                {

                    foreach (var group in p1.TaxFacts) 
                    {
                        foreach (var fact in group) 
                        {
                            paramtaxfactdict[p1].Add(new List<int>() { fact });
                            foreach (var p in otherfactparameters) 
                            {
                                var facts = p.TaxFacts[0];
                                paramtaxfactdict[p].Add(facts);
                            }
                        }
                    }
                    foreach (var item in paramtaxfactdict) 
                    {
                        item.Key.TaxFacts = item.Value;
                    }
                   
                }
               
            }
        }
        public static void ExecuteImplicitFiltering(Taxonomy taxonomy, ValidationRule rule)
        {
            var dict = new Dictionary<int, int[]>();
            var uncoveredmainaspects = GetUncoveredDomains(taxonomy, dict, rule);
            GroupTaxFacts(taxonomy, dict, rule, uncoveredmainaspects);
        }
        public static List<ValidationRuleResult> ExecuteImplicitFiltering(Taxonomy taxonomy, List<ValidationRuleResult> ruleresults)
        {
            var results = new List<ValidationRuleResult>();
            
            foreach (var result in ruleresults)
            {
                var dict = new Dictionary<int, int[]>();
                var uncoveredmainaspects = GetUncoveredDomains(taxonomy, dict,result);
                var expanded = ExpandResult(taxonomy, dict, result, uncoveredmainaspects);
                results.AddRange(expanded);
            }
            return results;
        }

        public static List<ValidationRuleResult> ExecuteImplicitFiltering(Instance instance, List<ValidationRuleResult> ruleresults)
        {
            var taxonomy = instance.Taxonomy;
            var results = new List<ValidationRuleResult>();
            foreach (var result in ruleresults)
            {
                var uncoveredmainaspects = GetUncoveredDomains(instance, result);
                var typedomains = instance.TypedFactMembers.Keys.ToList();
                var uncoveredaspects = typedomains.Intersect(typedomains).ToList();
                if (uncoveredaspects.Count > 0)
                {
                    var expanded = ExpandResult(instance, result, uncoveredaspects);
                    results.AddRange(expanded);
                }
                else 
                {
                    results = ruleresults;
                }
            }
            return results;
        }
        public static List<int> GetUncoveredDomains(Instance instance, ValidationRuleResult ruleresult) 
        {
            var result = new List<int>();
            var taxonomy = instance.Taxonomy;
            var rule = ruleresult.Rule;
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

        public static List<int> GetUncoveredDomains(Taxonomy taxonomy,Dictionary<int, int[]> factkeydict, ValidationRuleResult ruleresult)
        {
            var result = new List<int>();
            var rule = taxonomy.ValidationRules.FirstOrDefault(i => i.ID == ruleresult.ID);
            var ruleparameterdictionary = rule.Parameters.ToDictionary(i => i.Name);
            var ruleresultparameterdictionary = ruleresult.Parameters.ToDictionary(i => i.Name);
            var factparameterids = ruleparameterdictionary.Where(i => !i.Value.IsGeneral).Select(i => i.Key).ToList();
            var mainaspectsofparameters = new List<List<int>>();

            var factindexes = rule.Parameters.SelectMany(i => i.TaxFacts).SelectMany(i => i).OrderBy(i => i).ToList();
            foreach (var factindex in factindexes)
            {
                var key = taxonomy.FactsManager.GetFactKey(factindex);
                if (!factkeydict.ContainsKey(factindex))
                {
                    factkeydict.Add(factindex, null);
                }
                factkeydict[factindex] = key;
            }
            

            foreach (var parmaeterid in factparameterids)
            {
                var rulep = ruleparameterdictionary[parmaeterid];
                var p = ruleresultparameterdictionary[parmaeterid];
                var mainaspects = new HashSet<int>();
                foreach (var fact in p.FactIDs)
                {
                    var factid = Utilities.Converters.FastParse(fact.Substring(2));
                    var taxonomykey = factkeydict[factid];
                    foreach (var part in taxonomykey)
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

        public static List<int> GetUncoveredDomains(Taxonomy taxonomy, Dictionary<int, int[]> factkeydict, ValidationRule rule)
        {
            var result = new List<int>();
            var ruleparameterdictionary = rule.Parameters.ToDictionary(i => i.Name);
            var factparameterids = ruleparameterdictionary.Where(i => !i.Value.IsGeneral).Select(i => i.Key).ToList();
            var mainaspectsofparameters = new List<List<int>>();

            var factindexes = rule.Parameters.SelectMany(i => i.TaxFacts).SelectMany(i => i).OrderBy(i => i).ToList();
            foreach (var factindex in factindexes)
            {
                var key = taxonomy.FactsManager.GetFactKey(factindex);
                if (!factkeydict.ContainsKey(factindex))
                {
                    factkeydict.Add(factindex, null);
                }
                factkeydict[factindex] = key;
            }


            foreach (var p in rule.Parameters)
            {
                var rulep = ruleparameterdictionary[p.Name];
                var mainaspects = new HashSet<int>();
                foreach (var factgroup in p.TaxFacts)
                {
                    foreach (var factid in factgroup.AsEnumerable())
                    {
                        var taxonomykey = factkeydict[factid];
                        foreach (var part in taxonomykey)
                        {
                            var mainaspectid = taxonomy.DimensionDomainsOfMembers.ContainsKey(part) ? taxonomy.DimensionDomainsOfMembers[part] : part;

                            if (!mainaspects.Contains(mainaspectid))
                            {
                                mainaspects.Add(mainaspectid);
                            }
                        }
                    }
                }

                var aspectsofparameter = mainaspects.Except(rulep.CoveredParts).ToList();
                aspectsofparameter = aspectsofparameter.Except(rule.CoveredParts).ToList();
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
            var rule = ruleresult.Rule;
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
                    //SetCells(instance, projectedresult);
                }
            }
            else
            {
                result.Add(ruleresult);
            }
            return result;
        }

        public static List<ValidationRuleResult> ExpandResult(Taxonomy taxonomy, Dictionary<int, int[]> factkeydict, ValidationRuleResult ruleresult, List<int> groupaspects)
        {
            var result = new List<ValidationRuleResult>();
            var rule = taxonomy.ValidationRules.FirstOrDefault(i => i.ID == ruleresult.ID);
            var ruleparameterdictionary = rule.Parameters.ToDictionary(i => i.Name);
            var ruleresultparameterdictionary = ruleresult.Parameters.ToDictionary(i => i.Name);
            var factparameterids = ruleparameterdictionary.Where(i => !i.Value.IsGeneral).Select(i => i.Key).ToList();

            //get the projection
            var projections = new Dictionary<int[], Dictionary<string, List<int[]>>>(new IntArrayEqualityComparer());
            foreach (var parmaeterid in factparameterids)
            {
                var rulep = ruleparameterdictionary[parmaeterid];
                var p = ruleresultparameterdictionary[parmaeterid];

                foreach (var fact in p.FactIDs)
                {
                    var factid = Utilities.Converters.FastParse(fact.Substring(2));
                    var taxonomykey = factkeydict[factid];
                    var key = GetPartsOfMainParts(taxonomy, taxonomykey, groupaspects);
                    var keyarray = key.ToArray();
                    if (!projections.ContainsKey(keyarray))
                    {
                        projections.Add(keyarray, new Dictionary<string, List<int[]>>());
                    }
                    var paramdict = projections[keyarray];
                    if (!paramdict.ContainsKey(parmaeterid))
                    {
                        paramdict.Add(parmaeterid, new List<int[]>());
                    }
                    paramdict[parmaeterid].Add(taxonomykey);
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
                            p.FactIDs = facts.Select(i => String.Format("T:{0}", taxonomy.FactsManager.GetFactIndex(i))).ToList();
                            p.InstanceFacts.Clear();
                            //p.InstanceFacts = facts.ToList();
                        }
                        else
                        {
                            p.FactIDs.Clear();
                            p.InstanceFacts.Clear();
                            p.Value = ruleparameterdictionary[p.Name].FallBackValue;
                        }
                    }
                    //SetCells(instance, projectedresult);
                }
            }
            else
            {
                result.Add(ruleresult);
            }
            return result;
        }

        public static void GroupTaxFacts(Taxonomy taxonomy, Dictionary<int, int[]> factkeydict, ValidationRule rule, List<int> groupaspects)
        {
            if (groupaspects.Count == 0) 
            {
                return;
            }
            var p1 = rule.Parameters.FirstOrDefault(i => !i.IsGeneral && !i.BindAsSequence);
            var factparameters = rule.Parameters.Where(i => !i.IsGeneral).ToList();
            var sb = new StringBuilder();
            foreach (var p in factparameters) 
            {
                var tfc = p.TaxFacts.Count;
                var tfall = p.TaxFacts.SelectMany(i=>i).Count();
                if (tfall > tfc && !p.BindAsSequence) 
                {

                }
                sb.AppendLine(String.Format("{0}: {2}/{1}", p.Name, tfc, tfall));
            }
            var groups = new List<Dictionary<string,List<int>>>();
            var paramtaxfactdict = new Dictionary<string, List<List<int>>>();
            if ((Object)p1 != null) 
            {
                foreach (var fp in factparameters)
                {
                    paramtaxfactdict.Add(fp.Name, new List<List<int>>());
                }

                var otherfactparameters = rule.Parameters.Where(i => !i.IsGeneral && !i.Equals(p1)).ToList();
                var groupix = 0;
                foreach (var factgroup in p1.TaxFacts)
                {
                    var groupdictionary = new Dictionary<int[], Dictionary<string, List<int>>>(new IntArrayEqualityComparer());

                    //if (factgroup.Count > 1)
                    //{
                    foreach (var factid in factgroup.AsEnumerable())
                    {
                        var groupkey = AddFactToGroup(factid, p1.Name, taxonomy, groupaspects, groupdictionary, factkeydict);

                        foreach (var p in otherfactparameters)
                        {
                            var factids = p.TaxFacts[groupix];
                            //if (factids.Count > 1) 
                            //{

                            //}
                            foreach (var p_factid in factids) 
                            {
                                //AddFactToGroup(p_factid, p.Name, groupkey, groupdictionary, factkeydict);
                                AddFactToGroup(p_factid, p.Name, taxonomy, groupaspects, groupdictionary, factkeydict);
                            }
                            if (factids.Count == 0) 
                            {
                                AddEmptyFactToGroup(p.Name, groupkey, groupdictionary, factkeydict);

                            }
                            factids.Clear();
                        }
                    }
                    foreach (var group in groupdictionary) 
                    {
                        foreach (var item in group.Value) 
                        {
                            var il = new List<int>();
                            paramtaxfactdict[item.Key].Add(il);
                            foreach (var fact in item.Value) 
                            {
                                il.Add(fact);

                            }
                        }
                    }
                    //}
                    groupix++;
                }

                foreach (var fp in factparameters)
                {
                    var taxfacts = paramtaxfactdict[fp.Name];
                    fp.TaxFacts = taxfacts;
                }
              
            }

        }

        public static int[] AddFactToGroup(int factid, string parametername,Taxonomy taxonomy,List<int> groupaspects, Dictionary<int[], Dictionary<string, List<int>>> groupdictionary, Dictionary<int, int[]> factkeydict) 
        {
            var taxonomykey = factkeydict[factid];
            var groupkey = GetPartsOfMainParts(taxonomy, taxonomykey, groupaspects).ToArray();
            if (!groupdictionary.ContainsKey(groupkey))
            {
                groupdictionary.Add(groupkey, new Dictionary<string, List<int>>());
            }
            var group = groupdictionary[groupkey];
            if (!group.ContainsKey(parametername))
            {
                group.Add(parametername, new List<int>());
            }
            group[parametername].Add(factid);
            return groupkey;

        }
        public static void AddFactToGroup(int factid, string parametername, int[] groupkey, Dictionary<int[], Dictionary<string, List<int>>> groupdictionary, Dictionary<int, int[]> factkeydict)
        {
            if (!groupdictionary.ContainsKey(groupkey))
            {
                groupdictionary.Add(groupkey, new Dictionary<string, List<int>>());
            }
            var group = groupdictionary[groupkey];
            if (!group.ContainsKey(parametername))
            {
                group.Add(parametername, new List<int>());
            }
            group[parametername].Add(factid);

        }

        public static void AddEmptyFactToGroup(string parametername, int[] groupkey, Dictionary<int[], Dictionary<string, List<int>>> groupdictionary, Dictionary<int, int[]> factkeydict)
        {
            if (!groupdictionary.ContainsKey(groupkey))
            {
                groupdictionary.Add(groupkey, new Dictionary<string, List<int>>());
            }
            var group = groupdictionary[groupkey];
            if (!group.ContainsKey(parametername))
            {
                group.Add(parametername, new List<int>());
            }
            //group[parametername].Add(factid);

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

        public static List<int> GetPartsOfMainParts(Taxonomy taxonomy, int[] factparts, List<int> mainparts)
        {
            var parts = new List<int>();
            for (int i = 0; i < factparts.Length; i++)
            {
                var aspectid = factparts[i];
                var mainaspectid = taxonomy.DimensionDomainsOfMembers.ContainsKey(aspectid) ? taxonomy.DimensionDomainsOfMembers[aspectid] : aspectid;
                if (mainparts.Contains(mainaspectid))
                {
                    parts.Add(factparts[i]);
                }

            }
            return parts;
        }
    }
}
