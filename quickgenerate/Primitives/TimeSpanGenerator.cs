using System;
using QuickGenerate.Implementation;

namespace QuickGenerate.Primitives
{
    public class TimeSpanGenerator : Generator<TimeSpan>
    {
        private readonly int max;

        public TimeSpanGenerator() : this(1000) { }

        public TimeSpanGenerator(int maxValue) { max = maxValue; }

        public override TimeSpan GetRandomValue()
        {
            return new TimeSpan(Seed.Random.Next(0, max));
        }
    }
}