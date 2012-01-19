using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Issues
{
    public class ForEachNotDoneWhenConstructorWithParams
    {
        [Fact]
        public void Reproduce()
        {
            int count = 0;
            new DomainGenerator()
                .OneToOne<Something, SomethingElse>((s, se) => s.MySomethingElse = se)
                .ForEach<Something>(s => ++count)
                .One<Something>();

            Assert.Equal(1, count);
        }

        public class Something
        {
            public SomethingElse MySomethingElse { get; set; }
        }

        public class SomethingElse
        {
            private readonly int anInt;

            public SomethingElse(int anInt)
            {
                this.anInt = anInt;
            }
        }
    }
}