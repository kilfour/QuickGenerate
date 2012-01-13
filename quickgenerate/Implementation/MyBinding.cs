using System.Reflection;

namespace QuickGenerate.Implementation
{
    public static class MyBinding
    {
        public const BindingFlags Flags =
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy;
    }
}