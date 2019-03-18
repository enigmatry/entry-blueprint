using System.Collections.Generic;
using System.Linq;

namespace Enigmatry.Blueprint.Core.Email
{
    public class EmailMessage
    {
        public EmailMessage(string subject, string content, IEnumerable<string> to, IEnumerable<string> from = null)
        {
            Subject = subject;
            Content = content;
            To = to;
            From = from ?? Enumerable.Empty<string>();
        }

        public string Subject { get; }
        public string Content { get; }
        public IEnumerable<string> To { get; }
        public IEnumerable<string> From { get; }
    }
}
