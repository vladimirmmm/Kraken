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
            //matches($a, "^(1|1,2|1,2,3|2|2,3|3)$")
            Testexpression("matches($a, \"^(1|1,2|1,2,3|2|2,3|3)$\")");
            Testexpression("matches(string(xfi:fact-typed-dimension-value($a,QName(&quot;http://eiopa.europa.eu/xbrl/s2c/dict/dim&quot;,&quot;UI&quot;))/s2c_typ:ID), &quot;^ISIN/[A-Z0-9]{12}$&quot;)  or &#xD;&#xA;matches(string(xfi:fact-typed-dimension-value($a,QName(&quot;http://eiopa.europa.eu/xbrl/s2c/dict/dim&quot;,&quot;UI&quot;))/s2c_typ:ID), &quot;^CUSIP/.*&quot;)  or &#xD;&#xA;matches(string(xfi:fact-typed-dimension-value($a,QName(&quot;http://eiopa.europa.eu/xbrl/s2c/dict/dim&quot;,&quot;UI&quot;))/s2c_typ:ID), &quot;^SEDOL/.*&quot;)  or &#xD;&#xA;matches(string(xfi:fact-typed-dimension-value($a,QName(&quot;http://eiopa.europa.eu/xbrl/s2c/dict/dim&quot;,&quot;UI&quot;))/s2c_typ:ID), &quot;^WKN/.*&quot;)  or &#xD;&#xA;matches(string(xfi:fact-typed-dimension-value($a,QName(&quot;http://eiopa.europa.eu/xbrl/s2c/dict/dim&quot;,&quot;UI&quot;))/s2c_typ:ID), &quot;^BT/.*&quot;)  or &#xD;&#xA;matches(string(xfi:fact-typed-dimension-value($a,QName(&quot;http://eiopa.europa.eu/xbrl/s2c/dict/dim&quot;,&quot;UI&quot;))/s2c_typ:ID), &quot;^BBGID/.*&quot;)  or &#xD;&#xA;matches(string(xfi:fact-typed-dimension-value($a,QName(&quot;http://eiopa.europa.eu/xbrl/s2c/dict/dim&quot;,&quot;UI&quot;))/s2c_typ:ID), &quot;^RIC/.*&quot;)  or &#xD;&#xA;matches(string(xfi:fact-typed-dimension-value($a,QName(&quot;http://eiopa.europa.eu/xbrl/s2c/dict/dim&quot;,&quot;UI&quot;))/s2c_typ:ID), &quot;^FIGI/.*&quot;)  or &#xD;&#xA;matches(string(xfi:fact-typed-dimension-value($a,QName(&quot;http://eiopa.europa.eu/xbrl/s2c/dict/dim&quot;,&quot;UI&quot;))/s2c_typ:ID), &quot;^OCANNA/.*&quot;)  or &#xD;&#xA;matches(string(xfi:fact-typed-dimension-value($a,QName(&quot;http://eiopa.europa.eu/xbrl/s2c/dict/dim&quot;,&quot;UI&quot;))/s2c_typ:ID), &quot;^CAU/.*&quot;) or nilled(xfi:fact-typed-dimension-value($a,QName(&quot;http://eiopa.europa.eu/xbrl/s2c/dict/dim&quot;,&quot;UI&quot;))/s2c_typ:ID)");
            Testexpression("iaf:numeric-greater-equal-than((for $i in $a return iaf:abs($i)), (for $i in $b return iaf:abs($i)))");
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
            var expr0 = this.ParseExpression(" $a = (xs:QName('eba_NC:A'), xs:QName('eba_NC:B'))");
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
