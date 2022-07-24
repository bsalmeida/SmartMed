using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace SmartMed.Data
{
    public static class MongoBootstrap
    {
        public static IServiceCollection AddMongo(this IServiceCollection services)
        {
            services.AddSingleton<IMongoClient>(x => new MongoClient("mongodb://localhost:27017/SVC_MEDICATIONS"));

            return services;
        }
    }
}
