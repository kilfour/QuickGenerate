using System;
using QuickGenerate.Implementation;

namespace QuickGenerate.Primitives
{
    public class DecimalGenerator : Generator<decimal>
    {
        private readonly decimal min;
        private readonly decimal max;

        public DecimalGenerator() : this(1, 100) { }

        public DecimalGenerator(decimal minValue, decimal maxValue)
        {
            min = minValue; 
            max = maxValue;
        }

        public override decimal GetRandomValue()
        {
            if(max < min)
                throw new ArgumentOutOfRangeException();

            return (((decimal)Seed.Random.NextDouble() * (max - min)) + min);
        }
    }
}