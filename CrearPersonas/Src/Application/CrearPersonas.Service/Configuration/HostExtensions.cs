using Helpers.Commons.Exceptions;
using Helpers.Commons.MongoConfiguration;
using Helpers.ObjectsUtils.Setting;
using MongoDB.Driver;
using Serilog;
using Serilog.Events;

namespace CrearPersonas.Service.Configuration
{
    public static class HostExtensions
    {
        public static IHostBuilder ConfigureSerilog(this IHostBuilder hostBuilder, MongoSinkParameters mongoSinkParameters) =>
           hostBuilder.UseSerilog((context, loggerConfiguration) =>
           {
               loggerConfiguration
               .ReadFrom.Configuration(context.Configuration)
               .WriteTo.Console()
               .WriteTo.MongoDBBson(cfg =>
               {
                   cfg.SetExpireTTL(TimeSpan.FromDays(mongoSinkParameters.DocumentExpiration));
                   cfg.SetMongoDatabase(ConfigureMongoSink(mongoSinkParameters.ConexionMongo, mongoSinkParameters.DatabaseName));
                   cfg.SetCollectionName(mongoSinkParameters.CollectionName);
               }, ParseEnum<LogEventLevel>(mongoSinkParameters.MinimumLevel))

               .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
               .Enrich.WithProperty("ApplicationName", context.HostingEnvironment.ApplicationName)
               .Enrich.FromLogContext();
           });


        public static IConfigurationBuilder AddMongoProvider(this ConfigurationManager configuration,
                                                           string configurationName,
                                                           string connectionString)
        {
            MongoConfigurationOptions settings = new();

            configuration.GetSection(configurationName).Bind(settings);

            settings.ConnectionString = connectionString;

            configuration.AddMongoConfiguration(opt =>
            {
                opt.ConnectionString = settings.ConnectionString;
                opt.CollectionName = settings.CollectionName;
                opt.DatabaseName = settings.DatabaseName;
                opt.ReloadOnChange = settings.ReloadOnChange;

            });

            return configuration;
        }

        #region Métodos Privados

        private static IMongoDatabase ConfigureMongoSink(string cadenaConexion, string nombreBaseDeDatos)
        {
            MongoClient mongoClient = new(cadenaConexion);
            return mongoClient.GetDatabase(nombreBaseDeDatos);
        }

        private static T ParseEnum<T>(string valorEnum)
        {
            if (string.IsNullOrEmpty(valorEnum))
                throw new BusinessException(TypeBusinessException.MinimumLevelEmpty, nameof(valorEnum));

            return (T)System.Enum.Parse(typeof(T), valorEnum, true);
        }

        private static IConfigurationBuilder AddMongoConfiguration(this IConfigurationBuilder builder,
                                                                   Action<MongoConfigurationOptions> options)
        {
            if (options is null)
                throw new ArgumentNullException(nameof(options));

            MongoConfigurationOptions mongoAppSettingConfiguration = new();

            options(mongoAppSettingConfiguration);

            builder.Add(new MongoConfigurationSource(mongoAppSettingConfiguration));

            return builder;
        }

        #endregion Métodos Privados
    }
}
