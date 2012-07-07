using QuickGenerate.Primitives;

namespace QuickGenerate.Complex
{
    public class WordGenerator : StringGenerator
    {
        public WordGenerator()
            : this(2, 10) { }
        public WordGenerator(int min, int max)
            : base(min, max, "abcdefghijklmnopqrstuvwxyz".ToCharArray()) { }
    }
}