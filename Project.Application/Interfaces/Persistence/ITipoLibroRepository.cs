using Project.Application.DTOs.Infraestructure.Persistence.TipoLibro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Interfaces.Persistence
{
    public interface ITipoLibroRepository
    {
        public Task CrearTipoLibro(CreateTipoLibroDTO tipoLibro);
        public Task ActualizarTipoLibro(UpdateTipoLibroDTO tipoLibro);
        public Task EliminarTipoLibro(int id);
        public Task<List<ReadTipoLibroDTO>> ObtenerTiposLibros();
        public Task<ReadTipoLibroDTO> ObtenerTipoLibroPorId(int id);
    }
}
