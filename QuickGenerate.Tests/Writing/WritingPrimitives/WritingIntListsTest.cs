using System.Collections.Generic;
using QuickGenerate.Writing;
using Xunit;

namespace QuickGenerate.Tests.Writing.WritingPrimitives
{
    public class WritingIntListsTest
    {
        [Fact(Skip = "On the todo list")]
        public void TheObjectWriter()
        {
            var stream = new StringStream();
            var objectWriter = new ObjectWriter();
            objectWriter.Write(stream, new SomethingToWrite());
            var reader = stream.ToReader();
            Assert.Equal("#1 : SomethingToWrite.", reader.ReadLine());
            Assert.Equal("    IntListProperty : [Int32].", reader.ReadLine());
            Assert.True(reader.EndOfStream);
        }

        public class SomethingToWrite
        {
            public List<int> IntListProperty { get { return new List<int> { 1, 3, 11, 13, 42 }; } }
        }
    }
}