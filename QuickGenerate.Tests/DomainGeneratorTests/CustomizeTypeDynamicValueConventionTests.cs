using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class CustomizeTypeDynamicValueConventionTests
    {
        private readonly IDomainGenerator domainGenerator;

        public CustomizeTypeDynamicValueConventionTests()
        {
            domainGenerator =
                new DomainGenerator()
                    .With<Something>(g => g.For(e => e.Value, GetNextProductId));
        }

        private int autoincrement;
        private int GetNextProductId()
        {
            return ++autoincrement;
        }

        [Fact]
        public void JustWorks()
        {
            var product1 = domainGenerator.One<Something>();
            Assert.Equal(1, product1.Value);

            var product2 = domainGenerator.One<Something>();
            Assert.Equal(2, product2.Value);

            var product3 = domainGenerator.One<Something>();
            Assert.Equal(3, product3.Value);
        }

        public class Something
        {
            public int Value { get; set; }
        }
    }
}