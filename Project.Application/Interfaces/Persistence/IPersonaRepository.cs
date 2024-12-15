using Project.Application.DTOs.Infraestructure.Persistence.Persona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Interfaces.Persistence
{
    public interface IPersonaRepository
    {
        public Task CrearPersona(CreatePersonaDTO persona);
        public Task ActualizarPersona(UpdatePersonaDTO persona);
        public Task EliminarPersona(int id);
        public Task<List<ReadPersonaDTO>> ObtenerPersonas();
        public Task<ReadPersonaDTO> ObtenerPersonaPorId(int id);
    }
}
