﻿using LogicalModel.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace LogicalModel.Validation
{
    public static class Extensions 
    {
        public static bool In(this ValidationParameter obj, params String[] values)
        {
            foreach (var stringval in obj.StringValues)
            {
                var subresult = false;
                for (int i = 0; i < values.Length; i++)
                {
                    if (String.Equals(stringval, values[i]))
                    {
                        subresult = true;
                        break;
                    }
                }
                if (!subresult) 
                {
                    return false;
                }
            }
            return true;
        }
    }
    public class test
    {
        public void testparameter() 
        {
            var p = new ValidationParameter("p","12");
            p.Type = TypeEnum.String;
            var s = Utilities.Converters.ToJson(p);
            var px = Utilities.Converters.JsonTo<ValidationParameter>(s);
        }
    }
    public class ValidationParameter
    {
        public string Name { get; set; }
        public string RuleID = null;
        [JsonIgnore]
        public FactBaseQuery BaseQuery = null;
        [JsonIgnore]
        public IList<int> Data = null;
        public string Concept = "";
        
        private List<List<int>> _TaxFacts = new List<List<int>>();
        public List<List<int>> TaxFacts
        {
            get { return _TaxFacts; }
            set
            {
                _TaxFacts = value;

            }
        }
        
        /*
        private List<IntervalList> _TaxFacts = new List<IntervalList>();
        public List<IntervalList> TaxFacts
        {
            get { return _TaxFacts; }
            set
            {
                _TaxFacts = value;

            }
        }
        */
        private string _FallBackValue ="0";
        [DefaultValue("0")]
        public string FallBackValue
        {
            get { return _FallBackValue; }
            set
            {
                if (value != "non")
                {
                    _FallBackValue = value;
                }
            }
        }
        private TypeEnum _Type = TypeEnum.Unknown;
        public TypeEnum Type
        {
            get { return _Type; }
            set
            {
                _Type = value;
            }
        }
        public bool BindAsSequence { get; set; }

        private List<int> _TypedDimensions = new List<int>();
        public List<int> TypedDimensions { get { return _TypedDimensions; } set { _TypedDimensions = value; } }

        private List<int> _CoveredParts = new List<int>();
        public List<int> CoveredParts { get { return _CoveredParts; } set { _CoveredParts = value; } }


        //public List<String> CurrentCells = new List<String>();
        public List<InstanceFact> CurrentFacts = new List<InstanceFact>();



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
        //[JsonIgnore]
        public string StringValue
        {
            get
            {
                return String.IsNullOrEmpty(_StringValue) ? FallBackValue : _StringValue;
            }
            set { _StringValue = value; }

        }
        public string OriginalStringValue { get { return _StringValue; } }

        [JsonIgnore]
        public bool BooleanValue
        {
            get
            {
                return Utilities.ObjectExtensions.In(StringValue,"true","1");
            }

        }

        public decimal DecimalValue
        {
            get { return LogicalModel.Validation.Functions.GetNumber(this.StringValue); }
        }
        public DateTime DateValue
        {
            get { return Utilities.Converters.StringToDateTime(this.StringValue,Utilities.Converters.DateTimeFormat); }
        }

        public string[] StringValues = new string[] { };
        public decimal[] DecimalValues = new decimal[] { };
        

        public ValidationParameter(string name, string ruleID)
        {
            this.Name = name;
            this.RuleID = ruleID;
            this.Type = TypeEnum.Unknown;
        }

        public void AddTaxFacts(IList<int> list)
        {
            IntervalList intervals = list as IntervalList;
            if (intervals == null) 
            {
                intervals = new IntervalList();
                foreach (var item in list) 
                {
                    intervals.Add(item);
                }
            }
            //this.TaxFacts.Add(intervals);
            this.TaxFacts.Add((List<int>)list);
        }


        internal void Clear()
        {
            this.StringValue = "";
            this.StringValues = new string[] { };
            this.DecimalValues = new decimal[] { };
            this.CurrentFacts.Clear();
            //this.CurrentCells.Clear();
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

        }
        public override bool Equals(object obj)
        {
            if (this.StringValues.Length > 1) 
            {
                return SequenceEquals(obj);
            }
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

        public bool SequenceEquals(object obj) 
        {
            var result = false;
            foreach (var stringval in this.StringValues) 
            {
                if (this.Type == TypeEnum.Boolean) 
                {
                    var bval = Utilities.ObjectExtensions.In(stringval, "true", "1");
                    if (bval.Equals(obj)) 
                    {
                        return true;
                    }
                }
                if (this.Type != TypeEnum.Boolean)
                {
                    if (stringval.Equals(String.Format("{0}", obj)))
                    {
                        return true;
                    }
                }
            }
            return result;
        }

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
                if (lhs.Type == TypeEnum.Date) { return lhs.DateValue >= rhs.DateValue; }      
            }
            return false;
        }
        public static bool operator <=(ValidationParameter lhs, ValidationParameter rhs)
        {
            if (lhs.Type == rhs.Type)
            {
                if (lhs.Type == TypeEnum.Numeric) { return lhs.DecimalValue <= rhs.DecimalValue; }
                if (lhs.Type == TypeEnum.Date) { return lhs.DateValue <= rhs.DateValue; }
            }
            return false;
        }
        public static bool operator >(ValidationParameter lhs, ValidationParameter rhs)
        {
            if (lhs.Type == rhs.Type)
            {
                if (lhs.Type == TypeEnum.Numeric) { return lhs.DecimalValue > rhs.DecimalValue; }
                if (lhs.Type == TypeEnum.Date) { return lhs.DateValue > rhs.DateValue; }
            }
            return false;
        }
        public static bool operator <(ValidationParameter lhs, ValidationParameter rhs)
        {
            if (lhs.Type == rhs.Type)
            {
                if (lhs.Type == TypeEnum.Numeric) { return lhs.DecimalValue < rhs.DecimalValue; }
                if (lhs.Type == TypeEnum.Date) { return lhs.DateValue < rhs.DateValue; }
            }
            return false;
        }
    }
   
}
