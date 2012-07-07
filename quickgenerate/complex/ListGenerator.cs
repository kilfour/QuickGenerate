using System.Collections.Generic;
using System.IO;
using QuickGenerate.Implementation;

namespace QuickGenerate.Complex
{
    public class ListGenerator : Generator<string>
    {
        private readonly IGenerator<string> generator;

        public ListGenerator(IGenerator<string> generator)
        {
            this.generator = generator;
        }

        public static ListGenerator FromFile(string fileName)
        {
            var words = new List<string>();
            using (var reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                    words.Add(reader.ReadLine());
            }
            return new ListGenerator(new ChoiceGenerator<string>(words.ToArray()));
        }

        public override string GetRandomValue()
        {
            return generator.GetRandomValue();
        }
    }
}