using QuickGenerate.Modifying;

namespace QuickGenerate
{
    public static class Modify
    {
        public static DomainModifier<T> This<T>(T value)
        {
            return new DomainModifier<T>(value, new DomainGenerator());
        }
    }
}