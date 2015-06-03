using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Linq
    {
        public void X() 
        {

        }
        public int XX()
        {
            return 1;
        }
        public static string GetPropertyName<T, P>(Expression<Func<T, P>> action) where T : class
        {
            var expression = (MemberExpression)action.Body;
            string name = expression.Member.Name;

            return name;
        }

        public static string GetPropertyName(LambdaExpression action) 
        {
            var expression = (MemberExpression)action.Body;
            string name = expression.Member.Name;

            return name;
        }

        public static string GetMemberName<T>(Expression<Func<T, object>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentException(
                    "The expression cannot be null.");
            }

            return GetMemberName(expression.Body);
        }

        public static string GetMemberName<T>(
                Expression<Action<T>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentException(
                    "The expression cannot be null.");
            }

            return GetMemberName(expression.Body);
        }

        private static string GetMemberName(Expression expression)
        {
            if (expression == null)
            {
                throw new ArgumentException(
                    "The expression cannot be null.");
            }

            if (expression is MemberExpression)
            {
                // Reference type property or field
                var memberExpression = (MemberExpression)expression;
                return memberExpression.Member.Name;
            }

            if (expression is MethodCallExpression)
            {
                // Reference type method
                var methodCallExpression =
                    (MethodCallExpression)expression;
                return methodCallExpression.Method.Name;
            }

            if (expression is UnaryExpression)
            {
                // Property, field of method returning value type
                var unaryExpression = (UnaryExpression)expression;
                return GetMemberName(unaryExpression);
            }

            throw new ArgumentException("Invalid expression");
        }

        private static string GetMemberName(
                UnaryExpression unaryExpression)
        {
            if (unaryExpression.Operand is MethodCallExpression)
            {
                var methodExpression =
                    (MethodCallExpression)unaryExpression.Operand;
                return methodExpression.Method.Name;
            }

            return ((MemberExpression)unaryExpression.Operand)
                        .Member.Name;
        }
        /*
        public static string GetMemberName(LambdaExpression memberSelector)
        {
            var currentExpression = memberSelector.Body;

            while (true)
            {
                switch (currentExpression.NodeType)
                {
                    case ExpressionType.Parameter:
                        return ((ParameterExpression)currentExpression).Name;
                    case ExpressionType.MemberAccess:
                        return ((MemberExpression)currentExpression).Member.Name;
                    case ExpressionType.Call:
                        return ((MethodCallExpression)currentExpression).Method.Name;
                    case ExpressionType.Convert:
                    case ExpressionType.ConvertChecked:
                        currentExpression = ((UnaryExpression)currentExpression).Operand;
                        break;
                    case ExpressionType.Invoke:
                        currentExpression = ((InvocationExpression)currentExpression).Expression;
                        break;
                    case ExpressionType.ArrayLength:
                        return "Length";
                    default:
                        throw new Exception("not a proper member selector");
                }
            }
        }

        public static string GetMethodName(LambdaExpression action) 
        {


            var expression = (MemberExpression)action.Body;
            string name = expression.Member.Name;

            return name;
        }

        public static string MethodName(LambdaExpression expression)
        {
            var unaryExpression = (UnaryExpression)expression.Body;
            var methodCallExpression = (MethodCallExpression)unaryExpression.Operand;
            //if (IsNET45)
            //{
            //    var methodCallObject = (ConstantExpression)methodCallExpression.Object;
            //    var methodInfo = (MethodInfo)methodCallObject.Value;
            //    return methodInfo.Name;
            //}
            //else
            //{
                var methodInfoExpression = (ConstantExpression)methodCallExpression.Arguments.Last();
                var methodInfo = (MemberInfo)methodInfoExpression.Value;
                return methodInfo.Name;
            //}
        }
        */
    }
}
