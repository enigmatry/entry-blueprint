using System;
using System.Linq;
using Enigmatry.Blueprint.Core.Email;
using Enigmatry.Blueprint.Core.Settings;
using MimeKit;
using MimeKit.Text;

namespace Enigmatry.Blueprint.Infrastructure.Email
{
    public static class MimeMessageExtensions
    {
        public static void SetEmailData(this MimeMessage message, EmailMessage email, SmtpSettings settings)
        {
            message.To.AddRange(email.To.Select(address => MailboxAddress.Parse(address)));
            message.From.AddRange(email.From.Select(address => MailboxAddress.Parse(address)));

            if (!String.IsNullOrEmpty(settings.From))
            {
                message.From.Add(MailboxAddress.Parse(settings.From));
            }

            message.Subject = email.Subject;
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = email.Content
            };
        }
    }
}
