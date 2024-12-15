using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs.Infraestructure.Persistence.Autor;
using Project.Application.DTOs.Infraestructure.Persistence.Libro;
using Project.Application.DTOs.Infraestructure.Persistence.Tema;
using Project.Application.Interfaces.Persistence;
using Project.Application.Wrappers;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly ILibroRepository _libroRepository;
        public LibrosController(ILibroRepository libroRepository)
        {
            _libroRepository = libroRepository;
        }
        [HttpPost("CrearLibro")]
        public async Task<IActionResult> CrearLibro(CreateLibroDTO libro)
        {
            try
            {
                await _libroRepository.CrearLibro(libro);
                return Ok(new Response<bool>(message: "Libro creado exitosamente", succeded: true));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrio un error al crear el libro", succeded: false));
            }
        }
        [HttpPut("ActualizarLibro")]
        public async Task<IActionResult> ActualizarLibro(UpdateLibroDTO libro)
        {
            try
            {
                await _libroRepository.ActualizarLibro(libro);
                return Ok(new Response<bool>(message: "Libro actualizado exitosamente", succeded: true));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrio un error al actualizar el libro", succeded: false));
            }
        }
        [HttpDelete("EliminarLibro")]
        public async Task<IActionResult> EliminarLibro(int id)
        {
            try
            {
                await _libroRepository.EliminarLibro(id);
                return Ok(new Response<bool>(message: "Libro eliminado exitosamente", succeded: true));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrio un error al eliminar el libro", succeded: false));
            }
        }
        [HttpGet("ObtenerInformacionCompletaDeUnLibroPorId")]
        public async Task<IActionResult> LeerInformacionCompletaDeUnLibroPorId(int id)
        {
            try
            {
                ReadLibroCompleteInformationDTO libro = await _libroRepository.LeerInformacionCompletaDeUnLibroPorId(id);
                return Ok(new Response<ReadLibroCompleteInformationDTO>(data: libro, message: "Informacion completa del libro obtenida exitosamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<ReadLibroCompleteInformationDTO>(message: "Ocurrio un error al obtener la informacion completa del libro", succeded: false));
            }
        }
        [HttpGet("ObtenerInformacionCompletaDeTodosLosLibros")]
        public async Task<IActionResult> LeerInformacionCompletaDeTodosLosLibros()
        {
            try
            {
                List<ReadLibroCompleteInformationDTO> libros = await _libroRepository.LeerInformacionCompletaDeTodosLosLibros();
                return Ok(new Response<List<ReadLibroCompleteInformationDTO>>(data: libros, message: "Informacion completa de los libros obtenida exitosamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<List<ReadLibroCompleteInformationDTO>>(message: "Ocurrio un error al obtener la informacion completa de los libros", succeded: false));
            }
        }
        [HttpGet("ObtenerLibros")]
        public async Task<IActionResult> ObtenerLibros()
        {
            try
            {
                List<ReadLibroDTO> libros = await _libroRepository.ObtenerLibros();
                return Ok(new Response<List<ReadLibroDTO>>(data: libros, message: "Libros obtenidos exitosamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<List<ReadLibroDTO>>(message: "Ocurrio un error al obtener los libros", succeded: false));
            }
        }
        [HttpGet("ObtenerLibroPorId")]
        public async Task<IActionResult> ObtenerLibroPorId(int id)
        {
            try
            {
                ReadLibroDTO libro = await _libroRepository.ObtenerLibroPorId(id);
                return Ok(new Response<ReadLibroDTO>(data: libro, message: "Libro obtenido exitosamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<ReadLibroDTO>(message: "Ocurrio un error al obtener el libro", succeded: false));
            }
        }
        [HttpGet("ObtenerAutoresDeUnLibroPorId")]
        public async Task<IActionResult> ObtenerAutoresDeUnLibroPorId(int id)
        {
            try
            {
                ReadAutoresxLibro autores = await _libroRepository.ObtenerAutoresDeUnLibroPorId(id);
                return Ok(new Response<ReadAutoresxLibro>(data: autores, message: "Autores del libro obtenidos exitosamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<ReadAutoresxLibro>(message: "Ocurrio un error al obtener los autores del libro", succeded: false));
            }
        }
        [HttpGet("ObtenerTemasDeUnLibroPorId")]
        public async Task<IActionResult> ObtenerTemasDeUnLibroPorId(int id)
        {
            try
            {
                ReadTemasxLibro temas = await _libroRepository.ObtenerTemasDeUnLibroPorId(id);
                return Ok(new Response<ReadTemasxLibro>(data: temas, message: "Temas del libro obtenidos exitosamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<ReadTemasxLibro>(message: "Ocurrio un error al obtener los temas del libro", succeded: false));
            }
        }
        [HttpPost("AgregarAutoresAUnLibro")]
        public async Task<IActionResult> AgregarAutoresAUnLibro(AddAutoresAUnLibro autores)
        {
            try
            {
                await _libroRepository.AgregarAutoresAUnLibro(autores);
                return Ok(new Response<bool>(message: "Autores agregados al libro exitosamente", succeded: true));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrio un error al agregar los autores al libro", succeded: false));
            }
        }
        [HttpPost("AgregarTemasAUnLibro")]
        public async Task<IActionResult> AgregarTemasAUnLibro(AddTemasAUnLibro temas)
        {
            try
            {
                await _libroRepository.AgregarTemasAUnLibro(temas);
                return Ok(new Response<bool>(message: "Temas agregados al libro exitosamente", succeded: true));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrio un error al agregar los temas al libro", succeded: false));
            }
        }
        [HttpGet("ObtenerAutoresNoRelacionadosPorLibro")]
        public async Task<IActionResult> ObtenerAutoresNoRelacionadosPorLibro(int id)
        {
            try
            {
                List<ReadAutorDTO> autores = await _libroRepository.ObtenerAutoresNoRelacionadosPorLibro(id);
                return Ok(new Response<List<ReadAutorDTO>>(data: autores, message: "Autores no relacionados con el libro obtenidos exitosamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<List<ReadAutorDTO>>(message: "Ocurrio un error al obtener los autores no relacionados con el libro", succeded: false));
            }
        }
        [HttpGet("ObtenerTemasNoRelacionadosPorLibro")]
        public async Task<IActionResult> ObtenerTemasNoRelacionadosPorLibro(int id)
        {
            try
            {
                List<ReadTemaDTO> temas = await _libroRepository.ObtenerTemasNoRelacionadosPorLibro(id);
                return Ok(new Response<List<ReadTemaDTO>>(data: temas, message: "Temas no relacionados con el libro obtenidos exitosamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<List<ReadTemaDTO>>(message: "Ocurrio un error al obtener los temas no relacionados con el libro", succeded: false));
            }
        }
        [HttpGet("ObtenerInformacionParaBusquedaDeLibrosDetallado")]
        public async Task<IActionResult> ObtenerInformacionParaBusquedaDeLibros()
        {
            try
            {
                List<ReadInformacionParaBuscarLibrosDetalladoDTO> informacion = await _libroRepository.ObtenerInformacionParaBusquedaDeLibrosDetallado();
                return Ok(new Response<List<ReadInformacionParaBuscarLibrosDetalladoDTO>>(data: informacion, message: "Informacion para busqueda de libros obtenida exitosamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<List<ReadInformacionParaBuscarLibrosDetalladoDTO>>(message: "Ocurrio un error al obtener la informacion para busqueda de libros", succeded: false));
            }
        }
        [HttpGet("ObtenerInformacionParaBusquedaDeLibrosSimple")]
        public async Task<IActionResult> ObtenerInformacionParaBusquedaDeLibrosSimple()
        {
            try
            {
                List<ReadInformacionParaBuscarLibrosSimpleDTO> informacion = await _libroRepository.ObtenerInformacionParaBusquedaDeLibrosSimple();
                return Ok(new Response<List<ReadInformacionParaBuscarLibrosSimpleDTO>>(data: informacion, message: "Informacion para busqueda de libros obtenida exitosamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<List<ReadInformacionParaBuscarLibrosSimpleDTO>>(message: "Ocurrio un error al obtener la informacion para busqueda de libros", succeded: false));
            }
        }
        [HttpGet("ObtenerInformacionCompletaDeTodosLosLibrosParaReporte")]
        public async Task<IActionResult> LeerInformacionCompletaDeTodosLosLibrosParaReporte()
        {
            try
            {
                List<ReadReporteLibrosDTO> libros = await _libroRepository.LeerInformacionCompletaDeTodosLosLibrosParaReporte();
                return Ok(new Response<List<ReadReporteLibrosDTO>>(data: libros, message: "Informacion completa de los libros para reporte obtenida exitosamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<List<ReadReporteLibrosDTO>>(message: "Ocurrio un error al obtener la informacion completa de los libros para reporte", succeded: false));
            }
        }
    }
}
