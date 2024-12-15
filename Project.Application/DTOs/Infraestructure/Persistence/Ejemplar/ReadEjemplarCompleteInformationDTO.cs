using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.Infraestructure.Persistence.Ejemplar
{
    public class ReadEjemplarCompleteInformationDTO
    {
        public int Id { get; set; }
        public int IdLibro { get; set; }
        public string Titulo { get; set; }
        public int IdOrigen { get; set; }
        public string Origen { get; set; }
        public string Correlativo { get; set; }
        public string Clase { get; set; }
        public string Categoria { get; set; }
        public bool Disponibilidad { get; set; } // Indica si está disponible
        public string Campo { get; set; }
    }
}
