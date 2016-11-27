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
                strings.Add(m.Value);
                fixedstring = fixedstring.Remove(m.Index, m.Value.Length);
                fixedstring = fixedstring.Insert(m.Index, "\"#!" + mix + "\"");
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
            fixedstring = fixedstring.Replace("@ ", "@-").Replace(" @", "-@");
            fixedstring = fixedstring.Replace(" ", "");
            fixedstring = fixedstring.Replace("&spc_", " ");
            fixedstring = fixedstring.Replace("@-", "@ ").Replace("-@", " @");

            for (int i = 0; i < strings.Count; i++)
            {
                fixedstring = fixedstring.Replace("\"#!" + i + "\"", strings[i]);
            }


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

            return expression;
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
                isinstring=true;
            }
            if (trimmeditem.StartsWith(Syntax.StringDelimiter2) && trimmeditem.EndsWith(Syntax.StringDelimiter2))
            {
                isinstring=true;
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
                    string p_false="";
                    if (item.IndexOf(Syntax.Else + Syntax.ExpressionContainer_Left) == -1) 
                    {
                        var elsix =item.IndexOf(Syntax.Else);
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
            //if (expression.truepath.SubExpressions.Count == 0) 
            //{
            //    expression.truepath.SubExpressions.Add(expression.SubExpressions[1]);
            //}
            sb.Append(String.Format(format, Translate(expression.condition), Translate(expression.truepath), Translate(expression.falsepath)));
            return sb.ToString();
        }

        #endregion
        
        public void Test2()
        {
            //matches($a, "[\d\w]+(([_|\.|\-])?[\d\w]+)*@[\d\w]+(([_|\.|\-])?[\d\w]+)*")
            Testexpression("string-length(string(xfi:fact-typed-dimension-value($a,QName(\"http://www.boi.org.il/xbrl/dict/dim\",\"TDR\")))) <= 20");
            Testexpression("matches($a, \"[\\d\\w]+(([_|\\.|\\-])?[\\d\\w]+)*@[\\d\\w]+(([_|\\.|\\-])?[\\d\\w]+)*\")");
            Testexpression("if ($a ne 0) then (iaf:numeric-equal($b, iaf:sum(($c, $d, $e, iaf:numeric-unary-minus($a))))) else true()");
            Testexpression("concat(month-from-date($a), \"-\", day-from-date($a)) = (\"3-31\" cast as xs:string) or concat(month-from-date($a), \"-\", day-from-date($a)) = (\"6-30\" cast as xs:string) or concat(month-from-date($a), \"-\", day-from-date($a)) = (\"9-30\" cast as xs:string) or concat(month-from-date($a), \"-\", day-from-date($a)) = (\"12-31\" cast as xs:string)");
            Testexpression("if (string-length(string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\")))) = 9)  then ((substring(string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))),1,1) != \"8\")  and (substring(string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))),1,3) != \"999\")  and (string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) != \"111111111\")) else (true())");
            Testexpression("$a = $b + $c");
            Testexpression("not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700009848 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520041690 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700003825 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520031931 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700010085 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 513890368 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700013329 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520036658 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 644035024 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520000472 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700000862 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520013954 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700003817 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520014143 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700003957 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 510216054 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700003809 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 511076572 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700011612 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520022732 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700011620 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 570000745 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016082 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520044322 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016090 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520028283 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016108 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520005067 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016116 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520004078 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016124 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520027830 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016132 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520017070 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016140 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520024647 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016181 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 33260971 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016157 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520033093 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016165 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 513767079 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016173 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520003781 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016470 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 510313778 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016934 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520037565 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700017080 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520032285 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700017122 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 511984213 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700017130 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 511780793 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700017833 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 511325870 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700017957 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520033234 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700018096 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 513326439 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700007891 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 512480971 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700018955 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 511888356 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700019151 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 550212021 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700019714 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 550225510 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700020183 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520004896 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700020357 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 510947153 cast as xs:string))");
            Testexpression("(month-from-dateTime(xfi:period-instant(xfi:period($a))) = 4 and day-from-dateTime(xfi:period-instant(xfi:period($a))) = 1) or (month-from-dateTime(xfi:period-instant(xfi:period($a))) = 7 and day-from-dateTime(xfi:period-instant(xfi:period($a))) = 1) or (month-from-dateTime(xfi:period-instant(xfi:period($a))) = 10 and day-from-dateTime(xfi:period-instant(xfi:period($a))) = 1) or (month-from-dateTime(xfi:period-instant(xfi:period($a))) = 1 and day-from-dateTime(xfi:period-instant(xfi:period($a))) = 1)");
            Testexpression("((floor($a div 100) = 1 or floor($a div 100) = 4 or floor($a div 100) = 7 or floor($a div 100) = 9)) and  ((floor(($a - floor($a div 100) * 100) div 10) = 0) or (floor(($a - floor($a div 100) * 100) div 10) = 1) or (floor(($a - floor($a div 100) * 100) div 10) = 2))");
      
        }

        public void Testexpression(string expr) 
        {
            var xbrlparser = new XbrlFormulaParser();
            var csparser = new CSharpParser();
       
            var expr3 = this.ParseExpression(expr);
            var item3_xbrl = xbrlparser.Translate(expr3);
            var v4 = new LogicalModel.Validation.ValidationRule();
            v4.RootExpression = expr3;
            v4.ID = "v3";
            var item4_xbrl = xbrlparser.Translate(expr3);
            var item4_cs = csparser.GetFunction(v4);
        }
        
        public void Test()
        {
            var xbrlparser = new XbrlFormulaParser();
            var csparser = new CSharpParser();
            var expr1 = this.ParseExpression(" $a = (xs:QName('eba_NC:A'), xs:QName('eba_NC:B'))");
            var expr2 = this.ParseExpression("if ($ReportingLevel = 'con') then ($a = xs:QName('eba_SC:x7')) else (true())");
            var expr3 = this.ParseExpression("iaf:numeric-equal($a, iaf:numeric-divide((iaf:sum((iaf:max((iaf:sum((iaf:numeric-multiply($b, 0.18), iaf:numeric-multiply($c, 0.18), iaf:numeric-multiply($d, 0.12), iaf:numeric-multiply((iaf:sum(($e, $f, iaf:numeric-multiply($g, 0.035)))), 0.15), iaf:numeric-multiply((iaf:sum(($h, $i, iaf:numeric-multiply($j, 0.035)))), 0.12), iaf:numeric-multiply($k, 0.18), iaf:numeric-multiply($l, 0.15), iaf:numeric-multiply($m, 0.12))), 0)), iaf:max((iaf:sum((iaf:numeric-multiply($n, 0.18), iaf:numeric-multiply($o, 0.18), iaf:numeric-multiply($p, 0.12), iaf:numeric-multiply((iaf:sum(($q, $r, iaf:numeric-multiply($s, 0.035)))), 0.15), iaf:numeric-multiply((iaf:sum(($t, $u, iaf:numeric-multiply($v, 0.035)))), 0.12), iaf:numeric-multiply($w, 0.18), iaf:numeric-multiply($x, 0.15), iaf:numeric-multiply($y, 0.12))), 0)), iaf:max((iaf:sum((iaf:numeric-multiply($z, 0.18), iaf:numeric-multiply($aa, 0.18), iaf:numeric-multiply($bb, 0.12), iaf:numeric-multiply((iaf:sum(($cc, $dd, iaf:numeric-multiply($ee, 0.035)))), 0.15), iaf:numeric-multiply((iaf:sum(($ff, $gg, iaf:numeric-multiply($hh, 0.035)))), 0.12), iaf:numeric-multiply($ii, 0.18), iaf:numeric-multiply($jj, 0.15), iaf:numeric-multiply($kk, 0.12))), 0))))), 3))");


            var expr4 = this.ParseExpression("if ($a = xs:QName('s2c_CN:x1')) then iaf:numeric-equal($b,$c) else (true())");
            var v4 = new LogicalModel.Validation.ValidationRule();
            v4.RootExpression = expr3;
            v4.ID = "v3";
            var item4_xbrl = xbrlparser.Translate(expr4);
            var item4_cs = csparser.GetFunction(v4);

            var prs1 = GetParameters(expr1);
            var prs2 = GetParameters(expr2);
            var prs3 = GetParameters(expr3);

            var v1 = new LogicalModel.Validation.ValidationRule();
            v1.RootExpression = expr1;
            v1.ID = "v1";
            foreach (var p in prs1) 
            {
                var pm = new LogicalModel.Validation.ValidationParameter(p, v1.ID);
                pm.Type = LogicalModel.TypeEnum.Numeric;
                v1.Parameters.Add(pm);

            }

            var v2 = new LogicalModel.Validation.ValidationRule();
            v2.RootExpression = expr2;
            v2.ID = "v2";
            foreach (var p in prs2)
            {
                var pm = new LogicalModel.Validation.ValidationParameter(p, v2.ID);
                pm.Type = LogicalModel.TypeEnum.Numeric;
                v2.Parameters.Add(pm);

            }

            var v3 = new LogicalModel.Validation.ValidationRule();
            v3.RootExpression = expr3;
            v3.ID = "v3";
            foreach (var p in prs3)
            {
                var pm = new LogicalModel.Validation.ValidationParameter(p,v3.ID);
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
