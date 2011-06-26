using Xunit;

namespace QuickGenerate.Tests.EntityGeneratorTests
{
    public class PluggableFunctionsTests
    {
        [Fact]
        public void GeneratorIsApplied()
        {
            var generator =
                new EntityGenerator<SomethingToGenerate>()
                    .For(e => e.MyProperty,
                         0, val => ++val,
                         val => string.Format("SomeString{0}", val));

            Assert.Equal("SomeString1", generator.One().MyProperty);
            Assert.Equal("SomeString2", generator.One().MyProperty);
            Assert.Equal("SomeString3", generator.One().MyProperty);
            Assert.Equal("SomeString4", generator.One().MyProperty);
            Assert.Equal("SomeString5", generator.One().MyProperty);
            Assert.Equal("SomeString6", generator.One().MyProperty);
        }

        public class SomethingToGenerate
        {
            public string MyProperty { get; set; }
        }
    }
}