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
            foreach (var p in Parameters) 
            {
                //TODO
                var facts = instance.GetFacts(p.Facts.FirstOrDefault().GetFactString());
                if (facts.Count >0)
                {
                    p.StringValue = facts.FirstOrDefault().Value;
                }
            }
            result = Function(Parameters);
            return result;
        }


    }

    public class ValidationParameter 
    {
        public string Name { get; set; }
        public List<FactBase> Facts = new List<FactBase>();
        public string FallBackValue { get; set; }
        public string TypeString { get; set; }
        public bool BindAsSequence { get; set; }
        private string _StringValue = "";
        public string StringValue {
            get 
            {
                return String.IsNullOrEmpty(_StringValue) ? FallBackValue : _StringValue;
            }
            set { _StringValue=value; }
        
        }

        public double DoubleValue 
        {
            get { return double.Parse(this.StringValue); }
        }

        public ValidationParameter(string name) 
        {
            this.Name = name;
        }



    }

    public class ValidationParameter<T> : ValidationParameter 
    {
        public ValidationParameter(string name):base(name)
        {
            this.Name = name;
        }
        public T Value
        {
            get 
            {
                if (typeof(T) == typeof(double)) 
                {
                    Object o = double.Parse(this.StringValue);
                    return (T)o;
                }
                if (typeof(T) == typeof(string))
                {
                    Object o = this.StringValue;
                    return (T)o;
                }
                return default(T);
            }
        }
    }


    public enum TypeEnum 
    {
        Numeric,
        String,
        Date
    }
}
