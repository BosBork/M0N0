using EntitiesCL.DataAccess;
using EntitiesCL.EFModels;
using ServicesCL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesCL.Repo
{
    public class MakeRepo : VehicleServiceRepoBase<VehicleMake>, IMakeRepo
    {
        public MakeRepo(ApplicationDbContext context) :base(context)
        {

        }
    }
}
