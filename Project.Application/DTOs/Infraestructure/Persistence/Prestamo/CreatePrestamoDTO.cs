using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.Infraestructure.Persistence.Prestamo
{
    public class CreatePrestamoDTO
    {
        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<int> EjemplaresIds { get; set; } // IDs de los ejemplares a prestar
    }
}
