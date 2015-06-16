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
                    var factsofconcept = instance.Facts.Where(i => i.Concept == firstfact.Concept.Content).ToList();
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

            //

            //var allinstancefacts = 
            if (this.ID.Contains("4012"))
            {
            }
            foreach (var factgroup in factgroups) 
            {
                //var firstfact = factgroup.Facts.FirstOrDefault();
                factgroup.FactString = factgroup.GetFactString();

                var HasAtLeastOneValue = false;
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
                        foreach (var instancefact in instancefacts) 
                        {
                            instancefact.SetFromString(instancefact.FactString);
                        }
                        foreach (var dimension in factgroup.Dimensions)
                        {
                            instancefacts = instancefacts.Where(i => i.Dimensions.Any(j => j.DomainMemberFullName == dimension.DomainMemberFullName)).ToList();
                        }

                        p.CurrentFacts = instancefacts.Cast<FactBase>().ToList();
                        var stringvalues = instancefacts.Select(i => i.Value);
                        if (p.Type == TypeEnum.String)
                        {
                            p.StringValues = stringvalues.ToArray(); //GetValues(parameterfactgroup.Facts).ToArray();
                            p.StringValue = Utilities.Strings.ArrayToString(p.StringValues);
                        }
                        if (p.Type == TypeEnum.Numeric)
                        {
                            p.DecimalValues = stringvalues.Select(i => decimal.Parse(i)).ToArray();// GetValues(parameterfactgroup.Facts).Select(i => double.Parse(i)).ToArray();
                            p.StringValue = Utilities.Strings.ArrayToString(p.DecimalValues);

                        }
                    }
                    else 
                    {
                        var parameterfactgroup = p.FactGroups.Count == 1 ? p.FactGroups.FirstOrDefault() : p.FactGroups.FirstOrDefault(i => i.GetFactString() == fg_factstring);
                        p.CurrentFacts = parameterfactgroup.Facts;
                        if (parameterfactgroup.Facts.Count > 1)
                        {
                            Console.WriteLine("Issue with " + this.ID + " parameter " + p.Name);
                        }
                        else 
                        {
                            var fact = parameterfactgroup.Facts.FirstOrDefault();
                            var instancefacts = instance.GetFacts(fact.GetFactKey());
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
                                HasAtLeastOneValue = true;
                                InstanceFact realfact = null;
                                if (instancefacts.Count == 1)
                                {
                                    realfact = instancefacts.FirstOrDefault();
                                    p.StringValue = realfact.Value;
                                }
                                else
                                {
                                    //dynamic reports

                                    foreach (var dimension in factgroup.Dimensions) 
                                    {
                                        instancefacts = instancefacts.Where(i => i.Dimensions.Any(j => j.DomainMemberFullName == dimension.DomainMemberFullName)).ToList();
                                    }
                                    realfact = instancefacts.FirstOrDefault();
                                    if (realfact != null)
                                    {
                                        p.StringValue = realfact.Value;
                                    }
                                }
                            }
                        }
                    }
                    //var skiprule = (Parameters.Any(i => String.IsNullOrEmpty(i.FallBackValue) && String.IsNullOrEmpty(i.StringValue))) ;
                    
                  
                }
            
                if (HasAtLeastOneValue)
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
                            sp.Facts = item.CurrentFacts.Select(i => i.GetFactKey()).ToList();
                            foreach (var f in sp.Facts)
                            {
                                if (Taxonomy.Facts.ContainsKey(f))
                                {
                                    sp.Cells.AddRange(Taxonomy.Facts[f]);
                                }
                            }
                            v.Parameters.Add(sp);
                        }

                        //Console.WriteLine(String.Format("Error: {0}", v.GetDetails()));

                        results.Add(v);
                    }
                }
            }
            if (results.Count > 0)
            {
                var sb = new StringBuilder();
                sb.AppendLine(String.Format("Error: {0}", this.DisplayText));
                sb.AppendLine(String.Format("Test : {0}", this.OriginalExpression));

                foreach (var result in results)
                {
                    sb.AppendLine(result.GetDetails());

                }
                sb.AppendLine();
                Console.WriteLine(sb.ToString());
                Console.WriteLine("");
            }
            return results;
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
