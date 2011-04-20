using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using QuickGenerate.Implementation;
using QuickGenerate.Interfaces;
using QuickGenerate.Reflect;

namespace QuickGenerate.DomainGeneratorImplementation
{
    public interface IIntCounterGeneratorOptions
    {
        bool Matches(PropertyInfo propertyInfo);
        void Apply(object target, PropertyInfo propertyInfo);
    }

    public class IntCounterGeneratorOptions<T> : IIntCounterGeneratorOptions
    {
        private readonly Dictionary<Func<MemberInfo, bool>, Func<PropertyInfo, object>> dynamicValueConventions =
            new Dictionary<Func<MemberInfo, bool>, Func<PropertyInfo, object>>();

        public bool Matches(PropertyInfo propertyInfo)
        {
            return dynamicValueConventions.Keys.Any(c => c(propertyInfo));
        }

        public void Apply(object target, PropertyInfo propertyInfo)
        {
            var key = dynamicValueConventions.Keys.FirstOrDefault(c => c(propertyInfo));
            var value = dynamicValueConventions[key](propertyInfo);
            propertyInfo.SetValue(target, value, null);
        }

        public void Counter(Expression<Func<T, int>> propertyExpression)
        {
            var generator = new FuncGenerator<int>(0, val => ++val);
            For(propertyExpression, generator);
        }

        public void Counter(Expression<Func<T, int>> propertyExpression, int startingValue)
        {
            var generator = new FuncGenerator<int>(startingValue - 1, val => ++val);
            For(propertyExpression, generator);
        }

        public void Counter(Expression<Func<T, int>> propertyExpression, int startingValue, int step)
        {
            var generator = new FuncGenerator<int>(startingValue - step, val => val = val + step);
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