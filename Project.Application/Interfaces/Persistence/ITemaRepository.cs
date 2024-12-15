using Microsoft.Identity.Client;
using Project.Application.DTOs.Infraestructure.Persistence.Tema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Interfaces.Persistence
{
    public interface ITemaRepository
    {
        public Task CrearTema(CreateTemaDTO tema);
        public Task ActualizarTema(UpdateTemaDTO tema);
        public Task EliminarTema(int id);
        public Task<List<ReadTemaDTO>> ObtenerTemas();
        public Task<ReadTemaDTO> ObtenerTemaPorId(int id);
    }
}
