﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.Infraestructure.Persistence.Usuario
{
    public class CreateUsuarioDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El email es obligatorio")]
        public string Email { get; set; }
        [Required(ErrorMessage = "La contrasenia es obligatoria")]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
