using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Repositories;
using VendasWebCore.Services;
using VendasWebInfrastructure.Persistence.Repositories;
using VendasWebInfrastructure.Persistence;
using VendasWebInfrastructure.Services;
using VendasWebInfrastructure.AuthServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace VendasWebInfrastructure
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {        
            //Real DB
            //services.AddDbContext<VendasWebDbContext>
            //    (options => options.UseSqlServer
            //    (configuration.GetConnectionString("Default")));
            
            services.AddDbContext<VendasWebDbContext>
                (options => options.UseInMemoryDatabase("tst"));

            services.AddScoped<IAuthService, AuthService>();
            services.AddTransient<IEmailService, EmailSenderService>();

            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IItensPedidoRepository, ItensPedidoRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
