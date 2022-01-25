using Project.DAL;
using Project.Common;
using Project.Model.DTOs;
using Project.Model.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Project.Model.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Common.Enums;

namespace Project.Repository.Common.Interfaces
{
    public interface IModelRepo/* : IVehicleServiceRepoBase<VehicleModel>*/
    {
        Task<PagedList<IVehicleModelDTO>> GetAllVehicleModelsAsync(ModelParams makeParams, Include include = Include.No);
        Task<IVehicleModelDTO> GetVehicleModelByIdAsync(int VehicleModelId);

        Task<int> CreateVehicleModel(IVehicleModelCreateDTO vehicleModel);
        //Task<int> UpdateVehicleModel(IVehicleModelUpdateDTO vehicleModelUpdateDTO);
        Task UpdateVehicleModel(IVehicleModelUpdateDTO vehicleModelUpdateDTO);

        Task DeleteVehicleModel(IVehicleModelDTO VehicleModel);
    }
}
