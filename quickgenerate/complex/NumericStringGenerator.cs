using System.Text;
using QuickGenerate.Primitives;

namespace QuickGenerate.Complex
{
    public class NumericStringGenerator : Generator<string>
    {
        private readonly int minLength;
        private readonly int maxLength;
        private readonly IntGenerator intGenerator = new IntGenerator(0, 10); // Excludes 10

        public NumericStringGenerator(int length)
        {
            minLength = length;
            maxLength = length;
        }

        public NumericStringGenerator(int minLength, int maxLength)
        {
            this.minLength = minLength;
            this.maxLength = maxLength;
        }

        public override string GetRandomValue()
        {
            var length = new[] { minLength, maxLength }.FromRange();
            var result = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                result.Append(intGenerator.GetRandomValue().ToString());
            }
            return result.ToString();
        }
    }
}
