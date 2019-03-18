using System;
using System.IO;
using Enigmatry.Blueprint.Core.Email;
using Enigmatry.Blueprint.Core.Settings;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace Enigmatry.Blueprint.Infrastructure.Email
{
    public class MailKitPickupDirectoryEmailClient : IEmailClient
    {
        private readonly ILogger<MailKitPickupDirectoryEmailClient> _logger;
        private readonly SmtpSettings _settings;

        public MailKitPickupDirectoryEmailClient(SmtpSettings settings, ILogger<MailKitPickupDirectoryEmailClient> logger)
        {
            _settings = settings;
            _logger = logger;
        }

        public void Send(EmailMessage email)
        {
            var message = new MimeMessage();
            message.SetEmailData(email, _settings);

            if (!Directory.Exists(_settings.PickupDirectoryLocation))
            {
                Directory.CreateDirectory(_settings.PickupDirectoryLocation);
            }
            var filePath = Path.Combine(_settings.PickupDirectoryLocation, $"{DateTime.Now.Ticks}.eml");
            message.WriteTo(filePath);

            _logger.LogDebug($"Email '{message.Subject}' is written to filesystem: {filePath}");
        }
    }
}
