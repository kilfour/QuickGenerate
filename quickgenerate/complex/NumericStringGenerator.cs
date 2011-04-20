using System.Text;
using QuickGenerate.Primitives;

namespace QuickGenerate.Complex
{
    public class NumericStringGenerator : Generator<string>
    {
        private readonly IntGenerator intGenerator = new IntGenerator(0, 10); // Excludes 10
        private readonly int min;
        private readonly int max;

        public NumericStringGenerator(int length)
        {
            min = length;
            max = length;
        }

        public NumericStringGenerator(int min, int max)
        {
            this.min = min;
            this.max = max;
        }

        public override string GetRandomValue()
        {
            var length = new[] {min, max}.FromRange();
            var result = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                result.Append(intGenerator.GetRandomValue().ToString());
            }
            return result.ToString();
        }
    }
}
