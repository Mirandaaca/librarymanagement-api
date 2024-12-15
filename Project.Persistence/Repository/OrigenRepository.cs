using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Project.Application.DTOs.Infraestructure.Persistence.Origen;
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
    public class OrigenRepository : IOrigenRepository
    {
        private readonly LibraryContext _context;
        public OrigenRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task ActualizarOrigen(UpdateOrigenDTO origen)
        {
            Origen objOrigen = await _context.Origenes.FirstOrDefaultAsync(o => o.Id == origen.Id);
            if (objOrigen == null)
            {
                throw new Exception("El origen no existe");
            }
            else
            {
                objOrigen.Descripcion = origen.Descripcion;
                _context.Origenes.Update(objOrigen);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CrearOrigen(CreateOrigenDTO origen)
        {
            Origen objOrigen = new Origen
            {
                Descripcion = origen.Descripcion
            };
            _context.Origenes.Add(objOrigen);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarOrigen(int id)
        {
            Origen objOrigen = await _context.Origenes.FirstOrDefaultAsync(o => o.Id == id);
            if (objOrigen == null)
            {
                throw new Exception("El origen no existe");
            }
            else
            {
                _context.Origenes.Remove(objOrigen);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ReadOrigenDTO>> ObtenerOrigenes()
        {
            List<Origen> listaOrigenes = await _context.Origenes.ToListAsync();
            List<ReadOrigenDTO> listaOrigenesDTO = new List<ReadOrigenDTO>();
            foreach (Origen origen in listaOrigenes)
            {
                ReadOrigenDTO origenDTO = new ReadOrigenDTO
                {
                    Id = origen.Id,
                    Descripcion = origen.Descripcion
                };
                listaOrigenesDTO.Add(origenDTO);
            }
            return listaOrigenesDTO;
        }

        public async Task<ReadOrigenDTO> ObtenerOrigenPorId(int id)
        {
            Origen objOrigen = await _context.Origenes.FirstOrDefaultAsync(o => o.Id == id);
            ReadOrigenDTO origenDTO = new ReadOrigenDTO
            {
                Id = objOrigen.Id,
                Descripcion = objOrigen.Descripcion
            };
            return origenDTO;
        }
    }
}
