using System.Text;
using QuickGenerate.Implementation;

namespace QuickGenerate.Complex
{
    public class EmailGenerator : Generator<string>
    {
        private readonly ChoiceGenerator<string> firstnameGenerator;
        private readonly ChoiceGenerator<string> lastnameGenerator;
        private readonly ChoiceGenerator<string> domainGenerator;

        public EmailGenerator(string[] firstnames, string[] lastnames, string[] domains)
        {
            firstnameGenerator = new ChoiceGenerator<string>(firstnames);
            lastnameGenerator = new ChoiceGenerator<string>(lastnames);
            domainGenerator = new ChoiceGenerator<string>(domains);
        }

        public override string GetRandomValue()
        {
            var sb = new StringBuilder();
            sb.Append(firstnameGenerator.GetRandomValue());
            sb.Append(".");
            sb.Append(lastnameGenerator.GetRandomValue());
            sb.Append("@");
            sb.Append(domainGenerator.GetRandomValue());
            return sb.ToString();
        }
    }
}