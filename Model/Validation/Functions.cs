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


            AddFunction("iaf:numeric-equal", (Double a, Double b) => a == b);
            AddFunction("iaf:numeric-less-than", (Double a, Double b) => a < b);
            AddFunction("iaf:numeric-less-equal-than", (Double a, Double b) => a <= b);
            AddFunction("iaf:numeric-greater-than", (Double a, Double b) => a > b);
            AddFunction("iaf:numeric-greater-equal-than", (Double a, Double b) => a >= b);

            AddFunction("iaf:numeric-equal-treshold", (Double a, Double b) => a == b);
            AddFunction("iaf:numeric-less-than-treshold", (Double a, Double b) => a < b);
            AddFunction("iaf:numeric-less-equal-than-treshold", (Double a, Double b) => a <= b);
            AddFunction("iaf:numeric-greater-than-treshold", (Double a, Double b) => a > b);
            AddFunction("iaf:numeric-greater-equal-than-treshold", (Double a, Double b) => a >= b);

            AddFunction("iaf:numeric-subtract", (Double a, Double b) => a - b);
            AddFunction("iaf:numeric-divide", (Double a, Double b) => a / b);
            AddFunction("iaf:numeric-multiply", (Double a, Double b) => a * b);
            AddFunction("xs:qname", (String a) => a);
            AddFunction("not", (Boolean a) => !a);
            AddFunction("empty", (Object a) => a == null);
            AddFunction("abs", (Double a) => a < 0 ? -a : a);
            AddFunction("sum", (Double[] a) => sum(a));

        }
        public bool N_Equals(Double a, Double b) 
        {
            return a == b;
        }
        public bool N_Less(Double a, Double b)
        {
            return a < b;
        }
        public bool N_LessEqual(Double a, Double b)
        {
            return a <= b;
        }
        public bool N_Greater(Double a, Double b)
        {
            return a > b;
        }
        public bool N_GreaterEqual(Double a, Double b)
        {
            return a >= b;
        }

        public bool N_Equals_Treshold(Double a, Double b)
        {
            return a == b;
        }
        public bool N_Less_Treshold(Double a, Double b)
        {
            return a < b;
        }
        public bool N_LessEqual_Treshold(Double a, Double b)
        {
            return a <= b;
        }
        public bool N_Greater_Treshold(Double a, Double b)
        {
            return a > b;
        }
        public bool N_GreaterEqual_Treshold(Double a, Double b)
        {
            return a >= b;
        }

        public double N_Subtract(Double a, Double b)
        {
            return a - b;
        }
        public double N_Divide(Double a, Double b)
        {
            return a / b;
        }
        public double N_Multiply(Double a, Double b)
        {
            return a * b;
        }
        public double N_Unary_Minus(Double a)
        {
            return a - 1;
        }
        public double N_Unary_Plus(Double a)
        {
            return a + 1;
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
        public double abs(double a)
        {
            return a < 0 ? -a : a;
        }
        public double sum(params double[] parameters) 
        {
            return parameters.Sum();
        }
        public double max(params double[] parameters)
        {
            return parameters.Sum();
        }
        private Double Invert(Double a) 
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
