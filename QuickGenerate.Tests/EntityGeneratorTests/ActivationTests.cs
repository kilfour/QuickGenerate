using Xunit;

namespace QuickGenerate.Tests.EntityGeneratorTests
{
    public class ActivationTests
    {
        [Fact]
        public void CanHandleProtectedConstructors()
        {
            Assert.NotNull(new EntityGenerator<SomethingProtectedToGenerate>().With(42).One());
        }

        public class SomethingProtectedToGenerate
        {
            protected SomethingProtectedToGenerate() { }
        }

        [Fact]
        public void CanHandleArguments()
        {
            Assert.Equal(42, new EntityGenerator<SomethingArgumentativeToGenerate>().With(42).One().CheckIt());
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
            Assert.Equal(42, new EntityGenerator<SomethingElseToGenerate>().With(42).One().CheckIt());
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
            Assert.Equal(42, new EntityGenerator<SomethingEvenElserToGenerate>().With(42).One().CheckIt());
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
