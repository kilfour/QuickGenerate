using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using QuickGenerate.DomainGeneratorImplementation;
using QuickGenerate.Implementation;
using QuickGenerate.Modifying;
using QuickGenerate.Primitives;

namespace QuickGenerate
{
    public class DomainGenerator
    {
        private readonly List<object> generatedObjects = new List<object>();

        public IEnumerable<object> GeneratedObjects { get { return generatedObjects; } }

        private readonly PrimitiveGenerators primitiveGenerators = new PrimitiveGenerators();

        public readonly Dictionary<Func<MemberInfo, bool>, Func<PropertyInfo, object>> dynamicValueConventions =
            new Dictionary<Func<MemberInfo, bool>, Func<PropertyInfo, object>>();

        private readonly List<OneToManyRelation> oneToManyRelations =
            new List<OneToManyRelation>();

        private readonly List<ManyToOneRelation> manyToOneRelations =
            new List<ManyToOneRelation>();

        public readonly List<ActionConvention> actionConventions
            = new List<ActionConvention>();

        private readonly List<ChoiceConvention> choiceConventions
            = new List<ChoiceConvention>();

        private readonly Dictionary<Type, Func<object>> constructionConventions
            = new Dictionary<Type, Func<object>>();

        private readonly IDictionary<Type, IIgnoreGeneratorOptions> ignoreGeneratorOptions =
            new Dictionary<Type, IIgnoreGeneratorOptions>();

        private readonly IDictionary<Type, IInheritanceGeneratorOptions> inheritanceGeneratorOptions =
            new Dictionary<Type, IInheritanceGeneratorOptions>();

        private readonly IDictionary<Type, IConstructorGeneratorOptions> constructorGeneratorOptions =
            new Dictionary<Type, IConstructorGeneratorOptions>();


        public DomainGenerator OneToOne<TOne, TMany>(Action<TOne, TMany> action)
        {
            return OneToMany(1, action);
        }

        public DomainGenerator OneToMany<TOne, TMany>(int numberOfMany, Action<TOne, TMany> action)
        {
            var oneType = typeof (TOne);
            if (IsComponentType(oneType))
                throw new NoRelationAllowedOnComponentsException(String.Format("Component : {0}.", oneType.Name));

            var manyType = typeof(TMany);

            oneToManyRelations.Add(
                new OneToManyRelation
                    {
                        AddChildElement = (one, many) => action((TOne)one, (TMany)many),
                        Amount = () => numberOfMany,
                        One = oneType, 
                        Many = manyType
                    });
            return this;
        }

        public DomainGenerator OneToMany<TOne, TMany>(
            int numberOfMany,
            Func<TOne, TMany> manyFunc,
            Action<TOne, TMany> action)
        {
            return OneToMany(numberOfMany, numberOfMany, manyFunc, action);
        }

        public DomainGenerator OneToMany<TOne, TMany>(
            int minmumNumberOfMany,
            int maximumNumberOfMany,
            Func<TOne, TMany> manyFunc,
            Action<TOne, TMany> action)
        {
            var oneType = typeof(TOne);
            if (IsComponentType(oneType))
                throw new NoRelationAllowedOnComponentsException(String.Format("Component : {0}.", oneType.Name));

            oneToManyRelations.Add(
                new OneToManyRelation
                {
                    CreateChildElement = one => manyFunc((TOne)one),
                    AddChildElement = (one, many) => action((TOne)one, (TMany)many),
                    Amount = () => new IntGenerator(minmumNumberOfMany, maximumNumberOfMany).GetRandomValue(),
                    One = typeof(TOne),
                    Many = typeof(TMany)
                });
            return this;
        }

        public DomainGenerator OneToMany<TOne, TMany>(int minmumNumberOfMany, int maximumNumberOfMany, Action<TOne, TMany> action)
        {
            oneToManyRelations.Add(
                new OneToManyRelation
                {
                    AddChildElement = (one, many) => action((TOne)one, (TMany)many),
                    Amount = () => new IntGenerator(minmumNumberOfMany, maximumNumberOfMany).GetRandomValue(),
                    One = typeof(TOne),
                    Many = typeof(TMany)
                });
            return this;
        }

        public DomainGenerator ManyToOne<TMany, TOne>(int numberOfMany, Action<TMany, TOne> action)
        {
            return ManyToOne(numberOfMany, numberOfMany, action);
        }

