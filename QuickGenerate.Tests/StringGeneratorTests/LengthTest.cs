using QuickGenerate.Primitives;
using Xunit;

namespace QuickGenerate.Tests.StringGeneratorTests
{
    public class LengthTest
    {
        [Fact]
        public void Zero()
        {
            var generator = new StringGenerator(0, 0);
            10.Times(() => Assert.Equal(0, generator.GetRandomValue().Length));
        }

        [Fact]
        public void One()
        {
            var generator = new StringGenerator(1, 1);
            10.Times(() => Assert.Equal(1, generator.GetRandomValue().Length));
        }

        [Fact]
        public void ZeroOrOne()
        {
            var generator = new StringGenerator(0, 1);
            var isSometimesZero = false;
            var isSometimesOne = false;
            50.Times(
                () =>
                    {
                        var length = generator.GetRandomValue().Length;
                        if (length == 0)
                            isSometimesZero = true;
                        else if(length == 1)
                            isSometimesOne = true;
                        else
                            Assert.True(false, string.Format("length is {0}.", length));
                    });
            Assert.True(isSometimesZero);

            // is this what we want ?
            Assert.False(isSometimesOne);
        }
    }
}
