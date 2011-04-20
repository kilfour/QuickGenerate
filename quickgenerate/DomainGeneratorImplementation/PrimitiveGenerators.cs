using System;
using System.Collections.Generic;
using System.Linq;
using QuickGenerate.Implementation;
using QuickGenerate.Interfaces;

namespace QuickGenerate.DomainGeneratorImplementation
{
    public class PrimitiveGenerators
    {
        private Dictionary<Type, IGenerator> generators { get; set; }
        public PrimitiveGenerators()
        {
            generators = new Dictionary<Type, IGenerator>();

            var requiredTypes = GetRequiredGeneratorTypes().ToList();

            var primitives =
                requiredTypes
                    .Select(type => generators[GetTypeOfGeneratedValues(type)] = (IGenerator) Activator.CreateInstance(type))
                    .ToList();

            var nullables =
                requiredTypes
                    .Where(type => typeof (ValueType).IsAssignableFrom(GetTypeOfGeneratedValues(type)))
                    .Select(type => generators[GetNullableTypeOfGeneratedValues(type)] = GetNullableGeneratorFor(type))
                    .ToList();
        }

        

        private IGenerator GetNullableGeneratorFor(Type type)
        {
            var nullable = typeof (NullableGenerator<>).MakeGenericType(GetTypeOfGeneratedValues(type));
            var innerGenerator = Activator.CreateInstance(type);
            var generator = (IGenerator)Activator.CreateInstance(nullable, new[] {innerGenerator});
            return generator;
        }

        public IGenerator Get(Type type)
        {
            if(generators.ContainsKey(type))
                return generators[type];

            return null;
        }

        private static IEnumerable<Type> GetRequiredGeneratorTypes()
        {
            return typeof(PrimitiveGenerators).Assembly.GetTypes().Where(IsARequiredGenerator);
        }

        private static bool IsARequiredGenerator(Type type)
        {
            return type.Namespace == "QuickGenerate.Primitives";
        }

        private static Type GetTypeOfGeneratedValues(Type type)
        {
            return type.GetInterface("IGenerator`1").GetGenericArguments()[0];
        }

        private static Type GetNullableTypeOfGeneratedValues(Type type)
        {
            var valuesType = GetTypeOfGeneratedValues(type);
            var nullable = typeof (Nullable<>).MakeGenericType(valuesType);
            return nullable;
        }
    }

    public class NullableGenerator<T> : Generator<object>
    {
        private readonly IGenerator<T> generator;

        public NullableGenerator(IGenerator<T> generator)
        {
            this.generator = generator;
        }

        public override object GetRandomValue()
        {
            if(new[]{1,10}.FromRange()==1)
                return null;
            return generator.GetRandomValue();
        }
    }
}