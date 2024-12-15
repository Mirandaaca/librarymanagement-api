using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs.Infraestructure.Persistence.Documento;
using Project.Application.Interfaces.Persistence;
using Project.Application.Wrappers;
using System.Security.Cryptography;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentosController : ControllerBase
    {
        private readonly IDocumentoRepository _repository;
        public DocumentosController(IDocumentoRepository repository)
        {
            _repository = repository;
        }
        [HttpPost("CrearDocumento")]
        public async Task<IActionResult> CrearDocumento([FromBody] CreateDocumentoDTO documento)
        {
            try
            {
                await _repository.CrearDocumento(documento);
                return Ok(new Response<bool>(message: "Documento guardado exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al guardar el documento", succeded: false));
            }
        }
        [HttpPut("ActualizarDocumento")]
        public async Task<IActionResult> ActualizarDocumento([FromBody] UpdateDocumentoDTO documento)
        {
            try
            {
                await _repository.ActualizarDocumento(documento);
                return Ok(new Response<bool>(message: "Documento actualizado exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al actualizar el documento", succeded: false));
            }
        }
        [HttpDelete("EliminarDocumento")]
        public async Task<IActionResult> EliminarDocumento(int id)
        {
            try
            {
                await _repository.EliminarDocumento(id);
                return Ok(new Response<bool>(message: "Documento eliminado exitosamente", succeded: true));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al eliminar el documento", succeded: false));
            }
        }
        [HttpGet("ObtenerDocumentos")]
        public async Task<IActionResult> ObtenerDocumentos()
        {
            try
            {
                List<ReadDocumentoDTO> listaDocumentos = await _repository.ObtenerDocumentos();
                return Ok(new Response<List<ReadDocumentoDTO>>(data: listaDocumentos, message: "Documentos obtenidos exitosamente"));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al obtener los documentos", succeded: false));
            }
        }
        [HttpGet("ObtenerDocumentoPorId")]
        public async Task<IActionResult> ObtenerDocumentoPorId(int id)
        {
            try
            {
                ReadDocumentoDTO documentoDTO = await _repository.ObtenerDocumentoPorId(id);
                return Ok(new Response<ReadDocumentoDTO>(data: documentoDTO, message: "Documento obtenido exitosamente"));
            }
            catch (Exception)
            {
                return StatusCode(500, new Response<bool>(message: "Ocurrió un error al obtener el documento", succeded: false));
            }
        }
    }
}
