using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Linq
    {
        public static string GetPropertyName<T, P>(Expression<Func<T, P>> action) where T : class
        {
            var expression = (MemberExpression)action.Body;
            string name = expression.Member.Name;

            return name;
        }
    }
}
