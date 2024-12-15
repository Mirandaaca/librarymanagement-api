using Project.Application.DTOs.Infraestructure.Persistence.Idioma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Interfaces.Persistence
{
    public interface IIdiomaRepository
    {
        public Task CrearIdioma(CreateIdiomaDTO idioma);
        public Task ActualizarIdioma(UpdateIdiomaDTO idioma);
        public Task ElimnarIdiomar(int id);
        public Task<List<ReadIdiomaDTO>> ObtenerIdiomas();
        public Task<ReadIdiomaDTO> ObtenerIdiomaPorId(int id);
    }
}
