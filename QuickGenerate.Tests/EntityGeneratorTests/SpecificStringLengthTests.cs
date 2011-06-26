using Xunit;

namespace QuickGenerate.Tests.EntityGeneratorTests
{
    public class SpecificStringLengthTests
    {
        private readonly EntityGenerator<SomethingToGenerate> domainGenerator =
                new EntityGenerator<SomethingToGenerate>()
                    .Length(e => e.MyProperty, 5)
                    .Length(e => e.MyOtherProperty, 3, 10);
       

        [Fact]
        public void GeneratorIsApplied()
        {
            10.Times(
                () =>
                {
                    var something = domainGenerator.One();
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