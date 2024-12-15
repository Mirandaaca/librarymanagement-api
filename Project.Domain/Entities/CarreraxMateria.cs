using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public class CarreraxMateria
    {
        public int Id { get; set; }
        public int IdCarrera { get; set; }
        public int IdMateria { get; set; }
        public string Descripcion { get; set; }
        [ForeignKey("IdCarrera")]
        public Carrera Carrera { get; set; }
        [ForeignKey("IdMateria")]
        public Materia Materia { get; set; }
    }
}
