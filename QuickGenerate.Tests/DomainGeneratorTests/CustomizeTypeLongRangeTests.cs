using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class CustomizeTypeLongRangeTests
    {
        private readonly DomainGenerator domainGenerator =
            new DomainGenerator()
                .With<SomethingToGenerate>(g => g.Range(e => e.MyProperty, 3, 10));

        [Fact]
        public void GeneratorIsApplied()
        {
            10.Times(
                () =>
                    {
                        var something = domainGenerator.One<SomethingToGenerate>();
                        Assert.True(something.MyProperty <= 10);
                        Assert.True(something.MyProperty >= 3);
                    });
        }

        public class SomethingToGenerate
        {
            public long MyProperty { get; set; }
        }
    }
}