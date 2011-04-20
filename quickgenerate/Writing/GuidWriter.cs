using System;
using System.Text;

namespace QuickGenerate.Writing
{
    public class GuidWriter : IWriter
    {
        public void Write(IStream stream, Guid? property)
        {
            var stringBuilder = new StringBuilder();
            if (property.HasValue)
                stringBuilder.Append(property.Value.ToString());
            else
                stringBuilder.Append("null");
            stringBuilder.Append(" : Guid.");
            stream.Write(stringBuilder.ToString());
        }

        public void Write(IStream stream, object somethingToWrite)
        {
            Write(stream, (Guid?)somethingToWrite);
        }

        public void WriteLine(IStream stream, object somethingToWrite)
        {
            Write(stream, (Guid?)somethingToWrite);
            Write(stream, Environment.NewLine);
        }
    }
}