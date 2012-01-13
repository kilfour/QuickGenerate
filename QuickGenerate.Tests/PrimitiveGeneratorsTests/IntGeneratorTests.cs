using System;
using QuickGenerate.Primitives;
using Xunit;

namespace QuickGenerate.Tests.PrimitiveGeneratorsTests
{
    public class IntGeneratorTests
    {
        [Fact]
        public void Zero()
        {
            var generator = new IntGenerator(0, 0);
            10.Times(() => Assert.Equal(0, generator.GetRandomValue()));
        }

        [Fact]
        public void DefaultGeneratorNeverGeneratesZero()
        {
            var generator = new IntGenerator();
            100.Times(() => Assert.NotEqual(0, generator.GetRandomValue()));
        }

        [Fact]
        public void UsingDomainGenerator()
        {
            var generator = new DomainGenerator();
            100.Times(() =>
                          {
                              var something = generator.One<SomethingToGenerate>();
                              Assert.NotEqual(0, something.PropOne);
                              Assert.NotEqual(0, something.PropTwo);
                          });
        }

        public class SomethingToGenerate
        {
            public Int32 PropOne { get; set; }
            public int PropTwo { get; set; }
        }
    }
}