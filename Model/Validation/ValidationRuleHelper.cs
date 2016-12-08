using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.Validation
{
    public class ValidationRuleHelper
    {
        public static List<ValidationRuleResult> GetTypedResults(Instance instance, ValidationRule rule, ValidationRuleResult ruleresult) 
        {
            if (rule.ID.Contains("1635")) 
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
            foreach(var factparameter in singlefactparameters)
            {
                var resultparameter = ruleresult.Parameters.FirstOrDefault(i=>i.Name==factparameter.Name);
                var factkeystring = resultparameter.FactIDs.FirstOrDefault();
                var factkey = instance.GetFactKeyByIdString(factkeystring);
                var typekeylist = new List<int>();

                //mapping the typedomainkey to the index within the factkey
                keymap.Add(factparameter.Name, new Dictionary<int,int>());
                for (int kix = 0; kix < factkey.Length; kix++) 
                {
                    var key = factkey[kix];
                    if (Taxonomy.IsTyped(taxonomy, key)) 
                    {
                        keymap[factparameter.Name].Add(key,kix);
                        typekeylist.Add(key);
                    }
                }
                //setting typeddict which will be used for consistency check
                var typekeys = typekeylist.ToArray();
                if (typekeys.Length>0 && !typeddict.ContainsKey(typekeys))
                {
                    typeddict.Add(typekeys,null);
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
                    foreach (var p in ruleresult.Parameters) 
                    {
                       
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
                            var position = pkeymap[taxkey];
                            pkey[position] = instkey;
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


        //public static bool IsTyped(Taxonomy taxonomy, int key)
        //{
        //    var x = taxonomy.TypedDimensions.Any(i => taxonomy.FactParts[i.Key] == key);
        //    return x != null;
        //}
    }
}
