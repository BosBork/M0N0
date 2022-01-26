using Project.Common;
using System.Threading.Tasks;
using Project.Common.Enums;
using Project.Model.Common.Query.Model;
using Project.Model.DTOs.Common;

namespace Project.Repository.Common.Interfaces
{
    public interface IModelRepo/* : IVehicleServiceRepoBase<VehicleModel>*/
    {
        Task<PagedList<IVehicleModelDTO>> GetAllVehicleModelsAsync(IModelParams makeParams, Include include = Include.No);
        Task<IVehicleModelDTO> GetVehicleModelByIdAsync(int VehicleModelId);

        Task<int> CreateVehicleModel(IVehicleModelCreateDTO vehicleModel);
        Task UpdateVehicleModel(IVehicleModelUpdateDTO vehicleModelUpdateDTO);

        Task DeleteVehicleModel(IVehicleModelDTO VehicleModel);
    }
}
