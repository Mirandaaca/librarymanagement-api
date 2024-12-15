using Microsoft.EntityFrameworkCore;
using Project.Application.DTOs.Infraestructure.Persistence.Documento;
using Project.Application.Interfaces.Persistence;
using Project.Domain.Entities;
using Project.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistence.Repository
{
    public class DocumentoRepository : IDocumentoRepository
    {
        private readonly LibraryContext _context;
        public DocumentoRepository(LibraryContext context) {
            _context = context;
        }

        public async Task ActualizarDocumento(UpdateDocumentoDTO documento)
        {
            Documento objDocumento = await _context.Documentos.FirstOrDefaultAsync(d => d.Id.Equals(documento.Id));
            if (objDocumento == null)
            {
                throw new Exception("Documento no encontrado");
            }
            objDocumento.Descripcion = documento.Descripcion;
            await _context.SaveChangesAsync();
        }

        public async Task CrearDocumento(CreateDocumentoDTO documento)
        {
            Documento objDocumento = new Documento
            {
                Descripcion = documento.Descripcion,
                IdPersona = documento.IdPersona

            };
            _context.Documentos.Add(objDocumento);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarDocumento(int id)
        {
            Documento objDocumento = await _context.Documentos.FirstOrDefaultAsync(d => d.Id.Equals(id));
            if (objDocumento == null) {
                throw new Exception("Documento no encontrado");
            }
            _context.Documentos.Remove(objDocumento);
            await _context.SaveChangesAsync();
        }
        public async Task<ReadDocumentoDTO> ObtenerDocumentoPorId(int id)
        {
            Documento objDocumento = await _context.Documentos
                .Include(d => d.Persona)
                .FirstOrDefaultAsync(d => d.Id.Equals(id));
            ReadDocumentoDTO documentoDTO = new ReadDocumentoDTO
            {
                Id = objDocumento.Id,
                NombreCompleto = $"{objDocumento.Persona.Nombre} {objDocumento.Persona.Apellido}",
                Registro = objDocumento.Persona.Registro,
                IdPersona = objDocumento.IdPersona,
                Descripcion = objDocumento.Descripcion
            };
            return documentoDTO;
        }

        public async Task<List<ReadDocumentoDTO>> ObtenerDocumentos()
        {
            List<Documento> listaDocumentos = await _context.Documentos
                .Include(d => d.Persona)
                .ToListAsync();
            List<ReadDocumentoDTO> listaDocumentosDTO = new List<ReadDocumentoDTO>();
            foreach (Documento objDocumento in listaDocumentos)
            {
                ReadDocumentoDTO documentoDTO = new ReadDocumentoDTO
                {
                    Id = objDocumento.Id,
                    NombreCompleto = $"{objDocumento.Persona.Nombre} {objDocumento.Persona.Apellido}",
                    Registro = objDocumento.Persona.Registro,
                    IdPersona = objDocumento.IdPersona,
                    Descripcion = objDocumento.Descripcion
                };
                listaDocumentosDTO.Add(documentoDTO);
            }
            return listaDocumentosDTO;
        }
    }
}
