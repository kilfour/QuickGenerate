using QuickGenerate.DomainGeneratorImplementation;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class CustomizeTypeSpecificCompositeValueTests
    {
        private readonly IDomainGenerator domainGenerator;

        public CustomizeTypeSpecificCompositeValueTests()
        {
            domainGenerator =
                new DomainGenerator()
                    .With(new SomethingToGenerate {Property = 42, OtherProperty = 11})
                    .With(new SomethingDerivedToGenerate {Property = 42, OtherProperty = 11});
        }

        [Fact]
        public void GeneratorIsApplied()
        {
            10.Times(
                () =>
                    {
                        Assert.Equal(42, domainGenerator.One<SomethingToGenerate>().Property);
                        Assert.Equal(11, domainGenerator.One<SomethingToGenerate>().OtherProperty);
                    });
        }

        [Fact]
        public void GeneratorIsAppliedToDerived()
        {
            10.Times(
                () =>
                {
                    Assert.Equal(42, domainGenerator.One<SomethingDerivedToGenerate>().Property);
                    Assert.Equal(11, domainGenerator.One<SomethingDerivedToGenerate>().OtherProperty);
                });
        }

        public class SomethingToGenerate
        {
            public int Property { get; set; }
            public int OtherProperty { get; set; }
        }

        public class SomethingDerivedToGenerate : SomethingToGenerate { }
    }
}