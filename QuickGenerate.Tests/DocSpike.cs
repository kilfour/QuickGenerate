using System.IO;
using QuickDoc;
using QuickDoc.Reporters;

namespace QuickGenerate.Tests
{
    public class DocSpike
    {
        public void Doc()
        {
            using (var writer = new StreamWriter(@"../.../../doc.html"))
            {
                new DocParser()
                .FromAssemblyName("QuickGenerate.Tests")
                .Report(new HtmlReporter(writer));
            }
        }
    }
}
