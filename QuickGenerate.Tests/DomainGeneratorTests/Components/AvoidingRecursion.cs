using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Components
{
    public class AvoidingRecursion
    {
        [Fact]
        public void Works()
        {
            var thing =
                new DomainGenerator()
                    .Component<Something>()
                    .One<Something>();

            Assert.Null(thing.MySomething.MySomething);
        }

        public class Something
        {
            public Something MySomething { get; set; }
        }
    }
}
