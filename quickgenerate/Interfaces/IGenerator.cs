namespace QuickGenerate.Interfaces
{
    public interface IGenerator
    {
        object RandomAsObject();
    }

    public interface IGenerator<T> : IGenerator
    {
        T GetRandomValue();
    }
}