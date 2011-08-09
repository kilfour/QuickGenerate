using System.Linq;
using Xunit;

namespace QuickGenerate.Tests.EntityGeneratorTests
{
    public class ManyTests
    {
        private readonly EntityGenerator<SomethingToGenerate> generator;

        public ManyTests()
        {
            generator = new EntityGenerator<SomethingToGenerate>();
        }

        [Fact]
        public void GetsCorrectAmount()
        {
            var many = generator.Many(3);
            Assert.Equal(3, many.Count());
        }

        [Fact]
        public void GetsCorrectAmountWithinRange()
        {
            var many = generator.Many(3, 3);
            Assert.Equal(3, many.Count());
        }

        public class SomethingToGenerate { }
    }
}
