using QuickGenerate.Primitives;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class CustomizeTypeSpecificGeneratorTests
    {
        private readonly DomainGenerator domainGenerator =
                new DomainGenerator()
                    .With<SomethingToGenerate>(g => g.For(e => e.Property, new IntGenerator(42, 42)));

        [Fact]
        public void GeneratorIsApplied()
        {
            10.Times(() => Assert.Equal(42, domainGenerator.One<SomethingToGenerate>().Property));
        }

        [Fact]
        public void GeneratorIsAppliedToDerived()
        {
            10.Times(() => Assert.Equal(42, domainGenerator.One<SomethingDerivedToGenerate>().Property));
        }

        public class SomethingToGenerate
        {
            public int Property { get; set; }
        }

        public class SomethingDerivedToGenerate : SomethingToGenerate { }
    }
}