using Project.Application.DTOs.Infraestructure.Persistence.Carrera;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Interfaces.Persistence
{
    public interface ICarreraRepository
    {
        public Task CrearCarrera(CreateCarreraDTO carrera);
        public Task ActualizarCarrera(UpdateCarreraDTO carrera);
        public Task EliminarCarrera(int id);
        public Task<List<ReadCarreraDTO>> ObtenerCarreras();
        public Task<ReadCarreraDTO> ObtenerCarreraPorId(int id);
    }
}
