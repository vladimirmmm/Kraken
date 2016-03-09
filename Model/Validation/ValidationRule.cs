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
    }
    public class ConceptValidationRule : SimpleValidationRule 
    {
        public Func<InstanceFact, bool> IsOk = (f) => true;
    
    }
    public class TypedDimensionValidationRule : SimpleValidationRule
    {
        public Func<string, bool> IsOk = (s) => true;

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

    }
    
    public class ValidationRule:SimpleValidationRule
    {
        internal Taxonomy Taxonomy = null;
        public String FunctionString= "";
        public String ID { get; set; }
        public String FunctionName { get { return Utilities.Strings.AlfaNumericOnly(this.ID); } }
        public Expression RootExpression = null;

        [JsonIgnore]
        public override string OriginalExpression { get; set; }
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
                    var item = instance.FactDictionary[factkey].FirstOrDefault();
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
                    var item = instance.FactDictionary[factkey];
                    values.AddRange(item);
                }
            }
            return values;
        }

        public List<ValidationRuleResult> GetAllResults()
        {
            var results = new List<ValidationRuleResult>();
            var factgroups = Parameters.FirstOrDefault().TaxFacts;
            if (this.ID.Contains("de_sprv_vcrs_0030"))
            {

            }
            for (int i = 0; i < factgroups.Count; i++)
            {
                var vruleresult = new ValidationRuleResult();
                results.Add(vruleresult);
                vruleresult.ID = this.ID;

                foreach (var p in Parameters)
                {
                    if (p.IsGeneral) { continue; }
                    p.Clear();
                    p.CurrentCells.Clear();
                    var itemfacts = new List<string>();
                    var itemfactids = new List<int>();
                  

                    var sp = new SimpleValidationParameter();
                    sp.Name = p.Name;
                    sp.BindAsSequence = p.BindAsSequence;
                    vruleresult.Parameters.Add(sp);

                    var facts = p.TaxFacts[i];

                    if (p.BindAsSequence)
                    {
                        //set the cells
                        itemfactids.AddRange(facts);
                        //itemfacts.AddRange(facts.Select(f => Taxonomy.GetFactStringKey(Taxonomy.FactsIndex[f])));
                        foreach (var tax_fact in itemfactids)
                        {
                            var cellist = new List<string>();
                            sp.Cells.Add(cellist);
                            var taxfactkey = Taxonomy.FactsIndex[tax_fact];
                            if (Taxonomy.HasFact(taxfactkey))
                            {

                                var cells = Taxonomy.GetCellsOfFact(taxfactkey);

                                cellist.AddRange(cells);
                            }
                      
                        }

                    }
                    else
                    {
                        if (facts.Count > 1)
                        {
                            Logger.WriteLine("Issue with " + this.ID + " parameter " + p.Name);
                        }
                        else
                        {
                            if (facts.Count == 1)
                            {
                                //var fact = Taxonomy.GetFactStringKey(Taxonomy.FactsIndex[facts.FirstOrDefault()]);
                                //itemfacts.Add(fact);
                                var fact = facts.FirstOrDefault();
                                var factkey = Taxonomy.FactsIndex[fact];
                                itemfactids.Add(fact);
                                //set the cells
                                var cells = new List<String>();
                                sp.Cells.Add(cells); ;
                                if (Taxonomy.HasFact(factkey))
                                {
                                    cells.AddRange(Taxonomy.GetCellsOfFact(factkey));
                                }
                                
                            }
                        }
                    }

                    //sp.Facts.AddRange(itemfacts);
                    sp.FactIDs.AddRange(itemfactids.Select(f => String.Format("T:{0}", f)));
                    //p.FactIDs.Add(String.Format("I:{0}", fact.IX));
                    //

                }
            }

           

            return results;
        }

        protected InstanceFact GetFact(string factstring, Instance instance) 
        {
            var facts = new List<InstanceFact>();
            if (instance.FactDictionary.ContainsKey(factstring)) 
            {
                facts = instance.FactDictionary[factstring];
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
                    facts = instance.FactDictionary[factkey].ToList();
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

        public List<ValidationRuleResult> GetAllInstanceResults(Instance instance)
        {
            if (this.ID.Contains("1067")) 
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
                        Taxonomy.GetFactStringKey(Taxonomy.FactsIndex[Utilities.Converters.FastParse(i.Substring(2))]
                         ))).ToList();
                    hastyped = facts.Any(i => i.Dimensions.Any(j => j.IsTyped));
                }
                if (hastyped)
                {
                    //facts = result.Parameters.SelectMany(i => i.Facts).Select(i => FactBase.GetFactFrom(i)).ToList();
                    facts = result.Parameters.SelectMany(i => i.FactIDs).Select(i => FactBase.GetFactFrom(
                          Taxonomy.GetFactStringKey(Taxonomy.FactsIndex[Utilities.Converters.FastParse(i.Substring(2))]
                           ))).ToList();
                    var resultfactgroup = result.FactGroup;

                    var firstfact = facts.FirstOrDefault();
                    var instancefacts = instance.GetFacts(firstfact.GetFactKey());
                    var typestoreplace = new Dictionary<string, string>();
                    if (instancefacts.Count > 0) 
                    {
                        resultstoremove.Add(result);
                        var typedfacts = instancefacts[0].Dimensions.Where(i=>i.IsTyped).Select(i=>i.ToStringForKey());
                        foreach (var typedfact in typedfacts) 
                        {
                            var key = typedfact.Substring(typedfact.IndexOf(":"));
                            typestoreplace.Add(key, "");
                        }

                    }
                    for (int i = 0; i < instancefacts.Count;i++ )
                    {
                        var instancefact = instancefacts[i];
                        var typedimensions = instancefact.Dimensions.Where(d=>d.IsTyped).ToList();
                        foreach (var tdim in typedimensions) 
                        {
                            var key = tdim.ToStringForKey();
                            key = key.Substring(key.IndexOf(":"));
                            typestoreplace[key] = tdim.DomainMemberFullName.Substring(tdim.DomainMemberFullName.IndexOf(":"));
                        }

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
                         
                                var factid = Utilities.Converters.FastParse( p.FactIDs[f_ix].Substring(2));
                                var fact = Taxonomy.GetFactStringKey(Taxonomy.FactsIndex[factid]);
                                //var fact = p.Facts[f_ix];
                                var newfactstring = fact.ToString();
                                foreach (var key in typestoreplace.Keys) 
                                {
                                    newfactstring = newfactstring.Replace(key, typestoreplace[key]);
                                }
                                //p.Facts[f_ix] = newfactstring;
                                var instfact = GetFact(newfactstring, instance);
                                var inst_ix = instfact == null ? -1 : instfact.IX;
                                p.FactIDs[f_ix] = String.Format("I:{0}", inst_ix);
                                var newfact = FactBase.GetFactFrom(newfactstring);
                                if (Taxonomy.HasFact(fact))
                                {
                                    var cellist = new List<string>();
                                    p.Cells.Add(cellist);
                                    var cells = Taxonomy.GetCellsOfFact(fact);
                                    cellist.Clear();
                                    foreach (var cell in cells)
                                    {
                                        cellist.Add(instance.GetDynamicCellID(cell, newfact));
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
            return allinstanceresults;
        }

        public void ValidateResult(ValidationRuleResult result, Instance instance)
        {

            if (this.ID.Contains("0008")) 
            { 
            }
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

                    //foreach (var factstring in p.Facts)
                    //{
                    //    var factininstance = GetFact(factstring, instance);
                    //    if (factininstance != null)
                    //    {
                    //        facts.Add(factininstance);
                    //    }
                    //}
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
                        rp.DecimalValues = stringvalues.Select(i => LogicalModel.Validation.Functions.Number(i)).ToArray();// GetValues(parameterfactgroup.Facts).Select(i => double.Parse(i)).ToArray();
                        rp.StringValue = Utilities.Strings.ArrayToString(rp.DecimalValues);
                        rp.Decimals = decimals;
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
                                if (rp.StringValue.Length > 29 || !Utilities.Strings.IsDigitsOnly(rp.StringValue, '.', '-'))
                                {
                                    var cells = Utilities.Strings.ArrayToString(rp.CurrentCells.ToArray());
                                    Logger.WriteLine(String.Format("Invalid Value Detected at {2}: {0} Cells: {1}", rp.StringValue, cells, this.ID));
                                    rp.StringValue = "";
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
                if (this.ID.Contains("173")) 
                { 
                }
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
            foreach (var r in allinstanceresults) 
            {
                if (this.ID.Contains("0170"))
                {
                }
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
    

    public class SimpleValidationParameter 
    {
        public string Name { get; set; }

        private List<String> _Facts = new List<String>();
        public List<String> Facts { get { return _Facts; } set { _Facts = value; } }

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
            this.Facts.Clear();
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
            sp.Facts.AddRange(_Facts);
            sp.FactIDs.AddRange(_FactIDs);
            sp.Name = this.Name;
            sp.Value = this.Value;
            sp.BindAsSequence = this.BindAsSequence;
            return sp;
        }
    }

}
