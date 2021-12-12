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

        public static void FilterByMatchingIds(ref IQueryable<T> vehicle, int? idSelected)
        {
            if (!vehicle.Any() || !idSelected.HasValue)
                return;

            vehicle = vehicle.Where(x => x.VehicleMakeId == idSelected);
        }

    }
}
