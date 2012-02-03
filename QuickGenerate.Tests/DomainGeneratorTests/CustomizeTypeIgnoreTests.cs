using QuickGenerate.DomainGeneratorImplementation;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class CustomizeTypeIgnoreTests
    {
        private readonly IDomainGenerator domainGenerator;

        public CustomizeTypeIgnoreTests()
        {
            domainGenerator =
                new DomainGenerator()
                    .With<Something>(g => g.Ignore(e => e.Value))
                    .With<SomethingElse>(g => g.Ignore(e => e.Value));
        }

        [Fact]
        public void StaysDefaultvalue()
        {
            10.Times(
                () =>
                {
                    var product = domainGenerator.One<Something>();
                    Assert.Equal(0, product.Value);
                });

            10.Times(
                () =>
                {
                    var category = domainGenerator.One<SomethingElse>();
                    Assert.Equal(null, category.Value);
                });
        }

        public class Something
        {
            public int Value { get; set; }
        }

        public class SomethingElse
        {
            public string Value { get; set; }
        }
    }
}