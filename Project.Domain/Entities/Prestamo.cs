using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public class Prestamo
    {
        public int Id { get; set; }
        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; } // 0: Activo, 1: Inactivo

        [ForeignKey("IdPersona")]
        public Persona Persona { get; set; }

        // Un préstamo puede tener varios ejemplares (a través de PrestamoDetalle)
        public List<PrestamoDetalle> PrestamoDetalles { get; set; }
    }
}
