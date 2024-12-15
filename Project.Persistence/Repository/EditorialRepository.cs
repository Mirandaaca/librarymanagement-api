using Microsoft.EntityFrameworkCore;
using Project.Application.DTOs.Infraestructure.Persistence.Editorial;
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
    public class EditorialRepository : IEditorialRepository
    {
        private readonly LibraryContext _context;
        public EditorialRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task ActualizarEditorial(UpdateEditorialDTO editorial)
        {
            Editorial objEditorial = await _context.Editoriales.FirstOrDefaultAsync(e => e.Id.Equals(editorial.Id));
            if(objEditorial == null)
            {
                throw new Exception("Esa editorial no existe");
            }
            objEditorial.Nombre = editorial.Nombre;
            _context.Editoriales.Update(objEditorial);
            await _context.SaveChangesAsync();
        }

        public async Task CrearEditorial(CreateEditorialDTO editorial)
        {
            Editorial objEditorial = new Editorial
            {
                Nombre = editorial.Nombre
            };
            _context.Editoriales.Add(objEditorial);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarEditorial(int id)
        {
            Editorial objEditorial = await _context.Editoriales.FirstOrDefaultAsync(e => e.Id.Equals(id));
            if (objEditorial == null)
            {
                throw new Exception("Esa editorial no existe");
            }
            _context.Editoriales.Remove(objEditorial);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ReadEditorialDTO>> ObtenerEditoriales()
        {
            List<Editorial> listaEditoriales = await _context.Editoriales.ToListAsync();
            List<ReadEditorialDTO> listaEditorialesDTO = new List<ReadEditorialDTO>();
            foreach(Editorial editorial in listaEditoriales)
            {
                ReadEditorialDTO editorialDTO = new ReadEditorialDTO
                {
                    Id = editorial.Id,
                    Nombre = editorial.Nombre
                };
                listaEditorialesDTO.Add(editorialDTO);
            }
            return listaEditorialesDTO;
        }

        public async Task<ReadEditorialDTO> ObtenerEditorialPorId(int id)
        {
            Editorial objEditorial = await _context.Editoriales.FirstOrDefaultAsync(e => e.Id.Equals(id));
            ReadEditorialDTO editorialDTO = new ReadEditorialDTO
            {
                Id = objEditorial.Id,
                Nombre = objEditorial.Nombre
            };
            return editorialDTO;
        }
    }
}
