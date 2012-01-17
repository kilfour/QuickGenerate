using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickGenerate.DomainGeneratorImplementation
{
    public interface IConstructorGeneratorOptions
    {
        ConstructorParameterInfo[] GetParameterInfos();
    }

    public class ConstructorParameterInfo
    {
        public Type Type { get; set; }
        public Func<object> Generate { get; set; } 
    }

    public class ConstructorGeneratorOptions<TBase> : IConstructorGeneratorOptions
    {
        private readonly List<ConstructorParameterInfo> parmeterTypes = new List<ConstructorParameterInfo>();

        public ConstructorGeneratorOptions<TBase> Construct()
        {
            return this;
        }

        public ConstructorGeneratorOptions<TBase> Construct<TParameter>()
        {
            parmeterTypes.Add(new ConstructorParameterInfo{Type = typeof(TParameter)});
            return this;
        }

        public ConstructorGeneratorOptions<TBase> Construct<TParameter>(params TParameter[] possibleValues)
        {
            parmeterTypes.Add(new ConstructorParameterInfo { Type = typeof(TParameter), Generate = () => possibleValues.PickOne()});
            return this;
        }

        public ConstructorGeneratorOptions<TBase> Construct<TParameter>(IGenerator<TParameter> generator)
        {
            parmeterTypes.Add(new ConstructorParameterInfo { Type = typeof(TParameter), Generate = () => generator.GetRandomValue()});
            return this;
        }

        public ConstructorParameterInfo[] GetParameterInfos()
        {
            return parmeterTypes.ToArray();
        }
    }

    
}