using System;
using Xunit;

namespace QuickGenerate.Tests.Trying
{
    public class TryingTests
    {
        [Fact]
        public void Simple()
        {
        	var count = 0;

        	var attempts = 2.Attempts();
			while (attempts.Next())
			{
				count++;
			}
			Assert.Equal(2, count);
        }

		[Fact]
		public void Reset()
		{
			var count = 0;
			var reset = false;
			var attempts = 2.Attempts();
			while (attempts.Next())
			{
				if(!reset)
				{
					attempts.Reset();
					reset = true;
				}
				count++;
			}
			Assert.Equal(3, count);
		}
    }
}
