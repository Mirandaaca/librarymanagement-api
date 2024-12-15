using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.Infraestructure.Persistence.Ejemplar
{
    public class ReadEjemplarParaPrestamoDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Correlativo { get; set; }
        public string Clase { get; set; }
        public string Categoria { get; set; }
        public bool Disponible { get; set; }
    }
}
