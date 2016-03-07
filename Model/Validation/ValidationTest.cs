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
//            this.FunctionDictionary.Add("boiv1f", this.boiv1f);
//            this.FunctionDictionary.Add("boiv10f", this.boiv10f);
//            this.FunctionDictionary.Add("boiv100f", this.boiv100f);
//            this.FunctionDictionary.Add("boiv101f", this.boiv101f);
//            this.FunctionDictionary.Add("boiv102f", this.boiv102f);
//            this.FunctionDictionary.Add("boiv103f", this.boiv103f);
//            this.FunctionDictionary.Add("boiv104f", this.boiv104f);
//            this.FunctionDictionary.Add("boiv105f", this.boiv105f);
//            this.FunctionDictionary.Add("boiv106f", this.boiv106f);
//            this.FunctionDictionary.Add("boiv107f", this.boiv107f);
//            this.FunctionDictionary.Add("boiv108f", this.boiv108f);
//            this.FunctionDictionary.Add("boiv109f", this.boiv109f);
//            this.FunctionDictionary.Add("boiv11f", this.boiv11f);
//            this.FunctionDictionary.Add("boiv110f", this.boiv110f);
//            this.FunctionDictionary.Add("boiv111f", this.boiv111f);
//            this.FunctionDictionary.Add("boiv112f", this.boiv112f);
//            this.FunctionDictionary.Add("boiv113f", this.boiv113f);
//            this.FunctionDictionary.Add("boiv114f", this.boiv114f);
//            this.FunctionDictionary.Add("boiv115f", this.boiv115f);
//            this.FunctionDictionary.Add("boiv116f", this.boiv116f);
//            this.FunctionDictionary.Add("boiv117f", this.boiv117f);
//            this.FunctionDictionary.Add("boiv118f", this.boiv118f);
//            this.FunctionDictionary.Add("boiv119f", this.boiv119f);
//            this.FunctionDictionary.Add("boiv12f", this.boiv12f);
//            this.FunctionDictionary.Add("boiv120f", this.boiv120f);
//            this.FunctionDictionary.Add("boiv121f", this.boiv121f);
//            this.FunctionDictionary.Add("boiv122f", this.boiv122f);
//            this.FunctionDictionary.Add("boiv123f", this.boiv123f);
//            this.FunctionDictionary.Add("boiv124f", this.boiv124f);
//            this.FunctionDictionary.Add("boiv125f", this.boiv125f);
//            this.FunctionDictionary.Add("boiv126f", this.boiv126f);
//            this.FunctionDictionary.Add("boiv127f", this.boiv127f);
//            this.FunctionDictionary.Add("boiv128f", this.boiv128f);
//            this.FunctionDictionary.Add("boiv129f", this.boiv129f);
//            this.FunctionDictionary.Add("boiv13f", this.boiv13f);
//            this.FunctionDictionary.Add("boiv130f", this.boiv130f);
//            this.FunctionDictionary.Add("boiv131f", this.boiv131f);
//            this.FunctionDictionary.Add("boiv132f", this.boiv132f);
//            this.FunctionDictionary.Add("boiv133f", this.boiv133f);
//            this.FunctionDictionary.Add("boiv134f", this.boiv134f);
//            this.FunctionDictionary.Add("boiv14f", this.boiv14f);
//            this.FunctionDictionary.Add("boiv16f", this.boiv16f);
//            this.FunctionDictionary.Add("boiv17f", this.boiv17f);
//            this.FunctionDictionary.Add("boiv18f", this.boiv18f);
//            this.FunctionDictionary.Add("boiv2f", this.boiv2f);
//            this.FunctionDictionary.Add("boiv20f", this.boiv20f);
//            this.FunctionDictionary.Add("boiv200f", this.boiv200f);
//            this.FunctionDictionary.Add("boiv201f", this.boiv201f);
//            this.FunctionDictionary.Add("boiv202f", this.boiv202f);
//            this.FunctionDictionary.Add("boiv203f", this.boiv203f);
//            this.FunctionDictionary.Add("boiv204f", this.boiv204f);
//            this.FunctionDictionary.Add("boiv205f", this.boiv205f);
//            this.FunctionDictionary.Add("boiv206f", this.boiv206f);
//            this.FunctionDictionary.Add("boiv207f", this.boiv207f);
//            this.FunctionDictionary.Add("boiv208f", this.boiv208f);
//            this.FunctionDictionary.Add("boiv209f", this.boiv209f);
//            this.FunctionDictionary.Add("boiv21f", this.boiv21f);
//            this.FunctionDictionary.Add("boiv210f", this.boiv210f);
//            this.FunctionDictionary.Add("boiv211f", this.boiv211f);
//            this.FunctionDictionary.Add("boiv212f", this.boiv212f);
//            this.FunctionDictionary.Add("boiv213f", this.boiv213f);
//            this.FunctionDictionary.Add("boiv214f", this.boiv214f);
//            this.FunctionDictionary.Add("boiv215f", this.boiv215f);
//            this.FunctionDictionary.Add("boiv216f", this.boiv216f);
//            this.FunctionDictionary.Add("boiv22f", this.boiv22f);
//            this.FunctionDictionary.Add("boiv23f", this.boiv23f);
//            this.FunctionDictionary.Add("boiv24f", this.boiv24f);
//            this.FunctionDictionary.Add("boiv25f", this.boiv25f);
//            this.FunctionDictionary.Add("boiv26f", this.boiv26f);
//            this.FunctionDictionary.Add("boiv27f", this.boiv27f);
//            this.FunctionDictionary.Add("boiv28f", this.boiv28f);
//            this.FunctionDictionary.Add("boiv29f", this.boiv29f);
//            this.FunctionDictionary.Add("boiv3f", this.boiv3f);
//            this.FunctionDictionary.Add("boiv30f", this.boiv30f);
//            this.FunctionDictionary.Add("boiv302f", this.boiv302f);
//            this.FunctionDictionary.Add("boiv303f", this.boiv303f);
//            this.FunctionDictionary.Add("boiv304f", this.boiv304f);
//            this.FunctionDictionary.Add("boiv305f", this.boiv305f);
//            this.FunctionDictionary.Add("boiv307f", this.boiv307f);
//            this.FunctionDictionary.Add("boiv308f", this.boiv308f);
//            this.FunctionDictionary.Add("boiv309f", this.boiv309f);
//            this.FunctionDictionary.Add("boiv31f", this.boiv31f);
//            this.FunctionDictionary.Add("boiv310f", this.boiv310f);
//            this.FunctionDictionary.Add("boiv311f", this.boiv311f);
//            this.FunctionDictionary.Add("boiv312f", this.boiv312f);
//            this.FunctionDictionary.Add("boiv313f", this.boiv313f);
//            this.FunctionDictionary.Add("boiv314f", this.boiv314f);
//            this.FunctionDictionary.Add("boiv32f", this.boiv32f);
//            this.FunctionDictionary.Add("boiv320f", this.boiv320f);
//            this.FunctionDictionary.Add("boiv321f", this.boiv321f);
//            this.FunctionDictionary.Add("boiv322f", this.boiv322f);
//            this.FunctionDictionary.Add("boiv323f", this.boiv323f);
//            this.FunctionDictionary.Add("boiv324f", this.boiv324f);
//            this.FunctionDictionary.Add("boiv325f", this.boiv325f);
//            this.FunctionDictionary.Add("boiv326f", this.boiv326f);
//            this.FunctionDictionary.Add("boiv327f", this.boiv327f);
//            this.FunctionDictionary.Add("boiv328f", this.boiv328f);
//            this.FunctionDictionary.Add("boiv329f", this.boiv329f);
//            this.FunctionDictionary.Add("boiv33f", this.boiv33f);
//            this.FunctionDictionary.Add("boiv330f", this.boiv330f);
//            this.FunctionDictionary.Add("boiv331f", this.boiv331f);
//            this.FunctionDictionary.Add("boiv333f", this.boiv333f);
//            this.FunctionDictionary.Add("boiv334f", this.boiv334f);
//            this.FunctionDictionary.Add("boiv336f", this.boiv336f);
//            this.FunctionDictionary.Add("boiv337f", this.boiv337f);
//            this.FunctionDictionary.Add("boiv338f", this.boiv338f);
//            this.FunctionDictionary.Add("boiv339f", this.boiv339f);
//            this.FunctionDictionary.Add("boiv34f", this.boiv34f);
//            this.FunctionDictionary.Add("boiv35f", this.boiv35f);
//            this.FunctionDictionary.Add("boiv353f", this.boiv353f);
//            this.FunctionDictionary.Add("boiv354f", this.boiv354f);
//            this.FunctionDictionary.Add("boiv355f", this.boiv355f);
//            this.FunctionDictionary.Add("boiv356f", this.boiv356f);
//            this.FunctionDictionary.Add("boiv357f", this.boiv357f);
//            this.FunctionDictionary.Add("boiv36f", this.boiv36f);
//            this.FunctionDictionary.Add("boiv37f", this.boiv37f);
//            this.FunctionDictionary.Add("boiv373f", this.boiv373f);
//            this.FunctionDictionary.Add("boiv374f", this.boiv374f);
//            this.FunctionDictionary.Add("boiv375f", this.boiv375f);
//            this.FunctionDictionary.Add("boiv376f", this.boiv376f);
//            this.FunctionDictionary.Add("boiv377f", this.boiv377f);
//            this.FunctionDictionary.Add("boiv378f", this.boiv378f);
//            this.FunctionDictionary.Add("boiv379f", this.boiv379f);
//            this.FunctionDictionary.Add("boiv38f", this.boiv38f);
//            this.FunctionDictionary.Add("boiv380f", this.boiv380f);
//            this.FunctionDictionary.Add("boiv381f", this.boiv381f);
//            this.FunctionDictionary.Add("boiv382f", this.boiv382f);
//            this.FunctionDictionary.Add("boiv383f", this.boiv383f);
//            this.FunctionDictionary.Add("boiv387f", this.boiv387f);
//            this.FunctionDictionary.Add("boiv389f", this.boiv389f);
//            this.FunctionDictionary.Add("boiv39f", this.boiv39f);
//            this.FunctionDictionary.Add("boiv390f", this.boiv390f);
//            this.FunctionDictionary.Add("boiv391f", this.boiv391f);
//            this.FunctionDictionary.Add("boiv392f", this.boiv392f);
//            this.FunctionDictionary.Add("boiv393f", this.boiv393f);
//            this.FunctionDictionary.Add("boiv394f", this.boiv394f);
//            this.FunctionDictionary.Add("boiv395f", this.boiv395f);
//            this.FunctionDictionary.Add("boiv4f", this.boiv4f);
//            this.FunctionDictionary.Add("boiv40f", this.boiv40f);
//            this.FunctionDictionary.Add("boiv406f", this.boiv406f);
//            this.FunctionDictionary.Add("boiv407f", this.boiv407f);
//            this.FunctionDictionary.Add("boiv408f", this.boiv408f);
//            this.FunctionDictionary.Add("boiv409f", this.boiv409f);
//            this.FunctionDictionary.Add("boiv41f", this.boiv41f);
//            this.FunctionDictionary.Add("boiv412f", this.boiv412f);
//            this.FunctionDictionary.Add("boiv413f", this.boiv413f);
//            this.FunctionDictionary.Add("boiv42f", this.boiv42f);
//            this.FunctionDictionary.Add("boiv43f", this.boiv43f);
//            this.FunctionDictionary.Add("boiv434f", this.boiv434f);
//            this.FunctionDictionary.Add("boiv44f", this.boiv44f);
//            this.FunctionDictionary.Add("boiv45f", this.boiv45f);
//            this.FunctionDictionary.Add("boiv46f", this.boiv46f);
//            this.FunctionDictionary.Add("boiv47f", this.boiv47f);
//            this.FunctionDictionary.Add("boiv48f", this.boiv48f);
//            this.FunctionDictionary.Add("boiv49f", this.boiv49f);
//            this.FunctionDictionary.Add("boiv5f", this.boiv5f);
//            this.FunctionDictionary.Add("boiv50f", this.boiv50f);
//            this.FunctionDictionary.Add("boiv51f", this.boiv51f);
//            this.FunctionDictionary.Add("boiv52f", this.boiv52f);
//            this.FunctionDictionary.Add("boiv53f", this.boiv53f);
//            this.FunctionDictionary.Add("boiv54f", this.boiv54f);
//            this.FunctionDictionary.Add("boiv55f", this.boiv55f);
//            this.FunctionDictionary.Add("boiv56f", this.boiv56f);
//            this.FunctionDictionary.Add("boiv57f", this.boiv57f);
//            this.FunctionDictionary.Add("boiv58f", this.boiv58f);
//            this.FunctionDictionary.Add("boiv59f", this.boiv59f);
//            this.FunctionDictionary.Add("boiv60f", this.boiv60f);
//            this.FunctionDictionary.Add("boiv61f", this.boiv61f);
//            this.FunctionDictionary.Add("boiv62f", this.boiv62f);
//            this.FunctionDictionary.Add("boiv63f", this.boiv63f);
//            this.FunctionDictionary.Add("boiv64f", this.boiv64f);
//            this.FunctionDictionary.Add("boiv65f", this.boiv65f);
//            this.FunctionDictionary.Add("boiv66f", this.boiv66f);
//            this.FunctionDictionary.Add("boiv67f", this.boiv67f);
//            this.FunctionDictionary.Add("boiv68f", this.boiv68f);
//            this.FunctionDictionary.Add("boiv69f", this.boiv69f);
//            this.FunctionDictionary.Add("boiv7f", this.boiv7f);
//            this.FunctionDictionary.Add("boiv70f", this.boiv70f);
//            this.FunctionDictionary.Add("boiv71f", this.boiv71f);
//            this.FunctionDictionary.Add("boiv72f", this.boiv72f);
//            this.FunctionDictionary.Add("boiv73f", this.boiv73f);
//            this.FunctionDictionary.Add("boiv74f", this.boiv74f);
//            this.FunctionDictionary.Add("boiv75f", this.boiv75f);
//            this.FunctionDictionary.Add("boiv76f", this.boiv76f);
//            this.FunctionDictionary.Add("boiv77f", this.boiv77f);
//            this.FunctionDictionary.Add("boiv78f", this.boiv78f);
//            this.FunctionDictionary.Add("boiv79f", this.boiv79f);
//            this.FunctionDictionary.Add("boiv8f", this.boiv8f);
//            this.FunctionDictionary.Add("boiv80f", this.boiv80f);
//            this.FunctionDictionary.Add("boiv81f", this.boiv81f);
//            this.FunctionDictionary.Add("boiv82f", this.boiv82f);
//            this.FunctionDictionary.Add("boiv83f", this.boiv83f);
//            this.FunctionDictionary.Add("boiv84f", this.boiv84f);
//            this.FunctionDictionary.Add("boiv85f", this.boiv85f);
//            this.FunctionDictionary.Add("boiv86f", this.boiv86f);
//            this.FunctionDictionary.Add("boiv87f", this.boiv87f);
//            this.FunctionDictionary.Add("boiv88f", this.boiv88f);
//            this.FunctionDictionary.Add("boiv89f", this.boiv89f);
//            this.FunctionDictionary.Add("boiv9f", this.boiv9f);
//            this.FunctionDictionary.Add("boiv90f", this.boiv90f);
//            this.FunctionDictionary.Add("boiv91f", this.boiv91f);
//            this.FunctionDictionary.Add("boiv92f", this.boiv92f);
//            this.FunctionDictionary.Add("boiv93f", this.boiv93f);
//            this.FunctionDictionary.Add("boiv94f", this.boiv94f);
//            this.FunctionDictionary.Add("boiv95f", this.boiv95f);
//            this.FunctionDictionary.Add("boiv96f", this.boiv96f);
//            this.FunctionDictionary.Add("boiv97f", this.boiv97f);
//            this.FunctionDictionary.Add("boiv98f", this.boiv98f);
//            this.FunctionDictionary.Add("boiv99f", this.boiv99f);

