using QuickGenerate.Tests.DomainGeneratorTests.TheDomain;
using Xunit;

namespace QuickGenerate.Tests.EntityGeneratorTests
{
    public class PossibleValuesTests
    {
        [Fact]
        public void GeneratorIsApplied()
        {
            var generator =
                new EntityGenerator<Product>()
                    .For(e => e.Id, 42, 43);

            var is42 = false;
            var is43 = false;
            20.Times(
                () =>
                {
                    var product = generator.One();
                    is42 = is42 || product.Id == 42;
                    is43 = is43 || product.Id == 43;
                });
            Assert.True(is42);
            Assert.True(is43);
        }
    }
}