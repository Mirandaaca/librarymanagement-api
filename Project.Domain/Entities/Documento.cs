using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public class Documento
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        // Relación con Persona
        public int IdPersona { get; set; }
        [ForeignKey("IdPersona")]
        public Persona Persona { get; set; }
    }
}
