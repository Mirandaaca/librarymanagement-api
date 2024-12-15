using Project.Application.DTOs.Infraestructure.Persistence.Ejemplar;
using Project.Application.DTOs.Infraestructure.Persistence.Prestamo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Interfaces.Persistence
{
    public interface IPrestamoRepository
    {
        public Task CrearPrestamo(CreatePrestamoDTO prestamo);
        public Task ActualizarPrestamo(UpdatePrestamoDTO prestamo);
        public Task EliminarPrestamo(int id);
        public Task<List<ReadPrestamoActivosDTO>> ObtenerPrestamosActivos();
        public Task<List<ReadPrestamoDetalleDTO>> ObtenerPrestamos();
        public Task<ReadPrestamoDetallePorId> ObtenerPrestamoPorId(int id);
        public Task AgregarEjemplaresAlPrestamo(AddEjemplaresToPrestamoDTO ejemplares);
        public Task RegistrarDevolucion(int idPrestamoDetalle);
        public Task<ReadPrestamoDetalleDTO> ObtenerDetallePrestamo(int idPrestamo);
        public Task<List<ReadEjemplarParaPrestamoDTO>> ObtenerEjemplaresDeUnPrestamo(int idPrestamo);
        public Task<VerificarPrestamosActivosResponseDTO> VerificarPrestamosActivos(int idPersona);
        public Task MarcarPrestamoComoDevuelto(int idPrestamo);
        public Task<List<ReadEjemplarParaPrestamoDTO>> ObtenerEjemplaresNoAsociadosAUnPrestamo();
    }
}
