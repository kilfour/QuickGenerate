using System;
using QuickGenerate.Implementation;

namespace QuickGenerate.Primitives
{
    public class LongGenerator : Generator<long>
    {
        private readonly long min;
        private readonly long max;

        public LongGenerator() : this(-1, 100) { }

        public LongGenerator(long minValue, long maxValue) { min = minValue; max = maxValue; }

        public override long GetRandomValue()
        {
            if (max < min)
                throw new ArgumentOutOfRangeException();

            return (long)((Seed.Random.NextDouble() * (max - min)) + min);
        }
    }
}