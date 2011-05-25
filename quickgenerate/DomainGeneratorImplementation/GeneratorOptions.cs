using System;
using System.Linq.Expressions;
using System.Reflection;
using QuickGenerate.Implementation;
using QuickGenerate.Primitives;
using QuickGenerate.Reflect;

namespace QuickGenerate.DomainGeneratorImplementation
{
    public class GeneratorOptions<T>
    {
        //public T Entity { get { return (T)domainGenerator.GeneratedObjects.Last(obj => obj.GetType() == typeof (T)); } }

        private readonly DomainGenerator domainGenerator;

        public GeneratorOptions(DomainGenerator domainGenerator)
        {
            this.domainGenerator = domainGenerator;
        }

        public GeneratorOptions<T> For<TProperty>(
            Expression<Func<T, TProperty>> propertyExpression,
            params IGenerator<TProperty>[] generators)
        {
            return AddConvention(propertyExpression, () => generators.PickOne().GetRandomValue());
        }

        public GeneratorOptions<T> For<TProperty>(
            Expression<Func<T, TProperty>> propertyExpression,
            params TProperty[] choices)
        {
            return AddConvention(propertyExpression, () => choices.PickOne());
        }

        public GeneratorOptions<T> For<TProperty>(
            Expression<Func<T, TProperty>> propertyExpression,
            Func<TProperty> valueFunc)
        {
            return AddConvention(propertyExpression, () => valueFunc());
        }

        public GeneratorOptions<T> For<TProperty>(
            Expression<Func<T, TProperty>> propertyExpression,
            Func<TProperty, TProperty> modifyFunc)
        {
            domainGenerator.actionConventions.Add(
                new ActionConvention
                    {
                        Type = typeof(T),
                        Action = t => propertyExpression.Set((T)t, modifyFunc(propertyExpression.Get((T)t)))
                    });
            return this;
        }

        public GeneratorOptions<T> For<TSeed, TProperty>(
            Expression<Func<T, TProperty>> propertyExpression,
            TSeed seed,
            Func<TSeed, TSeed> valueFunc,
            Func<TSeed, TProperty> castFunc)
        {
            var generator = new FuncGenerator<TSeed, TProperty>(seed, valueFunc, castFunc);
            AddConvention(propertyExpression, () => generator.GetRandomValue());
            return this;
        }

        public GeneratorOptions<T> For<TProperty>(
            Expression<Func<T, TProperty>> propertyExpression,
            TProperty seed,
            Func<TProperty, TProperty> valueFunc)
        {
            var generator = new FuncGenerator<TProperty>(seed, valueFunc);
            AddConvention(propertyExpression, () => generator.GetRandomValue());
            return this;
        }

        public GeneratorOptions<T> AppendCounter(Expression<Func<T, string>> propertyExpression)
        {
            var generator = new FuncGenerator<long, string>(0, val => ++val, val => val.ToString());
            return For(propertyExpression, val => val + generator.GetRandomValue());
        }

        public GeneratorOptions<T> Counter(Expression<Func<T, int>> propertyExpression)
        {
            var generator = new FuncGenerator<int>(0, val => ++val);
            return For(propertyExpression, generator);
        }

        public GeneratorOptions<T> Counter(Expression<Func<T, int>> propertyExpression, int startingValue)
        {
            var generator = new FuncGenerator<int>(startingValue - 1, val => ++val);
            return For(propertyExpression, generator);
        }

        public GeneratorOptions<T> Counter(Expression<Func<T, int>> propertyExpression, int startingValue, int step)
        {
            var generator = new FuncGenerator<int>(startingValue - step, val => val = val + step);
            return For(propertyExpression, generator);
        }

        public GeneratorOptions<T> Counter(Expression<Func<T, long>> propertyExpression)
        {
            var generator = new FuncGenerator<long>(0, val => ++val);
            return For(propertyExpression, generator);
        }

        public GeneratorOptions<T> Counter(Expression<Func<T, long>> propertyExpression, long startingValue)
        {
            var generator = new FuncGenerator<long>(startingValue - 1, val => ++val);
            return For(propertyExpression, generator);
        }

        public GeneratorOptions<T> Counter(Expression<Func<T, long>> propertyExpression, long startingValue, long step)
        {
            var generator = new FuncGenerator<long>(startingValue - step, val => val = val + step);
            return For(propertyExpression, generator);
        }

