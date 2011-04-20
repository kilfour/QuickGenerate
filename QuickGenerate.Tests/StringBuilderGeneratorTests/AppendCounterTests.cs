using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGenerate.Complex;
using Xunit;

namespace QuickGenerate.Tests.StringBuilderGeneratorTests
{

    public class AppendCounterTests
    {
        private readonly StringBuilderGenerator generator;

        public AppendCounterTests()
        {
            generator = 
                new StringBuilderGenerator()
                    .Append("SomeString")
                    .AppendCounter();
        }

        [Fact]
        public void Increments()
        {
            Assert.Equal("SomeString1", generator.GetRandomValue());
            Assert.Equal("SomeString2", generator.GetRandomValue());
            Assert.Equal("SomeString3", generator.GetRandomValue());
            Assert.Equal("SomeString4", generator.GetRandomValue());
            Assert.Equal("SomeString5", generator.GetRandomValue());
            Assert.Equal("SomeString6", generator.GetRandomValue());
        }
    }
}
