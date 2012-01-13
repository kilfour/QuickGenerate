using System.Collections.Generic;
using System.Text;

namespace QuickGenerate.Gathering
{
    public class GathererMatchResult
    {
        public bool IsMatch { get { return messages.Count == 0; } }
        private readonly List<string> messages = new List<string>();
        public IEnumerable<string> Messages { get { return messages; } }
        public string GetMessage()
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            foreach (var message in Messages)
            {
                sb.AppendLine(message);
            }
            return sb.ToString();
        }

        public void AddMessage(string message)
        {
            messages.Add(message);
        }
    }
}