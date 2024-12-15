using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.Infraestructure.Persistence.Documento
{
    public class ReadDocumentoDTO
    {
        public int Id { get; set; }
        public int IdPersona { get; set; }
        public string NombreCompleto { get; set; }
        public string Registro { get; set; }
        public string Descripcion { get; set; }
    }
}
