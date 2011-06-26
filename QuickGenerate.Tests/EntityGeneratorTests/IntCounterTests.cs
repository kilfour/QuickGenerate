using Xunit;

namespace QuickGenerate.Tests.EntityGeneratorTests
{
    public class IntCounterTests
    {
        [Fact]
        public void SimpleAppend()
        {
            var generator =
                new EntityGenerator<SomethingToGenerate>()
                    .Counter(e => e.MyProperty);

            Assert.Equal(1, generator.One().MyProperty);
            Assert.Equal(2, generator.One().MyProperty);
            Assert.Equal(3, generator.One().MyProperty);
        }

        [Fact]
        public void SupplyingStartingValue()
        {
            var generator =
                new EntityGenerator<SomethingToGenerate>()
                    .Counter(e => e.MyProperty, 5);

            Assert.Equal(5, generator.One().MyProperty);
            Assert.Equal(6, generator.One().MyProperty);
            Assert.Equal(7, generator.One().MyProperty);
        }

        [Fact]
        public void SupplyingStartingValueAndStep()
        {
            var generator =
                new EntityGenerator<SomethingToGenerate>()
                    .Counter(e => e.MyProperty, 5, 2);

            Assert.Equal(5, generator.One().MyProperty);
            Assert.Equal(7, generator.One().MyProperty);
            Assert.Equal(9, generator.One().MyProperty);
        }


        public class SomethingToGenerate
        {
            public int MyProperty { get; set; }
        }
    }
}