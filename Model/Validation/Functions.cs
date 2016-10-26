using System;
using System.Collections.Generic;
using System.Globalization;
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
        public string QName(String ns,String n)
        {
            var nsitem = Utilities.Xml.Namespaces.FirstOrDefault(i => i.Value == ns);
            if ((object)nsitem == null) { return ""; }
            return string.Format("{0}:{1}", nsitem.Key, n);
        }
        public string String(Object a)
        {
            return string.Format("{0}", a);
        }
        public string String(ValidationParameter a)
        {
            return a.StringValue;
        }
        public string XS_String(String a)
        {
            return a;
        }
        public DateTime XS_Date(object a)
        {
            return (DateTime)a;
        }
        public DateTime XS_Date(String a)
        {
            return Utilities.Converters.StringToDateTime(a, "yyyy-MM-dd");
        }
        public DateTime XS_Date(ValidationParameter a)
        {
            return a.DateValue;
        }
        public bool XS_Boolean(String a)
        {
            return bool.Parse(a);
        }
        public Boolean RegexpMatches(string a, string pattern)
        {
            var regexp = new Regex(pattern);
            //return regexp.Matches("^" + a + "$").Count > 0;
            return regexp.Matches(a).Count > 0;
        }
    
        //public bool Exists(string v)
        //{

        //    return !String.IsNullOrEmpty(v);
        //}

        public bool not(bool a)
        {
            return !a;
        }
        public int Day(ValidationParameter dt)
        {
            return dt.DateValue.Day;
        }
        public int Month(ValidationParameter dt)
        {
            return dt.DateValue.Month;
        }
        public int Year(ValidationParameter dt)
        {
            return dt.DateValue.Year;
        }
        public int Day(DateTime dt) 
        {
            return dt.Day;
        }
        public int Month(DateTime dt)
        {
            return dt.Month;
        }
        public int Year(DateTime dt)
        {
            return dt.Year;
        }
        public int Day(DateTime? dt)
        {
            return dt.HasValue ? dt.Value.Day : 0;
        }
        public int Month(DateTime? dt)
        {
            return dt.HasValue ? dt.Value.Month : 0;
        }
        public int Year(DateTime? dt)
        {
            return dt.HasValue ? dt.Value.Year : 0;
        }
        public int StringLength(string s) 
        {
            return s.Length;
        }
        public string Substring(ValidationParameter s, decimal startix, decimal count) 
        {
            return s.StringValue.Substring((int)startix, (int)count);
        }
        public int StringLength(ValidationParameter s)
        {
            return s.StringValue.Length;
        }
        public string Substring(string s, decimal startix, decimal count)
        {
            return s.Substring((int)startix, (int)count);
        }

        public string Concat(params object[] args) 
        {
            return string.Concat(args);
        }
        public bool empty(object a)
        {
            return a==null;
        }
        public decimal abs(decimal a)
        {
            return a < 0 ? -a : a;
        }
        public decimal floor(decimal a)
        {
            return Math.Floor(a);
        }
        public decimal floor(ValidationParameter a)
        {
            return Math.Floor(a.DecimalValue);
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
        public static decimal Number(object p) 
        {
            var n = string.Format("{0}", p);
            if (Utilities.Strings.IsDigitsOnly(n, '.', '-')) 
            {
                return decimal.Parse(n, new NumberFormatInfo() { NumberDecimalSeparator = "." });
            }
            return 0;
        }
        public string Translate(string text, string characterstosearch, string charactersreplacement) 
        {
            if (charactersreplacement.Length == characterstosearch.Length) 
            {
                for (int i = 0; i < characterstosearch.Length; i++) 
                {
                    var a = characterstosearch[i];
                    var b = charactersreplacement[i];
                    text = text.Replace(a, b);
                }
            }
            return text;
        }
        //public string Concat(params )
      

    }
}
