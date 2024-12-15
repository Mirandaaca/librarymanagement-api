﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.Interfaces.Persistence;
using Project.Persistence.Context;
using Project.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistence
{
    public static class ServiceExtensions
    {
        public static void AddPersistenceInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LibraryContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"))
            );
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IAutorRepository, AutorRepository>();
            services.AddTransient<IEditorialRepository, EditorialRepository>();
            services.AddTransient<IIdiomaRepository, IdiomaRepository>();
            services.AddTransient<ITemaRepository, TemaRepository>();
            services.AddTransient<ITipoLibroRepository, TipoLibroRepository>();
            services.AddTransient<ILibroRepository, LibroRepository>();
            services.AddTransient<ICarreraRepository, CarreraRepository>();
            services.AddTransient<IMateriaRepository, MateriaRepository>();
            services.AddTransient<IOrigenRepository, OrigenRepository>();
            services.AddTransient<IEjemplarRepository, EjemplarRepository>();
            services.AddTransient<IPrestamoRepository, PrestamoRepository>();
            services.AddTransient<IPersonaRepository, PersonaRepository>();
            services.AddTransient<IDocumentoRepository, DocumentoRepository>();

        }
    }
}
