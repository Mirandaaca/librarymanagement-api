using Project.Application.DTOs.Infraestructure.Persistence.Autor;
using Project.Application.DTOs.Infraestructure.Persistence.Tema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.Infraestructure.Persistence.Libro
{
    public class ReadLibroCompleteInformationDTO
    {
        public int Id { get; set; }
        public int IdTipoLibro { get; set; }
        public string TipoLibro { get; set; }
        public int IdIdioma { get; set; }
        public string Idioma { get; set; }
        public int IdEditorial { get; set; }
        public string Editorial { get; set; }
        public int IdCarrera { get; set; }
        public string Carrera { get; set; }
        public string SiglaCarrera { get; set; }
        public List<ReadAutorDTO> Autores { get; set; }
        public List<ReadTemaDTO> Temas { get; set; }
        public string Nombre { get; set; }
        public int Precio { get; set; }
    }
}
