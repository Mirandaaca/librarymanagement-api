using Project.Application.DTOs.Infraestructure.Persistence.Ejemplar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Interfaces.Persistence
{
    public interface IEjemplarRepository
    {
        public Task CrearEjemplar(CreateEjemplarDTO ejemplar);
        public Task ActualizarEjemplar(UpdateEjemplarDTO ejemplar);
        public Task EliminarEjemplar(int id);
        public Task<ReadEjemplarCompleteInformationDTO> ObtenerInformacionCompletaDeEjemplarPorId(int id);
        public Task<List<ReadEjemplarCompleteInformationDTO>> ObtenerInformacionCompletaDeEjemplares();
        public Task<List<ReadInformacionParaBuscarEjemplaresDTO>> ObtenerInformacionParaBusquedaPorEjemplares();
        public Task<VerificarEjemplarEnPrestamoActivoResponseDTO> VerificarEjemplarEnPrestamoActivo(int idEjemplar);
        public Task<ReadEjemplarDTO> ObtenerEjemplarPorId(int id);
        public Task<List<ReadEjemplarDTO>> ObtenerEjemplares();
    }
}
