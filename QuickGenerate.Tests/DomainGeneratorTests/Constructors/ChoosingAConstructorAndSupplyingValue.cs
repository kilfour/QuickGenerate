using QuickGenerate.Primitives;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Constructors
{
    public class ChoosingAConstructorAndSupplyingValue
    {
        [Fact]
        public void Generator()
        {
            var generator =
                new DomainGenerator()
                    .With<SomethingToGenerate>(opt => opt.Construct<int>(new IntGenerator(42, 42)));
            Assert.Equal(42, generator.One<SomethingToGenerate>().CheckInt());
        }

        [Fact]
        public void PossibleValue()
        {
            var generator =
                new DomainGenerator()
                    .With<SomethingToGenerate>(opt => opt.Construct(42));
            Assert.Equal(42, generator.One<SomethingToGenerate>().CheckInt());
        }

        [Fact]
        public void PossibleValues()
        {
            var generator =
                new DomainGenerator()
                    .With<SomethingToGenerate>(opt => opt.Construct(42, 43));

            var is42 = false;
            var is43 = false;
            20.Times(
                () =>
                {
                    var something = generator.One<SomethingToGenerate>();
                    is42 = is42 || something.CheckInt() == 42;
                    is43 = is43 || something.CheckInt() == 43;
                });
            Assert.True(is42);
            Assert.True(is43);
        }

        public class SomethingToGenerate
        {
            private readonly int anInt;
            public int CheckInt()
            {
                return anInt;
            }

            public SomethingToGenerate() { }

            public SomethingToGenerate(int anInt)
            {
                this.anInt = anInt;
            }
        }
    }
}