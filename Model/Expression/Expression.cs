using BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.Expressions
{
    public class Expression
    {
        public int PlaceHolderIndex = -1;
        public List<String> Parameters = new List<String>();
        public List<Expression> SubExpressions = new List<Expression>();
        public List<OperatorEnum> Operators = new List<OperatorEnum>();
        public bool IsString = false;
        public bool IsParameter = false;

        public string StringValue
        {
            get
            {
                return String.Format("{0}", this.Value);
            }
        }

        public virtual List<Expression> ChildExpressions() 
        {
            return SubExpressions;
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
         
            if (!handled) 
            {
                s = this.StringValue;
                if (IsString)
                {
                    s = parser.Syntax.StringDelimiter + s + parser.Syntax.StringDelimiter;
                }
                if (IsParameter)
                {
                    s = parser.Syntax.ParameterSpecifier + s;
                }
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
                    //sb.Append(parser.Syntax.ExpressionContainer_Left);
                }
                var subexpr = SubExpressions[i];
                sb.Append(parser.Translate(subexpr));
                if (i < SubExpressions.Count - 1)
                {
                    var opstring = parser.Syntax.CodeItemSeparator + parser.Syntax.Operators[Operators[i]] + parser.Syntax.CodeItemSeparator;
                    sb.Append(opstring);
                }
                else
                {

                    //sb.Append(parser.Syntax.ExpressionContainer_Right);
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

        public override List<Expression> ChildExpressions()
        {
            var result = new List<Expression>();
            result.AddRange(SubExpressions);
            result.AddRange(this.Items);
            return result;
        }

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
     
            //foreach (var subexpr in this.SubExpressions)
            //{
            //    var expr = subexpr.GetExpressionForPlaceHolder(ix);
            //    if (expr != null) { return expr; }
            //}
            return null;
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
