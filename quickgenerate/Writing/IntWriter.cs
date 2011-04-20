using System;
using System.Text;

namespace QuickGenerate.Writing
{
    public class IntWriter : IWriter
    {
        public void Write(IStream stream, int? intProperty)
        {
            var stringBuilder = new StringBuilder();
            if (intProperty.HasValue)
                stringBuilder.Append(intProperty.Value);
            else
                stringBuilder.Append("null");
            stringBuilder.Append(" : Int32.");
            stream.Write(stringBuilder.ToString());
        }

        public void Write(IStream stream, object somethingToWrite)
        {
            Write(stream, (int?)somethingToWrite);
        }


        public void WriteLine(IStream stream, object somethingToWrite)
        {
            Write(stream, (int?)somethingToWrite);
            Write(stream, Environment.NewLine);
        }
    }
}