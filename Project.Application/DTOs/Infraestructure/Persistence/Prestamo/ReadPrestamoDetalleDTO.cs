using Project.Application.DTOs.Infraestructure.Persistence.Ejemplar;
using Project.Application.DTOs.Infraestructure.Persistence.Persona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.Infraestructure.Persistence.Prestamo
{
    public class ReadPrestamoDetalleDTO
    {
        public int Id { get; set; } // ID del préstamo
        public string Nombre { get; set; } // Nombre del préstamo
        public DateTime FechaPrestamo { get; set; } // Fecha en la que se realizó el préstamo
        public bool Estado { get; set; } // Estado del préstamo (devuelto o no)
        public ReadPersonaPrestamoDTO Persona { get; set; } // Información de la persona asociada al préstamo
        public List<ReadEjemplarDePrestamoDetalleDTO> Ejemplares { get; set; } // Lista de ejemplares prestados
    }
}
