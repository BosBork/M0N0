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
using System.Reflection;

namespace ServicesCL.Repo
{
    public class MakeRepo : VehicleServiceRepoBase<VehicleMake>, IMakeRepo
    {
        private readonly ISortHelper<VehicleMake> _sortHelper;
        public MakeRepo(ApplicationDbContext context, ISortHelper<VehicleMake> sortHelper) :base(context)
        {
            _sortHelper = sortHelper;
        }

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


        public async Task<PagedList<VehicleMake>> GetAllVehicleMakesAsync(MakeParams makeParams)
        {
            #region SimpleSort
            //var makesx = FindAll();
            //switch (makeParams.Sort.ToLower())
            //{
            //    case "name desc":
            //        makesx = makesx.OrderByDescending(x => x.Name);
            //        break;
            //    default:
            //        makesx = makesx.OrderBy(x => x.Name);
            //        break;
            //} 
            #endregion

            var makes = FindAll();

            Other<VehicleMake>.FilterByFirstChar(ref makes, makeParams.First);

            Other<VehicleMake>.SearchByName(ref makes, makeParams.Name);

            var sortedMakes = _sortHelper.ApplySort(makes, makeParams.OrderBy);

            return await PagedList<VehicleMake>.ToPagedListAsync(/*makes.OrderBy(x=>x.Name)*/sortedMakes, makeParams.PageNumber, makeParams.PageSize);
        }


        public async Task<VehicleMake> GetVehicleMakeByIdAsync(int vehicleMakeId)
        {
            return await FindByCondition(x => x.VehicleMakeId.Equals(vehicleMakeId))
                .FirstOrDefaultAsync();
        }

        public async Task<VehicleMake> GetModelsOfVehicleByIdAsync(int vehicleMakeId)
        {
            return await FindByCondition(x => x.VehicleMakeId.Equals(vehicleMakeId))
                .Include(models => models.VehicleModels).FirstOrDefaultAsync();
        }

    }
}
