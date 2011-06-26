using Xunit;

namespace QuickGenerate.Tests.EntityGeneratorTests
{
    public class SpecificValueTests
    {
        private readonly EntityGenerator<SomethingToGenerate> domainGenerator;

        public SpecificValueTests()
        {
            domainGenerator =
                new EntityGenerator<SomethingToGenerate>()
                    .For(e => e.Property, 42);
        }

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