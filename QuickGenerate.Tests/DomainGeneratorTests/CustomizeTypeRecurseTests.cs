using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class CustomizeTypeRecurseTests
    {
        private readonly DomainGenerator domainGenerator;

        public CustomizeTypeRecurseTests()
        {
            domainGenerator = new DomainGenerator();
        }

        [Fact]
        public void NotDefined()
        {
            10.Times(() => Assert.Null(domainGenerator.One<SomethingToGenerate>().Child));
        }

        [Fact(Skip="Not supported right now")]
        public void One_Deep()
        {
            //domainGenerator
            //    .With<SomethingToGenerate>(g => g.Recurse(1, e => e.Child));

            10.Times(
                () =>
                    {
                        Assert.NotNull(domainGenerator.One<SomethingToGenerate>().Child);
                        Assert.Null(domainGenerator.One<SomethingToGenerate>().Child.Child);
                    });
        }

        [Fact(Skip="Not supported right now")]
        public void Three_Deep()
        {
            //domainGenerator
            //    .With<SomethingToGenerate>(g => g.Recurse(3, e => e.Child));

            10.Times(
                () =>
                    {
                        var something = domainGenerator.One<SomethingToGenerate>();
                        Assert.NotNull(something.Child);
                        Assert.NotNull(something.Child.Child);
                        Assert.NotNull(something.Child.Child.Child);
                        Assert.Null(something.Child.Child.Child.Child);
                    });
        }

        public class SomethingToGenerate
        {
            public SomethingToGenerate Child { get; set; }   
        }
    }
}
