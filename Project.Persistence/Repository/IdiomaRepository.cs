using Microsoft.EntityFrameworkCore;
using Project.Application.DTOs.Infraestructure.Persistence.Idioma;
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
    public class IdiomaRepository : IIdiomaRepository
    {
        private readonly LibraryContext _context;
        public IdiomaRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task ActualizarIdioma(UpdateIdiomaDTO idioma)
        {
            Idioma objIdioma = await _context.Idiomas.FirstOrDefaultAsync(i => i.Id.Equals(idioma.Id));
            if(objIdioma == null)
            {
                throw new Exception("El idioma no existe");
            }
            else
            {
                objIdioma.Descripcion = idioma.Descripcion;
                _context.Idiomas.Update(objIdioma);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CrearIdioma(CreateIdiomaDTO idioma)
        {
            Idioma objIdioma = new Idioma
            {
                Descripcion = idioma.Descripcion
            };
            _context.Idiomas.Add(objIdioma);
            await _context.SaveChangesAsync();
        }

        public async Task ElimnarIdiomar(int id)
        {
            Idioma objIdioma = await _context.Idiomas.FirstOrDefaultAsync(i => i.Id.Equals(id));
            if (objIdioma == null)
            {
                throw new Exception("El idioma no existe");
            }
            else
            {
                _context.Idiomas.Remove(objIdioma);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ReadIdiomaDTO> ObtenerIdiomaPorId(int id)
        {
            Idioma objIdioma = await _context.Idiomas.FirstOrDefaultAsync(i => i.Id.Equals(id));
            ReadIdiomaDTO idiomaDTO = new ReadIdiomaDTO
            {
                Id = objIdioma.Id,
                Descripcion = objIdioma.Descripcion
            };
            return idiomaDTO;
        }

        public async Task<List<ReadIdiomaDTO>> ObtenerIdiomas()
        {
            List<Idioma> listaIdiomas = await _context.Idiomas.ToListAsync();
            List<ReadIdiomaDTO> listaIdiomasDTO = new List<ReadIdiomaDTO>();
            foreach (Idioma idioma in listaIdiomas)
            {
                ReadIdiomaDTO idiomaDTO = new ReadIdiomaDTO
                {
                    Id = idioma.Id,
                    Descripcion = idioma.Descripcion
                };
                listaIdiomasDTO.Add(idiomaDTO);
            }
            return listaIdiomasDTO;
        }
    }
}
