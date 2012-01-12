using QuickGenerate.Primitives;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class GeneratorByTypeTests
    {
        [Fact]
        public void GeneratorIsApplied()
        {
            var domainGenerator =
                new DomainGenerator()
                    .With(() => new IntGenerator(42, 42));

            Assert.Equal(42, domainGenerator.One<SomethingToGenerate>().Value);
        }

        public class SomethingToGenerate
        {
            public int Value { get; set; }
        }
    }
}