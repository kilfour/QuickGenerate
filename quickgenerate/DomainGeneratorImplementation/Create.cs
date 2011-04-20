using System;
using System.Linq;
using System.Reflection;

namespace QuickGenerate.DomainGeneratorImplementation
{
    public static class Create
    {
        private static readonly PrimitiveGenerators primitiveGenerators =
            new PrimitiveGenerators();

        public static object Object(this DomainGenerator domaingenerator, Type type)
        {
            if (domaingenerator.constructionConventions.ContainsKey(type))
                return domaingenerator.constructionConventions[type]();

            var choiceConvention = domaingenerator.choiceConventions.FirstOrDefault(c => c.Type == type);
            if (choiceConvention != null)
                return choiceConvention.Possibilities.PickOne();

            var isPrimitiveGenerator = primitiveGenerators.Get(type);
            if (isPrimitiveGenerator != null)
            {
                return isPrimitiveGenerator.RandomAsObject();
            }

            var publicConstructors =
                type.GetConstructors(DomainGenerator.FlattenHierarchyBindingFlag);

            if (publicConstructors.Count() > 0)
            {
                var highestParameterCount = publicConstructors.Max(c => c.GetParameters().Count());
                var constructor = publicConstructors.First(c => c.GetParameters().Count() == highestParameterCount);
                return Construct(domaingenerator, constructor, highestParameterCount);
            }

            var allConstructors = type.GetConstructors(DomainGenerator.FlattenHierarchyBindingFlag);
            if (allConstructors.Count() > 0)
            {
                var highestParameterCount = allConstructors.Max(c => c.GetParameters().Count());
                var constructor = allConstructors.First(c => c.GetParameters().Count() == highestParameterCount);
                return Construct(domaingenerator, constructor, highestParameterCount); 
            }
            return Activator.CreateInstance(type);
        }

        private static object Construct(DomainGenerator domaingenerator, ConstructorInfo constructor, int highestParameterCount)
        {
            var parameters = constructor.GetParameters();
            var parameterValues = new object[highestParameterCount];
            for (int i = 0; i < parameters.Length; i++)
            {
                var parmeterChoiceConvention = domaingenerator.choiceConventions.FirstOrDefault(c => c.Type == parameters[i].ParameterType);
                if (parmeterChoiceConvention != null)
                {
                    var choice = parmeterChoiceConvention.Possibilities.PickOne();
                    parameterValues.SetValue(choice, i);
                    continue;
                }
                var primitiveGenerator = primitiveGenerators.Get(parameters[i].ParameterType);
                if (primitiveGenerator != null)
                {
                    parameterValues.SetValue(primitiveGenerator.RandomAsObject(), i);
                }
            }
            return constructor.Invoke(parameterValues);
        }
    }
}