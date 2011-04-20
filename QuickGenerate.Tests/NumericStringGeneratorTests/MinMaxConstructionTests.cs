using QuickGenerate.Complex;
using Xunit;

namespace QuickGenerate.Tests.NumericStringGeneratorTests
{
    public class MinMaxConstructionTests
    {
        [Fact]
        public void Empty()
        {
            var generator = new NumericStringGenerator(0);
            5.Times(() => Assert.Equal("", generator.GetRandomValue()));
        }

        [Fact]
        public void Empty_Min_Max()
        {
            var generator = new NumericStringGenerator(0, 0);
            5.Times(() => Assert.Equal("", generator.GetRandomValue()));
        }

        [Fact]
        public void OneLong()
        {
            var generator = new NumericStringGenerator(1, 1);
            5.Times(() => Assert.Equal(1, generator.GetRandomValue().Length));
        }

        [Fact]
        public void OneOrZeroLong()
        {
            var generator = new NumericStringGenerator(0, 2); // TODO: fix this
            var zero = false;
            var one = false;
            var two = false;
            20.Times(
                () =>
                    {
                        var s = generator.GetRandomValue();
                        zero = zero || s.Length == 0;
                        one = one || s.Length == 1;
                        two = two || s.Length == 2;
                    });
            Assert.True(zero);
            Assert.True(one);
            Assert.False(two); // TODO: fix this
        }
    }
}
