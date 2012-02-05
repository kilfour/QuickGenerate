using System;
using System.Linq.Expressions;
using System.Reflection;

namespace QuickGenerate.Reflect
{
    public static class ExpressionExtensions
    {
        public static MemberExpression AsMemberExpression<TTarget, TExpression>(this Expression<Func<TTarget,
                                                                                    TExpression>> expression)
        {
            if (expression.Body is UnaryExpression)
            {
                // WHY: expressions that target value types store the MemberExpression in another place than reference types
                return ((UnaryExpression)expression.Body).Operand as MemberExpression;
            }

            return expression.Body as MemberExpression;
        }

        public static PropertyInfo AsPropertyInfo<TTarget, TExpression>(this Expression<Func<TTarget, TExpression>> expression)
        {
            return expression.AsMemberExpression().Member as PropertyInfo;
        }

        public static TExpression Get<TTarget, TExpression>(this Expression<Func<TTarget, TExpression>> expression, TTarget target)
        {
            return (TExpression)expression.AsPropertyInfo().GetValue(target, null);
        }

        public static void Set<TTarget, TExpression>(this Expression<Func<TTarget, TExpression>> expression, TTarget target, TExpression value)
        {
            expression.AsPropertyInfo().SetValue(target, value, null);
        }

        public static string GetName<TTarget, TExpression>(this Expression<Func<TTarget, TExpression>> expression, TTarget target)
        {
            return expression.AsPropertyInfo().Name;
        }
    }
}