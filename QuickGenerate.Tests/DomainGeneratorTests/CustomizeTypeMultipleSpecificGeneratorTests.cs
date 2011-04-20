using QuickGenerate.Primitives;
using QuickGenerate.Tests.DomainGeneratorTests.TheDomain;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class CustomizeTypeMultipleSpecificGeneratorTests
    {
        private readonly DomainGenerator domainGenerator;

        public CustomizeTypeMultipleSpecificGeneratorTests()
        {
            domainGenerator =
                new DomainGenerator()
                    .With<Product>(g => g.For(e => e.Id, new IntGenerator(42, 42), new IntGenerator(43, 43)));
        }

        [Fact]
        public void GeneratorIsApplied()
        {
            var is42 = false;
            var is43 = false;
            
            20.Times(
                () =>
                    {
                        is42 = is42 || domainGenerator.One<Product>().Id == 42;
                        is43 = is43 || domainGenerator.One<Product>().Id == 43;
                    });

            Assert.True(is42);
            Assert.True(is43);
        }
    }
}