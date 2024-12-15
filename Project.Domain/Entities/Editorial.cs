using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
  public class Editorial
  {
    public int Id { get; set; }
    public string Nombre { get; set; }
    public List<Libro> Libros { get; set; }
  }
}
