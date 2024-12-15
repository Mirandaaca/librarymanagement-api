using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public class Origen
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public List<Ejemplar> Ejemplares { get; set; }
    }
}
