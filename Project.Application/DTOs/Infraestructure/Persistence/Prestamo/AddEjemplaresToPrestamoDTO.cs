using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.Infraestructure.Persistence.Prestamo
{
    public class AddEjemplaresToPrestamoDTO
    {
        public int IdPrestamo { get; set; }
        public List<int> EjemplaresIds { get; set; } // IDs de los ejemplares a agregar
    }
}
