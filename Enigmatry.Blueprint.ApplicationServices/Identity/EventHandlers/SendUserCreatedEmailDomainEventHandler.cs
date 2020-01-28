using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Enigmatry.Blueprint.Core.Email;
using Enigmatry.Blueprint.Model.Identity.DomainEvents;
using JetBrains.Annotations;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Enigmatry.Blueprint.ApplicationServices.Identity.EventHandlers
{
    [UsedImplicitly]
    public class SendUserCreatedEmailDomainEventHandler : INotificationHandler<UserCreatedDomainEvent>
    {
        private readonly ILogger<SendUserCreatedEmailDomainEventHandler> _log;
        private readonly IEmailClient _emailClient;

        public SendUserCreatedEmailDomainEventHandler(ILogger<SendUserCreatedEmailDomainEventHandler> log,
            IEmailClient emailClient)
        {
            _log = log;
            _emailClient = emailClient;
        }

        public Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _emailClient.Send(new EmailMessage("New user", "Your account is successfully created.", new[] {notification.UserName}, Enumerable.Empty<string>()));

            _log.LogDebug("User email sent: {UserName}", notification.UserName);

            return Task.CompletedTask;
        }
    }
}
