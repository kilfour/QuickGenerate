using QuickGenerate.Implementation;

namespace QuickGenerate.Primitives
{
    public class ShortGenerator : Generator<short>
    {
        private readonly short min;
        private readonly short max;

        public ShortGenerator() : this(1, 100) { }

        public ShortGenerator(short minValue, short maxValue) { min = minValue; max = maxValue; }

        public override short GetRandomValue()
        {
            return (short)((Seed.Random.NextDouble() * (max - min)) + min);
        }
    }
}