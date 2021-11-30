using EntitiesCL.EFModels;
using EntitiesCL.Helpers;
using EntitiesCL.OtherModels.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesCL.Interfaces
{
    public interface IMakeRepo : IVehicleServiceRepoBase<VehicleMake>
    {
        //IEnumerable<VehicleMake> GetVehicleMakes(MakeParams makeParams);
        PagedList<VehicleMake> GetAllVehicleMakes(MakeParams makeParams);
        VehicleMake GetVehicleMakeById(int vehicleMakeId);
        VehicleMake GetVehicleMakesModelsById(int vehicleMakeId);

    }
}
