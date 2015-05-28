using LogicalModel.Expression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBRLProcessor
{
    public class XbrlFormulaParser : Parser
    {

        public XbrlFormulaParser()
        {
            this.Syntax = new LanguageSyntax();

            this.Syntax.ExpressionContainer_Left = "(";
            this.Syntax.ExpressionContainer_Right = ")";
            this.Syntax.BlockContainer_Left = "(";
            this.Syntax.BlockContainer_Right = ")";
            this.Syntax.ParameterSeparator = ",";
            this.Syntax.Separator = " ";
            this.Syntax.StringDelimiter = "'";
            this.Syntax.If = "if";
            this.Syntax.Then = "then";
            this.Syntax.Else = "else";
            this.Syntax.ParameterSpecifier = "$";
            this.Syntax.ExpressionPlaceholder = "#";
            this.Syntax.CaseSensitive = false;


            this.Syntax.Operators.AddItem(OperatorEnum.Unknown, " !!unknown!! ");
            this.Syntax.Operators.AddItem(OperatorEnum.Addition, " + ");
            this.Syntax.Operators.AddItem(OperatorEnum.And, " and ");
            this.Syntax.Operators.AddItem(OperatorEnum.AndAlso, " and ");
            this.Syntax.Operators.AddItem(OperatorEnum.Division, " / ");
            this.Syntax.Operators.AddItem(OperatorEnum.Equals, " = ");
            this.Syntax.Operators.AddItem(OperatorEnum.Greater, " > ");
            this.Syntax.Operators.AddItem(OperatorEnum.GreaterOrEqual, " >= ");
            this.Syntax.Operators.AddItem(OperatorEnum.IntegerDivision, " / ");
            this.Syntax.Operators.AddItem(OperatorEnum.Less, " < ");
            this.Syntax.Operators.AddItem(OperatorEnum.LessOrEqual, " <= ");
            this.Syntax.Operators.AddItem(OperatorEnum.Modulo, " \\ ");
            this.Syntax.Operators.AddItem(OperatorEnum.Multiplication, " * ");
            this.Syntax.Operators.AddItem(OperatorEnum.Not, "");
            this.Syntax.Operators.AddItem(OperatorEnum.NotEquals, "");
            this.Syntax.Operators.AddItem(OperatorEnum.Or, " or ");
            this.Syntax.Operators.AddItem(OperatorEnum.OrAlso, " or ");
            this.Syntax.Operators.AddItem(OperatorEnum.Subtraction, " - ");

        }

        public override Expression ParseExpressionString(string expressionstring)
        {
            var expression = new Expression();
            //var tree = GetTreeString(expressionstring);

            var fixedstring = expressionstring;
            var operators = Syntax.Operators.Values.Where(i => !String.IsNullOrEmpty(i)).Distinct().ToList();
            foreach (var op in operators)
            {
                fixedstring = fixedstring.Replace(op, "@" + op + "@");
            }
            fixedstring = fixedstring.Replace("@ ", "@-").Replace(" @", "-@");
            fixedstring = fixedstring.Replace(" ", "");
            fixedstring = fixedstring.Replace("@-", "@ ").Replace("-@", " @");

            var items = fixedstring.Split(new string[] { "@" }, StringSplitOptions.RemoveEmptyEntries);
            if (items.Length == 1)
            {
                var item = items[0];
                var simpleexpression = GetSimpleExpression(item);
                //expression.SubExpressions.Add(simpleexpression);
                expression = simpleexpression;
            }
            if (items.Length > 1 && items.Length % 2 == 1)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    var item = items[i];
                    if (i % 2 == 0)
                    {
                        var iexpression = GetSimpleExpression(item);
                        expression.SubExpressions.Add(iexpression);
                    }
                    else
                    {
                        var op = Syntax.Operators.FirstOrDefault(o => o.Value == item);
                        if (!op.Equals(null))
                        {
                            expression.Operators.Add(op.Key);
                        }
                        else
                        {
                            Console.WriteLine("Operator not found!");
                        }
                    }
                }
            }
            if (expression.SubExpressions.Count == 1)
            {
                //var subexpression = expression.SubExpressions[0];
                //subexpression.SubExpressions.AddRange(expression.SubExpressions.Where(i=>i!=subexpression));
                //return subexpression;
            }
            return expression;
        }

        public override Expression GetSimpleExpression(string item)
        {
            Expression result = null;
            var isfunction = false;

            var leftcontainer_ix = item.IndexOf(Syntax.ExpressionContainer_Left);
            var rightcontainer_ix = item.IndexOf(Syntax.ExpressionContainer_Right);
            var separator_ix = item.IndexOf(Syntax.ParameterSeparator);
            if (leftcontainer_ix > -1) 
            {
                if (separator_ix > -1)
                {
                    if (rightcontainer_ix < leftcontainer_ix)
                    {
                        isfunction = leftcontainer_ix < separator_ix;
                    }
                }
                else 
                {
                    isfunction = true;
                }
            }
            if (isfunction)
            {
                var functionname = item.Remove(item.IndexOf(Syntax.ExpressionContainer_Left));
                if (functionname == Syntax.If)
                {
                    var iffunction = new IfExpression();
                    var p_condition = Utilities.Strings.TextBetween(item, Syntax.If + Syntax.ExpressionContainer_Left, Syntax.ExpressionContainer_Right);
                    var p_true = Utilities.Strings.TextBetween(item, Syntax.Then + Syntax.ExpressionContainer_Left, Syntax.ExpressionContainer_Right);
                    var p_false = Utilities.Strings.TextBetween(item, Syntax.Else + Syntax.ExpressionContainer_Left, Syntax.ExpressionContainer_Right);
                    iffunction.condition = GetSimpleExpression(p_condition);
                    iffunction.truepath = GetSimpleExpression(p_true);
                    iffunction.falsepath = GetSimpleExpression(p_false);
                    /*
                    iffunction.SubExpressions.Add(iffunction.condition);
                    iffunction.SubExpressions.Add(iffunction.truepath);
                    iffunction.SubExpressions.Add(iffunction.falsepath);
                    */
                    for (int i = 0; i < 3;i++)
                    {
                        //var parameter = new Expression();
                        //parameter.Value = Syntax.ExpressionPlaceholder + i.ToString();
                        //var param = new ParameterValue();
                        //param.PlaceHolderIndex = GetSubExpressionIndex(param.StringValue);
                        //iffunction.Parameters.Add(param);
                        var pv = Syntax.ExpressionPlaceholder + i.ToString();
                        var itemexpression = GetSimpleExpression(pv);
                        itemexpression.PlaceHolderIndex = GetSubExpressionIndex(itemexpression.StringValue);
                        iffunction.Items.Add(itemexpression);

                    }
                    result = iffunction;

                }
                else
                {
                    var func = new FunctionExpression();
                    func.Name = functionname;
                    var parameterstring = Utilities.Strings.TextBetween(item, Syntax.ExpressionContainer_Left, Syntax.ExpressionContainer_Right);
                    if (!string.IsNullOrEmpty(parameterstring))
                    {
                        //var param = new ParameterValue(parameterstring);
                        //param.PlaceHolderIndex = GetSubExpressionIndex(param.StringValue);
                        //func.Parameters.Add(param);
                        var itemexpression = GetSimpleExpression(parameterstring);
                        itemexpression.PlaceHolderIndex = GetSubExpressionIndex(itemexpression.StringValue);
                        func.Items.Add(itemexpression);
                      
                    }
                    //TODO add the Expression<Func here
                    result = func;
                }
            }
            else
            {
                if (item.IndexOf(Syntax.StringDelimiter) < item.IndexOf(Syntax.ParameterSeparator))
                {
                    var parameters = item.Split(new string[] { Syntax.ParameterSeparator }, StringSplitOptions.RemoveEmptyEntries);
                    var listexpression = new ListExpression();
                    foreach (var pm in parameters)
                    {
                        //var pmv = new ParameterValue(pm);
                        //pmv.PlaceHolderIndex = GetSubExpressionIndex(pmv.StringValue);
                        //pmv.Name = pm.Contains(Syntax.ParameterSpecifier) ? pmv.Name : "";
                        //listexpression.Parameters.Add(pmv);

                        var itemexpression = GetSimpleExpression(pm);
                        itemexpression.PlaceHolderIndex = GetSubExpressionIndex(itemexpression.StringValue);
                        listexpression.Items.Add(itemexpression);
                    }
                    result = listexpression;
                }
            }
            if (result == null)
            {
                result = new Expression();
                result.PlaceHolderIndex = GetSubExpressionIndex(item);
                result.Value = item;
            }
            return result;
        }

        public void Test() 
        {
            var expr = this.ParseExpression("$a = (xs:QName('eba_ZZ:x27'), xs:QName('eba_ZZ:x28'))");     
            var item = expr.Translate(this);
        }
    }

}
