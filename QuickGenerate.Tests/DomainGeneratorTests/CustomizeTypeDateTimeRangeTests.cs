using System;
using QuickGenerate.DomainGeneratorImplementation;
using QuickGenerate.HardCodeThoseDates;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class CustomizeTypeDateTimeRangeTests
    {
        private readonly IDomainGenerator domainGenerator =
            new DomainGenerator()
                .With<SomethingToGenerate>(g => g.Range(e => e.MyProperty, 1.January(2010), 3.January(2010)));

        [Fact]
        public void GeneratorIsApplied()
        {
            10.Times(
                () =>
                    {
                        var something = domainGenerator.One<SomethingToGenerate>();
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