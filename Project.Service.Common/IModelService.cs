using Project.Common;
using Project.Common.Enums;
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
        Task<PagedList<IVehicleModelDTO>> GetAllVehicleModelsAsync(ModelParams makeParams, Include include = Include.No);
        Task<IVehicleModelDTO> GetVehicleModelByIdAsync(int VehicleModelId);

        Task<int> CreateVehicleModel(IVehicleModelCreateDTO vehicleModel);
        Task/*<int> */UpdateVehicleModel(IVehicleModelUpdateDTO vehicleModelUpdateDTO);

        Task DeleteVehicleModel(IVehicleModelDTO VehicleModel);
    }
}
