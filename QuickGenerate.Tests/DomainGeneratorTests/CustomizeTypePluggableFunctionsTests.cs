using QuickGenerate.DomainGeneratorImplementation;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class CustomizeTypePluggableFunctionsTests
    {
        private readonly IDomainGenerator domainGenerator = 
                new DomainGenerator()
                    .With<SomethingToGenerate>(
                        g => g.For(
                            e => e.MyProperty,
                            0, val => ++val,
                            val => string.Format("SomeString{0}", val)));

        [Fact]
        public void Works()
        {
            Assert.Equal("SomeString1", domainGenerator.One<SomethingToGenerate>().MyProperty);
            Assert.Equal("SomeString2", domainGenerator.One<SomethingToGenerate>().MyProperty);
            Assert.Equal("SomeString3", domainGenerator.One<SomethingToGenerate>().MyProperty);
            Assert.Equal("SomeString4", domainGenerator.One<SomethingToGenerate>().MyProperty);
            Assert.Equal("SomeString5", domainGenerator.One<SomethingToGenerate>().MyProperty);
            Assert.Equal("SomeString6", domainGenerator.One<SomethingToGenerate>().MyProperty);
        }

        public class SomethingToGenerate
        {
            public string MyProperty { get; set; }
        }
    }
}