//        }
//        //false()
//        public bool boiv1f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return false;
//        }

//        //string-length($a) <= 300
//        public bool boiv10f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(p_a) <= 300m;
//        }

//        //$a <= $b
//        public bool boiv100f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv101f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv102f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv103f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv104f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv105f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a = $b + $c
//        public bool boiv106f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a == p_b + p_c;
//        }

//        //empty($a)
//        public bool boiv107f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.empty(p_a);
//        }

//        //$a = sum($b)
//        public bool boiv108f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a == functions.sum(p_b);
//        }

//        //$a <= $b
//        public bool boiv109f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a != 0
//        public bool boiv11f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a != 0m;
//        }

//        //$a <= $b
//        public bool boiv110f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv111f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a = $b - $c
//        public bool boiv112f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a == p_b - p_c;
//        }

//        //$a <= $b
//        public bool boiv113f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //empty($a)
//        public bool boiv114f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.empty(p_a);
//        }

//        //$a <= $b
//        public bool boiv115f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv116f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv117f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv118f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv119f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //false()
//        public bool boiv12f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return false;
//        }

//        //$a <= $b
//        public bool boiv120f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv121f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv122f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv123f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a = sum($b)
//        public bool boiv124f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a == functions.sum(p_b);
//        }

//        //$a <= $b
//        public bool boiv125f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv126f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv127f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //($a = 0) or ($a = 1)
//        public bool boiv128f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return p_a == 0m | p_a == 1m;
//        }

