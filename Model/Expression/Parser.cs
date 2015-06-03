using LogicalModel.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.Expressions
{
    public class LanguageSyntax 
    {
        public OperatorCollection Operators = new OperatorCollection();
        public Dictionary<string, string> FunctionMap = new Dictionary<string, string>();

        public string ExpressionContainer_Left = "";
        public string ExpressionContainer_Right = "";
        public string BlockContainer_Left = "";
        public string BlockContainer_Right = "";
        public string ParameterSeparator = "";
        public string CodeItemSeparator = "";
        public string StringDelimiter = "";
        public string If = "";
        public string Then = "";
        public string Else = "";
        public string ParameterSpecifier = "";
        public string ExpressionPlaceholder = "";
        public string StatementEnd = "";
        public bool CaseSensitive = false;

        public void AddFunction<TFunctionContainer>(String Name, System.Linq.Expressions.Expression<Func<TFunctionContainer, Object>> expr) where TFunctionContainer : class
        {
            this.FunctionMap.Add(Name, "functions." + Utilities.Linq.GetMemberName<TFunctionContainer>(expr));
        }

        //public void AddFunction(String Name, System.Linq.Expressions.LambdaExpression expr)
        //{
        //    this.FunctionMap.Add(Name, "functions." + Utilities.Linq.GetPropertyName(expr));
        //}
    }

    public abstract class Parser
    {
        public LanguageSyntax Syntax = null;

        public Parser() 
        {

        }

        public virtual String Translate(Expression expr)
        {
            if (typeof(IfExpression) == expr.GetType()) 
            {
                return Translate(expr as IfExpression);
            }
            if (typeof(FunctionExpression) == expr.GetType())
            {
                return Translate(expr as FunctionExpression);
            }
            if (typeof(ListExpression) == expr.GetType())
            {
                return Translate(expr as ListExpression);
            }
            return expr.Translate(this);
        }
        
        public virtual String Translate(IfExpression expr)
        {
            return expr.Translate(this);
        }
        
        public virtual String Translate(ListExpression expression)
        {
            var sb = new StringBuilder();
            string delimiter = "";
            for (int i = 0; i < expression.Items.Count; i++)
            {
                var subexpr = expression.Items[i];
                delimiter = i == expression.Items.Count - 1 ? "" : Syntax.ParameterSeparator + Syntax.CodeItemSeparator;
                bool handled = false;
                var paramv = subexpr.StringValue;
                if (subexpr.StringValue.StartsWith(Syntax.ExpressionPlaceholder))
                {
                    var ix = int.Parse(subexpr.StringValue.Substring(Syntax.ExpressionPlaceholder.Length));
                    if (expression.SubExpressions.Count > 1)
                    {
                    }
                    paramv = Translate(expression.SubExpressions.FirstOrDefault());
                    sb.Append(paramv + delimiter);
                    handled = true;
                }

                if (!handled)
                {
                    sb.Append(Translate(subexpr) + delimiter);
                }
            }
            return sb.ToString();
        }

        public virtual String Translate(FunctionExpression expression)
        {
            var sb = new StringBuilder();
            if (String.IsNullOrEmpty(expression.Name))
            {
                sb.Append(Translate(expression as ListExpression));

            }
            else
            {
                sb.Append(String.Format("{0}({1})", expression.Name, Translate(expression as ListExpression)));
            }
            return sb.ToString();
        }
        
        protected int GetSubExpressionIndex(string value) 
        {
            var result = -1;
            if (value.StartsWith(Syntax.ExpressionPlaceholder)) 
            {
                value = value.Substring(Syntax.ExpressionPlaceholder.Length);
                result = int.Parse(value);
            }
            return result;
        }

        public void ManagePlaceHolders(Expression expr) 
        {
            if (expr is IfExpression) 
            {
                var ifexpr = expr as IfExpression;
                var ix_con = GetSubExpressionIndex(ifexpr.condition.StringValue);
                var ix_true = GetSubExpressionIndex(ifexpr.truepath.StringValue);
                var ix_false = GetSubExpressionIndex(ifexpr.falsepath.StringValue);
                if (ix_con > -1)
                {
                    ifexpr.condition = expr.SubExpressions[ix_con];
                }
                if (ix_true > -1)
                {
                    ifexpr.truepath = expr.SubExpressions[ix_true];
                }
                if (ix_false > -1)
                {
                    ifexpr.falsepath = expr.SubExpressions[ix_false];
                }
            }

            foreach (var subexpr in expr.SubExpressions) 
            {
                ManagePlaceHolders(subexpr);
            }
        }

        public virtual Expression ParseExpression(string expressionstring)
        {
            var tree = GetTreeString(expressionstring);
            var expr = ParseHierarchy(null, tree);
            ManagePlaceHolders(expr);
            return expr;
        }

        public Expression ParseHierarchy(Expression parentexpression,BaseModel.Hier<string> exprstringtree)
        {
            if (parentexpression == null)
            {
                //parentexpression = ParseExpressionString(exprstringtree.Item);
            }
            var expr = ParseExpressionString(exprstringtree.Item);

            var ix=0;
            foreach (var item in exprstringtree.HChildren)
            {
                var subexpr = ParseHierarchy(parentexpression, item);
                var exprplaceholder = expr.GetExpressionForPlaceHolder(ix);
                if (exprplaceholder != null) 
                {
                    exprplaceholder.SubExpressions.Add(subexpr);
                }
                //expr.SubExpressions.Add(subexpr);
                ix++;
            }
            //parentexpression.SubExpressions.Add(expr);
            return expr;
      
        }

        public virtual Expression ParseExpressionString(string expressionstring)
        {
            return null;
        }

        public virtual Expression GetSimpleExpression(string item)
        {
            Expression result = null;

            return result;
        }

        public virtual BaseModel.Hier<String> GetTreeString(string expression)
        {
            var item = new BaseModel.Hier<String>();
            var leftexprcontainercount = 0;
            var rightexprcontainercount = 0;
            var leftcontainerix = -1;
            var subexpressions = new List<string>();

            for (int i = 0; i < expression.Length; i++)
            {
                var character = expression[i].ToString();

                if (character == Syntax.ExpressionContainer_Left)
                {
                    leftexprcontainercount++;
                    if (leftcontainerix == -1)
                    {
                        leftcontainerix = i;
                    }
                }
                if (character == Syntax.ExpressionContainer_Right)
                {
                    rightexprcontainercount++;
                }
                if (rightexprcontainercount > leftexprcontainercount)
                {
                    throw new Exception("InvalidExpression");
                }
                if (leftexprcontainercount > 0 && leftexprcontainercount == rightexprcontainercount)
                {
                    var ix1 = leftcontainerix + 1;
                    var ix2 = i - ix1;
                    var subexpression = expression.Substring(ix1, ix2);
                    if (!String.IsNullOrEmpty(subexpression.Trim()))
                    {
                        var parameter = Syntax.ExpressionPlaceholder + subexpressions.Count;
                        subexpressions.Add(subexpression);

                        expression = expression.Remove(ix1, ix2);
                        expression = expression.Insert(ix1, parameter);
                        i = leftcontainerix + parameter.Length + 1;
                        leftexprcontainercount = 0;
                        rightexprcontainercount = 0;
                        leftcontainerix = -1;
                        var subtree = GetTreeString(subexpression);
                        item.AddChildren(subtree);
                    }
                    //item.Children.Add(subtree);
                }
            }
            item.Item = expression;
            return item;
        }

        public virtual List<String> GetParameters(Expression expr)
        {
            var result = new List<String>();
            {
                result.AddRange(expr.Parameters);

                foreach (var subexpr in expr.ChildExpressions()) 
                {
                    result.AddRange(GetParameters(subexpr));

                }
            }
            return result;
        }

        public virtual string GetFunction(ValidationRule rule)
        {
          
            return "";
        }
    }

    public class OperatorCollection : Dictionary<OperatorEnum, string>
    {
        public void AddItem(OperatorEnum type, string representation)
        {
            if (!this.ContainsKey(type))
            {
                this.Add(type, representation);
            }
        }

        public String GetRepresentation(OperatorEnum type)
        {
            if (!this.ContainsKey(type))
            {
                return this[type];
            }
            return "";
        }
    }

    public enum OperatorEnum
    {
        Unknown,

        Addition,
        Subtraction,
        Multiplication,
        Division,
        IntegerDivision,
        Modulo,

        Equals,
        NotEquals,
        Greater,
        Less,
        GreaterOrEqual,
        LessOrEqual,

        Not,
        And,
        Or,
        AndAlso,
        OrAlso,
    }

}
