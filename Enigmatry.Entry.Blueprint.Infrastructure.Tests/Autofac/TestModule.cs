using Autofac;
using Enigmatry.Entry.Core;
using JetBrains.Annotations;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Tests.Autofac;

[UsedImplicitly]
public class TestModule : Module
{
    protected override void Load(ContainerBuilder builder) =>
        builder.RegisterType<SettableTimeProvider>()
            .As<ITimeProvider>()
            .AsSelf()
            .SingleInstance();
}
