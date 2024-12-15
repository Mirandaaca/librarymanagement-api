using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
  public class LibroxAutor
  {
        public int Id { get; set; }
        public int IdLibro { get; set; }
        public int IdAutor { get; set; }
        public string Descripcion { get; set; }
        [ForeignKey("IdLibro")]
        public Libro Libro { get; set; }
        [ForeignKey("IdAutor")]
        public Autor Autor { get; set; }
  }
}
