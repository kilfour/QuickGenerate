using System.IO;

namespace QuickGenerate.Writing
{
    public interface IStream
    {
        void Write(string input);
        void WriteLine();
        StreamReader ToReader();
    }
}