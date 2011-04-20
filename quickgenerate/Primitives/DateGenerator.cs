using System;

namespace QuickGenerate.Primitives
{
    public class DateGenerator : Generator<DateTime>
    {
        private readonly LongGenerator generator;

        public DateGenerator()
            : this(new DateTime(1970, 1, 1), new DateTime(2010, 12, 31)) { }

        public DateGenerator(DateTime min, DateTime max)
        {
            generator = new LongGenerator(min.Ticks, max.Ticks);
        }

        public override DateTime GetRandomValue()
        {
            var value = new DateTime(generator.GetRandomValue());
            value = new DateTime(value.Year, value.Month, value.Day);
            return value;
        }
    }
}