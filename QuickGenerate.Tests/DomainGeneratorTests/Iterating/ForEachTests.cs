using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Iterating
{
    public class ForEachTests
    {
        [Fact]
        public void IsApplied()
        {
            var spy = new ForEachSpy();

            var domainGenerator =
                new DomainGenerator()
                    .ForEach<Something>(spy.Check);

            var thing = domainGenerator.One<Something>();
            Assert.True(spy.Checked.Contains(thing));
        }

        public class Something { }       
    }
}