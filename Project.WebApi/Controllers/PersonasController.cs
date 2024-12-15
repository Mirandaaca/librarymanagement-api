using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs.Infraestructure.Persistence.Persona;
using Project.Application.Interfaces.Persistence;
using Project.Application.Wrappers;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly IPersonaRepository _repository;
        public PersonasController(IPersonaRepository repository)
        {
            _repository = repository;
        }
        [HttpPost("CrearPersona")]
        public async Task<IActionResult> CrearPersona([FromBody] CreatePersonaDTO persona)
        {
            try
            {
                await _repository.CrearPersona(persona);
                return Ok(new Response<bool>(message: "Persona guardada exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al guardar la persona", succeded: false));
            }
        }
        [HttpPut("ActualizarPersona")]
        public async Task<IActionResult> ActualizarPersona([FromBody] UpdatePersonaDTO persona)
        {
            try
            {
                await _repository.ActualizarPersona(persona);
                return Ok(new Response<bool>(message: "Persona actualizada exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al actualizar la persona", succeded: false));
            }
        }
        [HttpDelete("EliminarPersona")]
        public async Task<IActionResult> EliminarPersona(int id)
        {
            try
            {
                await _repository.EliminarPersona(id);
                return Ok(new Response<bool>(message: "Persona eliminada exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al eliminar a la persona", succeded: false));
            }
        }
        [HttpGet("ObtenerPersonas")]
        public async Task<IActionResult> ObtenerPersonas()
        {
            try
            {
                List<ReadPersonaDTO> listaPersonas = await _repository.ObtenerPersonas();
                return Ok(new Response<List<ReadPersonaDTO>>(data: listaPersonas, message: "Personas obtenidas exitosamente"));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al obtener las personas", succeded: false));
            }
        }
        [HttpGet("ObtenerPersonaPorId")]
        public async Task<IActionResult> ObtenerPersonaPorId(int id)
        {
            try
            {
                ReadPersonaDTO persona = await _repository.ObtenerPersonaPorId(id);
                return Ok(new Response<ReadPersonaDTO>(data: persona, message: "Persona obtenida exitosamente"));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al obtener la persona", succeded: false));
            }
        }
    }
}
