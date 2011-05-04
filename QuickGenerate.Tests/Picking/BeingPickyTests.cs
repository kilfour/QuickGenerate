using System.Linq;
using Xunit;

namespace QuickGenerate.Tests.Picking
{
    public class BeingPickyTests
    {
        [Fact]
        public void You_May_Choose_A_String()
        {
            Assert.NotNull(new[] { "result", "test", "iets" }.PickOne());
            Assert.NotEqual("", new[] { "result", "test", "iets" }.PickOne());
            Assert.Equal("result", new[] { "result" }.PickOne());
        }

        [Fact]
        public void You_May_Pick_More_Than_One()
        {
            var ints = new[] { 1, 2, 3, 5 };
            10.Times(
                () =>
                {
                    var twoInts = ints.Pick(2);
                    Assert.Equal(2, twoInts.Count());
                    Assert.NotEqual(twoInts.First(), twoInts.Last());
                });
        }

        [Fact]
        public void You_May_Pick_More_Than_One_Carefully()
        {
            10.Times(
                () =>
                {
                    var ints = new[] { 1, 2, 3, 4 };
                    Assert.Throws<BeingPicky.TheNumberOfElementsToPickMustBeSmallerThanTheNumberOfPossibleChoices>(
                        () => ints.Pick(5));
                });
        }

        [Fact]
        public void Pick_Some_Returns_the_correct_number_of_elements()
        {
            var list = new[] {1, 2, 3, 4, 5};
            var newList = list.Pick(2);
            Assert.Equal(2, newList.Count());
        }

        [Fact]
        public void Pick_Some_Does_not_Quote_Repick_Unquote()
        {
            var list = new[] { 1, 2, 3, 4, 5 };
            10.Times(
                () =>
                    {
                        var newList = list.Pick(2).ToList();
                        Assert.NotEqual(newList.First(), newList.Last());
                    });
        }
    }
}
