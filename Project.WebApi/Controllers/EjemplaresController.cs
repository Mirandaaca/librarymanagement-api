using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs.Infraestructure.Persistence.Ejemplar;
using Project.Application.Interfaces.Persistence;
using Project.Application.Wrappers;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EjemplaresController : ControllerBase
    {
        private readonly IEjemplarRepository _repository;
        public EjemplaresController(IEjemplarRepository repository)
        {
            _repository = repository;
        }
        [HttpPost("CrearEjemplar")]
        public async Task<IActionResult> CrearEjemplar([FromBody] CreateEjemplarDTO ejemplar)
        {
            try
            {
                await _repository.CrearEjemplar(ejemplar);
                return Ok(new Response<bool>(message: "Ejemplar guardado exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al guardar el ejemplar", succeded: false));
            }
        }
        [HttpPut("ActualizarEjemplar")]
        public async Task<IActionResult> ActualizarEjemplar([FromBody] UpdateEjemplarDTO ejemplar)
        {
            try
            {
                await _repository.ActualizarEjemplar(ejemplar);
                return Ok(new Response<bool>(message: "Ejemplar actualizado exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al actualizar el ejemplar", succeded: false));
            }
        }
        [HttpDelete("EliminarEjemplar")]
        public async Task<IActionResult> EliminarEjemplar(int id)
        {
            try
            {
                await _repository.EliminarEjemplar(id);
                return Ok(new Response<bool>(message: "Ejemplar eliminado exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al eliminar el ejemplar", succeded: false));
            }
        }
        [HttpGet("ObtenerEjemplares")]
        public async Task<IActionResult> ObtenerEjemplares()
        {
            try
            {
                List<ReadEjemplarDTO> listaEjemplares = await _repository.ObtenerEjemplares();
                return Ok(new Response<List<ReadEjemplarDTO>>(data: listaEjemplares));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al obtener los ejemplares", succeded: false));
            }
        }
        [HttpGet("ObtenerEjemplarPorId")]
        public async Task<IActionResult> ObtenerEjemplarPorId(int id)
        {
            try
            {
                ReadEjemplarDTO ejemplar = await _repository.ObtenerEjemplarPorId(id);
                return Ok(new Response<ReadEjemplarDTO>(data: ejemplar));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al obtener el ejemplar", succeded: false));
            }
        }
        [HttpGet("ObtenerInformacionCompletaDeEjemplares")]
        public async Task<IActionResult> ObtenerInformacionCompletaDeEjemplares()
        {
            try
            {
                List<ReadEjemplarCompleteInformationDTO> listaEjemplares = await _repository.ObtenerInformacionCompletaDeEjemplares();
                return Ok(new Response<List<ReadEjemplarCompleteInformationDTO>>(data: listaEjemplares));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al obtener la información completa de los ejemplares", succeded: false));
            }
        }
        [HttpGet("ObtenerInformacionCompletaDeEjemplarPorId")]
        public async Task<IActionResult> ObtenerInformacionCompletaDeEjemplarPorId(int id)
        {
            try
            {
                ReadEjemplarCompleteInformationDTO ejemplar = await _repository.ObtenerInformacionCompletaDeEjemplarPorId(id);
                return Ok(new Response<ReadEjemplarCompleteInformationDTO>(data: ejemplar));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al obtener la información completa del ejemplar", succeded: false));
            }
        }
        [HttpGet("ObtenerInformacionParaBusquedaPorEjemplares")]
        public async Task<IActionResult> ObtenerInformacionParaBusquedaPorEjemplares()
        {
            try
            {
                List<ReadInformacionParaBuscarEjemplaresDTO> listaInformacion = await _repository.ObtenerInformacionParaBusquedaPorEjemplares();
                return Ok(new Response<List<ReadInformacionParaBuscarEjemplaresDTO>>(data: listaInformacion));
            }
            catch
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al obtener la información para la búsqueda de ejemplares", succeded: false));
            }
        }
        [HttpGet("VerificarEjemplarEnPrestamoActivo")]
        public async Task<IActionResult> VerificarEjemplarEnPrestamoActivo(int idEjemplar)
        {
            try
            {
                VerificarEjemplarEnPrestamoActivoResponseDTO response = await _repository.VerificarEjemplarEnPrestamoActivo(idEjemplar);
                return Ok(new Response<VerificarEjemplarEnPrestamoActivoResponseDTO>(data: response));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al verificar el ejemplar en prestamo activo", succeded: false));
            }
        }

    }
}
