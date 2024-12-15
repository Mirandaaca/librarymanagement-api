using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.Infraestructure.Persistence.Libro
{
    public class ReadLibroDTO
    {
        public int Id { get; set; }
        public int IdTipoLibro { get; set; }
        public int IdIdioma { get; set; }
        public int IdEditorial { get; set; }
        public int IdCarrera { get; set; }
        public string Nombre { get; set; }
        public int Precio { get; set; }
    }
}
