using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Project.Common
{
    public abstract class QueryHelper<T> where T : class, IQueryHelper
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

        #region test_later
        //public static class ExtensionHelpers
        //{
        //    public static IQueryable<T> FilterByFirstChar<T>(this IQueryable<T> vehicle, string first) where T : class, IOther
        //    {
        //        if (!vehicle.Any() || string.IsNullOrWhiteSpace(first))
        //            return vehicle;

        //        return vehicle.Where(x => x.Name.StartsWith(first.Trim().Substring(0, 1)));
        //    }
        //} 
        #endregion


    }
}
