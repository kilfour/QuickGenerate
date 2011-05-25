using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class CustomizeTypeSupplyStartingValue
    {
        private DomainGenerator generator =
            new DomainGenerator()
                    .With<SomethingToGenerate>(
                        opt => opt.StartingValue(() => new SomethingToGenerate(42)));
        [Fact]
        public void UsesDefaultValue()
        {
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
