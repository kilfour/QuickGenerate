using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class CustomizeTypeAppendCounterTests
    {
        [Fact]
        public void SimpleAppend()
        {
            var domainGenerator =
                new DomainGenerator()
                    .With<SomethingToGenerate>(g => g.For(e => e.MyProperty, "SomeString"))
                    .With<SomethingToGenerate>(g => g.AppendCounter(e => e.MyProperty));

            Assert.Equal("SomeString1", domainGenerator.One<SomethingToGenerate>().MyProperty);
            Assert.Equal("SomeString2", domainGenerator.One<SomethingToGenerate>().MyProperty);
            Assert.Equal("SomeString3", domainGenerator.One<SomethingToGenerate>().MyProperty);
        }

        [Fact]
        public void CounterOnly()
        {
            var domainGenerator =
                new DomainGenerator()
                    .With<SomethingToGenerate>(g => g.AppendCounter(e => e.MyProperty));

            // the value starts with a random string, and :
            Assert.True(domainGenerator.One<SomethingToGenerate>().MyProperty.EndsWith("1"));
            Assert.True(domainGenerator.One<SomethingToGenerate>().MyProperty.EndsWith("2"));
            Assert.True(domainGenerator.One<SomethingToGenerate>().MyProperty.EndsWith("3"));
        }

        [Fact]
        public void SupplyingFullValue()
        {
            var domainGenerator =
                new DomainGenerator()
                    .With<SomethingToGenerate>(g => g.AppendCounter(e => e.MyProperty, () => "SomeString"));

            Assert.Equal("SomeString1", domainGenerator.One<SomethingToGenerate>().MyProperty);
            Assert.Equal("SomeString2", domainGenerator.One<SomethingToGenerate>().MyProperty);
            Assert.Equal("SomeString3", domainGenerator.One<SomethingToGenerate>().MyProperty);
        }

        public class SomethingToGenerate
        {
            public string MyProperty { get; set; }
        }
    }
}