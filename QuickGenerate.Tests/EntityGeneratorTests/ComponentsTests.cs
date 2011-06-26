using Xunit;

namespace QuickGenerate.Tests.EntityGeneratorTests
{
    public class ComponentsTests
    {
        [Fact]
        public void GeneratorIsApplied()
        {
            Assert.Equal(42,
                         new EntityGenerator<SomethingToGenerate>()
                             .With(42)
                             .Component<SomeComponent>()
                             .One()
                             .One.Answer);

            Assert.Equal(42,
                         new EntityGenerator<SomethingToGenerate>()
                             .With(42)
                             .Component<SomeComponent>()
                             .One()
                             .Two.Answer);

            Assert.Equal(42,
                         new EntityGenerator<SomethingElseToGenerate>()
                             .With(42)
                             .Component<SomeComponent>()
                             .One()
                             .Three.Answer);
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
