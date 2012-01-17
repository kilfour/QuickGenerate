using Xunit;

namespace QuickGenerate.Tests.Inspecting
{
    public class InspectorReportTests
    {
        public class Something { public int MyProp { get; set; } }

        private bool result;

        [Fact]
        public void ReportResult()
        {
            var thingOne = new Something { MyProp = 42 };
            var thingTwo = new Something { MyProp = 42 };
            var inspector = 
                Inspect
                    .This(thingOne, thingTwo)
                    .Report((b, m) => result = b);
            Assert.True(inspector.AreMemberWiseEqual());
            Assert.True(result);
            thingTwo.MyProp = 43;
            Assert.False(inspector.AreMemberWiseEqual());
            Assert.False(result);
        }
    }
}