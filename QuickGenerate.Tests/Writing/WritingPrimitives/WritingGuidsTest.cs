using System;
using QuickGenerate.Writing;
using Xunit;

namespace QuickGenerate.Tests.Writing.WritingPrimitives
{
    public class WritingGuidsTest
    {
        [Fact]
        public void TheObjectWriter()
        {
            var objectWriter = new ObjectWriter();
            objectWriter.Write(
                new SomethingToWrite
                    {
                        Property = Guid.Empty,
                        OtherProperty = null
                    });
            var reader = objectWriter.ToReader();
            Assert.Equal("#1 : SomethingToWrite.", reader.ReadLine());
            Assert.Equal("    Property = 00000000-0000-0000-0000-000000000000 : Guid.", reader.ReadLine());
            Assert.Equal("    OtherProperty = null : Guid.", reader.ReadLine()); ;
            Assert.True(reader.EndOfStream);
        }

        public class SomethingToWrite
        {
            public Guid Property { get; set; }
            public Guid? OtherProperty { get; set; }
        }
    }
}