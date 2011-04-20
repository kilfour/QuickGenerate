using Xunit;

namespace QuickGenerate.Tests
{
    public class StructTests
    {
        [Fact]
        public void GeneratorIsApplied()
        {
            var something = new DomainGenerator().With(42).One<SomethingToGenerate>();
            Assert.Equal(42, something.Prop);
        }

        public struct SomethingToGenerate
        {
            public int Prop { get; set; }
        }

        [Fact]
        public void GeneratorIsAppliedToGenericStruct()
        {
            var something = new DomainGenerator().With(42).One<SomethingGenericToGenerate<int>>();
            Assert.Equal(42, something.Prop);
        }

        public struct SomethingGenericToGenerate<T>
        {
            public T Prop { get; private set; }
        }
    }
}