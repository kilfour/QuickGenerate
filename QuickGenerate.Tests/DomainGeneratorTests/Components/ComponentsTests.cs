using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Components
{
    public class ComponentsTests
    {
        private readonly IDomainGenerator domainGenerator;

        public ComponentsTests()
        {
            domainGenerator =
                new DomainGenerator()
                    .Component<SomeComponent>()
                    .With(42);
        }

        [Fact]
        public void GeneratorIsApplied()
        {
            Assert.Equal(42, domainGenerator.One<SomethingToGenerate>().One.Answer);
            Assert.Equal(42, domainGenerator.One<SomethingToGenerate>().Two.Answer);
            Assert.Equal(42, domainGenerator.One<SomethingElseToGenerate>().Three.Answer);
        }

        public class SomethingToGenerate
        {
            public SomeComponent One { get; set; }
            public SomeComponent Two { get; set; }
        }

        public class SomethingElseToGenerate
        {
            public SomeComponent Three { get; set; }
        }

        public class SomeComponent
        {
            public int Answer { get; set; }
        }
    }
}
