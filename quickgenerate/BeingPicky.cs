using System;
using System.Collections.Generic;
using System.Linq;
using QuickGenerate.Implementation;

namespace QuickGenerate
{
	public static class BeingPicky
    {
        public class TheNumberOfElementsToPickMustBeSmallerThanTheNumberOfPossibleChoices 
            : Exception { }

        public static IEnumerable<T> Pick<T>(this IEnumerable<T> choices, int number)
        {
            var list = choices.ToList();
            if (list.Count() < number)
                throw new TheNumberOfElementsToPickMustBeSmallerThanTheNumberOfPossibleChoices();
            var newList = new List<T>();
            number.Times(
                () =>
                    {
                        var el = new ChoiceGenerator<T>(list.ToArray).GetRandomValue();
                        newList.Add(el);
                        list.Remove(el);
                    });
            return newList;
        }

        public static T PickOne<T>(this IEnumerable<T> choices)
        {
            return new ChoiceGenerator<T>(choices.ToArray).GetRandomValue();
        }
    }
}