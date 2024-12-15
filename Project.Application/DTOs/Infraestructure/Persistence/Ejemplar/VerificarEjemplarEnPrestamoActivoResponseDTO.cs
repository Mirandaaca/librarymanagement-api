using Project.Application.DTOs.Infraestructure.Persistence.Prestamo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.Infraestructure.Persistence.Ejemplar
{
    public class VerificarEjemplarEnPrestamoActivoResponseDTO
    {
        public bool EstaEnPrestamoActivo { get; set; } // Indica si el ejemplar está en un préstamo activo
        public PrestamoActivoDTO Prestamo { get; set; } // Detalles del préstamo activo (si aplica)
    }
}
