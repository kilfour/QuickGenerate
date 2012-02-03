using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class DynamicValueConventionTests
    {
        private readonly IDomainGenerator domainGenerator;

        public DynamicValueConventionTests()
        {
            domainGenerator =
                new DomainGenerator()
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
            var something1 = domainGenerator.One<SomethingToGenerate>();
            Assert.Equal(1, something1.MyProperty);

            var something2 = domainGenerator.One<SomethingToGenerate>();
            Assert.Equal(2, something2.MyProperty);

            var something3 = domainGenerator.One<SomethingToGenerate>();
            Assert.Equal(3, something3.MyProperty);
        }

        public class SomethingToGenerate
        {
            public int MyProperty { get; set; }
        }
    }
}