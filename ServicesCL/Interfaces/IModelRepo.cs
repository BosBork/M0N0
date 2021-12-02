using EntitiesCL.EFModels;
using EntitiesCL.Helpers;
using EntitiesCL.OtherModels.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServicesCL.Interfaces
{
    public interface IModelRepo /*: IVehicleServiceRepoBase<VehicleModel>*/
    {
        Task<PagedList<VehicleModel>> GetAllVehicleModelsAsync(ModelParams makeParams);
        Task<VehicleModel> GetVehicleModelByIdAsync(int VehicleModelId);

        void CreateVehicleModel(VehicleModel vehicleModel);
        void UpdateVehicleModel(VehicleModel VehicleModel);
        void DeleteVehicleModel(VehicleModel VehicleModel);
    }
}
