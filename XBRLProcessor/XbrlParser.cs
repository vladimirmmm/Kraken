using LogicalModel.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utilities;

namespace XBRLProcessor
{
    public partial class XbrlFormulaParser : Parser
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
            this.Syntax.StringDelimiter2 = "\"";
            this.Syntax.Spacing = " ";
            this.Syntax.For = "for";
            this.Syntax.ForEach = "for";
            this.Syntax.In = "in";
            this.Syntax.Return = "return";
            this.Syntax.If = "if";
            this.Syntax.Then = "then";
            this.Syntax.Else = "else";
            this.Syntax.ParameterSpecifier = "$";
            this.Syntax.ExpressionPlaceholder = "#";
            this.Syntax.CaseSensitive = false;



            this.Syntax.Operators.AddItem(OperatorEnum.Unknown, " !!unknown!! ");
            this.Syntax.Operators.AddItem(OperatorEnum.Cast, " cast as ");

            this.Syntax.Operators.AddItem(OperatorEnum.Addition, " + ");
            this.Syntax.Operators.AddItem(OperatorEnum.Subtraction, " - ");
            this.Syntax.Operators.AddItem(OperatorEnum.And, " and ");
            this.Syntax.Operators.AddItem(OperatorEnum.AndAlso, " and ");
            //this.Syntax.Operators.AddItem(OperatorEnum.Division, "/");
            this.Syntax.Operators.AddItem(OperatorEnum.Division, " div ");

            this.Syntax.Operators.AddItem(OperatorEnum.GreaterOrEqual, ">=");
            this.Syntax.Operators.AddItem(OperatorEnum.LessOrEqual, "<=");
            this.Syntax.Operators.AddItem(OperatorEnum.NotEquals, "!=");
            this.Syntax.Operators.AddItem(OperatorEnum.Equals, "=");
            this.Syntax.Operators.AddItem(OperatorEnum.Greater, ">");
            this.Syntax.Operators.AddItem(OperatorEnum.Less, "<");

            this.Syntax.Operators.AddItem(OperatorEnum.VGreaterOrEqual, " ge ");
            this.Syntax.Operators.AddItem(OperatorEnum.VLessOrEqual, " le ");
            this.Syntax.Operators.AddItem(OperatorEnum.VNotEquals, " ne ");
            this.Syntax.Operators.AddItem(OperatorEnum.VEquals, " eq ");
            this.Syntax.Operators.AddItem(OperatorEnum.VGreater, " gt ");
            this.Syntax.Operators.AddItem(OperatorEnum.VLess, " lt ");

