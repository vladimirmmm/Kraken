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
    public class SimpleValidationRule
    {
        public String ID { get; set; }
        public String DisplayText { get; set; }
        public String OriginalExpression { get; set; }

        public SimpleValidationRule() 
        {

        }
        public SimpleValidationRule(ValidationRule rule)
        {
            this.ID = rule.ID;
            this.DisplayText = rule.DisplayText;
            this.OriginalExpression = rule.OriginalExpression;
        }

    }
    public class ValidationRule
    {
        protected Taxonomy Taxonomy = null;
        public String FunctionString= "";
        public String ID { get; set; }
        public String FunctionName { get { return Utilities.Strings.AlfaNumericOnly(this.ID); } }
        public Expression RootExpression = null;

        public string OriginalExpression { get; set; }
        
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
        public string DisplayText 
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
        
        public List<ValidationRuleResult> Validate(Instance instance) 
        {
            if (this.ID.Contains("0312"))
            {
            }
            Setinstance(instance);
            var results = new List<ValidationRuleResult>();
            var factgroups = Parameters.FirstOrDefault().FactGroups;
            if (factgroups.Count == 1) 
            {
                var factgroup = factgroups.FirstOrDefault();
                var hastyped = factgroup.Facts.Any(i => i.Dimensions.Any(j => j.IsTyped));
                if (hastyped) {
                    var p = Parameters.FirstOrDefault();
                    var parameterfactgroup = p.FactGroups.FirstOrDefault(i => i.GetFactKey() == factgroup.GetFactKey());
                    var fact = parameterfactgroup.Facts.FirstOrDefault();
                    var instancefacts = instance.GetFacts(fact.GetFactKey());
                    factgroups.Clear();
                    foreach (var instancefact in instancefacts) 
                    {
                        var fg = new FactGroup();
                        if (instancefact.Dimensions.Count == 0) 
                        {
                            instancefact.SetFromString(instancefact.FactString);
                        }
                        fg.Dimensions.AddRange(instancefact.Dimensions.Where(
                            i=>factgroup.Dimensions.Any(j=>j.ToStringForKey()==i.ToStringForKey())));
                        fg.Concept=factgroup.Concept;
                        fg.Facts.Add(instancefact);
                        factgroups.Add(fg);
                    }
                }
                var firstfact =factgroup.Facts.FirstOrDefault(); 
                if (factgroup.Facts.Count == 1 && firstfact.Dimensions.Count == 0) 
                {
                    if (firstfact.Concept == null) { 

                    }
                    else
                    {
                        var factsofconcept = instance.Facts.Where(i => i.Concept.Content == firstfact.Concept.Content).ToList();
                        foreach (var instancefact in factsofconcept)
                        {
                            var fg = new FactGroup();
                            if (instancefact.Dimensions.Count == 0)
                            {
                                instancefact.SetFromString(instancefact.FactString);
                            }
                            fg.Dimensions.AddRange(instancefact.Dimensions);
                            fg.Concept = factgroup.Concept;
                            fg.Facts.Add(instancefact);
                            factgroups.Add(fg);
                        }
                    }
                }

            }

            //

            //var allinstancefacts = 
         
            foreach (var factgroup in factgroups) 
            {
                //var firstfact = factgroup.Facts.FirstOrDefault();
                factgroup.FactString = factgroup.GetFactString();

                var HasAtLeastOneValue = false;
                var HasMissingValue = false;
                foreach (var p in Parameters)
                {
                    if (p.IsGeneral) 
                    {
                        continue;
                    }
                    p.Clear();

                    //var parameterfactgroup = p.FactGroups.FirstOrDefault(i => i.GetFactKey() == factgroup.GetFactKey());
                    var fg_factstring = factgroup.FactString;

              
                    if (p.BindAsSequence)
                    {
                        var parameterfactgroup = p.FactGroups.FirstOrDefault();//i => i.GetFactString() == factgroup.GetFactKey());
                        var instancefacts = GetInstanceFacts(parameterfactgroup.Facts);
                        //set the cells
                        var cells = new List<String>();
                        p.CurrentCells.Clear();
                        foreach (var tax_fact in parameterfactgroup.Facts) 
                        {
                            var taxfactkey = tax_fact.GetFactKey();
                            if (Taxonomy.Facts.ContainsKey(taxfactkey))
                            {
                                cells.AddRange(Taxonomy.Facts[taxfactkey]);
                            }
                        }
                        p.CurrentCells.AddRange(cells);
                        //

                        foreach (var instancefact in instancefacts) 
                        {
                            instancefact.SetFromString(instancefact.FactString);
                        }
                        foreach (var dimension in factgroup.Dimensions)
                        {
                            instancefacts = instancefacts.Where(i => i.Dimensions.Any(j => j.DomainMemberFullName == dimension.DomainMemberFullName)).ToList();
                        }

                        p.CurrentFacts = instancefacts;
                        var stringvalues = instancefacts.Select(i => i.Value);
                        var decimals = instancefacts.Select(i => i.Decimals).ToArray();
                        if (p.Type == TypeEnum.String)
                        {
                            p.StringValues = stringvalues.ToArray(); //GetValues(parameterfactgroup.Facts).ToArray();
                            p.StringValue = Utilities.Strings.ArrayToString(p.StringValues);
                        }
                        if (p.Type == TypeEnum.Numeric)
                        {
                            p.DecimalValues = stringvalues.Select(i => decimal.Parse(i)).ToArray();// GetValues(parameterfactgroup.Facts).Select(i => double.Parse(i)).ToArray();
                            p.StringValue = Utilities.Strings.ArrayToString(p.DecimalValues);
                            p.Decimals = decimals;


                        }
                    }
                    else 
                    {
                        var parameterfactgroup = p.FactGroups.Count == 1 ? p.FactGroups.FirstOrDefault() : p.FactGroups.FirstOrDefault(i => i.GetFactString() == fg_factstring);
                        if (parameterfactgroup.Facts.Count > 1)
                        {
                            Console.WriteLine("Issue with " + this.ID + " parameter " + p.Name);
                        }
                        else 
                        {
                            var fact = parameterfactgroup.Facts.FirstOrDefault();
                            var factkey = fact.GetFactKey();

                            //set the cells
                            var cells = new List<String>();
                            p.CurrentCells.Clear();
                            if (Taxonomy.Facts.ContainsKey(factkey))
                            {
                                cells.AddRange(Taxonomy.Facts[factkey]);
                            }                 
                            p.CurrentCells.AddRange(cells);
                            //

                            var instancefacts = instance.GetFacts(factkey);
                            foreach (var instancefact in instancefacts)
                            {
                                instancefact.SetFromString(instancefact.FactString);
                            }
                            if (instancefacts.Count == 0)
                            {
                                //fact is not present in the instance
                            }
                            else
                            {
                                p.CurrentFacts = instancefacts;

                                HasAtLeastOneValue = true;
                                InstanceFact realfact = null;
                                if (instancefacts.Count == 1 && !instancefacts.FirstOrDefault().HasTypedDimension())
                                {
                                    realfact = instancefacts.FirstOrDefault();
                                    p.StringValue = realfact.Value;
                                    p.Decimals = new string[] { realfact.Decimals };
                                }
                                else
                                {
                                    //dynamic reports

                                    foreach (var dimension in factgroup.Dimensions) 
                                    {
                                        instancefacts = instancefacts.Where(i => i.Dimensions.Any(j => j.DomainMemberFullName == dimension.DomainMemberFullName)).ToList();
                                    }
                                    realfact = instancefacts.FirstOrDefault();
                                    p.CurrentFacts = instancefacts;
    
                                    if (realfact != null)
                                    {
                                        for (int i = 0; i < p.CurrentCells.Count; i++)
                                        {
                                            p.CurrentCells[i] = instance.GetDynamicCellID(p.CurrentCells[i], realfact);

                                        }
                                        p.StringValue = realfact.Value;
                                    }
                                }

                                //		Decimal.MaxValue	79228162514264337593543950335	decimal
                                if (p.Type == TypeEnum.Numeric) 
                                {
                                    if (p.StringValue.Length>29 ||!Utilities.Strings.IsDigitsOnly(p.StringValue,'.','-')) 
                                    {
                                        Console.WriteLine(String.Format("Invalid Value Detected: {0}", p.StringValue));
                                        p.StringValue = "";
                                    }
                                }
                            }
                        }
                    }
                    if (String.IsNullOrEmpty(p.StringValue.Trim())) { HasMissingValue = true; }
                    //var skiprule = (Parameters.Any(i => String.IsNullOrEmpty(i.FallBackValue) && String.IsNullOrEmpty(i.StringValue))) ;
     
                 
                }

                if (HasAtLeastOneValue && !HasMissingValue)
                {
                    var partialresult = Function(Parameters);
                    if (!partialresult)
                    {
                        var v = new ValidationRuleResult();
                        v.ID = this.ID;
                        foreach (var item in Parameters)
                        {
                            var sp = new SimlpeValidationParameter();
                            sp.Name = item.Name;
                            sp.Value = item.StringValue;
                            //sp.Facts = item.CurrentFacts.Select(i => i.GetFactKey()).ToList();
                            sp.Facts = item.CurrentFacts.Select(i => i.GetFactString()).ToList();
                            sp.Cells.AddRange(item.CurrentCells);
                            if (sp.Cells.Count == 0)
                            {

                            }
                            v.Parameters.Add(sp);
                        }

                        //Console.WriteLine(String.Format("Error: {0}", v.GetDetails()));

                        results.Add(v);
                    }
                }
            }
            return results;
        }

        public override string ToString()
        {
            return String.Format("ValidationRule {0}",this.ID);
        }


    }
    public class SimlpeValidationParameter 
    {
        public string Name { get; set; }
        private List<String> _Facts = new List<String>();
        public List<String> Facts { get { return _Facts; } set { _Facts = value; } }

        private List<String> _Cells = new List<string>();
        public List<String> Cells { get { return _Cells; } set { _Cells = value; } }

        public string Value { get; set; }

    }
    public class ValidationRuleResult 
    {
        public bool IsOk = true;
        public string ID { get; set; }
        //public Dictionary<String, List<String>> ParameterFactDictionary = new Dictionary<string, List<string>>();
        private List<SimlpeValidationParameter> _Parameters = new List<SimlpeValidationParameter>();
        public List<SimlpeValidationParameter> Parameters { get { return _Parameters; } set { _Parameters = value; } }
        public string Message = "";

        public string GetDetails ()
        {
            var sb = new StringBuilder();
            //sb.AppendLine(Message);
            sb.Append("    ");
            foreach (var p in Parameters)
            {
                sb.AppendFormat("{0}({1}), ", p.Name, p.Value);

            }
            sb.AppendLine();
            sb.Append("    ");

            foreach (var p in Parameters) 
            {
                sb.AppendFormat("{0}[", p.Name);
                foreach (var item in p.Facts)
                {
                    sb.Append(item + ", ");
                }
                sb.Append("], ");

            }
            sb.AppendLine();

            return sb.ToString();
        }

        public override string ToString()
        {
            return String.Format("{0} - {1}", this.GetType().Name, this.ID);
        }
    }

}
