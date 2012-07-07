using System;
using System.Text;
using QuickGenerate.Implementation;
using QuickGenerate.Primitives;

namespace QuickGenerate.Complex
{
    public class DescriptionGenerator : Generator<string>
    {
        private readonly int nrOfWords;

        private readonly Generator<string>[] wordGenerators =
            new Generator<string>[]
                {
                    new ChoiceGenerator<string>("I", "a", "of", "it", "the"),
                    new WordGenerator()
                };

        private readonly IntGenerator lengthGenerator = new IntGenerator(5, 20);

        public DescriptionGenerator(int nrOfWords)
        {
            this.nrOfWords = nrOfWords;
        }

        public override string GetRandomValue()
        {
            var sb = new StringBuilder();
            var ix = 0;
            while (ix < nrOfWords)
            {
                var sentenceLength = lengthGenerator.GetRandomValue();
                var first = wordGenerators.PickOne().GetRandomValue().Capitalize();
                sb.Append(first);
                sb.Append(" ");
                ix++;
                for(var i = 1; i < sentenceLength - 1; i ++)
                {
                    var word = wordGenerators.PickOne().GetRandomValue();
                    sb.Append(word);
                    sb.Append(" ");
                    ix++;
                }
                var last = wordGenerators.PickOne().GetRandomValue();
                sb.Append(last);
                sb.Append(". ");
                Maybe.Do(() => sb.Append(Environment.NewLine));
                ix++;
            }
            return sb.ToString();
        }
    }
}