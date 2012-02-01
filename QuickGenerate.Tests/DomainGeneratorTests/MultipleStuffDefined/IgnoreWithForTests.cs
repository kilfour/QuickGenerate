using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.MultipleStuffDefined
{
    public class IgnoreWithForTests
    {
        [Fact]
        public void IgnoreWins()
        {
            var generator =
                new DomainGenerator()
                    .With<SomethingToGenerate>(opt => opt.Ignore(e => e.MyProperty))
                    .With<SomethingToGenerate>(opt => opt.For(e => e.MyProperty, 666));

            var isIgnored = false;
            var is666 = false;
            var isSomethingElse = false;

            100.Times(
                () =>
                    {
                        var thing = generator.One<SomethingToGenerate>();
                        isIgnored = thing.MyProperty == 0;
                        is666 = thing.MyProperty == 666;
                        isSomethingElse = thing.MyProperty != 0 && thing.MyProperty != 666;
                    });

            Assert.True(isIgnored);
            Assert.False(is666);
            Assert.False(isSomethingElse);
        }

        [Fact]
        public void IgnoreAlwaysWins()
        {
            var generator =
                new DomainGenerator()
                    .With<SomethingToGenerate>(opt => opt.For(e => e.MyProperty, 666))
                    .With<SomethingToGenerate>(opt => opt.Ignore(e => e.MyProperty));

            var isIgnored = false;
            var is666 = false;
            var isSomethingElse = false;

            100.Times(
                () =>
                {
                    var thing = generator.One<SomethingToGenerate>();
                    isIgnored = thing.MyProperty == 0;
                    is666 = thing.MyProperty == 666;
                    isSomethingElse = thing.MyProperty != 0 && thing.MyProperty != 666;
                });

            Assert.True(isIgnored);
            Assert.False(is666);
            Assert.False(isSomethingElse);
        }

        public class SomethingToGenerate
        {
            public int MyProperty { get; set; }
        }
    }
}