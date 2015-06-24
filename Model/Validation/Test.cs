using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicalModel.Expressions;
using LogicalModel.Base;
using LogicalModel.Validation;
using Utilities;
namespace LogicalModel.Validation
{
    public class ValidationsX : ValidationFunctionContainer
    {
        public ValidationsX()
        {
            this.FunctionDictionary.Add("content48", this.content48);
            this.FunctionDictionary.Add("content50", this.content50);
            this.FunctionDictionary.Add("content52", this.content52);
            this.FunctionDictionary.Add("content54", this.content54);
            this.FunctionDictionary.Add("content56", this.content56);
            this.FunctionDictionary.Add("content58", this.content58);
            this.FunctionDictionary.Add("content60", this.content60);
            this.FunctionDictionary.Add("content62", this.content62);
            this.FunctionDictionary.Add("content64", this.content64);
            this.FunctionDictionary.Add("content66", this.content66);
            this.FunctionDictionary.Add("content68", this.content68);
            this.FunctionDictionary.Add("content70", this.content70);
            this.FunctionDictionary.Add("content72", this.content72);
            this.FunctionDictionary.Add("content74", this.content74);
            this.FunctionDictionary.Add("content76", this.content76);
            this.FunctionDictionary.Add("content78", this.content78);
            this.FunctionDictionary.Add("content80", this.content80);
            this.FunctionDictionary.Add("content82", this.content82);
            this.FunctionDictionary.Add("content84", this.content84);
            this.FunctionDictionary.Add("S250108B7", this.S250108B7);
            this.FunctionDictionary.Add("S250110B7", this.S250110B7);
            this.FunctionDictionary.Add("S260104D82", this.S260104D82);
            this.FunctionDictionary.Add("S260106D82", this.S260106D82);
            this.FunctionDictionary.Add("S260204C3", this.S260204C3);
            this.FunctionDictionary.Add("S260206C3", this.S260206C3);
            this.FunctionDictionary.Add("S260304D92", this.S260304D92);
            this.FunctionDictionary.Add("S260306D92", this.S260306D92);
            this.FunctionDictionary.Add("S260404F16", this.S260404F16);
            this.FunctionDictionary.Add("S260406F16", this.S260406F16);
            this.FunctionDictionary.Add("S260504F13", this.S260504F13);
            this.FunctionDictionary.Add("S260506F13", this.S260506F13);
            this.FunctionDictionary.Add("S260604A4", this.S260604A4);
            this.FunctionDictionary.Add("S260606A4", this.S260606A4);
            this.FunctionDictionary.Add("S270104OJ33", this.S270104OJ33);
            this.FunctionDictionary.Add("S270106OJ33", this.S270106OJ33);

        }
        //$v_0 = (xs:QName('s2c_CN:x1'))
        public bool content48(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            var p_indicator = parameters.FirstOrDefault(i => i.Name == "indicator").ValuesWithTresholds;
            return p_v_0.In(functions.XS_QName("s2c_CN:x1"));
        }

        //$v_0 = (xs:QName('s2c_CN:x0'))
        public bool content50(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            return p_v_0.In(functions.XS_QName("s2c_CN:x0"));
        }

        //$v_0 = (xs:QName('s2c_CN:x0'))
        public bool content52(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            return p_v_0.In(functions.XS_QName("s2c_CN:x0"));
        }

        //$v_0 = (xs:QName('s2c_CN:x2'), xs:QName('s2c_CN:x3'))
        public bool content54(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            return p_v_0.In(functions.XS_QName("s2c_CN:x2"), functions.XS_QName("s2c_CN:x3"));
        }

        //$v_0 = (xs:QName('s2c_CN:x0'))
        public bool content56(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            return p_v_0.In(functions.XS_QName("s2c_CN:x0"));
        }

        //$v_0 = (xs:QName('s2c_CN:x2'), xs:QName('s2c_CN:x11'))
        public bool content58(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            return p_v_0.In(functions.XS_QName("s2c_CN:x2"), functions.XS_QName("s2c_CN:x11"));
        }

        //$v_0 = (xs:QName('s2c_CN:x2'), xs:QName('s2c_CN:x10'), xs:QName('s2c_CN:x11'))
        public bool content60(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            return p_v_0.In(functions.XS_QName("s2c_CN:x2"), functions.XS_QName("s2c_CN:x10"), functions.XS_QName("s2c_CN:x11"));
        }

        //$v_0 = (xs:QName('s2c_CN:x2'), xs:QName('s2c_CN:x10'), xs:QName('s2c_CN:x12'))
        public bool content62(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            return p_v_0.In(functions.XS_QName("s2c_CN:x2"), functions.XS_QName("s2c_CN:x10"), functions.XS_QName("s2c_CN:x12"));
        }

