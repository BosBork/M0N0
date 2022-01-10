using Project.DAL;
using Project.Common;
using Project.Model.OtherModels.Query;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Model.Common;
using System.Linq.Expressions;

namespace Project.Repository.Common.Interfaces
{
    public interface IMakeRepo/* : IVehicleServiceRepoBase<VehicleMake>*/
    {
        Task<List<SelectListItem>> GetAllMakesForDPSelectListItem();
        Task<PagedList<IVehicleMakeDTO>> GetAllVehicleMakesAsync(MakeParams makeParams);
        Task<IVehicleMakeDTO> GetVehicleMakeByIdAsync(int vehicleMakeId);
        Task<IVehicleMakeDTO> GetModelsOfVehicleByIdAsync(int vehicleMakeId);

        Task<IVehicleMakeCreateDTO> CreateVehicleMake(IVehicleMakeCreateDTO vehicleMake);
        Task<IVehicleMakeUpdateDTO> UpdateVehicleMake(IVehicleMakeUpdateDTO vehicleMake); 
        Task DeleteVehicleMake(IVehicleMakeDTO vehicleMake);

        Task<bool> FindIfExists(Expression<Func<IVehicleMakeDTO, bool>> selector);
        Task<int[]> FindAllMakeIdsForRandom();
    }
}
