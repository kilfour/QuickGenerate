using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using QuickGenerate.DomainGeneratorImplementation;
using QuickGenerate.Implementation;
using QuickGenerate.Modifying;
using QuickGenerate.Primitives;
using QuickGenerate.Reflect;

namespace QuickGenerate
{
    public class DomainGenerator
    {
        private readonly List<object> generatedObjects = new List<object>();

        public IEnumerable<object> GeneratedObjects {get { return generatedObjects; }}

        private readonly PrimitiveGenerators primitiveGenerators =
            new PrimitiveGenerators();

        private readonly List<Func<MemberInfo, bool>> ignoreConventions = 
            new List<Func<MemberInfo, bool>>();

        public readonly Dictionary<Func<MemberInfo, bool>, Func<PropertyInfo, object>> dynamicValueConventions =
            new Dictionary<Func<MemberInfo, bool>, Func<PropertyInfo, object>>();

        private readonly List<OneToManyRelation> oneToManyRelations =
            new List<OneToManyRelation>();

        private readonly List<ManyToOneRelation> manyToOneRelations =
            new List<ManyToOneRelation>();

        public readonly List<ActionConvention> actionConventions
            = new List<ActionConvention>();

        public readonly List<ChoiceConvention> choiceConventions
            = new List<ChoiceConvention>();

        public readonly Dictionary<Type, Func<object>> constructionConventions
            = new Dictionary<Type, Func<object>>();

        private readonly IDictionary<Type, IIgnoreGeneratorOptions> ignoreGeneratorOptions =
            new Dictionary<Type, IIgnoreGeneratorOptions>();

        private readonly IDictionary<Type, IInheritanceGeneratorOptions> inheritanceGeneratorOptions =
            new Dictionary<Type, IInheritanceGeneratorOptions>();

        private readonly IDictionary<Type, IConstructorGeneratorOptions> constructorGeneratorOptions =
            new Dictionary<Type, IConstructorGeneratorOptions>();

        public DomainGenerator Ignore(Func<MemberInfo, bool> ignoreThis)
        {
            ignoreConventions.Add(ignoreThis);
            return this;
        }

        public DomainGenerator Ignore<T, TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            Func<MemberInfo, bool> func =
                mi => mi.ReflectedType == typeof (T)
                      && mi.Name == propertyExpression.AsPropertyInfo().Name;

            ignoreConventions.Add(func);

            return this;
        }

        public DomainGenerator OneToOne<TOne, TMany>(Action<TOne, TMany> action)
        {
            return OneToMany(1, action);
        }

        public DomainGenerator OneToMany<TOne, TMany>(int numberOfMany, Action<TOne, TMany> action)
        {
            var oneType = typeof (TOne);
            if (IsComponentType(oneType))
                throw new NoRelationAllowedOnComponentsException(string.Format("Component : {0}.", oneType.Name));

            var manyType = typeof(TMany);
            //if (IsComponentType(manyType))
            //    throw new NoRelationAllowedOnComponentsException(string.Format("Component : {0}.", manyType.Name));

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
                throw new NoRelationAllowedOnComponentsException(string.Format("Component : {0}.", oneType.Name));

            //var manyType = typeof(TMany);
            //if (IsComponentType(manyType))
            //    throw new NoRelationAllowedOnComponentsException(string.Format("Component : {0}.", manyType.Name));

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
            //var oneType = typeof(TOne);
            //if (IsComponentType(oneType))
            //    throw new NoRelationAllowedOnComponentsException(string.Format("Component : {0}.", oneType.Name));

            var manyType = typeof(TMany);
            if (IsComponentType(manyType))
                throw new NoRelationAllowedOnComponentsException(string.Format("Component : {0}.", manyType.Name));

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

        public DomainGenerator With<T>(Func<MemberInfo, bool> predicate, IGenerator<T> generator)
        {
            dynamicValueConventions[predicate] = pi => generator.RandomAsObject();
            return this;
        }

        public DomainGenerator With<T>(Func<GeneratorOptions<T>, GeneratorOptions<T>> customization)
        {
            customization(new GeneratorOptions<T>(this));
            return this;
        }

        public DomainGenerator With<T>(Action<IgnoreGeneratorOptions<T>> customization)
        {
            if (!ignoreGeneratorOptions.ContainsKey(typeof(T)))
                ignoreGeneratorOptions[typeof (T)] = new IgnoreGeneratorOptions<T>();
            customization((IgnoreGeneratorOptions<T>)ignoreGeneratorOptions[typeof(T)]);
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

        public DomainGenerator With<T>(Func<IGenerator<T>> generatorFunc)
        {
            var generator = generatorFunc();
            Func<MemberInfo, bool> predicate = mi => ((PropertyInfo)mi).PropertyType == typeof(T);
            dynamicValueConventions[predicate] = pi => generator.GetRandomValue();
            return this;
        }

        public DomainGenerator With<T>(params T[] choices)
        {
            choiceConventions
                .Add(
                    new ChoiceConvention
                        {
                            Type = typeof (T), 
                            Possibilities = choices.Cast<object>().ToArray()
                        });
            return this;
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

        private bool NeedsToBeIgnored(PropertyInfo propertyInfo)
        {
            return 
                ignoreConventions.Any(ignoreConvention => ignoreConvention(propertyInfo))
             || ignoreGeneratorOptions.Any(opt => opt.Value.NeedsToBeIgnored(propertyInfo));
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
            foreach (var actionConvention in actionConventions)
            {
                for (int i = 0; i < generatedObjects.Count(); i++)
                {
                    var obj = generatedObjects[i];
                    if (actionConvention.Type.IsAssignableFrom(obj.GetType()))
                        actionConvention.Action(obj);
                }
            }
            return result;
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

            return OneWithoutRelations(this.Object(type));
        }

        private bool IsWritable(PropertyInfo propertyInfo)
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
            for (int i = 0; i < numberOfMany; i++)
            {
                yield return One<T>();
            }
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
                    throw new RecursiveRelationDefinedException(string.Format("Component Type for {0} is same as {1}.", target.GetType(), propertyInfo.PropertyType));

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
                throw new NoRelationAllowedOnComponentsException(string.Format("Component : {0}.", type.Name));
            if (manyToOneRelations.Any(r => r.One == type || r.Many == type))
                throw new NoRelationAllowedOnComponentsException(string.Format("Component : {0}.", type.Name));
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
                throw new RecursiveRelationDefinedException(string.Format("From {0} to {1}.", oneToMany.One, oneToMany.Many));
            var manyToOne = manyToOneRelations.FirstOrDefault(r => r.Many == parameterType && r.One == type);
            if (manyToOne != null)
                throw new RecursiveRelationDefinedException(string.Format("From {0} to {1}.", manyToOne.Many, manyToOne.One));
        }
    }
}