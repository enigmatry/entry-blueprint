using System.Reflection;

namespace Enigmatry.Blueprint.Infrastructure.Api.Init;

public static class AssemblyFinder
{
    private const string ProjectPrefix = "Enigmatry.Blueprint";

    public static Assembly ApplicationServicesAssembly => FindAssembly("ApplicationServices");
    public static Assembly ApiAssembly => FindAssembly("Api");
    public static Assembly DomainAssembly => FindAssembly("Model");
    public static Assembly InfrastructureAssembly => FindAssembly("Infrastructure");


    private static Assembly FindAssembly(string projectSuffix) => Find($"{ProjectPrefix}.{projectSuffix}");

    public static Assembly Find(string assemblyName) => Assembly.Load(assemblyName);
}
