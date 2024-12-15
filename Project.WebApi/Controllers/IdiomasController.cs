using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs.Infraestructure.Persistence.Idioma;
using Project.Application.Interfaces.Persistence;
using Project.Application.Wrappers;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdiomasController : ControllerBase
    {
        private readonly IIdiomaRepository _repository;
        public IdiomasController(IIdiomaRepository repository)
        {
            _repository = repository;
        }
        [HttpPost("CrearIdioma")]
        public async Task<IActionResult> CrearIdioma(CreateIdiomaDTO idioma)
        {
            try
            {
                await _repository.CrearIdioma(idioma);
                return Ok(new Response<bool>(message: "Idioma creado exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al crear el Idioma", succeded: false));
            }
        }
        [HttpPut("ActualizarIdioma")]
        public async Task<IActionResult> ActualizarIdioma(UpdateIdiomaDTO idioma)
        {
            try
            {
                await _repository.ActualizarIdioma(idioma);
                return Ok(new Response<bool>(message: "Idioma actualizado exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al actualizar el Idioma", succeded: false));
            }
        }
        [HttpDelete("EliminarIdioma")]
        public async Task<IActionResult> EliminarIdioma(int id)
        {
            try
            {
                await _repository.ElimnarIdiomar(id);
                return Ok(new Response<bool>(message: "Idioma eliminado exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al eliminar el Idioma", succeded: false));
            }
        }
        [HttpGet("ObtenerIdiomas")]
        public async Task<IActionResult> ObtenerIdiomas()
        {
            try
            {
                var response = await _repository.ObtenerIdiomas();
                return Ok(new Response<List<ReadIdiomaDTO>>(data: response));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al obtener los Idiomas", succeded: false));
            }
        }
        [HttpGet("ObtenerIdiomaPorId")]
        public async Task<IActionResult> ObtenerIdiomaPorId(int id)
        {
            try
            {
                var response = await _repository.ObtenerIdiomaPorId(id);
                return Ok(new Response<ReadIdiomaDTO>(data: response));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al obtener el Idioma", succeded: false));
            }
        }
    }
}