            this.Syntax.Operators.AddItem(OperatorEnum.IntegerDivision, "/");
            this.Syntax.Operators.AddItem(OperatorEnum.Modulo, "\\");
            this.Syntax.Operators.AddItem(OperatorEnum.Multiplication, "*");
            this.Syntax.Operators.AddItem(OperatorEnum.Not, "");
            this.Syntax.Operators.AddItem(OperatorEnum.Or, " or ");
            this.Syntax.Operators.AddItem(OperatorEnum.OrAlso, " or ");

        }

        public override Expression ParseExpressionString(string expressionstring)
        {
            var expression = new Expression();
            expression.OriginalSource = expressionstring;
            var fixedstring = expressionstring;
            fixedstring = fixedstring.Replace(Literals.Literal.AtSign, Literals.Literal.AtSignReplacement);

            Regex regExpr = new Regex("\"[^\"]*\"", RegexOptions.IgnoreCase);
            var strings = new List<string>();
            var matchlist = new List<Match>();
            foreach (Match m in regExpr.Matches(fixedstring))
            {
            
                matchlist.Insert(0, m);

            }
            var mix = 0;
            foreach (var m in matchlist)
            {
                var literal = m.Value.Trim('"');
                strings.Add(literal);
                fixedstring = fixedstring.Remove(m.Index, m.Value.Length);
                fixedstring = fixedstring.Insert(m.Index, "\"literal_" + mix + "\"");
                mix++;
            }

            var operators = Syntax.Operators.Values.Where(i => !String.IsNullOrEmpty(i)).Distinct().ToList();
            var ix = 0;
            foreach (var op in operators)
            {
                fixedstring = fixedstring.Replace(op, "@" + ix.ToString() + "@");
                ix++;

            }
            ix = 0;
            //fixedstring = fixedstring.Replace(" ", "");

            foreach (var op in operators)
            {
                var opstring = op.Replace(" ", "&spc_");
                fixedstring = fixedstring.Replace("@" + ix.ToString() + "@", "@" + opstring + "@");
                ix++;
            }
            //fixedstring = MarkLoops(fixedstring);
            fixedstring = fixedstring.Replace("@ ", "@-").Replace(" @", "-@");
            fixedstring = fixedstring.Replace(" ", "");
            fixedstring = fixedstring.Replace("&spc_", " ");
            fixedstring = fixedstring.Replace("@-", "@ ").Replace("-@", " @");

            //for (int i = 0; i < strings.Count; i++)
            //{
            //    fixedstring = fixedstring.Replace("\"#!" + i + "\"", strings[i]);
            //}


            var items = fixedstring.Split(new string[] { "@" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = items[i].Replace(Literals.Literal.AtSignReplacement, Literals.Literal.AtSign);
            }
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
                            Logger.WriteLine("Operator not found!");
                        }
                    }
                }
            }
            if (items.Length == 2 && items[0] == Syntax.Operators[OperatorEnum.Subtraction])
            {
                var simpleexpression = GetSimpleExpression(Syntax.Operators[OperatorEnum.Subtraction] + items[1]);
                expression = simpleexpression;
            }
            SetLiteralsBack(expression, strings);
            return expression;
        }

        public void SetLiteralsBack(Expression expression, List<string> strings)
        {
            if (expression.IsString)
            {
                var strval = expression.StringValue;
                for (int i = 0; i < strings.Count; i++)
                {
                    strval = strval.Replace("literal_" + i + "", strings[i]);
                }
                expression.Value = strval;
            }
            foreach (var expr in expression.ChildExpressions()) 
            {
                SetLiteralsBack(expr, strings);
            }
        }

        public string MarkLoops(string expressionstring)
        {
            var result = "";
            var expstr = expressionstring.Trim();
            //for $i in $a return iaf:abs($i)
            if (expstr.StartsWith("for", StringComparison.InvariantCultureIgnoreCase))
            {
                var variablename = Utilities.Strings.TextBetween(expstr, Syntax.For, Syntax.In).Trim();
                var source = Utilities.Strings.TextBetween(expstr, Syntax.In, Syntax.Return).Trim();
                var action = expstr.Substring(expstr.IndexOf(Syntax.Return) + Syntax.Return.Length);
                result = string.Format(" for({0},{1},{2}) ", variablename, source, action);
                //for($i,$a,iaf:abs($i))
            }
            return result;
        }

        public override Expression GetSimpleExpression(string item)
        {
            //if (item.Contains("s2c_typ:ID")) 
            //{

            //}
            Expression result = null;
            var isfunction = false;
            var isinstring = false;
            var trimmeditem = item.Trim();
            if (trimmeditem.StartsWith(Syntax.StringDelimiter) && trimmeditem.EndsWith(Syntax.StringDelimiter))
            {
                isinstring = true;
            }
            if (trimmeditem.StartsWith(Syntax.StringDelimiter2) && trimmeditem.EndsWith(Syntax.StringDelimiter2))
            {
                isinstring = true;
            }
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
            if (isfunction && !isinstring)
            {
                var functionname = item.Remove(item.IndexOf(Syntax.ExpressionContainer_Left));
                functionname = functionname.Trim();
                if (functionname == Syntax.If)
                {
                    var iffunction = new IfExpression();
                    var p_condition = Utilities.Strings.TextBetween(item, Syntax.If + Syntax.ExpressionContainer_Left, Syntax.ExpressionContainer_Right);
                    var p_true = Utilities.Strings.TextBetween(item, Syntax.Then + Syntax.ExpressionContainer_Left, Syntax.ExpressionContainer_Right);
                    if (String.IsNullOrEmpty(p_true))
                    {
                        p_true = Utilities.Strings.TextBetween(item, Syntax.Then, Syntax.Else);
                    }
                    string p_false = "";
                    if (item.IndexOf(Syntax.Else + Syntax.ExpressionContainer_Left) == -1)
                    {
                        var elsix = item.IndexOf(Syntax.Else);
                        if (elsix > -1)
                        {
                            p_false = item.Substring(elsix + Syntax.Else.Length);
                        }
                    }
                    else
                    {
                        p_false = Utilities.Strings.TextBetween(item, Syntax.Else + Syntax.ExpressionContainer_Left, Syntax.ExpressionContainer_Right);
                    }
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
                        //TODO t1
                        //func.SubExpressions.Add(itemexpression);

                    }
                    //TODO add the Expression<Func here
                    result = func;
                }
            }
            else
            {
                //if (item.IndexOf(Syntax.StringDelimiter) < item.IndexOf(Syntax.ParameterSeparator))
                var enumeration = GetEnumeration(item);
                if (enumeration.Length > 1)
                {
                    //var parameters = item.Split(new string[] { Syntax.ParameterSeparator }, StringSplitOptions.RemoveEmptyEntries);
                    var listexpression = new ListExpression();
                    foreach (var pm in enumeration)
                    {

                        var itemexpression = GetSimpleExpression(pm);
                        itemexpression.PlaceHolderIndex = GetSubExpressionIndex(itemexpression.StringValue);
                        listexpression.Items.Add(itemexpression);

                        //TODO t1
                        //listexpression.SubExpressions.Add(itemexpression);
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
                if (trimmed.StartsWith(Syntax.StringDelimiter2) && trimmed.EndsWith(Syntax.StringDelimiter2))
                {
                    svalue = trimmed.Trim(Syntax.StringDelimiter2.ToCharArray());
                    result.IsString = true;
                }
                if (trimmed.StartsWith(Syntax.ParameterSpecifier))
                {
                    result.IsParameter = true;
                    svalue = trimmed.Substring(Syntax.ParameterSpecifier.Length);
                    result.Parameters.Add(svalue);
                }
                else
                {
                    if (!trimmed.StartsWith(Syntax.ExpressionPlaceholder)
                        && !result.IsString
                        && !Utilities.Strings.IsNumeric(trimmed)
                        && !String.IsNullOrEmpty(trimmed))
                    {
                        result.IsString = true;
                    }
                }
                result.Value = svalue.Trim();

            }
            return result;
        }

        private bool IsEnumeration(string item)
        {
            var items = item.Split(new string[] { Syntax.ParameterSeparator }, StringSplitOptions.RemoveEmptyEntries);
            if (items.Length == 1) { return false; }
            foreach (var itempart in items)
            {
                if (itempart.IndexOf(Syntax.StringDelimiter) > -1)
                {
                    if (Utilities.Strings.ContainsCount(Syntax.StringDelimiter, itempart) % 2 == 1)
                    {
                        return false;
                    }
                }
                if (itempart.IndexOf(Syntax.StringDelimiter2) > -1)
                {
                    if (Utilities.Strings.ContainsCount(Syntax.StringDelimiter2, itempart) % 2 == 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private string RemoveSeparatorsBetween(string item, char between, char separator, string replacement)
        {
            var result = "";
            var iswithin = false;
            foreach (char c in item)
            {
                var toadd = c.ToString();
                if (c == between)
                {
                    iswithin = !iswithin;
                }
                if (c == separator && iswithin)
                {
                    toadd = replacement;
                }
                result += toadd;
            }
            return result;
        }

        private string[] GetEnumeration(string item)
        {
            if (item.IndexOf(Syntax.ParameterSeparator) == -1)
            {
                return new string[] { item };
            }
            char separator = Syntax.ParameterSeparator[0];
            char d1 = Syntax.StringDelimiter[0];
            char d2 = Syntax.StringDelimiter2[0];
            var replacement = "@@DX@";
            var strx = RemoveSeparatorsBetween(item, d1, separator, replacement);
            strx = RemoveSeparatorsBetween(item, d2, separator, replacement);
            //item = item.Replace(Syntax.StringDelimiter2, Syntax.StringDelimiter);
            var items = item.Split(new string[] { Syntax.ParameterSeparator }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = items[i].Replace(replacement, Syntax.ParameterSeparator);
            }
            return items;
        }
    }

}
