using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using VendasWebApplication.Services;
using VendasWebCore.Services;

namespace VendasWebApplication
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => 
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
                .AddServices();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {            
            services.AddScoped<IItensPedidoService, ItensPedidoService>();

            return services;
        }
    }
}
