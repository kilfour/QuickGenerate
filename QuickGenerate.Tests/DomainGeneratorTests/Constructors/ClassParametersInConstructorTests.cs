using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Constructors
{
    public class ClassParametersInConstructorTests
    {
        [Fact]
        public void GeneratorIsApplied()
        {
            var generated = Generator.For<ToBeGenerated>().With(42).One();
            Assert.NotNull(generated.GetFieldValue("justSome"));
            Assert.Equal(42, ((JustSome)generated.GetFieldValue("justSome")).Prop);
        }

        public class ToBeGenerated
        {
            private JustSome justSome;

            public ToBeGenerated(JustSome justSome)
            {
                this.justSome = justSome;
            }
        }

        public class JustSome
        {
            public int Prop { get; set; }
        }
    }
}
