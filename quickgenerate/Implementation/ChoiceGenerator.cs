using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickGenerate.Implementation
{
    public class ChoiceGenerator<T> : Generator<T>
    {
        private IEnumerable<T> choices;
        private readonly Func<T[]> choicesFunc;
		public ChoiceGenerator(IEnumerable<T> possibilities) { choices = possibilities; }
        public ChoiceGenerator(params T[] possibilities) { choices = possibilities; }
        public ChoiceGenerator(Func<T[]> possibilitiesFunc) { choicesFunc = possibilitiesFunc; }
        public override T GetRandomValue()
        {
            if (choicesFunc != null)
                choices = choicesFunc();
			return choices.ElementAt(Seed.Random.Next(0, choices.Count()));
        }
    }
}