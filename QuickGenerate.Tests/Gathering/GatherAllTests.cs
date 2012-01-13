using System;
using Xunit;

namespace QuickGenerate.Tests.Gathering
{
    public class GatherAllTests
    {
        [Fact]
        public void Collect_Recall()
        {
            var something = Generate.One<SomethingToGenerate>();
            var theGatherer =
                Gather
                    .From(something)
                    .CollectAll();

            Assert.Equal(something.MyInt, theGatherer.Recall(s => s.MyInt));
            Assert.Equal(something.MyString, theGatherer.Recall(s => s.MyString));
            Assert.Equal(something.MyBool, theGatherer.Recall(s => s.MyBool));
            Assert.Equal(something.MyLong, theGatherer.Recall(s => s.MyLong));
            Assert.Equal(something.MyDatetime, theGatherer.Recall(s => s.MyDatetime));
            Assert.Equal(something.MyDecimal, theGatherer.Recall(s => s.MyDecimal));
            Assert.Equal(something.MyInt, theGatherer.Recall(s => s.MyInt));
            Assert.Equal(something.MySomethingElse, theGatherer.Recall(s => s.MySomethingElse));
        }

        public class SomethingToGenerate
        {
            public int MyInt { get; set; }
            public string MyString { get; set; }
            public bool MyBool { get; set; }
            public long MyLong { get; set; }
            public DateTime MyDatetime{ get; set; }
            public decimal MyDecimal { get; set; }
            public SomethingElse MySomethingElse { get; set; }
        }

        public class SomethingElse { }
    }
}