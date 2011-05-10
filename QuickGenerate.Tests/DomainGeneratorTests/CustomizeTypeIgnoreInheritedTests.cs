using QuickGenerate.Primitives;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class CustomizeTypeIgnoreInheritedTests
    {
        [Fact]
        public void DerivedPropertyIsIgnored()
        {
            var something =
                new DomainGenerator()
                    .With(42)
                    .With<SomethingToGenerate>(opt => opt.Ignore(e => e.PropertyToBeIgnored))
                    .One<SomethingDerivedToGenerate>();

            Assert.Equal(0, something.PropertyToBeIgnored);

        }

        public class SomethingToGenerate
        {
            public int PropertyToBeIgnored { get; set; }
        }

        public class SomethingDerivedToGenerate : SomethingToGenerate { }
    }
}
