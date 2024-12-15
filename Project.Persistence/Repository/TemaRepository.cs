using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Project.Application.DTOs.Infraestructure.Persistence.Tema;
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
    public class TemaRepository : ITemaRepository
    {
        private readonly LibraryContext _context;
        public TemaRepository(LibraryContext context)
        {
            _context = context;
        }
        public async Task ActualizarTema(UpdateTemaDTO tema)
        {
            Tema objTema = await _context.Temas.FirstOrDefaultAsync(t => t.Id.Equals(tema.Id));
            if (objTema == null)
            {
                throw new Exception("El tema no existe");
            }
            else
            {
                objTema.Descripcion = tema.Descripcion;
                _context.Temas.Update(objTema);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CrearTema(CreateTemaDTO tema)
        {
            Tema objTema = new Tema
            {
                Descripcion = tema.Descripcion
            };
            _context.Temas.Add(objTema);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarTema(int id)
        {
            Tema objTema = await _context.Temas.FirstOrDefaultAsync(t => t.Id.Equals(id));
            if(objTema == null)
            {
                throw new Exception("El tema no existe");
            }
            else
            {
                _context.Temas.Remove(objTema);
                await _context.SaveChangesAsync();
            }   
        }

        public async Task<ReadTemaDTO> ObtenerTemaPorId(int id)
        {
            Tema objTema = await _context.Temas.FirstOrDefaultAsync(t => t.Id.Equals(id));
            ReadTemaDTO temaDTO = new ReadTemaDTO { 
                Id = objTema.Id,
                Descripcion = objTema.Descripcion
            };
            return temaDTO;
        }

        public async Task<List<ReadTemaDTO>> ObtenerTemas()
        {
            List<Tema> listaTemas = await _context.Temas.ToListAsync();
            List<ReadTemaDTO> listaTemasDTO = new List<ReadTemaDTO>();
            foreach (Tema tema in listaTemas)
            {
                ReadTemaDTO temaDTO = new ReadTemaDTO
                {
                    Id = tema.Id,
                    Descripcion = tema.Descripcion
                };
                listaTemasDTO.Add(temaDTO);
            }
            return listaTemasDTO;
        }
    }
}
