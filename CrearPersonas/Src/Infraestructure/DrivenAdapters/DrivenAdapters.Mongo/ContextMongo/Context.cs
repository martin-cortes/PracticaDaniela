using DrivenAdapters.Mongo.Entities;
using MongoDB.Driver;

namespace DrivenAdapters.Mongo.ContextMongo
{
    public class Context : IContext
    {
        private readonly IMongoDatabase _database;
        private readonly string _collectionName;

        public Context(string connectionString, string dataBase, string collectionName)
        {
            MongoClient mongoClient = new(connectionString);
            _database = mongoClient.GetDatabase(dataBase);
            _collectionName = collectionName;
        }

        public IMongoCollection<PersonasEntities> Persona => _database.GetCollection<PersonasEntities>(_collectionName);
    }
}
