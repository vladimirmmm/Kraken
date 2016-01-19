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
//            this.FunctionDictionary.Add("fcd1g", this.fcd1g);
//            this.FunctionDictionary.Add("filingIndicatorOutsidefIndicatorsTupleAssertion", this.filingIndicatorOutsidefIndicatorsTupleAssertion);
//            this.FunctionDictionary.Add("s2mdBV1001", this.s2mdBV1001);
//            this.FunctionDictionary.Add("s2mdBV1011", this.s2mdBV1011);
//            this.FunctionDictionary.Add("s2mdBV1021", this.s2mdBV1021);
//            this.FunctionDictionary.Add("s2mdBV1342", this.s2mdBV1342);
//            this.FunctionDictionary.Add("s2mdBV1362", this.s2mdBV1362);
//            this.FunctionDictionary.Add("s2mdBV1394", this.s2mdBV1394);
//            this.FunctionDictionary.Add("s2mdBV206101", this.s2mdBV206101);
//            this.FunctionDictionary.Add("s2mdBV20611", this.s2mdBV20611);
//            this.FunctionDictionary.Add("s2mdBV206111", this.s2mdBV206111);
//            this.FunctionDictionary.Add("s2mdBV206121", this.s2mdBV206121);
//            this.FunctionDictionary.Add("s2mdBV206131", this.s2mdBV206131);
//            this.FunctionDictionary.Add("s2mdBV206141", this.s2mdBV206141);
//            this.FunctionDictionary.Add("s2mdBV206151", this.s2mdBV206151);
//            this.FunctionDictionary.Add("s2mdBV206161", this.s2mdBV206161);
//            this.FunctionDictionary.Add("s2mdBV206171", this.s2mdBV206171);
//            this.FunctionDictionary.Add("s2mdBV206181", this.s2mdBV206181);
//            this.FunctionDictionary.Add("s2mdBV206191", this.s2mdBV206191);
//            this.FunctionDictionary.Add("s2mdBV20621", this.s2mdBV20621);
//            this.FunctionDictionary.Add("s2mdBV20631", this.s2mdBV20631);
//            this.FunctionDictionary.Add("s2mdBV20641", this.s2mdBV20641);
//            this.FunctionDictionary.Add("s2mdBV20651", this.s2mdBV20651);
//            this.FunctionDictionary.Add("s2mdBV20661", this.s2mdBV20661);
//            this.FunctionDictionary.Add("s2mdBV20671", this.s2mdBV20671);
//            this.FunctionDictionary.Add("s2mdBV20681", this.s2mdBV20681);
//            this.FunctionDictionary.Add("s2mdBV20691", this.s2mdBV20691);
//            this.FunctionDictionary.Add("s2mdBV25412", this.s2mdBV25412);
//            this.FunctionDictionary.Add("s2mdBV25512", this.s2mdBV25512);
//            this.FunctionDictionary.Add("s2mdBV31313", this.s2mdBV31313);
//            this.FunctionDictionary.Add("s2mdBV31413", this.s2mdBV31413);
//            this.FunctionDictionary.Add("s2mdBV31513", this.s2mdBV31513);
//            this.FunctionDictionary.Add("s2mdBV31613", this.s2mdBV31613);
//            this.FunctionDictionary.Add("s2mdBV31713", this.s2mdBV31713);
//            this.FunctionDictionary.Add("s2mdBV31813", this.s2mdBV31813);
//            this.FunctionDictionary.Add("s2mdBV31913", this.s2mdBV31913);
//            this.FunctionDictionary.Add("s2mdBV32013", this.s2mdBV32013);
//            this.FunctionDictionary.Add("s2mdBV32113", this.s2mdBV32113);
//            this.FunctionDictionary.Add("s2mdBV32213", this.s2mdBV32213);
//            this.FunctionDictionary.Add("s2mdBV32313", this.s2mdBV32313);
//            this.FunctionDictionary.Add("s2mdBV32413", this.s2mdBV32413);
//            this.FunctionDictionary.Add("s2mdBV32513", this.s2mdBV32513);
//            this.FunctionDictionary.Add("s2mdBV32613", this.s2mdBV32613);
//            this.FunctionDictionary.Add("s2mdBV32713", this.s2mdBV32713);
//            this.FunctionDictionary.Add("s2mdBV32813", this.s2mdBV32813);
//            this.FunctionDictionary.Add("s2mdBV32913", this.s2mdBV32913);
//            this.FunctionDictionary.Add("s2mdBV33013", this.s2mdBV33013);
//            this.FunctionDictionary.Add("s2mdBV3432", this.s2mdBV3432);
//            this.FunctionDictionary.Add("s2mdBV3442", this.s2mdBV3442);
//            this.FunctionDictionary.Add("s2mdBV3452", this.s2mdBV3452);
//            this.FunctionDictionary.Add("s2mdBV3462", this.s2mdBV3462);
//            this.FunctionDictionary.Add("s2mdBV3472", this.s2mdBV3472);
//            this.FunctionDictionary.Add("s2mdBV3482", this.s2mdBV3482);
//            this.FunctionDictionary.Add("s2mdBV3492", this.s2mdBV3492);
//            this.FunctionDictionary.Add("s2mdBV3502", this.s2mdBV3502);
//            this.FunctionDictionary.Add("s2mdBV3512", this.s2mdBV3512);
//            this.FunctionDictionary.Add("s2mdBV3522", this.s2mdBV3522);
//            this.FunctionDictionary.Add("s2mdBV3543", this.s2mdBV3543);
//            this.FunctionDictionary.Add("s2mdBV3582", this.s2mdBV3582);
//            this.FunctionDictionary.Add("s2mdBV3592", this.s2mdBV3592);
//            this.FunctionDictionary.Add("s2mdBV3602", this.s2mdBV3602);
//            this.FunctionDictionary.Add("s2mdBV3954", this.s2mdBV3954);
//            this.FunctionDictionary.Add("s2mdBV5061", this.s2mdBV5061);
//            this.FunctionDictionary.Add("s2mdBV5251", this.s2mdBV5251);
//            this.FunctionDictionary.Add("s2mdBV5271", this.s2mdBV5271);
//            this.FunctionDictionary.Add("s2mdBV5291", this.s2mdBV5291);
//            this.FunctionDictionary.Add("s2mdBV5311", this.s2mdBV5311);
//            this.FunctionDictionary.Add("s2mdBV5351", this.s2mdBV5351);
//            this.FunctionDictionary.Add("s2mdBV5381", this.s2mdBV5381);
//            this.FunctionDictionary.Add("s2mdBV5411", this.s2mdBV5411);
//            this.FunctionDictionary.Add("s2mdBV5431", this.s2mdBV5431);
//            this.FunctionDictionary.Add("s2mdBV5491", this.s2mdBV5491);
//            this.FunctionDictionary.Add("s2mdBV5502", this.s2mdBV5502);
//            this.FunctionDictionary.Add("s2mdBV5512", this.s2mdBV5512);
//            this.FunctionDictionary.Add("s2mdBV742", this.s2mdBV742);
//            this.FunctionDictionary.Add("s2mdBV802", this.s2mdBV802);
//            this.FunctionDictionary.Add("s2mdBV812", this.s2mdBV812);
//            this.FunctionDictionary.Add("s2mdBV891", this.s2mdBV891);
//            this.FunctionDictionary.Add("s2mdBV901", this.s2mdBV901);
//            this.FunctionDictionary.Add("s2mdBV911", this.s2mdBV911);
//            this.FunctionDictionary.Add("s2mdBV921", this.s2mdBV921);
//            this.FunctionDictionary.Add("s2mdBV941", this.s2mdBV941);
//            this.FunctionDictionary.Add("s2mdBV951", this.s2mdBV951);
//            this.FunctionDictionary.Add("s2mdBV961", this.s2mdBV961);
//            this.FunctionDictionary.Add("s2mdBV971", this.s2mdBV971);
//            this.FunctionDictionary.Add("s2mdBV981", this.s2mdBV981);
//            this.FunctionDictionary.Add("s2mdBV991", this.s2mdBV991);
//            this.FunctionDictionary.Add("s2mdTV0111", this.s2mdTV0111);
//            this.FunctionDictionary.Add("s2mdTV0115", this.s2mdTV0115);
//            this.FunctionDictionary.Add("s2mdTV0116", this.s2mdTV0116);
//            this.FunctionDictionary.Add("s2mdTV0117", this.s2mdTV0117);
//            this.FunctionDictionary.Add("s2mdTV0128", this.s2mdTV0128);
//            this.FunctionDictionary.Add("s2mdTV0129", this.s2mdTV0129);
//            this.FunctionDictionary.Add("s2mdTV0130", this.s2mdTV0130);
//            this.FunctionDictionary.Add("s2mdTV0149", this.s2mdTV0149);
//            this.FunctionDictionary.Add("s2mdTV092", this.s2mdTV092);
//            this.FunctionDictionary.Add("s2mdTV093", this.s2mdTV093);
//            this.FunctionDictionary.Add("s2mdTV16", this.s2mdTV16);
//            this.FunctionDictionary.Add("s2mdTV2", this.s2mdTV2);

