using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs.Infraestructure.Identity.Materia;
using Project.Application.Interfaces.Persistence;
using Project.Application.Wrappers;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriasController : ControllerBase
    {
        private readonly IMateriaRepository _materiaRepository;
        public MateriasController(IMateriaRepository materiaRepository)
        {
            _materiaRepository = materiaRepository;
        }
        [HttpPost("CrearMateria")]
        public async Task<IActionResult> CrearMateria(CreateMateriaDTO materia)
        {
            try
            {
                await _materiaRepository.CrearMateria(materia);
                return Ok(new Response<bool>(message:"Materia creada exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Error al crear la materia", succeded: false));
            }
        }
        [HttpPut("ActualizarMateria")]
        public async Task<IActionResult> ActualizarMateria(UpdateMateriaDTO materia)
        {
            try
            {
                await _materiaRepository.ActualizarMateria(materia);
                return Ok(new Response<bool>(message: "Materia actualizada exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Error al actualizar la materia", succeded: false));
            }
        }
        [HttpDelete("EliminarMateria")]
        public async Task<IActionResult> EliminarMateria(int id)
        {
            try
            {
                await _materiaRepository.EliminarMateria(id);
                return Ok(new Response<bool>(message: "Materia eliminada exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Error al eliminar la materia", succeded: false));
            }
        }
        [HttpGet("ObtenerMaterias")]
        public async Task<IActionResult> ObtenerMaterias()
        {
            try
            {
                List<ReadMateriaDTO> response = await _materiaRepository.ObtenerMaterias();
                return Ok(new Response<List<ReadMateriaDTO>>(data: response, message: "Materias obtenidas exitosamente"));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<List<ReadMateriaDTO>>(message: "Error al obtener las materias", succeded: false));
            }
        }
        [HttpGet("ObtenerMateriaPorId")]
        public async Task<IActionResult> ObtenerMateriaPorId(int id)
        {
            try
            {
                ReadMateriaDTO response = await _materiaRepository.ObtenerMateriaPorId(id);
                return Ok(new Response<ReadMateriaDTO>(data: response, message: "Materia obtenida exitosamente"));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<ReadMateriaDTO>(message: "Error al obtener la materia", succeded: false));
            }
        }
    }
}
