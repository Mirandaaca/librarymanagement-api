using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistence.Context
{
    public class LibraryContext : IdentityDbContext<Usuario>
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Autor> Autores { get; set; }
        public virtual DbSet<Libro> Libros { get; set; }
        public virtual DbSet<LibroxAutor> LibrosxAutores { get; set; }
        public virtual DbSet<Idioma> Idiomas { get; set; }
        public virtual DbSet<Editorial> Editoriales { get; set; }
        public virtual DbSet<LibroxTema> LibrosxTemas { get; set; }
        public virtual DbSet<Tema> Temas { get; set; }
        public virtual DbSet<TipoLibro> TiposLibro { get; set; }
        public virtual DbSet<Ejemplar> Ejemplares { get; set; }
        public virtual DbSet<Prestamo> Prestamos { get; set; }
        public virtual DbSet<PrestamoDetalle> PrestamosDetalles { get; set; }
        public virtual DbSet<Persona> Personas { get; set; }
        public virtual DbSet<Origen> Origenes { get; set; }
        public virtual DbSet<Carrera> Carreras { get; set; }
        public virtual DbSet<Materia> Materias { get; set; }
        public virtual DbSet<CarreraxMateria> CarrerasxMaterias { get; set; }
        public virtual DbSet<Documento> Documentos { get; set; }
    }
}
