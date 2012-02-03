using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class CustomizeTypeIntRangeTests
    {
        [Fact]
        public void GeneratorIsApplied()
        {
            var domainGenerator =
                new DomainGenerator()
                    .With<SomethingToGenerate>(g => g.Range(e => e.MyProperty, 3, 10));

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
            public int MyProperty { get; set; }
        }
    }
}