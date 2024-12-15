using Project.Application.DTOs.Infraestructure.Identity.Materia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Interfaces.Persistence
{
    public interface IMateriaRepository
    {
        public Task CrearMateria(CreateMateriaDTO materia);
        public Task ActualizarMateria(UpdateMateriaDTO materia);
        public Task EliminarMateria(int id);
        public Task<List<ReadMateriaDTO>> ObtenerMaterias();
        public Task<ReadMateriaDTO> ObtenerMateriaPorId(int id);
    }
}
