using Xunit;

namespace QuickGenerate.Tests.EntityGeneratorTests
{
    public class IgnoreInheritedTests
    {
        [Fact]
        public void DerivedPropertyIsIgnored()
        {
            var something =
                new EntityGenerator<SomethingDerivedToGenerate>()
                    .With(42)
                    .Ignore(e => e.PropertyToBeIgnored)
                    .One();

            Assert.Equal(0, something.PropertyToBeIgnored);

        }

        public class SomethingToGenerate
        {
            public int PropertyToBeIgnored { get; set; }
        }

        public class SomethingDerivedToGenerate : SomethingToGenerate { }
    }
}
