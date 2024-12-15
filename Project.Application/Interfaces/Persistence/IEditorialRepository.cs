using Project.Application.DTOs.Infraestructure.Persistence.Editorial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Interfaces.Persistence
{
    public interface IEditorialRepository
    {
        public Task CrearEditorial(CreateEditorialDTO editorial);
        public Task ActualizarEditorial(UpdateEditorialDTO editorial);
        public Task EliminarEditorial(int id);
        public Task<List<ReadEditorialDTO>> ObtenerEditoriales();
        public Task<ReadEditorialDTO> ObtenerEditorialPorId(int id);
    }
}
