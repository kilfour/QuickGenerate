using System.Collections.Generic;

namespace QuickGenerate.Tests.DomainGeneratorTests.Iterating
{
    public class ForEachSpy
    {
        public List<object> Checked = new List<object>();
        public void Check(object toCheck)
        {
            Checked.Add(toCheck);
        }
    }
}