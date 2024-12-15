using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public class Libro
    {
        public int Id { get; set; }
        public int IdTipoLibro { get; set; }
        public int IdIdioma { get; set; }
        public int IdEditorial { get; set; }
        public int IdCarrera { get; set; }
        public string Nombre { get; set; }
        public int Precio { get; set; }
        [ForeignKey("IdTipoLibro")]
        public TipoLibro TipoLibro { get; set; }
        [ForeignKey("IdIdioma")]
        public Idioma Idioma { get; set; }
        [ForeignKey("IdEditorial")]
        public Editorial Editorial { get; set; }
        [ForeignKey("IdCarrera")]
        public Carrera Carrera { get; set; }
        public List<LibroxAutor> LibrosxAutores { get; set; }
        public List<LibroxTema> LibrosxTemas { get; set; }
        public List<Ejemplar> Ejemplares { get; set; }
    }
}
