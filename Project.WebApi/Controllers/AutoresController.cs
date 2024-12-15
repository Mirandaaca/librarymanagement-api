using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs.Infraestructure.Persistence.Autor;
using Project.Application.Interfaces.Persistence;
using Project.Application.Wrappers;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly IAutorRepository _repository;
        public AutoresController(IAutorRepository repository)
        {
            _repository = repository;
        }
        [HttpPost("CrearAutor")]
        public async Task<IActionResult> CrearAutor(CreateAutorDTO autor)
        {
            try
            {
                await _repository.CrearAutor(autor);
                return Ok(new Response<ReadAutorDTO>(message: "Autor guardado exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al guardar al autor", succeded: false));
            }
        }
        [HttpPut("ActualizarAutor")]
        public async Task<IActionResult> ActualizarAutor(UpdateAutorDTO autor)
        {
            try
            {
                await _repository.ActualizarAutor(autor);
                return Ok(new Response<bool>(message: "Autor actualizado exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al actualizar al autor", succeded: false));
            }
        }
        [HttpDelete("EliminarAutor")]
        public async Task<IActionResult> EliminarAutor(int id)
        {
            try
            {
                await _repository.EliminarAutor(id);
                return Ok(new Response<bool>(message: "Autor eliminado exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al eliminar al autor", succeded: false));
            }
        }
        [HttpGet("ObtenerAutores")]
        public async Task<IActionResult> ObtenerAutores()
        {
            try
            {
                List<ReadAutorDTO> response = await _repository.ObtenerAutores();
                return Ok(new Response<List<ReadAutorDTO>>(data: response));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al obtener los autores", succeded: false));
            }
        }
        [HttpGet("ObtenerAutorPorId")]
        public async Task<IActionResult> ObtenerAutorPorId(int id)
        {
            try
            {
                ReadAutorDTO response = await _repository.ObtenerAutorPorId(id);
                return Ok(new Response<ReadAutorDTO>(data: response));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al obtener al autor", succeded: false));
            }
        }
    }
}
