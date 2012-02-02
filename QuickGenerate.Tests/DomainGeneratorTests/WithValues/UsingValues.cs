using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.WithValues
{
    public class UsingValues
    {
        [Fact]
        public void GeneratorIsApplied()
        {
            var generator =
                new DomainGenerator()
                    .With(new SomethingToGenerate { MyProperty = 42 }, new SomethingToGenerate { MyProperty = 43 })
                    .With<SomethingToGenerate>(opt => opt.Ignore(e => e.MyProperty)); //otherwise our 42 will be overridden

            var is42 = false;
            var is43 = false;
            20.Times(
                () =>
                    {
                        var something = generator.One<SomethingToGenerate>();
                        is42 = is42 || something.MyProperty == 42;
                        is43 = is43 || something.MyProperty == 43;
                    });
            Assert.True(is42);
            Assert.True(is43);
        }

        public class SomethingToGenerate
        {
            public int MyProperty { get; set; }
        }
    }
}