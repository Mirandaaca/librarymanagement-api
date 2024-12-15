using Microsoft.EntityFrameworkCore;
using Project.Application.DTOs.Infraestructure.Persistence.Persona;
using Project.Application.Interfaces.Persistence;
using Project.Domain.Entities;
using Project.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistence.Repository
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly LibraryContext _context;
        public PersonaRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task ActualizarPersona(UpdatePersonaDTO persona)
        {
            Persona objPersona = await _context.Personas.FirstOrDefaultAsync(p => p.Id.Equals(persona.Id));
            if (objPersona == null)
            {
                throw new Exception("La persona no existe");
            }
            else
            {
                objPersona.Nombre = persona.Nombre;
                objPersona.Apellido = persona.Apellido;
                objPersona.Registro = persona.Registro;
                objPersona.Cedula = persona.Cedula;
                objPersona.Sexo = persona.Sexo;
                objPersona.Telefono = persona.Telefono;
                objPersona.Correo = persona.Correo;
                objPersona.FechaDeNacimiento = persona.FechaDeNacimiento;
                _context.Personas.Update(objPersona);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CrearPersona(CreatePersonaDTO persona)
        {
            Persona objPersona = new Persona
            {
                Nombre = persona.Nombre,
                Apellido = persona.Apellido,
                Cedula = persona.Cedula,
                Registro = persona.Registro,
                Sexo = persona.Sexo,
                Telefono = persona.Telefono,
                Correo = persona.Correo,
                FechaDeNacimiento = persona.FechaDeNacimiento,
            };
            _context.Personas.Add(objPersona);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarPersona(int id)
        {
            Persona objPersona = await _context.Personas.FirstOrDefaultAsync(p => p.Id.Equals(id));
            if (objPersona == null)
            {
                throw new Exception("La persona no existe");
            }
            else
            {
                _context.Personas.Remove(objPersona);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ReadPersonaDTO> ObtenerPersonaPorId(int id)
        {
            Persona objPersona = await _context.Personas.FirstOrDefaultAsync(p => p.Id.Equals(id));
            ReadPersonaDTO personaDTO = new ReadPersonaDTO
            {
                Id = objPersona.Id,
                Nombre = objPersona.Nombre,
                Apellido = objPersona.Apellido,
                Registro = objPersona.Registro,
                Cedula = objPersona.Cedula,
                Sexo = objPersona.Sexo,
                Telefono = objPersona.Telefono,
                Correo = objPersona.Correo,
                FechaDeNacimiento = objPersona.FechaDeNacimiento,
            };
            return personaDTO;
        }

        public async Task<List<ReadPersonaDTO>> ObtenerPersonas()
        {
            List<Persona> listaPersonas = await _context.Personas.ToListAsync();
            List<ReadPersonaDTO> listaPersonasDTO = new List<ReadPersonaDTO>();
            foreach (var persona in listaPersonas)
            {
                ReadPersonaDTO personaDTO = new ReadPersonaDTO
                {
                    Id = persona.Id,
                    Nombre = persona.Nombre,
                    Apellido = persona.Apellido,
                    Registro = persona.Registro,
                    Cedula = persona.Cedula,
                    Sexo = persona.Sexo,
                    Telefono = persona.Telefono,
                    Correo = persona.Correo,
                    FechaDeNacimiento = persona.FechaDeNacimiento,
                };
                listaPersonasDTO.Add(personaDTO);
            }
            return listaPersonasDTO;
        }
    }
}
