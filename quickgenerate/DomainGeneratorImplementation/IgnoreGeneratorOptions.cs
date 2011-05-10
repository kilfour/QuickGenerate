using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using QuickGenerate.Reflect;

namespace QuickGenerate.DomainGeneratorImplementation
{
    public interface IIgnoreGeneratorOptions
    {
        bool NeedsToBeIgnored(PropertyInfo propertyInfo);
    }

    public class IgnoreGeneratorOptions<T> : IIgnoreGeneratorOptions
    {
        private readonly List<Func<MemberInfo, bool>> ignoreConventions =
            new List<Func<MemberInfo, bool>>();

        public void Ignore<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            Func<MemberInfo, bool> func =
                mi =>  typeof(T).IsAssignableFrom(mi.ReflectedType)
                      && mi.Name == propertyExpression.AsPropertyInfo().Name;

            ignoreConventions.Add(func);
        }

        public bool NeedsToBeIgnored(PropertyInfo propertyInfo)
        {
            return ignoreConventions.Any(ignoreConvention => ignoreConvention(propertyInfo));
        }
    }
}