using EntitiesCL.DataAccess;
using EntitiesCL.EFModels;
using EntitiesCL.Helpers;
using EntitiesCL.OtherModels.Query;
using Microsoft.EntityFrameworkCore;
using ServicesCL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesCL.Repo
{
    public class ModelRepo : VehicleServiceRepoBase<VehicleModel>, IModelRepo
    {
        private readonly ISortHelper<VehicleModel> _sortHelper;
        public ModelRepo(ApplicationDbContext context, ISortHelper<VehicleModel> sortHelper) : base(context)
        {
            _sortHelper = sortHelper;
        }

        public void UpdateVehicleModel(VehicleModel VehicleModel)
        {
            Update(VehicleModel);
        }

        public void CreateVehicleModel(VehicleModel vehicleModel)
        {
            Create(vehicleModel);
        }

        public void DeleteVehicleModel(VehicleModel VehicleModel)
        {
            Delete(VehicleModel);
        }

        public async Task<PagedList<VehicleModel>> GetAllVehicleModelsAsync(ModelParams modelParams)
        {
            var models = FindAll();

            #region Test_1
            //.Include(x => x.VehicleMake) as IQueryable<VehicleModel>; 
            #endregion

            FilterByFirstChar(ref models, modelParams.First);
            SearchByName(ref models, modelParams.Name);

            var sortedModels = _sortHelper.ApplySort(models, modelParams.OrderBy);

            return await PagedList<VehicleModel>.ToPagedListAsync(sortedModels, modelParams.PageNumber, modelParams.PageSize);
        }

        public async Task<VehicleModel> GetVehicleModelByIdAsync(int vehicleModelId)
        {
            return await FindByCondition(x => x.VehicleModelId.Equals(vehicleModelId))
                .FirstOrDefaultAsync();
        }

        #region Methods
        private void FilterByFirstChar(ref IQueryable<VehicleModel> vehicleModels, string first)
        {
            if (!vehicleModels.Any() || string.IsNullOrWhiteSpace(first))
                return;

            vehicleModels = vehicleModels.Where(x => x.Name.StartsWith(first.Trim().Substring(0, 1)));
        }

        private void SearchByName(ref IQueryable<VehicleModel> vehicleModels, string vehicleModelName)
        {
            if (!vehicleModels.Any() || string.IsNullOrWhiteSpace(vehicleModelName))
                return;

            vehicleModels = vehicleModels.Where(o => o.Name.ToLower().Contains(vehicleModelName.Trim().ToLower()));
        }
        #endregion

    }
}
