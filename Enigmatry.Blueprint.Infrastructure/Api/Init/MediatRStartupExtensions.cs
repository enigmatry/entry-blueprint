using Enigmatry.Entry.MediatR;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Enigmatry.Blueprint.Infrastructure.Api.Init;

public static class MediatRStartupExtensions
{
    public static void AppAddMediatR(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        //services.AddScoped(typeof(IRequestPreProcessor<>), typeof(SamplePreRequestBehavior<>));
        //services.AddScoped(typeof(IRequestPostProcessor<,>), typeof(SamplePostRequestBehavior<,>));

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(AssemblyFinder.ApiAssembly,
                AssemblyFinder.DomainAssembly,
                AssemblyFinder.ApplicationServicesAssembly);
        });
    }
}
