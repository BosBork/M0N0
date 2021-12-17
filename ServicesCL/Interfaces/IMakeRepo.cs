using EntitiesCL.EFModels;
using EntitiesCL.Helpers;
using EntitiesCL.OtherModels.Query;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServicesCL.Interfaces
{
    public interface IMakeRepo : IVehicleServiceRepoBase<VehicleMake>
    {
        Task<List<SelectListItem>> GetAllMakesForDPSelectListItem();
        Task<PagedList<VehicleMake>> GetAllVehicleMakesAsync(MakeParams makeParams);
        Task<VehicleMake> GetVehicleMakeByIdAsync(int vehicleMakeId);
        Task<VehicleMake> GetModelsOfVehicleByIdAsync(int vehicleMakeId);

        void CreateVehicleMake(VehicleMake vehicleMake);
        void UpdateVehicleMake(VehicleMake vehicleMake);
        void DeleteVehicleMake(VehicleMake vehicleMake);

    }
}
