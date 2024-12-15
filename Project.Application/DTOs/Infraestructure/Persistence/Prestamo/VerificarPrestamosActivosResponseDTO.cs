using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.Infraestructure.Persistence.Prestamo
{
    public class VerificarPrestamosActivosResponseDTO
    {
        public bool TienePrestamosActivos { get; set; } // Indica si la persona tiene préstamos activos
        public PrestamoActivoDTO PrestamoActivo{ get; set; } // Detalle del prestamo activo
    }
}
