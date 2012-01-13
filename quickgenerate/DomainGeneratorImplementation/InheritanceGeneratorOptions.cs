using System;
using System.Collections.Generic;

namespace QuickGenerate.DomainGeneratorImplementation
{
    public interface IInheritanceGeneratorOptions
    {
        Type PickType();
    }

    public class InheritanceGeneratorOptions<TBase> : IInheritanceGeneratorOptions
    {
        private readonly List<Type> possibleTypes = new List<Type>();
        public InheritanceGeneratorOptions<TBase> Use<TDerived>()
            where TDerived : TBase
        {
            possibleTypes.Add(typeof(TDerived));
            return this;
        }

        public Type PickType()
        {
            return possibleTypes.PickOne();
        }
    }
}