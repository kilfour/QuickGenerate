using System;
using QuickGenerate.HardCodeThoseDates;
using Xunit;

namespace QuickGenerate.Tests.EntityGeneratorTests
{
    public class DateTimeRangeTests
    {
        private readonly EntityGenerator<SomethingToGenerate> generator =
            new EntityGenerator<SomethingToGenerate>()
                .Range(e => e.MyProperty, 1.January(2010), 3.January(2010));

        [Fact]
        public void GeneratorIsApplied()
        {
            10.Times(
                () =>
                    {
                        var something = generator.One();
                        Assert.True(something.MyProperty >= 1.January(2010));
                        Assert.True(something.MyProperty <= 3.January(2010));
                    });
        }

        public class SomethingToGenerate
        {
            public DateTime MyProperty { get; set; }
        }
    }
}