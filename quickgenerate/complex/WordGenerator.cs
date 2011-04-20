using System.Collections.Generic;
using System.IO;
using QuickGenerate.Implementation;
using QuickGenerate.Interfaces;

namespace QuickGenerate.Complex
{
    public class WordGenerator : Generator<string>
    {
        private readonly IGenerator<string> generator;

        public WordGenerator(IGenerator<string> generator)
        {
            this.generator = generator;
        }

        public static WordGenerator FromFile(string fileName)
        {
            var words = new List<string>();
            using (var reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                    words.Add(reader.ReadLine());
            }
            return new WordGenerator(new ChoiceGenerator<string>(words.ToArray()));
        }

        public override string GetRandomValue()
        {
            return generator.GetRandomValue();
        }
    }
}