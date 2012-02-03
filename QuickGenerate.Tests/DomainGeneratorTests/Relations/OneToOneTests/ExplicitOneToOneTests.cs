using QuickGenerate.DomainGeneratorImplementation;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Relations.OneToOneTests
{
    public class ExplicitOneToOneTests
    {
        private readonly IDomainGenerator domainGenerator;

        public ExplicitOneToOneTests()
        {
            domainGenerator =
                new DomainGenerator()
                    .OneToOne<SomethingToGenerate, SomethingElseToGenerate>((l, r) => l.MyMethod(r));
        }

        [Fact]
        public void GeneratingLeftHand()
        {
            var something = domainGenerator.One<SomethingToGenerate>();
            Assert.Equal(something, something.SomethingElse.Something);
        }

        [Fact]
        public void GeneratingRightHandDoesNotGenerateLeftHand()
        {
            var somethingElse = domainGenerator.One<SomethingElseToGenerate>();
            Assert.Null(somethingElse.Something);
        }

        public class SomethingToGenerate
        {
            public SomethingElseToGenerate SomethingElse { get; private set; }
            public void MyMethod(SomethingElseToGenerate somethingElse)
            {
                SomethingElse = somethingElse;
                SomethingElse.Something = this;
            }
        }

        public class SomethingElseToGenerate
        {
            public SomethingToGenerate Something { get; set; }
        }
    }
}