        //$v_0 = (xs:QName('s2c_CN:x2'), xs:QName('s2c_CN:x11'), xs:QName('s2c_CN:x13'))
        public bool content64(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            return p_v_0.In(functions.XS_QName("s2c_CN:x2"), functions.XS_QName("s2c_CN:x11"), functions.XS_QName("s2c_CN:x13"));
        }

        //$v_0 = (xs:QName('s2c_CN:x2'), xs:QName('s2c_CN:x11'), xs:QName('s2c_CN:x13'))
        public bool content66(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            return p_v_0.In(functions.XS_QName("s2c_CN:x2"), functions.XS_QName("s2c_CN:x11"), functions.XS_QName("s2c_CN:x13"));
        }

        //$v_0 = (xs:QName('s2c_CN:x2'), xs:QName('s2c_CN:x11'), xs:QName('s2c_CN:x13'))
        public bool content68(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            return p_v_0.In(functions.XS_QName("s2c_CN:x2"), functions.XS_QName("s2c_CN:x11"), functions.XS_QName("s2c_CN:x13"));
        }

        //$v_0 = (xs:QName('s2c_CN:x2'), xs:QName('s2c_CN:x11'), xs:QName('s2c_CN:x13'))
        public bool content70(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            return p_v_0.In(functions.XS_QName("s2c_CN:x2"), functions.XS_QName("s2c_CN:x11"), functions.XS_QName("s2c_CN:x13"));
        }

        //$v_0 = (xs:QName('s2c_CN:x2'), xs:QName('s2c_CN:x11'), xs:QName('s2c_CN:x13'))
        public bool content72(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            return p_v_0.In(functions.XS_QName("s2c_CN:x2"), functions.XS_QName("s2c_CN:x11"), functions.XS_QName("s2c_CN:x13"));
        }

        //$v_0 = (xs:QName('s2c_CN:x2'), xs:QName('s2c_CN:x11'), xs:QName('s2c_CN:x13'))
        public bool content74(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            return p_v_0.In(functions.XS_QName("s2c_CN:x2"), functions.XS_QName("s2c_CN:x11"), functions.XS_QName("s2c_CN:x13"));
        }

        //$v_0 = (xs:QName('s2c_CN:x2'), xs:QName('s2c_CN:x11'), xs:QName('s2c_CN:x13'))
        public bool content76(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            return p_v_0.In(functions.XS_QName("s2c_CN:x2"), functions.XS_QName("s2c_CN:x11"), functions.XS_QName("s2c_CN:x13"));
        }

        //$v_0 = (xs:QName('s2c_CN:x0'))
        public bool content78(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            return p_v_0.In(functions.XS_QName("s2c_CN:x0"));
        }

        //$v_0 = (xs:QName('s2c_CN:x0'))
        public bool content80(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            return p_v_0.In(functions.XS_QName("s2c_CN:x0"));
        }

        //$v_0 = (xs:QName('s2c_CN:x2'), xs:QName('s2c_CN:x14'))
        public bool content82(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            return p_v_0.In(functions.XS_QName("s2c_CN:x2"), functions.XS_QName("s2c_CN:x14"));
        }

        //$v_0 = (xs:QName('s2c_CN:x0'))
        public bool content84(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            return p_v_0.In(functions.XS_QName("s2c_CN:x0"));
        }

        //iaf:numeric-equal(($v_0), $v_1)
        public bool S250108B7(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            var p_v_1 = parameters.FirstOrDefault(i => i.Name == "v_1").ValueWithTreshold;
            return functions.N_Equals(p_v_0, p_v_1);
        }

        //iaf:numeric-equal(($v_0), $v_1)
        public bool S250110B7(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            var p_v_1 = parameters.FirstOrDefault(i => i.Name == "v_1").ValueWithTreshold;
            return functions.N_Equals(p_v_0, p_v_1);
        }

        //iaf:numeric-greater-equal-than(($v_0), 0)
        public bool S260104D82(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            return functions.N_GreaterEqual(p_v_0, 0m);
        }

        //iaf:numeric-greater-equal-than(($v_0), 0)
        public bool S260106D82(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            return functions.N_GreaterEqual(p_v_0, 0m);
        }

        //iaf:numeric-equal(($v_0), iaf:numeric-subtract($v_1,iaf:sum(($v_2, $v_3))))
        public bool S260204C3(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            var p_v_1 = parameters.FirstOrDefault(i => i.Name == "v_1").ValueWithTreshold;
            var p_v_2 = parameters.FirstOrDefault(i => i.Name == "v_2").ValueWithTreshold;
            var p_v_3 = parameters.FirstOrDefault(i => i.Name == "v_3").ValueWithTreshold;
            return functions.N_Equals(p_v_0, functions.N_Subtract(p_v_1, functions.sum(p_v_2, p_v_3)));
        }

