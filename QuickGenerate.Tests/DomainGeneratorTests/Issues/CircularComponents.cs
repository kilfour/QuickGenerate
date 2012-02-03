using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Issues
{
    public class CircularComponents
    {
        [Fact]
        public void Applied()
        {
            var thing =
                new DomainGenerator()
                    .Component<One>()
                    .Component<Two>()
                    .Component<Three>()
                    .One<Root>();
            Assert.Null(thing.MyOne.MyTwo.MyThree.MyOne);
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