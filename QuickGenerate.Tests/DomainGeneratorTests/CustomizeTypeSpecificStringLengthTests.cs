using QuickGenerate.Tests.DomainGeneratorTests.TheDomain;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class CustomizeTypeSpecificStringLengthTests
    {
        private readonly DomainGenerator domainGenerator;

        public CustomizeTypeSpecificStringLengthTests()
        {
            domainGenerator =
                new DomainGenerator()
                    .With<SomethingToGenerate>(g => g.Length(e => e.MyProperty, 5))
                    .With<SomethingToGenerate>(g => g.Length(e => e.MyOtherProperty, 3, 10));
        }

        [Fact]
        public void GeneratorIsApplied()
        {
            10.Times(
                () =>
                    {
                        var something = domainGenerator.One<SomethingToGenerate>();
                        Assert.Equal(5, something.MyProperty.Length);
                        Assert.True(something.MyOtherProperty.Length < 10);
                        Assert.True(something.MyOtherProperty.Length >= 3);
                    });
        }

        public class SomethingToGenerate
        {
            public string MyProperty { get; set; }
            public string MyOtherProperty { get; set; }
        }
    }
}