//        //($a = 0) or ($a = 1)
//        public bool boiv129f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return p_a == 0m | p_a == 1m;
//        }

//        //string($a) = "לווים מעל 10% מההון"
//        public bool boiv13f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.String(p_a) == "לווים מעל 10% מההון";
//        }

//        //($a = 0) or ($a = 1)
//        public bool boiv130f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return p_a == 0m | p_a == 1m;
//        }

//        //$a <= $b
//        public bool boiv131f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //($a >= 0) and ($a < 100)
//        public bool boiv132f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return p_a >= 0m & p_a < 100m;
//        }

//        //$a + $b + $c <= $d
//        public bool boiv133f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            var p_d = parameters.FirstOrDefault(i => i.Name == "d");
//            return p_a + p_b + p_c <= p_d;
//        }

//        //$a + $b + $c <= $d
//        public bool boiv134f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            var p_d = parameters.FirstOrDefault(i => i.Name == "d");
//            return p_a + p_b + p_c <= p_d;
//        }

//        //string($a) = "קבוצת לווים נשלטת"
//        public bool boiv14f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.String(p_a) == "קבוצת לווים נשלטת";
//        }

//        //string-length(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV")))) = 9
//        public bool boiv16f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDV")))) == 9m;
//        }

//        //((substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 1, 1) = "8")  and (substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 2, 2) != "88"))  or ((substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 1, 4) = "9999") and (substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 5, 5) = xfi:entity-identifier(xfi:entity($a))))  or ((substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 1, 3) = "888") and (substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 4, 5)= xfi:entity-identifier(xfi:entity($a))))  or (substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 1, 9) = "111111111")
//        public bool boiv17f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 1m, 1m) == "8" | functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 1m, 9m) == "111111111";
//        }

//        //if ((string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDV")))) = 9) and (string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))) != "111111111"))  then ( if ((number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),1,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),2,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),2,1)) * 2),2,1))),"NaN","000"))  +  number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),3,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),4,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),4,1)) * 2),2,1))),"NaN","000")) +  number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),5,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),6,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),6,1)) * 2),2,1))),"NaN","000")) + number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),7,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),8,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),8,1)) * 2),2,1))),"NaN","000"))) mod 10 = 0)  then (number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),9,1)) = 0)  else((10 - ((number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),1,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),2,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),2,1)) * 2),2,1))),"NaN","000"))  +  number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),3,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),4,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),4,1)) * 2),2,1))),"NaN","000")) +  number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),5,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),6,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),6,1)) * 2),2,1))),"NaN","000")) + number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),7,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),8,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),8,1)) * 2),2,1))),"NaN","000"))) mod 10)) = number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),9,1))))  else (true())
//        public bool boiv18f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDV")))) == 9m & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))) != "111111111" ? functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 1m, 1m)) + functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 3m, 1m)) + functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 5m, 1m)) == 0m ? functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 9m, 1m)) == 0m : 10m - functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 1m, 1m)) + functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 3m, 1m)) + functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 5m, 1m)) == functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 9m, 1m)) : true;
//        }

//        //$a != 0
//        public bool boiv2f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a != 0m;
//        }

//        //if ($a = 0) then (false()) else (true())
//        public bool boiv20f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a == 0m ? false : true;
//        }

//        //string-length($a) <= 300
//        public bool boiv200f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(p_a) <= 300m;
//        }

//        //string-length(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV")))) = 9
//        public bool boiv201f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDV")))) == 9m;
//        }

//        //((substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 1, 1) = "8")  and (substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 2, 2) != "88"))  or ((substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 1, 3) = "888") and (substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 4, 5)= xfi:entity-identifier(xfi:entity($a))))
//        public bool boiv202f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 1m, 1m) == "8";
//        }

