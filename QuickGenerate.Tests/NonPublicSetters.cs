using Xunit;

namespace QuickGenerate.Tests
{
    public class NonPublicSettersTest
    {
        [Fact]
        public void GeneratorIsApplied()
        {
            var something = new DomainGenerator().With(42).One<SomethingToGenerate>();
            Assert.Equal(42, something.Prop);
        }

        public class SomethingToGenerate
        {
            public int Prop { get; private set; }
        }
    }
}
