using System;
using QuickGenerate.DomainGeneratorImplementation;
using Xunit;

namespace QuickGenerate.Tests.PrimitiveGeneratorsTests
{
    public class NullablesTests
    {
        private readonly PrimitiveGenerators primitiveGenerators = new PrimitiveGenerators();

        [Fact]
        public void NullableInts()
        {
            var generator = primitiveGenerators.Get(typeof (int?));
            Assert.NotNull(generator);
        }

        [Fact]
        public void NullableDates()
        {
            var generator = primitiveGenerators.Get(typeof(DateTime?));
            Assert.NotNull(generator);
        }

        [Fact]
        public void NullableDoubles()
        {
            var generator = primitiveGenerators.Get(typeof(double?));
            Assert.NotNull(generator);
        }

        [Fact]
        public void NullableTimespans()
        {
            var generator = primitiveGenerators.Get(typeof(TimeSpan?));
            Assert.NotNull(generator);
        }
    }
}
