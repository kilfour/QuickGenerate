using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Constructors
{
    public class ActivationTests
    {
        private readonly DomainGenerator domainGenerator;

        public ActivationTests()
        {
            domainGenerator =
                new DomainGenerator()
                    .With(42);
        }

        [Fact]
        public void CanHandleProtectedConstructors()
        {
            Assert.NotNull(domainGenerator.One<SomethingProtectedToGenerate>());
        }

        public class SomethingProtectedToGenerate
        {
            protected SomethingProtectedToGenerate() { }
        }

        [Fact]
        public void CanHandleArguments()
        {
            Assert.Equal(42, domainGenerator.One<SomethingArgumentativeToGenerate>().CheckIt());
        }

        public class SomethingArgumentativeToGenerate
        {
            private readonly int anArgument;
            public int CheckIt()
            {
                return anArgument;
            }
            public SomethingArgumentativeToGenerate(int anArgument)
            {
                this.anArgument = anArgument;
            }
        }

        [Fact]
        public void PrefersMostAccesibleConstructor()
        {
            Assert.Equal(42, domainGenerator.One<SomethingElseToGenerate>().CheckIt());
        }

        public class SomethingElseToGenerate
        {
            private readonly int anArgument;
            public int CheckIt()
            {
                return anArgument;
            }

            protected SomethingElseToGenerate() { }

            public SomethingElseToGenerate(int anArgument)
            {
                this.anArgument = anArgument;
            }
        }

        [Fact]
        public void PrefersTheOneWithTheMostParameters()
        {
            Assert.Equal(42, domainGenerator.One<SomethingEvenElserToGenerate>().CheckIt());
        }

        public class SomethingEvenElserToGenerate
        {
            private readonly int anArgument;
            public int CheckIt()
            {
                return anArgument;
            }

            public SomethingEvenElserToGenerate() { }

            public SomethingEvenElserToGenerate(int anArgument)
            {
                this.anArgument = anArgument;
            }
        }
    }
}
