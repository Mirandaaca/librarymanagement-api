using Project.Application.DTOs.Infraestructure.Persistence.Ejemplar;
using Project.Application.DTOs.Infraestructure.Persistence.Persona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.Infraestructure.Persistence.Prestamo
{
    public class ReadHistorialPrestamosDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public ReadPersonaPrestamoDTO Persona { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public bool Estado { get; set; }
        public List<ReadEjemplarPrestamoDTO> Ejemplares { get; set; }
    }
}
