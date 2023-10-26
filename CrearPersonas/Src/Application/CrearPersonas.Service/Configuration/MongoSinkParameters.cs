namespace CrearPersonas.Service.Configuration
{
    public class MongoSinkParameters
    {
        public string MinimumLevel { get; set; }

        public string ConexionMongo { get; set; }

        public string CollectionName { get; set; }

        public string DatabaseName { get; set; }

        public int DocumentExpiration { get; set; }
    }
}