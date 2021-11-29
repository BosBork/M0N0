using EntitiesCL.DataAccess;
using EntitiesCL.EFModels;
using ServicesCL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesCL.Repo
{
    public class ModelRepo : VehicleServiceRepoBase<VehicleModel>, IModelRepo
    {
        public ModelRepo(ApplicationDbContext context): base(context)
        {

        }
    }
}
