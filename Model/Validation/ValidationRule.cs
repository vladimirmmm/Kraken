using LogicalModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicalModel.Expressions;
using Utilities;
using Newtonsoft.Json;

namespace LogicalModel.Validation
{
    public class ValidationFunctionContainer 
    {
        public Dictionary<String, Func<List<ValidationParameter>, bool>> FunctionDictionary = new Dictionary<string, Func<List<ValidationParameter>, bool>>();
        public Functions functions = new Functions();

        public void AddInstance(Instance instance) 
        {
            functions.Instance = instance;
        }
    }
    public class ConceptValidationRule : SimpleValidationRule 
    {
        public string Concept;

        public Func<InstanceFact, bool> IsOk = (f) => true;

        public override List<ValidationRuleResult> GetAllResults(Taxonomy taxonomy)
        {
            var results = new List<ValidationRuleResult>();
            var ix = taxonomy.FactParts[Concept];
            //FP if (taxonomy.FactsOfConcepts.ContainsKey(Concept))
            if (taxonomy.FactsOfParts.ContainsKey(ix))
            {
                var facts = taxonomy.FactsOfParts[ix];
            }
            return results;
        }
    }
    public class TypedDimensionValidationRule : SimpleValidationRule
    {
        public Func<string, bool> IsOk = (s) => true;
        public string TypedDimension;

        public override List<ValidationRuleResult> GetAllResults(Taxonomy taxonomy)
        {
            var results = new List<ValidationRuleResult>();
            var ix = taxonomy.FactParts[TypedDimension];
            if (taxonomy.FactsOfParts.ContainsKey(ix)) 
            {
                var facts = taxonomy.FactsOfParts[ix];
            }
            return results;
        }

    }
    public class SimpleValidationRule
    {
        public String ID { get; set; }
        public virtual String DisplayText { get; set; }
        public virtual String OriginalExpression { get; set; }
        protected List<String> _Tables = new List<string>();
        public virtual List<String> Tables { get { return _Tables; } set { _Tables = value; } }


        public SimpleValidationRule() 
        {

        }
        public SimpleValidationRule(ValidationRule rule)
        {
            this.ID = rule.ID;
            this.DisplayText = rule.DisplayText;
            this.OriginalExpression = rule.OriginalExpression;
            this.Tables = rule.Tables;
      
           
        }

        public virtual List<ValidationRuleResult> GetAllInstanceResults(Instance instance)
        {
            return GetAllResults(instance.Taxonomy);
        }
        public virtual List<ValidationRuleResult> GetAllResults(Taxonomy taxonomy)
        {
            var results = new List<ValidationRuleResult>();

            return results;
        }

    }
    
    public class ValidationRule:SimpleValidationRule
    {
        internal Taxonomy Taxonomy = null;
        private String _FunctionString= "";
        public String FunctionString { get { return _FunctionString; } set { _FunctionString = value; } }
        public String ID { get; set; }
        public String FunctionName { get { return Utilities.Strings.AlfaNumericOnly(this.ID); } }
        public Expression RootExpression = null;
        public FactBaseQuery BaseQuery = null;

        [JsonIgnore]
        public string RawInfo = "";
        //[JsonIgnore]
        //public override string OriginalExpression { get; set; }
        [JsonIgnore]
        public override List<string> Tables
        {
            get
            {
                return base.Tables;
            }
            set
            {
                base.Tables = value;
            }
        }
        [JsonIgnore]
        public string LabelID { get; set; }
        
        private Label _Label = null;
        [JsonIgnore]
        public Label Label {
            get
            {
                if (_Label == null && !String.IsNullOrEmpty(LabelID))
                {
                    var labelkey = LogicalModel.Label.GetKey("val", LabelID);
                    _Label = Taxonomy.FindLabel(labelkey);
               
                }
                return _Label;
            }
            set
            {
                _Label = value;
            }
        }

        private string _DisplayText = "";
        [JsonIgnore]        
        public override string DisplayText 
        {
            get {
                if (String.IsNullOrEmpty(_DisplayText) && Label != null) 
                {
                    _DisplayText = Label.Content;
                }
                return _DisplayText;
            }
            set 
            {
                _DisplayText = value;
            }
        }

