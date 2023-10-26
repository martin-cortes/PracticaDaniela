using DrivenAdapters.Mongo.MongoCP;
using Helpers.ObjectsUtils.Setting;
using Microsoft.Extensions.Configuration;

namespace Helpers.Commons.MongoConfiguration
{
    public class MongoConfigurationSource : IConfigurationSource
    {
        private readonly MongoConfigurationOptions _options;
        public MongoConfigurationSource(MongoConfigurationOptions options)
        {
            _options = options;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder) =>
            new MongoConfigurationProvider(_options.ConnectionString,
                                           _options.DatabaseName,
                                           _options.CollectionName,
                                           _options.ReloadOnChange);




    }
}
