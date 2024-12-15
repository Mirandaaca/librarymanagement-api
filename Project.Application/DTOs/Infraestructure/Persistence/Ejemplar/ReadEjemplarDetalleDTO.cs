using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.Infraestructure.Persistence.Ejemplar
{
    public class ReadEjemplarDetalleDTO
    {
        public string Correlativo { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public int PrecioPrestamo { get; set; }
    }
}
