using Microsoft.EntityFrameworkCore;
using Project.Application.DTOs.Infraestructure.Persistence.Carrera;
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
    public class CarreraRepository : ICarreraRepository
    {
        private readonly LibraryContext _context;
        public CarreraRepository(LibraryContext context)
        {
            _context = context;
        }
        public async Task ActualizarCarrera(UpdateCarreraDTO carrera)
        {
            Carrera objCarrera = await _context.Carreras.FirstOrDefaultAsync(x => x.Id == carrera.Id);
            if(objCarrera == null)
            {
                throw new Exception("Carrera no encontrada");
            }
            objCarrera.Nombre = carrera.Nombre;
            objCarrera.Sigla = carrera.Sigla;
            _context.Carreras.Update(objCarrera);
            await _context.SaveChangesAsync();
        }

        public async Task CrearCarrera(CreateCarreraDTO carrera)
        {
            Carrera objCarrera = new Carrera
            {
                Nombre = carrera.Nombre,
                Sigla = carrera.Sigla
            };
            _context.Carreras.Add(objCarrera);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarCarrera(int id)
        {
            Carrera objCarrera = await _context.Carreras.FirstOrDefaultAsync(x => x.Id == id);
            if (objCarrera == null)
            {
                throw new Exception("Carrera no encontrada");
            }
            _context.Carreras.Remove(objCarrera);
            await _context.SaveChangesAsync();
        }

        public async Task<ReadCarreraDTO> ObtenerCarreraPorId(int id)
        {
            Carrera objCarrera = await _context.Carreras.FirstOrDefaultAsync(x => x.Id == id);
            ReadCarreraDTO carreraDTO = new ReadCarreraDTO
            {
                Id = objCarrera.Id,
                Nombre = objCarrera.Nombre,
                Sigla = objCarrera.Sigla
            };
            return carreraDTO;
        }

        public Task<List<ReadCarreraDTO>> ObtenerCarreras()
        {
            List<ReadCarreraDTO> carreras = _context.Carreras.Select(x => new ReadCarreraDTO
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Sigla = x.Sigla
            }).ToList();
            return Task.FromResult(carreras);
        }
    }
}
