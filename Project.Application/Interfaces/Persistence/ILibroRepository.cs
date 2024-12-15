using Project.Application.DTOs.Infraestructure.Persistence.Autor;
using Project.Application.DTOs.Infraestructure.Persistence.Libro;
using Project.Application.DTOs.Infraestructure.Persistence.Tema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Interfaces.Persistence
{
    public interface ILibroRepository
    {
        public Task CrearLibro(CreateLibroDTO libro);
        public Task ActualizarLibro(UpdateLibroDTO libro);
        public Task EliminarLibro(int id);
        public Task<ReadLibroCompleteInformationDTO> LeerInformacionCompletaDeUnLibroPorId(int id);
        public Task<List<ReadLibroCompleteInformationDTO>> LeerInformacionCompletaDeTodosLosLibros();
        public Task<List<ReadReporteLibrosDTO>> LeerInformacionCompletaDeTodosLosLibrosParaReporte();
        public Task<List<ReadInformacionParaBuscarLibrosSimpleDTO>> ObtenerInformacionParaBusquedaDeLibrosSimple();
        public Task<List<ReadInformacionParaBuscarLibrosDetalladoDTO>> ObtenerInformacionParaBusquedaDeLibrosDetallado();
        public Task<ReadAutoresxLibro> ObtenerAutoresDeUnLibroPorId(int id);
        public Task<ReadTemasxLibro> ObtenerTemasDeUnLibroPorId(int id);
        public Task<ReadLibroDTO> ObtenerLibroPorId(int id);
        public Task<List<ReadLibroDTO>> ObtenerLibros();
        public Task AgregarAutoresAUnLibro(AddAutoresAUnLibro autores);
        public Task AgregarTemasAUnLibro(AddTemasAUnLibro temas);
        public Task<List<ReadAutorDTO>> ObtenerAutoresNoRelacionadosPorLibro(int id);
        public Task<List<ReadTemaDTO>> ObtenerTemasNoRelacionadosPorLibro(int id);    
    }
}
