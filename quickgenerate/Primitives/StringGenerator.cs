using System.Text;
using QuickGenerate.Implementation;

namespace QuickGenerate.Primitives
{
    public class StringGenerator : Generator<string>
    {
        private readonly int min;
        private readonly int max;
        public IGenerator<char> CharGenerator { get; set; }

        public StringGenerator() : this(new CharGenerator(), 0, 10) { }

        public StringGenerator(int minValue, int maxValue) : this(new CharGenerator(), minValue, maxValue) { }

        public StringGenerator(int minValue, int maxValue, params char[] possibilities) 
            : this(new ChoiceGenerator<char>(possibilities), minValue, maxValue) { }

        public StringGenerator(IGenerator<char> charGenerator) : this(charGenerator, 0, 64) { }
        
        public StringGenerator(IGenerator<char> charGenerator, int minValue, int maxValue)
        {
            CharGenerator = charGenerator;
            min = minValue;
            max = maxValue;
        }

        public override string GetRandomValue()
        {
            int numberOfChars = Seed.Random.Next(min, max);
            var sb = new StringBuilder();
            for (int i = 0; i < numberOfChars; i++)
            {
                sb.Append(CharGenerator.GetRandomValue());
            }
            return sb.ToString();
        }
    }
}