using System;
using System.Linq.Expressions;
using System.Reflection;

namespace QuickGenerate
{
    public class EntityGenerator<TEntity>
    {
        private readonly DomainGenerator generator =
            new DomainGenerator();

        public EntityGenerator<TEntity> With<T>(params T[] choices)
        {
            generator.With(choices);
            return this;
        }

        public EntityGenerator<TEntity> With<T>(Func<MemberInfo, bool> predicate, Func<T> func)
        {
            generator.With(predicate, func);
            return this;
        }

        public EntityGenerator<TEntity> With<T>(Func<MemberInfo, bool> predicate, IGenerator<T> aGenerator)
        {
            generator.With(predicate, aGenerator);
            return this;
        }

        public EntityGenerator<TEntity> Ignore<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression)
        {
            generator.With<TEntity>(opt => opt.Ignore(propertyExpression));
            return this;
        }

        public EntityGenerator<TEntity> StartingValue(Func<TEntity> func)
        {
            generator.With<TEntity>(opt => opt.StartingValue(func));
            return this;
        }

        public EntityGenerator<TEntity> For<TProperty>(
            Expression<Func<TEntity, TProperty>> propertyExpression,
            params TProperty[] choices)
        {
            generator.With<TEntity>(opt => opt.For(propertyExpression, choices));
            return this;
        }

        public EntityGenerator<TEntity> For<TProperty>(
            Expression<Func<TEntity, TProperty>> propertyExpression,
            Func<TProperty> valueFunc)
        {
            generator.With<TEntity>(opt => opt.For(propertyExpression, valueFunc));
            return this;
        }

        public EntityGenerator<TEntity> For<TProperty>(
            Expression<Func<TEntity, TProperty>> propertyExpression,
            params IGenerator<TProperty>[] generators)
        {
            generator.With<TEntity>(opt => opt.For(propertyExpression, generators));
            return this;
        }

        public EntityGenerator<TEntity> For<TProperty>(
            Expression<Func<TEntity, TProperty>> propertyExpression,
            Func<TProperty, TProperty> modifyFunc)
        {
            generator.With<TEntity>(opt => opt.For(propertyExpression, modifyFunc));
            return this;
        }

        public EntityGenerator<TEntity> For<TSeed, TProperty>(
            Expression<Func<TEntity, TProperty>> propertyExpression,
            TSeed seed,
            Func<TSeed, TSeed> valueFunc,
            Func<TSeed, TProperty> castFunc)
        {
            generator.With<TEntity>(opt => opt.For(propertyExpression, seed, valueFunc, castFunc));
            return this;
        }

        public EntityGenerator<TEntity> For<TProperty>(
            Expression<Func<TEntity, TProperty>> propertyExpression,
            TProperty seed,
            Func<TProperty, TProperty> valueFunc)
        {
            generator.With<TEntity>(opt => opt.For(propertyExpression, seed, valueFunc));
            return this;
        }

        public EntityGenerator<TEntity> Component<T>()
        {
            generator.Component<T>();
            return this;
        }

        public EntityGenerator<TEntity> Length(Expression<Func<TEntity, string>> propertyExpression, int length)
        {
            generator.With<TEntity>(opt => opt.Length(propertyExpression, length));
            return this;
        }

        public EntityGenerator<TEntity> Length(Expression<Func<TEntity, string>> propertyExpression, int minlength, int maxlength)
        {
            generator.With<TEntity>(opt => opt.Length(propertyExpression, minlength, maxlength));
            return this;
        }

        public EntityGenerator<TEntity> Range(Expression<Func<TEntity, decimal>> propertyExpression, decimal min, decimal max)
        {
            generator.With<TEntity>(opt => opt.Range(propertyExpression, min, max));
            return this;
        }

        public EntityGenerator<TEntity> Range(Expression<Func<TEntity, double>> propertyExpression, double min, double max)
        {
            generator.With<TEntity>(opt => opt.Range(propertyExpression, min, max));
            return this;
        }

        public EntityGenerator<TEntity> Range(Expression<Func<TEntity, int>> propertyExpression, int min, int max)
        {
            generator.With<TEntity>(opt => opt.Range(propertyExpression, min, max));
            return this;
        }

        public EntityGenerator<TEntity> Range(Expression<Func<TEntity, long>> propertyExpression, long min, long max)
        {
            generator.With<TEntity>(opt => opt.Range(propertyExpression, min, max));
            return this;
        }

        public EntityGenerator<TEntity> Range(Expression<Func<TEntity, DateTime>> propertyExpression, DateTime min, DateTime max)
        {
            generator.With<TEntity>(opt => opt.Range(propertyExpression, min, max));
            return this;
        }

        public EntityGenerator<TEntity> Method<TParameter>(Expression<Action<TEntity, TParameter>> method)
        {
            generator.With<TEntity>(opt => opt.Method(method));
            return this;
        }

        public EntityGenerator<TEntity> Counter(Expression<Func<TEntity, int>> propertyExpression)
        {
            generator.With<TEntity>(opt => opt.Counter(propertyExpression));
            return this;
        }

        public EntityGenerator<TEntity> Counter(Expression<Func<TEntity, int>> propertyExpression, int startingValue)
        {
            generator.With<TEntity>(opt => opt.Counter(propertyExpression, startingValue));
            return this;
        }

        public EntityGenerator<TEntity> Counter(Expression<Func<TEntity, int>> propertyExpression, int startingValue, int step)
        {
            generator.With<TEntity>(opt => opt.Counter(propertyExpression, startingValue, step));
            return this;
        }

        public EntityGenerator<TEntity> Counter(Expression<Func<TEntity, long>> propertyExpression)
        {
            generator.With<TEntity>(opt => opt.Counter(propertyExpression));
            return this;
        }

        public EntityGenerator<TEntity> Counter(Expression<Func<TEntity, long>> propertyExpression, long startingValue)
        {
            generator.With<TEntity>(opt => opt.Counter(propertyExpression, startingValue));
            return this;
        }

        public EntityGenerator<TEntity> Counter(Expression<Func<TEntity, long>> propertyExpression, long startingValue, long step)
        {
            generator.With<TEntity>(opt => opt.Counter(propertyExpression, startingValue, step));
            return this;
        }

        public EntityGenerator<TEntity> AppendCounter(Expression<Func<TEntity, string>> propertyExpression)
        {
            generator.With<TEntity>(opt => opt.AppendCounter(propertyExpression));
            return this;
        }

        public EntityGenerator<TEntity> AppendCounter(Expression<Func<TEntity, string>> propertyExpression, Func<string> prefix)
        {
            generator.With<TEntity>(opt => opt.AppendCounter(propertyExpression, prefix));
            return this;
        }

        public TEntity One()
        {
            return generator.One<TEntity>();
        }
    }
}