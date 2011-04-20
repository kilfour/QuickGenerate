namespace QuickGenerate.Reflect
{
    public static class FromAssembly
    {
        public static TypePicker Containing<T>()
        {
            return new TypePicker(typeof(T).Assembly.GetTypes());
        }
    }
}