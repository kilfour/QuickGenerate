using Xunit;

namespace QuickGenerate.Tests.EntityGeneratorTests
{
    public class PossibleValuesTests
    {
        [Fact]
        public void GeneratorIsApplied()
        {
            var generator =
                new EntityGenerator<Something>()
                    .For(e => e.Value, 42, 43);

            var is42 = false;
            var is43 = false;
            20.Times(
                () =>
                {
                    var something = generator.One();
                    is42 = is42 || something.Value == 42;
                    is43 = is43 || something.Value == 43;
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