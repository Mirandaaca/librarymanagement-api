using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.Infraestructure.Identity.Materia
{
    public class CreateMateriaDTO
    {
        public string Nombre { get; set; }
        public string Sigla { get; set; }
        public List<int> CarrerasIds { get; set; } // IDs de las carreras asociadas
    }
}
