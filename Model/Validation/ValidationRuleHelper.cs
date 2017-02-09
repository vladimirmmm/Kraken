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
        public static void LoadTypedFacts(Instance instance, List<ValidationRuleResult> ruleresults)
        {
            foreach (var ruleresult in ruleresults) 
            {
                LoadTypedFacts(instance, ruleresult);
            }
        }
        public static void LoadTypedFacts(Instance instance, ValidationRuleResult ruleresult)
        {
            var taxonomy = instance.Taxonomy;
            var rule = taxonomy.ValidationRules.FirstOrDefault(i => i.ID == ruleresult.ID);
            var rulefactparameters = rule.Parameters.Where(i => !i.IsGeneral).ToList();
            foreach (var rulefactparameter in rulefactparameters)
            {
                var p = ruleresult.Parameters.FirstOrDefault(i => i.Name == rulefactparameter.Name);
                
                p.InstanceFacts = p.FactIDs.SelectMany(i => instance.GetFactsByIdString(i)).ToList();
                var newfactids = new List<string>();
                foreach (var factid in p.FactIDs) 
                {
                    var facts = instance.GetFactsByIdString(factid);
                    if (facts.Count> 0)
                    {
                        foreach (var fact in facts) 
                        {
                            newfactids.Add(String.Format("I:{0}", fact.IX));

                        }
                    }
                    else 
                    {
                        newfactids.Add(factid);
                    }
                }
                p.FactIDs = newfactids;
                //p.InstanceFacts = p.FactIDs.SelectMany(i => instance.GetFactsByIdString(i)).ToList();
                //p.FactIDs = p.InstanceFacts.Select(i => String.Format("I:{0}", i.IX)).ToList();
            }
            
        }
        /*
        public static List<ValidationRuleResult> ResolveTypedFacts(Instance instance, ValidationRuleResult ruleresult)
        {
            var results = new List<ValidationRuleResult>();
            var taxonomy = instance.Taxonomy;
            var rule = taxonomy.ValidationRules.FirstOrDefault(i => i.ID == ruleresult.ID);
            if (rule.ID.Contains("de_sprv_vrsk_1330"))
            { 

            }
            ruleresult.Rule = rule;
            var factparameters = rule.Parameters.Where(i => !i.IsGeneral ).ToList();
            var singlefactparameters = factparameters.Where(i => !i.BindAsSequence).ToList();
            var multifactparameters = factparameters.Where(i => i.BindAsSequence).ToList();
            var firstsinglefactparameter = singlefactparameters.FirstOrDefault();

            //get typed keys

            var ids = ruleresult.Parameters.Select(i => i.Name).ToList();

            var resultfactparameters = ruleresult.Parameters.Where(i=>ids.Contains(i.Name)).ToList();

            foreach (var p in resultfactparameters)
            {
                p.InstanceFacts = p.FactIDs.SelectMany(i => instance.GetFactsByIdString(i)).ToList();
                p.FactIDs = p.InstanceFacts.Select(i => String.Format("I:{0}", i.IX)).ToList();
            }
            
            

            if (results.Count == 0)
            {
                results.Add(ruleresult);
            }
            foreach (var result in results)
            {
                SetCells(instance, result);
            }
            var issues = GetIssues(results);
            if (issues.Count > 0) 
            {
                var sb = new StringBuilder();
                sb.AppendLine(String.Format("Can't resolve rule: {0}",rule.ID));
                foreach(var issue in issues){
                   sb.AppendLine(String.Format("Issue: {0}",issue.ToString()));
                }
                Utilities.Logger.WriteLine(sb.ToString());
            }
            return results;
            //var firstfactid = p.FactIDs.FirstOrDefault();
            //var pkey = instance.GetFactKeyByIdString(firstfactid).ToList().ToArray();
        }
        */
        /*
        public static List<ValidationRuleResult> ExecuteImplicitFilter(Instance instance, ValidationRuleResult ruleresult) 
        {
            var result = new List<ValidationRuleResult>();
            var taxonomy = instance.Taxonomy;
            var rule = taxonomy.ValidationRules.FirstOrDefault(i => i.ID == ruleresult.ID);

            var factparameters = rule.Parameters.Where(i => !i.IsGeneral).ToList();
            List<int> uncoveredresultparts = null;
            var resultfacts = new List<InstanceFact>();

            //getting the uncovered aspects of the facts of the current Validation Result
            foreach (var ruleparameter in factparameters) 
            {
                var p = ruleresult.Parameters.FirstOrDefault(i => i.Name == ruleparameter.Name);
                List<int> uncoveredparameteroparts = null;

                foreach(var fact in p.InstanceFacts)
                {
                    resultfacts.Add(fact);
                    var domainkeys = fact.TaxonomyKey.Select(i =>taxonomy.DimensionDomainsOfMembers.ContainsKey(i)? taxonomy.DimensionDomainsOfMembers[i]:i);

                    var uncovered = domainkeys.Except(ruleparameter.CoveredParts).ToList();
                    if (uncoveredparameteroparts == null)
                    {
                        uncoveredparameteroparts = uncovered;
                    }
                    else 
                    {
                        uncoveredparameteroparts = uncoveredparameteroparts.Intersect(uncovered).ToList();
                    }

                }
                if (uncoveredresultparts == null)
                {
                    uncoveredresultparts = uncoveredparameteroparts;
                }
                else 
                {
                    uncoveredresultparts = uncoveredresultparts.Intersect(uncoveredparameteroparts).ToList();
                }
     
            }

            //Projecting the uncovered aspect values from the instance
            var projections = new HashSet<int[]>(new IntArrayEqualityComparer());
            foreach (var fact in resultfacts) 
            {
                var keys = new List<int>();
                for (int u = 0; u < uncoveredresultparts.Count; u++)
                {
                    for (int i = 0; i < fact.TaxonomyKey.Length; i++)
                    {
                        var dompartkey = taxonomy.DimensionDomainsOfMembers.ContainsKey(i)?taxonomy.DimensionDomainsOfMembers[i]:i;
                        if (dompartkey == uncoveredresultparts[u]) 
                        {
                            keys.Add(fact.InstanceKey[i]);
                            break;
                        }
                    }
                  
                }
                var key = keys.ToArray();
                if (!projections.Contains(key)) 
                {
                    projections.Add(key);
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
                        var pfacts = ruleresult.Parameters.FirstOrDefault(i => i.Name == p.Name).InstanceFacts;
                        var facts = pfacts.Where(i => ic.Contains(i.InstanceKey, projectionkey));
                        p.FactIDs = facts.Select(i => String.Format("I:{0}", i.IX)).ToList();
                        p.InstanceFacts = facts.ToList();
                    }
                    SetCells(instance, projectedresult);
                }
            }
            else
            {
                result.Add(ruleresult);
            }

            //var coveredaspects = p.CoveredParts.Select(i => taxonomy.DimensionDomainsOfMembers[i]).Distinct().ToList();
            //if (commonuncoveredaspects == null)
            //{
            //    commonuncoveredaspects = new List<int>(coveredaspects);
            //}
            //else 
            //{
            //    commonuncoveredaspects = commonuncoveredaspects.Intersect(coveredaspects).ToList();
            //}
            return result;
        }
        */
        public static List<ValidationRuleIssue> GetIssues(List<ValidationRuleResult> results) 
        {
            var issues = new List<ValidationRuleIssue>();
            foreach (var result in results) 
            {
                var issue = GetIssue(result);
                if (issue != null) 
                {
                    issues.Add(issue);
                }
            }
            return issues;
        }
        public static ValidationRuleIssue GetIssue(ValidationRuleResult result) 
        {
            foreach (var p in result.Parameters) 
            {
                var ruleparameter = result.Rule.Parameters.FirstOrDefault(i => i.Name == p.Name);
                if (!ruleparameter.BindAsSequence) 
                {
                    if (p.FactIDs.Count > 1) 
                    {
                        return new ValidationRuleIssue(ValidationRuleIssueType.NonSequencedMultipleFacts, result.ID, p.Name);
                    }
                }
            }
            return null ;
        }
        
        public static IntListEqualityComparer listcomparer = new IntListEqualityComparer();
        
        public static void SetResult(Instance instance,ValidationRuleResult result, SimpleValidationParameter referenceparameter, InstanceFact referencefact) 
        {
            var taxonomy = instance.Taxonomy;

            var factparameters = result.Rule.Parameters.Where(i => !i.IsGeneral && i.Name!=referenceparameter.Name).Select(i=>i.Name).ToList();

            var resultfactparameters = result.Parameters.Where(i => factparameters.Contains(i.Name));
            var referencetypeddomains = instance.GetTypedPartDomainIds(referencefact);
            foreach (var p in resultfactparameters) 
            {
                var facts = p.FactIDs.SelectMany(i=>{
                    return instance.GetFactsByIdString(i);
                    //var key = instance.GetFactKeyByIdString(i);
                    //var instancefacts = instance.GetFactsByTaxKey(key);
                    //return instancefacts;
                }).ToArray().ToList();

                p.FactIDs.Clear();

                foreach(var fact in facts)
                {
                    var typeddomains = instance.GetTypedPartDomainIds(fact);
                    var commontypeddomains = typeddomains.Intersect(referencetypeddomains).ToList();
                    var commontypemmembersoffact = instance.GetTypedPartIds(fact, commontypeddomains);
                    var commontypemmembersofreference = instance.GetTypedPartIds(referencefact, commontypeddomains);
                    if (ValidationRuleHelper.listcomparer.Equals(commontypemmembersoffact, commontypemmembersofreference)) 
                    {
                        p.FactIDs.Add(string.Format("I:{0}", fact.IX));
                    }
                }

            }
        }

        public static void SetParamerterTypes(Taxonomy taxonomy, ValidationRule rule) 
        {
       
            foreach (var p in rule.Parameters) 
            {
                if (String.IsNullOrEmpty(p.Concept))
                {
                    var firstfactgroup = p.TaxFacts.FirstOrDefault(i => i.Count > 0);
                    int firstfactindex = firstfactgroup != null ? firstfactgroup[0] : -2;
                    if (firstfactindex != -2)
                    {
                        var factkey = taxonomy.FactsManager.GetFactKey(firstfactindex);
                        if (factkey.Length > 0 && taxonomy.CounterFactParts.ContainsKey(factkey[0]))
                        {
                            p.Concept = taxonomy.CounterFactParts[factkey[0]];

                        }
                    }
                }
                if (p.Type == TypeEnum.Unknown)
                {
                    p.Type = GetType(taxonomy, p.Concept);
                 
                }
            }
        }
        
        public static TypeEnum GetType(Taxonomy taxonomy, string conceptstr) 
        {
            var Concept = new Concept();
            Concept.Content = conceptstr;
            var conceptschemaelement = taxonomy.Concepts.ContainsKey(Concept.Content) ? taxonomy.Concepts[Concept.Content] : null;

            if (conceptschemaelement != null)
            {
                var type = conceptschemaelement.ItemType;
                type = type.StartsWith("xbrli") ? type : "xbrli:" + type;
                #region xbrli
                /*
                                /*xbrli:
                                xbrli:decimalItemType
                                xbrli:floatItemType
                                xbrli:doubleItemType
                                xbrli:monetaryItemType
                                xbrli:sharesItemType
                                xbrli:pureItemType
                                xbrli:fractionItemType
                                xbrli:integerItemType
                                xbrli:nonPositiveIntegerItemType
                                xbrli:negativeIntegerItemType
                                xbrli:longItemType
                                xbrli:intItemType
                                xbrli:shortItemType
                                xbrli:byteItemType
                                xbrli:nonNegativeIntegerItemType
                                xbrli:unsignedLongItemType
                                xbrli:unsignedIntItemType
                                xbrli:unsignedShortItemType
                                xbrli:unsignedByteItemType
                                xbrli:positiveIntegerItemType
                                xbrli:stringItemType
                                xbrli:booleanItemType
                                xbrli:hexBinaryItemType
                                xbrli:base64BinaryItemType
                                xbrli:anyURIItemType
                                xbrli:QNameItemType
                                xbrli:durationItemType
                                xbrli:dateTimeItemType
                                xbrli:timeItemType
                                xbrli:dateItemType
                                xbrli:gYearMonthItemType
                                xbrli:gYearItemType
                                xbrli:gMonthDayItemType
                                xbrli:gDayItemType
                                xbrli:gMonthItemType
                                xbrli:normalizedStringItemType
                                xbrli:tokenItemType
                                xbrli:languageItemType
                                xbrli:NameItemType
                                xbrli:NCNameItemType
                                xbrli:contextEntityType
                                xbrli:contextPeriodType
                                xbrli:contextScenarioType
                                xbrli:measuresType
                                 */
                #endregion
                if (type.In(taxonomy.numericxbrltypes)) { return TypeEnum.Numeric; }
                if (type.In(taxonomy.datexbrltypes)) { return TypeEnum.Date; }
                if (type.In(taxonomy.stringxbrltypes)) { return TypeEnum.String; }
                if (type.In(taxonomy.booleanxbrltypes)) { return TypeEnum.Boolean; }
                if (type.In(taxonomy.enumerationxbrltypes)) { return TypeEnum.String; }
            }
            return TypeEnum.Unknown; 

        }
        public static void SetCells(Taxonomy taxonomy, List<ValidationRuleResult> results)
        {
            foreach (var result in results)
            {
                SetCells(taxonomy, result);
            }
        }
        public static void SetCells(Taxonomy taxonomy, ValidationRuleResult result)
        {
            foreach (var p in result.Parameters)
            {
                p.Cells.Clear();
                foreach (var factid in p.FactIDs)
                {
                    var taxfactid = Utilities.Converters.FastParse(factid.Substring(2));

                    var cells = taxonomy.GetCellsOfFact(taxfactid);
                    p.Cells.Add(cells);

                }
            }
        }
        public static void SetCells(Instance instance, List<ValidationRuleResult> results)
        {
            foreach (var result in results)
            {
                SetCells(instance, result);
            }
        }
        public static void SetCells(Instance instance, ValidationRuleResult result) 
        {
            var p_i=0;
            //result.Rule = instance.Taxonomy.ValidationRules result.ID
            foreach (var p in result.Parameters)
            {
                p.Cells.Clear();
                foreach (var factid in p.FactIDs) 
                {
                    var cells = instance.GetCells(factid);
                    p.Cells.Add(cells);
                    var instancefact = instance.GetFactByIDString(factid);
                    if (instancefact != null) 
                    {
                        p.Value = instancefact.Value + ",";
                    }

                }
                if (string.IsNullOrEmpty(p.Value)) 
                {
                    p.Value = result.Rule.Parameters[p_i].FallBackValue;
                }
                p.Value = p.Value.TrimEnd(',');

                /*
                var facts = p.InstanceFacts;
                if (facts.Count > 0)
                {
                    var newcelllist = new List<List<string>>();
                    var firstcelllist = p.Cells.FirstOrDefault();
                    var firstcell = firstcelllist.FirstOrDefault();
                    p.Cells.Clear();
                    p.Value="";
                    //p.FactIDs.Clear();
                    foreach (var fact in facts)
                    {
                        var cell = instance.GetDynamicCellID(firstcell, fact);
                        newcelllist.Add(new List<string>() { cell });
                        p.Value += fact.Value + ",";
                    }
                    p.Value = p.Value.TrimEnd(',');
                    //p.FactIDs.AddRange(facts.Select(i => String.Format("I:{0}", i.IX)));
                    p.Cells.AddRange(newcelllist);
                }
                */
                p_i++;
            }
        }
        /*
        public static List<ValidationRuleResult> GetTypedResults(Instance instance, ValidationRule rule, ValidationRuleResult ruleresult) 
        {
            if (rule.ID.Contains("de_sprv_vrdp-bi_3260")) 
            {
            }
            var taxonomy = instance.Taxonomy;
            var result = new List<ValidationRuleResult>();
            var singlefactparameters = rule.Parameters.Where(i => !i.IsGeneral && !i.BindAsSequence).ToList();
            var multifactparameters = rule.Parameters.Where(i => !i.IsGeneral && i.BindAsSequence).ToList();
            var typeddict = new Dictionary<int[], ValidationParameter>(new Utilities.IntArrayEqualityComparer());
            var keymap = new Dictionary<string, Dictionary<int,int>>();
            var typedinstdict = new Dictionary<int[], int[]>(new Utilities.IntArrayEqualityComparer());
            var allinstancefactkeys=new List<int[]>();

            //setting cells for multifact parameters
            foreach (var factparameter in multifactparameters)
            {
                var p = ruleresult.Parameters.FirstOrDefault(i => i.Name == factparameter.Name);
                var firstfactid = p.FactIDs.FirstOrDefault();
                var pkey = instance.GetFactKeyByIdString(firstfactid).ToList().ToArray();
                var facts = instance.GetFactsByTaxKey(pkey);
                var newcelllist = new List<List<string>>();
                var firstcelllist = p.Cells.FirstOrDefault();
                var firstcell = firstcelllist.FirstOrDefault();
                p.Cells.Clear();
                p.FactIDs.Clear();
                foreach (var fact in facts) 
                {
                    var cell = instance.GetDynamicCellID(firstcell, fact);
                    newcelllist.Add(new List<string>() { cell });
               
                }
                p.FactIDs.AddRange(facts.Select(i => String.Format("I:{0}", i.IX)));
                p.Cells.AddRange(newcelllist);
            }
            //setting up the typedictionaries
            foreach(var factparameter in singlefactparameters)
            {
                var resultparameter = ruleresult.Parameters.FirstOrDefault(i=>i.Name==factparameter.Name);
                if (resultparameter.FactIDs.Count > 0)
                {
                    var factkeystring = resultparameter.FactIDs.FirstOrDefault();
                    var factkey = instance.GetFactKeyByIdString(factkeystring);
                    var typekeylist = new List<int>();

                    //mapping the typedomainkey to the index within the factkey
                    keymap.Add(factparameter.Name, new Dictionary<int, int>());
                    for (int kix = 0; kix < factkey.Length; kix++)
                    {
                        var key = factkey[kix];
                        if (Taxonomy.IsTyped(taxonomy, key))
                        {
                            keymap[factparameter.Name].Add(key, kix);
                            typekeylist.Add(key);
                        }
                    }
                    //setting typeddict which will be used for consistency check
                    var typekeys = typekeylist.ToArray();
                    if (typekeys.Length > 0 && !typeddict.ContainsKey(typekeys))
                    {
                        typeddict.Add(typekeys, null);
                        //mapping instancekeys to taxkeys
                        var factsofresultparameter = instance.GetFactsByTaxKey(factkey);
                        foreach (var factofresultparameter in factsofresultparameter)
                        {
                            var instancetypekeys = instance.GetTypedPartIds(factofresultparameter).ToArray();
                            var taxtypekeys = instance.GetTypedPartDomainIds(factofresultparameter).ToArray();
                            if (!typedinstdict.ContainsKey(instancetypekeys))
                            {
                                typedinstdict.Add(instancetypekeys, taxtypekeys);
                            }
                        }
                    }
                    if (typekeys.Length == 0)
                    {

                    }
                }
               
            }


            if (typeddict.Count>1)
            {
                Utilities.Logger.WriteLine(String.Format("Rule {0} has multiple typed dimension configurations!",rule.ID));
            }
            bool ishandled = false;
            if (typeddict.Count==1)
            {
                ishandled = true;
                foreach (var tkey in typedinstdict) 
                {
                    var r = new ValidationRuleResult();
                    result.Add(r);
                    r.ID = rule.ID;
                    foreach (var prule in singlefactparameters) 
                    {
                        var p = ruleresult.Parameters.FirstOrDefault(i => i.Name == prule.Name);
                        var pkeymap = keymap[p.Name];

                        var firstfactid = p.FactIDs.FirstOrDefault();
                        var pkey = instance.GetFactKeyByIdString(firstfactid).ToList().ToArray();

                        var newp = p.Copy();
                        r.Parameters.Add(newp);
                        newp.FactIDs.Clear();
                        newp.Cells.Clear();

                        for (int ix = 0; ix < tkey.Key.Length; ix++) 
                        {
                            var instkey = tkey.Key[ix];
                            var taxkey = tkey.Value[ix];
                            if (pkeymap.ContainsKey(taxkey))
                            {
                                var position = pkeymap[taxkey];
                                pkey[position] = instkey;
                            }
                        }

                        var instancefacts = instance.GetFactsByInstKey(pkey);
                        if (instancefacts.Count == 1)
                        {
                            var instancefact = instancefacts.FirstOrDefault();
                            var factidstring = String.Format("I:{0}", instancefact.IX);
                            newp.FactIDs.Add(factidstring);
                            newp.Value = instancefact.Value;
                            var cells = taxonomy.GetCellsOfFact(instancefact.TaxonomyKey);
                            var celllist = new List<string>();
                            foreach (var cell in cells)
                            {
                                celllist.Add(instance.GetDynamicCellID(cell, instancefact));
                            }
                            newp.Cells.Add(celllist);
                        }
                        if (instancefacts.Count > 1)
                        {
                            Utilities.Logger.WriteLine(String.Format("Multiple facts found in the instance for Rule {0}, parameter {1}", rule.ID, p.Name));
                        }

                    }
                }
            }
            if (!ishandled) 
            {
                result.Add(ruleresult);
            }
            return result;
        }
        */

        //public static bool IsTyped(Taxonomy taxonomy, int key)
        //{
        //    var x = taxonomy.TypedDimensions.Any(i => taxonomy.FactParts[i.Key] == key);
        //    return x != null;
        //}
    }
}
