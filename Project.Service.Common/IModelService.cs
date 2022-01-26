using Project.Common;
using Project.Common.Enums;
using Project.Model.Common.Query.Model;
using Project.Model.DTOs.Common;
using System.Threading.Tasks;

namespace Project.Service.Common
{
    public interface IModelService
    {
        Task<PagedList<IVehicleModelDTO>> GetAllVehicleModelsAsync(IModelParams makeParams, Include include = Include.No);
        Task<IVehicleModelDTO> GetVehicleModelByIdAsync(int VehicleModelId);

        Task<int> CreateVehicleModel(IVehicleModelCreateDTO vehicleModel);
        Task UpdateVehicleModel(IVehicleModelUpdateDTO vehicleModelUpdateDTO);

        Task DeleteVehicleModel(IVehicleModelDTO VehicleModel);
    }
}
