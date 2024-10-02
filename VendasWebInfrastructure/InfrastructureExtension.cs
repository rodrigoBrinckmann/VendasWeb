using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using VendasWebCore.Repositories;
using VendasWebCore.Services;
using VendasWebInfrastructure.AuthServices;
using VendasWebInfrastructure.MessageBus;
using VendasWebInfrastructure.Persistence;
using VendasWebInfrastructure.Persistence.Repositories;
using VendasWebInfrastructure.Services.EmailService;
using VendasWebInfrastructure.Services.OrderNotificationService;
using VendasWebInfrastructure.Services.PasswordChangeNotificationService;

namespace VendasWebInfrastructure
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();            

            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IItensPedidoRepository, ItensPedidoRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddSingleton<IOrderNotificationService, OrderNotificationService>();
            services.AddSingleton<IPasswordChangeNotificationService, PasswordChangeNotificationService>();

            return services;
        }

        public static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration configuration)
        {
            var config = new MailConfig();
            
            configuration.GetSection("GoogleApi").Bind(config);                    
            
            services.AddSingleton<MailConfig>(m => config);            
            
            //services.AddSendGrid(sp => sp.ApiKey = config.SendGridApiKey);
            
            services.AddTransient<IEmailService, EmailSenderService>();
            return services;            
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            //Real DB
            services.AddDbContext<VendasWebDbContext>
                (options => options.UseSqlServer
                (configuration.GetConnectionString("Default")));

            //services.AddDbContext<VendasWebDbContext>
            //    (options => options.UseInMemoryDatabase("tst"));

            return services;
        }

        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            Console.WriteLine(configuration["RabbitMQ:HostName"]);
            Console.WriteLine(Convert.ToInt32(configuration["RabbitMQ:Port"]));
            Console.WriteLine(configuration["RabbitMQ:Username"]);
            Console.WriteLine(configuration["RabbitMQ:Password"]);            
            var connectionFactory = new ConnectionFactory
            {
                //HostName = "localhost",
                HostName = configuration["RabbitMq:HostName"],
                Port = Convert.ToInt32(configuration["RabbitMq:Port"]),
                UserName = configuration["RabbitMq:Username"],
                Password = configuration["RabbitMq:Password"]
            };

            var connection = connectionFactory.CreateConnection("VendasWebApi");

            services.AddSingleton(new ProducerConnetion(connection));
            services.AddSingleton<IMessageBusClient, RabbitMqClient>();


            return services;
        }
    }
}
