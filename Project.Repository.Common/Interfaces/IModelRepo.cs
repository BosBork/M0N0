using Project.DAL;
using Project.Common;
using Project.Model.OtherModels.DTOs;
using Project.Model.OtherModels.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Project.Model.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project.Repository.Common.Interfaces
{
    public interface IModelRepo/* : IVehicleServiceRepoBase<VehicleModel>*/
    {
        Task<PagedList<IVehicleModelDTO>> GetAllVehicleModelsAsync(ModelParams makeParams);
        Task<IVehicleModelDTO> GetVehicleModelByIdAsync(int VehicleModelId);

        Task<int> CreateVehicleModel(IVehicleModelCreateDTO vehicleModel);
        Task<int> UpdateVehicleModel(IVehicleModelUpdateDTO VehicleModel);
        Task DeleteVehicleModel(IVehicleModelDTO VehicleModel);
    }
}
