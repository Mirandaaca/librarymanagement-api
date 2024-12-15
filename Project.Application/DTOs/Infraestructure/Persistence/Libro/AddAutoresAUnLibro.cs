using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.Infraestructure.Persistence.Libro
{
    public class AddAutoresAUnLibro
    {
        public int IdLibro { get; set; }
        public List<int> IdAutores { get; set; }
    }
}
