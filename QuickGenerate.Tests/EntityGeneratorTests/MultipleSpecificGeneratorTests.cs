using QuickGenerate.Primitives;
using Xunit;

namespace QuickGenerate.Tests.EntityGeneratorTests
{
    public class MultipleSpecificGeneratorTests
    {
        [Fact]
        public void GeneratorIsApplied()
        {
            var generator =
                new EntityGenerator<Something>()
                    .For(e => e.Value, new IntGenerator(42, 42), new IntGenerator(43, 43));

            var is42 = false;
            var is43 = false;
            
            20.Times(
                () =>
                    {
                        is42 = is42 || generator.One().Value == 42;
                        is43 = is43 || generator.One().Value == 43;
                    });

            Assert.True(is42);
            Assert.True(is43);
        }

        public class Something
        {
            public int Value { get; set; }
        }
    }
}