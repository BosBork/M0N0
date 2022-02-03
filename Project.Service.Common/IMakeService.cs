using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Common;
using Project.Common.Enums;
using Project.Model.Common;
using Project.Model.Common.Query.Make;
using Project.Model.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Project.Service.Common
{
    public interface IMakeService/* : IVehicleServiceRepoBase<VehicleMake>*/
    {
        Task<PagedList<IVehicleMakeDTO>> GetAllVehicleMakesAsync(
            IMakeFilter makeFilter, IMakeSort makeSort, IPagingParamsBase paging, Include include = Include.No);
        Task<IVehicleMakeDTO> GetVehicleMakeByIdAsync(int vehicleMakeId, Include include = Include.No);

        Task<int> CreateVehicleMake(IVehicleMakeCreateDTO vehicleMake);
        Task UpdateVehicleMake(IVehicleMakeUpdateDTO vehicleMake);

        Task DeleteVehicleMake(IVehicleMakeDTO vehicleMake);

        Task<bool> FindIfExists(Expression<Func<IVehicleMakeDTO, bool>> expression);
        Task<int[]> FindAllMakeIdsForRandom();
    }
}
