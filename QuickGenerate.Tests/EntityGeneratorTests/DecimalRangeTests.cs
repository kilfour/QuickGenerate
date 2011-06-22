using Xunit;

namespace QuickGenerate.Tests.EntityGeneratorTests
{
    public class DecimalRangeTests
    {
        private readonly EntityGenerator<SomethingToGenerate> generator =
            new EntityGenerator<SomethingToGenerate>()
                .Range(e => e.MyProperty, 3, 10);

        [Fact]
        public void GeneratorIsApplied()
        {
            10.Times(
                () =>
                    {
                        var something = generator.One();
                        Assert.True(something.MyProperty <= 10);
                        Assert.True(something.MyProperty >= 3);
                    });
        }

        public class SomethingToGenerate
        {
            public decimal MyProperty { get; set; }
        }
    }
}