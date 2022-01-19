using Project.DAL.DataAccess;
using Project.DAL;
using Project.Common;
using Project.Model.OtherModels.Query;
using Microsoft.EntityFrameworkCore;
using Project.Repository.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Model.Common;
using AutoMapper;
using System.Linq.Expressions;

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

        #region Kanta
        //public async Task<IVehicleMakeCreateDTO> CreateVehicleMake(IVehicleMakeCreateDTO vehicleMake)
        //{
        //    VehicleMake mapped = _mapper.Map<VehicleMake>(vehicleMake);
        //    VehicleMake makeCreated = await Create(mapped);
        //    await SaveAsync();
        //    IVehicleMakeCreateDTO makeBackTo = _mapper.Map<IVehicleMakeCreateDTO>(makeCreated);
        //    return makeBackTo;
        //} 
        #endregion

        public async Task<int> CreateVehicleMake(IVehicleMakeCreateDTO vehicleMake)
        {
            VehicleMake mapped = _mapper.Map<VehicleMake>(vehicleMake);
            VehicleMake makeCreated = await Create(mapped);
            await SaveAsync();
            //IVehicleMakeCreateDTO makeBackTo = _mapper.Map<IVehicleMakeCreateDTO>(makeCreated);
            return makeCreated.VehicleMakeId;
        }

        public async Task<int> UpdateVehicleMake(IVehicleMakeUpdateDTO vehicleMake)
        {
            VehicleMake mapped = _mapper.Map<VehicleMake>(vehicleMake);
            VehicleMake makeCreated = await Update(mapped, mapped.VehicleMakeId);
            await SaveAsync();
            //IVehicleMakeUpdateDTO makeBackTo = _mapper.Map<IVehicleMakeUpdateDTO>(makeCreated);
            return makeCreated.VehicleMakeId;
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

        public async Task<PagedList<IVehicleMakeDTO>> GetAllVehicleMakesAsync(MakeParams makeParams)
        {
            IQueryable<VehicleMake> makes = FindAll();

            QueryHelper<VehicleMake>.FilterByFirstChar(ref makes, makeParams.First);

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

        public async Task<IVehicleMakeDTO> GetModelsOfVehicleByIdAsync(int vehicleMakeId)
        {
            var result = await FindByCondition(x => x.VehicleMakeId.Equals(vehicleMakeId))
                .Include(models => models.VehicleModels).FirstOrDefaultAsync();
            return _mapper.Map<IVehicleMakeDTO>(result);
        }

    }
}