//        //if ((number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),1,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),2,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),2,1)) * 2),2,1))),"NaN","000"))  +  number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),3,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),4,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),4,1)) * 2),2,1))),"NaN","000")) +  number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),5,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),6,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),6,1)) * 2),2,1))),"NaN","000")) + number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),7,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),8,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),8,1)) * 2),2,1))),"NaN","000"))) mod 10 = 0)  then (number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),9,1)) = 0)  else((10 - ((number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),1,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),2,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),2,1)) * 2),2,1))),"NaN","000"))  +  number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),3,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),4,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),4,1)) * 2),2,1))),"NaN","000")) +  number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),5,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),6,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),6,1)) * 2),2,1))),"NaN","000")) + number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),7,1)) + number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),8,1)) * 2),1,1)) + number(translate(string(number(substring(string(xs:integer(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),8,1)) * 2),2,1))),"NaN","000"))) mod 10)) = number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))),9,1)))
//        public bool boiv203f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 1m, 1m)) + functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 3m, 1m)) + functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 5m, 1m)) == 0m ? functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 9m, 1m)) == 0m : 10m - functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 1m, 1m)) + functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 3m, 1m)) + functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 5m, 1m)) == functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 9m, 1m));
//        }

//        //string-length($a) <= 150
//        public bool boiv204f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(p_a) <= 150m;
//        }

//        //string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD")))) > 4
//        public bool boiv205f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))) > 4m;
//        }

//        //if (string-length(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))) = 9)  then ((substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),1,1) != "8")  and (substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),1,3) != "999")  and (string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) != "111111111")) else (true()) 
//        public bool boiv206f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))) == 9m ? functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))), 1m, 1m) != "8" : true;
//        }

//        //if((number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 8,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 7,1))),"NaN","000")) * 2),1,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 7,1))),"NaN","000")) * 2),2,1))),"NaN","000"))  +number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 6,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 5,1))),"NaN","000")) * 2),1,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 5,1))),"NaN","000")) * 2),2,1))),"NaN","000")) +number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 4,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 3,1))),"NaN","000")) * 2),1,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 3,1))),"NaN","000")) * 2),2,1))),"NaN","000"))  +number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 2,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 1,1))),"NaN","000")) * 2),1,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 1,1))),"NaN","000")) * 2),2,1))),"NaN","000"))) mod 10 = 0) then (number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD")))),1)) = 0) else(((10 - ((number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 8,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 7,1))),"NaN","000")) * 2),1,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 7,1))),"NaN","000")) * 2),2,1))),"NaN","000"))  +number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 6,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 5,1))),"NaN","000")) * 2),1,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 5,1))),"NaN","000")) * 2),2,1))),"NaN","000")) +number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 4,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 3,1))),"NaN","000")) * 2),1,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 3,1))),"NaN","000")) * 2),2,1))),"NaN","000"))  +number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 2,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 1,1))),"NaN","000")) * 2),1,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 1,1))),"NaN","000")) * 2),2,1))),"NaN","000"))) mod 10)) = number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD")))),1))))
//        public bool boiv207f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.Number(functions.Translate(functions.String(functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))), functions.Number(functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))))) - 8m, 1m))), "NaN", "000")) == 0m ? functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))), functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))), 1m)) == 0m : 10m - functions.Number(functions.Translate(functions.String(functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))), functions.Number(functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))))) - 8m, 1m))), "NaN", "000")) == functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))), functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))), 1m));
//        }

//        //false()
//        public bool boiv208f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return false;
//        }

//        //not((string-length(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))) = 9) and (substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))), 1, 1) = "5"))
//        public bool boiv209f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))) == 9m & functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))), 1m, 1m) == "5");
//        }

//        //if (((string-length(string(xfi:fact-typed-dimension-value($b,QName("http://www.boi.org.il/xbrl/dict/dim","TDD")))) = 0) and (string-length(string(xfi:fact-typed-dimension-value($b,QName("http://www.boi.org.il/xbrl/dict/dim","TDV")))) != 0)) or ((string-length(string(xfi:fact-typed-dimension-value($b,QName("http://www.boi.org.il/xbrl/dict/dim","TDD")))) != 0) and (string-length(string(xfi:fact-typed-dimension-value($b,QName("http://www.boi.org.il/xbrl/dict/dim","TDV")))) = 0))) then (empty($a)) else (true()) 
//        public bool boiv21f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))) == 0m & functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDV")))) != 0m | functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))) != 0m & functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDV")))) == 0m ? functions.empty(p_a) : true;
//        }

//        //not((string-length(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))) < 9) or (substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))), 1, 1) != "5"))
//        public bool boiv210f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))) < 9m | functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))), 1m, 1m) != "5");
//        }

//        //not(string-length(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))) < 9)
//        public bool boiv211f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))) < 9m);
//        }

//        //not(empty($a))
//        public bool boiv212f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.empty(p_a));
//        }

//        //if ((string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD")))) = 0) or (string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDV")))) = 0)) then (false()) else (true()) 
//        public bool boiv213f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))) == 0m | functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDV")))) == 0m ? false : true;
//        }

//        //not(empty($a))
//        public bool boiv214f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.empty(p_a));
//        }

//        //not(empty($a))
//        public bool boiv215f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.empty(p_a));
//        }

//        //not(empty($a))
//        public bool boiv216f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.empty(p_a));
//        }

//        //empty($a)
//        public bool boiv22f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.empty(p_a);
//        }

//        //(string-length(string($a)) = 3) or (string-length(string($a)) = 4)
//        public bool boiv23f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(functions.String(p_a)) == 3m | functions.StringLength(functions.String(p_a)) == 4m;
//        }

//        //false()
//        public bool boiv24f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return false;
//        }

//        //string($a) != "0"
//        public bool boiv25f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.String(p_a) != "0";
//        }

//        //$a = 4100
//        public bool boiv26f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return p_a == 4100m;
//        }

//        //string-length($a) <= 50
//        public bool boiv27f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(p_a) <= 50m;
//        }

//        //string-length($a) <= 50
//        public bool boiv28f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(p_a) <= 50m;
//        }

//        //string-length($a) <= 50
//        public bool boiv29f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(p_a) <= 50m;
//        }

//        //false()
//        public bool boiv3f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return false;
//        }

//        //not(empty($a))
//        public bool boiv30f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.empty(p_a));
//        }

//        //$a != 0
//        public bool boiv302f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a != 0m;
//        }

//        //false()
//        public bool boiv303f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return false;
//        }

//        //matches($a, "קבוצת רכישה")
//        public bool boiv304f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.RegexpMatches(p_a, "קבוצת רכישה");
//        }

//        //string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD")))) <= 9
//        public bool boiv305f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))) <= 9m;
//        }

//        //string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD")))) > 4
//        public bool boiv307f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))) > 4m;
//        }

//        //if (string-length(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))) = 9)  then ((substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),1,1) != "8")  and (substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),1,3) != "999")  and (string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) != "111111111")) else (true()) 
//        public bool boiv308f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))) == 9m ? functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))), 1m, 1m) != "8" : true;
//        }

