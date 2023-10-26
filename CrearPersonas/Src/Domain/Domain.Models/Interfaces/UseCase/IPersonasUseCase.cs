using Domain.Models.Entities;

namespace Domain.Models.Interfaces.UseCase
{
    public interface IPersonasUseCase
    {
        Task<PersonaResponse> GetPersona(string cedula);

        Task<IEnumerable<PersonaResponse>> GetListPersona();

        Task Post(PersonaRequest personaRequest);

        Task Update(PersonaRequest personaRequest);

        Task Delete(string cedula);
    }
}
