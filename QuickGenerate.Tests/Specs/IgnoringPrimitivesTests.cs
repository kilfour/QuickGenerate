using System;
using System.Linq.Expressions;
using System.Reflection;
using QuickDotNetCheck;
using QuickDotNetCheck.NotInTheRoot;
using QuickGenerate.Reflect;
using QuickGenerate.Tests.Specs.TestObjects;
using Xunit;

namespace QuickGenerate.Tests.Specs
{
    public class IgnoringPrimitivesTests 
    {
        [Fact]
        public void RunTest()
        {
            var suite = new Suite(100);
            suite.Using(() => new DomainGenerator().With(true));
            RegisterFixtures(suite);
            suite.Run();
        }

        public Suite RegisterFixtures (Suite suite)
        {
            return
                suite.Do(1, opt => opt
                    .Register(Build<AllPrimitives, long>(e => e.LongProperty))
                    .Register(Build<AllPrimitives, DateTime>(e => e.DateTimeProperty))
                    .Register(Build<AllPrimitives, short>(e => e.ShortProperty))
                    .Register(Build<AllPrimitives, decimal>(e => e.DecimalProperty))
                    .Register(Build<AllPrimitives, int>(e => e.IntProperty))
                    .Register(Build<AllPrimitives, string>(e => e.StringProperty))
                    .Register(Build<AllPrimitives, Guid>(e => e.GuidProperty))
                    .Register(Build<AllPrimitives, char>(e => e.CharProperty))
                    .Register(Build<AllPrimitives, float>(e => e.FloatProperty))
                    .Register(Build<AllPrimitives, TimeSpan>(e => e.TimeSpanProperty))
                    .Register(Build<AllPrimitives, double>(e => e.DoubleProperty)));
        }

        private Func<IFixture> Build<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> expression)
        {
            return () => new PrimitiveIgnoreFixture<TEntity, TProperty>(expression);
        }
    }

    public class PrimitiveIgnoreFixture<TEntity, TProperty> : Fixture, IUse<DomainGenerator>
    {
        private DomainGenerator generator;
        private readonly Expression<Func<TEntity, TProperty>> expression;
        private readonly PropertyInfo propertyInfo;
        private object thing { get; set; }

        public void Set(DomainGenerator state)
        {
            generator = state;
        }

        public PrimitiveIgnoreFixture(Expression<Func<TEntity, TProperty>> expression)
        {
            this.expression = expression;
            propertyInfo = expression.AsPropertyInfo();
        }

        public override void Arrange()
        {
            generator.With<TEntity>(opt => opt.Ignore(expression));
        }

        protected override void Act()
        {
            thing = generator.One<TEntity>();
        }

        [Spec]
        public void DefaultValueIsGeneratedForIgnoredPrimitives()
        {
            Inspect
                .This(thing)
                .ForAll(
                    pi => pi.Name == propertyInfo.Name && pi.DeclaringType == propertyInfo.DeclaringType,
                    (pi, p) => Ensure.True(IsDefaultValue(p), pi.Name));
        }

        [Spec]
        public void NoDefaultValueIsGeneratedForNonIgnoredPrimitives()
        {
            Inspect
                .This(thing)
                .ForAll(
                    pi => pi.Name != propertyInfo.Name || pi.DeclaringType != propertyInfo.DeclaringType,
                    (pi, p) => Ensure.False(IsDefaultValue(p), pi.Name));
        }

        public Spec TheDefaultStringValueIsNull()
        {
            return new Spec(
                () => Inspect
                          .This(thing)
                          .ForAll(
                              pi => pi.Name == propertyInfo.Name && pi.DeclaringType == propertyInfo.DeclaringType,
                              (pi, p) => Ensure.Null(p, pi.Name)))
                .If(() => propertyInfo.PropertyType == typeof (string));
        }

 
        public Spec NoOtherButTheDefaultStringValueIsNull()
        {
            return new Spec(
                () => Inspect
                          .This(thing)
                          .ForAll(
                              pi => pi.Name == propertyInfo.Name && pi.DeclaringType == propertyInfo.DeclaringType,
                              (pi, p) => Ensure.NotNull(p, pi.Name)))
                .If(() => propertyInfo.PropertyType != typeof(string));
        }

        private bool IsDefaultValue(object obj)
        {
            if (obj is string)
                return Equals(obj, default(string));
            if (obj == null)
                return true;
            return Equals(obj, Activator.CreateInstance(obj.GetType(), null));
        }
    }
}