        public DomainGenerator ManyToOne<TMany, TOne>(int minmumNumberOfMany, int maximumNumberOfMany, Action<TMany, TOne> action)
        {
            var manyType = typeof(TMany);
            if (IsComponentType(manyType))
                throw new NoRelationAllowedOnComponentsException(String.Format("Component : {0}.", manyType.Name));

            manyToOneRelations.Add(
                new ManyToOneRelation
                {
                    Action = (many, one) => action((TMany)many, (TOne)one),
                    Amount = () => new IntGenerator(minmumNumberOfMany, maximumNumberOfMany).GetRandomValue(),
                    One = typeof(TOne),
                    Many = typeof(TMany)
                });
            return this;
        }

        public DomainGenerator With<T>(Func<T> func)
        {
            constructionConventions[typeof (T)] = () => func();
            return this;
        }

        public DomainGenerator With<T>(Func<T, T> func)
        {
            return With(mi => true, () => func(One<T>()));
        }

        public DomainGenerator With<T>(Func<MemberInfo, bool> predicate, Func<T> func)
        {
            Func<MemberInfo, bool> decoratedPredicate =
                mi => ((PropertyInfo)mi).PropertyType.IsAssignableFrom(typeof(T)) && predicate(mi);
            dynamicValueConventions[decoratedPredicate] = pi => func();
            return this;
        }

        public DomainGenerator With<T>(Func<GeneratorOptions<T>, GeneratorOptions<T>> customization)
        {
            customization(new GeneratorOptions<T>(this));
            return this;
        }

        public DomainGenerator With<T>(params T[] choices)
        {
            choiceConventions
                .Add(
                    new ChoiceConvention
                    {
                        Type = typeof(T),
                        Possibilities = choices.Cast<object>().ToArray()
                    });
            return this;
        }

        public DomainGenerator With<T>(Action<IgnoreGeneratorOptions<T>> customization)
        {
            if (!ignoreGeneratorOptions.ContainsKey(typeof(T)))
                ignoreGeneratorOptions[typeof (T)] = new IgnoreGeneratorOptions<T>();
            customization((IgnoreGeneratorOptions<T>)ignoreGeneratorOptions[typeof(T)]);
            return this;
        }

        public DomainGenerator With(Action<IgnoreGeneratorOptions> customization)
        {
            if (!ignoreGeneratorOptions.ContainsKey(typeof(object)))
                ignoreGeneratorOptions[typeof(object)] = new IgnoreGeneratorOptions();
            customization((IgnoreGeneratorOptions)ignoreGeneratorOptions[typeof(object)]);
            return this;
        }

        public DomainGenerator With<T>(Action<InheritanceGeneratorOptions<T>> customization)
        {
            if (!inheritanceGeneratorOptions.ContainsKey(typeof(T)))
                inheritanceGeneratorOptions[typeof(T)] = new InheritanceGeneratorOptions<T>();
            customization((InheritanceGeneratorOptions<T>)inheritanceGeneratorOptions[typeof(T)]);
            return this;
        }

        public Type GetTypeToGenerate(Type type)
        {
            if (!inheritanceGeneratorOptions.ContainsKey(type))
                return type;
            var options = inheritanceGeneratorOptions[type];
            return options.PickType();
        }

        public DomainGenerator With<T>(Action<ConstructorGeneratorOptions<T>> customization)
        {
            if (!constructorGeneratorOptions.ContainsKey(typeof(T)))
                constructorGeneratorOptions[typeof(T)] = new ConstructorGeneratorOptions<T>();
            customization((ConstructorGeneratorOptions<T>)constructorGeneratorOptions[typeof(T)]);
            return this;
        }

        public ConstructorParameterInfo[] GetConstructorTypeParameters(Type type)
        {
            if (!constructorGeneratorOptions.ContainsKey(type))
                return null;
            var options = constructorGeneratorOptions[type];
            return options.GetParameterInfos();
        }

        private bool IsAKnownPossibility(object target, PropertyInfo propertyInfo)
        {
            var choiceConvention = choiceConventions.FirstOrDefault(c => c.Type == propertyInfo.PropertyType);
            if (choiceConvention != null)
            {
                var choice = choiceConvention.Possibilities.PickOne();
                SetPropertyValue(propertyInfo, target, choice);
                return true;
            }
            return false;
        }

        public void ApplyForEachActions(object target)
        {
            foreach (var actionConvention in actionConventions)
            {
                if (actionConvention.Type.IsAssignableFrom(target.GetType()))
                    actionConvention.Action(target);
            }
        }

