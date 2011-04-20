using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class CustomizeTypeSupplyStartingValue
    {
        [Fact]
        public void UsesDefaultValue()
        {
            Assert.Equal(
                42,
                new DomainGenerator()
                    .With<SomethingToGenerate>(
                        opt => opt.DefaultValue(() => new SomethingToGenerate(42)))
                    .One<SomethingToGenerate>().GetInt());
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