//        }
//        //$filingIndicator = ('S.01.01','S.01.02','S.01.03','S.02.01','S.23.01','S.25.01','S.25.02','S.25.03','S.32.01','S.33.01','S.34.01')
//        public bool fcd1g(List<ValidationParameter> parameters)
//        {
//            var p_filingIndicator = parameters.FirstOrDefault(i => i.Name == "filingIndicator");
//            return p_filingIndicator.In("S.01.01", "S.01.02", "S.01.03", "S.02.01", "S.23.01", "S.25.01", "S.25.02", "S.25.03", "S.32.01", "S.33.01", "S.34.01");
//        }

//        //false()
//        public bool filingIndicatorOutsidefIndicatorsTupleAssertion(List<ValidationParameter> parameters)
//        {
//            var p_filingIndicator = parameters.FirstOrDefault(i => i.Name == "filingIndicator");
//            return false;
//        }

//        //iaf:numeric-equal(iaf:numeric-multiply($a, $b), $c)
//        public bool s2mdBV1001(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return functions.IAF_N_Equals(functions.IAF_N_Multiply(p_a, p_b), p_c);
//        }

//        //iaf:numeric-equal(iaf:numeric-multiply($a, $b), $c)
//        public bool s2mdBV1011(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return functions.IAF_N_Equals(functions.IAF_N_Multiply(p_a, p_b), p_c);
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//        public bool s2mdBV1021(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c));
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g)))
//        public bool s2mdBV1342(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            var p_d = parameters.FirstOrDefault(i => i.Name == "d");
//            var p_e = parameters.FirstOrDefault(i => i.Name == "e");
//            var p_f = parameters.FirstOrDefault(i => i.Name == "f");
//            var p_g = parameters.FirstOrDefault(i => i.Name == "g");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c, p_d, p_e, p_f, p_g));
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//        public bool s2mdBV1362(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c));
//        }

