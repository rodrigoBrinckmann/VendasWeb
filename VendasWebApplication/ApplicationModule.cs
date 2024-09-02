using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace VendasWebApplication
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));         

            return services;
        }        
    }
}
