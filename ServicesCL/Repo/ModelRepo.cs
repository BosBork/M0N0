using EntitiesCL.DataAccess;
using EntitiesCL.EFModels;
using EntitiesCL.Helpers;
using ServicesCL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesCL.Repo
{
    public class ModelRepo : VehicleServiceRepoBase<VehicleModel>, IModelRepo
    {
        private readonly ISortHelper<VehicleModel> _sortHelper;
        public ModelRepo(ApplicationDbContext context, ISortHelper<VehicleModel> sortHelper) : base(context)
        {
            _sortHelper = sortHelper;
        }
    }
}
