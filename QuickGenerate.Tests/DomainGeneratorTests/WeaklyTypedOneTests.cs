using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class WeaklyTypedOneTests
    {
        private readonly DomainGenerator domainGenerator;

        public WeaklyTypedOneTests()
        {
            domainGenerator = new DomainGenerator().With(42);
        }

        [Fact]
        public void GetsCorrectAmount()
        {
            var one = domainGenerator.One(typeof(SomethingToGenerate));
            Assert.Equal(typeof(SomethingToGenerate), one.GetType());
            var something = (SomethingToGenerate)one;
            Assert.Equal(42, something.Property);
        }

        public class SomethingToGenerate
        {
            public int Property { get; set; }
        }
    }
}