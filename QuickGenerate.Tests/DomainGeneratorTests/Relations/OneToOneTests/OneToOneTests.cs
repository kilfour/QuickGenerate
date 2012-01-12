using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Relations.OneToOneTests
{
    public class OneToOneTests
    {
        [Fact]
        public void UnidirectionalOneToOne()
        {
            var generator = new DomainGenerator()
                .OneToOne<Something, SomethingElse>((l, r) => l.MySomethingElse = r);

            var notZero = false;
            5.Times(() => notZero = notZero || generator.One<Something>().MySomethingElse.Value != 0);
            Assert.True(notZero);
        }

        [Fact]
        public void BidirectionalOneToOne()
        {
            var something =
                new DomainGenerator()
                    .OneToOne<Something, BidirectionalSomething>(
                        (l, r) =>
                            {
                                l.MyBidirectionalSomething = r;
                                r.MySomething = l;
                            })
                    .One<Something>();

            Assert.NotNull(something.MyBidirectionalSomething);

            Assert.Equal(something, something.MyBidirectionalSomething.MySomething);
        }

        [Fact]
        public void BidirectionalOneToOne_OtherWayRound_does_Not_Generate_LeftHand()
        {
            var bidirectionalSomething = 
                new DomainGenerator()
                    .OneToOne<Something, BidirectionalSomething>(
                        (l, r) =>
                        {
                            l.MyBidirectionalSomething = r;
                            r.MySomething = l;
                        }).One<BidirectionalSomething>();

            Assert.Null(bidirectionalSomething.MySomething);
        }

        public class Something
        {
            public SomethingElse MySomethingElse { get; set; }
            public BidirectionalSomething MyBidirectionalSomething { get; set; }
        }

        public class BidirectionalSomething
        {
            public Something MySomething { get; set; }
            public int Value { get; set; }
        }

        public class SomethingElse
        {
            public int Value { get; set; }
        }
    }
}