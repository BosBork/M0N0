using Project.DAL.DataAccess;
using Project.DAL;
using Project.Common;
using Project.Model.OtherModels.Query;
using Microsoft.EntityFrameworkCore;
using Project.Repository.Common.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using Project.Model.Common;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Project.Model.OtherModels.DTOs;

namespace Project.Repository.Repo
{
    public class ModelRepo : VehicleServiceRepoBase<VehicleModel>, IModelRepo
    {
        private readonly IMapper _mapper;
        private readonly ISortHelper<VehicleModel> _sortHelper;
        public ModelRepo(ApplicationDbContext context, ISortHelper<VehicleModel> sortHelper, IMapper mapper) : base(context)
        {
            _mapper = mapper;
            _sortHelper = sortHelper;
        }

        public async Task<IVehicleModelUpdateDTO> UpdateVehicleModel(IVehicleModelUpdateDTO vehicleModel)
        {
            VehicleModel mapped = _mapper.Map<VehicleModel>(vehicleModel);
            VehicleModel modelCreated = await Update(mapped, mapped.VehicleModelId);
            await SaveAsync();
            IVehicleModelUpdateDTO modelBackTo = _mapper.Map<IVehicleModelUpdateDTO>(modelCreated);
            return modelBackTo;
        }

        public async Task<IVehicleModelCreateDTO> CreateVehicleModel(IVehicleModelCreateDTO vehicleModel)
        {
            VehicleModel mapped = _mapper.Map<VehicleModel>(vehicleModel);
            VehicleModel modelCreated = await Create(mapped);
            await SaveAsync();
            IVehicleModelCreateDTO modelBackTo = _mapper.Map<IVehicleModelCreateDTO>(modelCreated);
            return modelBackTo;
        }

        public async Task DeleteVehicleModel(IVehicleModelDTO VehicleModel)
        {
            await DeleteById(VehicleModel.VehicleModelId);
            await SaveAsync();
        }

        public async Task<PagedList<IVehicleModelDTO>> GetAllVehicleModelsAsync(ModelParams modelParams)
        {
            IQueryable<VehicleModel> models = FindAll();

            #region Include
            //.Include(x => x.VehicleMake) as IQueryable<VehicleModel>;
            #endregion

            Other<VehicleModel>.FilterByFirstChar(ref models, modelParams.First);

            Other<VehicleModel>.SearchByName(ref models, modelParams.Name);

            Other<VehicleModel>.FilterByMatchingIds(ref models, modelParams.MakeIdFilterSelected); //for dropdown filter

            IQueryable<VehicleModel> sortedModels = _sortHelper.ApplySort(models, modelParams.OrderBy);

            var mapped = await sortedModels.ToMappedPagedListAsync<IVehicleModelDTO>(modelParams.PageNumber, modelParams.PageSize, _mapper);

            return mapped;
        }

        public async Task<IVehicleModelDTO> GetVehicleModelByIdAsync(int vehicleModelId)
        {
            var result = await FindByCondition(x => x.VehicleModelId.Equals(vehicleModelId)).Include(x => x.VehicleMake)
                .FirstOrDefaultAsync();
            return _mapper.Map<IVehicleModelDTO>(result);
        }

    }
}
