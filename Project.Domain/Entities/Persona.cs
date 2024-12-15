using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Registro { get; set; }
        public string Cedula { get; set; }
        public string Sexo { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public DateTime FechaDeNacimiento { get; set; }

        // Relación con Documento
        public List<Documento> Documentos { get; set; }

        public List<Prestamo> Prestamos { get; set; }
    }
}
