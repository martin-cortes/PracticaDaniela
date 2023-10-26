using Domain.Models.Entities;
using Domain.Models.Interfaces.DrivenAdapters;
using Domain.Models.Interfaces.UseCase;
using Helpers.Commons.Exceptions;
using Helpers.ObjectsUtils.Extension;
using Microsoft.Extensions.Logging;

namespace Domain.UseCase
{
    public class PersonasUseCase : IPersonasUseCase
    {
        private readonly ILogger<PersonasUseCase> _logger;
        private readonly IPersonasAdapter _personasAdapter;

        public PersonasUseCase(ILogger<PersonasUseCase> logger, IPersonasAdapter personasAdapter)
        {
            _logger = logger;
            _personasAdapter = personasAdapter;
        }

        public async Task<PersonaResponse> GetPersona(string cedula)
        {
            PersonaResponse personaResponse = await _personasAdapter.Get(cedula) ??
                throw new BusinessException( TypeBusinessException.ErrorConsulta);

            return personaResponse;
        }

        public async Task<IEnumerable<PersonaResponse>> GetListPersona()
        {
            IEnumerable<PersonaResponse> personaResponses = await _personasAdapter.GetList()??
                throw new BusinessException(TypeBusinessException.ErrorConsulta);

            return personaResponses;
        }

        public async Task Post(PersonaRequest personaRequest)
        {
            await _personasAdapter.Post(personaRequest);
        }

        public async Task Update(PersonaRequest personaRequest)
        {
            await _personasAdapter.Update(personaRequest);
        }

        public async Task Delete(string cedula)
        {
            await _personasAdapter.Delete(cedula);
        }
    }
}
