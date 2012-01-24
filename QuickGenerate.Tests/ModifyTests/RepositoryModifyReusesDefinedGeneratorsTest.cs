using Xunit;

namespace QuickGenerate.Tests.ModifyTests
{
    public class RepositoryModifyReusesDefinedGeneratorsTest
    {
        [Fact]
        public void Demonstrating()
        {
            var repository = new DomainGenerator().With(0, 42).With<SomethingtoGenerate>();
            var something = new SomethingtoGenerate {MyProperty = 0};
            repository.ModifyThis(something).Change(s => s.MyProperty);
            5.Times(() => Assert.Equal(42, something.MyProperty));
        }

        [Fact(Skip = "Lost in translation")]
        public void Reusing_InnerGenerators()
        {
            var repository = 
                new DomainGenerator()
                    .With(666)
                    .With<SomethingtoGenerate>(g => g.For(s => s.MyProperty, 42));

            var something = new SomethingtoGenerate { MyProperty = 0 };
            repository.ModifyThis(something).Change(s => s.MyProperty);
            Assert.Equal(42, something.MyProperty);
        }

        [Fact]
        public void ReusingConventions()
        {
            var repository =
                new DomainGenerator()
                    .With(mi => true, () => new[] {0, 42}.PickOne())
                    .With<SomethingtoGenerate>();

            var something = new SomethingtoGenerate { MyProperty = 0 };
            repository.ModifyThis(something).Change(s => s.MyProperty);
            Assert.Equal(42, something.MyProperty);
        }

        public class SomethingtoGenerate
        {
            public int MyProperty { get; set; }
        }
    }
}
