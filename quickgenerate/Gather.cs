using QuickGenerate.Implementation;

namespace QuickGenerate
{
    public static class Gather
    {
        public static Gatherer<T> From<T>(T value)
        {
            return new Gatherer<T>(value);
        }
    }
}