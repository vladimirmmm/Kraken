using LogicalModel.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.Validation
{
    public static class Extensions 
    {
        public static bool In(this ValidationParameter obj, params String[] values)
        {
            var stringval = obj.StringValue;
            for (int i = 0; i < values.Length; i++)
            {
                if (String.Equals(stringval, values[i]))
                    return true;
            }
            return false;
        }
    }
    public class ValidationParameter
    {
        public string Name { get; set; }
        public string RuleID = null;

        private List<List<int>> _TaxFacts = new List<List<int>>();
        public List<List<int>> TaxFacts
        {
            get { return _TaxFacts; }
            set
            {
                _TaxFacts = value;

            }
        }

        private Dictionary<string, FactGroup> _FactGroups = new Dictionary<string, FactGroup>();
        public Dictionary<string, FactGroup> FactGroups
        {
            get { return _FactGroups; }
            set
            {
                _FactGroups = value;

            }
        }

        public void SetMyFactBase()
        {
            foreach (var key in FactGroups.Keys) 
            {
                var fg = FactGroups[key];
                fg.SetFromString(key);
            }
        }

        private string _FallBackValue ="0";
        [DefaultValue("0")]
        public string FallBackValue { get { return _FallBackValue; } set { _FallBackValue = value; } }
        private TypeEnum _Type = TypeEnum.String;
        public TypeEnum Type { get { return _Type; } set { _Type = value; } }
        public bool BindAsSequence { get; set; }

        public List<String> CurrentCells = new List<String>();
        public List<InstanceFact> CurrentFacts = new List<InstanceFact>();
        //public InstanceFact[] Facts
        //{
        //    get { return CurrentFacts.ToArray();  }
        //}
        public InstanceFact FirstFact 
        {
            get 
            {
                return CurrentFacts.FirstOrDefault();
            }

        }

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
                    typestring = Utilities.Linq.GetPropertyName((ValidationParameter i) => i.ValueWithTreshold);
                }
                if (this.Type == TypeEnum.Numeric && BindAsSequence)
                {
                    typestring = Utilities.Linq.GetPropertyName((ValidationParameter i) => i.ValuesWithTresholds);
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
        [JsonIgnore]
        public string StringValue
        {
            get
            {
                return String.IsNullOrEmpty(_StringValue) ? FallBackValue : _StringValue;
            }
            set { _StringValue = value; }

        }

        [JsonIgnore]
        public bool BooleanValue
        {
            get
            {
                return StringValue=="true";
            }

        }

        public decimal DecimalValue
        {
            get { return decimal.Parse(this.StringValue); }
        }

        public string[] StringValues = new string[] { };
        public decimal[] DecimalValues = new decimal[] { };
        
        //public ValidationParameter()
        //{
         
        //}

        public ValidationParameter(string name, string ruleID)
        {
            this.Name = name;
            this.RuleID = ruleID;
        }




        internal void Clear()
        {
            this.StringValue = "";
            this.StringValues = new string[] { };
            this.DecimalValues = new decimal[] { };
            this.CurrentFacts.Clear();
            this.CurrentCells.Clear();
        }

        public bool IsGeneral { get; set; }

        public override string ToString()
        {
            return String.Format("Parameter {0}: {1}", this.Name, this.StringValue);

        }

        public string[] Decimals = new string[] { };

        public ValueWithTreshold[] ValuesWithTresholds 
        {
            get {
                var trs =new List<ValueWithTreshold>();
                foreach (var fact in CurrentFacts) 
                {
                    var tr = Functions.GetValueWithTreshold(fact, this.FallBackValue);
                    trs.Add(tr);

                }
                return trs.ToArray();
            }
        }


        public ValueWithTreshold ValueWithTreshold
        {
            get
            {
                if (this.CurrentFacts.Count == 0) 
                {
                    return new ValueWithTreshold(this.DecimalValue, 0);
                }
                return ValuesWithTresholds.FirstOrDefault();
            }
        }

        public void ClearObjects()
        {
            foreach (var fg in FactGroups.Values) 
            {
                fg.ClearObjects();
                foreach (var fact in fg.Facts) 
                {
                    fact.ClearObjects();
                }
            }
        }
        public override bool Equals(object obj)
        {
            if (typeof(string) == (obj == null ? typeof(object) : obj.GetType())) 
            {
                return this.StringValue == String.Format("{0}", obj);
            }
            return base.Equals(obj);
        }

        #region boolean

        public static bool operator |(ValidationParameter lhs, bool rhs)
        {
            return lhs.BooleanValue | rhs;
        }
        public static bool operator |(ValidationParameter lhs, ValidationParameter rhs)
        {
            return lhs.BooleanValue | rhs.BooleanValue;
        }
        public static bool operator |(bool lhs, ValidationParameter rhs)
        {
            return lhs | rhs.BooleanValue;
        }

        public static bool operator &(ValidationParameter lhs, bool rhs)
        {
            return lhs.BooleanValue & rhs;
        }
        public static bool operator &(ValidationParameter lhs, ValidationParameter rhs)
        {
            return lhs.BooleanValue & rhs.BooleanValue;
        }
        public static bool operator &(bool lhs, ValidationParameter rhs)
        {
            return lhs & rhs.BooleanValue;
        }

        #endregion

        public static bool operator ==(ValidationParameter lhs, decimal rhs)
        {
            return Equals(lhs.DecimalValue, rhs);
        }
        public static bool operator !=(ValidationParameter lhs, decimal rhs)
        {
            return !Equals(lhs.DecimalValue, rhs);
        }
        public static bool operator ==(decimal lhs, ValidationParameter rhs)
        {
            return Equals(lhs, rhs.DecimalValue);
        }
        public static bool operator !=(decimal lhs, ValidationParameter rhs)
        {
            return !Equals(lhs, rhs.DecimalValue);
        }

        //> <
        public static bool operator >=(ValidationParameter lhs, decimal rhs)
        {
            return lhs.DecimalValue>= rhs;
        }
        public static bool operator <=(ValidationParameter lhs, decimal rhs)
        {
            return lhs.DecimalValue<= rhs;
        }
        public static bool operator >=(decimal lhs, ValidationParameter rhs)
        {
            return lhs >= rhs.DecimalValue;
        }
        public static bool operator <=(decimal lhs, ValidationParameter rhs)
        {
            return lhs <= rhs.DecimalValue;
        }

       
        public static bool operator >(ValidationParameter lhs, decimal rhs)
        {
            return lhs.DecimalValue>rhs;
        }
        public static bool operator <(ValidationParameter lhs, decimal rhs)
        {
            return lhs.DecimalValue< rhs;
        }
        public static bool operator >(decimal lhs, ValidationParameter rhs)
        {
            return lhs>rhs.DecimalValue;
        }
        public static bool operator <(decimal lhs, ValidationParameter rhs)
        {
            return lhs< rhs.DecimalValue;
        }
        //+ -
        public static decimal operator +(ValidationParameter lhs, decimal rhs)
        {
            return lhs.DecimalValue + rhs;
        }
        public static decimal operator -(ValidationParameter lhs, decimal rhs)
        {
            return lhs.DecimalValue - rhs;
        }
        public static decimal operator +(decimal lhs, ValidationParameter rhs)
        {
            return lhs + rhs.DecimalValue;
        }
        public static decimal operator -(decimal lhs, ValidationParameter rhs)
        {
            return lhs - rhs.DecimalValue;
        }

        //* /
        public static decimal operator *(ValidationParameter lhs, decimal rhs)
        {
            return lhs.DecimalValue * rhs;
        }
        public static decimal operator /(ValidationParameter lhs, decimal rhs)
        {
            return lhs.DecimalValue / rhs;
        }
        public static decimal operator *(decimal lhs, ValidationParameter rhs)
        {
            return lhs * rhs.DecimalValue;
        }
        public static decimal operator /(decimal lhs, ValidationParameter rhs)
        {
            return lhs / rhs.DecimalValue;
        }

        public static decimal operator *(ValidationParameter lhs, ValidationParameter rhs)
        {
            return lhs.DecimalValue * rhs.DecimalValue;
        }
        public static decimal operator /(ValidationParameter lhs, ValidationParameter rhs)
        {
            return lhs.DecimalValue / rhs.DecimalValue;
        }

        //== !=
        public static bool operator ==(ValidationParameter lhs, bool rhs)
        {
            return Equals(lhs.BooleanValue, rhs);
        }
        public static bool operator !=(ValidationParameter lhs, bool rhs)
        {
            return !Equals(lhs.BooleanValue, rhs);
        }
        public static bool operator ==(bool lhs, ValidationParameter rhs)
        {
            return Equals(lhs, rhs.BooleanValue);
        }
        public static bool operator !=(bool lhs, ValidationParameter rhs)
        {
            return !Equals(lhs, rhs.BooleanValue);
        }

        public static bool operator ==(ValidationParameter lhs, string rhs)
        {
            return Equals(lhs.StringValue, rhs);
        }
        public static bool operator !=(ValidationParameter lhs, string rhs)
        {
            return !Equals(lhs.StringValue, rhs);
        }
        public static bool operator ==(string lhs, ValidationParameter rhs)
        {
            return Equals(lhs, rhs.StringValue);
        }
        public static bool operator !=(string lhs, ValidationParameter rhs)
        {
            return !Equals(lhs, rhs.StringValue);
        }


        public static bool operator ==(ValidationParameter lhs, ValidationParameter rhs)
        {
            return Equals(lhs.StringValue, rhs.StringValue);
        }
        public static bool operator !=(ValidationParameter lhs, ValidationParameter rhs)
        {
            return !Equals(lhs.StringValue, rhs.StringValue);
        }

        public static decimal operator +(ValidationParameter lhs, ValidationParameter rhs)
        {
            return lhs.DecimalValue+ rhs.DecimalValue;
        }
        public static decimal operator -(ValidationParameter lhs, ValidationParameter rhs)
        {
            return lhs.DecimalValue - rhs.DecimalValue;
        }


        // >= <=
        public static bool operator >=(ValidationParameter lhs, ValidationParameter rhs)
        {
            //return !Equals(lhs.DecimalValue, rhs.DecimalValue) || !Equals(lhs.Treshold, rhs.Treshold);
            if (lhs.Type == rhs.Type) 
            {
                if (lhs.Type == TypeEnum.Numeric) { return lhs.DecimalValue >= rhs.DecimalValue; }      
            }
            return false;
        }
        public static bool operator <=(ValidationParameter lhs, ValidationParameter rhs)
        {
            if (lhs.Type == rhs.Type)
            {
                if (lhs.Type == TypeEnum.Numeric) { return lhs.DecimalValue <= rhs.DecimalValue; }
            }
            return false;
        }
        public static bool operator >(ValidationParameter lhs, ValidationParameter rhs)
        {
            if (lhs.Type == rhs.Type)
            {
                if (lhs.Type == TypeEnum.Numeric) { return lhs.DecimalValue > rhs.DecimalValue; }
            }
            return false;
        }
        public static bool operator <(ValidationParameter lhs, ValidationParameter rhs)
        {
            if (lhs.Type == rhs.Type)
            {
                if (lhs.Type == TypeEnum.Numeric) { return lhs.DecimalValue < rhs.DecimalValue; }
            }
            return false;
        }
    }
   
}
