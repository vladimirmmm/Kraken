using BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.Expression
{
    public class Expression
    {
        public int PlaceHolderIndex = -1;
        public List<Expression> SubExpressions = new List<Expression>();
        public List<OperatorEnum> Operators = new List<OperatorEnum>();
        
        public string StringValue
        {
            get
            {
                return String.Format("{0}", this.Value);
            }
        }

        public Object Value = null;

        public virtual Object Evaluate(params ParameterValue[] parameters)
        {
            return Value;
        }

        public virtual Object Evaluate(params Object[] parameters)
        {
            return Value;
        }

        public virtual String Translate(Parser parser)
        {
            if (this.SubExpressions.Count == 0)
            {
                return TranslateSimple(parser);
            }
            else 
            {
                return TranslateComplex(parser);
            }
        }


        public virtual Expression GetExpressionForPlaceHolder(int ix)
        {
            if (this.PlaceHolderIndex==ix)
            {
                return this;
            }
            foreach (var subexpr in this.SubExpressions) 
            {
                var expr =subexpr.GetExpressionForPlaceHolder(ix);
                if (expr != null) { return expr; }
            }
            return null;
        }

        public string TranslateSimple(Parser parser) 
        {
            var s = "";
            var handled = false;
            if (this.StringValue.StartsWith(parser.Syntax.ParameterSpecifier)) 
            {
                s = this.StringValue.Substring(parser.Syntax.ParameterSpecifier.Length);
                handled = true;
            }
            if (!handled) 
            {
                s = this.StringValue;
            }
            return s;
        }
        
        public string TranslateComplex(Parser parser)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < SubExpressions.Count; i++)
            {
                if (i == 0)
                {
                    sb.Append(parser.Syntax.ExpressionContainer_Left);
                }
                var subexpr = SubExpressions[i];
                sb.Append(subexpr.Translate(parser));
                if (i < SubExpressions.Count - 1)
                {
                    var opstring = parser.Syntax.Operators[Operators[i]];
                    sb.Append(opstring);
                }
                else
                {

                    sb.Append(parser.Syntax.ExpressionContainer_Right);
                }
            }
            return sb.ToString();
        }
        
        public override string ToString()
        {
            return String.Format("{0} - {1}", this.GetType().Name, this.Value);
        }

    }

    public class ListExpression : Expression
    {
        private List<Expression> _Items = new List<Expression>();
        public List<Expression> Items { get { return _Items; } set { _Items = value; } }

        public override Expression GetExpressionForPlaceHolder(int ix)
        {
            if (this.PlaceHolderIndex == ix)
            {
                return this;
            }
            foreach (var param in this.Items)
            {
                if (param.PlaceHolderIndex == ix)
                {
                    return this;
                }
     
            }
            foreach (var param in this.Items)
            {
                var expr = param.GetExpressionForPlaceHolder(ix);
                if (expr != null) { return expr; }
            }
     
            foreach (var subexpr in this.SubExpressions)
            {
                var expr = subexpr.GetExpressionForPlaceHolder(ix);
                if (expr != null) { return expr; }
            }
            return null;
        }

        public override string Translate(Parser parser)
        {
            var sb = new StringBuilder();
            string delimiter = "";
            for (int i = 0; i < Items.Count; i++)
            {
                var subexpr = Items[i];
                delimiter = i == Items.Count - 1 ? "" : parser.Syntax.ParameterSeparator;
                bool handled = false;
                var paramv = subexpr.StringValue;
                if (subexpr.StringValue.StartsWith(parser.Syntax.ExpressionPlaceholder))
                {
                    var ix = int.Parse(subexpr.StringValue.Substring(parser.Syntax.ExpressionPlaceholder.Length));
                    if (this.SubExpressions.Count > 1) 
                    { 
                    }
                    //paramv = SubExpressions[ix].Translate(parser);
                    paramv = SubExpressions.FirstOrDefault().Translate(parser);
                    sb.Append(paramv + delimiter);
                    handled = true;
                }
                if (subexpr.StringValue.StartsWith(parser.Syntax.ParameterSpecifier))
                {
                    paramv = subexpr.StringValue.Substring(parser.Syntax.ParameterSpecifier.Length);
                    sb.Append(paramv + delimiter);
                    handled = true;
                }
                if (!handled)
                {
                    sb.Append(subexpr.Translate(parser) + delimiter);
                }
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0} - ", this.GetType().Name);
            foreach (var p in SubExpressions) 
            {
                sb.AppendFormat("{0}, ", p);
            }
            return sb.ToString();
        }

    }
    
    public class FunctionExpression : ListExpression
    {
        public string Name = "";
        public static Func<string, Func<Object[], Object>> FunctionProvider = null;
        private Func<Object[], Object> _Function = null;
        public Func<Object[], Object> Function
        {
            get 
            {
                if (_Function == null) 
                {
                    _Function = FunctionProvider(this.Name);
                }
                return _Function;
            }
            set 
            {
                _Function = value;
            }
        }

        public override string Translate(Parser parser)
        {
            var sb = new StringBuilder();
            sb.Append(String.Format("{0}({1})", this.Name, base.Translate(parser)));
            return sb.ToString();
        }

        public override Object Evaluate(params Object[] parameters)
        {
            Object result = null;
            if (Function != null)
            {
                result = Function(parameters);
            }
            return result;
        }

        public override Object Evaluate(params ParameterValue[] parameters)
        {
            Object result = null;
     
            if (Function != null)
            {
                result = Function(parameters.Select(i => i.Value).ToArray());
            }
            return result;
        }

        public override string ToString()
        {
            return String.Format("{0} - {1}",this.GetType() , this.Name);
        }
    }

    public class IfExpression : FunctionExpression
    {
        public Expression condition = null;
        public Expression truepath = null;
        public Expression falsepath = null;

        public IfExpression()
        {

        }
        public override string Translate(Parser parser)
        {
            var sb = new StringBuilder();
            var format = parser.Syntax.If + parser.Syntax.ExpressionContainer_Left + "{0}" + parser.Syntax.ExpressionContainer_Right
                + parser.Syntax.Then
                + parser.Syntax.ExpressionContainer_Left + "{1}" + parser.Syntax.ExpressionContainer_Right
                + parser.Syntax.Else
                + parser.Syntax.ExpressionContainer_Left + "{2}" + parser.Syntax.ExpressionContainer_Right;

            sb.Append(String.Format(format, condition.Translate(parser), truepath.Translate(parser), falsepath.Translate(parser)));
            return sb.ToString();
        }

        public override object Evaluate(params ParameterValue[] parameters)
        {
            return null;
        }

        public override object Evaluate(params Object[] parameters)
        {
            if (condition == null) { throw new Exception("Condition Missing!"); }
            if (truepath == null) { throw new Exception("True Path Missing!"); }
            if ((bool)condition.Evaluate(parameters))
            {
                return truepath.Evaluate(parameters);
            }
            else
            {
                if (falsepath != null)
                {
                    return falsepath.Evaluate(parameters);
                }
                return null;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0} - ", this.GetType().Name);
            sb.AppendFormat("if ({0}) {{{1}}} else {{{2}}}", condition, truepath, falsepath);
            return sb.ToString();
        }
    }
    
    public class Parameter 
    {
        public ParameterType Type { get; set; }
        public String Name { get; set; }
        public int PlaceHolderIndex = -1;

        public Parameter() { }
        public Parameter(string name) 
        {
            this.Name = name;
            this.Type = ParameterType.RealNumber;
        }
    }
    public class ParameterValue:Parameter
    {
        public ParameterValue() 
        { 

        }
        public ParameterValue(Object value)
        {
            this.Value = value;
        }
        public Object Value { get; set; }
        public string StringValue
        {
            get
            {
                return String.Format("{0}", this.Value);
            }
        }
    }

    public enum ParameterType 
    {
        Integer,
        RealNumber,
        DateTime,
        String,
        Boolean,
    }
}
