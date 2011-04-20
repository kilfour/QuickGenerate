namespace QuickGenerate
{
    public static class Generate
    {
        public static T One<T>()
        {
            return new DomainGenerator().One<T>();
        }
    }
}
