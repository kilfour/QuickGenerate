using System;
using QuickGenerate.Writing;
using Xunit;

namespace QuickGenerate.Tests.Writing.WritingPrimitives
{
    public class WritingStringsTest
    {
        [Fact]
        public void TheObjectWriter()
        {
            var stream = new StringStream();
            var objectWriter = new ObjectWriter();
            objectWriter.Write(
                stream,
                new SomethingToWrite
                    {
                        StringProperty = "42",
                        OtherStringProperty = null
                    });
            var reader = stream.ToReader();
            Assert.Equal("#1 : SomethingToWrite.", reader.ReadLine());
            Assert.Equal("    StringProperty = 42 : String.", reader.ReadLine());
            Assert.Equal("    OtherStringProperty = null : String.", reader.ReadLine());;
            Assert.True(reader.EndOfStream);
        }

        public class SomethingToWrite
        {
            public string StringProperty { get; set; }
            public string OtherStringProperty { get; set; }
        }
    }
}