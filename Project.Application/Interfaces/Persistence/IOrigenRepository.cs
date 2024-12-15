using Project.Application.DTOs.Infraestructure.Persistence.Origen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Interfaces.Persistence
{
    public interface IOrigenRepository
    {
        public Task CrearOrigen(CreateOrigenDTO origen);
        public Task ActualizarOrigen(UpdateOrigenDTO origen);
        public Task EliminarOrigen(int id);
        public Task<ReadOrigenDTO> ObtenerOrigenPorId(int id);
        public Task<List<ReadOrigenDTO>> ObtenerOrigenes();
    }
}
