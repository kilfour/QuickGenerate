using System;
using QuickGenerate.Implementation;

namespace QuickGenerate.Primitives
{
    public class FloatGenerator : Generator<float>
    {
        private readonly float min;
        private readonly float max;

        public FloatGenerator() : this(-1, 100) { }

        public FloatGenerator(float minValue, float maxValue)
        {
            min = minValue; 
            max = maxValue;
        }

        public override float GetRandomValue()
        {
            if(max < min)
                throw new ArgumentOutOfRangeException();
            return
                (((float)Seed.Random.NextDouble() * (max - min)) + min);
        }
    }
}