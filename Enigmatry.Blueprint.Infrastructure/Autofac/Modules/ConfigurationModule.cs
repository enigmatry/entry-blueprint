using Autofac;
using Enigmatry.Blueprint.Infrastructure.Configuration;
using Enigmatry.Entry.Core.Settings;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;

namespace Enigmatry.Blueprint.Infrastructure.Autofac.Modules;

[UsedImplicitly]
public class ConfigurationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(c => c.Resolve<IConfiguration>().ReadAppSettings())
            .AsSelf()
            .SingleInstance();

        builder.Register(c => c.Resolve<IConfiguration>().ReadSettingsSection<DbContextSettings>("DbContext"))
            .AsSelf()
            .SingleInstance();

        builder.Register(c => c.Resolve<IConfiguration>()
                .ReadSettingsSection<ApplicationInsightsSettings>("ApplicationInsights"))
            .AsSelf()
            .SingleInstance();
    }
}
