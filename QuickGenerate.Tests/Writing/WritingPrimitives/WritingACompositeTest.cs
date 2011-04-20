using QuickGenerate.Writing;
using Xunit;

namespace QuickGenerate.Tests.Writing.WritingPrimitives
{
    public class WritingACompositeTest
    {
        [Fact(Skip = "Too much again")]
        public void TheObjectWriter()
        {
            var stream = new StringStream();
            var objectWriter = new ObjectWriter();
            objectWriter.Write(
                stream,
                new SomethingComplexToWrite
                    {
                        SomethingSimple =
                            new SomethingToWrite
                                {
                                    IntProperty = 42
                                }
                    });
            var reader = stream.ToReader();
            Assert.Equal("#1 : SomethingComplexToWrite.", reader.ReadLine());
            Assert.Equal("    SomethingSimple = #2 : SomethingToWrite.", reader.ReadLine());
            Assert.Equal("        IntProperty = 42 : Int32.", reader.ReadLine());
            Assert.True(reader.EndOfStream);

        }

        public class SomethingComplexToWrite
        {
            public SomethingToWrite SomethingSimple { get; set; }
        }

        public class SomethingToWrite
        {
            public int IntProperty { get; set; }
        }
    }
}