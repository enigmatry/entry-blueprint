using Autofac;
using Enigmatry.Blueprint.Core.Email;
using Enigmatry.Blueprint.Core.Settings;
using Enigmatry.Blueprint.Infrastructure.Email;
using Microsoft.Extensions.Logging;

namespace Enigmatry.Blueprint.Infrastructure.Autofac.Modules
{
    public class EmailModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context =>
                {
                    var smtpSettings = context.Resolve<SmtpSettings>();

                    IEmailClient emailClient = smtpSettings.UsePickupDirectory
                        ? (IEmailClient) new MailKitPickupDirectoryEmailClient(smtpSettings, context.Resolve<ILogger<MailKitPickupDirectoryEmailClient>>())
                        : new MailKitEmailClient(smtpSettings, context.Resolve<ILogger<MailKitEmailClient>>());

                    return emailClient;
                })
                .InstancePerLifetimeScope();
        }
    }
}
