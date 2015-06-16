using LogicalModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.Validation
{
    public class ValidationParameter
    {
        public string Name { get; set; }
        private List<FactGroup> _FactGroups = new List<FactGroup>();
        public List<FactGroup> FactGroups { get { return _FactGroups; } set { _FactGroups = value; } }
        public string FallBackValue { get; set; }
        private TypeEnum _Type = TypeEnum.String;
        public TypeEnum Type { get { return _Type; } set { _Type = value; } }
        public bool BindAsSequence { get; set; }

        public List<FactBase> CurrentFacts = new List<FactBase>();

        public string TypeString
        {
            get
            {
                var typestring = Utilities.Linq.GetPropertyName((ValidationParameter i) => i.StringValue);
                if (this.Type == TypeEnum.String && BindAsSequence) 
                {
                    typestring = Utilities.Linq.GetPropertyName((ValidationParameter i) => i.StringValues);
                }
                if (this.Type == TypeEnum.Numeric && !BindAsSequence)
                {
                    typestring = Utilities.Linq.GetPropertyName((ValidationParameter i) => i.DecimalValue);
                }
                if (this.Type == TypeEnum.Numeric && BindAsSequence)
                {
                    typestring = Utilities.Linq.GetPropertyName((ValidationParameter i) => i.DecimalValues);
                }
                if (this.Type == TypeEnum.Date && !BindAsSequence)
                {
                }
                if (this.Type == TypeEnum.Date && BindAsSequence)
                {
                }
                return typestring;
            }
        }
        
        
        private string _StringValue = "";

        public string StringValue
        {
            get
            {
                return String.IsNullOrEmpty(_StringValue) ? FallBackValue : _StringValue;
            }
            set { _StringValue = value; }

        }

        public decimal DecimalValue
        {
            get { return decimal.Parse(this.StringValue); }
        }

        public string[] StringValues = new string[] { };
        public decimal[] DecimalValues = new decimal[] { };
     

        public ValidationParameter(string name)
        {
            this.Name = name;
        }




        internal void Clear()
        {
            this.StringValue = "";
            this.StringValues = new string[] { };
            this.DecimalValues = new decimal[] { };
        }

        public bool IsGeneral { get; set; }
    }

    public class ValidationParameter<T> : ValidationParameter
    {
        public ValidationParameter(string name)
            : base(name)
        {
            this.Name = name;
        }
        public T Value
        {
            get
            {
                if (typeof(T) == typeof(decimal))
                {
                    Object o = decimal.Parse(this.StringValue);
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
        Date,
    }
}
