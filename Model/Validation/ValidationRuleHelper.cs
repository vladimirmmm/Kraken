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
            var rule = ruleresult.Rule;
            var rulefactparameters = rule.Parameters.Where(i => !i.IsGeneral).ToList();
            foreach (var rulefactparameter in rulefactparameters)
            {
                var p = ruleresult.Parameters.FirstOrDefault(i => i.Name == rulefactparameter.Name);
                
                p.InstanceFacts = p.FactIDs.SelectMany(i => instance.GetFactsByIdString(i)).ToList();

                var newfactids = new List<string>();
                foreach (var fact in p.InstanceFacts)
                {
                    newfactids.Add(String.Format("I:{0}", fact.IX));

                }
                //foreach (var factid in p.FactIDs) 
                //{
                //    var facts = instance.GetFactsByIdString(factid);
                //    if (facts.Count> 0)
                //    {
                //        foreach (var fact in facts) 
                //        {
                //            newfactids.Add(String.Format("I:{0}", fact.IX));

                //        }
                //    }
                //    else 
                //    {
                //        newfactids.Add(factid);
                //    }
                //}
                p.FactIDs = newfactids;
            }
            
        }
       
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
                    var instancefact = instance.GetFactByIDStringForCell(factid);
                    var cells = new List<string>();
                    if (instancefact.InstanceKey.Length > 0)
                    {
                        cells = instance.GetCells(instancefact);
                        p.Value = instancefact.Value + ",";

                    }
                    else 
                    {
                        cells = instance.GetCellsByTaxFactIndex(instancefact.TaxonomyKey);
                    }
                   
                    p.Cells.Add(cells);
                  

                }
                if (string.IsNullOrEmpty(p.Value)) 
                {
                    p.Value = result.Rule.Parameters[p_i].FallBackValue;
                }
                p.Value = p.Value.TrimEnd(',');

       
                p_i++;
            }
        }
      
    }
}