        private List<ValidationParameter> _Parameters = new List<ValidationParameter>();
        public List<ValidationParameter> Parameters { get { return _Parameters; } set { _Parameters = value; } }

        public Func<List<ValidationParameter>, bool> Function = null;


        private List<int> _TypedDimensions = new List<int>();
        public List<int> TypedDimensions { get { return _TypedDimensions; } set { _TypedDimensions = value; } }

        private List<int> _CoveredParts = new List<int>();
        public List<int> CoveredParts { get { return _CoveredParts; } set { _CoveredParts = value; } }


        private Instance instance = null;

        public void Setinstance(Instance instance)
        {
            this.instance = instance;
        }
        public void SetTaxonomy(Taxonomy taxonomy)
        {
            this.Taxonomy = taxonomy;
            var rule = this;
            var tableids = taxonomy.Tables.Where(i=>i.ValidationRules.Contains(rule.ID)).Select(i=>i.ID).ToList();
            this.Tables.Clear();
            this.Tables.AddRange(tableids);
        }



        public List<String> GetValues(List<FactBase> Facts)
        {
            var values = new List<String>();
            foreach (var fact in Facts)
            {
                var factkey = fact.GetFactKey();
                if (instance.FactDictionary.ContainsKey(factkey))
                {
                    var item = instance.FactDictionary.GetFact(factkey);
                    values.Add(item.Value);
                }
            }
            return values;
        }

        public List<InstanceFact> GetInstanceFacts(List<FactBase> Facts)
        {
            var values = new List<InstanceFact>();
            foreach (var fact in Facts)
            {
                var factkey = fact.GetFactKey();
                if (instance.FactDictionary.ContainsKey(factkey))
                {
                    var item = instance.FactDictionary.GetFacts(factkey);
                    values.AddRange(item);
                }
            }
            return values;
        }
        public override List<ValidationRuleResult> GetAllResults(Taxonomy taxonomy)
        {
            SetTaxonomy(taxonomy);
            return GetAllResults();
        }
        public List<ValidationRuleResult> GetAllResults()
        {
            var results = new List<ValidationRuleResult>();
            var factgroups = Parameters.FirstOrDefault().TaxFacts;
  
            for (int i = 0; i < factgroups.Count; i++)
            {
                var vruleresult = new ValidationRuleResult();
                vruleresult.Rule = this;
                results.Add(vruleresult);
                vruleresult.ID = this.ID;

                foreach (var p in Parameters)
                {
                    if (p.IsGeneral) { continue; }
                    p.Clear();
                    //p.CurrentCells.Clear();
                    var itemfacts = new List<string>();
                    var itemfactids = new List<int>();
                  

                    var sp = new SimpleValidationParameter();
                    sp.Name = p.Name;
                    sp.BindAsSequence = p.BindAsSequence;
                    vruleresult.Parameters.Add(sp);
                    
                    //TODO
                    IList<int> facts=null;
                    if (i >= p.TaxFacts.Count && p.TaxFacts.Count==1)
                    {
                        facts = p.TaxFacts[0];

                    }else
                    {
                        facts = p.TaxFacts[i];
                    }
                  
                    if (p.BindAsSequence)
                    {
                        //set the cells
                        itemfactids.AddRange(facts);
                        itemfacts.AddRange(facts.Select(f => Taxonomy.GetFactStringKey(Taxonomy.FactsManager.GetFactKey(f))));
                        foreach (var tax_fact in itemfactids)
                        {
                            //var cellist = new List<string>();
                            //sp.Cells.Add(cellist);
                            //var taxfactkey = Taxonomy.FactsManager.GetFactKey(tax_fact);
                            //if (Taxonomy.HasFact(taxfactkey))
                            //{

                            //    var cells = Taxonomy.GetCellsOfFact(taxfactkey);

                            //    cellist.AddRange(cells);
                            //}
                      
                        }

                    }
                    else
                    {
                        if (facts.Count > 1)
                        {
                            //TODO
                            //Logger.WriteLine("Issue with " + this.ID + " parameter " + p.Name);
                        }
                        else
                        {
                            if (facts.Count == 1)
                            {
                                var factkey = Taxonomy.GetFactStringKey(Taxonomy.FactsManager.GetFactKey(facts.FirstOrDefault()));
                                itemfacts.Add(factkey);
                                var factid = facts.FirstOrDefault();
                                itemfactids.Add(factid);
                                //set the cells
                                //var cells = new List<String>();
                                //sp.Cells.Add(cells); ;
                                //if (Taxonomy.HasFact(factkey))
                                //{
                                //    cells.AddRange(Taxonomy.GetCellsOfFact(factkey));
                                //}
                                
                            }
                        }
                    }

                    //sp.Facts.AddRange(itemfacts);
                    sp.FactIDs.AddRange(itemfactids.Select(f => String.Format("T:{0}", f)));
                    //p.FactIDs.Add(String.Format("I:{0}", fact.IX));
                    //

                }
            }

            //results = ValidationRuleHelper.ExecuteImplicitFiltering(Taxonomy, results);
            //Taxonomy.SetCells(results);
            ValidationRuleHelper.SetCells(Taxonomy, results);
            return results;
        }

