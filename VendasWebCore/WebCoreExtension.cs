using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Calculation;
using VendasWebCore.Repositories;

namespace VendasWebCore
{
    public static class WebCoreExtension
    {        
        public static IServiceCollection AddWebCore(this IServiceCollection services)
        {
            services.AddSingleton<ICalculos, Calculos>();

            return services;
        }
    }
}
