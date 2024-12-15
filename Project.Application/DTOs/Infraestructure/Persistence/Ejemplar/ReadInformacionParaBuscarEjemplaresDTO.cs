using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.Infraestructure.Persistence.Ejemplar
{
    public class ReadInformacionParaBuscarEjemplaresDTO
    {
        public string Titulo { get; set; }
        public List<string> Autores { get; set; }
        public string Correlativo { get; set; }
        public string Editorial { get; set; }
        public string Area { get; set; }
        public List<string> Temas { get; set; }
        public bool Disponibilidad { get; set; }
    }
}
