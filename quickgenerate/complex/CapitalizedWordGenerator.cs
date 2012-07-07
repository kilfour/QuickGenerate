using QuickGenerate.Primitives;

namespace QuickGenerate.Complex
{
    public class CapitalizedWordGenerator : StringGenerator
    {
        public CapitalizedWordGenerator()
            : this(2, 10) { }
        public CapitalizedWordGenerator(int min, int max)
            : base(min, max, "abcdefghijklmnopqrstuvwxyz".ToCharArray()) { }

        public override string GetRandomValue()
        {
            return base.GetRandomValue().Capitalize();
        }
    }
}