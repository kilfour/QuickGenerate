using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class CustomizeTypePossibleValuesTests
    {
        private readonly DomainGenerator domainGenerator;

        public CustomizeTypePossibleValuesTests()
        {
            domainGenerator =
                new DomainGenerator()
                    .With<Something>(g => g.For(e => e.Value, 42, 43));
        }

        [Fact]
        public void GeneratorIsApplied()
        {
            var is42 = false;
            var is43 = false;
            20.Times(
                () =>
                    {
                        var something = domainGenerator.One<Something>();
                        is42 = is42 || something.Value == 42;
                        is43 = is43 || something.Value == 43;
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