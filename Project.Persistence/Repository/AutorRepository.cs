using Microsoft.EntityFrameworkCore;
using Project.Application.DTOs.Infraestructure.Persistence.Autor;
using Project.Application.Interfaces.Persistence;
using Project.Domain.Entities;
using Project.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistence.Repository
{
    public class AutorRepository : IAutorRepository
    {
        private readonly LibraryContext _context;
        public AutorRepository(LibraryContext context)
        {
            _context = context;
        }
        public async Task ActualizarAutor(UpdateAutorDTO autor)
        {
            Autor objAutor = await _context.Autores.FirstOrDefaultAsync(a => a.Id.Equals(autor.Id));
            if(objAutor == null)
            {
                throw new Exception("El autor no existe");
            }
            else
            {
                objAutor.Nombre = autor.Nombre;
                _context.Autores.Update(objAutor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CrearAutor(CreateAutorDTO autor)
        {
            Autor objAutor = new Autor
            {
                Nombre = autor.Nombre
            };
            _context.Autores.Add(objAutor);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAutor(int id)
        {
            Autor objAutor = await _context.Autores.FirstOrDefaultAsync(a => a.Id.Equals(id));
            if (objAutor == null)
            {
                throw new Exception("El autor no existe");
            }
            else
            {
                _context.Autores.Remove(objAutor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ReadAutorDTO>> ObtenerAutores()
        {
            List<Autor> listaAutores = await _context.Autores.ToListAsync();
            List<ReadAutorDTO> listaAutoresDTO = new List<ReadAutorDTO>();
            foreach (Autor autor in listaAutores)
            {
                ReadAutorDTO autorDTO = new ReadAutorDTO
                {
                    Id = autor.Id,
                    Nombre = autor.Nombre
                };
                listaAutoresDTO.Add(autorDTO);
            }
            return listaAutoresDTO;
        }

        public async Task<ReadAutorDTO> ObtenerAutorPorId(int id)
        {
            Autor objAutor = await _context.Autores.FirstOrDefaultAsync(a => a.Id.Equals(id));
   
            ReadAutorDTO autorDTO = new ReadAutorDTO() { 
                Id = objAutor.Id,
                Nombre = objAutor.Nombre
            };
            return autorDTO;
        }
    }
}