        public DomainGenerator ForEach<T>(Action<T> action)
        {
            actionConventions.Add(
                new ActionConvention
                    {
                        Type = typeof(T),
                        Action = t => action((T)t)
                    });
            return this;
        }

        public DomainGenerator ForEach<T>(Action<int, T> action)
        {
            actionConventions.Add(
                new ActionConvention
                {
                    Type = typeof(T),
                    IndexAction = (i, t) => action(i, (T)t)
                });
            return this;
        }

        public bool NeedsToBeIgnored(PropertyInfo propertyInfo)
        {
            return ignoreGeneratorOptions.Any(opt => opt.Value.NeedsToBeIgnored(propertyInfo));
        }

        private void ApplyConvention(object target, PropertyInfo propertyInfo, Func<MemberInfo, bool> key)
        {
            var value = dynamicValueConventions[key](propertyInfo);
            SetPropertyValue(propertyInfo, target, value);
        }

        private bool MatchesAConvention(object target, PropertyInfo propertyInfo)
        {
            var key = dynamicValueConventions.Keys.FirstOrDefault(c => c(propertyInfo));
            if (key == null)
                return false;
            ApplyConvention(target, propertyInfo, key);
            return true;
        }

        private bool IsAKnownPrimitive(object target, PropertyInfo propertyInfo)
        {
            var primitiveGenerator = primitiveGenerators.Get(propertyInfo.PropertyType);
            if (primitiveGenerator != null)
            {
                SetPropertyValue(propertyInfo, target, primitiveGenerator.RandomAsObject());
                return true;
            }
            return false;
        }

        private void ApplyRelations<TTarget>(TTarget target)
        {
            ApplyOneToManyRelations(target, oneToManyRelations);
            ApplyManyToOneRelations(target, manyToOneRelations);
        }

        private void ApplyOneToManyRelations<TTarget>(TTarget target, List<OneToManyRelation> oneToManies)
        {
            var relations = oneToManies.Where(r => r.One.IsAssignableFrom(target.GetType())).ToList();
            foreach (var relation in relations)
            {
                var amount = relation.Amount();
                for (int i = 0; i < amount; i++)
                {
                    var many =
                        relation.CreateChildElement == null
                            ? OneWithoutRelations(relation.Many)
                            : OneWithoutRelations(relation.CreateChildElement(target));
                    relation.AddChildElement(target, many);
                    ApplyOneToManyRelations(many, oneToManies.Where(r => r != relation).ToList());
                    ApplyManyToOneRelations(many, manyToOneRelations.Where(r => r.One != relation.One || r.Many != relation.Many).ToList());
                }
            }
        }

        private void ApplyManyToOneRelations<TTarget>(TTarget target, List<ManyToOneRelation> manyToOnes)
        {
            var relations = manyToOnes.Where(r => r.Many.IsAssignableFrom(target.GetType())).ToList();

            foreach (var relation in relations)
            {
                object one = null;
                var amount = relation.Amount();
                if (amount > 0)
                {
                    one = OneWithoutRelations(relation.One);
                    ApplyManyToOneRelations(one, manyToOnes.Where(r => r != relation).ToList());
                    ApplyOneToManyRelations(one, oneToManyRelations.Where(r => r.One != relation.One || r.Many != relation.Many).ToList());
                    relation.Action(target, one);
                }

                for (int i = 1; i < amount; i++)
                {
                    var many = OneWithoutRelations(relation.Many);
                    ApplyManyToOneRelations(one, manyToOnes.Where(r => r != relation).ToList());
                    ApplyOneToManyRelations(one, oneToManyRelations.Where(r => r.One != relation.One || r.Many != relation.Many).ToList());
                    relation.Action(many, one);
                }
            }
        }

        public T One<T>()
        {
            return (T)One(typeof(T));
        }

        public object One(Type resultType)
        {
            generatedObjects.Clear();
            var result = AnotherOne(resultType);
            ApplyActionConventions();
            return result;
        }

