using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using QuickGenerate.Reflect;

namespace QuickGenerate.Implementation
{
    public class Gatherer<T>
    {
        private readonly T value;
        private readonly Dictionary<PropertyInfo, object> collected = new Dictionary<PropertyInfo, object>();
        private readonly Dictionary<PropertyInfo, object> gatherers = new Dictionary<PropertyInfo, object>();

        public Gatherer(T value)
        {
            this.value = value;
        }

        public Gatherer<T> Collect<TProperty>(Expression<Func<T, TProperty>> propertyFunc)
        {
            var property = propertyFunc.AsPropertyInfo();
            collected.Add(property, property.GetValue(value, null));
            return this;
        }

        public TProperty Recall<TProperty>(Expression<Func<T, TProperty>> propertyFunc)
        {
            var property = propertyFunc.AsPropertyInfo();
            return (TProperty)collected[property];
        }

        public Gatherer<T> From<TProperty>(Expression<Func<T, TProperty>> propertyFunc, Func<Gatherer<TProperty>, Gatherer<TProperty>> gatherFunc)
        {
            var property = propertyFunc.AsPropertyInfo();
            var gatherer = gatherFunc(Gather.From(propertyFunc.Get(value)));
            gatherers.Add(property, gatherer);
            return this;
        }

        public Gatherer<TProperty> RecallFrom<TProperty>(Expression<Func<T, TProperty>> propertyFunc)
        {
            var property = propertyFunc.AsPropertyInfo();
            return (Gatherer<TProperty>)gatherers[property];
        }

        public TResult[] Collected<TResult>()
        {
            return collected.Values.Where(val => val.GetType() == typeof(TResult)).Cast<TResult>().ToArray();
        }
    }
}