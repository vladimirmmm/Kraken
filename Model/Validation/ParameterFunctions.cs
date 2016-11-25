using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.Validation
{
    public partial class Functions
    {
        public ValueWithTreshold IAF_N_Add(ValidationParameter a,ValidationParameter b)
        {
            return IAF_N_Add(a.ValueWithTreshold, b.ValueWithTreshold);
        }
        public bool IAF_N_Equals(ValidationParameter a, ValidationParameter b)
        {
            return IAF_N_Equals(a.ValueWithTreshold, b.ValueWithTreshold);
        }
        public bool IAF_N_Less(ValidationParameter a, ValidationParameter b)
        {
            return IAF_N_Less(a.ValueWithTreshold, b.ValueWithTreshold);
        }
        public bool IAF_N_LessEqual(ValidationParameter a, ValidationParameter b)
        {
            return IAF_N_LessEqual(a.ValueWithTreshold, b.ValueWithTreshold);
        }
        public bool IAF_N_Greater(ValidationParameter a, ValidationParameter b)
        {
            return IAF_N_Greater(a.ValueWithTreshold, b.ValueWithTreshold);
        }
        public bool IAF_N_GreaterEqual(ValidationParameter a, ValidationParameter b)
        {
            return IAF_N_GreaterEqual(a.ValueWithTreshold, b.ValueWithTreshold);
        }

        public ValueWithTreshold IAF_N_Subtract(ValidationParameter a, ValidationParameter b)
        {
            return IAF_N_Subtract(a.ValueWithTreshold, b.ValueWithTreshold); ;
        }
        public ValueWithTreshold IAF_N_Divide(ValidationParameter a, ValidationParameter b)
        {
            return IAF_N_Divide(a.ValueWithTreshold, b.ValueWithTreshold); ;
        }
        public IValueWithTreshold IAF_N_Multiply(ValidationParameter a, ValidationParameter b)
        {
            return IAF_N_Multiply(a.ValueWithTreshold, b.ValueWithTreshold);
        }
        public IValueWithTreshold IAF_N_Unary_Minus(ValidationParameter a)
        {
            return IAF_N_Unary_Minus(a.ValueWithTreshold);
        }
        public IValueWithTreshold IAF_N_Unary_Plus(ValidationParameter a)
        {
            return IAF_N_Unary_Plus(a.ValueWithTreshold);
        }

        public ValueWithTreshold IAF_abs(ValidationParameter a)
        {
            return IAF_abs(a.ValueWithTreshold);
        }
        public ValueWithTreshold IAF_sum(params ValidationParameter[] parameters)
        {
            var items = parameters.SelectMany(i => i.ValuesWithTresholds).ToArray();
            return IAF_sum(items);
        }
        public ValueWithTreshold IAF_max(params ValidationParameter[] parameters)
        {
            return IAF_max(parameters.SelectMany(i => i.ValuesWithTresholds.ToArray()));
        }
        public ValueWithTreshold IAF_min(params ValidationParameter[] parameters)
        {
            return IAF_min(parameters.SelectMany(i => i.ValuesWithTresholds.ToArray()));
        }
        






        public decimal N_Add(params ValidationParameter[] parameters)
        {
            return parameters.SelectMany(i=>i.DecimalValues).Sum();
        }
        public bool N_Equals(ValidationParameter a, ValidationParameter b)
        {
            return N_Equals(a.DecimalValue, b.DecimalValue);
        }
        public bool N_Less(ValidationParameter a, ValidationParameter b)
        {
            return N_Less(a.DecimalValue, b.DecimalValue);
        }
        public bool N_LessEqual(ValidationParameter a, ValidationParameter b)
        {
            return N_LessEqual(a.DecimalValue, b.DecimalValue);
        }
        public bool N_Greater(ValidationParameter a, ValidationParameter b)
        {
            return N_Greater(a.DecimalValue, b.DecimalValue);
        }
        public bool N_GreaterEqual(ValidationParameter a, ValidationParameter b)
        {
            return N_GreaterEqual(a.DecimalValue, b.DecimalValue);
        }

        public decimal N_Subtract(ValidationParameter a, ValidationParameter b)
        {
            return N_Subtract(a.DecimalValue, b.DecimalValue); ;
        }
        public decimal N_Divide(ValidationParameter a, ValidationParameter b)
        {
            return N_Divide(a.DecimalValue, b.DecimalValue); ;
        }
        public decimal N_Multiply(ValidationParameter a, ValidationParameter b)
        {
            return N_Multiply(a.DecimalValue, b.DecimalValue);
        }
        public decimal N_Unary_Minus(ValidationParameter a)
        {
            return N_Unary_Minus(a.DecimalValue);
        }
        public decimal N_Unary_Plus(ValidationParameter a)
        {
            return N_Unary_Plus(a.DecimalValue);
        }

        //public decimal abs(ValidationParameter a)
        //{
        //    return abs(a.DecimalValue);
        //}
        //public decimal sum(params ValidationParameter[] parameters)
        //{
        //    return parameters.SelectMany(i => i.DecimalValues).Sum();
        //}
        //public decimal max(params ValidationParameter[] parameters)
        //{
        //    return parameters.SelectMany(i => i.DecimalValues).Max();
        //}

        public string XS_QName(ValidationParameter a)
        {
            return XS_QName(a.StringValue);
        }
        public string XS_String(ValidationParameter a)
        {
            return XS_String(a.StringValue);
        }
        public int XS_Integer(ValidationParameter a)
        {
            return XS_Integer(a.StringValue);
        }
        public bool XS_Boolean(ValidationParameter a)
        {
            return XS_Boolean(a.StringValue);
        }

        public Boolean RegexpMatches(ValidationParameter a, string pattern)
        {
            return RegexpMatches(a.StringValue,pattern);
        }

        public bool Exists(ValidationParameter p)
        {

            return p.CurrentFacts.Count > 0;
        }

        public bool not(ValidationParameter a)
        {
            return not(a.BooleanValue);
        }

        public bool empty(ValidationParameter a)
        {
            return a.CurrentFacts.Count==0;
        }
        public decimal abs(ValidationParameter a)
        {
            return abs(a.DecimalValue);
        }
        public decimal sum(params ValidationParameter[] parameters)
        {
            return parameters.SelectMany(i=>i.DecimalValues).Sum();
        }
        public decimal max(params ValidationParameter[] parameters)
        {
            return parameters.SelectMany(i => i.DecimalValues).Max();
        }
        private decimal Invert(ValidationParameter a)
        {
            return Invert(a.DecimalValue);
        }

    }
}
