using Xunit;

namespace QuickGenerate.Tests.EntityGeneratorTests
{
    public class AppendCounterTests
    {
        [Fact]
        public void SimpleAppend()
        {
            var generator =
                new EntityGenerator<SomethingToGenerate>()
                    .For(e => e.MyProperty, "SomeString")
                    .AppendCounter(e => e.MyProperty);

            Assert.Equal("SomeString1", generator.One().MyProperty);
            Assert.Equal("SomeString2", generator.One().MyProperty);
            Assert.Equal("SomeString3", generator.One().MyProperty);
        }

        [Fact]
        public void CounterOnly()
        {
            var generator =
                new EntityGenerator<SomethingToGenerate>()
                    .AppendCounter(e => e.MyProperty);

            // the value starts with a random string, and :
            Assert.True(generator.One().MyProperty.EndsWith("1"));
            Assert.True(generator.One().MyProperty.EndsWith("2"));
            Assert.True(generator.One().MyProperty.EndsWith("3"));
        }

        [Fact]
        public void SupplyingFullValue()
        {
            var generator =
                new EntityGenerator<SomethingToGenerate>()
                    .AppendCounter(e => e.MyProperty, () => "SomeString");

            Assert.Equal("SomeString1", generator.One().MyProperty);
            Assert.Equal("SomeString2", generator.One().MyProperty);
            Assert.Equal("SomeString3", generator.One().MyProperty);
        }

        public class SomethingToGenerate
        {
            public string MyProperty { get; set; }
        }
    }
}