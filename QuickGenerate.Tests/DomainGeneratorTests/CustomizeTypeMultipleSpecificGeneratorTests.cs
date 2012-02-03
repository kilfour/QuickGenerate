using QuickGenerate.DomainGeneratorImplementation;
using QuickGenerate.Primitives;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class CustomizeTypeMultipleSpecificGeneratorTests
    {
        private readonly IDomainGenerator domainGenerator;

        public CustomizeTypeMultipleSpecificGeneratorTests()
        {
            domainGenerator =
                new DomainGenerator()
                    .With<Something>(g => g.For(e => e.Value, new IntGenerator(42, 42), new IntGenerator(43, 43)));
        }

        [Fact]
        public void GeneratorIsApplied()
        {
            var is42 = false;
            var is43 = false;
            
            20.Times(
                () =>
                    {
                        is42 = is42 || domainGenerator.One<Something>().Value == 42;
                        is43 = is43 || domainGenerator.One<Something>().Value == 43;
                    });

            Assert.True(is42);
            Assert.True(is43);
        }

        public class Something
        {
            public int Value { get; set; }
        }
    }
}