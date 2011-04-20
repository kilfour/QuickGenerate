using QuickGenerate.Tests.DomainGeneratorTests.TheDomain;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class CustomizeTypePossibleValuesTests
    {
        private readonly DomainGenerator domainGenerator;

        public CustomizeTypePossibleValuesTests()
        {
            domainGenerator =
                new DomainGenerator()
                    .With<Product>(g => g.For(e => e.Id, 42, 43));
        }

        [Fact]
        public void GeneratorIsApplied()
        {
            var is42 = false;
            var is43 = false;
            20.Times(
                () =>
                    {
                        var product = domainGenerator.One<Product>();
                        is42 = is42 || product.Id == 42;
                        is43 = is43 || product.Id == 43;
                    });
            Assert.True(is42);
            Assert.True(is43);
        }
    }
}