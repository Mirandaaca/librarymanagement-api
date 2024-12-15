using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs.Infraestructure.Persistence.Editorial;
using Project.Application.Interfaces.Persistence;
using Project.Application.Wrappers;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditorialesController : ControllerBase
    {
        private readonly IEditorialRepository _repository;
        public EditorialesController(IEditorialRepository repository)
        {
            _repository = repository;
        }
        [HttpPost("CrearEditorial")]
        public async Task<IActionResult> CrearEditorial(CreateEditorialDTO editorial)
        {
            try
            {
                await _repository.CrearEditorial(editorial);
                return Ok(new Response<bool>(message: "Editorial creado exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al crear la Editorial", succeded: false));
            }
        }
        [HttpPut("ActualizarEditorial")]
        public async Task<IActionResult> ActualizarEditorial(UpdateEditorialDTO editorial)
        {
            try
            {
                await _repository.ActualizarEditorial(editorial);
                return Ok(new Response<bool>(message: "Editorial actualizado exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al actualizar la Editorial", succeded: false));
            }
        }
        [HttpDelete("EliminarEditorial")]
        public async Task<IActionResult> EliminarEditorial(int id)
        {
            try
            {
                await _repository.EliminarEditorial(id);
                return Ok(new Response<bool>(message: "Editorial eliminado exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al eliminar la Editorial", succeded: false));
            }
        }
        [HttpGet("ObtenerEditoriales")]
        public async Task<IActionResult> ObtenerEditoriales()
        {
            try
            {
                var response = await _repository.ObtenerEditoriales();
                return Ok(new Response<List<ReadEditorialDTO>>(data: response));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al obtener las editoriales", succeded: false));
            }
        }
        [HttpGet("ObtenerEditorialPorId")]
        public async Task<IActionResult> ObtenerEditorialPorId(int id)
        {
            try
            {
                var response = await _repository.ObtenerEditorialPorId(id);
                return Ok(new Response<ReadEditorialDTO>(data: response));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al obtener las editoriales", succeded: false));
            }
        }
    }
}
