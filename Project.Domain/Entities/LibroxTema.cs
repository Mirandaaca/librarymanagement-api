using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
  public class LibroxTema
  {
    public int Id { get; set; }
    public int IdTema { get; set; }
    public int IdLibro { get; set; }
    public string Descripcion { get; set; }
    [ForeignKey("IdTema")]
    public Tema Tema { get; set; }
    [ForeignKey("IdLibro")]
    public Libro Libro { get; set; }
  } 
}
