using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using QuickGenerate.Implementation;
using QuickGenerate.Interfaces;
using QuickGenerate.Reflect;

namespace QuickGenerate.DomainGeneratorImplementation
{
    public interface ILongCounterGeneratorOptions { }

    public class LongCounterGeneratorOptions<T> : ILongCounterGeneratorOptions
    {
        private readonly Dictionary<Func<MemberInfo, bool>, Func<PropertyInfo, object>> dynamicValueConventions =
            new Dictionary<Func<MemberInfo, bool>, Func<PropertyInfo, object>>();

        public void Counter(Expression<Func<T, long>> propertyExpression)
        {
            var generator = new FuncGenerator<long>(0, val => ++val);
            For(propertyExpression, generator);
        }

        public void Counter(Expression<Func<T, long>> propertyExpression, long startingValue)
        {
            var generator = new FuncGenerator<long>(startingValue - 1, val => ++val);
            For(propertyExpression, generator);
        }

        public void Counter(Expression<Func<T, long>> propertyExpression, long startingValue, long step)
        {
            var generator = new FuncGenerator<long>(startingValue - step, val => val = val + step);
            For(propertyExpression, generator);
        }

        private void For<TProperty>(
            Expression<Func<T, TProperty>> propertyExpression,
            params IGenerator<TProperty>[] generators)
        {
            AddConvention(propertyExpression, () => generators.PickOne().GetRandomValue());
        }

        private void AddConvention<TProperty>(
            Expression<Func<T, TProperty>> propertyExpression,
            Func<object> func)
        {
            dynamicValueConventions[GetPredicate(propertyExpression)] = pi => func();
        }

        private static Func<MemberInfo, bool> GetPredicate<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            Func<MemberInfo, bool> predicate =
                mi => mi.ReflectedType.IsAssignableFrom(typeof(T))
                      && mi.Name == propertyExpression.AsPropertyInfo().Name;
            return predicate;
        }
    }
}