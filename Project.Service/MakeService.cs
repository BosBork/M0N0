using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Common;
using Project.Common.Enums;
using Project.Model.Common;
using Project.Model.Common.Query.Make;
using Project.Model.DTOs.Common;
using Project.Repository.Common.Interfaces;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Project.Service
{
    public class MakeService : /*VehicleServiceRepoBase<VehicleMake>, */IMakeService
    {
        private readonly IMakeRepo _repo;
        public MakeService(/*ApplicationDbContext context, */IMakeRepo repo) /*: base(context)*/
        {
            _repo = repo;
        }

        public async Task<bool> FindIfExists(Expression<Func<IVehicleMakeDTO, bool>> expression)
        {
            return await _repo.FindIfExists(expression);
        }

        public async Task<int[]> FindAllMakeIdsForRandom()
        {
            return await _repo.FindAllMakeIdsForRandom();
        }

        public async Task UpdateVehicleMake(IVehicleMakeUpdateDTO vehicleMake)
        {
            await _repo.UpdateVehicleMake(vehicleMake);
        }

        public async Task<int> CreateVehicleMake(IVehicleMakeCreateDTO vehicleMake)
        {
            return await _repo.CreateVehicleMake(vehicleMake);
        }

        public async Task DeleteVehicleMake(IVehicleMakeDTO vehicleMake)
        {
            await _repo.DeleteVehicleMake(vehicleMake);
        }

        public async Task<PagedList<IVehicleMakeDTO>> GetAllVehicleMakesAsync(
            IMakeFilter makeFilter, IMakeSort makeSort, IPagingParamsBase paging, Include include)
        {
            return await _repo.GetAllVehicleMakesAsync(makeFilter, makeSort, paging, include);
        }

        public async Task<IVehicleMakeDTO> GetVehicleMakeByIdAsync(int vehicleMakeId, Include include)
        {
            return await _repo.GetVehicleMakeByIdAsync(vehicleMakeId, include);
        }
    }
}
