using System;
using Xunit;

namespace QuickGenerate.Tests.Choices
{
    public class TimesTests
    {
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