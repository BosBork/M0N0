﻿using Project.DAL.DataAccess;
using Project.DAL;
using Project.Common;
using Microsoft.EntityFrameworkCore;
using Project.Repository.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using System.Linq.Expressions;
using Project.Common.Enums;
using Project.Model.Common.Query.Make;
using Project.Model.DTOs.Common;

namespace Project.Repository.Repo
{
    public class MakeRepo : VehicleServiceRepoBase<VehicleMake>, IMakeRepo
    {
        private readonly IMapper _mapper;
        private readonly ISortHelper<VehicleMake> _sortHelper;
        public MakeRepo(ApplicationDbContext context, ISortHelper<VehicleMake> sortHelper, IMapper mapper) : base(context)
        {
            _mapper = mapper;
            _sortHelper = sortHelper;
        }

        public async Task<bool> FindIfExists(Expression<Func<IVehicleMakeDTO, bool>> selector)
        {
            var expression = _mapper.Map<Expression<Func<VehicleMake, bool>>>(selector);
            var result = FindIfExists(expression);
            return await result;
        }

        public async Task<int[]> FindAllMakeIdsForRandom()
        {
            var result = FindAll().Select(x=>x.VehicleMakeId).ToArrayAsync();
            return await result;
        }

        public async Task<int> CreateVehicleMake(IVehicleMakeCreateDTO vehicleMake)
        {
            VehicleMake mapped = _mapper.Map<VehicleMake>(vehicleMake);
            VehicleMake makeCreated = await Create(mapped);
            await SaveAsync();
            return makeCreated.VehicleMakeId;
        }

        public async Task UpdateVehicleMake(IVehicleMakeUpdateDTO vehicleMake)
        {
            var mapped = _mapper.Map<VehicleMake>(vehicleMake);
            await Update(mapped, mapped.VehicleMakeId);
            await SaveAsync();
        }

        public async Task DeleteVehicleMake(IVehicleMakeDTO vehicleMake)
        {
            await DeleteById(vehicleMake.VehicleMakeId);
            await SaveAsync();
        }

        public async Task<List<SelectListItem>> GetAllMakesForDPSelectListItem()
        {
            IQueryable<SelectListItem> makes = FindAll().Select(x => new SelectListItem { Text = x.Name, Value = x.VehicleMakeId.ToString() });
            return await makes.ToListAsync();
        }

        public async Task<PagedList<IVehicleMakeDTO>> GetAllVehicleMakesAsync(IMakeParams makeParams, Include include)
        {
            IQueryable<VehicleMake> makes = FindAll();

            #region Include
            if (include == Include.Yes)
            {
                makes = makes.Include(x => x.VehicleModels) as IQueryable<VehicleMake>;
            }
            #endregion

            //QueryHelper<VehicleMake>.FilterByFirstChar(ref makes, makeParams.First);

            QueryHelper<VehicleMake>.SearchByName(ref makes, makeParams.Name);

            IQueryable<VehicleMake> sortedMakes = _sortHelper.ApplySort(makes, makeParams.OrderBy);

            var mapped = await sortedMakes.ToMappedPagedListAsync<IVehicleMakeDTO>(makeParams.PageNumber, makeParams.PageSize, _mapper);
            return mapped;
        }

        public async Task<IVehicleMakeDTO> GetVehicleMakeByIdAsync(int vehicleMakeId)
        {
            var result = await FindByCondition(x => x.VehicleMakeId.Equals(vehicleMakeId)).FirstOrDefaultAsync();
            return _mapper.Map<IVehicleMakeDTO>(result);
        }

        public async Task<IVehicleMakeDTO> GetVehicleMakeByIdWithModelsAsync(int vehicleMakeId)
        {
            #region test
            //var result = FindByCondition(x => x.VehicleMakeId.Equals(vehicleMakeId));

            //if (true)
            //{
            //    result = result.Include(models => models.VehicleModels);
            //}

            //await result.FirstOrDefaultAsync(); 
            #endregion

            var result = await FindByCondition(x => x.VehicleMakeId.Equals(vehicleMakeId))
                .Include(models => models.VehicleModels)
                .FirstOrDefaultAsync();

            return _mapper.Map<IVehicleMakeDTO>(result);
        }

    }
}
