using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class CustomizeTypeIntCounterTests
    {
        [Fact]
        public void SimpleAppend()
        {
            var domainGenerator =
                new DomainGenerator()
                    .With<SomethingToGenerate>(g => g.Counter(e => e.MyProperty));

            Assert.Equal(1, domainGenerator.One<SomethingToGenerate>().MyProperty);
            Assert.Equal(2, domainGenerator.One<SomethingToGenerate>().MyProperty);
            Assert.Equal(3, domainGenerator.One<SomethingToGenerate>().MyProperty);
        }

        [Fact]
        public void SupplyingStartingValue()
        {
            var domainGenerator =
                new DomainGenerator()
                    .With<SomethingToGenerate>(g => g.Counter(e => e.MyProperty, 5));

            Assert.Equal(5, domainGenerator.One<SomethingToGenerate>().MyProperty);
            Assert.Equal(6, domainGenerator.One<SomethingToGenerate>().MyProperty);
            Assert.Equal(7, domainGenerator.One<SomethingToGenerate>().MyProperty);
        }

        [Fact]
        public void SupplyingStartingValueAndStep()
        {
            var domainGenerator =
                new DomainGenerator()
                    .With<SomethingToGenerate>(g => g.Counter(e => e.MyProperty, 5, 2));

            Assert.Equal(5, domainGenerator.One<SomethingToGenerate>().MyProperty);
            Assert.Equal(7, domainGenerator.One<SomethingToGenerate>().MyProperty);
            Assert.Equal(9, domainGenerator.One<SomethingToGenerate>().MyProperty);
        }


        public class SomethingToGenerate
        {
            public int MyProperty { get; set; }
        }
    }
}