//        //iaf:numeric-equal($a, $b)
//        public bool s2mdBV1394(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.IAF_N_Equals(p_a, p_b);
//        }

//        //not((empty(($a))))
//        public bool s2mdBV206101(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.empty(p_a));
//        }

//        //not((empty(($a))))
//        public bool s2mdBV20611(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.empty(p_a));
//        }

//        //not((empty(($a))))
//        public bool s2mdBV206111(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.empty(p_a));
//        }

//        //not((empty(($a))))
//        public bool s2mdBV206121(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.empty(p_a));
//        }

//        //not((empty(($a))))
//        public bool s2mdBV206131(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.empty(p_a));
//        }

//        //not((empty(($a))))
//        public bool s2mdBV206141(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.empty(p_a));
//        }

//        //not((empty(($a))))
//        public bool s2mdBV206151(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.empty(p_a));
//        }

//        //not((empty(($a))))
//        public bool s2mdBV206161(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.empty(p_a));
//        }

//        //not((empty(($a))))
//        public bool s2mdBV206171(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.empty(p_a));
//        }

//        //not((empty(($a))))
//        public bool s2mdBV206181(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.empty(p_a));
//        }

//        //not((empty(($a))))
//        public bool s2mdBV206191(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.empty(p_a));
//        }

//        //not((empty(($a))))
//        public bool s2mdBV20621(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.empty(p_a));
//        }

//        //not((empty(($a))))
//        public bool s2mdBV20631(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.empty(p_a));
//        }

//        //not((empty(($a))))
//        public bool s2mdBV20641(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.empty(p_a));
//        }

//        //not((empty(($a))))
//        public bool s2mdBV20651(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.empty(p_a));
//        }

//        //not((empty(($a))))
//        public bool s2mdBV20661(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.empty(p_a));
//        }

//        //not((empty(($a))))
//        public bool s2mdBV20671(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.empty(p_a));
//        }

//        //not((empty(($a))))
//        public bool s2mdBV20681(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.empty(p_a));
//        }

//        //not((empty(($a))))
//        public bool s2mdBV20691(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.empty(p_a));
//        }

//        //iaf:numeric-equal($a, iaf:sum($b))
//        public bool s2mdBV25412(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b));
//        }

