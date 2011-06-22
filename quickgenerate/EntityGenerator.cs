using System;
using System.Linq.Expressions;

namespace QuickGenerate
{
    public class EntityGenerator<T>
    {
        private readonly DomainGenerator generator =
            new DomainGenerator();

        public EntityGenerator<T> Ignore<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            generator.With<T>(opt => opt.Ignore(propertyExpression));
            return this;
        }

        public EntityGenerator<T> Range(Expression<Func<T, decimal>> propertyExpression, decimal min, decimal max)
        {
            generator.With<T>(opt => opt.Range(propertyExpression, min, max));
            return this;
        }

        public EntityGenerator<T> Range(Expression<Func<T, double>> propertyExpression, double min, double max)
        {
            generator.With<T>(opt => opt.Range(propertyExpression, min, max));
            return this;
        }

        public EntityGenerator<T> Range(Expression<Func<T, int>> propertyExpression, int min, int max)
        {
            generator.With<T>(opt => opt.Range(propertyExpression, min, max));
            return this;
        }

        public EntityGenerator<T> Range(Expression<Func<T, long>> propertyExpression, long min, long max)
        {
            generator.With<T>(opt => opt.Range(propertyExpression, min, max));
            return this;
        }

        public EntityGenerator<T> Range(Expression<Func<T, DateTime>> propertyExpression, DateTime min, DateTime max)
        {
            generator.With<T>(opt => opt.Range(propertyExpression, min, max));
            return this;
        }

        public T One()
        {
            return generator.One<T>();
        }
    }
}