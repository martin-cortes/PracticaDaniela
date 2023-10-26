using DrivenAdapters.Mongo.Entities;
using MongoDB.Driver;

namespace DrivenAdapters.Mongo.ContextMongo
{
    public interface IContext
    {
        IMongoCollection<PersonasEntities> Persona { get; }
    }
}