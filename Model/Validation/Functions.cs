using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.Validation
{
    public class Functions
    {
        public Expression Find(string name) 
        {
            if (this.ExpressionDictionary.ContainsKey(name)) 
            {
                return this.ExpressionDictionary[name];
            }
            return null;
        }

        public Dictionary<string, Expression> ExpressionDictionary = new Dictionary<string, Expression>();
        public Dictionary<string, string> FunctionMap = new Dictionary<string, string>();
      
        public Functions() 
        {


            AddFunction("iaf:numeric-equal", (decimal a, decimal b) => a == b);
            AddFunction("iaf:numeric-less-than", (decimal a, decimal b) => a < b);
            AddFunction("iaf:numeric-less-equal-than", (decimal a, decimal b) => a <= b);
            AddFunction("iaf:numeric-greater-than", (decimal a, decimal b) => a > b);
            AddFunction("iaf:numeric-greater-equal-than", (decimal a, decimal b) => a >= b);

            AddFunction("iaf:numeric-equal-treshold", (decimal a, decimal b) => a == b);
            AddFunction("iaf:numeric-less-than-treshold", (decimal a, decimal b) => a < b);
            AddFunction("iaf:numeric-less-equal-than-treshold", (decimal a, decimal b) => a <= b);
            AddFunction("iaf:numeric-greater-than-treshold", (decimal a, decimal b) => a > b);
            AddFunction("iaf:numeric-greater-equal-than-treshold", (decimal a, decimal b) => a >= b);

            AddFunction("iaf:numeric-subtract", (decimal a, decimal b) => a - b);
            AddFunction("iaf:numeric-divide", (decimal a, decimal b) => a / b);
            AddFunction("iaf:numeric-multiply", (decimal a, decimal b) => a * b);
            AddFunction("xs:qname", (String a) => a);
            AddFunction("not", (Boolean a) => !a);
            AddFunction("empty", (Object a) => a == null);
            AddFunction("abs", (decimal a) => a < 0 ? -a : a);
            AddFunction("sum", (decimal[] a) => sum(a));

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
        public void AddFunction<TOutput>(String Name, Expression<Func<Functions, TOutput>> expr)
        {
            this.FunctionMap.Add(Name, "functions."+Utilities.Linq.GetPropertyName(expr));
        }
        public void AddFunction<TParam, TOutput>(String Name, Expression<Func<TParam, TOutput>> expr)
        {
            this.ExpressionDictionary.Add(Name, expr);
        }
        public void AddFunction<TParam1, TParam2, TOutput>(String Name, Expression<Func<TParam1, TParam2, TOutput>> expr)
        {
            AddExpression(Name, expr);
        }
        public void AddFunction<TParam1, TParam2, TParam3, TOutput>(String Name, Expression<Func<TParam1, TParam2, TParam3, TOutput>> expr)
        {
            AddExpression(Name, expr);
        }
        public void AddFunction<TParam1, TParam2, TParam3, TParam4, TOutput>(String Name, Expression<Func<TParam1, TParam2, TParam3, TParam4, TOutput>> expr)
        {
            AddExpression(Name, expr);
            var item = expr.Compile();

        }

        public void AddExpression(String Name, Expression expr) 
        {
            this.ExpressionDictionary.Add(Name, expr);
        
        }
      

        private void Test() 
        {
            foreach (var f in ExpressionDictionary.Values) 
            {
                var s = f.ToString();

            }
        }
    }
}