        //iaf:numeric-equal(($v_0), iaf:numeric-subtract($v_1,iaf:sum(($v_2, $v_3))))
        public bool S260206C3(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            var p_v_1 = parameters.FirstOrDefault(i => i.Name == "v_1").ValueWithTreshold;
            var p_v_2 = parameters.FirstOrDefault(i => i.Name == "v_2").ValueWithTreshold;
            var p_v_3 = parameters.FirstOrDefault(i => i.Name == "v_3").ValueWithTreshold;
            return functions.N_Equals(p_v_0, functions.N_Subtract(p_v_1, functions.sum(p_v_2, p_v_3)));
        }

        //iaf:numeric-greater-equal-than(($v_0), 0)
        public bool S260304D92(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            return functions.N_GreaterEqual(p_v_0, 0m);
        }

        //iaf:numeric-greater-equal-than(($v_0), 0)
        public bool S260306D92(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            return functions.N_GreaterEqual(p_v_0, 0m);
        }

        //iaf:numeric-equal(($v_0), iaf:sum(($v_1, $v_2, $v_3, $v_4)))
        public bool S260404F16(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            var p_v_1 = parameters.FirstOrDefault(i => i.Name == "v_1").ValueWithTreshold;
            var p_v_2 = parameters.FirstOrDefault(i => i.Name == "v_2").ValueWithTreshold;
            var p_v_3 = parameters.FirstOrDefault(i => i.Name == "v_3").ValueWithTreshold;
            var p_v_4 = parameters.FirstOrDefault(i => i.Name == "v_4").ValueWithTreshold;
            return functions.N_Equals(p_v_0, functions.sum(p_v_1, p_v_2, p_v_3, p_v_4));
        }

        //iaf:numeric-equal(($v_0), iaf:sum(($v_1, $v_2, $v_3, $v_4)))
        public bool S260406F16(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            var p_v_1 = parameters.FirstOrDefault(i => i.Name == "v_1").ValueWithTreshold;
            var p_v_2 = parameters.FirstOrDefault(i => i.Name == "v_2").ValueWithTreshold;
            var p_v_3 = parameters.FirstOrDefault(i => i.Name == "v_3").ValueWithTreshold;
            var p_v_4 = parameters.FirstOrDefault(i => i.Name == "v_4").ValueWithTreshold;
            return functions.N_Equals(p_v_0, functions.sum(p_v_1, p_v_2, p_v_3, p_v_4));
        }

        //iaf:numeric-equal(($v_0), iaf:sum(($v_1, $v_2, $v_3, $v_4, $v_5, $v_6, $v_7, $v_8, $v_9, $v_10, $v_11, $v_12)))
        public bool S260504F13(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            var p_v_1 = parameters.FirstOrDefault(i => i.Name == "v_1").ValueWithTreshold;
            var p_v_2 = parameters.FirstOrDefault(i => i.Name == "v_2").ValueWithTreshold;
            var p_v_3 = parameters.FirstOrDefault(i => i.Name == "v_3").ValueWithTreshold;
            var p_v_4 = parameters.FirstOrDefault(i => i.Name == "v_4").ValueWithTreshold;
            var p_v_5 = parameters.FirstOrDefault(i => i.Name == "v_5").ValueWithTreshold;
            var p_v_6 = parameters.FirstOrDefault(i => i.Name == "v_6").ValueWithTreshold;
            var p_v_7 = parameters.FirstOrDefault(i => i.Name == "v_7").ValueWithTreshold;
            var p_v_8 = parameters.FirstOrDefault(i => i.Name == "v_8").ValueWithTreshold;
            var p_v_9 = parameters.FirstOrDefault(i => i.Name == "v_9").ValueWithTreshold;
            var p_v_10 = parameters.FirstOrDefault(i => i.Name == "v_10").ValueWithTreshold;
            var p_v_11 = parameters.FirstOrDefault(i => i.Name == "v_11").ValueWithTreshold;
            var p_v_12 = parameters.FirstOrDefault(i => i.Name == "v_12").ValueWithTreshold;
            return functions.N_Equals(p_v_0, functions.sum(p_v_1, p_v_2, p_v_3, p_v_4, p_v_5, p_v_6, p_v_7, p_v_8, p_v_9, p_v_10, p_v_11, p_v_12));
        }