//        //if((number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 8,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 7,1))),"NaN","000")) * 2),1,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 7,1))),"NaN","000")) * 2),2,1))),"NaN","000"))  +number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 6,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 5,1))),"NaN","000")) * 2),1,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 5,1))),"NaN","000")) * 2),2,1))),"NaN","000")) +number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 4,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 3,1))),"NaN","000")) * 2),1,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 3,1))),"NaN","000")) * 2),2,1))),"NaN","000"))  +number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 2,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 1,1))),"NaN","000")) * 2),1,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 1,1))),"NaN","000")) * 2),2,1))),"NaN","000"))) mod 10 = 0) then (number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD")))),1)) = 0) else(((10 - ((number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 8,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 7,1))),"NaN","000")) * 2),1,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 7,1))),"NaN","000")) * 2),2,1))),"NaN","000"))  +number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 6,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 5,1))),"NaN","000")) * 2),1,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 5,1))),"NaN","000")) * 2),2,1))),"NaN","000")) +number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 4,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 3,1))),"NaN","000")) * 2),1,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 3,1))),"NaN","000")) * 2),2,1))),"NaN","000"))  +number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 2,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 1,1))),"NaN","000")) * 2),1,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 1,1))),"NaN","000")) * 2),2,1))),"NaN","000"))) mod 10)) = number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD")))),1))))
//        public bool boiv309f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.Number(functions.Translate(functions.String(functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))), functions.Number(functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))))) - 8m, 1m))), "NaN", "000")) == 0m ? functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))), functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))), 1m)) == 0m : 10m - functions.Number(functions.Translate(functions.String(functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))), functions.Number(functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))))) - 8m, 1m))), "NaN", "000")) == functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))), functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))), 1m));
//        }

//        //not(empty($a))
//        public bool boiv31f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.empty(p_a));
//        }

//        //string-length($a) <= 300
//        public bool boiv310f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(p_a) <= 300m;
//        }

//        //$a != 0
//        public bool boiv311f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return p_a != 0m;
//        }

//        //false()
//        public bool boiv312f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return false;
//        }

//        //string($a) = "לווים מעל 10% מההון"
//        public bool boiv313f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.String(p_a) == "לווים מעל 10% מההון";
//        }

//        //string($a) = "קבוצת לווים נשלטת"
//        public bool boiv314f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.String(p_a) == "קבוצת לווים נשלטת";
//        }

//        //if (((string(xfi:fact-explicit-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "BIC"))) = "boi_BI:x7")  and (substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 1, 1) = "8")) or  ((string(xfi:fact-explicit-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "BIC"))) = "boi_BI:x14")  and (substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 1, 4) = "9999")  and (substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 5, 5) = xfi:entity-identifier(xfi:entity($a)))) or  ((string(xfi:fact-explicit-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "BIC"))) = "boi_BI:x13")  and (substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 1, 3) = "888")  and (substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDV"))), 4, 5) = xfi:entity-identifier(xfi:entity($a))))) then (true()) else (false())
//        public bool boiv32f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.String(functions.XFI_Fact_Explicit_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "BIC"))) == "boi_BI:x7" | functions.String(functions.XFI_Fact_Explicit_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "BIC"))) == "boi_BI:x14" | functions.String(functions.XFI_Fact_Explicit_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "BIC"))) == "boi_BI:x13" ? true : false;
//        }

//        //if ($a = 0) then (false()) else (true())
//        public bool boiv320f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a == 0m ? false : true;
//        }

//        //if (((string-length(string(xfi:fact-typed-dimension-value($b,QName("http://www.boi.org.il/xbrl/dict/dim","TDD")))) = 0) and (string-length(string(xfi:fact-typed-dimension-value($b,QName("http://www.boi.org.il/xbrl/dict/dim","TDV")))) != 0)) or ((string-length(string(xfi:fact-typed-dimension-value($b,QName("http://www.boi.org.il/xbrl/dict/dim","TDD")))) != 0) and (string-length(string(xfi:fact-typed-dimension-value($b,QName("http://www.boi.org.il/xbrl/dict/dim","TDV")))) = 0))) then (empty($a)) else (true()) 
//        public bool boiv321f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))) == 0m & functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDV")))) != 0m | functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))) != 0m & functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDV")))) == 0m ? functions.empty(p_a) : true;
//        }

//        //empty($a)
//        public bool boiv322f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.empty(p_a);
//        }

//        //(string-length(string($a)) = 3) or (string-length(string($a)) = 4)
//        public bool boiv323f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(functions.String(p_a)) == 3m | functions.StringLength(functions.String(p_a)) == 4m;
//        }

//        //false()
//        public bool boiv324f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return false;
//        }

//        //string($a) != "0"
//        public bool boiv325f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.String(p_a) != "0";
//        }

//        //$a = 4100
//        public bool boiv326f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return p_a == 4100m;
//        }

//        //string-length($a) <= 50
//        public bool boiv327f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(p_a) <= 50m;
//        }

//        //string-length($a) <= 50
//        public bool boiv328f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(p_a) <= 50m;
//        }

//        //string-length($a) <= 50
//        public bool boiv329f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(p_a) <= 50m;
//        }

//        //if (((string(xfi:fact-explicit-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "BIC"))) = "boi_BI:x9")  and (string-length(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))) = 9) and (substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))), 1, 1) = "5")) or ((string(xfi:fact-explicit-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "BIC"))) = "boi_BI:x10")  and ((string-length(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))) != 9) or (substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))), 1, 1) != "5"))) or ((string(xfi:fact-explicit-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "BIC"))) = "boi_BI:x11")  and (string-length(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))) != 9))) then (false()) else (true())
//        public bool boiv33f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.String(functions.XFI_Fact_Explicit_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "BIC"))) == "boi_BI:x9" & functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))), 1m, 1m) == "5" | functions.String(functions.XFI_Fact_Explicit_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "BIC"))) == "boi_BI:x10" | functions.String(functions.XFI_Fact_Explicit_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "BIC"))) == "boi_BI:x11" ? false : true;
//        }

//        //not(empty($a))
//        public bool boiv330f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.empty(p_a));
//        }

//        //not(empty($a))
//        public bool boiv331f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.empty(p_a));
//        }

//        //if (((string(xfi:fact-explicit-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "BIC"))) = "boi_BI:x9")  and (string-length(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))) = 9) and (substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))), 1, 1) = "5")) or ((string(xfi:fact-explicit-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "BIC"))) = "boi_BI:x10")  and ((string-length(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))) != 9) or (substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))), 1, 1) != "5"))) or ((string(xfi:fact-explicit-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "BIC"))) = "boi_BI:x11")  and (string-length(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))) != 9))) then (false()) else (true())
//        public bool boiv333f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.String(functions.XFI_Fact_Explicit_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "BIC"))) == "boi_BI:x9" & functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))), 1m, 1m) == "5" | functions.String(functions.XFI_Fact_Explicit_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "BIC"))) == "boi_BI:x10" | functions.String(functions.XFI_Fact_Explicit_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "BIC"))) == "boi_BI:x11" ? false : true;
//        }

//        //empty($a)
//        public bool boiv334f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.empty(p_a);
//        }

