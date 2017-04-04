using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.Validation
{
    public partial class Functions
    {
        //cmf:numeric-equal
        //abs($paramA - $paramB) le $paramC
        public decimal GetDecimalValue(Object o) 
        {
            if (o != null) 
            {
                var ot = o.GetType();
                if (ot == typeof(ValidationParameter)) 
                {
                    return (o as ValidationParameter).DecimalValue;
                }
                //decimal r=-1;
                return (decimal)o;
            }
            return -1;
        }
        public bool CMF_N_Equal(object paramA, object paramB, object paramC)
        {
            return CMF_N_Equal(GetDecimalValue(paramA), GetDecimalValue(paramB), GetDecimalValue(paramC));
        }
        public bool CMF_N_Equal(decimal paramA, decimal paramB, decimal paramC)
        {
            return abs(paramA - paramB) <= paramC;
        }

        //cmf:numeric-less-than
        //($paramA - $paramB) lt $paramC
        public bool CMF_N_Less(object paramA, object paramB, object paramC)
        {
            return CMF_N_Less(GetDecimalValue(paramA), GetDecimalValue(paramB), GetDecimalValue(paramC));
        }
        public bool CMF_N_Less(decimal paramA, decimal paramB, decimal paramC)
        {
            return (paramA - paramB) < paramC;
        }

        //cmf:numeric-less-equal-than
        //($paramA - $paramB) le $paramC
        public bool CMF_N_LessEqual(object paramA, object paramB, object paramC)
        {
            return CMF_N_LessEqual(GetDecimalValue(paramA), GetDecimalValue(paramB), GetDecimalValue(paramC));
        }
        public bool CMF_N_LessEqual(decimal paramA, decimal paramB, decimal paramC)
        {
            return paramA - paramB <= paramC;
        }

        //cmf:numeric-greater-than
        //($paramB - $paramA) lt $paramC
        public bool CMF_N_Greater(object paramA, object paramB, object paramC)
        {
            return CMF_N_Greater(GetDecimalValue(paramA), GetDecimalValue(paramB), GetDecimalValue(paramC));
        }
        public bool CMF_N_Greater(decimal paramA, decimal paramB, decimal paramC)
        {
            return paramB - paramA < paramC;
        }

        //cmf:numeric-greater-equal-than
        //($paramB - $paramA) le $paramC
        public bool CMF_N_GreaterEqual(object paramA, object paramB, object paramC)
        {
            return CMF_N_GreaterEqual(GetDecimalValue(paramA), GetDecimalValue(paramB), GetDecimalValue(paramC));
        }
        public bool CMF_N_GreaterEqual(decimal paramA, decimal paramB, decimal paramC)
        {
            return abs(paramB - paramA) <= paramC;
        }
    }
}
