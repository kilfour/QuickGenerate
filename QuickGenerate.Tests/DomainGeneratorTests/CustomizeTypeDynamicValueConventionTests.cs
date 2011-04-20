using QuickGenerate.Tests.DomainGeneratorTests.TheDomain;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class CustomizeTypeDynamicValueConventionTests
    {
        private readonly DomainGenerator domainGenerator;

        public CustomizeTypeDynamicValueConventionTests()
        {
            domainGenerator =
                new DomainGenerator()
                    .With<Product>(g => g.For(e => e.Id, GetNextProductId));
        }

        private int autoincrement;
        private int GetNextProductId()
        {
            return ++autoincrement;
        }

        [Fact]
        public void JustWorks()
        {
            var product1 = domainGenerator.One<Product>();
            Assert.Equal(1, product1.Id);

            var product2 = domainGenerator.One<Product>();
            Assert.Equal(2, product2.Id);

            var product3 = domainGenerator.One<Product>();
            Assert.Equal(3, product3.Id);
        }
    }
}