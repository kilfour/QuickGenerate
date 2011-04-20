using System.Linq;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class ManyTests
    {
        private readonly DomainGenerator domainGenerator;

        public ManyTests()
        {
            domainGenerator = new DomainGenerator();
        }

        [Fact]
        public void GetsCorrectAmount()
        {
            var many = domainGenerator.Many<SomethingToGenerate>(3);
            Assert.Equal(3, many.Count());
        }

        public class SomethingToGenerate { }
    }
}
