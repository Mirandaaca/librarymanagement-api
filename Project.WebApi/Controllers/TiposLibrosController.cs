using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs.Infraestructure.Persistence.TipoLibro;
using Project.Application.Interfaces.Persistence;
using Project.Application.Wrappers;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposLibrosController : ControllerBase
    {
        private readonly ITipoLibroRepository _tipoLibroRepository;
        public TiposLibrosController(ITipoLibroRepository tipoLibroRepository)
        {
            _tipoLibroRepository = tipoLibroRepository;
        }
        [HttpPost("CrearTipoLibro")]
        public async Task<IActionResult> CrearLibro(CreateTipoLibroDTO tipoLibro)
        {
            try
            {
                await _tipoLibroRepository.CrearTipoLibro(tipoLibro);
                return Ok(new Response<bool>(message: "Libro creado exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Error al crear el libro", succeded: false));
            }
        }
        [HttpPut("ActualizarTipoLibro")]
        public async Task<IActionResult> ActualizarLibro(UpdateTipoLibroDTO tipoLibro)
        {
            try
            {
                await _tipoLibroRepository.ActualizarTipoLibro(tipoLibro);
                return Ok(new Response<bool>(message: "Libro actualizado exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Error al actualizar el libro", succeded: false));
            }
        }
        [HttpDelete("EliminarTipoLibro")]
        public async Task<IActionResult> EliminarLibro(int id)
        {
            try
            {
                await _tipoLibroRepository.EliminarTipoLibro(id);
                return Ok(new Response<bool>(message: "Libro eliminado exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Error al eliminar el libro", succeded: false));
            }
        }
        [HttpGet("ObtenerTiposLibros")]
        public async Task<IActionResult> ObtenerTiposLibros()
        {
            try
            {
                List<ReadTipoLibroDTO> tiposLibros = await _tipoLibroRepository.ObtenerTiposLibros();
                return Ok(new Response<List<ReadTipoLibroDTO>>(data: tiposLibros, message: "Tipos de libros obtenidos exitosamente"));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Error al obtener los tipos de libros", succeded: false));
            }
        }
        [HttpGet("ObtenerTipoLibroPorId")]
        public async Task<IActionResult> ObtenerTipoLibroPorId(int id)
        {
            try
            {
                ReadTipoLibroDTO tipoLibro = await _tipoLibroRepository.ObtenerTipoLibroPorId(id);
                return Ok(new Response<ReadTipoLibroDTO>(data: tipoLibro, message: "Tipo de libro obtenido exitosamente"));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Error al obtener el tipo de libro", succeded: false));
            }
        }
    }
}
