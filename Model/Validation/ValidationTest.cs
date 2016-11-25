//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using LogicalModel.Expressions;
//using LogicalModel.Base;
//using LogicalModel.Validation;
//using Utilities;
//namespace LogicalModel.Validation
//{
//    public class ValidationsX : ValidationFunctionContainer
//    {
//        public ValidationsX()
//        {
//            this.FunctionDictionary.Add("boiv900f", this.boiv900f);
//            this.FunctionDictionary.Add("boiv901f", this.boiv901f);
//            this.FunctionDictionary.Add("boiv902f", this.boiv902f);
//            this.FunctionDictionary.Add("boiv903f", this.boiv903f);
//            this.FunctionDictionary.Add("boiv904f", this.boiv904f);
//            this.FunctionDictionary.Add("boiv905f", this.boiv905f);
//            this.FunctionDictionary.Add("boiv906f", this.boiv906f);
//            this.FunctionDictionary.Add("boiv907f", this.boiv907f);
//            this.FunctionDictionary.Add("boiv908f", this.boiv908f);
//            this.FunctionDictionary.Add("boiv909f", this.boiv909f);
//            this.FunctionDictionary.Add("boiv910f", this.boiv910f);
//            this.FunctionDictionary.Add("boiv911f", this.boiv911f);
//            this.FunctionDictionary.Add("boiv912f", this.boiv912f);
//            this.FunctionDictionary.Add("boiv913f", this.boiv913f);
//            this.FunctionDictionary.Add("boiv914f", this.boiv914f);
//            this.FunctionDictionary.Add("boiv915f", this.boiv915f);
//            this.FunctionDictionary.Add("boiv916f", this.boiv916f);
//            this.FunctionDictionary.Add("boiv917f", this.boiv917f);
//            this.FunctionDictionary.Add("boiv918f", this.boiv918f);
//            this.FunctionDictionary.Add("boiv919f", this.boiv919f);
//            this.FunctionDictionary.Add("boiv920f", this.boiv920f);
//            this.FunctionDictionary.Add("boiv921f", this.boiv921f);
//            this.FunctionDictionary.Add("boiv922f", this.boiv922f);
//            this.FunctionDictionary.Add("boiv923f", this.boiv923f);
//            this.FunctionDictionary.Add("boiv924f", this.boiv924f);
//            this.FunctionDictionary.Add("boiv925f", this.boiv925f);
//            this.FunctionDictionary.Add("boiv926f", this.boiv926f);
//            this.FunctionDictionary.Add("boiv927f", this.boiv927f);
//            this.FunctionDictionary.Add("boiv928f", this.boiv928f);
//            this.FunctionDictionary.Add("boiv929f", this.boiv929f);
//            this.FunctionDictionary.Add("boiv930f", this.boiv930f);
//            this.FunctionDictionary.Add("boiv931f", this.boiv931f);
//            this.FunctionDictionary.Add("boiv932f", this.boiv932f);
//            this.FunctionDictionary.Add("boiv933f", this.boiv933f);
//            this.FunctionDictionary.Add("boiv934f", this.boiv934f);
//            this.FunctionDictionary.Add("boiv935f", this.boiv935f);
//            this.FunctionDictionary.Add("boiv936f", this.boiv936f);
//            this.FunctionDictionary.Add("boiv937f", this.boiv937f);
//            this.FunctionDictionary.Add("boiv938f", this.boiv938f);
//            this.FunctionDictionary.Add("boiv939f", this.boiv939f);
//            this.FunctionDictionary.Add("boiv940f", this.boiv940f);
//            this.FunctionDictionary.Add("boiv941f", this.boiv941f);
//            this.FunctionDictionary.Add("boiv942f", this.boiv942f);
//            this.FunctionDictionary.Add("boiv943f", this.boiv943f);
//            this.FunctionDictionary.Add("boiv944f", this.boiv944f);
//            this.FunctionDictionary.Add("boiv945f", this.boiv945f);
//            this.FunctionDictionary.Add("boiv946f", this.boiv946f);

//        }
//        //string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDR")))) <= 20
//        public bool boiv900f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR")))) <= 20m;
//        }

