using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.Expression
{
    public class LanguageSyntax 
    {
        public OperatorCollection Operators = new OperatorCollection();
        public string ExpressionContainer_Left = "";
        public string ExpressionContainer_Right = "";
        public string BlockContainer_Left = "";
        public string BlockContainer_Right = "";
        public string ParameterSeparator = "";
        public string Separator = "";
        public string StringDelimiter = "";
        public string If = "";
        public string Then = "";
        public string Else = "";
        public string ParameterSpecifier = "";
        public string ExpressionPlaceholder = "";
        public bool CaseSensitive = false;
    }
    public abstract class Parser
    {
        public LanguageSyntax Syntax = null;

        public Parser() 
        {

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
            //if (expr.SubExpressions.Count == 1)
            //{
            //    var subexpr = expr.SubExpressions[0];
            //    subexpr.Value = expr.Value;
            //    expr = subexpr;
            //}
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


        /*
        public Expression ParseHierarchy(BaseModel.Hier<string> exprstringtree)
        {
            var t_expr = new Expression();
            foreach (var item in exprstringtree.HChildren)
            {
                var subexpr = ParseHierarchy(item);
                t_expr.SubExpressions.Add(subexpr);

            }
            var expr = ParseExpressionString(exprstringtree.Item);
            var cont = expr.SubExpressions.FirstOrDefault();
            if (cont != null)
            {
                cont.SubExpressions.AddRange(t_expr.SubExpressions);
            }
            return expr;
        }
         * */
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
