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
                if (String.IsNullOrEmpty(_DisplayText) && _Label != null) 
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
        
        public List<ValidationRuleResult> Validate(Instance instance) 
        {
            Setinstance(instance);
            var results = new List<ValidationRuleResult>();
            var factgroups = Parameters.FirstOrDefault().FactGroups;
            foreach (var factgroup in factgroups) 
            {
   
                foreach (var p in Parameters)
                {
                    if (p.IsGeneral) 
                    {
                        continue;
                    }
                    p.Clear();
              
                    var parameterfactgroup = p.FactGroups.FirstOrDefault(i => i.GetFactKey() == factgroup.GetFactKey());
                    p.CurrentFacts = parameterfactgroup.Facts;
                    if (p.BindAsSequence)
                    {
                        if (p.Type == TypeEnum.String)
                        {
                            p.StringValues = GetValues(parameterfactgroup.Facts).ToArray();
                            p.StringValue = Utilities.Strings.ArrayToString(p.StringValues);
                        }
                        if (p.Type == TypeEnum.Numeric)
                        {
                            p.DoubleValues = GetValues(parameterfactgroup.Facts).Select(i => double.Parse(i)).ToArray();
                            p.StringValue = Utilities.Strings.ArrayToString(p.DoubleValues);

                        }
                    }
                    else 
                    {
                        if (parameterfactgroup.Facts.Count > 1)
                        {
                            Console.WriteLine("Issue with " + this.ID + " parameter " + p.Name);
                        }
                        else 
                        {
                            var fact = parameterfactgroup.Facts.FirstOrDefault();
                            var insatncefacts = instance.GetFacts(fact.GetFactKey());
                            if (insatncefacts.Count == 0)
                            {
                                //fact is not present in the instance
                            }
                            else
                            {
                                if (insatncefacts.Count == 1)
                                {
                                    p.StringValue = insatncefacts.FirstOrDefault().Value;
                                }
                                else
                                {
                                    //dynamic reports


                                }
                            }
                        }
                    }
                    var skiprule = (Parameters.Any(i => String.IsNullOrEmpty(i.FallBackValue) && String.IsNullOrEmpty(i.StringValue))) ;
                    if (!skiprule)
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
                                sp.Facts = item.CurrentFacts;
                                v.Parameters.Add(sp);
                            }

                            //Console.WriteLine(String.Format("Error: {0}", v.GetDetails()));

                            results.Add(v);
                        }
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
        public List<FactBase> Facts = new List<FactBase>();
        public string Value { get; set; }

    }
    public class ValidationRuleResult 
    {
        public bool IsOk = true;
        public string ID = "";
        //public Dictionary<String, List<String>> ParameterFactDictionary = new Dictionary<string, List<string>>();
        public List<SimlpeValidationParameter> Parameters = new List<SimlpeValidationParameter>();
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