//        //(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDR")))) = 5 and  ((number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),1,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),2,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),2,1)) * 2),2,1))),"NaN","000"))  +  number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),3,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),4,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),4,1)) * 2),2,1))),"NaN","000"))) mod 10 = 0) and (number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),5,1)) = 0)) or (string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDR")))) = 5 and ((10 - ((number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),1,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),2,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),2,1)) * 2),2,1))),"NaN","000"))  +  number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),3,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),4,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),4,1)) * 2),2,1))),"NaN","000"))) mod 10)) = number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),5,1)))) or (string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDR")))) = 6 and ((number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),1,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),1,1)) * 2),2,1))),"NaN","000"))  +  number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),2,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),3,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),3,1)) * 2),2,1))),"NaN","000")) +  number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),4,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),5,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),5,1)) * 2),2,1))),"NaN","000"))) mod 10 = 0) and (number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),6,1)) = 0)) or (string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDR")))) = 6 and ((10 - ((number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),1,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),1,1)) * 2),2,1))),"NaN","000"))  +  number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),2,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),3,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),3,1)) * 2),2,1))),"NaN","000")) +  number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),4,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),5,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),5,1)) * 2),2,1))),"NaN","000"))) mod 10)) = number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),6,1)))) or (string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDR")))) = 7 and ((number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),1,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),2,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),2,1)) * 2),2,1))),"NaN","000"))  +  number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),3,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),4,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),4,1)) * 2),2,1))),"NaN","000")) +  number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),5,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),6,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),6,1)) * 2),2,1))),"NaN","000"))) mod 10 = 0) and (number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),7,1)) = 0)) or (string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDR")))) = 7 and ((10 - ((number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),1,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),2,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),2,1)) * 2),2,1))),"NaN","000"))  +  number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),3,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),4,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),4,1)) * 2),2,1))),"NaN","000")) +  number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),5,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),6,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),6,1)) * 2),2,1))),"NaN","000"))) mod 10)) = number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),7,1)))) or (string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDR")))) = 8 and ((number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),1,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),1,1)) * 2),2,1))),"NaN","000"))  +  number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),2,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),3,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),3,1)) * 2),2,1))),"NaN","000")) +  number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),4,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),5,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),5,1)) * 2),2,1))),"NaN","000")) + number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),6,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),7,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),7,1)) * 2),2,1))),"NaN","000"))) mod 10 = 0) and (number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),8,1)) = 0)) or (string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDR")))) = 8 and ((10 - ((number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),1,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),1,1)) * 2),2,1))),"NaN","000"))  +  number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),2,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),3,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),3,1)) * 2),2,1))),"NaN","000")) +  number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),4,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),5,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),5,1)) * 2),2,1))),"NaN","000")) + number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),6,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),7,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),7,1)) * 2),2,1))),"NaN","000"))) mod 10)) = number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),8,1)))) or (string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDR")))) = 9 and ((number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),1,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),2,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),2,1)) * 2),2,1))),"NaN","000"))  +  number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),3,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),4,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),4,1)) * 2),2,1))),"NaN","000")) +  number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),5,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),6,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),6,1)) * 2),2,1))),"NaN","000")) + number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),7,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),8,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),8,1)) * 2),2,1))),"NaN","000"))) mod 10 = 0) and (number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),9,1)) = 0)) or (string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDR")))) = 9 and ((10 - ((number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),1,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),2,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),2,1)) * 2),2,1))),"NaN","000"))  +  number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),3,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),4,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),4,1)) * 2),2,1))),"NaN","000")) +  number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),5,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),6,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),6,1)) * 2),2,1))),"NaN","000")) + number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),7,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),8,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),8,1)) * 2),2,1))),"NaN","000"))) mod 10)) = number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))),9,1))))
//        public bool boiv901f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR")))) == 5m 
//                 & functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 1m, 1m)) 
//                    + functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 3m, 1m)) == 0m 
//                & functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 5m, 1m)) == 0m | functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR")))) == 5m 
//                & 10m - functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 1m, 1m)) + functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 3m, 1m)) == functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 5m, 1m)) | functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR")))) == 6m 
//                & functions.Number(functions.Substring(functions.String(functions.XS_Integer(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 1m, 1m)) * 2m), 1m, 1m)) + functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 2m, 1m)) + functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 4m, 1m)) == 0m 
//                & functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 6m, 1m)) == 0m | functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR")))) == 6m 
//                & 10m - functions.Number(functions.Substring(functions.String(functions.XS_Integer(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 1m, 1m)) * 2m), 1m, 1m)) + functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 2m, 1m)) + functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 4m, 1m)) == functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 6m, 1m)) | functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR")))) == 7m 
//                & functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 1m, 1m)) + functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 3m, 1m)) + functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 5m, 1m)) == 0m 
//                & functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 7m, 1m)) == 0m | functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR")))) == 7m 
//                & 10m - functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 1m, 1m)) + functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 3m, 1m)) + functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 5m, 1m)) == functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 7m, 1m)) | functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR")))) == 8m 
//                & functions.Number(functions.Substring(functions.String(functions.XS_Integer(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 1m, 1m)) * 2m), 1m, 1m)) + functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 2m, 1m)) + functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 4m, 1m)) == 0m 
//                & functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 8m, 1m)) == 0m | functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR")))) == 8m 
//                & 10m - functions.Number(functions.Substring(functions.String(functions.XS_Integer(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 1m, 1m)) * 2m), 1m, 1m)) + functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 2m, 1m)) + functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 4m, 1m)) == functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 8m, 1m)) | functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR")))) == 9m 
//                & functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 1m, 1m)) + functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 3m, 1m)) + functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 5m, 1m)) == 0m 
//                & functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 9m, 1m)) == 0m | functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR")))) == 9m 
//                & 10m - functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 1m, 1m)) + functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 3m, 1m)) + functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 5m, 1m)) == functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDR"))), 9m, 1m));
//        }

