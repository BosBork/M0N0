using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Common;
using Project.Common.Enums;
using Project.DAL;
using Project.DAL.DataAccess;
using Project.Model.Common;
using Project.Model.OtherModels.Query;
using Project.Repository.Common.Interfaces;
using Project.Repository.Repo;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
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

        public async Task/*<int> */UpdateVehicleMake(IVehicleMakeUpdateDTO vehicleMake)
        {
            /*return */await _repo.UpdateVehicleMake(vehicleMake);
        }

        #region Kanta
        //public async Task<IVehicleMakeCreateDTO> CreateVehicleMake(IVehicleMakeCreateDTO vehicleMake)
        //{
        //    return await _repo.CreateVehicleMake(vehicleMake);
        //} 
        #endregion

        public async Task<int> CreateVehicleMake(IVehicleMakeCreateDTO vehicleMake)
        {
            return await _repo.CreateVehicleMake(vehicleMake);
        }

        public async Task DeleteVehicleMake(IVehicleMakeDTO vehicleMake)
        {
            await _repo.DeleteVehicleMake(vehicleMake);
        }

        public async Task<List<SelectListItem>> GetAllMakesForDPSelectListItem()
        {
            return await _repo.GetAllMakesForDPSelectListItem();
        }

        public async Task<PagedList<IVehicleMakeDTO>> GetAllVehicleMakesAsync(MakeParams makeParams, Include include)
        {
            return await _repo.GetAllVehicleMakesAsync(makeParams, include);
        }

        public async Task<IVehicleMakeDTO> GetVehicleMakeByIdAsync(int vehicleMakeId)
        {
            return await _repo.GetVehicleMakeByIdAsync(vehicleMakeId);
        }

        public async Task<IVehicleMakeDTO> GetVehicleMakeByIdWithModelsAsync(int vehicleMakeId)
        {
            return await _repo.GetVehicleMakeByIdWithModelsAsync(vehicleMakeId);
        }

        //public async Task<IVehicleMakeDTO> GetVehicleMakeByIdWithModelsCountAsync(int vehicleMakeId)
        //{
        //    return await _repo.GetVehicleMakeByIdWithModelsCountAsync(vehicleMakeId);
        //}

        #region worky
        //public async Task UpdateVehicleMake(IVehicleMakeUpdateDTO vehicleMake)
        //{
        //    await _repo.UpdateVehicleMake(vehicleMake);
        //} 
        #endregion

    }
}
