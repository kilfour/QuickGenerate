using System;

namespace QuickGenerate
{
    public static class RepeatingOnesSelve
    {
        public static void Times(this int numberOfTimes, Action action)
        {
            if (numberOfTimes < 0)
                return;
            for (int i = 0; i < numberOfTimes; i++)
                action();
        }

        public static T Times<T>(this int numberOfTimes, Func<T> func)
        {
            T lastValue = default(T);
            if (numberOfTimes < 0)
                return lastValue;
            for (int i = 0; i < numberOfTimes; i++)
                lastValue = func();
            return lastValue;
        }
    }
}