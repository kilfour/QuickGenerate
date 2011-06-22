using QuickGenerate.Tests.DomainGeneratorTests.TheDomain;
using Xunit;

namespace QuickGenerate.Tests.EntityGeneratorTests
{
    public class IgnoreTests
    {
        private readonly EntityGenerator<SomethingToGenerate> generator;

        public IgnoreTests()
        {
            generator =
                new EntityGenerator<SomethingToGenerate>()
                    .Ignore(e => e.MyProperty);
        }

        [Fact]
        public void StaysDefaultvalue()
        {
            10.Times(
                () =>
                {
                    var something = generator.One();
                    Assert.Equal(0, something.MyProperty);
                });
        }

        public class SomethingToGenerate
        {
            public int MyProperty { get; set; }
        }
    }
}