        protected InstanceFact GetFact(string factstring, Instance instance) 
        {
            var facts = new List<InstanceFact>();
            if (instance.FactDictionary.ContainsKey(factstring)) 
            {
                facts = instance.FactDictionary.GetFacts(factstring);
                if (facts.Count == 1) {
                    return facts[0];
                }
            }
            var shouldprocess=false;
            foreach(var td in Taxonomy.TypedDimensions)
            {
                if (factstring.Contains(td.Key))
                {
                    shouldprocess=true;
                    break;
                }
            }
            if (shouldprocess)
            {
                var factbase = new FactBase();
                factbase.SetFromString(factstring);
                var factkey = factbase.GetFactKey();
                if (instance.FactDictionary.ContainsKey(factkey))
                {
                    facts = instance.FactDictionary.GetFacts(factkey);
                    foreach (var fact in facts)
                    {
                        if (fact.FactString == factstring)
                        {
                            return fact;
                        }
                    }
                    facts.Clear();
                }
            }
            return facts.FirstOrDefault();
        }
        public override List<ValidationRuleResult> GetAllInstanceResults(Instance instance)
        {
            var taxonomy = instance.Taxonomy;
            var allresults = GetAllResults();
            var results = new List<ValidationRuleResult>();

            ValidationRuleHelper.LoadTypedFacts(instance, allresults);

            var instanceresults = ValidationRuleHelper.ExecuteImplicitFiltering(instance, allresults);

            ValidationRuleHelper.SetCells(instance, instanceresults);
            
            results = instanceresults;
            return results;
        }
        /*
        public List<ValidationRuleResult> GetAllInstanceResultsOld(Instance instance)
        {
            if (this.ID.Contains("de_sprv_vrdp-bi_3260"))
            {
            }
            var taxonomy = instance.Taxonomy;
            var allresults = GetAllResults();
            var allinstanceresults = new List<ValidationRuleResult>();
            var resultstoremove = new List<ValidationRuleResult>();
            var resultstoadd = new List<ValidationRuleResult>();
            var hastyped = this.Parameters.Any(i => i.TypedDimensions.Count > 0);


            if (!hastyped)
            {
                var allfactsindexes = this.Parameters.SelectMany(i => i.TaxFacts.SelectMany(j => j)).ToList();

                foreach (var factindex in allfactsindexes)
                {
                    var factindexintervallist = new IntervalList(factindex);
                    var existing = Utilities.Objects.IntersectSorted(factindexintervallist, taxonomy.TypedIntervals, null);
                    if (existing.Intervals.Count > 0)
                    {
                        hastyped = true;
                        break;
                    }
                }
            }
            //var hastyped = this.Parameters.Any(i => i.TaxFacts.Any(j=>Taxonomy.IsTyped() > 0);
            if (this.Parameters.Count == 1 && this.Parameters.FirstOrDefault().IsGeneral)//.StringValue == "filingindicators") 
            {
                if (allresults.Count == 1)
                {
                    var theresult = allresults.FirstOrDefault();
                    resultstoremove.Add(theresult);
                    var finds = instance.FilingIndicators.Where(i => i.Filed).ToList();
                    foreach (var find in finds)
                    {
                        var findresult = new ValidationRuleResult();
                        resultstoadd.Add(findresult);

                        findresult.ID = theresult.ID;
                        findresult.Parameters.AddRange(theresult.Parameters.Select(p => p.Copy()));
                        findresult.Parameters.FirstOrDefault().Value = find.ID;
                    }
                }
                if (allresults.Count == 0)
                {
                    foreach (var find in instance.FilingIndicators)
                    {
                        var findresult = new ValidationRuleResult();
                        resultstoadd.Add(findresult);

                        findresult.ID = this.ID;
                        foreach (var p in Parameters)
                        {
                            var sp = new SimpleValidationParameter();
                            sp.Name = p.Name;
                            sp.BindAsSequence = p.BindAsSequence;
                            sp.Value = p.StringValue;
                            findresult.Parameters.Add(sp);
                            if (p.IsGeneral && p.StringValue == "filingindicators")
                            {
                                sp.Value = find.ID;
                            }
                        }
                    }
                }
            }

            foreach (var result in allresults)
            {
    
                if (hastyped)
                {
                    //resultstoadd.AddRange(ValidationRuleHelper.GetTypedResults(instance, this, result));
                    resultstoadd.AddRange(ValidationRuleHelper.ResolveTypedFacts(instance, result));
                    resultstoremove.Add(result);
                
                }

            }
            allinstanceresults = allresults;
            foreach (var r in resultstoremove)
            {
                allinstanceresults.Remove(r);

            }
            allinstanceresults.AddRange(resultstoadd);
            foreach (var result in allinstanceresults)
            {
                foreach (var p in result.Parameters)
                {
                    if (p.Cells.Any(c1 => c1.Any(c2 => c2.Contains(Literals.DynamicCode))))
                    {
                        for (int i = 0; i < p.FactIDs.Count; i++)
                        {
                            var cells = p.Cells.Count==p.FactIDs.Count ? p.Cells[i] : p.Cells.FirstOrDefault();
                            var fid = p.FactIDs[i];
                            var fk = instance.GetFactKeyByIndexString(fid);
                            //var dcells = instance.GetDynamicCellID(cells.FirstOrDefault(), fs);
                            var dcells = instance.GetDynamicCellID(cells.FirstOrDefault(), fk);
                            cells.Clear();
                            cells.Add(dcells);

                        }
                    }
                    var mFactIDs = p.FactIDs.ToList();
                    var ix = 0;
                    foreach (var factidentfier in p.FactIDs)
                    {
                        var parts = factidentfier.Split(":");
                        if (parts[0] == "T")
                        {
                            var tix = Utilities.Converters.FastParse(parts[1]);
                            var taxkey = instance.Taxonomy.FactsManager.GetFactKey(tix);
                            var facts = instance.GetFactsByTaxKey(taxkey);
                            if (facts.Count == 1)
                            {
                                var fact = facts.FirstOrDefault();
                                mFactIDs[ix] = string.Format("I:{0}", fact.IX);
                            }
                        }
                        ix++;
                    }
                    p.FactIDs = mFactIDs;
                }
            }

            return allinstanceresults;
        }
        */
        /*
        public List<ValidationRuleResult> GetAllInstanceResultsOld(Instance instance)
        {
            if (this.ID.Contains("0149")) 
            { 
            }
            var allresults = GetAllResults();
            var allinstanceresults = new List<ValidationRuleResult>();
            var resultstoremove = new List<ValidationRuleResult>();
            var resultstoadd = new List<ValidationRuleResult>();
            var hastyped = false;
            var waschecked = false;
            if (this.Parameters.Count == 1 && this.Parameters.FirstOrDefault().IsGeneral)//.StringValue == "filingindicators") 
            {
                if (allresults.Count == 1)
                {
                    var theresult = allresults.FirstOrDefault();
                    resultstoremove.Add(theresult);
                    var finds = instance.FilingIndicators.Where(i => i.Filed).ToList();
                    foreach (var find in finds)
                    {
                        var findresult = new ValidationRuleResult();
                        resultstoadd.Add(findresult);

                        findresult.ID = theresult.ID;
                        findresult.Parameters.AddRange(theresult.Parameters.Select(p => p.Copy()));
                        findresult.Parameters.FirstOrDefault().Value = find.ID;
                    }
                }
                if (allresults.Count == 0) 
                {
                    foreach (var find in instance.FilingIndicators)
                    {
                        var findresult = new ValidationRuleResult();
                        resultstoadd.Add(findresult);

                        findresult.ID = this.ID;
                        foreach (var p in Parameters)
                        {
                            var sp = new SimpleValidationParameter();
                            sp.Name = p.Name;
                            sp.BindAsSequence = p.BindAsSequence;
                            sp.Value = p.StringValue;
                            findresult.Parameters.Add(sp);
                            if (p.IsGeneral && p.StringValue == "filingindicators") 
                            {
                                sp.Value = find.ID;
                            }
                        }
                    }
                }
            }

            foreach (var result in allresults)
            {
                var facts = new List<FactBase>();
                if (!waschecked)
                {
                    waschecked = true;
                    //facts = result.Parameters.SelectMany(i => i.Facts).Select(i => FactBase.GetFactFrom(i)).ToList();
                    facts = result.Parameters.SelectMany(i => i.FactIDs).Select(i => FactBase.GetFactFrom(
                        Taxonomy.GetFactStringKey(Taxonomy.FactsManager.GetFactKey(Utilities.Converters.FastParse(i.Substring(2)))
                         ))).ToList();
                    hastyped = facts.Any(i => i.Dimensions.Any(j => j.IsTyped));
                }
                if (hastyped)
                {
                    //facts = result.Parameters.SelectMany(i => i.Facts).Select(i => FactBase.GetFactFrom(i)).ToList();
                    facts = result.Parameters.SelectMany(i => i.FactIDs).Select(i => FactBase.GetFactFrom(
                          Taxonomy.GetFactStringKey(Taxonomy.FactsManager.GetFactKey(Utilities.Converters.FastParse(i.Substring(2)))
                           ))).ToList();
                    var resultfactgroup = result.FactGroup;

                    //instance.TypedFactMembers.ContainsKey()

                    foreach (var firstfact in facts)
                    {
                        var instancefacts = instance.GetFactsByTaxKey(firstfact.GetFactKey());
                        var typestoreplace = new Dictionary<string, string>();
                        var typestoreplaceids = new Dictionary<int, int>();
                        if (instancefacts.Count > 0)
                        {
                            resultstoremove.Add(result);
                            var typedids = instance.GetTypedPartDomainIds(instancefacts[0]);
                            foreach (var typedid in typedids)
                            {
                                typestoreplaceids.Add(typedid, -2);
                            }
                            //var typedfacts = instancefacts[0].Dimensions.Where(i => i.IsTyped).Select(i => i.ToStringForKey());
                            //foreach (var typedfact in typedfacts)
                            //{
                            //    var key = typedfact.Substring(typedfact.IndexOf(":"));
                            //    typestoreplace.Add(key, "");
                            //}

                        }
                        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        for (int i = 0; i < instancefacts.Count; i++)
                        {
                            var instancefact = instancefacts[i];
                            var facttypedids = instance.GetTypedPartIds(instancefact);
                            var tix=0;
                            var tkeys = typestoreplaceids.Keys.ToList();
                            foreach (var tid in tkeys)
                            {
                                typestoreplaceids[tid] = facttypedids[tix];
                                tix++;
                            }
                            //foreach (var tdim in typedimensions)
                            //{
                            //    var key = tdim.ToStringForKey();
                            //    key = key.Substring(key.IndexOf(":"));
                            //    typestoreplace[key] = tdim.DomainMemberFullName.Substring(tdim.DomainMemberFullName.IndexOf(":"));
                            //}

                            var dynamicresult = new ValidationRuleResult();
                            resultstoadd.Add(dynamicresult);
                            dynamicresult.FactGroup = new List<string>() { instancefact.GetFactString() };

                            dynamicresult.ID = result.ID;
                            dynamicresult.Parameters.AddRange(result.Parameters.Select(p => p.Copy()));
                            dynamicresult.Message = result.Message;
                            foreach (var p in dynamicresult.Parameters)
                            {
                                p.Cells.Clear();
                                //for (var f_ix = 0; f_ix < p.Facts.Count; f_ix++)
                                for (var f_ix = 0; f_ix < p.FactIDs.Count; f_ix++)
                                {

                                    var factid = Utilities.Converters.FastParse(p.FactIDs[f_ix].Substring(2));
                                    var factkey = Taxonomy.FactsManager.GetFactKey(factid).ToList().ToArray();
                                    for (int fp_ix = 0; fp_ix < factkey.Length; fp_ix++) 
                                    {
                                        var fp = factkey[fp_ix];
                                        if (typestoreplaceids.ContainsKey(fp)) 
                                        {
                                            factkey[fp_ix] = typestoreplaceids[fp];
                                        }
                                    }
                         
                                    //p.Facts[f_ix] = newfactstring;
                                    var instfact = instance.GetFactsByInstKey(factkey).FirstOrDefault();
                                    var strkey = instfact != null ? instance.GetFactStringKey(instfact.InstanceKey) : "";
                                    var inst_ix = instfact == null ? -1 : instfact.IX;
                                    if (inst_ix == -1) 
                                    {

                                    }
                                    p.FactIDs[f_ix] = String.Format("I:{0}", inst_ix);

                                    if (instfact!=null && Taxonomy.HasFact(instfact.TaxonomyKey))
                                    {
                                        var cellist = new List<string>();
                                        p.Cells.Add(cellist);
                                        var cells = Taxonomy.GetCellsOfFact(instfact.TaxonomyKey);
                                        cellist.Clear();
                                        foreach (var cell in cells)
                                        {
                                            cellist.Add(instance.GetDynamicCellID(cell, instfact));
                                        }
                                    }

                                }
                            }

                        }
                    }
                }

            }
            allinstanceresults = allresults;
            foreach (var r in resultstoremove) 
            {
                allinstanceresults.Remove(r);

            }
            allinstanceresults.AddRange(resultstoadd);
            foreach (var result in allinstanceresults) 
            {
                foreach (var p in result.Parameters) 
                {
                    if (p.Cells.Any(c1 => c1.Any(c2 => c2.Contains(Literals.DynamicCode))))
                    {
                        for (int i = 0; i < p.FactIDs.Count;i++ )
                        {
                            var cells = p.Cells[i];
                            var fid = p.FactIDs[i];
                            var fk = instance.GetFactKeyByIndexString(fid);
                            var dcells = instance.GetDynamicCellID(cells.FirstOrDefault(), fk);
                            cells.Clear();
                            cells.Add(dcells );

                        }
                    }
                    var mFactIDs = p.FactIDs.ToList();
                    var ix=0;
                    foreach (var factidentfier in p.FactIDs) 
                    {
                        var parts = factidentfier.Split(":");
                        if (parts[0] == "T")
                        {
                            var tix = Utilities.Converters.FastParse(parts[1]);
                            var taxkey = instance.Taxonomy.FactsManager.GetFactKey(tix);
                            var facts = instance.GetFactsByTaxKey(taxkey);
                            if (facts.Count==1)
                            {
                                var fact = facts.FirstOrDefault();
                                mFactIDs[ix] = string.Format("I:{0}", fact.IX);
                            }
                        }
                        ix++;
                    }
                    p.FactIDs = mFactIDs;
                }
            }

            return allinstanceresults;
        }
        */
        public void ValidateResult(ValidationRuleResult result, Instance instance)
        {

            var HasAtLeastOneValue = false;
            var HasMissingValue = false;
            foreach (var p in result.Parameters)
            {
                var rp = Parameters.FirstOrDefault(i => i.Name == p.Name);
                if (rp.IsGeneral)
                {
                    if (rp.StringValue == "filingindicators" && rp.BindAsSequence) 
                    {
                       rp.StringValues = instance.FilingIndicators.Select(i=>i.ID).ToArray();
                       rp.StringValue = Utilities.Strings.ArrayToString(rp.StringValues);

                    }
                    continue;
                }

                rp.CurrentFacts.Clear();
                rp.StringValue = "";
                if (rp.BindAsSequence)
                {

                    var facts = new List<InstanceFact>();

                    foreach (var factstring in p.FactIDs)
                    {
                        var factininstance = instance.GetFactByIDString(factstring);
                        if (factininstance != null)
                        {
                            facts.Add(factininstance);
                        }
                        else 
                        {
                        }

                    }
                    if (facts.Count > 0)
                    {
                        HasAtLeastOneValue = true;
                    }
           
                    //facts = Getf p.Facts;
                    var instancefacts = facts; // GetInstanceFacts(facts);
                    //set the cells
     
                    rp.CurrentFacts.AddRange(instancefacts.ToArray());
                    var stringvalues = instancefacts.Select(i => i.Value);
                    var decimals = instancefacts.Select(i => i.Decimals).ToArray();

                    if (rp.Type == TypeEnum.String)
                    {
                        rp.StringValues = stringvalues.ToArray(); //GetValues(parameterfactgroup.Facts).ToArray();
                        rp.StringValue = Utilities.Strings.ArrayToString(rp.StringValues);
                    }
                    if (rp.Type == TypeEnum.Numeric)
                    {
                        rp.DecimalValues = stringvalues.Select(i => LogicalModel.Validation.Functions.GetNumber(i)).ToArray();// GetValues(parameterfactgroup.Facts).Select(i => double.Parse(i)).ToArray();
                        rp.StringValue = Utilities.Strings.ArrayToString(rp.DecimalValues);
                        rp.Decimals = decimals;
                    }
                    if (rp.Type == TypeEnum.Boolean)
                    {
                        rp.StringValues = stringvalues.ToArray(); //GetValues(parameterfactgroup.Facts).ToArray();
                        rp.StringValue = Utilities.Strings.ArrayToString(rp.StringValues);
                    }
                }
                else
                {
                    if (p.FactIDs.Count > 0)
                    {
                        var fact = instance.GetFactByIDString(p.FactIDs.FirstOrDefault());
                        if (fact != null)
                        {
                            var instancefacts = new List<InstanceFact>() { fact };
                            var factkey = fact.GetFactKey();
                     
                      
                            HasAtLeastOneValue = true;
                            InstanceFact realfact = instancefacts.FirstOrDefault();

                            rp.CurrentFacts.Add(realfact);
                            rp.StringValue = realfact.Value;
                            rp.Decimals = new string[] { realfact.Decimals };
                  
                            //		Decimal.MaxValue	79228162514264337593543950335	decimal
                            if (rp.Type == TypeEnum.Numeric)
                            {
                                if (rp.StringValue.Length > 29 || !Utilities.Strings.IsNumeric(rp.StringValue))
                                {
                                    //var cells = Utilities.Strings.ArrayToString(rp.CurrentCells.ToArray());
                                    Logger.WriteLine(String.Format("Invalid Value Detected at {2}: {0} Cells: {1}", rp.StringValue, "", this.ID));
                                    rp.StringValue = "";
                                    return;
                                }
                            }
                        }
                    }



                }
                if (String.IsNullOrEmpty(rp.StringValue.Trim())) { 
                    HasMissingValue = true;
                }
                if (!HasAtLeastOneValue) 
                {
 
                }
                p.Value = rp.StringValue;
            }

            if (HasAtLeastOneValue && !HasMissingValue)
            {
                Boolean partialresult=true;
           
                //var formula = Taxonomy.SimpleValidationRules.FirstOrDefault(i => i.ID == this.ID);
                try
                {
                    partialresult = Function(Parameters);
                }
                catch (Exception ex) 
                {
                    if (ex is DivideByZeroException) 
                    {
                        partialresult = false;
                    }
                }
                if (!partialresult)
                {
                    if (this.ID.Contains("-bi")) 
                    {
                    }
                    result.ID = this.ID;
                    result.IsOk = false;
                    var me =this;
                    var allfinds = new List<string>();
                    foreach(var table in this.Tables)
                    {
                        var tbl = Taxonomy.Tables.FirstOrDefault(i=>i.ID==table);

                        allfinds.Add(tbl.FilingIndicator);
                    }
                    allfinds = allfinds.Distinct().ToList();
                    var isok = true;
                    foreach (var find in allfinds) 
                    {
                        var hasdata = false;
                        var tg = Taxonomy.Module.TableGroups.FirstOrDefault(i => i.Item.FilingIndicator == find);
                        if (tg != null)
                        {
                            foreach (var tableid in tg.Item.TableIDs)
                            {
                                var tbl = Taxonomy.Tables.FirstOrDefault(i => i.ID == tableid);

                                if (tbl.InstanceFactsCount > 0)
                                {
                                    hasdata = true;
                                    break;
                                }
                            }
                        }
                        if (!hasdata) 
                        {
                            isok = false;
                            break;
                        }
                    }
                    result.HasAllFind = isok ? "1" : "2";
                }
            }

        }

