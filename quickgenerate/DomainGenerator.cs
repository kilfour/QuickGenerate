using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using QuickGenerate.DomainGeneratorImplementation;
using QuickGenerate.Implementation;
using QuickGenerate.Interfaces;
using QuickGenerate.Modifying;
using QuickGenerate.Primitives;
using QuickGenerate.Reflect;

namespace QuickGenerate
{
    public class DomainGenerator
    {
        public const BindingFlags FlattenHierarchyBindingFlag =
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy;

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

        public readonly List<ActionConvention> actionConventions
            = new List<ActionConvention>();

        public readonly List<ChoiceConvention> choiceConventions
            = new List<ChoiceConvention>();

        public readonly Dictionary<Type, Func<object>> constructionConventions
            = new Dictionary<Type, Func<object>>();

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
            oneToManyRelations.Add(
                new OneToManyRelation
                    {
                        Action = (one, many) => action((TOne)one, (TMany)many),
                        Amount = () => numberOfMany,
                        One = typeof(TOne), 
                        Many = typeof(TMany)
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
            oneToManyRelations.Add(
                new OneToManyRelation
                {
                    ManyFunc = one => manyFunc((TOne)one),
                    Action = (one, many) => action((TOne)one, (TMany)many),
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
                    Action = (one, many) => action((TOne)one, (TMany)many),
                    Amount = () => new IntGenerator(minmumNumberOfMany, maximumNumberOfMany).GetRandomValue(),
                    One = typeof(TOne),
                    Many = typeof(TMany)
                });
            return this;
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

        private readonly IDictionary<Type, IIgnoreGeneratorOptions> ignoreGeneratorOptions =
            new Dictionary<Type, IIgnoreGeneratorOptions>();

        public DomainGenerator With<T>(Action<IgnoreGeneratorOptions<T>> customization)
        {
            if (!ignoreGeneratorOptions.ContainsKey(typeof(T)))
                ignoreGeneratorOptions[typeof (T)] = new IgnoreGeneratorOptions<T>();
            customization((IgnoreGeneratorOptions<T>)ignoreGeneratorOptions[typeof(T)]);
            return this;
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
                propertyInfo.SetValue(target, choice, null);
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
            propertyInfo.SetValue(target, value, null);
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
                propertyInfo.SetValue(target, primitiveGenerator.RandomAsObject(), null);
                return true;
            }
            return false;
        }

        private void ApplyRelations<TTarget>(TTarget target, List<OneToManyRelation> oneToManies)
        {
            ApplyOneToManyRelations(target, oneToManies);
        }

        private void ApplyRelations<TTarget>(TTarget target)
        {
            ApplyRelations(target, oneToManyRelations);
        }

        private void ApplyOneToManyRelations<TTarget>(TTarget target, List<OneToManyRelation> oneToManies)
        {
            var relations = oneToManies.Where(r => r.Many.IsAssignableFrom(target.GetType())).ToList();
            
            foreach (var relation in relations)
            {
                object one = null;
                var amount = relation.Amount();
                if (amount > 0)
                {
                    one = OneWithoutRelations(relation.One);
                    ApplyRelations(one, oneToManies.Where(r => r != relation).ToList());
                    relation.Action(one, target);
                }

                for (int i = 1; i < amount; i++)
                {
                    var many =
                        relation.ManyFunc == null
                            ? OneWithoutRelations(relation.Many)
                            : OneWithoutRelations(relation.ManyFunc(one));
                    ApplyRelations(many, oneToManies.Where(r => r != relation).ToList()); 
                    relation.Action(one, many);
                }
            }

            relations = oneToManies.Where(r => r.One.IsAssignableFrom(target.GetType())).ToList();
            foreach (var relation in relations)
            {
                var amount = relation.Amount();
                for (int i = 0; i < amount; i++)
                {
                    var many =
                        relation.ManyFunc == null
                            ? OneWithoutRelations(relation.Many)
                            : OneWithoutRelations(relation.ManyFunc(target));
                    relation.Action(target, many);
                    ApplyRelations(many, oneToManies.Where(r => r != relation).ToList());
                }
            }
        }

        public T One<T>()
        {
            generatedObjects.Clear();
            var target = One(typeof(T));
            foreach (var actionConvention in actionConventions)
            {
                for (int i = 0; i < generatedObjects.Count(); i++)
                {
                    var obj = generatedObjects[i];
                    if (actionConvention.Type.IsAssignableFrom(obj.GetType()))
                        actionConvention.Action(obj);
                }
            }
            return (T)target;
        }

        private object One(Type resultType)
        {
            var result = OneWithoutRelations(resultType);
            ApplyRelations(result);
            return result;
        }

        public object OneWithoutRelations(object target)
        {
            if (target == null)
                return target;

            generatedObjects.Add(target);
            foreach (var propertyInfo in target.GetType().GetProperties(FlattenHierarchyBindingFlag))
            {
                if (IsSimpleProperty(target, propertyInfo))
                    continue;
            }
            return target;
        }

        public object OneWithoutRelations(Type type)
        {
            return OneWithoutRelations(Create.Object(this, type));
        }

        public bool IsSimpleProperty(object target, PropertyInfo propertyInfo)
        {
            if (!propertyInfo.CanWrite)
                return true;
            if (NeedsToBeIgnored(propertyInfo))
                return true;
            if (IsAKnownPossibility(target, propertyInfo))
                return true;
            if (MatchesAConvention(target, propertyInfo))
                return true;
            if (IsAKnownPrimitive(target, propertyInfo))
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

        public DomainGenerator WithStringNameCounterPattern()
        {
            return WithStringNameCounterPattern(0);
        }

        public DomainGenerator WithStringNameCounterPattern(int startingValue)
        {
            Func<MemberInfo, bool> predicate = mi => ((PropertyInfo)mi).PropertyType.IsAssignableFrom(typeof(string));
            dynamicValueConventions[predicate] = pi => pi.Name + GetGenerator(pi, startingValue).GetRandomValue().ToString();
            return this;
        }

        private Dictionary<PropertyInfo, FuncGenerator<int>> counters =
            new Dictionary<PropertyInfo, FuncGenerator<int>>();

        private FuncGenerator<int> GetGenerator(PropertyInfo propertyInfo, int startingValue)
        {
            if(!counters.ContainsKey(propertyInfo))
                counters[propertyInfo] = new FuncGenerator<int>(startingValue, val => ++val);
            return counters[propertyInfo];
        }

        private bool IsComponent(object target, PropertyInfo propertyInfo)
        {
            if (componentTypes.Any(t => t == propertyInfo.PropertyType))
            {
                propertyInfo.SetValue(target, OneWithoutRelations(propertyInfo.PropertyType), null);
                return true;
            }
            return false;
        }

        private readonly List<Type> componentTypes = new List<Type>();
        public DomainGenerator Component<T>()
        {
            componentTypes.Add(typeof(T));
            return this;
        }
    }
}