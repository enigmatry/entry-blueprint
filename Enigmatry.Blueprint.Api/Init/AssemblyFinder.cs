using System.Reflection;

namespace Enigmatry.Blueprint.Api.Init
{
    public static class AssemblyFinder
    {
        private const string ProjectPrefix = "Enigmatry.Blueprint";

        public static Assembly ApplicationServicesAssembly => FindAssembly("ApplicationServices");
        public static Assembly ApiAssembly => FindAssembly("Api");
        public static Assembly DomainAssembly => FindAssembly("Model");

        private static Assembly FindAssembly(string projectSuffix)
        {
            return Assembly.Load($"{ProjectPrefix}.{projectSuffix}");
        }
    }
}
