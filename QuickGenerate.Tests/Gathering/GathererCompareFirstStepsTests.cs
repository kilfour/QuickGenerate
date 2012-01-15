using System;
using System.Linq;
using Xunit;

namespace QuickGenerate.Tests.Gathering
{
    public class GathererCompareFirstStepsTests
    {
        [Fact]
        public void Matches()
        {
            var something = new DomainGenerator().Component<SomethingElse>().One<SomethingToGenerate>();
            var theGatherer =
                Gather
                    .From(something)
                    .CollectAll();

            var theOtherGatherer =
                Gather
                    .From(something)
                    .CollectAll();

            var result = theGatherer.Matches(theOtherGatherer);

            Assert.True(result.IsMatch, result.GetMessage());
            Assert.Equal(0, result.Messages.Count());
        }

        [Fact]
        public void MisMatchOne()
        {
            var something = new DomainGenerator().Component<SomethingElse>().One<SomethingToGenerate>();
            var theGatherer =
                Gather
                    .From(something)
                    .CollectAll();
            
            something.MyString = "blah";

            var theOtherGatherer =
                Gather
                    .From(something)
                    .CollectAll();

            var result = theGatherer.Matches(theOtherGatherer);

            Assert.False(result.IsMatch, result.GetMessage());
            Assert.Equal(1, result.Messages.Count());
        }

        public class SomethingToGenerate
        {
            public int MyInt { get; set; }
            public string MyString { get; set; }
            public bool MyBool { get; set; }
            public long MyLong { get; set; }
            public DateTime MyDatetime { get; set; }
            public decimal MyDecimal { get; set; }
            public SomethingElse MySomethingElse { get; set; }
        }

        public class SomethingElse { }
    }
}