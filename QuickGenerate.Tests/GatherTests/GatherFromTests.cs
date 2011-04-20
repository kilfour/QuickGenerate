using Xunit;

namespace QuickGenerate.Tests.GatherTests
{
    public class GatherFromTests
    {
        [Fact]
        public void Simple_Collect_Recall()
        {
            var something = Generate.One<SomethingToGenerate>();
            var theGatherer =
                Gather
                    .From(something)
                    .Collect(s => s.MyProperty);

            Assert.Equal(
                something.MyProperty, 
                theGatherer.Recall(s => s.MyProperty));;
        }

        [Fact]
        public void Composite_Collect_Recall()
        {
            var something =
                new SomethingComplexToGenerate
                    {
                        Something = Generate.One<SomethingToGenerate>()
                    };

            var theGatherer =
                Gather
                    .From(something)
                    .From(c => c.Something,
                        g => g.Collect(s => s.MyProperty));

            Assert.Equal(
                something.Something.MyProperty, 
                theGatherer
                    .RecallFrom(c => c.Something)
                    .Recall(s => s.MyProperty));
        }

        public class SomethingToGenerate
        {
            public int MyProperty { get; set; }
        }

        public class SomethingComplexToGenerate
        {
            public SomethingToGenerate Something { get; set; }
        }
    }
}
