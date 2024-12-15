using Project.Application.DTOs.Infraestructure.Persistence.Ejemplar;
using Project.Application.DTOs.Infraestructure.Persistence.Persona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.Infraestructure.Persistence.Prestamo
{
    public class ReadPrestamoDetallePorId
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public ReadPersonaPrestamoDTO Persona { get; set; }
        public List<ReadEjemplarDetalleDTO> Ejemplares { get; set; }
    }
}
