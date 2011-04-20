using QuickGenerate.Tests.DomainGeneratorTests.TheDomain;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class CustomizeTypeIgnoreTests
    {
        private readonly DomainGenerator domainGenerator;

        public CustomizeTypeIgnoreTests()
        {
            domainGenerator =
                new DomainGenerator()
                    .With<Product>(g => g.Ignore(e => e.Id))
                    .With<Category>(g => g.Ignore(e => e.Id));
        }

        [Fact]
        public void StaysDefaultvalue()
        {
            10.Times(
                () =>
                {
                    var product = domainGenerator.One<Product>();
                    Assert.Equal(0, product.Id);
                });

            10.Times(
                () =>
                {
                    var category = domainGenerator.One<Category>();
                    Assert.Equal(null, category.Id);
                });
        }
    }
}