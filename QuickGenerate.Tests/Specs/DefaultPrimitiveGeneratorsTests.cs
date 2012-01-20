using System;
using QuickDotNetCheck;
using QuickGenerate.Tests.Specs.TestObjects;
using Xunit;

namespace QuickGenerate.Tests.Specs
{
    public class DefaultPrimitiveGeneratorsTests : Fixture
    {
        private Type type { get; set; }
        private object thing { get; set; }
       
        public override void Arrange()
        {
            type = Pick.TestObjectType();
        }

        protected override void Act()
        {
            thing =
                new DomainGenerator()
                    .With(true)
                    .One(type);
        }

        [Fact]
        public void RunTest()
        {
            new Suite(100, 1)
                .Register<DefaultPrimitiveGeneratorsTests>()
                .Run();
        }

        [Spec]
        public void NoEmptyStringIsGenerated()
        {
            Inspect
                .This(thing)
                .ForAllPrimitives(
                    pi => pi.PropertyType == typeof (string), 
                    p => Ensure.NotEqual(String.Empty, p));
        }

        [Spec]
        public void NoDefaultValueIsGenerated()
        {
            Inspect
                .This(thing)
                .ForAllPrimitives(
                    pi => pi.PropertyType != typeof (bool),
                    (pi, p) => Ensure.False(IsDefaultValue(p), pi.Name));
        }

        [Spec]
        public void NoDefaultValueIsGenerated_ExceptForBools()
        {
            Inspect
                .This(thing)
                .ForAllPrimitives(
                 pi => pi.PropertyType == typeof(bool), 
                 (pi, p) => Ensure.False(IsDefaultValue(p), pi.Name));
        }

        [Spec]
        public void NoNullValuesAreGenerated() 
        {
            Inspect
                .This(thing)
                .ForAllPrimitives((pi, p) => Ensure.NotNull(p, pi.Name));
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