        private void ApplyActionConventions()
        {
            var indices = new Dictionary<Type, int>();
            foreach (var actionConvention in actionConventions)
            {
                for (int i = 0; i < generatedObjects.Count(); i++)
                {
                    var obj = generatedObjects[i];

                    if (actionConvention.Type.IsAssignableFrom(obj.GetType()))
                    {
                        if (actionConvention.IndexAction != null)
                        {
                            if (!indices.Keys.Contains(actionConvention.Type))
                                indices[actionConvention.Type] = 0;
                            var index = indices[actionConvention.Type];
                            actionConvention.IndexAction(index, obj);
                            indices[actionConvention.Type] = index + 1;
                            continue;
                        }
                        actionConvention.Action(obj);
                    }
                }
            }
        }

        public object AnotherOne(Type resultType)
        {
            var result = OneWithoutRelations(resultType);
            ApplyRelations(result);
            return result;
        }

        public object OneWithoutRelations(object target)
        {
            if (target == null)
                return null;

            generatedObjects.Add(target);
            foreach (var propertyInfo in target.GetType().GetProperties(MyBinding.Flags))
            {
                if (IsSimpleProperty(target, propertyInfo))
                    continue;
            }
            return target;
        }

        public object OneWithoutRelations(Type type)
        {
            var choiceConvention = choiceConventions.FirstOrDefault(c => c.Type == type);
            if (choiceConvention != null)
                return choiceConvention.Possibilities.PickOne();

            return OneWithoutRelations(Object(type));
        }

        public bool IsWritable(PropertyInfo propertyInfo)
        {
            if (propertyInfo.CanWrite)
                return true;
            var info = propertyInfo.DeclaringType.GetProperty(propertyInfo.Name);
            if (info == null)
                return false;
            return info.CanWrite;
        }

        public bool IsSimpleProperty(object target, PropertyInfo propertyInfo)
        {
            if (!IsWritable(propertyInfo))
                return true;
            if (NeedsToBeIgnored(propertyInfo))
                return true;
            if (IsAKnownPossibility(target, propertyInfo))
                return true;
            if (MatchesAConvention(target, propertyInfo))
                return true;
            if (IsAKnownPrimitive(target, propertyInfo))
                return true;
            if (IsAnEnumeration(target, propertyInfo))
                return true;
            if (IsComponent(target, propertyInfo))
                return true;
            return false;
        }

        private IEnumerable<T> PrivateMany<T>(int numberOfMany)
        {
            generatedObjects.Clear();
            var list = new List<T>();
            for (int i = 0; i < numberOfMany; i++)
            {
                list.Add((T)AnotherOne(typeof(T)));
            }
            ApplyActionConventions();
            return list;
        }

        public IEnumerable<T> Many<T>(int numberOfMany)
        {
            return PrivateMany<T>(numberOfMany).ToList();
        }

        public IEnumerable<T> Many<T>(int minNumberOfMany, int maxNumberOfMany)
        {
            var numberOfMany = new IntGenerator(minNumberOfMany, maxNumberOfMany).GetRandomValue();
            return PrivateMany<T>(numberOfMany).ToList();
        }

        public DomainModifier<T> ModifyThis<T>(T somethingToModify)
        {
            return new DomainModifier<T>(somethingToModify, this);
        }

        private void SetPropertyValue(PropertyInfo propertyInfo, object target, object value)
        {
            var prop = propertyInfo;
            if(!prop.CanWrite)
                prop = propertyInfo.DeclaringType.GetProperty(propertyInfo.Name);
            prop.SetValue(target, value, null);    
        }

        private bool IsComponentType(Type type)
        {
            return (componentTypes.Any(t => t == type));
        }

        private bool IsComponent(object target, PropertyInfo propertyInfo)
        {
            if (IsComponentType(propertyInfo.PropertyType))
            {
                if(target.GetType() == propertyInfo.PropertyType)
                    throw new RecursiveRelationDefinedException(String.Format("Component Type for {0} is same as {1}.", target.GetType(), propertyInfo.PropertyType));

                SetPropertyValue(propertyInfo, target, OneWithoutRelations(propertyInfo.PropertyType));
                return true;
            }
            return false;
        }

        private readonly List<Type> componentTypes = new List<Type>();
        public DomainGenerator Component<T>()
        {
            CheckIfComponentIsInRelation(typeof (T));
            componentTypes.Add(typeof(T));
            return this;
        }

        private void CheckIfComponentIsInRelation(Type type)
        {
            if(oneToManyRelations.Any(r => r.One == type || r.Many == type))
                throw new NoRelationAllowedOnComponentsException(String.Format("Component : {0}.", type.Name));
            if (manyToOneRelations.Any(r => r.One == type || r.Many == type))
                throw new NoRelationAllowedOnComponentsException(String.Format("Component : {0}.", type.Name));
        }

