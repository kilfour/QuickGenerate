using QuickGenerate.Implementation;

namespace QuickGenerate.Primitives
{
    public class IntGenerator : Generator<int>
    {
        private readonly int min;
        private readonly int max;

        public IntGenerator() : this(-1, 100) { }

        public IntGenerator(int minValue, int maxValue) { min = minValue; max = maxValue; }

        public override int GetRandomValue()
        {
            return Seed.Random.Next(min, max);
        }
    }
}