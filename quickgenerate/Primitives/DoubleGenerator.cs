using System;
using QuickGenerate.Implementation;

namespace QuickGenerate.Primitives
{
    public class DoubleGenerator : Generator<double>
    {
        private readonly double min;
        private readonly double max;

        public DoubleGenerator() : this(1, 100) { }

        public DoubleGenerator(double minValue, double maxValue)
        {
            min = minValue;
            max = maxValue;
        }

        public override double GetRandomValue()
        {
            if (max < min)
                throw new ArgumentOutOfRangeException();
            return
                ((Seed.Random.NextDouble() * (max - min)) + min);
        }
    }
}