using System;
using QuickGenerate.Writing;
using Xunit;

namespace QuickGenerate.Tests.Writing.WritingPrimitives
{
    public class WritingIntsTest
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
                    IntProperty = 42,
                    Int32Property = 42,
                    NullableIntProperty = 42,
                    NullableInt32Property = null
                });
            var reader = stream.ToReader();
            Assert.Equal("#1 : SomethingToWrite.", reader.ReadLine());
            Assert.Equal("    IntProperty = 42 : Int32.", reader.ReadLine());
            Assert.Equal("    Int32Property = 42 : Int32.", reader.ReadLine());
            Assert.Equal("    NullableIntProperty = 42 : Int32.", reader.ReadLine());
            Assert.Equal("    NullableInt32Property = null : Int32.", reader.ReadLine());
            Assert.True(reader.EndOfStream);
        }

        [Fact]
        public void TheIntWriter()
        {
            var writer = new IntWriter();
            var somethingToWrite =
                new SomethingToWrite
                    {
                        IntProperty = 42,
                        Int32Property = 42,
                        NullableIntProperty = 42,
                        NullableInt32Property = 42
                    };
            var stream = new StringStream();
            writer.Write(stream, somethingToWrite.IntProperty);
            Assert.Equal("42 : Int32.", stream.ToString());

            stream = new StringStream();
            writer.Write(stream, somethingToWrite.Int32Property);
            Assert.Equal("42 : Int32.", stream.ToString());

            stream = new StringStream();
            writer.Write(stream, somethingToWrite.NullableIntProperty);
            Assert.Equal("42 : Int32.", stream.ToString());

            stream = new StringStream();
            writer.Write(stream, somethingToWrite.NullableInt32Property);
            Assert.Equal("42 : Int32.", stream.ToString());

            stream = new StringStream();
            somethingToWrite.NullableIntProperty = null;
            writer.Write(stream, somethingToWrite.NullableIntProperty);
            Assert.Equal("null : Int32.", stream.ToString());

            stream = new StringStream();
            somethingToWrite.NullableInt32Property = null;
            writer.Write(stream, somethingToWrite.NullableInt32Property);
            Assert.Equal("null : Int32.", stream.ToString());


        }

        public class SomethingToWrite
        {
            public int IntProperty { get; set; }
            public Int32 Int32Property { get; set; }
            public int? NullableIntProperty { get; set; }
            public Int32? NullableInt32Property { get; set; }
        }
    }
}
