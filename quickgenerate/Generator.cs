using QuickGenerate.Interfaces;

namespace QuickGenerate
{
    public abstract class Generator<T> : IGenerator<T>
    {
        public abstract T GetRandomValue();

        public object RandomAsObject()
        {
            return GetRandomValue();
        }
    }
}