﻿using LogicalModel.Expressions;
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
            this.Syntax.CodeItemSeparator = " ";
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

                    for (int i = 0; i < 3; i++)
                    {

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
                var svalue = item;
                var trimmed = item.Trim();
                if (trimmed.StartsWith(Syntax.StringDelimiter) && trimmed.EndsWith(Syntax.StringDelimiter))
                {
                    svalue = trimmed.Trim(Syntax.StringDelimiter.ToCharArray());
                    result.IsString = true;
                }
                if (trimmed.StartsWith(Syntax.ParameterSpecifier)) 
                {
                    result.IsParameter = true;
                    svalue = trimmed.Substring(Syntax.ParameterSpecifier.Length);
                    result.Parameters.Add(svalue);
                }
                result.Value = svalue;

            }
            return result;
        }

        #region Translate


        public override string Translate(IfExpression expression)
        {
            var sb = new StringBuilder();
            var format = Syntax.If + Syntax.ExpressionContainer_Left + "#0" + Syntax.ExpressionContainer_Right
                + Syntax.CodeItemSeparator + Syntax.Then + Syntax.CodeItemSeparator
                + Syntax.BlockContainer_Left + "#1" + Syntax.BlockContainer_Right
                + Syntax.CodeItemSeparator + Syntax.Else + Syntax.CodeItemSeparator 
                + Syntax.BlockContainer_Left + "#2" + Syntax.BlockContainer_Right;
            format = format.Replace("{", "{{").Replace("}", "}}");
            format = format.Replace("#0", "{0}").Replace("#1", "{1}").Replace("#2", "{2}");
            sb.Append(String.Format(format, Translate(expression.condition), Translate(expression.truepath), Translate(expression.falsepath)));
            return sb.ToString();
        }

        #endregion

        public void Test()
        {
            var xbrlparser = new XbrlFormulaParser();
            var csparser = new CSharpParser();
            var expr1 = this.ParseExpression("$a = (xs:QName('eba_ZZ:x27'), xs:QName('eba_ZZ:x28'))");
            var expr2 = this.ParseExpression("if ($ReportingLevel = 'con') then ($a = xs:QName('eba_SC:x7')) else (true())");
            var expr3 = this.ParseExpression("iaf:numeric-equal($a, iaf:numeric-divide((iaf:sum(((iaf:numeric-multiply($b, $c)), (iaf:numeric-multiply($d, $e)), (iaf:numeric-multiply($f, $g))))), $h))");

            var prs1 = GetParameters(expr1);
            var prs2 = GetParameters(expr2);
            var prs3 = GetParameters(expr3);

            var v1 = new LogicalModel.Validation.ValidationRule();
            v1.RootExpression = expr1;
            v1.ID = "v1";
            foreach (var p in prs1) 
            {
                var pm = new LogicalModel.Validation.ValidationParameter(p);
                pm.TypeString = "DoubleValue";
                v1.Parameters.Add(pm);

            }

            var v2 = new LogicalModel.Validation.ValidationRule();
            v2.RootExpression = expr2;
            v2.ID = "v2";
            foreach (var p in prs2)
            {
                var pm = new LogicalModel.Validation.ValidationParameter(p);
                pm.TypeString = "DoubleValue";
                v2.Parameters.Add(pm);

            }

            var v3 = new LogicalModel.Validation.ValidationRule();
            v3.RootExpression = expr3;
            v3.ID = "v3";
            foreach (var p in prs3)
            {
                var pm = new LogicalModel.Validation.ValidationParameter(p);
                pm.TypeString = "DoubleValue";
                v3.Parameters.Add(pm);

            }


            var item1_xbrl = xbrlparser.Translate(expr1);
            var item1_cs = csparser.GetFunction(v1);
            var item2_xbrl = xbrlparser.Translate(expr2);
            var item2_cs = csparser.GetFunction(v2);
            var item3_xbrl = xbrlparser.Translate(expr3);
            var item3_cs = csparser.GetFunction(v3);
            var test = item1_cs + item2_cs + item3_cs;
        }
    }

}
