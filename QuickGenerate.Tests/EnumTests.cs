using Xunit;

namespace QuickGenerate.Tests
{
    public class EnumTests
    {
        [Fact]
        public void GeneratorIsApplied()
        {
            var generator = new DomainGenerator();
                    
            var first = false;
            var second = false;
            var third = false;

            100.Times(
                () =>
                    {
                        var something = generator.One<SomethingToGenerate>();
                        first = first || something.Property == AnEnum.First;
                        second = second || something.Property == AnEnum.Second;
                        third = third || something.Property == AnEnum.Third;
                    });

            Assert.True(first);
            Assert.True(second);
            Assert.True(third);
        }

        public enum AnEnum
        {
            First,
            Second,
            Third,
        }

        public struct SomethingToGenerate
        {
            public AnEnum? Property { get; set; }
        }
    }
}
