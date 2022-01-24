using Project.DAL;
using Project.Common;
using Project.Model.OtherModels.Query;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Model.Common;
using System.Linq.Expressions;
using Project.Common.Enums;

namespace Project.Repository.Common.Interfaces
{
    public interface IMakeRepo/* : IVehicleServiceRepoBase<VehicleMake>*/
    {
        Task<List<SelectListItem>> GetAllMakesForDPSelectListItem();
        Task<PagedList<IVehicleMakeDTO>> GetAllVehicleMakesAsync(MakeParams makeParams, Include include = Include.No);
        Task<IVehicleMakeDTO> GetVehicleMakeByIdAsync(int vehicleMakeId);
        Task<IVehicleMakeDTO> GetVehicleMakeByIdWithModelsAsync(int vehicleMakeId);
        //Task<IVehicleMakeDTO> GetVehicleMakeByIdWithModelsCountAsync(int vehicleMakeId);

        Task<int> CreateVehicleMake(IVehicleMakeCreateDTO vehicleMake);
        Task/*<int>*/ UpdateVehicleMake(IVehicleMakeUpdateDTO vehicleMake); 
        Task DeleteVehicleMake(IVehicleMakeDTO vehicleMake);

        Task<bool> FindIfExists(Expression<Func<IVehicleMakeDTO, bool>> selector);
        Task<int[]> FindAllMakeIdsForRandom();
    }
}
