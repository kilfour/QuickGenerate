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

    public static class Generator
    {
        public static EntityGenerator<T> For<T>()
        {
            return new EntityGenerator<T>();
        }
    }
}