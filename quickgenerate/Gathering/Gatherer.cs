using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using QuickGenerate.Implementation;
using QuickGenerate.Reflect;

namespace QuickGenerate.Gathering
{
    public class Gatherer<T>
    {
        private readonly T value;
        public readonly Dictionary<PropertyInfo, object> collected = new Dictionary<PropertyInfo, object>();
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

        public Gatherer<T> CollectAll()
        {
            foreach (var property in typeof(T).GetProperties(MyBinding.Flags))
            {
                collected.Add(property, property.GetValue(value, null));
            }
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

        public object[] AllCollected()
        {
            return collected.Values.ToArray();
        }

        public GathererMatchResult Matches(Gatherer<T> theOtherGatherer)
        {
            var matchResult = new GathererMatchResult();
            var iHaveMore = collected.Keys.Where(k1 => !theOtherGatherer.collected.Keys.Any(k2 => k1 == k2)).ToList();
            foreach (var propertyInfo in iHaveMore)
            {
                matchResult.AddMessage(
                    string.Format(
                        "{0}.{1} : {2} != [Not Collected]", 
                        propertyInfo.DeclaringType,
                        propertyInfo.Name, 
                        collected[propertyInfo]));
            }

            var youHaveMore = theOtherGatherer.collected.Keys.Where(k1 => !collected.Keys.Any(k2 => k1 == k2));
            foreach (var propertyInfo in youHaveMore)
            {
                matchResult.AddMessage(
                    string.Format(
                        "{0}.{1} : [Not Collected] != {2}", 
                        propertyInfo.DeclaringType,
                        propertyInfo.Name, 
                        collected[propertyInfo]));
            }

            var weBothHaveThem = collected.Keys.Where(k => !iHaveMore.Contains(k));
            foreach (var propertyInfo in weBothHaveThem)
            {
                if(!Equals(collected[propertyInfo], theOtherGatherer.collected[propertyInfo]))
                {
                    matchResult.AddMessage(
                        string.Format("{0}.{1} : {2} != {3}",
                                      propertyInfo.DeclaringType.Name,
                                      propertyInfo.Name,
                                      collected[propertyInfo],
                                      theOtherGatherer.collected[propertyInfo]));
                }
            }
            
            return matchResult;
        }
    }
}