//        //empty($a)
//        public bool boiv336f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.empty(p_a);
//        }

//        //not(empty($a))
//        public bool boiv337f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.not(functions.empty(p_a));
//        }

//        //string-length($a) <= 15
//        public bool boiv338f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(p_a) <= 15m;
//        }

//        //empty($a)
//        public bool boiv339f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.empty(p_a);
//        }

//        //empty($a)
//        public bool boiv34f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.empty(p_a);
//        }

//        //string($a) != "boi_BI:x24"
//        public bool boiv35f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.String(p_a) != "boi_BI:x24";
//        }

//        //string-length(string($a)) = 3
//        public bool boiv353f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(functions.String(p_a)) == 3m;
//        }

//        //((floor($a div 100) = 1 or floor($a div 100) = 4 or floor($a div 100) = 7 or floor($a div 100) = 9)) and  ((floor(($a - floor($a div 100) * 100) div 10) = 0) or (floor(($a - floor($a div 100) * 100) div 10) = 1) or (floor(($a - floor($a div 100) * 100) div 10) = 2))
//        public bool boiv354f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.floor(p_a / 100m) == 1m | functions.floor(p_a / 100m) == 4m | functions.floor(p_a / 100m) == 7m | functions.floor(p_a / 100m) == 9m & functions.floor(p_a - functions.floor(p_a / 100m) * 100m / 10m) == 0m | functions.floor(p_a - functions.floor(p_a / 100m) * 100m / 10m) == 1m | functions.floor(p_a - functions.floor(p_a / 100m) * 100m / 10m) == 2m;
//        }

//        //if (string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD")))) = 0) then (floor($a div 100) = 7) else (true())
//        public bool boiv355f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))) == 0m ? functions.floor(p_a / 100m) == 7m : true;
//        }

//        //floor($a div 100) != 4
//        public bool boiv356f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.floor(p_a / 100m) != 4m;
//        }

//        //empty($a)
//        public bool boiv357f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.empty(p_a);
//        }

//        //empty($a)
//        public bool boiv36f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.empty(p_a);
//        }

//        //not(empty($a))
//        public bool boiv37f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.not(functions.empty(p_a));
//        }

//        //$a = floor($a)
//        public bool boiv373f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return p_a == functions.floor(p_a);
//        }

//        //$a >= 0
//        public bool boiv374f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return p_a >= 0m;
//        }

//        //string-length(string($a)) <= 15
//        public bool boiv375f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(functions.String(p_a)) <= 15m;
//        }

//        //$a = sum($b)
//        public bool boiv376f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a == functions.sum(p_b);
//        }

//        //$a <= $b
//        public bool boiv377f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a = $b - $c
//        public bool boiv378f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a == p_b - p_c;
//        }

//        //$a <= $b
//        public bool boiv379f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //string-length($a) <= 15
//        public bool boiv38f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(p_a) <= 15m;
//        }

//        //$a <= $b
//        public bool boiv380f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv381f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv382f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a = $b - $c
//        public bool boiv383f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a == p_b - p_c;
//        }

//        //$a <= $b
//        public bool boiv387f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv389f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //empty($a)
//        public bool boiv39f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.empty(p_a);
//        }

//        //$a = $b - $c
//        public bool boiv390f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a == p_b - p_c;
//        }

//        //$a <= $b
//        public bool boiv391f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv392f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv393f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv394f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a = $b - $c
//        public bool boiv395f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a == p_b - p_c;
//        }

//        //matches($a, "קבוצת רכישה")
//        public bool boiv4f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.RegexpMatches(p_a, "קבוצת רכישה");
//        }

//        //string-length(string($a)) <= 15
//        public bool boiv40f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(functions.String(p_a)) <= 15m;
//        }

//        //$a = $b + $c
//        public bool boiv406f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a == p_b + p_c;
//        }

//        //empty($a)
//        public bool boiv407f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.empty(p_a);
//        }

//        //$a = sum($b)
//        public bool boiv408f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a == functions.sum(p_b);
//        }

//        //$a <= $b
//        public bool boiv409f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a >= 0
//        public bool boiv41f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return p_a >= 0m;
//        }

//        //$a = $b - $c
//        public bool boiv412f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a == p_b - p_c;
//        }

//        //$a <= $b
//        public bool boiv413f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //empty($a)
//        public bool boiv42f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.empty(p_a);
//        }

//        //string-length(string($a)) <= 15
//        public bool boiv43f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(functions.String(p_a)) <= 15m;
//        }

//        //$a + $b + $c <= $d
//        public bool boiv434f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            var p_d = parameters.FirstOrDefault(i => i.Name == "d");
//            return p_a + p_b + p_c <= p_d;
//        }

//        //empty($a)
//        public bool boiv44f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.empty(p_a);
//        }

//        //$a >= 0
//        public bool boiv45f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return p_a >= 0m;
//        }

//        //$a <= $b
//        public bool boiv46f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //empty($a)
//        public bool boiv47f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.empty(p_a);
//        }

//        //empty($a)
//        public bool boiv48f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.empty(p_a);
//        }

//        //empty($a)
//        public bool boiv49f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.empty(p_a);
//        }

//        //string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD")))) <= 9
//        public bool boiv5f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))) <= 9m;
//        }

//        //xs:date($a) <= xs:date(xfi:period-instant(xfi:period($a)))
//        public bool boiv50f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.XS_Date(p_a) <= functions.XS_Date(functions.XFI_Period_Instant(functions.XFI_Period(p_a)));
//        }

//        //concat(month-from-date($a), "-", day-from-date($a)) = ("3-31" cast as xs:string) or concat(month-from-date($a), "-", day-from-date($a)) = ("6-30" cast as xs:string) or concat(month-from-date($a), "-", day-from-date($a)) = ("9-30" cast as xs:string) or concat(month-from-date($a), "-", day-from-date($a)) = ("12-31" cast as xs:string)
//        public bool boiv51f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.Concat(functions.Month(p_a), "-", functions.Day(p_a)) == String.Format("{0}", 3 - 31) | functions.Concat(functions.Month(p_a), "-", functions.Day(p_a)) == String.Format("{0}", 6 - 30) | functions.Concat(functions.Month(p_a), "-", functions.Day(p_a)) == String.Format("{0}", 9 - 30) | functions.Concat(functions.Month(p_a), "-", functions.Day(p_a)) == String.Format("{0}", 12 - 31);
//        }

//        //empty($a)
//        public bool boiv52f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.empty(p_a);
//        }

//        //string-length(string($a)) = 3
//        public bool boiv53f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(functions.String(p_a)) == 3m;
//        }

//        //((floor($a div 100) = 1 or floor($a div 100) = 4 or floor($a div 100) = 7 or floor($a div 100) = 9)) and  ((floor(($a - floor($a div 100) * 100) div 10) = 0) or (floor(($a - floor($a div 100) * 100) div 10) = 1) or (floor(($a - floor($a div 100) * 100) div 10) = 2))
//        public bool boiv54f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.floor(p_a / 100m) == 1m | functions.floor(p_a / 100m) == 4m | functions.floor(p_a / 100m) == 7m | functions.floor(p_a / 100m) == 9m & functions.floor(p_a - functions.floor(p_a / 100m) * 100m / 10m) == 0m | functions.floor(p_a - functions.floor(p_a / 100m) * 100m / 10m) == 1m | functions.floor(p_a - functions.floor(p_a / 100m) * 100m / 10m) == 2m;
//        }

