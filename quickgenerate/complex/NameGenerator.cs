using QuickGenerate.Data;

namespace QuickGenerate.Complex
{
    public class NameGenerator : StringBuilderGenerator
    {
        public NameGenerator()
        {
            Append(Lists.FirstNames);
            Space();
            Append(Lists.LastNames);
        }
    }
}
