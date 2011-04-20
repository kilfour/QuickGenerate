using QuickGenerate.Writing;
using Xunit;

namespace QuickGenerate.Tests.Writing.WritingPrimitives
{
    public class WritingABiDirectionalOneToOneTest
    {
        [Fact(Skip="Too much again")]
        public void TheObjectWriter()
        {
            var somethingToWrite = 
                new SomethingToWrite
                    {
                        IntProperty = 42
                    };
            var somethingElseToWrite =
                new SomethingElseToWrite
                {
                    IntProperty = 42
                };

            somethingToWrite.SomethingSimple = somethingElseToWrite;
            somethingElseToWrite.SomethingSimple = somethingToWrite;

            var stream = new StringStream();
            var objectWriter = new ObjectWriter();

            objectWriter.Write(stream, somethingToWrite);
            var reader = stream.ToReader();
            Assert.Equal("#1 : SomethingToWrite.", reader.ReadLine());
            Assert.Equal("    IntProperty = 42 : Int32.", reader.ReadLine());
            Assert.Equal("    SomethingSimple = #2 : SomethingElseToWrite.", reader.ReadLine());
            Assert.Equal("        IntProperty = 42 : Int32.", reader.ReadLine());
            Assert.Equal("        SomethingSimple = #1 : SomethingToWrite.", reader.ReadLine());
            Assert.True(reader.EndOfStream);

        }

        public class SomethingToWrite
        {
            public int IntProperty { get; set; }
            public SomethingElseToWrite SomethingSimple { get; set; }
        }

        public class SomethingElseToWrite
        {
            public int IntProperty { get; set; }
            public SomethingToWrite SomethingSimple { get; set; }
        }
    }
}