//        //if (string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD")))) = 0) then (floor($a div 100) = 7) else (true())
//        public bool boiv55f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))) == 0m ? functions.floor(p_a / 100m) == 7m : true;
//        }

//        //floor($a div 100) != 4
//        public bool boiv56f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.floor(p_a / 100m) != 4m;
//        }

//        //empty($a)
//        public bool boiv57f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.empty(p_a);
//        }

//        //not(count($a) >= 2)
//        public bool boiv58f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.not(functions.Count(p_a) >= 2m);
//        }

//        //$b != 0
//        public bool boiv59f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_b != 0m;
//        }

//        //false()
//        public bool boiv60f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return false;
//        }

//        //(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD")))) != 0) and (string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDV")))) != 0)
//        public bool boiv61f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))) != 0m & functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDV")))) != 0m;
//        }

//        //b != 0
//        public bool boiv62f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return b != 0m;
//        }

//        //count($b) >= 2
//        public bool boiv63f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.Count(p_b) >= 2m;
//        }

//        //$a = $b
//        public bool boiv64f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a == p_b;
//        }

//        //$a = $b
//        public bool boiv65f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a == p_b;
//        }

//        //$a = $b
//        public bool boiv66f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a == p_b;
//        }

//        //$a = $b
//        public bool boiv67f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a == p_b;
//        }

//        //false()
//        public bool boiv68f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return false;
//        }

//        //not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700009848 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 520041690 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700003825 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 520031931 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700010085 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 513890368 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700013329 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 520036658 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 644035024 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 520000472 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700000862 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 520013954 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700003817 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 520014143 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700003957 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 510216054 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700003809 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 511076572 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700011612 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 520022732 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700011620 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 570000745 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700016082 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 520044322 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700016090 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 520028283 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700016108 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 520005067 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700016116 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 520004078 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700016124 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 520027830 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700016132 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 520017070 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700016140 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 520024647 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700016181 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 33260971 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700016157 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 520033093 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700016165 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 513767079 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700016173 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 520003781 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700016470 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 510313778 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700016934 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 520037565 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700017080 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 520032285 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700017122 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 511984213 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700017130 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 511780793 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700017833 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 511325870 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700017957 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 520033234 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700018096 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 513326439 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700007891 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 512480971 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700018955 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 511888356 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700019151 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 550212021 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700019714 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 550225510 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700020183 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 520004896 cast as xs:string)) and  not((string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 700020357 cast as xs:string) and (string(xfi:fact-typed-dimension-value($b, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) = 510947153 cast as xs:string))
//        public bool boiv69f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700009848) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 520041690)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700003825) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 520031931)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700010085) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 513890368)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700013329) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 520036658)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 644035024) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 520000472)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700000862) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 520013954)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700003817) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 520014143)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700003957) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 510216054)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700003809) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 511076572)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700011612) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 520022732)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700011620) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 570000745)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700016082) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 520044322)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700016090) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 520028283)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700016108) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 520005067)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700016116) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 520004078)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700016124) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 520027830)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700016132) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 520017070)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700016140) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 520024647)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700016181) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 33260971)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700016157) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 520033093)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700016165) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 513767079)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700016173) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 520003781)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700016470) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 510313778)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700016934) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 520037565)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700017080) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 520032285)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700017122) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 511984213)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700017130) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 511780793)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700017833) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 511325870)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700017957) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 520033234)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700018096) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 513326439)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700007891) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 512480971)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700018955) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 511888356)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700019151) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 550212021)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700019714) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 550225510)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700020183) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 520004896)) & functions.not(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 700020357) & functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_b, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) == String.Format("{0}", 510947153));
//        }

//        //string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD")))) > 4
//        public bool boiv7f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))) > 4m;
//        }

//        //($a = $b - $c) and ($d + $e + $f < $a)
//        public bool boiv70f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            var p_d = parameters.FirstOrDefault(i => i.Name == "d");
//            var p_e = parameters.FirstOrDefault(i => i.Name == "e");
//            var p_f = parameters.FirstOrDefault(i => i.Name == "f");
//            return p_a == p_b - p_c & p_d + p_e + p_f < p_a;
//        }

//        //xfi:entity-identifier(xfi:entity($a)) = "10001" or xfi:entity-identifier(xfi:entity($a)) = "11001" or xfi:entity-identifier(xfi:entity($a)) = "12001" or xfi:entity-identifier(xfi:entity($a)) = "13001" or xfi:entity-identifier(xfi:entity($a)) = "14001" or xfi:entity-identifier(xfi:entity($a)) = "17001" or xfi:entity-identifier(xfi:entity($a)) = "20001" or xfi:entity-identifier(xfi:entity($a)) = "26001" or xfi:entity-identifier(xfi:entity($a)) = "31001" or xfi:entity-identifier(xfi:entity($a)) = "34001" or xfi:entity-identifier(xfi:entity($a)) = "04001" or xfi:entity-identifier(xfi:entity($a)) = "46001" or xfi:entity-identifier(xfi:entity($a)) = "52001" or xfi:entity-identifier(xfi:entity($a)) = "54001" or xfi:entity-identifier(xfi:entity($a)) = "68001" or xfi:entity-identifier(xfi:entity($a)) = "27001" or xfi:entity-identifier(xfi:entity($a)) = "22001" or xfi:entity-identifier(xfi:entity($a)) = "23001" or xfi:entity-identifier(xfi:entity($a)) = "39001" or xfi:entity-identifier(xfi:entity($a)) = "65001" or xfi:entity-identifier(xfi:entity($a)) = "10023" or xfi:entity-identifier(xfi:entity($a)) = "10033" or xfi:entity-identifier(xfi:entity($a)) = "11012" or xfi:entity-identifier(xfi:entity($a)) = "12002" or xfi:entity-identifier(xfi:entity($a)) = "12011" or xfi:entity-identifier(xfi:entity($a)) = "12020" 
//        public bool boiv71f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.XFI_Entity_Identifier(functions.XFI_Entity(p_a)) == "10001" | functions.XFI_Entity_Identifier(functions.XFI_Entity(p_a)) == "11001" | functions.XFI_Entity_Identifier(functions.XFI_Entity(p_a)) == "12001" | functions.XFI_Entity_Identifier(functions.XFI_Entity(p_a)) == "13001" | functions.XFI_Entity_Identifier(functions.XFI_Entity(p_a)) == "14001" | functions.XFI_Entity_Identifier(functions.XFI_Entity(p_a)) == "17001" | functions.XFI_Entity_Identifier(functions.XFI_Entity(p_a)) == "20001" | functions.XFI_Entity_Identifier(functions.XFI_Entity(p_a)) == "26001" | functions.XFI_Entity_Identifier(functions.XFI_Entity(p_a)) == "31001" | functions.XFI_Entity_Identifier(functions.XFI_Entity(p_a)) == "34001" | functions.XFI_Entity_Identifier(functions.XFI_Entity(p_a)) == "04001" | functions.XFI_Entity_Identifier(functions.XFI_Entity(p_a)) == "46001" | functions.XFI_Entity_Identifier(functions.XFI_Entity(p_a)) == "52001" | functions.XFI_Entity_Identifier(functions.XFI_Entity(p_a)) == "54001" | functions.XFI_Entity_Identifier(functions.XFI_Entity(p_a)) == "68001" | functions.XFI_Entity_Identifier(functions.XFI_Entity(p_a)) == "27001" | functions.XFI_Entity_Identifier(functions.XFI_Entity(p_a)) == "22001" | functions.XFI_Entity_Identifier(functions.XFI_Entity(p_a)) == "23001" | functions.XFI_Entity_Identifier(functions.XFI_Entity(p_a)) == "39001" | functions.XFI_Entity_Identifier(functions.XFI_Entity(p_a)) == "65001" | functions.XFI_Entity_Identifier(functions.XFI_Entity(p_a)) == "10023" | functions.XFI_Entity_Identifier(functions.XFI_Entity(p_a)) == "10033" | functions.XFI_Entity_Identifier(functions.XFI_Entity(p_a)) == "11012" | functions.XFI_Entity_Identifier(functions.XFI_Entity(p_a)) == "12002" | functions.XFI_Entity_Identifier(functions.XFI_Entity(p_a)) == "12011" | functions.XFI_Entity_Identifier(functions.XFI_Entity(p_a)) == "12020";
//        }

