using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs.Infraestructure.Persistence.Tema;
using Project.Application.Interfaces.Persistence;
using Project.Application.Wrappers;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemasController : ControllerBase
    {
        private readonly ITemaRepository _repository;
        public TemasController(ITemaRepository repository)
        {
            _repository = repository;
        }
        [HttpPost("CrearTema")]
        public async Task<IActionResult> CrearTema (CreateTemaDTO tema)
        {
            try
            {
                await _repository.CrearTema(tema);
                return Ok(new Response<bool>(message: "Tema creado exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al crear el Tema", succeded: false));
            }
        }
        [HttpPut("ActualizarTema")]
        public async Task<IActionResult> ActualizarTema(UpdateTemaDTO tema)
        {
            try
            {
                await _repository.ActualizarTema(tema);
                return Ok(new Response<bool>(message: "Tema actualizado exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al actualizar el Tema", succeded: false));
            }
        }
        [HttpDelete("EliminarTema")]
        public async Task<IActionResult> EliminarTema(int id)
        {
            try
            {
                await _repository.EliminarTema(id);
                return Ok(new Response<bool>(message: "Tema eliminado exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al eliminar el Tema", succeded: false));
            }
        }
        [HttpGet("ObtenerTemas")]
        public async Task<IActionResult> ObtenerTemas()
        {
            try
            {
                var response = await _repository.ObtenerTemas();
                return Ok(new Response<List<ReadTemaDTO>>(data: response));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al obtener los temas", succeded: false));
            }
        }
        [HttpGet("ObtenerTemaPorId")]
        public async Task<IActionResult> ObtenerTemaPorId(int id)
        {
            try
            {
                var response = await _repository.ObtenerTemaPorId(id);
                return Ok(new Response<ReadTemaDTO>(data: response));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al obtener el tema", succeded: false));
            }
        }
    }
}