//        //iaf:numeric-equal($a, $b)
//        public bool s2mdBV25512(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.IAF_N_Equals(p_a, p_b);
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c, $d)))
//        public bool s2mdBV31313(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            var p_d = parameters.FirstOrDefault(i => i.Name == "d");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c, p_d));
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c, $d)))
//        public bool s2mdBV31413(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            var p_d = parameters.FirstOrDefault(i => i.Name == "d");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c, p_d));
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//        public bool s2mdBV31513(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c));
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//        public bool s2mdBV31613(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c));
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, $h, $i)))
//        public bool s2mdBV31713(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            var p_d = parameters.FirstOrDefault(i => i.Name == "d");
//            var p_e = parameters.FirstOrDefault(i => i.Name == "e");
//            var p_f = parameters.FirstOrDefault(i => i.Name == "f");
//            var p_g = parameters.FirstOrDefault(i => i.Name == "g");
//            var p_h = parameters.FirstOrDefault(i => i.Name == "h");
//            var p_i = parameters.FirstOrDefault(i => i.Name == "i");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i));
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//        public bool s2mdBV31813(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c));
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e)))
//        public bool s2mdBV31913(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            var p_d = parameters.FirstOrDefault(i => i.Name == "d");
//            var p_e = parameters.FirstOrDefault(i => i.Name == "e");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c, p_d, p_e));
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, $h, $i, $j, $k, $l, $m, $n, $o, $p, $q)))
//        public bool s2mdBV32013(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            var p_d = parameters.FirstOrDefault(i => i.Name == "d");
//            var p_e = parameters.FirstOrDefault(i => i.Name == "e");
//            var p_f = parameters.FirstOrDefault(i => i.Name == "f");
//            var p_g = parameters.FirstOrDefault(i => i.Name == "g");
//            var p_h = parameters.FirstOrDefault(i => i.Name == "h");
//            var p_i = parameters.FirstOrDefault(i => i.Name == "i");
//            var p_j = parameters.FirstOrDefault(i => i.Name == "j");
//            var p_k = parameters.FirstOrDefault(i => i.Name == "k");
//            var p_l = parameters.FirstOrDefault(i => i.Name == "l");
//            var p_m = parameters.FirstOrDefault(i => i.Name == "m");
//            var p_n = parameters.FirstOrDefault(i => i.Name == "n");
//            var p_o = parameters.FirstOrDefault(i => i.Name == "o");
//            var p_p = parameters.FirstOrDefault(i => i.Name == "p");
//            var p_q = parameters.FirstOrDefault(i => i.Name == "q");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j, p_k, p_l, p_m, p_n, p_o, p_p, p_q));
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//        public bool s2mdBV32113(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c));
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c, $d)))
//        public bool s2mdBV32213(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            var p_d = parameters.FirstOrDefault(i => i.Name == "d");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c, p_d));
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c, $d)))
//        public bool s2mdBV32313(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            var p_d = parameters.FirstOrDefault(i => i.Name == "d");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c, p_d));
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//        public bool s2mdBV32413(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c));
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c, $d)))
//        public bool s2mdBV32513(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            var p_d = parameters.FirstOrDefault(i => i.Name == "d");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c, p_d));
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c, $d)))
//        public bool s2mdBV32613(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            var p_d = parameters.FirstOrDefault(i => i.Name == "d");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c, p_d));
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c, $d)))
//        public bool s2mdBV32713(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            var p_d = parameters.FirstOrDefault(i => i.Name == "d");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c, p_d));
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//        public bool s2mdBV32813(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c));
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, $h, $i, $j, $k, $l, $m, $n, $o, $p, $q)))
//        public bool s2mdBV32913(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            var p_d = parameters.FirstOrDefault(i => i.Name == "d");
//            var p_e = parameters.FirstOrDefault(i => i.Name == "e");
//            var p_f = parameters.FirstOrDefault(i => i.Name == "f");
//            var p_g = parameters.FirstOrDefault(i => i.Name == "g");
//            var p_h = parameters.FirstOrDefault(i => i.Name == "h");
//            var p_i = parameters.FirstOrDefault(i => i.Name == "i");
//            var p_j = parameters.FirstOrDefault(i => i.Name == "j");
//            var p_k = parameters.FirstOrDefault(i => i.Name == "k");
//            var p_l = parameters.FirstOrDefault(i => i.Name == "l");
//            var p_m = parameters.FirstOrDefault(i => i.Name == "m");
//            var p_n = parameters.FirstOrDefault(i => i.Name == "n");
//            var p_o = parameters.FirstOrDefault(i => i.Name == "o");
//            var p_p = parameters.FirstOrDefault(i => i.Name == "p");
//            var p_q = parameters.FirstOrDefault(i => i.Name == "q");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j, p_k, p_l, p_m, p_n, p_o, p_p, p_q));
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, iaf:numeric-unary-minus($c))))
//        public bool s2mdBV33013(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, functions.IAF_N_Unary_Minus(p_c)));
//        }

