using QuickGenerate.DomainGeneratorImplementation;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Constructors
{
    public class ChoosingAConstructor
    {

        [Fact]
        public void TheDefaultConstructor()
        {
            var thing =
                new DomainGenerator()
                    .With<SomethingToGenerate>(opt => opt.Construct())
                    .One<SomethingToGenerate>();

            Assert.NotNull(thing);
            Assert.Equal(0, thing.CheckInt());
            Assert.Null(thing.CheckString());
        }

        [Fact]
        public void TheString()
        {
            var thing = 
                new DomainGenerator()
                    .With<SomethingToGenerate>(opt => opt.Construct<string>())
                    .One<SomethingToGenerate>();
            
            Assert.NotNull(thing);
            Assert.Equal(0, thing.CheckInt());
            Assert.NotNull(thing.CheckString());
        }

        [Fact]
        public void TheInt()
        {
            var thing =
                new DomainGenerator()
                    .With<SomethingToGenerate>(opt => opt.Construct<int>())
                    .One<SomethingToGenerate>();

            Assert.NotNull(thing);
            Assert.NotEqual(0, thing.CheckInt());
            Assert.Null(thing.CheckString());
        }

        [Fact]
        public void Both()
        {
            var thing =
                new DomainGenerator()
                    .With<SomethingToGenerate>(opt => opt.Construct<int>().Construct<string>())
                    .One<SomethingToGenerate>();

            Assert.NotNull(thing);
            Assert.NotEqual(0, thing.CheckInt());
            Assert.NotNull(thing.CheckString());
        }

        [Fact]
        public void NoMatchThrows()
        {
            Assert.Throws<CantFindConstructorException>(
                () =>
                new DomainGenerator()
                    .With<SomethingToGenerate>(opt => opt.Construct<string>().Construct<int>())
                    .One<SomethingToGenerate>());
        }

        public class SomethingToGenerate
        {
            private readonly int anInt;
            public int CheckInt()
            {
                return anInt;
            }

            private readonly string aString;
            public string CheckString()
            {
                return aString;
            }

            public SomethingToGenerate() { }

            public SomethingToGenerate(int anInt)
            {
                this.anInt = anInt;
            }

            public SomethingToGenerate(string aString)
            {
                this.aString = aString;
            }

            public SomethingToGenerate(int anInt, string aString)
            {
                this.anInt = anInt;
                this.aString = aString;
            }
        }
    }
}