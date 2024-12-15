using Microsoft.EntityFrameworkCore;
using Project.Application.DTOs.Infraestructure.Persistence.TipoLibro;
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
    public class TipoLibroRepository : ITipoLibroRepository
    {
        private readonly LibraryContext _context;
        public TipoLibroRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task ActualizarTipoLibro(UpdateTipoLibroDTO tipoLibro)
        {
            TipoLibro objTipoLibro = await _context.TiposLibro.FirstOrDefaultAsync(tl => tl.Id.Equals(tipoLibro.Id));
            if (objTipoLibro == null)
            {
                throw new Exception("El tipo de libro no existe");
            }
            else
            {
                objTipoLibro.Descripcion = tipoLibro.Descripcion;
                _context.TiposLibro.Update(objTipoLibro);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CrearTipoLibro(CreateTipoLibroDTO tipoLibro)
        {
            TipoLibro objTipoLibro = new TipoLibro
            {
                Descripcion = tipoLibro.Descripcion
            };
            _context.TiposLibro.Add(objTipoLibro);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarTipoLibro(int id)
        {
            TipoLibro objTipoLibro = await _context.TiposLibro.FirstOrDefaultAsync(tl => tl.Id.Equals(id));
            if (objTipoLibro == null)
            {
                throw new Exception("El tipo de libro no existe");
            }
            else
            {
                _context.TiposLibro.Remove(objTipoLibro);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ReadTipoLibroDTO> ObtenerTipoLibroPorId(int id)
        {
            TipoLibro objTipoLibro = await _context.TiposLibro.FirstOrDefaultAsync(tl => tl.Id.Equals(id));
            ReadTipoLibroDTO tipoLibroDTO = new ReadTipoLibroDTO{
                Id = objTipoLibro.Id,
                Descripcion = objTipoLibro.Descripcion
            };
            return tipoLibroDTO;
        }

        public async Task<List<ReadTipoLibroDTO>> ObtenerTiposLibros()
        {
            List<TipoLibro> listaTipoLibro = await _context.TiposLibro.ToListAsync();
            List<ReadTipoLibroDTO> listaTipoLibroDTO = new List<ReadTipoLibroDTO>();
            foreach (TipoLibro tipoLibro in listaTipoLibro)
            {
                ReadTipoLibroDTO tipoLibroDTO = new ReadTipoLibroDTO
                {
                    Id = tipoLibro.Id,
                    Descripcion = tipoLibro.Descripcion 
                };
                listaTipoLibroDTO.Add(tipoLibroDTO);
            }
            return listaTipoLibroDTO;
        }
    }
}
