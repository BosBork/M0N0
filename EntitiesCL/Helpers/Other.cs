using EntitiesCL.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EntitiesCL.Helpers
{
    public abstract class Other<T> where T : class, IOther
    {
        public static void FilterByFirstChar(ref IQueryable<T> vehicle, string first)
        {
            if (!vehicle.Any() || string.IsNullOrWhiteSpace(first))
                return;

            vehicle = vehicle.Where(x => x.Name.StartsWith(first.Trim().Substring(0, 1)));
        }

        public static void SearchByName(ref IQueryable<T> vehicle, string name)
        {
            if (!vehicle.Any() || string.IsNullOrWhiteSpace(name))
                return;

            vehicle = vehicle.Where(o => o.Name.ToLower().Contains(name.Trim().ToLower()));
        }
        #region trash
        //public static object FilterByFirstChar_(ref IQueryable<T> vehicle, string first)
        //{
        //    Type t = typeof(T);
        //    PropertyInfo prop = t.GetProperty("Name");
        //    return vehicle.OfType<T>().Where(x => (x.GetType().GetProperty("Name").GetValue(x, null)).ToString().StartsWith(first.Trim().Substring(0, 1)));
        //}


        //var legallyAlices = vehicle.OfType<VehicleModel>()
        //                .Where(x => x.Name.StartsWith(first.Trim().Substring(0, 1)));

        //var query = from c in vehicle.OfType<T>() select c;

        //query = query.Where(x => x.GetType().GetProperty("VehicleModelId").Equals(1));

        //var result = vehicle.Where(i => i.GetType().GetProperty("VehicleModelId").GetValue(i, null).Equals(1));

        //Parallel.ForEach(vehicle, (currentItem) =>
        //{
        //    var value = typeof(T).GetProperty("Name").GetValue(currentItem);
        //});

        //vehicle = vehicle.Where(x => x.GetType().GetProperty("Name").GetValue(x).ToString().StartsWith(first.Trim().Substring(0, 1))); 
        #endregion
    }
}
