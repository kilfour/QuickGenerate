using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class ExplicitOneToOneTests
    {
        private readonly DomainGenerator domainGenerator;

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
        public void GeneratingRightHand()
        {
            var somethingElse = domainGenerator.One<SomethingElseToGenerate>();
            Assert.Equal(somethingElse, somethingElse.Something.SomethingElse);
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