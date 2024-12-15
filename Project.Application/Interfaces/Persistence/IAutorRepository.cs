using Project.Application.DTOs.Infraestructure.Persistence.Autor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Interfaces.Persistence
{
    public interface IAutorRepository
    {
        public Task CrearAutor(CreateAutorDTO autor);
        public Task ActualizarAutor(UpdateAutorDTO autor);
        public Task EliminarAutor(int id);
        public Task<List<ReadAutorDTO>> ObtenerAutores();
        public Task<ReadAutorDTO> ObtenerAutorPorId(int id);
    }
}
