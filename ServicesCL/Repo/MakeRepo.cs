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

            var makes = FindAll();

            FilterByFirstChar(ref makes, makeParams.First);
            SearchByName(ref makes, makeParams.Name);

            var sortedMakes = _sortHelper.ApplySort(makes, makeParams.OrderBy);

            return await PagedList<VehicleMake>.ToPagedListAsync(/*makes.OrderBy(x=>x.Name)*/sortedMakes, makeParams.PageNumber, makeParams.PageSize);
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


        #region Methods
        private void FilterByFirstChar(ref IQueryable<VehicleMake> vehicleMakes, string first)
        {
            if (!vehicleMakes.Any() || string.IsNullOrWhiteSpace(first))
                return;

            vehicleMakes = vehicleMakes.Where(x => x.Name.StartsWith(first.Trim().Substring(0, 1)));
        }

        private void SearchByName(ref IQueryable<VehicleMake> vehicleMakes, string vehicleMakeName)
        {
            if (!vehicleMakes.Any() || string.IsNullOrWhiteSpace(vehicleMakeName))
                return;

            vehicleMakes = vehicleMakes.Where(o => o.Name.ToLower().Contains(vehicleMakeName.Trim().ToLower()));
        }
        #endregion

    }
}
