using EntitiesCL.EFModels;
using EntitiesCL.Helpers;
using EntitiesCL.OtherModels.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServicesCL.Interfaces
{
    public interface IMakeRepo /*: IVehicleServiceRepoBase<VehicleMake>*/
    {
        Task<PagedList<VehicleMake>> GetAllVehicleMakes(MakeParams makeParams);
        VehicleMake GetVehicleMakeById(int vehicleMakeId);
        VehicleMake GetModelsOfVehicleById(int vehicleMakeId);

        void CreateVehicleMake(VehicleMake vehicleMake);
        void UpdateVehicleMake(VehicleMake vehicleMake);
        void DeleteVehicleMake(VehicleMake vehicleMake);

    }
}
