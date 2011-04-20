using QuickGenerate.Primitives;
using QuickGenerate.Tests.DomainGeneratorTests.TheDomain;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class CustomizeTypeSpecificGeneratorTests
    {
        private readonly DomainGenerator domainGenerator;

        public CustomizeTypeSpecificGeneratorTests()
        {
            domainGenerator =
                new DomainGenerator()
                    .With<Product>(g => g.For(e => e.Id, new IntGenerator(42, 42)));
        }

        [Fact]
        public void GeneratorIsApplied()
        {
            10.Times(() => Assert.Equal(42, domainGenerator.One<Product>().Id));
        }
    }
}