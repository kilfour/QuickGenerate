using System;
using System.Linq;
using System.Reflection;

namespace QuickGenerate.DomainGeneratorImplementation
{
    public static class Create
    {
        private static readonly PrimitiveGenerators primitiveGenerators =
            new PrimitiveGenerators();

        public static object Object(this DomainGenerator domaingenerator, Type inputType)
        {
            var isPrimitiveGenerator = primitiveGenerators.Get(inputType);
            if (isPrimitiveGenerator != null)
            {
                return isPrimitiveGenerator.RandomAsObject();
            }
            var type = domaingenerator.GetTypeToGenerate(inputType);

            var constructorTypeParameters = domaingenerator.GetConstructorTypeParameters(type);
            if(constructorTypeParameters != null)
            {
                var constructor = 
                    type
                        .GetConstructors(DomainGenerator.FlattenHierarchyBindingFlag)
                        .Where(c => c.GetParameters().Count()  == constructorTypeParameters.Count())
                        .First(
                            c =>
                                {
                                    var ctorParams = c.GetParameters();
                                    int ix = 0;
                                    foreach (var parameterInfo in ctorParams)
                                    {
                                        if(parameterInfo.ParameterType != constructorTypeParameters[ix])
                                            return false;
                                        ix++;

                                    }
                                    return true;
                                });
                return Construct(type, domaingenerator, constructor, constructorTypeParameters.Count());
            }

            if (domaingenerator.constructionConventions.Keys.Any(t => t.IsAssignableFrom(type)))
            {
                if (domaingenerator.constructionConventions.Keys.Contains(type))
                    return domaingenerator.constructionConventions[type]();
            }

            var publicConstructors = type.GetConstructors(DomainGenerator.FlattenHierarchyBindingFlag);

            if (publicConstructors.Count() > 0)
            {
                var highestParameterCount = publicConstructors.Max(c => c.GetParameters().Count());
                var constructor = publicConstructors.First(c => c.GetParameters().Count() == highestParameterCount);
                return Construct(type, domaingenerator, constructor, highestParameterCount);
            }

            var allConstructors = type.GetConstructors(DomainGenerator.FlattenHierarchyBindingFlag);
            if (allConstructors.Count() > 0)
            {
                var highestParameterCount = allConstructors.Max(c => c.GetParameters().Count());
                var constructor = allConstructors.First(c => c.GetParameters().Count() == highestParameterCount);
                return Construct(type, domaingenerator, constructor, highestParameterCount); 
            }
            return Activator.CreateInstance(type);
        }

        private static object Construct(Type type, DomainGenerator domaingenerator, ConstructorInfo constructor, int highestParameterCount)
        {
            var parameters = constructor.GetParameters();
            var parameterValues = new object[highestParameterCount];
            for (int i = 0; i < parameters.Length; i++)
            {
                domaingenerator.CheckForRecursiveRelation(type, parameters[i].ParameterType);
                parameterValues.SetValue(domaingenerator.One(parameters[i].ParameterType), i);
            }
            return constructor.Invoke(parameterValues);
        }
    }
}