using System;

namespace QuickGenerate.Writing
{
    public class StringWriter : IWriter
    {
        public void Write(IStream stream, string input)
        {
            if(input == null)
                stream.Write("null");
            stream.Write(input);
            stream.Write(" : String.");
        }

        public void Write(IStream stream, object somethingToWrite)
        {
            Write(stream, (string)somethingToWrite);
        }

        public void WriteLine(IStream stream, object somethingToWrite)
        {
            Write(stream, (string)somethingToWrite);
            Write(stream, Environment.NewLine);
        }
    }
}