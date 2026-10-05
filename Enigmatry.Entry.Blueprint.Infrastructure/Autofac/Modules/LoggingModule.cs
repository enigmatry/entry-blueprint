using Autofac;
using Enigmatry.Entry.Blueprint.Core.Logging;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Logging;
using JetBrains.Annotations;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Autofac.Modules;

[UsedImplicitly]
public class LoggingModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<SecuritySerilogLogger>().AsSelf().SingleInstance();
        builder.RegisterGeneric(typeof(SecurityLogger<>)).As(typeof(ISecurityLogger<>)).SingleInstance();
    }
}
