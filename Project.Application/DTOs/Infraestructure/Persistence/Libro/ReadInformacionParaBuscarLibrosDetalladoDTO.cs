using Project.Application.DTOs.Infraestructure.Persistence.Ejemplar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.Infraestructure.Persistence.Libro
{
    public class ReadInformacionParaBuscarLibrosDetalladoDTO
    {
        public string Titulo { get; set; }
        public List<string> Autores { get; set; }
        public List<string> Temas { get; set; }
        public string Editorial { get; set; }
        public string Area { get; set; }
        public List<ReadEjemplarParaBusquedaDeLibroDetalladoDTO> Ejemplares { get; set; } // Nueva lista para ejemplares
    }
}
