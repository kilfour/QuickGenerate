using QuickGenerate.Primitives;
using QuickGenerate.Tests.DomainGeneratorTests.TheDomain;
using Xunit;

namespace QuickGenerate.Tests.EntityGeneratorTests
{
    public class MultipleSpecificGeneratorTests
    {
        [Fact]
        public void GeneratorIsApplied()
        {
            var generator =
                new EntityGenerator<Product>()
                    .For(e => e.Id, new IntGenerator(42, 42), new IntGenerator(43, 43));

            var is42 = false;
            var is43 = false;
            
            20.Times(
                () =>
                    {
                        is42 = is42 || generator.One().Id == 42;
                        is43 = is43 || generator.One().Id == 43;
                    });

            Assert.True(is42);
            Assert.True(is43);
        }
    }
}