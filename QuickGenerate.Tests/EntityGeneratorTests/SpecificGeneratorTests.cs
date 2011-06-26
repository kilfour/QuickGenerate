using QuickGenerate.Primitives;
using Xunit;

namespace QuickGenerate.Tests.EntityGeneratorTests
{
    public class SpecificGeneratorTests
    {
        private readonly EntityGenerator<SomethingToGenerate> domainGenerator =
                new EntityGenerator<SomethingToGenerate>()
                    .For(e => e.Property, new IntGenerator(42, 42));

        [Fact]
        public void GeneratorIsApplied()
        {
            10.Times(() => Assert.Equal(42, domainGenerator.One().Property));
        }

        [Fact]
        public void GeneratorIsAppliedToDerived()
        {
            10.Times(() => Assert.Equal(42, domainGenerator.One().Property));
        }

        public class SomethingToGenerate
        {
            public int Property { get; set; }
        }

        public class SomethingDerivedToGenerate : SomethingToGenerate { }
    }
}