//        //if ($a = xs:QName('s2c_CN:x1')) then iaf:numeric-equal($b,$c) else (true())
//        public bool s2mdBV3432(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a == functions.XS_QName("s2c_CN:x1") ? m : true;
//        }

//        //if ($a = xs:QName('s2c_CN:x1')) then iaf:numeric-equal($b,$c) else (true())
//        public bool s2mdBV3442(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a == functions.XS_QName("s2c_CN:x1") ? m : true;
//        }

//        //if ($a = xs:QName('s2c_CN:x1')) then iaf:numeric-equal($b,$c) else (true())
//        public bool s2mdBV3452(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a == functions.XS_QName("s2c_CN:x1") ? m : true;
//        }

//        //if ($a = xs:QName('s2c_CN:x1')) then iaf:numeric-equal($b,$c) else (true())
//        public bool s2mdBV3462(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a == functions.XS_QName("s2c_CN:x1") ? m : true;
//        }

//        //if ($a = xs:QName('s2c_CN:x1')) then iaf:numeric-equal($b,$c) else (true())
//        public bool s2mdBV3472(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a == functions.XS_QName("s2c_CN:x1") ? m : true;
//        }

//        //if ($a = xs:QName('s2c_CN:x1')) then iaf:numeric-equal($b,$c) else (true())
//        public bool s2mdBV3482(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a == functions.XS_QName("s2c_CN:x1") ? m : true;
//        }

//        //if ($a = xs:QName('s2c_CN:x1')) then iaf:numeric-equal($b,$c) else (true())
//        public bool s2mdBV3492(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a == functions.XS_QName("s2c_CN:x1") ? m : true;
//        }

//        //if ($a = xs:QName('s2c_CN:x1')) then iaf:numeric-equal($b,$c) else (true())
//        public bool s2mdBV3502(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a == functions.XS_QName("s2c_CN:x1") ? m : true;
//        }

//        //if ($a = xs:QName('s2c_CN:x1')) then iaf:numeric-equal($b,$c) else (true())
//        public bool s2mdBV3512(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a == functions.XS_QName("s2c_CN:x1") ? m : true;
//        }

//        //if ($a = xs:QName('s2c_CN:x1')) then iaf:numeric-equal($b,$c) else (true())
//        public bool s2mdBV3522(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a == functions.XS_QName("s2c_CN:x1") ? m : true;
//        }

//        //if ($a != xs:QName('s2c_CS:x14')) then ($b = xs:QName('s2c_CN:x1')) else (true())
//        public bool s2mdBV3543(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a != functions.XS_QName("s2c_CS:x14") ? p_b == functions.XS_QName("s2c_CN:x1") : true;
//        }

//        //if ($a != xs:QName('s2c_CS:x14') and $b = xs:QName('s2c_AP:x3')) then ($c = xs:QName('s2c_CN:x1')) else (true())
//        public bool s2mdBV3582(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a != functions.XS_QName("s2c_CS:x14") & p_b == functions.XS_QName("s2c_AP:x3") ? p_c == functions.XS_QName("s2c_CN:x1") : true;
//        }

//        //if ($a != xs:QName('s2c_CS:x14') and $b = xs:QName('s2c_AP:x2')) then ($c = xs:QName('s2c_CN:x1')) else (true())
//        public bool s2mdBV3592(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a != functions.XS_QName("s2c_CS:x14") & p_b == functions.XS_QName("s2c_AP:x2") ? p_c == functions.XS_QName("s2c_CN:x1") : true;
//        }

//        //if ($a != xs:QName('s2c_CS:x14') and $b = xs:QName('s2c_AP:x1')) then ($c = xs:QName('s2c_CN:x1')) else (true())
//        public bool s2mdBV3602(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a != functions.XS_QName("s2c_CS:x14") & p_b == functions.XS_QName("s2c_AP:x1") ? p_c == functions.XS_QName("s2c_CN:x1") : true;
//        }

