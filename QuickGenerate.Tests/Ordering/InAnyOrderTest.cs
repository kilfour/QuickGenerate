using Xunit;

namespace QuickGenerate.Tests.Ordering
{
	public class InAnyOrderTest
	{
		[Fact]
		public void Sorted()
		{
			var twoInts = new[] {0, 42};
			var is42 = false;
			var isNot42 = false;
			10.Times(
				() =>
					{
						var first = true;
						foreach (var anInt in twoInts.InAnyOrder())
						{
							if(first)
							{
								first = false;
								is42 = is42 || anInt == 42;
								isNot42 = isNot42 || anInt != 42;
							}
						}
					});
			Assert.True(is42);
			Assert.True(isNot42);
		}
	}
}
