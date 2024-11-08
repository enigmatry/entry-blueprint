using System.Reflection;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Init;
using Enigmatry.Entry.Blueprint.Infrastructure.Data;
using Enigmatry.Entry.MediatR;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Init;

public static class MediatRStartupExtensions
{
    public static void AppAddMediatR(this IServiceCollection services, params Assembly[] extraAssemblies)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(AppSaveChangesBehavior<,>));

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(extraAssemblies);
            config.RegisterServicesFromAssemblies(
                AssemblyFinder.DomainAssembly,
                AssemblyFinder.ApplicationServicesAssembly);
        });
    }
}