//        //string-length($a) <= 200
//        public bool boiv902f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(p_a) <= 200m;
//        }

//        //string-length($a) <= 40
//        public bool boiv903f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(p_a) <= 40m;
//        }

//        //string-length($a) <= 40
//        public bool boiv904f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(p_a) <= 40m;
//        }

//        //string-length($a) <= 100
//        public bool boiv905f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(p_a) <= 100m;
//        }

//        //string-length($a) <= 10
//        public bool boiv906f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(p_a) <= 10m;
//        }

//        //matches(string($a), "^[0-9]{7}$")
//        public bool boiv907f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.RegexpMatches(functions.String(p_a), "^[0-9]{7}$");
//        }

//        //string-length($a) <= 15
//        public bool boiv908f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(p_a) <= 15m;
//        }

//        //string-length($a) <= 40
//        public bool boiv909f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(p_a) <= 40m;
//        }

//        //matches($a, "[\d\w]+(([_|\.|\-])?[\d\w]+)*@[\d\w]+(([_|\.|\-])?[\d\w]+)*")
//        public bool boiv910f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.RegexpMatches(p_a, "[\\d\\w]+(([_|\\.|\\-])?[\\d\\w]+)*@[\\d\\w]+(([_|\\.|\\-])?[\\d\\w]+)*");
//        }

//        //string-length($a) <= 40
//        public bool boiv911f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(p_a) <= 40m;
//        }

//        //string-length($a) <= 40
//        public bool boiv912f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(p_a) <= 40m;
//        }

//        //string-length($a) <= 100
//        public bool boiv913f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(p_a) <= 100m;
//        }

//        //string-length($a) <= 100
//        public bool boiv914f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(p_a) <= 100m;
//        }

//        //string-length($a) <= 10
//        public bool boiv915f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(p_a) <= 10m;
//        }

//        //string-length($a) <= 200
//        public bool boiv916f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(p_a) <= 200m;
//        }

//        //matches(string($a), "^[0-9]{7}$")
//        public bool boiv917f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.RegexpMatches(functions.String(p_a), "^[0-9]{7}$");
//        }

//        //string-length($a) <= 40
//        public bool boiv918f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(p_a) <= 40m;
//        }

//        //xs:date($a) >= xs:date($b)
//        public bool boiv919f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.XS_Date(p_a) >= functions.XS_Date(p_b);
//        }

//        //string-length($a) <= 240
//        public bool boiv920f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(p_a) <= 240m;
//        }

//        //(($a = false()) and ((not(empty($c)) and not(empty($d)) and not(empty($e)) and not(empty($e)) and not(empty($f))) and empty($b))) or (($a = true()) and ((empty($c)) and (empty($d)) and (empty($e)) and (empty($e)) and (empty($f))) and not(empty($b)))
//        public bool boiv921f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            var p_d = parameters.FirstOrDefault(i => i.Name == "d");
//            var p_e = parameters.FirstOrDefault(i => i.Name == "e");
//            var p_f = parameters.FirstOrDefault(i => i.Name == "f");
//            var p_g = parameters.FirstOrDefault(i => i.Name == "g");
//            return p_a == false & functions.not(functions.empty(p_c)) & functions.not(functions.empty(p_d)) & functions.not(functions.empty(p_e)) & functions.not(functions.empty(p_e)) & functions.not(functions.empty(p_f)) & functions.empty(p_b) | p_a == true & functions.empty(p_c) & functions.empty(p_d) & functions.empty(p_e) & functions.empty(p_e) & functions.empty(p_f) & functions.not(functions.empty(p_b));
//        }

//        //string-length($a) <= 40
//        public bool boiv922f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(p_a) <= 40m;
//        }

