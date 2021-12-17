using AutoMapper;
using EntitiesCL.EFModels;
using EntitiesCL.Helpers;
using EntitiesCL.OtherModels.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UI.ViewModels;
using static UI.AutoMapperPagedListConverter;

namespace UI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<VehicleMake, VehicleMakeDTO>();
            CreateMap<VehicleMakeCreateDTO, VehicleMake>();
            CreateMap<VehicleMakeUpdateDTO, VehicleMake>();

            
            CreateMap<VehicleModel, VehicleModelDTO>();
            CreateMap<VehicleModelCreateDTO, VehicleModel>();
            CreateMap<VehicleModelUpdateDTO, VehicleModel>();

            CreateMap<VehicleModel, VehicleModelVM>();
            CreateMap<VehicleModelDTO, VehicleModelVM>();
            CreateMap<VehicleModelCreateVM, VehicleModelCreateDTO>();
            CreateMap<VehicleModelUpdateVM, VehicleModelUpdateDTO>();

            CreateMap<VehicleModel, VehicleModelCreateVM>();
            CreateMap<VehicleModel, VehicleModelUpdateVM>();
            //CreateMap<VehicleModelUpdateVM, VehicleModel>();

            CreateMap<VehicleMake, VehicleMakeVM>();
            CreateMap<VehicleMakeDTO, VehicleMakeVM>();

            //unbound generic types
            CreateMap(typeof(PagedList<>), typeof(PagedList<>)).ConvertUsing(typeof(PagedListConverter<,>));

            #region NonGenericTest
            //CreateMap<PagedList<VehicleModel>, PagedList<VehicleModelDTO>>().ConvertUsing<NonGeneric>(); 
            #endregion
        }

        #region NonGenericTest
        //public class NonGeneric : ITypeConverter<PagedList<VehicleModel>, PagedList<VehicleModelDTO>>
        //{
        //    public PagedList<VehicleModelDTO> Convert(PagedList<VehicleModel> source, PagedList<VehicleModelDTO> destination, ResolutionContext context)
        //    {
        //        var collection = source.Select(m => context.Mapper.Map<VehicleModel, VehicleModelDTO>(m)).ToList();

        //        var collection_v2 = context.Mapper.Map<IEnumerable<VehicleModel>, IEnumerable<VehicleModelDTO>>(source).ToList();

        //        return new PagedList<VehicleModelDTO>(collection, source.TotalCount, source.CurrentPage, source.PageSize);
        //    }
        //} 
        #endregion

    }
}
