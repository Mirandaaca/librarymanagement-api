using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.Infraestructure.Persistence.Ejemplar
{
    public class CreateEjemplarDTO
    {
        public int IdLibro { get; set; }
        public int IdOrigen { get; set; }
        public string Correlativo { get; set; }
        public string Clase { get; set; }
        public string Categoria { get; set; }
        public bool Disponibilidad { get; set; } = true; // Indica si está disponible
        public string Campo { get; set; }
    }
}
