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
                    //if (_Label == null) 
                    //{
                    //    labelkey = LogicalModel.Label.GetKeyWithoutPrefix("val", LabelID);
                    //    _Label = Taxonomy.FindLabel(labelkey);
                    //}
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
            var factgroups = Parameters.FirstOrDefault().FactGroups;
            if (this.ID.Contains("0310")) 
            {

            }
            var conceptonlyresults = ValidationRuleResult.GetResultsForConceptOnly(this);
            if (conceptonlyresults.Count > 0) 
            {
                results.AddRange(conceptonlyresults);
            }
            else
            {
                foreach (var factgroupkey in factgroups.Keys)
                {
                    var factgroup = factgroups[factgroupkey];
                    //var firstfact = factgroup.Facts.FirstOrDefault();
                    factgroup.FactString = factgroupkey;//factgroup.GetFactString();
                    var vruleresult = new ValidationRuleResult();
                    results.Add(vruleresult);
                    vruleresult.ID = this.ID;
                    vruleresult.FactGroup = factgroup;


                    foreach (var p in Parameters)
                    {

                        var fg_factstring = factgroup.FactString;

                        if (p.IsGeneral)
                        {
                            continue;
                        }
                        p.Clear();
                        //var parameterfactgroup = p.FactGroups.Count == 1 ? p.FactGroups.FirstOrDefault() : p.FactGroups.FirstOrDefault(i => i.GetFactString() == fg_factstring);

                        var parameterfactgroup = p.FactGroups[fg_factstring];


                        p.CurrentCells.Clear();
                        var itemfacts = new List<string>();

                        var sp = new SimpleValidationParameter();
                        sp.Name = p.Name;
                        sp.BindAsSequence = p.BindAsSequence;
                        vruleresult.Parameters.Add(sp);

                        if (p.BindAsSequence)
                        {
                            //set the cells
                            itemfacts.AddRange(parameterfactgroup.FullFacts.Select(i => i.GetFactKey()));
                            foreach (var tax_fact in parameterfactgroup.FullFacts)
                            {
                                var taxfactkey = tax_fact.GetFactKey();
                                if (Taxonomy.Facts.ContainsKey(taxfactkey))
                                {

                                    var cells = Taxonomy.Facts[taxfactkey];


                                    //sp.Facts.Add(taxfactkey);
                                    if (!sp.CellsOfFacts.ContainsKey(taxfactkey))
                                    {
                                        sp.CellsOfFacts.Add(taxfactkey, new List<string>());
                                    }
                                    sp.CellsOfFacts[taxfactkey].AddRange(cells);
                                }
                                else
                                {
                                    //Cell is blocked maybe.
                                }
                            }

                            //p.CurrentCells.AddRange(cells);
                            //

                        }
                        else
                        {
                            if (parameterfactgroup.FullFacts.Count > 1)
                            {
                                Logger.WriteLine("Issue with " + this.ID + " parameter " + p.Name);
                            }
                            else
                            {
                                var fact = parameterfactgroup.FullFacts.FirstOrDefault();
                                var factkey = fact.GetFactKey();
                                itemfacts.Add(factkey);

                                //set the cells
                                var cells = new List<String>();
                                if (Taxonomy.Facts.ContainsKey(factkey))
                                {
                                    cells.AddRange(Taxonomy.Facts[factkey]);
                                }
                                //p.CurrentCells.AddRange(cells);

                                if (!sp.CellsOfFacts.ContainsKey(factkey))
                                {
                                    sp.CellsOfFacts.Add(factkey, new List<string>());

                                }
                                sp.CellsOfFacts[factkey].AddRange(cells);
                            }
                        }

                        sp.Facts.AddRange(itemfacts);

                    }

                }
              

            }
            return results;
        }
        
        protected List<InstanceFact> GetFactsOfParameterGroup(FactGroup factgroup, ValidationParameter parameter, Instance instance) 
        {
            var facts = new List<InstanceFact>();
            var typedfactkeys = factgroup.Dimensions.Where(i => i.IsTyped).Select(i => i.DomainMemberFullName);
            if (parameter.FactGroups.Count!=1)
            {

            }
            var parameterfacts = parameter.FactGroups.FirstOrDefault().Value.FullFacts;
            var parameterinstancefacts = GetInstanceFacts(parameterfacts);
            foreach (var fact in parameterinstancefacts)
            {
                var ok = true;
                var fs = fact.FactString;
                foreach (var typedfactkey in typedfactkeys) 
                {
                    if (!fs.Contains(typedfactkey)) 
                    {
                        ok = false;
                        break;
                    }
                }
                if (ok) 
                {
                    facts.Add(fact);
                }
            }
            return facts;
        }

        protected InstanceFact GetFact(string factstring, Instance instance) 
        {
            var factbase = new FactBase();
            factbase.SetFromString(factstring);
            var factkey= factbase.GetFactKey();
            var facts=new List<InstanceFact>();
            if(instance.FactDictionary.ContainsKey(factkey))
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
            if (facts.Count == 0) 
            {
            }
            return facts.FirstOrDefault();
        }

        public List<ValidationRuleResult> GetAllInstanceResults(Instance instance)
        {
            if (this.ID.Contains("0310")) 
            { 
            }
            var allresults = GetAllResults();
            var allinstanceresults = new List<ValidationRuleResult>();
            foreach (var result in allresults)
            {
                var ishandled = false;
                var hastyped = result.FactGroup.FullFacts.Any(i => i.Dimensions.Any(j => j.IsTyped));
                if (hastyped)
                {
                    


                    //var mainparameter = Parameters.FirstOrDefault(i => !i.IsGeneral);
                    //var parameterfactgroup = mainparameter.FactGroups[result.FactGroup.GetFactKey()];
                    var resultfactgroup = result.FactGroup;
                    var fact = resultfactgroup.FullFacts.FirstOrDefault();
                    var instancefacts = instance.GetFacts(fact.GetFactKey());
                    foreach (var instancefact in instancefacts)
                    {
                        var fg = new FactGroup();
                        if (instancefact.Dimensions.Count == 0)
                        {
                            instancefact.SetFromString(instancefact.FactString);
                        }
                        fg.Dimensions.AddRange(instancefact.Dimensions);
                        //fg.Dimensions.AddRange(instancefact.Dimensions.Where(
                        //    i => result.FactGroup.Dimensions.Any(j => j.ToStringForKey() == i.ToStringForKey())));
                        fg.Concept = result.FactGroup.Concept;
                        fg.AddFact(instancefact);
                        var dynamicresult = new ValidationRuleResult();
                        dynamicresult.FactGroup = fg;

                        dynamicresult.ID = result.ID;
                        dynamicresult.Parameters.AddRange(result.Parameters.Select(i => i.Copy()));
                        dynamicresult.Message = result.Message;
                        foreach (var p in dynamicresult.Parameters)
                        {
                            var rp = this.Parameters.FirstOrDefault(i => i.Name == p.Name);
                            var key=resultfactgroup.GetFactKey();
                            FactGroup parameterfactgroup = null;
                            if (rp.FactGroups.ContainsKey(key))
                            {
                                parameterfactgroup = rp.FactGroups[key];
                            }
                            else 
                            {
                                if (rp.FactGroups.Count == 1)
                                {
                                    parameterfactgroup = resultfactgroup;
                                }
                                else 
                                {
                                }
                            }

                            p.Facts.Clear();
                            p.CellsOfFacts.Clear();
                            //var rpfactgroup = rp.FactGroups[parameterfactgroup.GetFactKey()];
                            if (rp.FactGroups.Count>1)
                            {

                            }
                            var rpfactgroup = parameterfactgroup;
                            foreach (var rpfact in rpfactgroup.FullFacts)
                            {
                                var newfact = new FactBase();
                                newfact.SetFromString(rpfact.GetFactString());
                                newfact.Dimensions = newfact.Dimensions.Where(i => !i.IsTyped).ToList();
                                newfact.Dimensions.AddRange(instancefact.Dimensions.Where(i => i.IsTyped));
                                newfact.SetFactString();
                                var factkey = newfact.GetFactKey();
                                p.Facts.Add(newfact.FactString);
                                if (Taxonomy.Facts.ContainsKey(factkey))
                                {
                                    p.CellsOfFacts.Add(factkey,new List<string>());
                                    var cells = Taxonomy.Facts[factkey];
                                    foreach (var cell in cells) 
                                    {
                                        p.CellsOfFacts[factkey].Add(instance.GetDynamicCellID(cell, newfact));
                                    }
                                }
                                else
                                {
                                   // results.ad
                                }


                            }

                            //var instparamfacts = GetFactsOfParameterGroup(fg, rp, instance);
                            //var instparamfactstrings = instparamfacts.Select(i => i.GetFactString());
                            //p.Facts.AddRange(instparamfactstrings);
                        }

                        allinstanceresults.Add(dynamicresult);
                        ishandled = true;

                    }
                    //ishandled = true;
                }
                //handling concept only 
                var factgroup = this.Parameters.FirstOrDefault().FactGroups.Values.FirstOrDefault();
                var firstfact = factgroup.FullFacts.FirstOrDefault();
                if (factgroup.FullFacts.Count == 1 && firstfact.Dimensions.Count == 0)
                {
                    if (firstfact.Concept == null)
                    {

                    }
                    else
                    {
                        ishandled = true;
                    }
                }



                if (!ishandled)
                {
                    allinstanceresults.Add(result);

                }
            }

            return allinstanceresults;
        }

        public void ValidateResult(ValidationRuleResult result, Instance instance)
        {

            if (this.ID.Contains("0312")) 
            { 
            }
            var HasAtLeastOneValue = false;
            var HasMissingValue = false;
            foreach (var p in result.Parameters)
            {
                var rp = Parameters.FirstOrDefault(i => i.Name == p.Name);
                if (rp.IsGeneral)
                {
                    continue;
                }
                //p.Clear();

                rp.CurrentFacts.Clear();
                rp.StringValue = "";
                if (rp.BindAsSequence)
                {

                    var facts = new List<InstanceFact>();

                    foreach (var factstring in p.Facts)
                    {
                        var factininstance = GetFact(factstring, instance);
                        if (factininstance != null)
                        {
                            facts.Add(factininstance);
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
                        rp.DecimalValues = stringvalues.Select(i => decimal.Parse(i)).ToArray();// GetValues(parameterfactgroup.Facts).Select(i => double.Parse(i)).ToArray();
                        rp.StringValue = Utilities.Strings.ArrayToString(rp.DecimalValues);
                        rp.Decimals = decimals;
                    }
                }
                else
                {
                    if (p.Facts.Count > 0)
                    {
                        var fact = GetFact(p.Facts.FirstOrDefault(), instance);
                        if (fact != null)
                        {
                            var instancefacts = new List<InstanceFact>() { fact };
                            var factkey = fact.GetFactKey();
                            if (rp.Name == "a" && fact.Value == "325235044.81" && this.ID.Contains("0647")) 
                            {
                            }
                      
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



                    //if (String.IsNullOrEmpty(rp.StringValue.Trim())) { HasMissingValue = true; }
                    //p.Value = rp.StringValue;
                }
                if (String.IsNullOrEmpty(rp.StringValue.Trim())) { HasMissingValue = true; }
                p.Value = rp.StringValue;
                //
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
                        foreach (var tableid in tg.Item.TableIDs)
                        {
                            var tbl = Taxonomy.Tables.FirstOrDefault(i => i.ID == tableid);

                            if (tbl.InstanceFactsCount > 0) 
                            {
                                hasdata = true;
                                break;
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
                if (this.ID.Contains("1045"))
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

        private Dictionary<string, List<String>> _CellsOfFacts = new Dictionary<string, List<string>>();
        public Dictionary<string, List<String>> CellsOfFacts { get { return _CellsOfFacts; } set { _CellsOfFacts = value; } }
        public bool BindAsSequence { get; set; }

        public string Value { get; set; }


        internal void Clear()
        {
            this.Facts.Clear();
            this.CellsOfFacts.Clear();
        }


        internal SimpleValidationParameter Copy()
        {
            var sp = new SimpleValidationParameter();
            foreach (var item in _CellsOfFacts)
            {
                sp.CellsOfFacts.Add(item.Key, item.Value.ToArray().ToList());

            }
            sp.Facts.AddRange(_Facts);
            sp.Name = this.Name;
            sp.Value = this.Value;
            sp.BindAsSequence = this.BindAsSequence;
            return sp;
        }
    }

}