//        //iaf:numeric-less-equal-than($a, $b)
//        public bool s2mdBV3954(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.IAF_N_LessEqual(p_a, p_b);
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c, $d)))
//        public bool s2mdBV5061(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            var p_d = parameters.FirstOrDefault(i => i.Name == "d");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c, p_d));
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//        public bool s2mdBV5251(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c));
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c, $d)))
//        public bool s2mdBV5271(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            var p_d = parameters.FirstOrDefault(i => i.Name == "d");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c, p_d));
//        }

//        //iaf:numeric-equal($a, $b)
//        public bool s2mdBV5291(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.IAF_N_Equals(p_a, p_b);
//        }

//        //iaf:numeric-equal($a, $b)
//        public bool s2mdBV5311(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.IAF_N_Equals(p_a, p_b);
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c, $d)))
//        public bool s2mdBV5351(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            var p_d = parameters.FirstOrDefault(i => i.Name == "d");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c, p_d));
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e)))
//        public bool s2mdBV5381(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            var p_d = parameters.FirstOrDefault(i => i.Name == "d");
//            var p_e = parameters.FirstOrDefault(i => i.Name == "e");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c, p_d, p_e));
//        }

//        //iaf:numeric-equal($a, $b)
//        public bool s2mdBV5411(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.IAF_N_Equals(p_a, p_b);
//        }

//        //iaf:numeric-equal($a, iaf:min((iaf:numeric-multiply($b, 0.25), $c)))
//        public bool s2mdBV5431(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return functions.IAF_N_Equals(p_a, functions.IAF_min(functions.IAF_N_Multiply(p_b, 0.25m), p_c));
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, $h, $i, $j, $k)))
//        public bool s2mdBV5491(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            var p_d = parameters.FirstOrDefault(i => i.Name == "d");
//            var p_e = parameters.FirstOrDefault(i => i.Name == "e");
//            var p_f = parameters.FirstOrDefault(i => i.Name == "f");
//            var p_g = parameters.FirstOrDefault(i => i.Name == "g");
//            var p_h = parameters.FirstOrDefault(i => i.Name == "h");
//            var p_i = parameters.FirstOrDefault(i => i.Name == "i");
//            var p_j = parameters.FirstOrDefault(i => i.Name == "j");
//            var p_k = parameters.FirstOrDefault(i => i.Name == "k");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j, p_k));
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//        public bool s2mdBV5502(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c));
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, iaf:numeric-unary-minus($c), iaf:numeric-unary-minus($d), iaf:numeric-unary-minus($e), iaf:numeric-unary-minus($f))))
//        public bool s2mdBV5512(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            var p_d = parameters.FirstOrDefault(i => i.Name == "d");
//            var p_e = parameters.FirstOrDefault(i => i.Name == "e");
//            var p_f = parameters.FirstOrDefault(i => i.Name == "f");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, functions.IAF_N_Unary_Minus(p_c), functions.IAF_N_Unary_Minus(p_d), functions.IAF_N_Unary_Minus(p_e), functions.IAF_N_Unary_Minus(p_f)));
//        }

//        //iaf:numeric-equal($a, $b)
//        public bool s2mdBV742(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.IAF_N_Equals(p_a, p_b);
//        }

