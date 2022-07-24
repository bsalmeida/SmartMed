using Microsoft.Extensions.DependencyInjection;

namespace SmartMed.Data
{
    public static class RepositoriesBootstrap
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IMedicationRepository, MedicationRepository>();

            return services;
        }
    }
}