        public List<ValidationRuleResult> Validate(Instance instance)
        {
            var results = new List<ValidationRuleResult>();
            Setinstance(instance);
            var allinstanceresults = GetAllInstanceResults(instance);
            if (this.ID.Contains("de_sprv_vrdp-r_1990")) 
            {

            }
            foreach (var r in allinstanceresults) 
            {
             
                ValidateResult(r, instance);
                if (!r.IsOk) 
                {
                    results.Add(r);
                }
            }
            return results;
        }

        public override string ToString()
        {
            return String.Format("ValidationRule {0}",this.ID);
        }



        public void ClearObjects()
        {
            foreach (var p in Parameters) 
            {
                p.ClearObjects();
            }
        }
    }
    public class GeneralValidationParameter
    {
        public string Name { get; set; }
        public string TranslatedName { get; set; }
        public string XPathName { get; set; }
        public string XPath { get; set; }
        public string XPathType { get; set; }

        public string GetValue(Instance instance) 
        {
            return instance.ExecuteXPath(XPath);
        }
    }

    public class SimpleValidationParameter 
    {
        public string Name { get; set; }

        public List<InstanceFact> InstanceFacts = new List<InstanceFact>();

        private List<String> _FactIDs = new List<String>();
        public List<String> FactIDs { get { return _FactIDs; } set { _FactIDs = value; } }

        //private Dictionary<string, List<String>> _CellsOfFacts = new Dictionary<string, List<string>>();
        //public Dictionary<string, List<String>> CellsOfFacts { get { return _CellsOfFacts; } set { _CellsOfFacts = value; } }

        private List<List<String>> _Cells = new List<List<string>>();
        public List<List<String>> Cells { get { return _Cells; } set { _Cells = value; } }
       
        public bool BindAsSequence { get; set; }

        public string Value { get; set; }


        internal void Clear()
        {
            this.InstanceFacts.Clear();
            this.FactIDs.Clear();
            this.Cells.Clear();
        }


        internal SimpleValidationParameter Copy()
        {
            var sp = new SimpleValidationParameter();
            foreach (var item in _Cells)
            {
                sp.Cells.Add(item.ToArray().ToList());

            }
            //sp.Facts.AddRange(_Facts);
            sp.FactIDs = new List<string>(_FactIDs.ToArray());
            sp.Name = this.Name;
            sp.Value = this.Value;
            sp.BindAsSequence = this.BindAsSequence;      
            return sp;
        }
    }

}
