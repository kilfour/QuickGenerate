using QuickGenerate.Modifying;
using QuickGenerate.Primitives;
using QuickGenerate.Uniqueness;
using Xunit;

namespace QuickGenerate.Tests.Uniqueness
{
    public class IntGeneratorUniqueTests
    {
        [Fact]
        public void SetOfOneElement()
        {
            var generator = new IntGenerator(42, 42).Unique();
            generator.GetRandomValue();
            Assert.Throws<HeyITriedFiftyTimesButCouldNotGetADifferentValue>(() => generator.GetRandomValue());
        }

        [Fact]
        public void SetOfOneThreeElements()
        {
            var generator = new IntGenerator(42, 45).Unique();
            3.Times(() => generator.GetRandomValue());
            Assert.Throws<HeyITriedFiftyTimesButCouldNotGetADifferentValue>(() => generator.GetRandomValue());
        }
    }
}
