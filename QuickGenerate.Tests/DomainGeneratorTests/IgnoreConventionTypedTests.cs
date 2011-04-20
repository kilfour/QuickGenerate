using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class IgnoreConventionTypedTests
    {
        private readonly DomainGenerator domainGenerator;

        public IgnoreConventionTypedTests()
        {
            domainGenerator =
                new DomainGenerator()
                    .Ignore<SomethingToGenerate, int>(something => something.Id)
                    .Ignore<SomethingElseToGenerate, string>(somethingElse => somethingElse.Id);
        }

        [Fact]
        public void StaysDefaultvalue()
        {
            10.Times(
                () =>
                    {
                        var something = domainGenerator.One<SomethingToGenerate>();
                        Assert.Equal(0, something.Id);
                    });

            10.Times(
                () =>
                    {
                        var something = domainGenerator.One<SomethingElseToGenerate>();
                        Assert.Null(something.Id);
                    });
        }

        public class SomethingToGenerate
        {
            public int Id { get; set; }
        }

        public class SomethingElseToGenerate
        {
            public string Id { get; set; }
        }
    }
}