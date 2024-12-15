using Project.Application.DTOs.Infraestructure.Persistence.Autor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.Infraestructure.Persistence.Libro
{
    public class ReadAutoresxLibro
    {
        public int IdLibro { get; set; }
        public List<ReadAutorDTO> Autores { get; set; }
    }
}
