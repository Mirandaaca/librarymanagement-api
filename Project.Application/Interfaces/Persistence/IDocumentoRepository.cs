using Project.Application.DTOs.Infraestructure.Persistence.Documento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Interfaces.Persistence
{
    public interface IDocumentoRepository
    {
        public Task CrearDocumento(CreateDocumentoDTO documento);
        public Task ActualizarDocumento(UpdateDocumentoDTO documento);
        public Task EliminarDocumento(int id);
        public Task<List<ReadDocumentoDTO>> ObtenerDocumentos();
        public Task<ReadDocumentoDTO> ObtenerDocumentoPorId(int id);
    }
}
