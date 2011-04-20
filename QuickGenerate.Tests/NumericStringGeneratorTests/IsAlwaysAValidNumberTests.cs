using QuickGenerate.Complex;
using Xunit;

namespace QuickGenerate.Tests.NumericStringGeneratorTests
{
    public class IsAlwaysAValidNumberTests
    {
        [Fact]
        public void Works()
        {
            var generator = new NumericStringGenerator(6);
            50.Times(
                () =>
                    {
                        var number = generator.GetRandomValue();
                        int numberAsInt;
                        var success = int.TryParse(number, out numberAsInt);
                        Assert.True(success, number + " is not a number.");
                    });
        }
    }
}
