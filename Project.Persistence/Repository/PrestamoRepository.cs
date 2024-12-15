using Microsoft.EntityFrameworkCore;
using Project.Application.DTOs.Infraestructure.Persistence.Ejemplar;
using Project.Application.DTOs.Infraestructure.Persistence.Persona;
using Project.Application.DTOs.Infraestructure.Persistence.Prestamo;
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
    public class PrestamoRepository : IPrestamoRepository
    {
        private readonly LibraryContext _context;
        public PrestamoRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task ActualizarPrestamo(UpdatePrestamoDTO prestamo)
        {
            Prestamo objPrestamo = await _context.Prestamos.FirstOrDefaultAsync(p =>p.Id == prestamo.Id);
            if (objPrestamo == null)
            {
                throw new Exception("Préstamo no encontrado.");
            }

            objPrestamo.Nombre = prestamo.Nombre;
            objPrestamo.Descripcion = prestamo.Descripcion;

            _context.Prestamos.Update(objPrestamo);
            await _context.SaveChangesAsync();
        }

        public async Task AgregarEjemplaresAlPrestamo(AddEjemplaresToPrestamoDTO ejemplares)
        {
            // Validar si el préstamo existe
            Prestamo prestamo = await _context.Prestamos
                .Include(p => p.PrestamoDetalles) // Incluye los detalles actuales del préstamo
                .FirstOrDefaultAsync(p => p.Id == ejemplares.IdPrestamo);

            if (prestamo == null)
            {
                throw new Exception("El préstamo no existe.");
            }

            // Validar que el préstamo no haya sido cerrado (devuelto)
            if (prestamo.Estado)
            {
                throw new Exception("No se pueden agregar ejemplares a un préstamo ya devuelto.");
            }

            // Validar que los ejemplares existen y están disponibles
            var nuevosEjemplares = await _context.Ejemplares
                .Where(e => ejemplares.EjemplaresIds.Contains(e.Id) && e.Disponibilidad)
                .ToListAsync();

            if (nuevosEjemplares.Count != ejemplares.EjemplaresIds.Count)
            {
                throw new Exception("Uno o más ejemplares no están disponibles o no existen.");
            }

            // Evitar duplicados: verificar si algún ejemplar ya está asociado al préstamo
            var ejemplaresYaEnPrestamo = prestamo.PrestamoDetalles
                .Where(pd => ejemplares.EjemplaresIds.Contains(pd.IdEjemplar))
                .Select(pd => pd.IdEjemplar)
                .ToList();

            if (ejemplaresYaEnPrestamo.Any())
            {
                throw new Exception($"Algunos ejemplares ya están en el préstamo: {string.Join(", ", ejemplaresYaEnPrestamo)}");
            }

            // Agregar nuevos ejemplares al préstamo
            foreach (var ejemplar in nuevosEjemplares)
            {
                var nuevoDetalle = new PrestamoDetalle
                {
                    IdPrestamo = prestamo.Id,
                    IdEjemplar = ejemplar.Id,
                    FechaPrestamo = DateTime.Now,
                    FechaDevolucion = null, // Aún no devuelto
                    PrecioPrestamo = 0 // Ajustar si es necesario calcular el precio
                };

                // Marcar el ejemplar como no disponible
                ejemplar.Disponibilidad = false;

                // Agregar el nuevo detalle al préstamo
                _context.PrestamosDetalles.Add(nuevoDetalle);
            }

            // Guardar cambios
            await _context.SaveChangesAsync();
        }

        public async Task CrearPrestamo(CreatePrestamoDTO prestamoDTO)
        {
            // Validar si la persona tiene préstamos activos (no devueltos)
            var prestamosActivos = await _context.Prestamos
                .Include(p => p.PrestamoDetalles)
                .Where(p => p.IdPersona == prestamoDTO.IdPersona && !p.Estado)
                .ToListAsync();

            if (prestamosActivos.Any())
            {
                throw new Exception("La persona tiene préstamos activos y no puede realizar un nuevo préstamo.");
            }

            // Validar que los ejemplares están disponibles
            var ejemplares = await _context.Ejemplares
                .Where(e => prestamoDTO.EjemplaresIds.Contains(e.Id) && e.Disponibilidad)
                .ToListAsync();

            if (ejemplares.Count != prestamoDTO.EjemplaresIds.Count)
            {
                throw new Exception("Uno o más ejemplares no están disponibles.");
            }

            // Crear el préstamo
            var nuevoPrestamo = new Prestamo
            {
                IdPersona = prestamoDTO.IdPersona,
                Nombre = prestamoDTO.Nombre,
                FechaPrestamo = DateTime.Now,
                Descripcion = prestamoDTO.Descripcion,
                Estado = false, // No devuelto inicialmente
                PrestamoDetalles = ejemplares.Select(e => new PrestamoDetalle
                {
                    IdEjemplar = e.Id,
                    FechaPrestamo = DateTime.Now,
                    FechaDevolucion = null, // Aún no devuelto
                    PrecioPrestamo = 100  // Modificar si se requiere cálculo de precio
                }).ToList()
            };

            // Marcar los ejemplares como no disponibles
            foreach (var ejemplar in ejemplares)
            {
                ejemplar.Disponibilidad = false;
            }

            // Guardar cambios
            _context.Prestamos.Add(nuevoPrestamo);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarPrestamo(int id)
        {
            Prestamo prestamo = await _context.Prestamos
        .Include(p => p.PrestamoDetalles)
        .FirstOrDefaultAsync(p => p.Id == id);

            if (prestamo == null)
            {
                throw new Exception("Préstamo no encontrado.");
            }

            // Liberar los ejemplares asociados
            foreach (var detalle in prestamo.PrestamoDetalles)
            {
                var ejemplar = await _context.Ejemplares.FindAsync(detalle.IdEjemplar);
                if (ejemplar != null)
                {
                    ejemplar.Disponibilidad = true; // Marcar como disponible
                }
            }

            _context.Prestamos.Remove(prestamo);
            await _context.SaveChangesAsync();
        }

        public async Task MarcarPrestamoComoDevuelto(int idPrestamo)
        {
            // Obtener el préstamo con sus detalles y ejemplares asociados
            Prestamo prestamo = await _context.Prestamos
                .Include(p => p.PrestamoDetalles)
                .ThenInclude(pd => pd.Ejemplar)
                .FirstOrDefaultAsync(p => p.Id == idPrestamo);

            if (prestamo == null)
            {
                throw new Exception("Préstamo no encontrado.");
            }

            // Validar si ya está marcado como devuelto
            if (prestamo.Estado)
            {
                throw new Exception("El préstamo ya fue marcado como devuelto.");
            }

            // Marcar todos los ejemplares como devueltos
            foreach (var detalle in prestamo.PrestamoDetalles)
            {
                if (detalle.FechaDevolucion == null) // Solo actualizar los que no tengan FechaDevolucion
                {
                    detalle.FechaDevolucion = DateTime.Now; // Marcar como devuelto
                    detalle.Ejemplar.Disponibilidad = true; // Cambiar disponibilidad del ejemplar
                }
            }

            // Cambiar el estado del préstamo a devuelto
            prestamo.Estado = true;

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();
        }

        public async Task<ReadPrestamoDetalleDTO> ObtenerDetallePrestamo(int idPrestamo)
        {
            var prestamo = await _context.Prestamos
         .Include(p => p.PrestamoDetalles)
         .ThenInclude(pd => pd.Ejemplar) // Incluye los ejemplares
         .ThenInclude(e => e.Libro)     // Incluye los libros asociados a los ejemplares
         .Include(p => p.Persona)       // Incluye la persona que realizó el préstamo
         .FirstOrDefaultAsync(p => p.Id == idPrestamo);

            if (prestamo == null)
            {
                throw new Exception("Préstamo no encontrado.");
            }

            return new ReadPrestamoDetalleDTO
            {
                Id = prestamo.Id,
                Nombre = prestamo.Nombre,
                FechaPrestamo = prestamo.FechaPrestamo,
                Estado = prestamo.Estado,
                Persona = new ReadPersonaPrestamoDTO
                {
                    Id = prestamo.Persona.Id,
                    NombreCompleto = $"{prestamo.Persona.Nombre} {prestamo.Persona.Apellido}"
                },
                Ejemplares = prestamo.PrestamoDetalles.Select(pd => new ReadEjemplarDePrestamoDetalleDTO
                {
                    IdDetallePrestamo = pd.Id,
                    Correlativo = pd.Ejemplar.Correlativo,
                    FechaPrestamo = pd.FechaPrestamo,
                    FechaDevolucion = pd.FechaDevolucion,
                    PrecioPrestamo = pd.PrecioPrestamo,
                    TituloLibro = pd.Ejemplar.Libro.Nombre // Agrega el título del libro
                }).ToList()
            };
        }

        public async Task<List<ReadEjemplarParaPrestamoDTO>> ObtenerEjemplaresDeUnPrestamo(int idPrestamo)
        {
            // Validar si el préstamo existe
            Prestamo prestamo = await _context.Prestamos
        .Include(p => p.PrestamoDetalles)
        .ThenInclude(pd => pd.Ejemplar)
        .ThenInclude(e => e.Libro) // Incluye los libros asociados
        .FirstOrDefaultAsync(p => p.Id == idPrestamo);

            if (prestamo == null)
            {
                throw new Exception("Préstamo no encontrado.");
            }

            // Obtener los ejemplares del préstamo
            var ejemplares = prestamo.PrestamoDetalles.Select(pd => pd.Ejemplar).ToList();

            // Proyectar a DTO
            return ejemplares.Select(e => new ReadEjemplarParaPrestamoDTO
            {
                Id = e.Id,
                Titulo = e.Libro.Nombre,
                Correlativo = e.Correlativo,
                Clase = e.Clase,
                Categoria = e.Categoria,
                Disponible = e.Disponibilidad
            }).ToList();
        }

        public async Task<List<ReadEjemplarParaPrestamoDTO>> ObtenerEjemplaresNoAsociadosAUnPrestamo()
        {
            // Buscar ejemplares asociados a préstamos activos (Estado = false)
            var ejemplaresEnPrestamosActivos = await _context.PrestamosDetalles
                .Where(pd => !pd.Prestamo.Estado) // Solo préstamos activos
                .Select(pd => pd.IdEjemplar)
                .ToListAsync();

            // Obtener ejemplares disponibles (no asociados a ningún préstamo activo)
            var ejemplaresDisponibles = await _context.Ejemplares
                .Include(e => e.Libro) // Incluir el libro asociado
                .Where(e => !ejemplaresEnPrestamosActivos.Contains(e.Id) && e.Disponibilidad)
                .ToListAsync();

            // Proyectar a DTO
            return ejemplaresDisponibles.Select(e => new ReadEjemplarParaPrestamoDTO
            {
                Id = e.Id,
                Correlativo = e.Correlativo,
                Clase = e.Clase,
                Categoria = e.Categoria,
                Disponible = e.Disponibilidad,
                Titulo = e.Libro.Nombre // Agregar el título del libro
            }).ToList();
        }

        public async Task<ReadPrestamoDetallePorId> ObtenerPrestamoPorId(int idPrestamo)
        {
            var prestamo = await _context.Prestamos
        .Include(p => p.PrestamoDetalles)
        .ThenInclude(pd => pd.Ejemplar) // Incluye los ejemplares prestados
        .Include(p => p.Persona) // Incluye la persona que realizó el préstamo
        .FirstOrDefaultAsync(p => p.Id == idPrestamo);

            if (prestamo == null)
            {
                throw new Exception("Préstamo no encontrado.");
            }

            return new ReadPrestamoDetallePorId
            {
                Id = prestamo.Id,
                Nombre = prestamo.Nombre,
                FechaPrestamo = prestamo.FechaPrestamo,
                Descripcion = prestamo.Descripcion,
                Estado = prestamo.Estado,
                Persona = new ReadPersonaPrestamoDTO
                {
                    Id = prestamo.Persona.Id,
                    NombreCompleto = $"{prestamo.Persona.Nombre} {prestamo.Persona.Apellido}"
                },
                Ejemplares = prestamo.PrestamoDetalles.Select(pd => new ReadEjemplarDetalleDTO
                {
                    Correlativo = pd.Ejemplar.Correlativo,
                    FechaPrestamo = pd.FechaPrestamo,
                    FechaDevolucion = pd.FechaDevolucion,
                    PrecioPrestamo = pd.PrecioPrestamo
                }).ToList()
            };
        }

        public async Task<List<ReadPrestamoDetalleDTO>> ObtenerPrestamos()
        {
            var prestamos = await _context.Prestamos
        .Include(p => p.PrestamoDetalles)
         .ThenInclude(pd => pd.Ejemplar) // Incluye los ejemplares
         .ThenInclude(e => e.Libro)     // Incluye los libros asociados a los ejemplares
         .Include(p => p.Persona)       // Incluye la persona que realizó el préstamo
        .ToListAsync();

            return prestamos.Select(prestamo => new ReadPrestamoDetalleDTO
            {
                Id = prestamo.Id,
                Nombre = prestamo.Nombre,
                Persona = new ReadPersonaPrestamoDTO
                {
                    Id = prestamo.Persona.Id,
                    NombreCompleto = $"{prestamo.Persona.Nombre} {prestamo.Persona.Apellido}"
                },
                FechaPrestamo = prestamo.FechaPrestamo,
                Estado = prestamo.Estado,
                Ejemplares = prestamo.PrestamoDetalles.Select(pd => new ReadEjemplarDePrestamoDetalleDTO
                {
                    IdDetallePrestamo = pd.Id,
                    Correlativo = pd.Ejemplar.Correlativo,
                    FechaPrestamo = pd.FechaPrestamo,
                    FechaDevolucion = pd.FechaDevolucion,
                    PrecioPrestamo = pd.PrecioPrestamo,
                    TituloLibro = pd.Ejemplar.Libro.Nombre // Agregar el título del libro
                }).ToList()
            }).ToList();
        }

        public async Task<List<ReadPrestamoActivosDTO>> ObtenerPrestamosActivos()
        {
            var prestamosActivos = await _context.Prestamos
        .Include(p => p.Persona) // Incluye la persona asociada al préstamo
        .Include(p => p.PrestamoDetalles) // Incluye los detalles del préstamo
        .ThenInclude(pd => pd.Ejemplar) // Incluye los ejemplares relacionados
        .Where(p => !p.Estado) // Filtrar préstamos no devueltos
        .ToListAsync();

            return prestamosActivos.Select(prestamo => new ReadPrestamoActivosDTO
            {
                Id = prestamo.Id,
                Nombre = prestamo.Nombre,
                Persona = new ReadPersonaPrestamoDTO
                {
                    Id = prestamo.Persona.Id,
                    NombreCompleto = $"{prestamo.Persona.Nombre} {prestamo.Persona.Apellido}"
                },
                FechaPrestamo = prestamo.FechaPrestamo,
                Estado = prestamo.Estado,
                Ejemplares = prestamo.PrestamoDetalles.Select(pd => new ReadEjemplarPrestamoDTO
                {
                    Correlativo = pd.Ejemplar.Correlativo

                }).ToList()
            }).ToList();
        }
        public async Task RegistrarDevolucion(int idPrestamoDetalle)
        {
            var detalle = await _context.PrestamosDetalles
        .Include(pd => pd.Ejemplar) // Incluye el ejemplar asociado
        .Include(pd => pd.Prestamo) // Incluye el préstamo asociado
        .ThenInclude(p => p.PrestamoDetalles) // Incluye todos los detalles del préstamo
        .FirstOrDefaultAsync(pd => pd.Id == idPrestamoDetalle);

            if (detalle == null)
            {
                throw new Exception("Detalle del préstamo no encontrado.");
            }

            if (detalle.FechaDevolucion != null)
            {
                throw new Exception("El ejemplar ya fue devuelto.");
            }

            // Registrar la devolución
            detalle.FechaDevolucion = DateTime.Now;

            // Marcar el ejemplar como disponible
            detalle.Ejemplar.Disponibilidad = true;

            // Verificar si todos los ejemplares del préstamo han sido devueltos
            var prestamo = detalle.Prestamo;
            bool todosDevueltos = prestamo.PrestamoDetalles.All(pd => pd.FechaDevolucion != null);

            if (todosDevueltos)
            {
                prestamo.Estado = true; // Marcar el préstamo como devuelto
            }

            await _context.SaveChangesAsync();
        }

        public async Task<VerificarPrestamosActivosResponseDTO> VerificarPrestamosActivos(int idPersona)
        {
            // Buscar el primer préstamo activo de la persona
            Prestamo prestamoActivo = await _context.Prestamos
                .Where(p => p.IdPersona == idPersona && !p.Estado)
                .FirstOrDefaultAsync();

            // Construir la respuesta
            VerificarPrestamosActivosResponseDTO response = new VerificarPrestamosActivosResponseDTO
            {
                TienePrestamosActivos = prestamoActivo != null,
                PrestamoActivo = prestamoActivo != null ? new PrestamoActivoDTO
                {
                    IdPrestamo = prestamoActivo.Id,
                    Nombre = prestamoActivo.Nombre,
                    FechaPrestamo = prestamoActivo.FechaPrestamo,
                    Descripcion = prestamoActivo.Descripcion
                } : null // Devolver null si no hay préstamos activos
            };

            return response;
        }

    }
}
