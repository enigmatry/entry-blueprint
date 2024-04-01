using System.Security.Principal;
using Autofac;
using Enigmatry.Entry.Blueprint.Infrastructure.Tests;
using Enigmatry.Entry.Blueprint.Tests.Infrastructure.Impersonation;
using Enigmatry.Entry.Core;
using JetBrains.Annotations;

namespace Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure.Autofac;

[UsedImplicitly]
public class TestModule : Module
{
    private readonly bool _replaceCurrentUser;

    public TestModule(bool replaceCurrentUser)
    {
        _replaceCurrentUser = replaceCurrentUser;
    }

    protected override void Load(ContainerBuilder builder)
    {
        if (_replaceCurrentUser)
        {
            ReplaceCurrentUserWithTestUser(builder);
        }

        builder.RegisterType<SettableTimeProvider>()
            .As<ITimeProvider>()
            .AsSelf()
            .SingleInstance();
    }

    private static void ReplaceCurrentUserWithTestUser(ContainerBuilder builder) =>
        builder.Register(_ => TestUserData.CreateClaimsPrincipal()).As<IPrincipal>().InstancePerLifetimeScope();
}
