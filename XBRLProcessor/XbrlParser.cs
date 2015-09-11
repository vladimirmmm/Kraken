using LogicalModel.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

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
            this.Syntax.StringDelimiter2 = "\"";
            this.Syntax.Spacing = " ";
            this.Syntax.If = "if";
            this.Syntax.Then = "then";
            this.Syntax.Else = "else";
            this.Syntax.ParameterSpecifier = "$";
            this.Syntax.ExpressionPlaceholder = "#";
            this.Syntax.CaseSensitive = false;


            this.Syntax.Operators.AddItem(OperatorEnum.Unknown, " !!unknown!! ");
            this.Syntax.Operators.AddItem(OperatorEnum.Addition, " + ");
            this.Syntax.Operators.AddItem(OperatorEnum.Subtraction, " - ");
            this.Syntax.Operators.AddItem(OperatorEnum.And, " and ");
            this.Syntax.Operators.AddItem(OperatorEnum.AndAlso, " and ");
            this.Syntax.Operators.AddItem(OperatorEnum.Division, "/");
            this.Syntax.Operators.AddItem(OperatorEnum.GreaterOrEqual, ">=");
            this.Syntax.Operators.AddItem(OperatorEnum.LessOrEqual, "<=");
            this.Syntax.Operators.AddItem(OperatorEnum.NotEquals, "!=");
            this.Syntax.Operators.AddItem(OperatorEnum.Equals, "=");
            this.Syntax.Operators.AddItem(OperatorEnum.Greater, ">");
            this.Syntax.Operators.AddItem(OperatorEnum.IntegerDivision, "/");
            this.Syntax.Operators.AddItem(OperatorEnum.Less, "<");
            this.Syntax.Operators.AddItem(OperatorEnum.Modulo, "\\");
            this.Syntax.Operators.AddItem(OperatorEnum.Multiplication, "*");
            this.Syntax.Operators.AddItem(OperatorEnum.Not, "");
            this.Syntax.Operators.AddItem(OperatorEnum.Or, " or ");
            this.Syntax.Operators.AddItem(OperatorEnum.OrAlso, " or ");

        }

        public override Expression ParseExpressionString(string expressionstring)
        {
            var expression = new Expression();

            var fixedstring = expressionstring;
            var operators = Syntax.Operators.Values.Where(i => !String.IsNullOrEmpty(i)).Distinct().ToList();
            var ix = 0;
            foreach (var op in operators)
            {
                fixedstring = fixedstring.Replace(op, "@" + ix.ToString() + "@");
                ix++;

            }
            ix = 0;
            foreach (var op in operators)
            {
                fixedstring = fixedstring.Replace("@" + ix.ToString() + "@", "@" + op + "@");
                ix++;
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
                            Logger.WriteLine("Operator not found!");
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
                functionname = functionname.Trim();
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
                if (enumeration.Length>1)
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
                if ( trimmed.StartsWith(Syntax.StringDelimiter2) && trimmed.EndsWith(Syntax.StringDelimiter2))
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
                if (itempart.IndexOf(Syntax.StringDelimiter) > -1 ) 
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

        public void Testx()
        {
            var str = "a,kutya,is\"ko,\"la,'egy','erdekes,\"hely,\",\",mint'olyan,'\"";
            var strx = RemoveSeparatorsBetween(str, '"', ',', "@@DX@");
            strx = RemoveSeparatorsBetween(strx, '\'', ',', "@@DX@");
            var items = GetEnumeration(strx);
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
        
        public void Test2()
        {
            var xbrlparser = new XbrlFormulaParser();
            var csparser = new CSharpParser();
            var expr3 = this.ParseExpression("iaf:numeric-equal($a, iaf:numeric-divide((iaf:sum((iaf:max((iaf:sum((iaf:numeric-multiply($b, 0.18), iaf:numeric-multiply($c, 0.18), iaf:numeric-multiply($d, 0.12), iaf:numeric-multiply((iaf:sum(($e, $f, iaf:numeric-multiply($g, 0.035)))), 0.15), iaf:numeric-multiply((iaf:sum(($h, $i, iaf:numeric-multiply($j, 0.035)))), 0.12), iaf:numeric-multiply($k, 0.18), iaf:numeric-multiply($l, 0.15), iaf:numeric-multiply($m, 0.12))), 0)), iaf:max((iaf:sum((iaf:numeric-multiply($n, 0.18), iaf:numeric-multiply($o, 0.18), iaf:numeric-multiply($p, 0.12), iaf:numeric-multiply((iaf:sum(($q, $r, iaf:numeric-multiply($s, 0.035)))), 0.15), iaf:numeric-multiply((iaf:sum(($t, $u, iaf:numeric-multiply($v, 0.035)))), 0.12), iaf:numeric-multiply($w, 0.18), iaf:numeric-multiply($x, 0.15), iaf:numeric-multiply($y, 0.12))), 0)), iaf:max((iaf:sum((iaf:numeric-multiply($z, 0.18), iaf:numeric-multiply($aa, 0.18), iaf:numeric-multiply($bb, 0.12), iaf:numeric-multiply((iaf:sum(($cc, $dd, iaf:numeric-multiply($ee, 0.035)))), 0.15), iaf:numeric-multiply((iaf:sum(($ff, $gg, iaf:numeric-multiply($hh, 0.035)))), 0.12), iaf:numeric-multiply($ii, 0.18), iaf:numeric-multiply($jj, 0.15), iaf:numeric-multiply($kk, 0.12))), 0))))), 3))");

            var item3_xbrl = xbrlparser.Translate(expr3);

        }
        
        public void Test()
        {
            var xbrlparser = new XbrlFormulaParser();
            var csparser = new CSharpParser();
            var expr1 = this.ParseExpression("if ($AccountingStandard = 'IFRS') then ($a = xs:QName('eba_AS:x2')) else (true())");
            var expr2 = this.ParseExpression("if ($ReportingLevel = 'con') then ($a = xs:QName('eba_SC:x7')) else (true())");
            var expr3 = this.ParseExpression("iaf:numeric-equal($a, iaf:numeric-divide((iaf:sum((iaf:max((iaf:sum((iaf:numeric-multiply($b, 0.18), iaf:numeric-multiply($c, 0.18), iaf:numeric-multiply($d, 0.12), iaf:numeric-multiply((iaf:sum(($e, $f, iaf:numeric-multiply($g, 0.035)))), 0.15), iaf:numeric-multiply((iaf:sum(($h, $i, iaf:numeric-multiply($j, 0.035)))), 0.12), iaf:numeric-multiply($k, 0.18), iaf:numeric-multiply($l, 0.15), iaf:numeric-multiply($m, 0.12))), 0)), iaf:max((iaf:sum((iaf:numeric-multiply($n, 0.18), iaf:numeric-multiply($o, 0.18), iaf:numeric-multiply($p, 0.12), iaf:numeric-multiply((iaf:sum(($q, $r, iaf:numeric-multiply($s, 0.035)))), 0.15), iaf:numeric-multiply((iaf:sum(($t, $u, iaf:numeric-multiply($v, 0.035)))), 0.12), iaf:numeric-multiply($w, 0.18), iaf:numeric-multiply($x, 0.15), iaf:numeric-multiply($y, 0.12))), 0)), iaf:max((iaf:sum((iaf:numeric-multiply($z, 0.18), iaf:numeric-multiply($aa, 0.18), iaf:numeric-multiply($bb, 0.12), iaf:numeric-multiply((iaf:sum(($cc, $dd, iaf:numeric-multiply($ee, 0.035)))), 0.15), iaf:numeric-multiply((iaf:sum(($ff, $gg, iaf:numeric-multiply($hh, 0.035)))), 0.12), iaf:numeric-multiply($ii, 0.18), iaf:numeric-multiply($jj, 0.15), iaf:numeric-multiply($kk, 0.12))), 0))))), 3))");

  

            var prs1 = GetParameters(expr1);
            var prs2 = GetParameters(expr2);
            var prs3 = GetParameters(expr3);

            var v1 = new LogicalModel.Validation.ValidationRule();
            v1.RootExpression = expr1;
            v1.ID = "v1";
            foreach (var p in prs1) 
            {
                var pm = new LogicalModel.Validation.ValidationParameter(p);
                pm.Type = LogicalModel.TypeEnum.Numeric;
                v1.Parameters.Add(pm);

            }

            var v2 = new LogicalModel.Validation.ValidationRule();
            v2.RootExpression = expr2;
            v2.ID = "v2";
            foreach (var p in prs2)
            {
                var pm = new LogicalModel.Validation.ValidationParameter(p);
                pm.Type = LogicalModel.TypeEnum.Numeric;
                v2.Parameters.Add(pm);

            }

            var v3 = new LogicalModel.Validation.ValidationRule();
            v3.RootExpression = expr3;
            v3.ID = "v3";
            foreach (var p in prs3)
            {
                var pm = new LogicalModel.Validation.ValidationParameter(p);
                pm.Type = LogicalModel.TypeEnum.Numeric;
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
