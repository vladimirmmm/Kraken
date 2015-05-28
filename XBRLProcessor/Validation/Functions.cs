using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace XBRLProcessor.Validation
{
    public class Functions
    {
        private static Functions _Current = null;
        public static Functions Current 
        {
            get 
            {
                if (_Current == null) 
                {
                    _Current = new Functions();
                }
                return _Current;
            }
        }

        public Func<Object[], Object> Find(string name) 
        {
            if (this.FunctionDictionary.ContainsKey(name)) 
            {
                return this.FunctionDictionary[name];
            }
            return null;
        }

        public Dictionary<string, Func<Object[], Object>> FunctionDictionary = new Dictionary<string, Func<Object[], object>>();
        public Dictionary<string, LambdaExpression> FunctionDictionary2 = new Dictionary<string, LambdaExpression>();
        /*
         name="iaf:sum" output="item()+">
         name="iaf:numeric-equal" output="xs:boolean">
         name="iaf:numeric-less-than" output="xs:boolean">
         name="iaf:numeric-less-equal-than" output="xs:boolean">
         name="iaf:numeric-greater-than" output="xs:boolean">
	     name="iaf:numeric-greater-equal-than" output="xs:boolean">
	     name="iaf:numeric-subtract" output="item()">
	     name="iaf:numeric-divide" output="item()">
	     name="iaf:numeric-multiply" output="item()">
	     name="iaf:numeric-multiply" output="item()">
	     name="iaf:numeric-unary-minus" output="item()">
	     name="iaf:splitValueThreshold" output="item()+">
	     name="iaf:joinValueThreshold" output="xs:string">
	     name="iaf:multiply-recursive" output="item()">
	     name="iaf:multiply-two-elements" output="item()">
	     name="iaf:numeric-equal-threshold" output="item()">
	     name="iaf:numeric-less-than-threshold" output="item()">
	     name="iaf:numeric-less-equal-than-threshold" output="item()">
	     name="iaf:numeric-greater-than-threshold" output="item()">
	     name="iaf:numeric-equal-test" output="item()+">
              */
        public Functions() 
        {
            this.FunctionDictionary.Add("sum", (object[] items) => { 
                double result = 0;
                foreach (var pv in items) 
                {
                    double val = 0;
                    //if (double.TryParse(pv, out val)) 
                    //{
                        result += (double)val;
                    //}
                }
                return result;
                });

            AddDoubleLogical("iaf:numeric-equal", (a, b) => a == b);
            AddDoubleLogical("iaf:numeric-less-than", (a, b) => a < b);
            AddDoubleLogical("iaf:numeric-less-equal-than", (a, b) => a <= b);
            AddDoubleLogical("iaf:numeric-greater-than", (a, b) => a > b);
            AddDoubleLogical("iaf:numeric-greater-equal-than", (a, b) => a >= b);

            AddDoubleLogical("iaf:numeric-equal-treshold", (a, b) => a == b);
            AddDoubleLogical("iaf:numeric-less-than-treshold", (a, b) => a < b);
            AddDoubleLogical("iaf:numeric-less-equal-than-treshold", (a, b) => a <= b);
            AddDoubleLogical("iaf:numeric-greater-than-treshold", (a, b) => a > b);
            AddDoubleLogical("iaf:numeric-greater-equal-than-treshold", (a, b) => a >= b);

            AddDoubleValue("iaf:numeric-subtract", (a, b) => a - b);
            AddDoubleValue("iaf:numeric-divide", (a, b) => a / b);
            AddDoubleValue("iaf:numeric-multiply", (a, b) => a * b);
            AddStringValue("xs:QName", (a) => a);
            AddBoolValue("not", (a) => !a);
            AddBoolValue("empty", (a) => a == null);
            /*
             	     name="iaf:numeric-subtract" output="item()">
	     name="iaf:numeric-divide" output="item()">
	     name="iaf:numeric-multiply" output="item()">
             */
        }
        private void AddObjectLogical(String name, Expression<Func<object, bool>> expr)
        {
            var function = expr.Compile();
            Func<object[], object> f = (object[] items) => (Object)function(items[0]);
            this.FunctionDictionary2.Add(name, expr);
            this.FunctionDictionary.Add(name, f);
        }
        private void AddBoolValue(String name, Expression<Func<bool, bool>> expr)
        {
            var function = expr.Compile();
            Func<object[], object> f = (object[] items) => (Object)function((bool)items[0]);
            this.FunctionDictionary2.Add(name, expr);
            this.FunctionDictionary.Add(name, f);
        }
        private void AddStringValue(String name, Expression<Func<string, string>> expr)
        {
            var function = expr.Compile();
            Func<object[], object> f = (object[] items) => (Object)function((string)items[0]);
            this.FunctionDictionary2.Add(name, expr);
            this.FunctionDictionary.Add(name, f);
        }
        private void AddDoubleLogical(String name, Expression<Func<double,double, bool>> expr)
        {
            var function = expr.Compile();
            Func<object[], object> f = (object[] items) => (Object)function((double)items[0], (double)items[1]);
            this.FunctionDictionary2.Add(name, expr);
            this.FunctionDictionary.Add(name, f);
        }
        private void AddDoubleValue(String name, Expression<Func<double, double, double>> expr)
        {
            var function = expr.Compile();
            Func<object[], object> f = (object[] items) => (Object)function((double)items[0], (double)items[1]);
            this.FunctionDictionary2.Add(name, expr);
            this.FunctionDictionary.Add(name, f);
        }
        //private void AddDoubleLogical(String name, Func<double,double, bool> function )
        //{
        //    Func<object[], object> f = (object[] items) => (Object)function((double)items[0], (double)items[1]);
        //    this.FunctionDictionary.Add(name, f);
        //}
        //private void AddDoubleValue(String name, Func<double, double, double> function)
        //{
        //    Func<object[], object> f = (object[] items) => (Object)function((double)items[0], (double)items[1]);
        //    this.FunctionDictionary.Add(name, f);
        //}

        private void Test() 
        {
            foreach (var f in FunctionDictionary2.Values) 
            {
                var s = f.ToString();

            }
        }
    }
}
