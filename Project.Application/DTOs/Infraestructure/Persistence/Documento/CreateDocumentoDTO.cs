using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.Infraestructure.Persistence.Documento
{
    public class CreateDocumentoDTO
    {
        public int IdPersona { get; set; }
        public string Descripcion { get; set; }
    }
}
