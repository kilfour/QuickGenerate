using System;
using QuickGenerate.Primitives;

namespace QuickGenerate
{
    public static class Maybe
    {
        public static void Do(Action action)
        {
            if (new BoolGenerator().GetRandomValue())
                action();
        }
    }
}