using AutoMapper;
using Domain.Models.Entities;
using DrivenAdapters.Mongo.Entities;

namespace DrivenAdapters.Mongo.AutoMapper
{
    public class MongoProfile : Profile
    {
        public MongoProfile()
        {
            CreateMap<PersonaRequest, PersonasEntities>();

            CreateMap<PersonasEntities, PersonaResponse>();
        }
    }
}
