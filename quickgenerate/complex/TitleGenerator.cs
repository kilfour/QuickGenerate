using System.Text;
using QuickGenerate.Implementation;
using QuickGenerate.Primitives;

namespace QuickGenerate.Complex
{
    public class TitleGenerator : Generator<string>
    {
        private readonly Generator<string>[] wordGenerators =
            new Generator<string>[]
                {
                    new ChoiceGenerator<string>("I", "a", "of", "it", "the"),
                    new CapitalizedWordGenerator()
                };

        private readonly IntGenerator lengthGenerator;

        public TitleGenerator(int min, int max)
        {
            lengthGenerator = new IntGenerator(min, max);
        }

        public override string GetRandomValue()
        {
            var sb = new StringBuilder();
            var sentenceLength = lengthGenerator.GetRandomValue();
            var first = wordGenerators.PickOne().GetRandomValue().Capitalize();
            sb.Append(first);
            sb.Append(" ");
            for(var i = 1; i < sentenceLength - 1; i ++)
            {
                var word = wordGenerators.PickOne().GetRandomValue();
                sb.Append(word);
                sb.Append(" ");
            }
            var last = wordGenerators.PickOne().GetRandomValue();
            sb.Append(last);
            return sb.ToString();
        }
    }
}