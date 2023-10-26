using AutoMapper;
using Domain.Models.Entities;
using Domain.Models.Interfaces.DrivenAdapters;
using DrivenAdapters.Mongo.ContextMongo;
using DrivenAdapters.Mongo.Entities;
using MongoDB.Driver;

namespace DrivenAdapters.Mongo.Adapters
{
    public class PersonasAdapter : IPersonasAdapter
    {
        private readonly IMapper _mapper;
        private readonly IContext _context;

        public PersonasAdapter(IMapper mapper, IContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<PersonaResponse> Get(string cedula)
        {
            PersonasEntities persona = await _context.Persona.Find(_ => _.Cedula == cedula).FirstOrDefaultAsync();

            return _mapper.Map<PersonaResponse>(persona);
        }

        public async Task<IEnumerable<PersonaResponse>> GetList()
        {
            IEnumerable<PersonasEntities> personasList = await _context.Persona.Find(_ => true).ToListAsync();

            return _mapper.Map<IEnumerable<PersonaResponse>>(personasList);    
        }

        public async Task Post(PersonaRequest request)
        {
            PersonasEntities persona = _mapper.Map<PersonasEntities>(request);

            await _context.Persona.InsertOneAsync(persona);
        }

        public async Task Update(PersonaRequest request)
        {
            PersonasEntities persona = _mapper.Map<PersonasEntities>(request);

            await _context.Persona.ReplaceOneAsync(_ => _.Cedula == request.Cedula, persona);
        }

        public async Task Delete(string cedula)
        {
            await _context.Persona.DeleteOneAsync(_ => _.Cedula == cedula);
        }
    }
}