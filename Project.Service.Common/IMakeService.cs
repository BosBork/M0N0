using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Common;
using Project.Common.Enums;
using Project.DAL;
using Project.Model.Common;
using Project.Model.OtherModels.Query;
using Project.Repository.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Project.Service.Common
{
    public interface IMakeService/* : IVehicleServiceRepoBase<VehicleMake>*/
    {
        Task<List<SelectListItem>> GetAllMakesForDPSelectListItem();
        Task<PagedList<IVehicleMakeDTO>> GetAllVehicleMakesAsync(MakeParams makeParams, Include include = Include.No);
        Task<IVehicleMakeDTO> GetVehicleMakeByIdAsync(int vehicleMakeId);
        Task<IVehicleMakeDTO> GetVehicleMakeByIdWithModelsAsync(int vehicleMakeId);
        //Task<IVehicleMakeDTO> GetVehicleMakeByIdWithModelsCountAsync(int vehicleMakeId);

        Task<int> CreateVehicleMake(IVehicleMakeCreateDTO vehicleMake);
        Task/*<int> */UpdateVehicleMake(IVehicleMakeUpdateDTO vehicleMake);

        Task DeleteVehicleMake(IVehicleMakeDTO vehicleMake);

        Task<bool> FindIfExists(Expression<Func<IVehicleMakeDTO, bool>> expression);
        Task<int[]> FindAllMakeIdsForRandom();
    }
}
