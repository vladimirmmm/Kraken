using LogicalModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicalModel.Expressions;
using Utilities;

namespace LogicalModel.Validation
{
    public class ValidationFunctionContainer 
    {
        public Dictionary<String, Func<List<ValidationParameter>, bool>> FunctionDictionary = new Dictionary<string, Func<List<ValidationParameter>, bool>>();
        public Functions functions = new Functions();
    }

    public class ValidationRule
    {
        public String FunctionString= "";
        private Functions functions = null;
        public String ID { get; set; }
        public String FunctionName { get { return Utilities.Strings.AlfaNumericOnly(this.ID); } }
        public Expression RootExpression = null;
        public string LabelID = null;
        public Label Label = null;

        private List<FactBase> _Facts = new List<FactBase>();
        public List<FactBase> Facts { get { return _Facts; } set { _Facts = value; } }

        private Instance instance = null;

        public void Setinstance(Instance instance) {
            this.instance = instance;
        }

        private List<ValidationParameter> _Parameters = new List<ValidationParameter>();
        public List<ValidationParameter> Parameters { get { return _Parameters; } set { _Parameters = value; } }

        public Func<List<ValidationParameter>, bool> Function = null;

        public bool Validate(Instance instance) 
        {
            Setinstance(instance);
            var result = true;
            var factgroups = Parameters.FirstOrDefault().FactGroups;
            foreach (var factgroup in factgroups) 
            {
                
                foreach (var p in Parameters)
                {
                    p.Clear();
                    var parameterfactgroup = p.FactGroups.FirstOrDefault(i => i.GetFactKey() == factgroup.GetFactKey());
                    if (p.BindAsSequence)
                    {
                        if (p.TypeString == Utilities.Linq.GetPropertyName((ValidationParameter i) => i.StringValue))
                        {
                            p.StringValues = GetValues(parameterfactgroup.Facts).ToArray();
                        }
                        if (p.TypeString == Utilities.Linq.GetPropertyName((ValidationParameter i) => i.DoubleValue))
                        {
                            p.DoubleValues = GetValues(parameterfactgroup.Facts).Select(i => double.Parse(i)).ToArray();
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
                    var partialresult = Function(Parameters);
                    result = result && partialresult;
                    if (!partialresult) 
                    { 
                        Console.WriteLine(String.Format("Error {0}", this.Label.Content));
                    }
                }
            }
     
            return result;
        }

        public List<String> GetValues(List<FactBase> Facts) 
        {
            var values = new List<String>();
            foreach (var fact in Facts) 
            {
                var item = instance.FactDictionary[fact.GetFactKey()].FirstOrDefault();
                values.Add(item.Value);
            }
            return values;
        }


    }


}
