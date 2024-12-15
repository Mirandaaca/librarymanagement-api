using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Project.Application.DTOs.Infraestructure.Persistence.Autor;
using Project.Application.DTOs.Infraestructure.Persistence.Ejemplar;
using Project.Application.DTOs.Infraestructure.Persistence.Prestamo;
using Project.Application.DTOs.Infraestructure.Persistence.Tema;
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
    public class EjemplarRepository : IEjemplarRepository
    {
        private readonly LibraryContext _context;
        public EjemplarRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task ActualizarEjemplar(UpdateEjemplarDTO ejemplar)
        {
            Ejemplar objEjemplar = await _context.Ejemplares.FirstOrDefaultAsync(e => e.Id.Equals(ejemplar.Id));
            if (objEjemplar == null)
            {
                throw new Exception("El ejemplar no existe");
            }
            else
            {
                objEjemplar.IdLibro = ejemplar.IdLibro;
                objEjemplar.IdOrigen = ejemplar.IdOrigen;
                objEjemplar.Correlativo = ejemplar.Correlativo;
                objEjemplar.Clase = ejemplar.Clase;
                objEjemplar.Categoria = ejemplar.Categoria;
                objEjemplar.Campo = ejemplar.Campo;
                objEjemplar.Disponibilidad = ejemplar.Disponibilidad;
                _context.Ejemplares.Update(objEjemplar);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CrearEjemplar(CreateEjemplarDTO ejemplar)
        {
            Ejemplar objEjemplar = new Ejemplar
            {
                IdLibro = ejemplar.IdLibro,
                IdOrigen = ejemplar.IdOrigen,
                Correlativo = ejemplar.Correlativo,
                Clase = ejemplar.Clase,
                Categoria = ejemplar.Categoria,
                Campo = ejemplar.Campo,
                Disponibilidad = true
            };
            _context.Ejemplares.Add(objEjemplar);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarEjemplar(int id)
        {
            Ejemplar objEjemplar = await _context.Ejemplares.FirstOrDefaultAsync(e=>e.Id.Equals(id));
            if (objEjemplar == null)
            {
                throw new Exception("El ejemplar no existe");
            }
            else
            {
                _context.Ejemplares.Remove(objEjemplar);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ReadEjemplarDTO>> ObtenerEjemplares()
        {
            List<Ejemplar> listaEjemplares = await _context.Ejemplares.ToListAsync();
            List<ReadEjemplarDTO> listaEjemplaresDTO = new List<ReadEjemplarDTO>();
            foreach (Ejemplar ejemplar in listaEjemplares)
            {
                ReadEjemplarDTO ejemplarDTO = new ReadEjemplarDTO
                {
                    Id = ejemplar.Id,
                    IdLibro = ejemplar.IdLibro,
                    IdOrigen = ejemplar.IdOrigen,
                    Correlativo = ejemplar.Correlativo,
                    Clase = ejemplar.Clase,
                    Categoria = ejemplar.Categoria,
                    Campo = ejemplar.Campo,
                    Disponibilidad = ejemplar.Disponibilidad
                };
                listaEjemplaresDTO.Add(ejemplarDTO);
            }
            return listaEjemplaresDTO;
        }

        public async Task<ReadEjemplarDTO> ObtenerEjemplarPorId(int id)
        {
            Ejemplar objEjemplar = await _context.Ejemplares.FirstOrDefaultAsync(e => e.Id.Equals(id));
            ReadEjemplarDTO ejemplarDTO = new ReadEjemplarDTO
            {
                Id = objEjemplar.Id,
                IdLibro = objEjemplar.IdLibro,
                IdOrigen = objEjemplar.IdOrigen,
                Correlativo = objEjemplar.Correlativo,
                Clase = objEjemplar.Clase,
                Categoria = objEjemplar.Categoria,
                Campo = objEjemplar.Campo,
                Disponibilidad = objEjemplar.Disponibilidad
            };
            return ejemplarDTO;
        }

        public async Task<List<ReadEjemplarCompleteInformationDTO>> ObtenerInformacionCompletaDeEjemplares()
        {
            List<Ejemplar> listaEjemplares = await _context.Ejemplares
                .Include(e => e.Libro)
                .Include(e => e.Origen)
                .ToListAsync();
            List<ReadEjemplarCompleteInformationDTO> listaEjemplaresDTO = new List<ReadEjemplarCompleteInformationDTO>();
            foreach (Ejemplar ejemplar in listaEjemplares)
            {
                ReadEjemplarCompleteInformationDTO ejemplarDTO = new ReadEjemplarCompleteInformationDTO
                {
                    Id = ejemplar.Id,
                    IdLibro = ejemplar.IdLibro,
                    Titulo = ejemplar.Libro.Nombre,
                    IdOrigen = ejemplar.IdOrigen,
                    Origen = ejemplar.Origen.Descripcion,
                    Correlativo = ejemplar.Correlativo,
                    Clase = ejemplar.Clase,
                    Categoria = ejemplar.Categoria,
                    Campo = ejemplar.Campo,
                    Disponibilidad = ejemplar.Disponibilidad
                };
                listaEjemplaresDTO.Add(ejemplarDTO);
            }
            return listaEjemplaresDTO;
        }

        public async Task<ReadEjemplarCompleteInformationDTO> ObtenerInformacionCompletaDeEjemplarPorId(int id)
        {
            Ejemplar objEjemplar = await _context.Ejemplares
                .Include(e => e.Libro)
                .Include(e => e.Origen)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
            ReadEjemplarCompleteInformationDTO ejemplarDTO = new ReadEjemplarCompleteInformationDTO
            {
                Id = objEjemplar.Id,
                IdLibro = objEjemplar.IdLibro,
                Titulo = objEjemplar.Libro.Nombre,
                IdOrigen = objEjemplar.IdOrigen,
                Origen = objEjemplar.Origen.Descripcion,
                Correlativo = objEjemplar.Correlativo,
                Clase = objEjemplar.Clase,
                Categoria = objEjemplar.Categoria,
                Campo = objEjemplar.Campo,
                Disponibilidad = objEjemplar.Disponibilidad
            };
            return ejemplarDTO;
        }

        public async Task<List<ReadInformacionParaBuscarEjemplaresDTO>> ObtenerInformacionParaBusquedaPorEjemplares()
        {
            var ejemplares = await _context.Ejemplares
        .Include(e => e.Libro) // Relación con el libro
        .ThenInclude(l => l.LibrosxAutores) // Relación con autores
        .ThenInclude(la => la.Autor)
        .Include(e => e.Libro.LibrosxTemas) // Relación con temas
        .ThenInclude(lt => lt.Tema)
        .Include(e => e.Libro.Editorial) // Relación con la editorial
        .Include(e => e.Libro.Carrera) // Relación con la carrera (Área)
        .ToListAsync();

            var ejemplaresDTO = ejemplares.Select(ejemplar => new ReadInformacionParaBuscarEjemplaresDTO
            {
                Titulo = ejemplar.Libro.Nombre,
                Autores = ejemplar.Libro.LibrosxAutores.Select(la => la.Autor.Nombre).ToList(),
                Correlativo = ejemplar.Correlativo, // Correlativo único del ejemplar
                Editorial = ejemplar.Libro.Editorial?.Nombre ?? "Sin editorial",
                Area = ejemplar.Libro.Carrera?.Nombre ?? "Sin área",
                Temas = ejemplar.Libro.LibrosxTemas.Select(lt => lt.Tema.Descripcion).ToList(),
                Disponibilidad = ejemplar.Disponibilidad // Disponibilidad específica del ejemplar
            }).ToList();

            return ejemplaresDTO;
        }

        public async Task<VerificarEjemplarEnPrestamoActivoResponseDTO> VerificarEjemplarEnPrestamoActivo(int idEjemplar)
        {
            // Buscar si el ejemplar está en un préstamo activo
            var prestamoDetalleActivo = await _context.PrestamosDetalles
                .Include(pd => pd.Prestamo) // Incluir el préstamo relacionado
                .FirstOrDefaultAsync(pd => pd.IdEjemplar == idEjemplar && !pd.Prestamo.Estado);

            // Construir la respuesta
            return new VerificarEjemplarEnPrestamoActivoResponseDTO
            {
                EstaEnPrestamoActivo = prestamoDetalleActivo != null,
                Prestamo = prestamoDetalleActivo != null ? new PrestamoActivoDTO
                {
                    IdPrestamo = prestamoDetalleActivo.Prestamo.Id,
                    Nombre = prestamoDetalleActivo.Prestamo.Nombre,
                    FechaPrestamo = prestamoDetalleActivo.Prestamo.FechaPrestamo,
                    Descripcion = prestamoDetalleActivo.Prestamo.Descripcion
                } : null
            };
        }
    }
}