//        //(month-from-dateTime(xfi:period-instant(xfi:period($a))) = 4 and day-from-dateTime(xfi:period-instant(xfi:period($a))) = 1) or (month-from-dateTime(xfi:period-instant(xfi:period($a))) = 7 and day-from-dateTime(xfi:period-instant(xfi:period($a))) = 1) or (month-from-dateTime(xfi:period-instant(xfi:period($a))) = 10 and day-from-dateTime(xfi:period-instant(xfi:period($a))) = 1) or (month-from-dateTime(xfi:period-instant(xfi:period($a))) = 1 and day-from-dateTime(xfi:period-instant(xfi:period($a))) = 1)
//        public bool boiv72f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.Month(functions.XFI_Period_Instant(functions.XFI_Period(p_a))) == 4m & functions.Day(functions.XFI_Period_Instant(functions.XFI_Period(p_a))) == 1m | functions.Month(functions.XFI_Period_Instant(functions.XFI_Period(p_a))) == 7m & functions.Day(functions.XFI_Period_Instant(functions.XFI_Period(p_a))) == 1m | functions.Month(functions.XFI_Period_Instant(functions.XFI_Period(p_a))) == 10m & functions.Day(functions.XFI_Period_Instant(functions.XFI_Period(p_a))) == 1m | functions.Month(functions.XFI_Period_Instant(functions.XFI_Period(p_a))) == 1m & functions.Day(functions.XFI_Period_Instant(functions.XFI_Period(p_a))) == 1m;
//        }

//        //$a = floor($a)
//        public bool boiv73f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return p_a == functions.floor(p_a);
//        }

//        //$a >= 0
//        public bool boiv74f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return p_a >= 0m;
//        }

//        //string-length(string($a)) <= 15
//        public bool boiv75f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(functions.String(p_a)) <= 15m;
//        }

//        //$a = sum($b)
//        public bool boiv76f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a == functions.sum(p_b);
//        }

//        //$a <= $b
//        public bool boiv77f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a = $b - $c
//        public bool boiv78f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a == p_b - p_c;
//        }

//        //$a <= $b
//        public bool boiv79f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //if (string-length(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))) = 9)  then ((substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),1,1) != "8")  and (substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),1,3) != "999")  and (string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))) != "111111111")) else (true()) 
//        public bool boiv8f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))) == 9m ? functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))), 1m, 1m) != "8" : true;
//        }

//        //$a <= $b
//        public bool boiv80f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv81f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv82f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a = $b - $c
//        public bool boiv83f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a == p_b - p_c;
//        }

//        //$a <= $b
//        public bool boiv84f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv85f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv86f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv87f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv88f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv89f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //if((number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 8,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 7,1))),"NaN","000")) * 2),1,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 7,1))),"NaN","000")) * 2),2,1))),"NaN","000"))  +number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 6,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 5,1))),"NaN","000")) * 2),1,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 5,1))),"NaN","000")) * 2),2,1))),"NaN","000")) +number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 4,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 3,1))),"NaN","000")) * 2),1,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 3,1))),"NaN","000")) * 2),2,1))),"NaN","000"))  +number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 2,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 1,1))),"NaN","000")) * 2),1,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 1,1))),"NaN","000")) * 2),2,1))),"NaN","000"))) mod 10 = 0) then (number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD")))),1)) = 0) else(((10 - ((number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 8,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 7,1))),"NaN","000")) * 2),1,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 7,1))),"NaN","000")) * 2),2,1))),"NaN","000"))  +number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 6,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 5,1))),"NaN","000")) * 2),1,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 5,1))),"NaN","000")) * 2),2,1))),"NaN","000")) +number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 4,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 3,1))),"NaN","000")) * 2),1,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 3,1))),"NaN","000")) * 2),2,1))),"NaN","000"))  +number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 2,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 1,1))),"NaN","000")) * 2),1,1))),"NaN","000")) +number(translate(string(number(substring(string(number(translate(string(number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),number(string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD"))))) - 1,1))),"NaN","000")) * 2),2,1))),"NaN","000"))) mod 10)) = number(substring(string(xfi:fact-typed-dimension-value($a, QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))),string-length(string(xfi:fact-typed-dimension-value($a,QName("http://www.boi.org.il/xbrl/dict/dim","TDD")))),1))))
//        public bool boiv9f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            return functions.Number(functions.Translate(functions.String(functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))), functions.Number(functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))))) - 8m, 1m))), "NaN", "000")) == 0m ? functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))), functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))), 1m)) == 0m : 10m - functions.Number(functions.Translate(functions.String(functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))), functions.Number(functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))))) - 8m, 1m))), "NaN", "000")) == functions.Number(functions.Substring(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD"))), functions.StringLength(functions.String(functions.XFI_Fact_Typed_Dimension_Value(p_a, functions.QName("http://www.boi.org.il/xbrl/dict/dim", "TDD")))), 1m));
//        }

//        //$a = $b - $c
//        public bool boiv90f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a == p_b - p_c;
//        }

//        //$a <= $b
//        public bool boiv91f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv92f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv93f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv94f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a = $b - $c
//        public bool boiv95f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            var p_c = parameters.FirstOrDefault(i => i.Name == "c");
//            return p_a == p_b - p_c;
//        }

//        //$a <= $b
//        public bool boiv96f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv97f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv98f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }

//        //$a <= $b
//        public bool boiv99f(List<ValidationParameter> parameters)
//        {
//            var p_a = parameters.FirstOrDefault(i => i.Name == "a");
//            var p_b = parameters.FirstOrDefault(i => i.Name == "b");
//            return p_a <= p_b;
//        }


//    }
//}
