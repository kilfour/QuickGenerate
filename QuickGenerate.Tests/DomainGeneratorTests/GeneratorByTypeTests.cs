using QuickGenerate.Primitives;
using QuickGenerate.Tests.DomainGeneratorTests.TheDomain;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class GeneratorByTypeTests
    {
        private readonly DomainGenerator domainGenerator;

        public GeneratorByTypeTests()
        {
            domainGenerator =
                new DomainGenerator()
                    .With(() => new IntGenerator(42, 42));
        }

        [Fact]
        public void GeneratorIsApplied()
        {
            Assert.Equal(42, domainGenerator.One<ProductPrice>().Value);
        }
    }
}