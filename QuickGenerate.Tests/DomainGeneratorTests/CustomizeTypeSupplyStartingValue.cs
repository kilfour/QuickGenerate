using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class CustomizeTypeSupplyStartingValue
    {
        [Fact]
        public void UsesDefaultValue()
        {
            var generator =
                new DomainGenerator().With(() => new SomethingToGenerate(42));
            Assert.Equal(42, generator.One<SomethingToGenerate>().GetInt());
        }

        public class SomethingToGenerate
        {
            private readonly int anInt;
            public int GetInt() { return anInt;}
            public SomethingToGenerate(int anInt)
            {
                this.anInt = anInt;
            }
        }
    }
}
