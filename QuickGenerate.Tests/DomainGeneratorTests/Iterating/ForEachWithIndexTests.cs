using System.Linq;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Iterating
{
    public class ForEachWithIndexTests
    {
        [Fact]
        public void IsApplied()
        {
            var spy = new ForEachSpy();

            var domainGenerator =
                new DomainGenerator()
                    .ForEach<Something>((i, e) => { if (i < 2) spy.Check(e); });

            var things = domainGenerator.Many<Something>(5).ToArray();
            Assert.Equal(2, spy.Checked.Count);
            Assert.True(spy.Checked.Contains(things[0]));
            Assert.True(spy.Checked.Contains(things[1]));
        }

        public class Something { }
    }
}