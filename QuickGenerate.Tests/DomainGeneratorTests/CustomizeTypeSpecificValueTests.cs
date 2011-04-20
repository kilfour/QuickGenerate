using QuickGenerate.Tests.DomainGeneratorTests.TheDomain;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class CustomizeTypeSpecificValueTests
    {
        private readonly DomainGenerator domainGenerator;

        public CustomizeTypeSpecificValueTests()
        {
            domainGenerator =
                new DomainGenerator()
                    .With<Product>(g => g.For(e => e.Id, 42));
        }

        [Fact]
        public void GeneratorIsApplied()
        {
            10.Times(() => Assert.Equal(42, domainGenerator.One<Product>().Id));
        }
    }
}