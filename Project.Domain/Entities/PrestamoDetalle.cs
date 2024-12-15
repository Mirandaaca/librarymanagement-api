using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public class PrestamoDetalle
    {
        public int Id { get; set; }
        public int IdPrestamo { get; set; }
        public int IdEjemplar { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public int PrecioPrestamo { get; set; }

        [ForeignKey("IdPrestamo")]
        public Prestamo Prestamo { get; set; }
        [ForeignKey("IdEjemplar")]
        public Ejemplar Ejemplar { get; set; }
    }
}
