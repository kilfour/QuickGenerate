using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class DefaultStringGeneratorIsNeverEmptyString
    {
        [Fact]
        public void See()
        {
            var generator = new DomainGenerator();

            100.Times(() => Assert.NotEqual(string.Empty, generator.One<ToGenerate>().Prop));
        }

        public class ToGenerate
        {
            public string Prop { get; set; }
        }
    }
}
