using QuickGenerate.Primitives;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class ReplacingPrimitiveGeneratorsTests
    {
        private readonly DomainGenerator domainGenerator;

        public ReplacingPrimitiveGeneratorsTests()
        {
            domainGenerator =
                new DomainGenerator()
                    ;//.With(() => new IntGenerator(42, 42));
        }

        [Fact(Skip = "functionality removed")]
        public void GeneratorIsApplied()
        {
            Assert.Equal(42, domainGenerator.One<SomethingToGenerate>().SomeInt);
        }

        public class SomethingToGenerate
        {
            public int SomeInt { get; set; }
        }
    }
}