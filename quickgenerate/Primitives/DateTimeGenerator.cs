using System;

namespace QuickGenerate.Primitives
{
    public class DateTimeGenerator : Generator<DateTime>
    {
        private readonly LongGenerator generator;

        public DateTimeGenerator() 
            : this (new DateTime(1970, 1, 1), new DateTime(2010, 12, 31)) { }

        public DateTimeGenerator(DateTime min, DateTime max)
        {
            generator = new LongGenerator(min.Ticks, max.Ticks);
        }

        public override DateTime GetRandomValue()
        {
            var value = new DateTime(generator.GetRandomValue());
            value = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second);
            return value;
        }
    }
}