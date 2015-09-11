using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogicalModel.Validation
{
    public interface IValueWithTreshold 
    {
        object ObjectValue { get; set; }
        decimal DecimalValue { get; set; }
        decimal Treshold { get; set; }
    }

    public class ValueWithTreshold :IValueWithTreshold
    {
        private object _ObjectValue;
        public object ObjectValue
        {
            get
            {
                return _ObjectValue;
            }
            set
            {
                _ObjectValue = value;
            }
        }

        private decimal _DecimalValue;
        private decimal _Treshold;
        public decimal DecimalValue
        {
            get
            {
                return _DecimalValue;
            }
            set
            {
                _DecimalValue = value;
            }
        }

        public decimal Treshold
        {
            get
            {
                return _Treshold;
            }
            set
            {
                _Treshold = value;
            }
        }

        public ValueWithTreshold()
        {

        }
        public ValueWithTreshold(decimal Value)
        {
            _DecimalValue = Value;
        }
        public ValueWithTreshold(decimal Value, decimal Treshold)
        {
            _DecimalValue = Value;
            _Treshold = Treshold;

        }
        public static bool operator ==(ValueWithTreshold lhs, decimal rhs)
        {
            return Equals(lhs.DecimalValue, rhs);
        }
        public static bool operator !=(ValueWithTreshold lhs, decimal rhs)
        {
            return !Equals(lhs.DecimalValue, rhs);
        }
        public static bool operator ==(decimal lhs, ValueWithTreshold rhs)
        {
            return Equals(lhs, rhs.DecimalValue);
        }
        public static bool operator !=(decimal lhs, ValueWithTreshold rhs)
        {
            return !Equals(lhs, rhs.DecimalValue);
        }

        public static bool operator ==(ValueWithTreshold lhs, IValueWithTreshold rhs)
        {
            //return !Equals(lhs.DecimalValue, rhs.DecimalValue) || !Equals(lhs.Treshold, rhs.Treshold);
            return Equals(lhs.DecimalValue, rhs.DecimalValue);
        }
        public static bool operator !=(ValueWithTreshold lhs, IValueWithTreshold rhs)
        {
            //return !Equals(lhs.DecimalValue, rhs.DecimalValue) || !Equals(lhs.Treshold, rhs.Treshold);
            return !Equals(lhs.DecimalValue, rhs.DecimalValue);
        }

        public override string ToString()
        {
            return String.Format("{0}", ObjectValue);
        }
    }
    public partial class Functions
    {

        public Dictionary<string, Expression> ExpressionDictionary = new Dictionary<string, Expression>();
        public Dictionary<string, string> FunctionMap = new Dictionary<string, string>();
      
        public Functions() 
        {


        }
       
        


        public decimal N_Add(params decimal[] parameters)
        {
            return parameters.Sum();
        }
        public bool N_Equals(decimal a, decimal b) 
        {
            return a == b;
        }
        public bool N_Less(decimal a, decimal b)
        {
            return a < b;
        }
        public bool N_LessEqual(decimal a, decimal b)
        {
            return a <= b;
        }
        public bool N_Greater(decimal a, decimal b)
        {
            return a > b;
        }
        public bool N_GreaterEqual(decimal a, decimal b)
        {
            return a >= b;
        }

        public bool N_Equals_Treshold(decimal a, decimal b)
        {
            return a == b;
        }
        public bool N_Less_Treshold(decimal a, decimal b)
        {
            return a < b;
        }
        public bool N_LessEqual_Treshold(decimal a, decimal b)
        {
            return a <= b;
        }
        public bool N_Greater_Treshold(decimal a, decimal b)
        {
            return a > b;
        }
        public bool N_GreaterEqual_Treshold(decimal a, decimal b)
        {
            return a >= b;
        }

        public decimal N_Subtract(decimal a, decimal b)
        {
            return a - b;
        }
        public decimal N_Divide(decimal a, decimal b)
        {
            return a / b;
        }
        public decimal N_Multiply(decimal a, decimal b)
        {
            return a * b;
        }
        public decimal N_Unary_Minus(decimal a)
        {
            return -a;
        }
        public decimal N_Unary_Plus(decimal a)
        {
            return a;
        }
        public string XS_QName(String a)
        {
            return a;
        }
        public string XS_String(String a)
        {
            return a;
        }

        public Boolean RegexpMatches(string a, string pattern)
        {
            var regexp = new Regex(pattern);
            return regexp.Matches(a).Count > 1;
        }
    
        //public bool Exists(string v)
        //{

        //    return !String.IsNullOrEmpty(v);
        //}

        public bool not(bool a)
        {
            return !a;
        }

        public bool empty(object a)
        {
            return a==null;
        }
        public decimal abs(decimal a)
        {
            return a < 0 ? -a : a;
        }
        public decimal sum(params decimal[] parameters) 
        {
            return parameters.Sum();
        }
        public decimal max(params decimal[] parameters)
        {
            return parameters.Sum();
        }
        private decimal Invert(decimal a) 
        {
            return -a;
        }

      

    }
}