//        //iaf:numeric-equal($a, $b)
//        public bool s2mdBV802(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.IAF_N_Equals(p_a, p_b);
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//        public bool s2mdBV812(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c));
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, iaf:sum($c), $d, $e, $f, $g, $h, $i, $j, $k, iaf:numeric-unary-minus($l), iaf:numeric-unary-minus($m))))
//        public bool s2mdBV891(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            var p_d = parameters.FirstOrDefault(i => i.Name == "d");
//            var p_e = parameters.FirstOrDefault(i => i.Name == "e");
//            var p_f = parameters.FirstOrDefault(i => i.Name == "f");
//            var p_g = parameters.FirstOrDefault(i => i.Name == "g");
//            var p_h = parameters.FirstOrDefault(i => i.Name == "h");
//            var p_i = parameters.FirstOrDefault(i => i.Name == "i");
//            var p_j = parameters.FirstOrDefault(i => i.Name == "j");
//            var p_k = parameters.FirstOrDefault(i => i.Name == "k");
//            var p_l = parameters.FirstOrDefault(i => i.Name == "l");
//            var p_m = parameters.FirstOrDefault(i => i.Name == "m");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, functions.IAF_sum(p_c), p_d, p_e, p_f, p_g, p_h, p_i, p_j, p_k, functions.IAF_N_Unary_Minus(p_l), functions.IAF_N_Unary_Minus(p_m)));
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, $h, iaf:numeric-unary-minus($i))))
//        public bool s2mdBV901(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            var p_d = parameters.FirstOrDefault(i => i.Name == "d");
//            var p_e = parameters.FirstOrDefault(i => i.Name == "e");
//            var p_f = parameters.FirstOrDefault(i => i.Name == "f");
//            var p_g = parameters.FirstOrDefault(i => i.Name == "g");
//            var p_h = parameters.FirstOrDefault(i => i.Name == "h");
//            var p_i = parameters.FirstOrDefault(i => i.Name == "i");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, functions.IAF_N_Unary_Minus(p_i)));
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, iaf:numeric-unary-minus($h))))
//        public bool s2mdBV911(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            var p_d = parameters.FirstOrDefault(i => i.Name == "d");
//            var p_e = parameters.FirstOrDefault(i => i.Name == "e");
//            var p_f = parameters.FirstOrDefault(i => i.Name == "f");
//            var p_g = parameters.FirstOrDefault(i => i.Name == "g");
//            var p_h = parameters.FirstOrDefault(i => i.Name == "h");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c, p_d, p_e, p_f, p_g, functions.IAF_N_Unary_Minus(p_h)));
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, $h, $i, $j, iaf:numeric-unary-minus($k))))
//        public bool s2mdBV921(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            var p_d = parameters.FirstOrDefault(i => i.Name == "d");
//            var p_e = parameters.FirstOrDefault(i => i.Name == "e");
//            var p_f = parameters.FirstOrDefault(i => i.Name == "f");
//            var p_g = parameters.FirstOrDefault(i => i.Name == "g");
//            var p_h = parameters.FirstOrDefault(i => i.Name == "h");
//            var p_i = parameters.FirstOrDefault(i => i.Name == "i");
//            var p_j = parameters.FirstOrDefault(i => i.Name == "j");
//            var p_k = parameters.FirstOrDefault(i => i.Name == "k");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j, functions.IAF_N_Unary_Minus(p_k)));
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, $h, iaf:numeric-unary-minus($i))))
//        public bool s2mdBV941(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            var p_d = parameters.FirstOrDefault(i => i.Name == "d");
//            var p_e = parameters.FirstOrDefault(i => i.Name == "e");
//            var p_f = parameters.FirstOrDefault(i => i.Name == "f");
//            var p_g = parameters.FirstOrDefault(i => i.Name == "g");
//            var p_h = parameters.FirstOrDefault(i => i.Name == "h");
//            var p_i = parameters.FirstOrDefault(i => i.Name == "i");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, functions.IAF_N_Unary_Minus(p_i)));
//        }

//        //iaf:numeric-equal($a, iaf:sum((iaf:sum($b), iaf:numeric-unary-minus($c), $d)))
//        public bool s2mdBV951(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            var p_d = parameters.FirstOrDefault(i => i.Name == "d");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(functions.IAF_sum(p_b), functions.IAF_N_Unary_Minus(p_c), p_d));
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, iaf:numeric-unary-minus($f), $g)))
//        public bool s2mdBV961(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            var p_d = parameters.FirstOrDefault(i => i.Name == "d");
//            var p_e = parameters.FirstOrDefault(i => i.Name == "e");
//            var p_f = parameters.FirstOrDefault(i => i.Name == "f");
//            var p_g = parameters.FirstOrDefault(i => i.Name == "g");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c, p_d, p_e, functions.IAF_N_Unary_Minus(p_f), p_g));
//        }

//        //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//        public bool s2mdBV971(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return functions.IAF_N_Equals(p_a, functions.IAF_sum(p_b, p_c));
//        }

//        //iaf:numeric-equal($a, $b)
//        public bool s2mdBV981(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.IAF_N_Equals(p_a, p_b);
//        }

//        //iaf:numeric-equal($a, $b)
//        public bool s2mdBV991(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.IAF_N_Equals(p_a, p_b);
//        }

//        //($a = (xs:QName('s2c_CN:x1')) and not(empty($b)) and empty($c)) or (($a ne (xs:QName('s2c_CN:x1'))) and (empty($b) or not(empty($c))))
//        public bool s2mdTV0111(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a.In(functions.XS_QName("s2c_CN:x1")) | p_a != functions.XS_QName("s2c_CN:x1") & functions.empty(p_b) | functions.not(functions.empty(p_c));
//        }

//        //($a = (xs:QName('s2c_CN:x1')) and not(empty($b)) and empty($c)) or (($a ne (xs:QName('s2c_CN:x1'))) and (empty($b) or not(empty($c))))
//        public bool s2mdTV0115(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a.In(functions.XS_QName("s2c_CN:x1")) | p_a != functions.XS_QName("s2c_CN:x1") & functions.empty(p_b) | functions.not(functions.empty(p_c));
//        }

