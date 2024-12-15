
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs.Infraestructure.Persistence.Origen;
using Project.Application.Interfaces.Persistence;
using Project.Application.Wrappers;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrigenesController : ControllerBase
    {
        private readonly IOrigenRepository _repository;
        public OrigenesController(IOrigenRepository repository)
        {
            _repository = repository;
        }
        
        [HttpPost("CrearOrigen")]
        public async Task<IActionResult> CrearOrigen(CreateOrigenDTO origen)
        {
            try
            {
                await _repository.CrearOrigen(origen);
                return Ok(new Response<bool>(message: "Origen guardado exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al guardar el origen", succeded: false));
            }
        }
        [HttpPut("ActualizarOrigen")]
        public async Task<IActionResult> ActualizarOrigen(UpdateOrigenDTO origen)
        {
            try
            {
                await _repository.ActualizarOrigen(origen);
                return Ok(new Response<bool>(message: "Origen actualizado exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al actualizar el origen", succeded: false));
            }
        }
        [HttpDelete("EliminarOrigen")]
        public async Task<IActionResult> EliminarOrigen(int id)
        {
            try
            {
                await _repository.EliminarOrigen(id);
                return Ok(new Response<bool>(message: "Origen eliminado exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al eliminar el origen", succeded: false));
            }
        }
        [HttpGet("ObtenerOrigenes")]
        public async Task<IActionResult> ObtenerOrigenes()
        {
            try
            {
                List<ReadOrigenDTO> origenes = await _repository.ObtenerOrigenes();
                return Ok(new Response<List<ReadOrigenDTO>>(data: origenes));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al obtener los origenes", succeded: false));
            }
        }
        [HttpGet("ObtenerOrigenPorId")]
        public async Task<IActionResult> ObtenerOrigenPorId(int id)
        {
            try
            {
                ReadOrigenDTO origen = await _repository.ObtenerOrigenPorId(id);
                return Ok(new Response<ReadOrigenDTO>(data: origen));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al obtener el origen", succeded: false));
            }
        }
    }
}