//        //false()
//        public bool boiv923f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return false;
//        }

//        //false()
//        public bool boiv924f(List<ValidationParameter> parameters)
//        {
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            var p_d = parameters.FirstOrDefault(i => i.Name == "d");
//            var p_e = parameters.FirstOrDefault(i => i.Name == "e");
//            var p_f = parameters.FirstOrDefault(i => i.Name == "f");
//            var p_g = parameters.FirstOrDefault(i => i.Name == "g");
//            return false;
//        }

//        //false()
//        public bool boiv925f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return false;
//        }

//        //matches($a, "^(([0-9]{2}\-[0-9]{7})|((1)\-[0-9]{3}\-[0-9]{3}\-[0-9]{3})|((0)[0-9]{2}\-[0-9]{7})|(\*[0-9]{4}))$")
//        public bool boiv926f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.RegexpMatches(p_a, "^(([0-9]{2}\\-[0-9]{7})|((1)\\-[0-9]{3}\\-[0-9]{3}\\-[0-9]{3})|((0)[0-9]{2}\\-[0-9]{7})|(\\*[0-9]{4}))$");
//        }

//        //matches($a, "^(([0-9]{2}\-[0-9]{7})|((1)\-[0-9]{3}\-[0-9]{3}\-[0-9]{3})|((0)[0-9]{2}\-[0-9]{7})|(\*[0-9]{4}))$")
//        public bool boiv927f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.RegexpMatches(p_a, "^(([0-9]{2}\\-[0-9]{7})|((1)\\-[0-9]{3}\\-[0-9]{3}\\-[0-9]{3})|((0)[0-9]{2}\\-[0-9]{7})|(\\*[0-9]{4}))$");
//        }

//        //matches($a, "^(([0-9]{2}\-[0-9]{7})|((1)\-[0-9]{3}\-[0-9]{3}\-[0-9]{3})|((0)[0-9]{2}\-[0-9]{7})|(\*[0-9]{4}))$")
//        public bool boiv928f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.RegexpMatches(p_a, "^(([0-9]{2}\\-[0-9]{7})|((1)\\-[0-9]{3}\\-[0-9]{3}\\-[0-9]{3})|((0)[0-9]{2}\\-[0-9]{7})|(\\*[0-9]{4}))$");
//        }

//        //matches($a, "^(([0-9]{2}\-[0-9]{7})|((1)\-[0-9]{3}\-[0-9]{3}\-[0-9]{3})|((0)[0-9]{2}\-[0-9]{7})|(\*[0-9]{4}))$")
//        public bool boiv929f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.RegexpMatches(p_a, "^(([0-9]{2}\\-[0-9]{7})|((1)\\-[0-9]{3}\\-[0-9]{3}\\-[0-9]{3})|((0)[0-9]{2}\\-[0-9]{7})|(\\*[0-9]{4}))$");
//        }

//        //not(empty($a))
//        public bool boiv930f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.not(functions.empty(p_a));
//        }

//        //not(empty($a))
//        public bool boiv931f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.not(functions.empty(p_a));
//        }

//        //not(empty($a))
//        public bool boiv932f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.not(functions.empty(p_a));
//        }

//        //not(empty($a))
//        public bool boiv933f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.not(functions.empty(p_a));
//        }

//        //not(empty($b))
//        public bool boiv934f(List<ValidationParameter> parameters)
//        {
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.empty(p_b));
//        }

//        //not(empty($a))
//        public bool boiv935f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.not(functions.empty(p_a));
//        }

//        //not(empty($a))
//        public bool boiv936f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.not(functions.empty(p_a));
//        }

//        //not(empty($a))
//        public bool boiv937f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.not(functions.empty(p_a));
//        }

//        //not(empty($a))
//        public bool boiv938f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.not(functions.empty(p_a));
//        }

//        //not(empty($a))
//        public bool boiv939f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.not(functions.empty(p_a));
//        }

//        //not(empty($a))
//        public bool boiv940f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.not(functions.empty(p_a));
//        }

//        //not(empty($a))
//        public bool boiv941f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.not(functions.empty(p_a));
//        }

//        //not(empty($a))
//        public bool boiv942f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.not(functions.empty(p_a));
//        }

//        //not(empty($a))
//        public bool boiv943f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.not(functions.empty(p_a));
//        }

//        //not(empty($a))
//        public bool boiv944f(List<ValidationParameter> parameters)
//        {
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.empty(p_a));
//        }

//        //not(empty($a))
//        public bool boiv945f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.not(functions.empty(p_a));
//        }

//        //not(empty($a))
//        public bool boiv946f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.not(functions.empty(p_a));
//        }


//    }
//}
