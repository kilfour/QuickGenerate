namespace QuickGenerate.Writing
{
    public interface IWriter
    {
        void Write(IStream stream, object somethingToWrite);
    }
}