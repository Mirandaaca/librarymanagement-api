using Microsoft.EntityFrameworkCore;
using Project.Application.DTOs.Infraestructure.Persistence.Autor;
using Project.Application.DTOs.Infraestructure.Persistence.Ejemplar;
using Project.Application.DTOs.Infraestructure.Persistence.Libro;
using Project.Application.DTOs.Infraestructure.Persistence.Tema;
using Project.Application.Interfaces.Persistence;
using Project.Domain.Entities;
using Project.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistence.Repository
{
    public class LibroRepository : ILibroRepository
    {
        private readonly LibraryContext _context;
        public LibroRepository(LibraryContext context)
        {
            _context = context;
        }
        public async Task ActualizarLibro(UpdateLibroDTO libro)
        {
            // Buscar el libro
            Libro objLibro = await _context.Libros
                .Include(l => l.LibrosxAutores)
                .Include(l => l.LibrosxTemas)
                .FirstOrDefaultAsync(l => l.Id == libro.Id);

            if (objLibro == null)
            {
                throw new Exception("Libro no encontrado");
            }

            // Actualizar los datos principales del libro
            objLibro.IdTipoLibro = libro.IdTipoLibro;
            objLibro.IdIdioma = libro.IdIdioma;
            objLibro.IdEditorial = libro.IdEditorial;
            objLibro.IdCarrera = libro.IdCarrera;
            objLibro.Nombre = libro.Nombre;
            objLibro.Precio = libro.Precio;

            // Actualizar relaciones con autores
            if (libro.IdAutores != null)
            {
                // Eliminar relaciones existentes
                _context.LibrosxAutores.RemoveRange(objLibro.LibrosxAutores);

                // Agregar nuevas relaciones
                foreach (var idAutor in libro.IdAutores)
                {
                    _context.LibrosxAutores.Add(new LibroxAutor
                    {
                        IdLibro = objLibro.Id,
                        IdAutor = idAutor,
                        Descripcion = "Sin descripcion"
                    });
                }
            }

            // Actualizar relaciones con temas
            if (libro.IdTemas != null)
            {
                // Eliminar relaciones existentes
                _context.LibrosxTemas.RemoveRange(objLibro.LibrosxTemas);

                // Agregar nuevas relaciones
                foreach (var idTema in libro.IdTemas)
                {
                    _context.LibrosxTemas.Add(new LibroxTema
                    {
                        IdLibro = objLibro.Id,
                        IdTema = idTema,
                        Descripcion = "Sin descripcion"
                    });
                }
            }

            // Guardar cambios
            await _context.SaveChangesAsync();
        }


        public async Task CrearLibro(CreateLibroDTO libro)
        {
            // Crear el libro principal
            Libro objLibro = new Libro
            {
                IdTipoLibro = libro.IdTipoLibro,
                IdIdioma = libro.IdIdioma,
                IdEditorial = libro.IdEditorial,
                IdCarrera = libro.IdCarrera,
                Nombre = libro.Nombre,
                Precio = libro.Precio
            };

            _context.Libros.Add(objLibro);
            await _context.SaveChangesAsync(); // Guardar primero para obtener el ID del libro

            // Crear relaciones en LibroxAutor
            if (libro.AutoresIds != null && libro.AutoresIds.Count > 0)
            {
                foreach (int autorId in libro.AutoresIds)
                {
                    LibroxAutor nuevaRelacion = new LibroxAutor
                    {
                        IdLibro = objLibro.Id,
                        IdAutor = autorId,
                        Descripcion = "Sin Descripcion"
                    };
                    _context.LibrosxAutores.Add(nuevaRelacion);
                }
            }

            // Crear relaciones en LibroxTema
            if (libro.TemasIds != null && libro.TemasIds.Count > 0)
            {
                foreach (int temaId in libro.TemasIds)
                {
                    LibroxTema nuevaRelacion = new LibroxTema
                    {
                        IdLibro = objLibro.Id,
                        IdTema = temaId,
                        Descripcion = "Sin Descripcion"
                    };
                    _context.LibrosxTemas.Add(nuevaRelacion);
                }
            }

            await _context.SaveChangesAsync(); // Guardar las relaciones
        }


        public async Task EliminarLibro(int id)
        {
            Libro objLibro = await _context.Libros.FirstOrDefaultAsync(l => l.Id == id);
            if (objLibro == null)
            {
                throw new Exception("Libro no encontrado");
            }
            _context.Libros.Remove(objLibro);
            await _context.SaveChangesAsync();
        }
        public async Task<ReadLibroCompleteInformationDTO> LeerInformacionCompletaDeUnLibroPorId(int id)
        {
            Libro objLibro = await _context.Libros
                //Se obtiene la información completa del tipo libro relacionado al libro
                .Include(l => l.TipoLibro)
                //Se obtiene la información completa del idioma relacionado al libro
                .Include(l => l.Idioma)
                //Se obtiene la información completa de la editorial relacionada al libro
                .Include(l => l.Editorial)
                //Se obtiene la información completa de la carrera relacionada al libro
                .Include(l => l.Carrera)
                //Se obtiene la información completa de los autores relacionados al libro
                .Include(l => l.LibrosxAutores)
                // Se obtiene la información completa del o los autores relacionados al libro 
                .ThenInclude(la => la.Autor)
                //Se obtiene la información completa de los temas relacionados al libro
                .Include(l => l.LibrosxTemas)
                // Se obtiene la información completa del o los temas relacionados al libro
                .ThenInclude(lt => lt.Tema)
                // Solo del libro con ese Id
                .FirstOrDefaultAsync(l => l.Id == id);
            ReadLibroCompleteInformationDTO libroDTO = new ReadLibroCompleteInformationDTO
            {
                Id = objLibro.Id,
                IdTipoLibro = objLibro.IdTipoLibro,
                TipoLibro = objLibro.TipoLibro.Descripcion,
                IdIdioma = objLibro.IdIdioma,
                Idioma = objLibro.Idioma.Descripcion,
                IdEditorial = objLibro.IdEditorial,
                Editorial = objLibro.Editorial.Nombre,
                IdCarrera = objLibro.IdCarrera,
                Carrera = objLibro.Carrera.Nombre,
                SiglaCarrera = objLibro.Carrera.Sigla,
                Nombre = objLibro.Nombre,
                Precio = objLibro.Precio,
                Autores = objLibro.LibrosxAutores.Select(la => new ReadAutorDTO
                {
                    Id = la.Autor.Id,
                    Nombre = la.Autor.Nombre
                }).ToList(),
                Temas = objLibro.LibrosxTemas.Select(lt => new ReadTemaDTO
                {
                    Id = lt.Tema.Id,
                    Descripcion = lt.Tema.Descripcion
                }).ToList()
            };
            return libroDTO;
        }
        public async Task<List<ReadLibroCompleteInformationDTO>> LeerInformacionCompletaDeTodosLosLibros()
        {
            List<Libro> listaLibros = await _context.Libros
                .Include(l => l.TipoLibro)
                .Include(l => l.Idioma)
                .Include(l => l.Editorial)
                .Include(l => l.Carrera)
                .Include(l => l.LibrosxAutores)
                .ThenInclude(la => la.Autor)
                .Include(l => l.LibrosxTemas)
                .ThenInclude(lt => lt.Tema)
                .ToListAsync();
            List<ReadLibroCompleteInformationDTO> listaLibrosDTO = new List<ReadLibroCompleteInformationDTO>();
            foreach (Libro libro in listaLibros)
            {
                ReadLibroCompleteInformationDTO libroDTO = new ReadLibroCompleteInformationDTO
                {
                    Id = libro.Id,
                    IdTipoLibro = libro.IdTipoLibro,
                    TipoLibro = libro.TipoLibro.Descripcion,
                    IdIdioma = libro.IdIdioma,
                    Idioma = libro.Idioma.Descripcion,
                    IdEditorial = libro.IdEditorial,
                    Editorial = libro.Editorial.Nombre,
                    IdCarrera = libro.IdCarrera,
                    Carrera = libro.Carrera.Nombre,
                    SiglaCarrera = libro.Carrera.Sigla,
                    Nombre = libro.Nombre,
                    Precio = libro.Precio,
                    Autores = libro.LibrosxAutores.Select(la => new ReadAutorDTO
                    {
                        Id = la.Id,
                        Nombre = la.Autor.Nombre
                    }).ToList(),  
                    Temas = libro.LibrosxTemas.Select(lt => new ReadTemaDTO
                    {
                        Id = lt.Id,
                        Descripcion = lt.Tema.Descripcion
                    }).ToList()
                };
                listaLibrosDTO.Add(libroDTO);
            }
            return listaLibrosDTO;
        }
        public async Task<ReadLibroDTO> ObtenerLibroPorId(int id)
        {
            Libro objLibro = await _context.Libros.FirstOrDefaultAsync(l => l.Id == id);
            ReadLibroDTO libroDTO = new ReadLibroDTO
            {
                Id = objLibro.Id,
                IdCarrera = objLibro.IdCarrera,
                IdEditorial = objLibro.IdEditorial,
                IdIdioma = objLibro.IdIdioma,
                IdTipoLibro = objLibro.IdTipoLibro,
                Nombre = objLibro.Nombre,
                Precio = objLibro.Precio
            };
            return libroDTO;
        }

        public async Task<List<ReadLibroDTO>> ObtenerLibros()
        {
            List<Libro> listaLibros = await _context.Libros.ToListAsync();
            List<ReadLibroDTO> listaLibrosDTO = new List<ReadLibroDTO>();
            foreach (Libro libro in listaLibros)
            {
                ReadLibroDTO libroDTO = new ReadLibroDTO
                {
                    Id = libro.Id,
                    IdCarrera = libro.IdCarrera,
                    IdEditorial = libro.IdEditorial,
                    IdIdioma = libro.IdIdioma,
                    IdTipoLibro = libro.IdTipoLibro,
                    Nombre = libro.Nombre,
                    Precio = libro.Precio
                };
                listaLibrosDTO.Add(libroDTO);
            }
            return listaLibrosDTO;
        }

        public async Task<ReadAutoresxLibro> ObtenerAutoresDeUnLibroPorId(int id)
        {
            Libro objLibro = await _context.Libros.Include(l => l.LibrosxAutores).ThenInclude(la => la.Autor).FirstOrDefaultAsync(l => l.Id == id);
            ReadAutoresxLibro libroDTO = new ReadAutoresxLibro
            {
                IdLibro = objLibro.Id,
                Autores = objLibro.LibrosxAutores.Select(la => new ReadAutorDTO
                {
                    Id = la.Autor.Id,
                    Nombre = la.Autor.Nombre
                }).ToList()
            };
            return libroDTO;
        }

        public async Task<ReadTemasxLibro> ObtenerTemasDeUnLibroPorId(int id)
        {
            Libro objLibro = await _context.Libros.Include(l => l.LibrosxTemas).ThenInclude(lt => lt.Tema).FirstOrDefaultAsync(l => l.Id == id);
            ReadTemasxLibro libroDTO = new ReadTemasxLibro
            {
                IdLibro = objLibro.Id,
                Temas = objLibro.LibrosxTemas.Select(lt => new ReadTemaDTO
                {
                    Id = lt.Tema.Id,
                    Descripcion = lt.Tema.Descripcion
                }).ToList()
            };
            return libroDTO;
        }

        public async Task AgregarAutoresAUnLibro(AddAutoresAUnLibro autores)
        {
            // Validar que el libro existe
            var libro = await _context.Libros.FirstOrDefaultAsync(l => l.Id == autores.IdLibro);
            if (libro == null)
            {
                throw new Exception("El libro no existe");
            }

            // Verificar que los autores existen
            var autoresExistentes = await _context.Autores
                .Where(a => autores.IdAutores.Contains(a.Id))
                .ToListAsync();

            if (autoresExistentes.Count != autores.IdAutores.Count)
            {
                throw new Exception("Algunos autores no existen");
            }

            // Agregar autores a la tabla intermedia
            foreach (var idAutor in autores.IdAutores)
            {
                var relacionExistente = await _context.LibrosxAutores
                    .FirstOrDefaultAsync(la => la.IdLibro == autores.IdLibro && la.IdAutor == idAutor);

                if (relacionExistente == null) // Evitar duplicados
                {
                    LibroxAutor nuevaRelacion = new LibroxAutor
                    {
                        IdLibro = autores.IdLibro,
                        IdAutor = idAutor,
                        Descripcion = "Sin Descripcion"
                    };
                    _context.LibrosxAutores.Add(nuevaRelacion);
                }
            }

            await _context.SaveChangesAsync(); // Guardar cambios
        }
     

        public async Task AgregarTemasAUnLibro(AddTemasAUnLibro temas)
        {
            // Validar que el libro existe
            var libro = await _context.Libros.FirstOrDefaultAsync(l => l.Id == temas.IdLibro);
            if (libro == null)
            {
                throw new Exception("El libro no existe");
            }

            // Verificar que los temas existen
            var temasExistentes = await _context.Temas
                .Where(t => temas.IdTemas.Contains(t.Id))
                .ToListAsync();

            if (temasExistentes.Count != temas.IdTemas.Count)
            {
                throw new Exception("Algunos temas no existen");
            }

            // Agregar temas a la tabla intermedia
            foreach (var idTema in temas.IdTemas)
            {
                var relacionExistente = await _context.LibrosxTemas
                    .FirstOrDefaultAsync(lt => lt.IdLibro == temas.IdLibro && lt.IdTema == idTema);

                if (relacionExistente == null) // Evitar duplicados
                {
                    LibroxTema nuevaRelacion = new LibroxTema
                    {
                        IdLibro = temas.IdLibro,
                        IdTema = idTema,
                        Descripcion = "Sin Descripcion"
                    };
                    _context.LibrosxTemas.Add(nuevaRelacion);
                }
            }

            await _context.SaveChangesAsync(); // Guardar cambios
        }

        public async Task<List<ReadAutorDTO>> ObtenerAutoresNoRelacionadosPorLibro(int id)
        {
            var autoresNoRelacionados = await _context.Autores
         .Where(a => !_context.LibrosxAutores
             .Any(la => la.IdAutor == a.Id && la.IdLibro == id))
         .Select(a => new ReadAutorDTO
         {
             Id = a.Id,
             Nombre = a.Nombre
         })
         .ToListAsync();
            return autoresNoRelacionados;

        }

        public async Task<List<ReadTemaDTO>> ObtenerTemasNoRelacionadosPorLibro(int id)
        {
            var temasNoRelacionados = await _context.Temas
                    .Where(t => !_context.LibrosxTemas
                        .Any(lt => lt.IdTema == t.Id && lt.IdLibro == id))
                    .Select(t => new ReadTemaDTO
                    {
                        Id = t.Id,
                        Descripcion = t.Descripcion
                    })
                    .ToListAsync();

            return temasNoRelacionados;
        }

        public async Task<List<ReadInformacionParaBuscarLibrosDetalladoDTO>> ObtenerInformacionParaBusquedaDeLibrosDetallado()
        {
            var libros = await _context.Libros
        .Include(l => l.LibrosxAutores) // Relación con autores
        .ThenInclude(la => la.Autor)
        .Include(l => l.LibrosxTemas) // Relación con temas
        .ThenInclude(lt => lt.Tema)
        .Include(l => l.Editorial) // Relación con la editorial
        .Include(l => l.Carrera) // Relación con la carrera (Área)
        .Include(l => l.Ejemplares) // Relación con ejemplares para correlativos y disponibilidad
        .ToListAsync();

            var librosDTO = libros.Select(libro => new ReadInformacionParaBuscarLibrosDetalladoDTO
            {
                Titulo = libro.Nombre,
                Autores = libro.LibrosxAutores.Select(la => la.Autor.Nombre).ToList(),
                Temas = libro.LibrosxTemas.Select(lt => lt.Tema.Descripcion).ToList(),
                Editorial = libro.Editorial?.Nombre ?? "Sin editorial",
                Area = libro.Carrera?.Nombre ?? "Sin área",
                Ejemplares = libro.Ejemplares.Select(e => new ReadEjemplarParaBusquedaDeLibroDetalladoDTO
                {
                    Correlativo = e.Correlativo,
                    Disponibilidad = e.Disponibilidad
                }).ToList() // Proyecta cada ejemplar con su disponibilidad
            }).ToList();

            return librosDTO;
        }

        public async Task<List<ReadInformacionParaBuscarLibrosSimpleDTO>> ObtenerInformacionParaBusquedaDeLibrosSimple()
        {
            var libros = await _context.Libros
        .Include(l => l.LibrosxAutores) // Relación con autores
        .ThenInclude(la => la.Autor)
        .Include(l => l.LibrosxTemas) // Relación con temas
        .ThenInclude(lt => lt.Tema)
        .Include(l => l.Editorial) // Relación con la editorial
        .Include(l => l.Carrera) // Relación con la carrera (Área)
        .Include(l => l.Ejemplares) // Relación con ejemplares para disponibilidad
        .ToListAsync();

            var librosDTO = libros.Select(libro => new ReadInformacionParaBuscarLibrosSimpleDTO
            {
                Titulo = libro.Nombre,
                Autores = libro.LibrosxAutores.Select(la => la.Autor.Nombre).ToList(),
                Correlativo = libro.Ejemplares.FirstOrDefault()?.Correlativo ?? "Sin ejemplares",
                Editorial = libro.Editorial?.Nombre ?? "Sin editorial",
                Area = libro.Carrera?.Nombre ?? "Sin área",
                Temas = libro.LibrosxTemas.Select(lt => lt.Tema.Descripcion).ToList(),
                Disponibilidad = libro.Ejemplares.Any(e => e.Disponibilidad) // Al menos un ejemplar disponible
            }).ToList();

            return librosDTO;
        }

        public async Task<List<ReadReporteLibrosDTO>> LeerInformacionCompletaDeTodosLosLibrosParaReporte()
        {
            // Obtener la lista de libros con todas sus relaciones
            List<Libro> listaLibros = await _context.Libros
                .Include(l => l.TipoLibro)
                .Include(l => l.Idioma)
                .Include(l => l.Editorial)
                .Include(l => l.Carrera)
                .Include(l => l.LibrosxAutores)
                .ThenInclude(la => la.Autor)
                .Include(l => l.LibrosxTemas)
                .ThenInclude(lt => lt.Tema)
                .Include(l => l.Ejemplares) // Incluir los ejemplares asociados al libro
                .ToListAsync();

            // Convertir la lista de libros a DTOs
            List<ReadReporteLibrosDTO> listaLibrosDTO = new List<ReadReporteLibrosDTO>();
            foreach (Libro libro in listaLibros)
            {
                ReadReporteLibrosDTO libroDTO = new ReadReporteLibrosDTO
                {
                    Id = libro.Id,
                    IdTipoLibro = libro.IdTipoLibro,
                    TipoLibro = libro.TipoLibro.Descripcion,
                    IdIdioma = libro.IdIdioma,
                    Idioma = libro.Idioma.Descripcion,
                    IdEditorial = libro.IdEditorial,
                    Editorial = libro.Editorial.Nombre,
                    IdCarrera = libro.IdCarrera,
                    Carrera = libro.Carrera.Nombre,
                    SiglaCarrera = libro.Carrera.Sigla,
                    Nombre = libro.Nombre,
                    Precio = libro.Precio,
                    Autores = libro.LibrosxAutores.Select(la => new ReadAutorDTO
                    {
                        Id = la.Autor.Id,
                        Nombre = la.Autor.Nombre
                    }).ToList(),
                    Temas = libro.LibrosxTemas.Select(lt => new ReadTemaDTO
                    {
                        Id = lt.Tema.Id,
                        Descripcion = lt.Tema.Descripcion
                    }).ToList(),
                    Ejemplares = libro.Ejemplares.Select(e => new ReadReporteLibroEjemplarDTO
                    {
                        Id = e.Id,
                        Correlativo = e.Correlativo,
                        Clase = e.Clase,
                        Categoria = e.Categoria,
                        Disponible = e.Disponibilidad,
                        Campo = e.Campo
                    }).ToList()
                };
                listaLibrosDTO.Add(libroDTO);
            }
            return listaLibrosDTO;
        }
    }
}
