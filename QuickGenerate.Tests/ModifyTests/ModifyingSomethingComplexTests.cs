using Xunit;

namespace QuickGenerate.Tests.ModifyTests
{
    public class ModifyingSomethingComplexTests
    {
        public class SomethingToGenerate
        {
            public int MyProperty { get; set; }
        }

        public class SomethingComplexToGenerate
        {
            public SomethingToGenerate MySomethingToGenerate { get; set; }
        }

        [Fact]
        public void Tdd()
        {
            var something =
                new SomethingComplexToGenerate
                {
                    MySomethingToGenerate = Generate.One<SomethingToGenerate>()
                };

            var theGatherer =
                Gather
                    .From(something)
                    .From(c => c.MySomethingToGenerate,
                        g => g.Collect(s => s.MyProperty));

            Assert.Equal(
                something.MySomethingToGenerate.MyProperty,
                theGatherer
                    .RecallFrom(c => c.MySomethingToGenerate)
                    .Recall(s => s.MyProperty));

        }
    }
}
