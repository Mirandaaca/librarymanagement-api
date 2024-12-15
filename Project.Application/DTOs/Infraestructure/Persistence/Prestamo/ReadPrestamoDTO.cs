using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.Infraestructure.Persistence.Prestamo
{
    public class ReadPrestamoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public string Descripcion { get; set; }
        public List<string> Ejemplares { get; set; } // Ejemplares prestados
    }
}
