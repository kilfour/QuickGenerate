using QuickGenerate.DomainGeneratorImplementation;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class ReplacingPrimitiveGeneratorsWithConstantValueTests
    {
        private readonly IDomainGenerator domainGenerator;

        public ReplacingPrimitiveGeneratorsWithConstantValueTests()
        {
            domainGenerator =
                new DomainGenerator()
                    .With(42);
        }

        [Fact]
        public void ConstantIsApplied()
        {
            Assert.Equal(42, domainGenerator.One<SomethingToGenerate>().SomeInt);
        }

        public class SomethingToGenerate
        {
            public int SomeInt { get; set; }
        }
    }
}