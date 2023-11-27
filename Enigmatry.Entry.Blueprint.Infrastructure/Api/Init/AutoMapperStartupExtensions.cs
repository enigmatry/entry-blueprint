using Microsoft.Extensions.DependencyInjection;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Api.Init;

public static class AutoMapperStartupExtensions
{
    public static void AppAddAutoMapper(this IServiceCollection services) => services.AddAutoMapper(AssemblyFinder.ApiAssembly, AssemblyFinder.ApplicationServicesAssembly);
}
