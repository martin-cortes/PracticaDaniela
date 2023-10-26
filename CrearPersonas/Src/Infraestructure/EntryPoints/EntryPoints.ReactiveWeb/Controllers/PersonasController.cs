using Domain.Models.Entities;
using Domain.Models.Interfaces.UseCase;
using Microsoft.AspNetCore.Mvc;

namespace EntryPoints.ReactiveWeb.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly IPersonasUseCase _personaUseCase;

        public PersonasController(IPersonasUseCase personasUseCase)
        {
            _personaUseCase = personasUseCase;
        }

        [HttpGet]
        public async Task<ActionResult<PersonaResponse>> Get(string cedula)
        {
            return Ok(await _personaUseCase.GetPersona(cedula));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonaResponse>>> GetList() =>
            Ok(await _personaUseCase.GetListPersona());

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PersonaRequest personaRequest)
        {
            await _personaUseCase.Post(personaRequest);

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] PersonaRequest personaRequest)
        {
            await _personaUseCase.Update(personaRequest);

            return Ok();
        }

        [HttpDelete]

        public async Task<ActionResult> Delete([FromBody] string cedula)
        {
            await _personaUseCase.Delete(cedula);

            return Ok();
        }
    }
}