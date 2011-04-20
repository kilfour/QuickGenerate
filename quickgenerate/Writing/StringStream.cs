using System.IO;
using System.Text;

namespace QuickGenerate.Writing
{
    public class StringStream : IStream
    {
        private readonly StringBuilder stringBuilder = new StringBuilder();

        public void Write(string input)
        {
            stringBuilder.Append(input);
        }

        public void WriteLine()
        {
            stringBuilder.AppendLine();
        }

        public override string ToString()
        {
            return stringBuilder.ToString();
        }

        public StreamReader ToReader()
        {
            byte[] byteArray = Encoding.ASCII.GetBytes(ToString());
            var stream = new MemoryStream(byteArray);
            return new StreamReader(stream);
        }
    }
}