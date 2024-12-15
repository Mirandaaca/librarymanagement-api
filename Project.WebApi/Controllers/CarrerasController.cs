using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs.Infraestructure.Persistence.Carrera;
using Project.Application.Interfaces.Persistence;
using Project.Application.Wrappers;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrerasController : ControllerBase
    {
        private readonly ICarreraRepository _carreraRepository;
        public CarrerasController(ICarreraRepository carreraRepository)
        {
            _carreraRepository = carreraRepository;
        }
        [HttpPost("CrearCarrera")]
        public async Task<IActionResult> CrearCarrera(CreateCarreraDTO carrera)
        {
            try
            {
                await _carreraRepository.CrearCarrera(carrera);
                return Ok(new Response<bool>(message:"Carrera creada exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Error al crear la carrera", succeded: false));
            }
        }
        [HttpPut("ActualizarCarrera")]
        public async Task<IActionResult> ActualizarCarrera(UpdateCarreraDTO carrera)
        {
            try
            {
                await _carreraRepository.ActualizarCarrera(carrera);
                return Ok(new Response<bool>(message: "Carrera actualizada exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Error al actualizar la carrera", succeded: false));
            }
        }
        [HttpDelete("EliminarCarrera")]
        public async Task<IActionResult> EliminarCarrera(int id)
        {
            try
            {
                await _carreraRepository.EliminarCarrera(id);
                return Ok(new Response<bool>(message: "Carrera eliminada exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Error al eliminar la carrera", succeded: false));
            }
        }
        [HttpGet("ObtenerCarreras")]
        public async Task<IActionResult> ObtenerCarreras()
        {
            try
            {
                List<ReadCarreraDTO> carreras = await _carreraRepository.ObtenerCarreras();
                return Ok(new Response<List<ReadCarreraDTO>>(data: carreras, message: "Carreras obtenidas exitosamente"));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Error al obtener las carreras", succeded: false));
            }
        }
        [HttpGet("ObtenerCarreraPorId")]
        public async Task<IActionResult> ObtenerCarreraPorId(int id)
        {
            try
            {
                ReadCarreraDTO carrera = await _carreraRepository.ObtenerCarreraPorId(id);
                return Ok(new Response<ReadCarreraDTO>(data: carrera, message: "Carrera obtenida exitosamente"));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Error al obtener la carrera", succeded: false));
            }
        }
    }
}
