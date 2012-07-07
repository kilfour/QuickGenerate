using System.Linq;

namespace QuickGenerate.Data
{
    public static partial class Lists
    {
        public static string[] FirstNames
        {
            get
            {
                return MaleFirstNames.Union(FemaleFirstNames).ToArray();
            }
        }
    }
}
