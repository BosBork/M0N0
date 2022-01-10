using Project.Common;
using Project.DAL;
using Project.Model.Common;
using Project.Model.OtherModels.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Common
{
    public interface IModelService
    {
        Task<PagedList<IVehicleModelDTO>> GetAllVehicleModelsAsync(ModelParams makeParams);
        Task<IVehicleModelDTO> GetVehicleModelByIdAsync(int VehicleModelId);

        Task<IVehicleModelCreateDTO> CreateVehicleModel(IVehicleModelCreateDTO vehicleModel);
        Task<IVehicleModelUpdateDTO> UpdateVehicleModel(IVehicleModelUpdateDTO VehicleModel);
        Task DeleteVehicleModel(IVehicleModelDTO VehicleModel);
    }
}
