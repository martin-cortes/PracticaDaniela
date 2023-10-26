using Domain.Models.Interfaces.DrivenAdapters;
using Domain.Models.Interfaces.UseCase;
using Domain.UseCase;
using DrivenAdapters.Mongo.Adapters;
using DrivenAdapters.Mongo.AutoMapper;
using DrivenAdapters.Mongo.ContextMongo;

namespace CrearPersonas.Service.Configuration
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection RegisterAutoMapper(this IServiceCollection services) =>
            services.AddAutoMapper(typeof(MongoProfile));

        public static IServiceCollection RegisterMongo(this IServiceCollection services, string connectionString, string dataBase, string collectionName) =>
        services.AddSingleton<IContext>(cfg => new Context(connectionString, dataBase, collectionName));

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {

            #region Adapters

            services.AddScoped<IPersonasAdapter, PersonasAdapter>();

            #endregion Adapters

            services.AddScoped<IPersonasUseCase, PersonasUseCase>();

            return services;
        }
    }
}
