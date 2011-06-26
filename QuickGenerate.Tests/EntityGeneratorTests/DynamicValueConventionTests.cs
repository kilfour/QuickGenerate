using Xunit;

namespace QuickGenerate.Tests.EntityGeneratorTests
{
    public class DynamicValueConventionTests
    {
        private readonly EntityGenerator<SomethingToGenerate> domainGenerator;

        public DynamicValueConventionTests()
        {
            domainGenerator =
                new EntityGenerator<SomethingToGenerate>()
                    .With(mi => mi.Name == "MyProperty", () => GetNextProductId());
        }

        private int autoincrement;
        private int GetNextProductId()
        {
            return ++autoincrement;
        }

        [Fact]
        public void GeneratorIsApplied()
        {
            var something1 = domainGenerator.One();
            Assert.Equal(1, something1.MyProperty);

            var something2 = domainGenerator.One();
            Assert.Equal(2, something2.MyProperty);

            var something3 = domainGenerator.One();
            Assert.Equal(3, something3.MyProperty);
        }

        public class SomethingToGenerate
        {
            public int MyProperty { get; set; }
        }
    }
}