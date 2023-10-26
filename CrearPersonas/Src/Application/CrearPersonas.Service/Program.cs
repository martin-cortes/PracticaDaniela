using CrearPersonas.Service.Configuration;
using DrivenAdapters.Mongo.MongoCP;
using Helpers.ObjectsUtils.Setting;

var builder = WebApplication.CreateBuilder(args);
IWebHostEnvironment environment = builder.Environment;
IConfiguration configuration = builder.Configuration;

#region Host Configuration

builder.Host.ConfigureAppConfiguration((context, config) =>
{
    config
   .AddJsonFile("config/appsettings.json", optional: true, reloadOnChange: true)
   .AddJsonFile($"appsettings.{environment.ApplicationName}.json", optional: true, reloadOnChange: true)
   .AddEnvironmentVariables()
   .Build();
});

builder.Host.ConfigureSerilog(new MongoSinkParameters()
{
    ConexionMongo = configuration.GetSection("AppSettings:ConnectionStringMongo").Value,
    DatabaseName = configuration.GetSection("AppSettings:Database").Value,
    CollectionName = configuration.GetSection("MongoSink:CollectionName").Value,
    MinimumLevel = configuration.GetSection("MongoSink:MinimumLevel").Value,
    DocumentExpiration = int.Parse(configuration.GetSection("MongoSink:DocumentExpiration").Value)
});

#endregion Host Configuration

string mongoConnectionString = configuration.GetSection("AppSettings:ConnectionStringMongo").Value;

builder.Configuration.AddMongoProvider(nameof(MongoConfigurationProvider), mongoConnectionString);

builder.Services.Configure<AppSettings>(builder.Configuration.GetRequiredSection(nameof(AppSettings)));

AppSettings appSettings = builder.Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();

// Add services to the container.

builder.Services
    .RegisterServices()
    .RegisterAutoMapper()
    .RegisterMongo(mongoConnectionString, configuration.GetSection("AppSettings:Database").Value, "RegistroPersonas");


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

#region Logging

Microsoft.Extensions.Logging.ILogger logger = app.Logger;
IHostApplicationLifetime lifetime = app.Lifetime;

lifetime.ApplicationStarted.Register(() =>
{
    logger.LogInformation("===============PROGRAM=============");
    logger.LogInformation($"App = {environment.ApplicationName}");
    logger.LogInformation($"HealthChecks = {appSettings.HealthChecksEndPoint}");
    logger.LogInformation($"Environment = {builder.Environment.EnvironmentName}");
    logger.LogInformation($"Date = {DateTime.UtcNow.ToLocalTime()}");
    logger.LogInformation("=============END PROGRAM==========");
});

#endregion Logging

app.Run();
