namespace Helpers.ObjectsUtils.Setting
{
    public class MongoConfigurationOptions
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public string CollectionName { get; set; }

        public bool ReloadOnChange { get; set; }
    }
}
