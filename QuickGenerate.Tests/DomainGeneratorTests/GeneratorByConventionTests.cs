using QuickGenerate.Primitives;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class GeneratorByConventionTests
    {
        [Fact]
        public void GeneratorIsApplied()
        {
            var domainGenerator =
                new DomainGenerator()
                    .With(mi => mi.Name == "Value", new IntGenerator(42, 42));

            Assert.Equal(42, domainGenerator.One<SomethingToGenerate>().Value);
        }

        public class SomethingToGenerate
        {
            public int Value { get; set; }
        }
    }
}
