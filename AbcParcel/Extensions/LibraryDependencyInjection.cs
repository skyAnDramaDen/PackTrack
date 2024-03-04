using NetCore.AutoRegisterDi;
using System.Reflection;

namespace AbcParcel.Extensions
{
    public static class LibraryDependencyInjection
    {
        public static IServiceCollection RegisterApplicationService<T>(this IServiceCollection services)
        {
            var assemblyToScan = Assembly.GetAssembly(typeof(T));
            services.RegisterAssemblyPublicNonGenericClasses(assemblyToScan)
                .Where(x => x.Name.EndsWith("Service"))
                .AsPublicImplementedInterfaces();
            return services;
        }
    }
}