//        //($a = (xs:QName('s2c_CN:x1')) and not(empty($b)) and empty($c)) or (($a ne (xs:QName('s2c_CN:x1'))) and (empty($b) or not(empty($c))))
//        public bool s2mdTV0116(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a.In(functions.XS_QName("s2c_CN:x1")) | p_a != functions.XS_QName("s2c_CN:x1") & functions.empty(p_b) | functions.not(functions.empty(p_c));
//        }

//        //($a = (xs:QName('s2c_CN:x1')) and not(empty($b)) and empty($c)) or (($a ne (xs:QName('s2c_CN:x1'))) and (empty($b) or not(empty($c))))
//        public bool s2mdTV0117(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a.In(functions.XS_QName("s2c_CN:x1")) | p_a != functions.XS_QName("s2c_CN:x1") & functions.empty(p_b) | functions.not(functions.empty(p_c));
//        }

//        //($a = (xs:QName('s2c_CN:x1')) and not(empty($b)) and empty($c)) or (($a ne (xs:QName('s2c_CN:x1'))) and (empty($b) or not(empty($c))))
//        public bool s2mdTV0128(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a.In(functions.XS_QName("s2c_CN:x1")) | p_a != functions.XS_QName("s2c_CN:x1") & functions.empty(p_b) | functions.not(functions.empty(p_c));
//        }

//        //($a = (xs:QName('s2c_CN:x1')) and not(empty($b)) and empty($c)) or (($a ne (xs:QName('s2c_CN:x1'))) and (empty($b) or not(empty($c))))
//        public bool s2mdTV0129(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a.In(functions.XS_QName("s2c_CN:x1")) | p_a != functions.XS_QName("s2c_CN:x1") & functions.empty(p_b) | functions.not(functions.empty(p_c));
//        }

//        //($a = (xs:QName('s2c_CN:x1')) and not(empty($b)) and empty($c)) or (($a ne (xs:QName('s2c_CN:x1'))) and (empty($b) or not(empty($c))))
//        public bool s2mdTV0130(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a.In(functions.XS_QName("s2c_CN:x1")) | p_a != functions.XS_QName("s2c_CN:x1") & functions.empty(p_b) | functions.not(functions.empty(p_c));
//        }

//        //($a = (xs:QName('s2c_CN:x1')) and not(empty($b)) and empty($c)) or (($a ne (xs:QName('s2c_CN:x1'))) and (empty($b) or not(empty($c))))
//        public bool s2mdTV0149(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a.In(functions.XS_QName("s2c_CN:x1")) | p_a != functions.XS_QName("s2c_CN:x1") & functions.empty(p_b) | functions.not(functions.empty(p_c));
//        }

//        //($a = (xs:QName('s2c_CN:x1')) and not(empty($b)) and empty($c)) or (($a ne (xs:QName('s2c_CN:x1'))) and (empty($b) or not(empty($c))))
//        public bool s2mdTV092(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a.In(functions.XS_QName("s2c_CN:x1")) | p_a != functions.XS_QName("s2c_CN:x1") & functions.empty(p_b) | functions.not(functions.empty(p_c));
//        }

//        //($a = (xs:QName('s2c_CN:x1')) and not(empty($b)) and empty($c)) or (($a ne (xs:QName('s2c_CN:x1'))) and (empty($b) or not(empty($c))))
//        public bool s2mdTV093(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a.In(functions.XS_QName("s2c_CN:x1")) | p_a != functions.XS_QName("s2c_CN:x1") & functions.empty(p_b) | functions.not(functions.empty(p_c));
//        }

//        //matches($a, "^LEI/[A-Z0-9]{20}$") or  matches($a, "^SC/.*")
//        public bool s2mdTV16(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.RegexpMatches(p_a, "^LEI/[A-Z0-9]{20}$") | functions.RegexpMatches(p_a, "^SC/.*");
//        }

//        //matches(string(xfi:fact-typed-dimension-value($a,QName("http://eiopa.europa.eu/xbrl/s2c/dict/dim","CE"))), "^LEI/[A-Z0-9]{20}$") or  matches(string(xfi:fact-typed-dimension-value($a,QName("http://eiopa.europa.eu/xbrl/s2c/dict/dim","CE"))), "^SC/.*")
//        public bool s2mdTV2(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return true;
//        }


//    }
//}
