using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickGenerate.Implementation
{
    public class ChoiceGenerator<T> : Generator<T>
    {
        private T[] choices;
        private readonly Func<T[]> choicesFunc;

        public ChoiceGenerator(params T[] possibilities) { choices = possibilities; }
        public ChoiceGenerator(Func<T[]> possibilitiesFunc) { choicesFunc = possibilitiesFunc; }
        public override T GetRandomValue()
        {
            if (choicesFunc != null)
                choices = choicesFunc();
            return Next(choices);
        }

        public static T Next(IEnumerable<T> list)
        {
            var asList = list.ToList();
            var count = asList.Count();
            if (count == 0)
                throw new InvalidOperationException("Can't choose something from an empty collection.");
            return asList.ElementAt(Seed.Random.Next(0, count));
        }

        // This dead code is still here as we might introduce 'probabilities'.
        // But currently it is easier to just add another generator.
        // Probabilities should give more fine-grained control if needed.
        //
        //public static T Next(IEnumerable<T> list, IEnumerable<int> probabilities)
        //{
        //    int totalWeight = 0;
        //    foreach (int prob in probabilities)
        //    {
        //        totalWeight += prob;
        //    }
        //    int rand = Seed.Random.Next(0, totalWeight);
        //    int weightSoFar = 0;
        //    for (int i = 0; i < probabilities.Count(); i++)
        //    {
        //        weightSoFar += probabilities.ElementAt(i);
        //        if (weightSoFar >= rand)
        //            return list.ElementAt(i);
        //    }
        //    return list.ElementAt(list.Count() - 1);
        //}
    }
}