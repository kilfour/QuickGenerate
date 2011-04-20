using QuickGenerate.Implementation;

namespace QuickGenerate.Primitives
{
    public class CharGenerator : Generator<char>
    {
        private readonly int min;
        private readonly int max;

        public CharGenerator() : this(90, 122 + 1) { }
        public CharGenerator(int minValue, int maxValue)
        {
            min = minValue; 
            max = maxValue;
        }

        public override char GetRandomValue()
        {
            return (char)Seed.Random.Next(min, max);
        }
    }
}