using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Objects
    {
        public static Type GetEnumerableType(Type type)
        {
            foreach (Type intType in type.GetInterfaces())
            {
                if (intType.IsGenericType
                    && intType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    return intType.GetGenericArguments()[0];
                }
            }
            return null;
        }

        public static String ListToString<T>(IEnumerable<T> items) 
        {
            var sb = new StringBuilder();
            foreach (var item in items) 
            {
                sb.AppendLine(item.ToString());
            }
            return sb.ToString();
        }
    }
}
