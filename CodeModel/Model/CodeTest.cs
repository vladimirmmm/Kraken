using CodeModel.Languages.CSharp;
using CodeModel.Languages.Xpath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeModel.Model
{
    public class CodeTest
    {
        String Xpathfile = @"C:\My\Developement\XTree-M\XTree-M\CodeModel\Languages\Xpath\Xpath.json";
        List<string> formulas = new List<string>() 
        { 
            "string-length(string(xfi:fact-typed-dimension-value($a,QName(\"http://www.boi.org.il/xbrl/dict/dim\",\"TDR\")))) <= 20",
            "matches($a, \"[\\d\\w]+(([_|\\.|\\-])?[\\d\\w]+)*@[\\d\\w]+(([_|\\.|\\-])?[\\d\\w]+)*\")",
            "if ($a ne 0) then (iaf:numeric-equal($b, iaf:sum(($c, $d, $e, iaf:numeric-unary-minus($a))))) else true()",
            "concat(month-from-date($a), \"-\", day-from-date($a)) = (\"3-31\" cast as xs:string) or concat(month-from-date($a), \"-\", day-from-date($a)) = (\"6-30\" cast as xs:string) or concat(month-from-date($a), \"-\", day-from-date($a)) = (\"9-30\" cast as xs:string) or concat(month-from-date($a), \"-\", day-from-date($a)) = (\"12-31\" cast as xs:string)",
            "if (string-length(string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\")))) = 9)  then ((substring(string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))),1,1) != \"8\")  and (substring(string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))),1,3) != \"999\")  and (string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) != \"111111111\")) else (true())",
            "$a = $b + $c",
            "(month-from-dateTime(xfi:period-instant(xfi:period($a))) = 4 and day-from-dateTime(xfi:period-instant(xfi:period($a))) = 1) or (month-from-dateTime(xfi:period-instant(xfi:period($a))) = 7 and day-from-dateTime(xfi:period-instant(xfi:period($a))) = 1) or (month-from-dateTime(xfi:period-instant(xfi:period($a))) = 10 and day-from-dateTime(xfi:period-instant(xfi:period($a))) = 1) or (month-from-dateTime(xfi:period-instant(xfi:period($a))) = 1 and day-from-dateTime(xfi:period-instant(xfi:period($a))) = 1)",
            "((floor($a div 100) = 1 or floor($a div 100) = 4 or floor($a div 100) = 7 or floor($a div 100) = 9)) and  ((floor(($a - floor($a div 100) * 100) div 10) = 0) or (floor(($a - floor($a div 100) * 100) div 10) = 1) or (floor(($a - floor($a div 100) * 100) div 10) = 2))",
            "iaf:numeric-greater-equal-than((for $i in $a return iaf:abs($i)), (for $i in $b return iaf:abs($i)))"
        };
        public CodeTest() 
        {

        }
        public void testxfg() 
        {
           // var str = " 솼 ";
            var str = " = <> ";
            var encodings = Encoding.GetEncodings();
            Encoding utf8 = Encoding.ASCII;
            byte[] utfBytes = utf8.GetBytes(str);
            var sb = new StringBuilder();
            foreach (var encoding in encodings) 
            {
              
                var enc = Encoding.GetEncoding(encoding.Name);
                var encinfo = String.Format("{0} [{1}]: ", enc.HeaderName, enc.CodePage);
                encinfo = encinfo.PadLeft(50, ' ');

                byte[] isoBytes = Encoding.Convert(utf8, enc, utfBytes);
                string msg = enc.GetString(isoBytes);
              
               

                /*
                var enc = Encoding.GetEncoding(encoding.Name);
                var sourbytes = enc.GetBytes(str);
                byte[] isoBytes = Encoding.Convert(enc, utf8, utfBytes);
                string msg = utf8.GetString(isoBytes);
       
                Console.WriteLine(encinfo + msg);
                */
                Console.WriteLine(encinfo + msg);
            }
        }
        public void textxx() 
        {
            var path = @"C:\Program Files (x86)\FRS Apps\ILA 440\ILA\GetMessages.csv";
            var path2 = @"C:\Program Files (x86)\FRS Apps\ILA 440\ILA\GetMessagesX.csv";
            String line;
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            var sb = new StringBuilder();
            while ((line = file.ReadLine()) != null)
            {
                var nl = line;
                nl = nl.Replace("\"", "");

                if (nl.Contains(",$RB"))
                {
                    var refb = Utilities.Strings.TextBetween(nl, ",$RB", ",");
                    nl = nl.Replace(",$RB" + refb + ",", ",\"$RB" + refb + "\",");
                }
                nl = nl.Replace(",POL,", ",POL,\"");
                nl = nl.Replace(",GER,", ",GER,\"");
                nl = nl.Replace(",FRA,", ",FRA,\"");
                nl = nl.Replace(",ENG,", ",ENG,\"");
                sb.AppendLine(nl);
            }

            file.Close();

            var content = sb.ToString();
            sb.Clear();
            //content = content.Replace("\"", "");
            content = content.Replace("\r\n", "\"\r\n");

            content = content.Replace(",\"\"\r\n", ",@#$1@");
            while (content.IndexOf("\"\"") > -1) 
            {
                content = content.Replace("\"\"", "\"");
            }
            
  
            content = content.Replace(",@#$1@", ",\"\"\r\n");
            System.IO.File.WriteAllText(path2, content);
        }

        public void getrules() 
        {
            var folder = @"C:\Users\vladimir.balacescu\AppData\Roaming\XbrlStudio\Taxonomies\";
            var files = System.IO.Directory.GetFiles(folder, "*.xml", System.IO.SearchOption.AllDirectories);
            var outputfile = folder + "allrules.txt";
            var sb = new StringBuilder();
            foreach (var file in files) 
            {
                if (file.IndexOf("\\val\\") > -1)
                {
                    try
                    {
                        var c = System.IO.File.ReadAllText(file);
                        var tests = Utilities.Strings.TextsBetween(c, " test=\"", "\"");
                        foreach (var t in tests)
                        {
                            sb.AppendLine("<<<<" + t + ">>>>");

                        }
                    }
                    catch (Exception ex) 
                    {

                    }
                }

            }
            System.IO.File.WriteAllText(outputfile, sb.ToString());
        }
        
        public void test_x()
        {
            var l_xpath = new XPathLanguage();
            var l_csharp = new CSharpLanguage();
            foreach (var formula in formulas)
            {
                var expr1 = l_xpath.Parser.GetExpression(formula);
                var formula2 = l_xpath.Materializer.Materialize(expr1);
                var expr2 = l_xpath.Parser.GetExpression(formula2);
                var charpformula1 = l_csharp.Materializer.Materialize(expr1);
                var charpformula2 = l_csharp.Materializer.Materialize(expr2);
                if (charpformula1 != charpformula2) 
                {
                    Console.WriteLine("Error in " + formula);
                }
            }
        }
        public void test_expr1() 
        {
            var syntax = new Syntax2();
            syntax.Load(Xpathfile);
            var sx = new XPathParser(syntax); 

            var x = new List<List<Glyph>>();
            
            x.Add(sx.ConvertToGlyphs("string-length(string(xfi:fact-typed-dimension-value($a,QName(\"http://www.boi.org.il/xbrl/dict/dim\",\"TDR\")))) <= 20"));
            x.Add(sx.ConvertToGlyphs("matches($a, \"[\\d\\w]+(([_|\\.|\\-])?[\\d\\w]+)*@[\\d\\w]+(([_|\\.|\\-])?[\\d\\w]+)*\")"));
            x.Add(sx.ConvertToGlyphs("if ($a ne 0) then (iaf:numeric-equal($b, iaf:sum(($c, $d, $e, iaf:numeric-unary-minus($a))))) else true()"));
            x.Add(sx.ConvertToGlyphs("concat(month-from-date($a), \"-\", day-from-date($a)) = (\"3-31\" cast as xs:string) or concat(month-from-date($a), \"-\", day-from-date($a)) = (\"6-30\" cast as xs:string) or concat(month-from-date($a), \"-\", day-from-date($a)) = (\"9-30\" cast as xs:string) or concat(month-from-date($a), \"-\", day-from-date($a)) = (\"12-31\" cast as xs:string)"));
            x.Add(sx.ConvertToGlyphs("if (string-length(string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\")))) = 9)  then ((substring(string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))),1,1) != \"8\")  and (substring(string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))),1,3) != \"999\")  and (string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) != \"111111111\")) else (true())"));
            x.Add(sx.ConvertToGlyphs("$a = $b + $c"));
            x.Add(sx.ConvertToGlyphs("(month-from-dateTime(xfi:period-instant(xfi:period($a))) = 4 and day-from-dateTime(xfi:period-instant(xfi:period($a))) = 1) or (month-from-dateTime(xfi:period-instant(xfi:period($a))) = 7 and day-from-dateTime(xfi:period-instant(xfi:period($a))) = 1) or (month-from-dateTime(xfi:period-instant(xfi:period($a))) = 10 and day-from-dateTime(xfi:period-instant(xfi:period($a))) = 1) or (month-from-dateTime(xfi:period-instant(xfi:period($a))) = 1 and day-from-dateTime(xfi:period-instant(xfi:period($a))) = 1)"));
            x.Add(sx.ConvertToGlyphs("((floor($a div 100) = 1 or floor($a div 100) = 4 or floor($a div 100) = 7 or floor($a div 100) = 9)) and  ((floor(($a - floor($a div 100) * 100) div 10) = 0) or (floor(($a - floor($a div 100) * 100) div 10) = 1) or (floor(($a - floor($a div 100) * 100) div 10) = 2))"));
            x.Add(sx.ConvertToGlyphs("iaf:numeric-greater-equal-than((for $i in $a return iaf:abs($i)), (for $i in $b return iaf:abs($i)))"));
            
            x.Add(sx.ConvertToGlyphs("not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700009848 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520041690 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700003825 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520031931 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700010085 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 513890368 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700013329 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520036658 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 644035024 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520000472 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700000862 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520013954 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700003817 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520014143 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700003957 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 510216054 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700003809 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 511076572 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700011612 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520022732 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700011620 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 570000745 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016082 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520044322 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016090 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520028283 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016108 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520005067 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016116 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520004078 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016124 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520027830 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016132 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520017070 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016140 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520024647 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016181 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 33260971 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016157 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520033093 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016165 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 513767079 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016173 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520003781 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016470 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 510313778 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016934 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520037565 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700017080 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520032285 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700017122 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 511984213 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700017130 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 511780793 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700017833 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 511325870 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700017957 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520033234 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700018096 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 513326439 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700007891 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 512480971 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700018955 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 511888356 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700019151 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 550212021 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700019714 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 550225510 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700020183 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520004896 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700020357 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 510947153 cast as xs:string))"));

      
        }
        public void test_expr2()
        {
            var syntax = new Syntax2();
            syntax.Load(Xpathfile);
            var sx = new XPathParser(syntax); 
            var x = new List<Expression>();

            x.Add(sx.GetExpression("if ($a ne 0) then (iaf:numeric-equal($b, iaf:sum(($c, $d, $e, iaf:numeric-unary-minus($a))))) else true()"));
            x.Add(sx.GetExpression("string-length(string(xfi:fact-typed-dimension-value($a,QName(\"http://www.boi.org.il/xbrl/dict/dim\",\"TDR\")))) <= 20"));
            x.Add(sx.GetExpression("iaf:numeric-greater-equal-than((for $i in $a return iaf:abs($i)), (for $i in $b return iaf:abs($i)))"));
            x.Add(sx.GetExpression("matches($a, \"[\\d\\w]+(([_|\\.|\\-])?[\\d\\w]+)*@[\\d\\w]+(([_|\\.|\\-])?[\\d\\w]+)*\")"));
            x.Add(sx.GetExpression("concat(month-from-date($a), \"-\", day-from-date($a)) = (\"3-31\" cast as xs:string) or concat(month-from-date($a), \"-\", day-from-date($a)) = (\"6-30\" cast as xs:string) or concat(month-from-date($a), \"-\", day-from-date($a)) = (\"9-30\" cast as xs:string) or concat(month-from-date($a), \"-\", day-from-date($a)) = (\"12-31\" cast as xs:string)"));
            x.Add(sx.GetExpression("if (string-length(string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\")))) = 9)  then ((substring(string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))),1,1) != \"8\")  and (substring(string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))),1,3) != \"999\")  and (string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) != \"111111111\")) else (true())"));
            x.Add(sx.GetExpression("$a = $b + $c"));
            x.Add(sx.GetExpression("not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700009848 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520041690 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700003825 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520031931 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700010085 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 513890368 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700013329 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520036658 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 644035024 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520000472 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700000862 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520013954 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700003817 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520014143 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700003957 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 510216054 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700003809 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 511076572 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700011612 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520022732 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700011620 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 570000745 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016082 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520044322 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016090 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520028283 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016108 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520005067 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016116 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520004078 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016124 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520027830 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016132 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520017070 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016140 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520024647 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016181 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 33260971 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016157 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520033093 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016165 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 513767079 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016173 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520003781 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016470 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 510313778 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700016934 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520037565 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700017080 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520032285 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700017122 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 511984213 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700017130 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 511780793 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700017833 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 511325870 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700017957 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520033234 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700018096 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 513326439 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700007891 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 512480971 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700018955 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 511888356 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700019151 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 550212021 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700019714 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 550225510 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700020183 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 520004896 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 700020357 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName(\"http://www.boi.org.il/xbrl/dict/dim\", \"TDD\"))) = 510947153 cast as xs:string))"));
            x.Add(sx.GetExpression("(month-from-dateTime(xfi:period-instant(xfi:period($a))) = 4 and day-from-dateTime(xfi:period-instant(xfi:period($a))) = 1) or (month-from-dateTime(xfi:period-instant(xfi:period($a))) = 7 and day-from-dateTime(xfi:period-instant(xfi:period($a))) = 1) or (month-from-dateTime(xfi:period-instant(xfi:period($a))) = 10 and day-from-dateTime(xfi:period-instant(xfi:period($a))) = 1) or (month-from-dateTime(xfi:period-instant(xfi:period($a))) = 1 and day-from-dateTime(xfi:period-instant(xfi:period($a))) = 1)"));
            x.Add(sx.GetExpression("((floor($a div 100) = 1 or floor($a div 100) = 4 or floor($a div 100) = 7 or floor($a div 100) = 9)) and  ((floor(($a - floor($a div 100) * 100) div 10) = 0) or (floor(($a - floor($a div 100) * 100) div 10) = 1) or (floor(($a - floor($a div 100) * 100) div 10) = 2))"));

        }
        public void test2() 
        {
            var separator = "\"";
            var t1 = "\"sdfdsf\"sdffds";
            var t2 = "1313\"sdfdsf\"sdffds";
            var t3 = "1313\"sdfdsf\"";
            var item1 = t1.Split(new string[] { separator }, StringSplitOptions.None);

            var item2 = t2.Split(new string[] { separator }, StringSplitOptions.None);

            var item3 = t3.Split(new string[] { separator }, StringSplitOptions.None);

        }
    }
}
