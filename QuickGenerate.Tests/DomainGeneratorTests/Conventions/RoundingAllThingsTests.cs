using System;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Conventions
{
    public class RoundingAllThingsTests
    {
        [Fact]
        public void Decimals()
        {
            var something =
                new DomainGenerator()
                    .With<decimal>(val => Math.Round(val, 2))
                    .One<SomeThingToGenerate>();

            Assert.Equal(Math.Round(something.Decimal, 2), something.Decimal);
            Assert.Equal(Math.Round(something.OtherDecimal, 2), something.OtherDecimal);
        }

        public class SomeThingToGenerate
        {
            public decimal Decimal { get; set; }
            public Decimal OtherDecimal { get; set; }
        }
    }
}
