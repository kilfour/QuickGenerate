using System;
using System.Collections.Generic;

namespace QuickGenerate.DomainGeneratorImplementation
{
    public interface IConstructorGeneratorOptions
    {
        Type[] GetParameterTypes();
    }

    public class ConstructorGeneratorOptions<TBase> : IConstructorGeneratorOptions
    {
        private readonly List<Type> parmeterTypes = new List<Type>();

        public ConstructorGeneratorOptions<TBase> Construct()
        {
            return this;
        }

        public ConstructorGeneratorOptions<TBase> Construct<TDerived>()
        {
            parmeterTypes.Add(typeof(TDerived));
            return this;
        }

        public Type[] GetParameterTypes()
        {
            return parmeterTypes.ToArray();
        }
    }

    
}