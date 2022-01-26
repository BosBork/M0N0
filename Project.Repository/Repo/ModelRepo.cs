using Project.DAL.DataAccess;
using Project.DAL;
using Project.Common;
using Microsoft.EntityFrameworkCore;
using Project.Repository.Common.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using Project.Common.Enums;
using Project.Model.Common.Query.Model;
using Project.Model.DTOs.Common;

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

        public async Task UpdateVehicleModel(IVehicleModelUpdateDTO vehicleModelUpdate)
        {
            var mapped = _mapper.Map<VehicleModel>(vehicleModelUpdate);
            await Update(mapped, mapped.VehicleModelId);
            await SaveAsync();
        }

        public async Task<int> CreateVehicleModel(IVehicleModelCreateDTO vehicleModel)
        {
            VehicleModel mapped = _mapper.Map<VehicleModel>(vehicleModel);
            VehicleModel modelCreated = await Create(mapped);
            await SaveAsync();
            return modelCreated.VehicleModelId;
        }

        public async Task DeleteVehicleModel(IVehicleModelDTO VehicleModel)
        {
            await DeleteById(VehicleModel.VehicleModelId);
            await SaveAsync();
        }

        public async Task<PagedList<IVehicleModelDTO>> GetAllVehicleModelsAsync(IModelParams modelParams, Include include)
        {
            IQueryable<VehicleModel> models = FindAll();

            #region Include
            if (include == Include.Yes)
            {
                models = models.Include(x => x.VehicleMake) as IQueryable<VehicleModel>;
            }
            #endregion

            //QueryHelper<VehicleModel>.FilterByFirstChar(ref models, modelParams.First);

            QueryHelper<VehicleModel>.SearchByName(ref models, modelParams.Name);

            QueryHelper<VehicleModel>.FilterByMatchingIds(ref models, modelParams.MakeIdFilterSelected); //for dropdown filter

            IQueryable<VehicleModel> sortedModels = _sortHelper.ApplySort(models, modelParams.OrderBy);

            var mapped = await sortedModels.ToMappedPagedListAsync<IVehicleModelDTO>(modelParams.PageNumber, modelParams.PageSize, _mapper);

            return mapped;
        }

        public async Task<IVehicleModelDTO> GetVehicleModelByIdAsync(int vehicleModelId)
        {
            var result = await FindByCondition(x => x.VehicleModelId.Equals(vehicleModelId))
                .Include(x => x.VehicleMake)
                .FirstOrDefaultAsync();
            return _mapper.Map<IVehicleModelDTO>(result);
        }

    }
}
