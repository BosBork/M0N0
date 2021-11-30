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

namespace ServicesCL.Repo
{
    public class MakeRepo : VehicleServiceRepoBase<VehicleMake>, IMakeRepo
    {
        public MakeRepo(ApplicationDbContext context) :base(context)
        {

        }

        //public IEnumerable<VehicleMake> GetVehicleMakes(MakeParams makeParams)
        //{
        //    return FindAll()
        //        .OrderBy(on => on.Name)
        //        .Skip((makeParams.PageNumber - 1) * makeParams.PageSize)
        //        .Take(makeParams.PageSize)
        //        .ToList();
        //}

        public PagedList<VehicleMake> GetAllVehicleMakes(MakeParams makeParams)
        {
            #region Test
            //var max = ((int)Math.Ceiling(FindAll().Count() / (double)makeParams.PageSize));
            //if (makeParams.PageNumber > max)
            //{
            //    makeParams.PageNumber = 1;
            //} 
            #endregion
            return PagedList<VehicleMake>.ToPagedList(FindAll()
                .OrderBy(x => x.Name), makeParams.PageNumber, makeParams.PageSize);
        }

        public VehicleMake GetVehicleMakeById(int vehicleMakeId)
        {
            return FindByCondition(x => x.VehicleMakeId.Equals(vehicleMakeId))
                .FirstOrDefault();
        }

        public VehicleMake GetVehicleMakesModelsById(int vehicleMakeId)
        {
            return FindByCondition(x => x.VehicleMakeId.Equals(vehicleMakeId))
                .Include(models => models.VehicleModels).FirstOrDefault();
        }
    }
}