        public GeneratorOptions<T> AppendCounter(Expression<Func<T, string>> propertyExpression, Func<string> prefix)
        {
            var generator = new FuncGenerator<long, string>(0, val => ++val, val => val.ToString());
            return For(propertyExpression, () => prefix() + generator.GetRandomValue());
        }

        private GeneratorOptions<T> AddConvention<TProperty>(
            Expression<Func<T, TProperty>> propertyExpression,
            Func<object> func)
        {
            domainGenerator.dynamicValueConventions[GetPredicate(propertyExpression)] = pi => func();
            return this;
        }

        private static Func<MemberInfo, bool> GetPredicate<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            Func<MemberInfo, bool> predicate =
                mi => typeof(T).IsAssignableFrom(mi.ReflectedType)
                      && mi.Name == propertyExpression.AsPropertyInfo().Name;
            return predicate;
        }

        public GeneratorOptions<T> StartingValue(Func<T> func)
        {
            domainGenerator.constructionConventions[typeof(T)] = () => func();
            return this;
        }

        public GeneratorOptions<T> Method(Expression<Action<T>> method)
        {
            domainGenerator.actionConventions.Add(
                new ActionConvention
                    {
                        Type = typeof(T),
                        Action = t => method.Compile().Invoke((T)t)
                    });
            return this;
        }

        public GeneratorOptions<T> Method<TParameter>(Expression<Action<T, TParameter>> method)
        {
            domainGenerator.actionConventions.Add(
                new ActionConvention
                    {
                        Type = typeof(T),
                        Action = t => method.Compile().Invoke((T)t, (TParameter)domainGenerator.OneWithoutRelations(typeof(TParameter)))
                    });
            return this;
        }

        public GeneratorOptions<T> Method<TParameter>(int minTimes, int maxTimes, Expression<Action<T, TParameter>> method)
        {
            Action<object> action =
                t => method.Compile().Invoke((T) t, (TParameter) domainGenerator.OneWithoutRelations(typeof (TParameter)));

            Action<object> decoratedAction =
                t => new[] {minTimes, maxTimes}.FromRange().Times(() => action(t));

            domainGenerator.actionConventions.Add(
                new ActionConvention
                {
                    Type = typeof(T),
                    Action = decoratedAction
                });

            return this;
        }

        public GeneratorOptions<T> Method<TParameterOne, TParameterTwo>(Expression<Action<T, TParameterOne, TParameterTwo>> method)
        {
            domainGenerator.actionConventions.Add(
                new ActionConvention
                    {
                        Type = typeof(T),
                        Action =
                            t => method.Compile().Invoke(
                                (T)t,
                                (TParameterOne)domainGenerator.OneWithoutRelations(typeof(TParameterOne)),
                                (TParameterTwo)domainGenerator.OneWithoutRelations(typeof(TParameterTwo)))
                    });
            return this;
        }

        public GeneratorOptions<T> Method<TParameterOne, TParameterTwo>(int times, Expression<Action<T, TParameterOne, TParameterTwo>> method)
        {
            times.Times(() => Method(method));
            return this;
        }

        public GeneratorOptions<T> Length(Expression<Func<T, string>> propertyExpression, int length)
        {
            return For(propertyExpression, new StringGenerator(length, length));
        }

        public GeneratorOptions<T> Length(Expression<Func<T, string>> propertyExpression, int minlength, int maxlength)
        {
            return For(propertyExpression, new StringGenerator(minlength, maxlength));
        }

        public GeneratorOptions<T> Range(Expression<Func<T, decimal>> propertyExpression, decimal min, decimal max)
        {
            var generator = new DecimalGenerator(min, max);
            return For(propertyExpression, generator);
        }

        public GeneratorOptions<T> Range(Expression<Func<T, double>> propertyExpression, double min, double max)
        {
            var generator = new DoubleGenerator(min, max);
            return For(propertyExpression, generator);
        }

        public GeneratorOptions<T> Range(Expression<Func<T, int>> propertyExpression, int min, int max)
        {
            var generator = new IntGenerator(min, max);
            return For(propertyExpression, generator);
        }

        public GeneratorOptions<T> Range(Expression<Func<T, long>> propertyExpression, long min, long max)
        {
            var generator = new LongGenerator(min, max);
            return For(propertyExpression, generator);
        }

        public GeneratorOptions<T> Range(Expression<Func<T, DateTime>> propertyExpression, DateTime min, DateTime max)
        {
            var generator = new DateTimeGenerator(min, max);
            return For(propertyExpression, generator);
        }
    }
}