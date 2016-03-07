using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.Validation
{
    public partial class Functions
    {


        public static decimal FactTreshold(InstanceFact fact) 
        {
            decimal treshold = 0;
            int decimals=0;
            if (fact.Decimals=="INF") 
            {
                return 0;
            }
            else
            {
                decimals = int.Parse(fact.Decimals);

            }
            decimals = -1 * decimals;
            decimal power = (decimal)Math.Pow(10, decimals);
            treshold = power / 2;

            return treshold;
        }
       
        public static ValueWithTreshold GetValueWithTreshold(InstanceFact fact, string FallBackValue)
        {
            var result = new ValueWithTreshold();
            if (fact == null) {
                fact = new InstanceFact();
                fact.Value = FallBackValue;
                result.ObjectValue = fact.Value;
                result.DecimalValue = fact.Value_F;
                result.Treshold = 0;
            }
            else
            {
                result.ObjectValue = fact.Value;
                result.DecimalValue = fact.Value_F;
                result.Treshold = FactTreshold(fact);
            }
            return result;
        }
        
        public ValueWithTreshold SplitValueThreshold(Object item)
        {
            var result = new ValueWithTreshold(); 
            if (item is decimal) 
            {
                result.DecimalValue = (decimal)item ;
                result.Treshold = 0;
            }
            if (item is ValidationParameter)
            {
                var p = (ValidationParameter)item;
                result = p.ValueWithTreshold;
            }
            if (item is ValueWithTreshold)
            {
                return SplitValueThreshold(item as ValueWithTreshold);
            }
            return result;
        }
        
        public ValueWithTreshold SplitValueThreshold(IValueWithTreshold fact) 
        {
            var treshold = fact.Treshold;
            return new ValueWithTreshold(fact.DecimalValue, treshold);
        }

        
        //public bool HasInvalidItem

        public ValueWithTreshold IAF_N_Add(Object a, Object b)
        {
            var v_a = SplitValueThreshold(a);
            var v_b = SplitValueThreshold(b);
            var value = v_a.DecimalValue + v_b.DecimalValue;
            var treshold = v_a.Treshold + v_b.Treshold;
            return new ValueWithTreshold(value, treshold);
        }
        public bool IAF_N_Equals(Object a, Object b)
        {
            var v_a = SplitValueThreshold(a);
            var v_b = SplitValueThreshold(b);

            return abs(v_a.DecimalValue - v_b.DecimalValue) <= v_a.Treshold + v_b.Treshold;
        }
        public bool IAF_N_Less(Object a, Object b)
        {
            var v_a = SplitValueThreshold(a);
            var v_b = SplitValueThreshold(b);

            return v_a.DecimalValue - v_b.DecimalValue < v_a.Treshold + v_b.Treshold;
        }
        public bool IAF_N_LessEqual(Object a, Object b)
        {
            var v_a = SplitValueThreshold(a);
            var v_b = SplitValueThreshold(b);

            return v_a.DecimalValue - v_b.DecimalValue <= v_a.Treshold + v_b.Treshold;
        }
        public bool IAF_N_Greater(Object a, Object b)
        {
            var v_a = SplitValueThreshold(a);
            var v_b = SplitValueThreshold(b);

            return v_a.DecimalValue > v_b.DecimalValue - (v_a.Treshold + v_b.Treshold);
        }
        public bool IAF_N_GreaterEqual(Object a, Object b)
        {
            var v_a = SplitValueThreshold(a);
            var v_b = SplitValueThreshold(b);

            return v_a.DecimalValue >= v_b.DecimalValue - (v_a.Treshold + v_b.Treshold);
        }

        public bool IAF_N_Equals_Treshold(Object a, Object b)
        {
            var v_a = SplitValueThreshold(a);
            var v_b = SplitValueThreshold(b);

            return abs(v_a.DecimalValue - v_b.DecimalValue) <= v_a.Treshold + v_b.Treshold;
        }
        public bool IAF_N_Less_Treshold(Object a, Object b)
        {
            var v_a = SplitValueThreshold(a);
            var v_b = SplitValueThreshold(b);

            return v_a.DecimalValue - v_b.DecimalValue < v_a.Treshold + v_b.Treshold;
        }
        public bool IAF_N_LessEqual_Treshold(Object a, Object b)
        {
            var v_a = SplitValueThreshold(a);
            var v_b = SplitValueThreshold(b);

            return v_a.DecimalValue - v_b.DecimalValue <= v_a.Treshold + v_b.Treshold;
        }
        public bool IAF_N_Greater_Treshold(Object a, Object b)
        {
            var v_a = SplitValueThreshold(a);
            var v_b = SplitValueThreshold(b);

            return v_a.DecimalValue > v_b.DecimalValue - (v_a.Treshold + v_b.Treshold);
        }
        public bool IAF_N_GreaterEqual_Treshold(Object a, Object b)
        {
            var v_a = SplitValueThreshold(a);
            var v_b = SplitValueThreshold(b);

            return v_a.DecimalValue >= v_b.DecimalValue - (v_a.Treshold + v_b.Treshold);
        }

        //Tresholds
        //public bool N_Equals_Treshold(decimal a, decimal b)
        //{
        //    return a == b;
        //}
        //public bool N_Less_Treshold(decimal a, decimal b)
        //{
        //    return a < b;
        //}
        //public bool N_LessEqual_Treshold(decimal a, decimal b)
        //{
        //    return a <= b;
        //}
        //public bool N_Greater_Treshold(decimal a, decimal b)
        //{
        //    return a > b;
        //}
        //public bool N_GreaterEqual_Treshold(decimal a, decimal b)
        //{
        //    return a >= b;
        //}

        public ValueWithTreshold IAF_N_Subtract(Object a, Object b)
        {
            var v_a = SplitValueThreshold(a);
            var v_b = SplitValueThreshold(b);
            var value = v_a.DecimalValue - v_b.DecimalValue;
            var treshold = v_a.Treshold + v_b.Treshold;
            return new ValueWithTreshold(value, treshold);
        }
        public ValueWithTreshold IAF_N_Divide(Object a, Object b)
        {
            var v_a = SplitValueThreshold(a);
            var v_b = SplitValueThreshold(b);
            var A = v_a.DecimalValue;
            var B = v_b.DecimalValue;
            var deltaA = v_a.Treshold;
            var deltaB = v_b.Treshold;
            if (B == 0) 
            {
                throw new DivideByZeroException();
                return new ValueWithTreshold(decimal.MaxValue, 0);
            }
            //if (B == 0) {  }
            var AdivB = A / B;
            var J0 = (A + deltaA) / (B + deltaB);
            var J1 = (A + deltaA) / (B - deltaB);
            var J2 = (A - deltaA) / (B + deltaB);
            var J3 = (A - deltaA) / (B - deltaB);
            var treshold = max(abs(AdivB - J0), abs(AdivB - J1), abs(AdivB - J2), abs(AdivB - J3));
            return new ValueWithTreshold(AdivB, treshold);
        }

        public IValueWithTreshold IAF_N_Multiply(Object a, Object b)
        {
            var v_a = SplitValueThreshold(a);
            var v_b = SplitValueThreshold(b);
            var A = v_a.DecimalValue;
            var B = v_b.DecimalValue;
            var deltaA = v_a.Treshold;
            var deltaB = v_b.Treshold;
            var AxB = A * B;
            var treshold = sum(abs(A * deltaB), abs(B * deltaA), deltaA * deltaB);
            return new ValueWithTreshold(AxB, treshold);
        }
        public IValueWithTreshold IAF_N_Unary_Minus(Object a)
        {
            var result = SplitValueThreshold(a);
            result.DecimalValue = -result.DecimalValue;
            return result;
        }
        public IValueWithTreshold IAF_N_Unary_Plus(Object a)
        {
            var result = SplitValueThreshold(a);
            result.DecimalValue = result.DecimalValue;
            return result;
        }
        //public decimal N_Unary_Plus(decimal a)
        //{
        //    return a;
        //}
        //public string XS_QName(String a)
        //{
        //    return a;
        //}

        //public bool not(bool a)
        //{
        //    return !a;
        //}

        //public bool empty(object a)
        //{
        //    return a == null;
        //}
        public int Count(ValidationParameter a) 
        {
            return a.CurrentFacts.Count;
        }
        public Period XFI_Period(ValidationParameter a)
        {
            var fact = a.CurrentFacts.FirstOrDefault();
            return fact.Period;
        }
        public DateTime? XFI_Period_Instant(Period p) 
        {
            return p.Instant;
        }
        public Entity XFI_Entity(ValidationParameter a) 
        {
            var fact = a.CurrentFacts.FirstOrDefault();

            return fact.Entity;
        }
        public string XFI_Entity_Identifier(Entity e) 
        {
            return e.ID;
        }
        public string XFI_Fact_Typed_Dimension_Value(ValidationParameter a, string qname) 
        {
            var result = "";
            var fact = a.CurrentFacts.FirstOrDefault();
            var dim = fact.Dimensions.FirstOrDefault(i => i.DimensionItem == qname);
            if (dim != null) 
            {
                result = dim.DomainMember;
            }
            return result;
        }
        public string XFI_Fact_Explicit_Dimension_Value(ValidationParameter a, string qname)
        {
            var result = "";
            var fact = a.CurrentFacts.FirstOrDefault();
            var dim = fact.Dimensions.FirstOrDefault(i => i.DimensionItem == qname);
            if (dim != null)
            {
                result = dim.DomainAndMember;
            }
            return result;
        }

        public ValueWithTreshold IAF_abs(Object a)
        {
            var result = SplitValueThreshold(a);
            if (result.DecimalValue < 0) { result.DecimalValue = result.DecimalValue * -1; }
            return result;
        }
        public ValueWithTreshold IAF_sum(params IValueWithTreshold[] facts)
        {
            var aggregated = new ValueWithTreshold();
            foreach (var fact in facts) 
            {
                aggregated.DecimalValue += fact.DecimalValue;
                aggregated.Treshold += fact.Treshold;
            }

            return aggregated;
        }

        public ValueWithTreshold IAF_sum(params Object[] items)
        {
            var casteditems = items.Select(i => SplitValueThreshold(i)).ToList();
            var aggregated = new ValueWithTreshold();
            foreach (var fact in casteditems)
            {
                aggregated.DecimalValue += fact.DecimalValue;
                aggregated.Treshold += fact.Treshold;
            }

            return aggregated;
        }

        public ValueWithTreshold IAF_max(params Object[] parameters)
        {
            var items = parameters.Select(i => SplitValueThreshold(i)).ToList();
            var max = items.Max(i=>i.DecimalValue);
            var result = items.FirstOrDefault(i => i.DecimalValue == max);
            return result;
        }

        public ValueWithTreshold IAF_min(params Object[] parameters)
        {
            var items = parameters.Select(i => SplitValueThreshold(i)).ToList();
            var min = items.Min(i => i.DecimalValue);
            var result = items.FirstOrDefault(i => i.DecimalValue == min);
            return result;
        }



    }
}
