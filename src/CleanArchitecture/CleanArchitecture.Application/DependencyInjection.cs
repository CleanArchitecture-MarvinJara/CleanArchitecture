using CleanArchitecture.Domain.Alquileres;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application
{
    // Clase contenedora de dependencias
    public static class DependencyInjection
    {
        public static IServiceCollection AddAplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });

            services.AddTransient<PrecioService>();

            return services;
        }
    }
}
