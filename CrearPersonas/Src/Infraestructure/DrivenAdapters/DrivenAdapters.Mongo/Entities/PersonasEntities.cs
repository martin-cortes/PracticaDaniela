using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DrivenAdapters.Mongo.Entities
{
    public class PersonasEntities
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("cedula")]
        public string Cedula { get; set; }

        [BsonElement("nombre")]
        public string Nombre { get; set; }

        [BsonElement("apellido")]
        public string Apellido { get; set; }

        [BsonElement("direccion")]
        public string Direccion { get; set; }
    }
}
