using Domain.Models.Entities;

namespace Domain.Models.Interfaces.DrivenAdapters
{
    public interface IPersonasAdapter
    {
        Task<PersonaResponse> Get(string cedula);

        Task<IEnumerable<PersonaResponse>> GetList();

        Task Post(PersonaRequest request);

        Task Update(PersonaRequest request);

        Task Delete(string cedula);
    }
}
