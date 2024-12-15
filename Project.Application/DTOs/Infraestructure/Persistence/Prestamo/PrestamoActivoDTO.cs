using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.Infraestructure.Persistence.Prestamo
{
    public class PrestamoActivoDTO
    {
        public int IdPrestamo { get; set; } // ID del préstamo
        public string Nombre { get; set; } // Nombre del préstamo
        public DateTime FechaPrestamo { get; set; } // Fecha en que se realizó el préstamo
        public string Descripcion { get; set; } // Descripción del préstamo
    }
}