        //iaf:numeric-equal(($v_0), iaf:sum(($v_1, $v_2, $v_3, $v_4, $v_5, $v_6, $v_7, $v_8, $v_9, $v_10, $v_11, $v_12)))
        public bool S260506F13(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            var p_v_1 = parameters.FirstOrDefault(i => i.Name == "v_1").ValueWithTreshold;
            var p_v_2 = parameters.FirstOrDefault(i => i.Name == "v_2").ValueWithTreshold;
            var p_v_3 = parameters.FirstOrDefault(i => i.Name == "v_3").ValueWithTreshold;
            var p_v_4 = parameters.FirstOrDefault(i => i.Name == "v_4").ValueWithTreshold;
            var p_v_5 = parameters.FirstOrDefault(i => i.Name == "v_5").ValueWithTreshold;
            var p_v_6 = parameters.FirstOrDefault(i => i.Name == "v_6").ValueWithTreshold;
            var p_v_7 = parameters.FirstOrDefault(i => i.Name == "v_7").ValueWithTreshold;
            var p_v_8 = parameters.FirstOrDefault(i => i.Name == "v_8").ValueWithTreshold;
            var p_v_9 = parameters.FirstOrDefault(i => i.Name == "v_9").ValueWithTreshold;
            var p_v_10 = parameters.FirstOrDefault(i => i.Name == "v_10").ValueWithTreshold;
            var p_v_11 = parameters.FirstOrDefault(i => i.Name == "v_11").ValueWithTreshold;
            var p_v_12 = parameters.FirstOrDefault(i => i.Name == "v_12").ValueWithTreshold;
            return functions.N_Equals(p_v_0, functions.sum(p_v_1, p_v_2, p_v_3, p_v_4, p_v_5, p_v_6, p_v_7, p_v_8, p_v_9, p_v_10, p_v_11, p_v_12));
        }

        //iaf:numeric-equal(($v_0), iaf:numeric-add(iaf:numeric-multiply(0.0045,iaf:max((0, iaf:numeric-subtract($v_1,$v_2)))),iaf:numeric-multiply(0.03,iaf:max((0, $v_3)))))
        public bool S260604A4(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            var p_v_1 = parameters.FirstOrDefault(i => i.Name == "v_1").ValueWithTreshold;
            var p_v_2 = parameters.FirstOrDefault(i => i.Name == "v_2").ValueWithTreshold;
            var p_v_3 = parameters.FirstOrDefault(i => i.Name == "v_3").ValueWithTreshold;
            return functions.N_Equals(p_v_0, functions.N_Add(functions.N_Multiply(0.0045m, functions.max(0m, functions.N_Subtract(p_v_1, p_v_2))), functions.N_Multiply(0.03m, functions.max(0m, p_v_3))));
        }

        //iaf:numeric-equal(($v_0), iaf:numeric-add(iaf:numeric-multiply(0.0045,iaf:max((0, iaf:numeric-subtract($v_1,$v_2)))),iaf:numeric-multiply(0.03,iaf:max((0, $v_3)))))
        public bool S260606A4(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            var p_v_1 = parameters.FirstOrDefault(i => i.Name == "v_1").ValueWithTreshold;
            var p_v_2 = parameters.FirstOrDefault(i => i.Name == "v_2").ValueWithTreshold;
            var p_v_3 = parameters.FirstOrDefault(i => i.Name == "v_3").ValueWithTreshold;
            return functions.N_Equals(p_v_0, functions.N_Add(functions.N_Multiply(0.0045m, functions.max(0m, functions.N_Subtract(p_v_1, p_v_2))), functions.N_Multiply(0.03m, functions.max(0m, p_v_3))));
        }

        //iaf:numeric-equal(($v_0), iaf:numeric-subtract($v_1,$v_2))
        public bool S270104OJ33(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            var p_v_1 = parameters.FirstOrDefault(i => i.Name == "v_1").ValueWithTreshold;
            var p_v_2 = parameters.FirstOrDefault(i => i.Name == "v_2").ValueWithTreshold;
            return functions.N_Equals(p_v_0, functions.N_Subtract(p_v_1, p_v_2));
        }

        //iaf:numeric-equal(($v_0), iaf:numeric-subtract($v_1,$v_2))
        public bool S270106OJ33(List<ValidationParameter> parameters)
        {
            var p_v_0 = parameters.FirstOrDefault(i => i.Name == "v_0").ValueWithTreshold;
            var p_v_1 = parameters.FirstOrDefault(i => i.Name == "v_1").ValueWithTreshold;
            var p_v_2 = parameters.FirstOrDefault(i => i.Name == "v_2").ValueWithTreshold;
            return functions.N_Equals(p_v_0, functions.N_Subtract(p_v_1, p_v_2));
        }


    }
}
