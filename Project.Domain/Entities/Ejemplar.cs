using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public class Ejemplar
    {
        public int Id { get; set; }
        public int IdLibro { get; set; }
        public int IdOrigen { get; set; }
        public string Correlativo { get; set; }
        public string Clase { get; set; }
        public string Categoria { get; set; }
        [DefaultValue(true)]
        public bool Disponibilidad { get; set; } // Indica si está disponible
        public string Campo { get; set; }
        [ForeignKey("IdLibro")]
        public Libro Libro { get; set; }
        [ForeignKey("IdOrigen")]
        public Origen Origen { get; set; }
    }
}
