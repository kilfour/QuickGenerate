using Xunit;

namespace QuickGenerate.Tests.EntityGeneratorTests
{
    public class SupplyStartingValue
    {
        private readonly EntityGenerator<SomethingToGenerate> generator =
            new EntityGenerator<SomethingToGenerate>()
                    .StartingValue(() => new SomethingToGenerate(42));
        [Fact]
        public void UsesDefaultValue()
        {
            Assert.Equal(42, generator.One().GetInt());
        }

        public class SomethingToGenerate
        {
            private readonly int anInt;
            public int GetInt() { return anInt; }
            public SomethingToGenerate(int anInt)
            {
                this.anInt = anInt;
            }
        }
    }
}
