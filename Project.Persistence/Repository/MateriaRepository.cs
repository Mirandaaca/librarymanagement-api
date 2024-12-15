using Microsoft.EntityFrameworkCore;
using Project.Application.DTOs.Infraestructure.Identity.Materia;
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
    internal class MateriaRepository : IMateriaRepository
    {
        private readonly LibraryContext _context;
        public MateriaRepository(LibraryContext context)
        {
            _context = context;
        }
        public async Task ActualizarMateria(UpdateMateriaDTO materia)
        {
            Materia objMateria = await _context.Materias.FirstOrDefaultAsync(x => x.Id == materia.Id);
            if (objMateria == null)
            {
                throw new Exception("Materia no encontrada");
            }
            objMateria.Nombre = materia.Nombre;
            objMateria.Sigla = materia.Sigla;
            _context.Materias.Update(objMateria);
            await _context.SaveChangesAsync();
        }

        public async Task CrearMateria(CreateMateriaDTO materia)
        {
            // Crear la materia principal
            Materia objMateria = new Materia
            {
                Nombre = materia.Nombre,
                Sigla = materia.Sigla
            };

            _context.Materias.Add(objMateria);
            await _context.SaveChangesAsync(); // Guardar para obtener el ID de la materia

            // Poblar la tabla intermedia CarreraxMateria
            if (materia.CarrerasIds != null && materia.CarrerasIds.Any())
            {
                foreach (int carreraId in materia.CarrerasIds)
                {
                    CarreraxMateria nuevaRelacion = new CarreraxMateria
                    {
                        IdMateria = objMateria.Id,
                        IdCarrera = carreraId,
                        Descripcion = "Relacion creada automáticamente"
                    };
                    _context.CarrerasxMaterias.Add(nuevaRelacion);
                }
                await _context.SaveChangesAsync(); // Guardar las relaciones
            }
        }


        public async Task EliminarMateria(int id)
        {
            Materia objMateria = await _context.Materias.FirstOrDefaultAsync(x => x.Id == id);
            if (objMateria == null)
            {
                throw new Exception("Materia no encontrada");
            }
            _context.Materias.Remove(objMateria);
            await _context.SaveChangesAsync();
        }

        public async Task<ReadMateriaDTO> ObtenerMateriaPorId(int id)
        {
            Materia objMateria = await _context.Materias.FirstOrDefaultAsync(x => x.Id == id);
            ReadMateriaDTO materiaDTO = new ReadMateriaDTO
            {
                Nombre = objMateria.Nombre,
                Sigla = objMateria.Sigla
            };
            return materiaDTO;
        }

        public async Task<List<ReadMateriaDTO>> ObtenerMaterias()
        {
            List<Materia> listaMaterias = await _context.Materias.ToListAsync();
            List<ReadMateriaDTO> listaMateriaDTO = new List<ReadMateriaDTO>();
            foreach (Materia materia in listaMaterias)
            {
                ReadMateriaDTO materiaDTO = new ReadMateriaDTO
                {
                    Id = materia.Id,
                    Nombre = materia.Nombre,
                    Sigla = materia.Sigla
                };
                listaMateriaDTO.Add(materiaDTO);
            }
            return listaMateriaDTO; 
        }
    }
}
