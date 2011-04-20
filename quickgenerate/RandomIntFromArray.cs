using System;
using System.Collections.Generic;
using System.Linq;
using QuickGenerate.Primitives;

namespace QuickGenerate
{
    public static class RandomIntFromArray
    {
        public static int FromRange(this IEnumerable<int> range)
        {
            var list = range.ToList();

            if (list.Count() != 2)
                throw new InvalidOperationException();

            return new IntGenerator(list.First(), list.Last()).GetRandomValue();
        }
    }
}