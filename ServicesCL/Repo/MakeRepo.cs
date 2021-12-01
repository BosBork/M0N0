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
    public class MakeRepo : VehicleServiceRepoBase<VehicleMake>, IMakeRepo
    {
        public MakeRepo(ApplicationDbContext context) :base(context)
        {

        }

        #region old
        //public IEnumerable<VehicleMake> GetVehicleMakes(MakeParams makeParams)
        //{
        //    return FindAll()
        //        .OrderBy(on => on.Name)
        //        .Skip((makeParams.PageNumber - 1) * makeParams.PageSize)
        //        .Take(makeParams.PageSize)
        //        .ToList();
        //} 
        #endregion

        public void UpdateVehicleMake(VehicleMake vehicleMake)
        {
            Update(vehicleMake);
        }

        public void CreateVehicleMake(VehicleMake vehicleMake)
        {
            Create(vehicleMake);
        }

        public void DeleteVehicleMake(VehicleMake vehicleMake)
        {
            Delete(vehicleMake);
        }


        public async Task<PagedList<VehicleMake>> GetAllVehicleMakes(MakeParams makeParams)
        {
            #region Test
            //var max = ((int)Math.Ceiling(FindAll().Count() / (double)makeParams.PageSize));
            //if (makeParams.PageNumber > max)
            //{
            //    makeParams.PageNumber = 1;
            //} 
            #endregion

            #region Test
            //if (makeParams.First == "All" || string.IsNullOrEmpty(makeParams.First))
            //{
            //    return await PagedList<VehicleMake>.ToPagedListAsync(FindAll().OrderBy(x => x.Name), makeParams.PageNumber, makeParams.PageSize);
            //} 
            //string First = makeParams.First.Substring(0, 1);
            //var makes = FindByCondition(x => x.Name.StartsWith(First)).OrderBy(x => x.Name);
            #endregion

            var makes = (makeParams.First == "All" ? FindAll() : FindByCondition(x => x.Name.StartsWith(makeParams.First.Substring(0, 1)))).OrderBy(x=>x.Name);

            return await PagedList<VehicleMake>.ToPagedListAsync(makes, makeParams.PageNumber, makeParams.PageSize);
        }


        public VehicleMake GetVehicleMakeById(int vehicleMakeId)
        {
            return FindByCondition(x => x.VehicleMakeId.Equals(vehicleMakeId))
                .FirstOrDefault();
        }

        public VehicleMake GetModelsOfVehicleById(int vehicleMakeId)
        {
            return FindByCondition(x => x.VehicleMakeId.Equals(vehicleMakeId))
                .Include(models => models.VehicleModels).FirstOrDefault();
        }

    }
}
