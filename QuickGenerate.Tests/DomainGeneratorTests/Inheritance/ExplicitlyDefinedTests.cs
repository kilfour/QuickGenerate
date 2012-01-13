using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Inheritance
{
    public class ExplicitlyDefinedTests
    {
        [Fact]
        public void GeneratorIsApplied()
        {
            var generator =
                new DomainGenerator()
                    .With(42)
                    .With<Base>(opt => opt.Use<Base>().Use<DerivedOne>().Use<DerivedTwo>());

            var isBase = false;
            var isDerivedOne = false;
            var isDerivedTwo = false;

            100.Times(
                () =>
                {
                    var thing = generator.One<Base>();
                    if (thing.GetType() == typeof(Base))
                    {
                        isBase = true;
                        Assert.Equal(42, thing.BaseProp);
                    }

                    if (thing.GetType() == typeof(DerivedOne))
                    {
                        var derived = (DerivedOne)thing;
                        isDerivedOne = true;
                        Assert.Equal(42, derived.BaseProp);
                        Assert.Equal(42, derived.DerivedProp);
                    }

                    if (thing.GetType() == typeof(DerivedTwo))
                    {
                        var derived = (DerivedTwo)thing;
                        isDerivedTwo = true;
                        Assert.Equal(42, derived.BaseProp);
                        Assert.Equal(42, derived.DerivedProp);
                    }
                });

            Assert.True(isBase);
            Assert.True(isDerivedOne);
            Assert.True(isDerivedTwo);
        }

        public class Base
        {
            public int BaseProp { get; set; }
        }

        public class DerivedOne : Base
        {
            public int DerivedProp { get; set; }
        }

        public class DerivedTwo : Base
        {
            public int DerivedProp { get; set; }
        }
    }
}
