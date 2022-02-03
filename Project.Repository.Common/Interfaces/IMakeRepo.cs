using Project.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Project.Common.Enums;
using Project.Model.Common.Query.Make;
using Project.Model.DTOs.Common;
using Project.Model.Common;

namespace Project.Repository.Common.Interfaces
{
    public interface IMakeRepo/* : IVehicleServiceRepoBase<VehicleMake>*/
    {
        Task<PagedList<IVehicleMakeDTO>> GetAllVehicleMakesAsync(IMakeFilter makeFilter, IMakeSort makeSort, IPagingParamsBase paging, Include include = Include.No);
        Task<IVehicleMakeDTO> GetVehicleMakeByIdAsync(int vehicleMakeId, Include include = Include.No);

        Task<int> CreateVehicleMake(IVehicleMakeCreateDTO vehicleMake);
        Task UpdateVehicleMake(IVehicleMakeUpdateDTO vehicleMake); 
        Task DeleteVehicleMake(IVehicleMakeDTO vehicleMake);

        Task<bool> FindIfExists(Expression<Func<IVehicleMakeDTO, bool>> selector);
        Task<int[]> FindAllMakeIdsForRandom();
    }
}
