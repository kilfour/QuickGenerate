using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Issues
{
    public class CircularOneToOne
    {
        [Fact]
        public void Throws()
        {
            var thing =
                new DomainGenerator()
                    .OneToOne<Root, One>((r, o) => r.MyOne = o)
                    .OneToOne<One, Two>((o, t) => o.MyTwo = t)
                    .OneToOne<Two, Three>((two, three) => two.MyThree = three)
                    .OneToOne<Three, One>((t, o) => t.MyOne = o)
                    .One<Root>();
            Assert.Null(thing.MyOne.MyTwo.MyThree.MyOne.MyTwo);
        }

        public class Root
        {
            public One MyOne { get; set; }
        }

        public class One
        {
            public Two MyTwo { get; set; }
        }

        public class Two
        {
            public Three MyThree { get; set; }
        }

        public class Three
        {
            public One MyOne { get; set; }
        }
    }
}