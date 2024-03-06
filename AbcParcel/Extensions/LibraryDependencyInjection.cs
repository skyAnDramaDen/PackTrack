using NetCore.AutoRegisterDi;
using System.Reflection;

namespace AbcParcel.Extensions
{
    // methods for registering application services.
    public static class LibraryDependencyInjection
    {
        // Registers application services from the specified assembly.
        // This method scans the specified assembly for classes ending with "Service"
        // and registers them as their implemented interfaces.
        public static IServiceCollection RegisterApplicationService<T>(this IServiceCollection services)
        {
            // get the assembly to scan based on the specified type T.
            var assemblyToScan = Assembly.GetAssembly(typeof(T));

            // register all public non-generic classes in the assembly
            // that end with "Service" as their implemented interfaces.
            services.RegisterAssemblyPublicNonGenericClasses(assemblyToScan)
                .Where(x => x.Name.EndsWith("Service"))
                .AsPublicImplementedInterfaces();

            return services;
        }
    }
}