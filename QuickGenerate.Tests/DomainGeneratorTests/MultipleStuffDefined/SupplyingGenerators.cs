using QuickGenerate.Primitives;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.MultipleStuffDefined
{
    public class SupplyingGenerators
    {
        [Fact(Skip="Functionality removed")]
        public void LastOneWins()
        {
            var generator =
                new DomainGenerator();
                    //.With(() => new IntGenerator(42, 42))
                    //.With(() => new IntGenerator(666, 666));

            var is42 = false;
            var is666 = false;
            var isSomethingElse = false;

            100.Times(
                () =>
                    {
                        var thing = generator.One<SomethingToGenerate>();
                        is42 = thing.MyProperty == 42;
                        is666 = thing.MyProperty == 666;
                        isSomethingElse = thing.MyProperty != 42 && thing.MyProperty != 666;
                    });

            Assert.False(is42);
            Assert.True(is666);
            Assert.False(isSomethingElse);
        }

        public class SomethingToGenerate
        {
            public int MyProperty { get; set; }
        }
    }
}