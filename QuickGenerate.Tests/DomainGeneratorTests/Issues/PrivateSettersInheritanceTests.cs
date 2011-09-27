using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Issues
{
    public class PrivateSettersInheritanceTests
    {
        [Fact]
        public void GeneratorIsApplied()
        {
            var something =
                new DomainGenerator()
                    .With((decimal)42)
                    .One<SomethingToGenerate>();

            Assert.Equal(42, something.Property);

        }

        [Fact]
        public void GeneratorIsAppliedToDerived()
        {
            var something =
                new DomainGenerator()
                    .With((decimal)42)
                    .One<SomethingDerivedToGenerate>();

            Assert.Equal(42, something.Property);

        }

        public class SomethingToGenerate
        {
            public decimal Property { get; private set; }
        }

        public class SomethingDerivedToGenerate : SomethingToGenerate { }
    }

    public class PrivateSettersInheritanceTestsInt
    {
        [Fact]
        public void GeneratorIsApplied()
        {
            var something =
                new DomainGenerator()
                    .With(42)
                    .One<SomethingToGenerate>();

            Assert.Equal(42, something.Property);

        }

        [Fact]
        public void GeneratorIsAppliedToDerived()
        {
            var something =
                new DomainGenerator()
                    .With(42)
                    .One<SomethingDerivedToGenerate>();

            Assert.Equal(42, something.Property);

        }

        public class SomethingToGenerate
        {
            public int Property { get; private set; }
        }

        public class SomethingDerivedToGenerate : SomethingToGenerate { }
    }
}
