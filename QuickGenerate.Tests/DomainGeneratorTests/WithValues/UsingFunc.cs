using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.WithValues
{
    public class UsingFunc
    {
        [Fact]
        public void GeneratorIsApplied()
        {
            var thing =
                new DomainGenerator()
                    .With(() => new SomethingToGenerate { MyProperty = 42 })
                    .With<SomethingToGenerate>(opt => opt.Ignore(e => e.MyProperty)) //otherwise our 42 will be overridden
                    .One<SomethingToGenerate>();

            Assert.Equal(42, thing.MyProperty);
        }

        public class SomethingToGenerate
        {
            public int MyProperty { get; set; }
        }
    }
}