        private bool IsAnEnumeration(object target, PropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyType.IsEnum)
            {

                SetPropertyValue(propertyInfo, target, GetEnumValues(propertyInfo.PropertyType).PickOne());
                return true;
            }
            return false;
        }

        private static IEnumerable<object> GetEnumValues(IReflect type)
        {
            return
                type.GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Select(i => i.GetRawConstantValue());
        }

        public void CheckForRecursiveRelation(Type type, Type parameterType)
        {
            var oneToMany = oneToManyRelations.FirstOrDefault(r => r.One == parameterType && r.Many == type);
            if(oneToMany != null)
                throw new RecursiveRelationDefinedException(String.Format("From {0} to {1}.", oneToMany.One, oneToMany.Many));
            var manyToOne = manyToOneRelations.FirstOrDefault(r => r.Many == parameterType && r.One == type);
            if (manyToOne != null)
                throw new RecursiveRelationDefinedException(String.Format("From {0} to {1}.", manyToOne.Many, manyToOne.One));
        }

        private object Object(Type inputType)
        {
            var isPrimitiveGenerator = primitiveGenerators.Get(inputType);
            if (isPrimitiveGenerator != null)
            {
                return isPrimitiveGenerator.RandomAsObject();
            }
            var type = GetTypeToGenerate(inputType);

            var constructorTypeParameters = GetConstructorTypeParameters(type);
            if (constructorTypeParameters != null)
            {
                var constructor =
                    type
                        .GetConstructors(MyBinding.Flags)
                        .Where(c => c.GetParameters().Count() == constructorTypeParameters.Count())
                        .FirstOrDefault(
                            c =>
                                {
                                    var ctorParams = c.GetParameters();
                                    int ix = 0;
                                    foreach (var parameterInfo in ctorParams)
                                    {
                                        if (parameterInfo.ParameterType != constructorTypeParameters[ix].Type)
                                            return false;
                                        ix++;

                                    }
                                    return true;
                                });
                if (constructor == null)
                {
                    var types = String.Join(", ", constructorTypeParameters.Select(pi => pi.Type.Name).ToArray());
                    throw new CantFindConstructorException(String.Format("For these types : {0}.", types));
                }
                return Construct(type, constructor, constructorTypeParameters);
            }

            if (constructionConventions.Keys.Any(t => t.IsAssignableFrom(type)))
            {
                if (constructionConventions.Keys.Contains(type))
                    return constructionConventions[type]();
            }

            var publicConstructors = type.GetConstructors(MyBinding.Flags);

            if (publicConstructors.Count() > 0)
            {
                var highestParameterCount = publicConstructors.Max(c => c.GetParameters().Count());
                var constructor = publicConstructors.First(c => c.GetParameters().Count() == highestParameterCount);
                return Construct(type, constructor, highestParameterCount);
            }

            var allConstructors = type.GetConstructors(MyBinding.Flags);
            if (allConstructors.Count() > 0)
            {
                var highestParameterCount = allConstructors.Max(c => c.GetParameters().Count());
                var constructor = allConstructors.First(c => c.GetParameters().Count() == highestParameterCount);
                return Construct(type, constructor, highestParameterCount);
            }
            return Activator.CreateInstance(type);
        }

        private object Construct(Type type, ConstructorInfo constructor, ConstructorParameterInfo[] constructorTypeParameters)
        {
            var parameterValues = new object[constructorTypeParameters.Count()];
            int i = 0;
            foreach (var constructorTypeParameter in constructorTypeParameters)
            {
                CheckForRecursiveRelation(type, constructorTypeParameter.Type);
                parameterValues.SetValue(
                    constructorTypeParameter.Generate != null
                        ? constructorTypeParameter.Generate()
                        : AnotherOne(constructorTypeParameter.Type), i);
                i++;
            }
            return constructor.Invoke(parameterValues);
        }

        private object Construct(Type type, ConstructorInfo constructor, int highestParameterCount)
        {
            var parameters = constructor.GetParameters();
            var parameterValues = new object[highestParameterCount];
            for (int i = 0; i < parameters.Length; i++)
            {
                CheckForRecursiveRelation(type, parameters[i].ParameterType);
                parameterValues.SetValue(AnotherOne(parameters[i].ParameterType), i);
            }
            return constructor.Invoke(parameterValues);
        }
    }
}