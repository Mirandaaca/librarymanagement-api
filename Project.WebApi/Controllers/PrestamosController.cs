using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs.Infraestructure.Persistence.Ejemplar;
using Project.Application.DTOs.Infraestructure.Persistence.Prestamo;
using Project.Application.Interfaces.Persistence;
using Project.Application.Wrappers;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamosController : ControllerBase
    {
        private readonly IPrestamoRepository _repository;
        public PrestamosController(IPrestamoRepository repository)
        {
            _repository = repository;
        }
        [HttpPost("CrearPrestamo")]
        public async Task<IActionResult> CrearPrestamo(CreatePrestamoDTO prestamo)
        {
            try
            {
                await _repository.CrearPrestamo(prestamo);
                return Ok(new Response<bool>(message: "Prestamo creado exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<bool>(message: "Error al crear el prestamo", succeded: false));
            }
        }
        [HttpPut("ActualizarPrestamo")]
        public async Task<IActionResult> ActualizarPrestamo(UpdatePrestamoDTO prestamo)
        {
            try
            {
                await _repository.ActualizarPrestamo(prestamo);
                return Ok(new Response<bool>(message: "Prestamo actualizado exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<bool>(message: "Error al actualizar el prestamo", succeded: false));
            }
        }
        [HttpDelete("EliminarPrestamo")]
        public async Task<IActionResult> EliminarPrestamo(int id)
        {
            try
            {
                await _repository.EliminarPrestamo(id);
                return Ok(new Response<bool>(message: "Prestamo eliminado exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<bool>(message: "Error al eliminar el prestamo", succeded: false));
            }
        }
        [HttpGet("ObtenerPrestamosActivos")]
        public async Task<IActionResult> ObtenerPrestamosActivos()
        {
            try
            {
                var prestamos = await _repository.ObtenerPrestamosActivos();
                return Ok(new Response<List<ReadPrestamoActivosDTO>>(prestamos, message: "Prestamos activos obtenidos exitosamente"));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<bool>(message: "Error al obtener los prestamos activos", succeded: false));
            }
        }
        [HttpGet("ObtenerPrestamos")]
        public async Task<IActionResult> ObtenerPrestamos()
        {
            try
            {
                var prestamos = await _repository.ObtenerPrestamos();
                return Ok(new Response<List<ReadPrestamoDetalleDTO>>(prestamos, message: "Historial de prestamos obtenido exitosamente"));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<bool>(message: "Error al obtener el historial de prestamos", succeded: false));
            }
        }
        [HttpGet("ObtenerPrestamoPorId")]
        public async Task<IActionResult> ObtenerPrestamoPorId(int id)
        {
            try
            {
                var prestamo = await _repository.ObtenerPrestamoPorId(id);
                return Ok(new Response<ReadPrestamoDetallePorId>(prestamo, message: "Prestamo obtenido exitosamente"));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<bool>(message: "Error al obtener el prestamo", succeded: false));
            }
        }
        [HttpPost("AgregarEjemplaresAlPrestamo")]
        public async Task<IActionResult> AgregarEjemplaresAlPrestamo(AddEjemplaresToPrestamoDTO ejemplares)
        {
            try
            {
                await _repository.AgregarEjemplaresAlPrestamo(ejemplares);
                return Ok(new Response<bool>(message: "Ejemplares agregados al prestamo exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<bool>(message: "Error al agregar los ejemplares al prestamo", succeded: false));
            }
        }
        [HttpPost("RegistrarDevolucion")]
        public async Task<IActionResult> RegistrarDevolucion(int idPrestamoDetalle)
        {
            try
            {
                await _repository.RegistrarDevolucion(idPrestamoDetalle);
                return Ok(new Response<bool>(message: "Devolucion registrada exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<bool>(message: "Error al registrar la devolucion", succeded: false));
            }
        }
        [HttpGet("ObtenerDetallePrestamo")]
        public async Task<IActionResult> ObtenerDetallePrestamo(int idPrestamo)
        {
            try
            {
                var detalle = await _repository.ObtenerDetallePrestamo(idPrestamo);
                return Ok(new Response<ReadPrestamoDetalleDTO>(detalle, message: "Detalle del prestamo obtenido exitosamente"));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<bool>(message: "Error al obtener el detalle del prestamo", succeded: false));
            }
        }
        [HttpGet("ObtenerEjemplaresDeUnPrestamo")]
        public async Task<IActionResult> ObtenerEjemplaresDeUnPrestamo(int idPrestamo)
        {
            try
            {
                var ejemplares = await _repository.ObtenerEjemplaresDeUnPrestamo(idPrestamo);
                return Ok(new Response<List<ReadEjemplarParaPrestamoDTO>>(ejemplares, message: "Ejemplares del prestamo obtenidos exitosamente"));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<bool>(message: "Error al obtener los ejemplares del prestamo", succeded: false));
            }
        }
        [HttpPost("MarcarPrestamoComoDevuelto")]
        public async Task<IActionResult> MarcarPrestamoComoDevuelto(int idPrestamo)
        {
            try
            {
                await _repository.MarcarPrestamoComoDevuelto(idPrestamo);
                return Ok(new Response<bool>(message: "Prestamo marcado como devuelto exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<bool>(message: "Error al marcar el prestamo como devuelto", succeded: false));
            }
        }
        [HttpGet("VerificarPrestamosActivos")]
        public async Task<IActionResult> VerificarPrestamosActivos(int idPersona)
        {
            try
            {
                var response = await _repository.VerificarPrestamosActivos(idPersona);
                return Ok(new Response<VerificarPrestamosActivosResponseDTO>(response, message: "Prestamos activos verificados exitosamente"));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<bool>(message: "Error al verificar los prestamos activos", succeded: false));
            }
        }
        [HttpGet("ObtenerEjemplaresNoAsociadosAUnPrestamo")]
        public async Task<IActionResult> ObtenerEjemplaresNoAsociadosAUnPrestamo()
        {
            try
            {
                var ejemplares = await _repository.ObtenerEjemplaresNoAsociadosAUnPrestamo();
                return Ok(new Response<List<ReadEjemplarParaPrestamoDTO>>(ejemplares, message: "Ejemplares no asociados a un prestamo obtenidos exitosamente"));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<bool>(message: "Error al obtener los ejemplares no asociados a un prestamo", succeded: false));
            }
        }
    }
}
