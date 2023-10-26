using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace DrivenAdapters.Mongo.Entities
{
    public class BusinessParameter
    {
        public string Nombre { get; set; }

        public object Valor { get; set; }

        public string Descripcion { get; set; }

        public string Tipo { get; set; }
        public DateTime FechaModificacion { get; set; }

        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        public object Id { get; set; }
    }
}
