using System.Linq;
using Xunit;

namespace QuickGenerate.Tests.Picking
{
    public class PickSomeTests
    {
        [Fact]
        public void Returns_the_correct_number_of_elements()
        {
            var list = new[] {1, 2, 3, 4, 5};
            var newList = list.Pick(2);
            Assert.Equal(2, newList.Count());
        }

        [Fact]
        public void Does_not_Quote_Repick_Unquote()
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
