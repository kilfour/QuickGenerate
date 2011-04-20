using System;
using System.Linq;
using Xunit;

namespace QuickGenerate.Tests.Choices
{
    
    public class EnumerableExtensionTests
    {
        [Fact] 
        public void You_May_Choose_A_String()
        {
            Assert.NotNull(new[] {"result", "test", "iets"}.PickOne());
            Assert.NotEqual("", new[] { "result", "test", "iets" }.PickOne());
            Assert.Equal("result", new[] { "result" }.PickOne());
        }

        [Fact]
        public void You_May_Pick_More_Than_One()
        {
            var ints = new[] {1, 2, 3, 5};
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
                    var ints = new DomainGenerator().Many<int>(0, 5); // 5 == 4, tschh
                    Assert.Throws<BeingPicky.TheNumberOfElementsToPickMustBeSmallerThanTheNumberOfPossibleChoices>(
                        () => ints.Pick(5));
                });
        }

        [Fact]
        public void You_May_Choose_An_Int_Range()
        {
            Assert.NotNull(new[] { 0, 10 }.FromRange());
            Assert.NotEqual(0, new[] { 1, 10 }.FromRange());
            Assert.Equal(5, new[] { 5, 5 }.FromRange());
        }

        [Fact]
        public void Int_Range_Throws_When_Less_Than_Two_List_Elements()
        {
            Assert.Throws<InvalidOperationException>(() => Assert.NotNull(new int[] { }.FromRange()));
            Assert.Throws<InvalidOperationException>(() => Assert.NotNull(new[] { 0 }.FromRange()));
        }

        [Fact]
        public void Int_Range_Throws_When_More_Than_Two_List_Elements()
        {
            Assert.Throws<InvalidOperationException>(() => Assert.NotNull(new[] { 0, 1, 2 }.FromRange()));
            Assert.Throws<InvalidOperationException>(() => Assert.NotNull(new[] { 0, 1, 2, 3 }.FromRange()));
        }

        [Fact]
        public void Times_Behaves_Like_For()
        {
            int count = 0;
            5.Times(() => count++);
            Assert.Equal(5, count);
        }

        [Fact]
        public void NegativeTimes()
        {
            int count = 0;
            (-1).Times(() => count++);
            Assert.Equal(0, count);
        }
    }
}
