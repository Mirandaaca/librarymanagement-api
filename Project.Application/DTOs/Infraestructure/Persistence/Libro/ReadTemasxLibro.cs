using Project.Application.DTOs.Infraestructure.Persistence.Tema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.Infraestructure.Persistence.Libro
{
    public class ReadTemasxLibro
    {
        public int IdLibro { get; set; }
        public List<ReadTemaDTO> Temas { get; set; }
    }
}
