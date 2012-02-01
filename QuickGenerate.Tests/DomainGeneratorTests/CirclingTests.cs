using System.Linq;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class CirclingTests
    {
        [Fact]
        public void GeneratorIsApplied()
        {
            var things =
                new DomainGenerator()
                    .With<SomethingToGenerate>(opt => opt.Circle(e => e.MyProperty, 1, 2, 3))
                    .Many<SomethingToGenerate>(4)
                    .ToArray();

            Assert.Equal(1, things[0].MyProperty);
            Assert.Equal(2, things[1].MyProperty);
            Assert.Equal(3, things[2].MyProperty);
            Assert.Equal(1, things[3].MyProperty);
        }

        public class SomethingToGenerate
        {
            public int MyProperty { get; set; }
        }
    }
}
