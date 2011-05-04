using System;
using Xunit;

namespace QuickGenerate.Tests.Choices
{
    public class FromRangeTests
    {
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
    }
}
