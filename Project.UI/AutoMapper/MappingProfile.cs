using AutoMapper;
using Project.DAL;
using Project.Common;
using Project.Model.OtherModels.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Project.UI.ViewModels;
using Project.UI.ViewModels.Make;
using static Project.UI.AutoMapperPagedListConverter;
using Project.Model.Common;
using System.Linq.Expressions;

namespace Project.UI
{
    #region Test
    //public class ExpressionMappingProfile : MappingProfile
    //{
    //    public ExpressionMappingProfile()
    //    {
    //        CreateMap<IVehicleMakeDTO, VehicleMake>()
    //        .ForMember(ol => ol.VehicleMakeId, conf => conf.MapFrom(dto => dto.VehicleMakeId));
    //        CreateMap<IVehicleMakeDTO, VehicleMake>()
    //        .ForMember(dto => dto.Name, conf => conf.MapFrom(ol => ol.Name));
    //        CreateMap<IVehicleMakeDTO, VehicleMake>()
    //        .ForMember(dto => dto.Abrv, conf => conf.MapFrom(ol => ol.Abrv));
    //        CreateMap<IVehicleMakeDTO, VehicleMake>()
    //        .ForMember(dto => dto.VehicleModels, conf => conf.MapFrom(ol => ol.VehicleModels));
    //    }
    //} 
    #endregion

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            #region Make
            CreateMap<IVehicleMakeDTO, VehicleMake>().ReverseMap();
            CreateMap<IVehicleMakeUpdateDTO, VehicleMake>().ReverseMap();
            CreateMap<IVehicleMakeCreateDTO, VehicleMake>().ReverseMap();

            CreateMap<IVehicleMakeUpdateDTO, VehicleMakeUpdateVM>().ReverseMap(); 
            CreateMap<IVehicleMakeDTO, VehicleMakeUpdateVM>();

            CreateMap<IVehicleMakeCreateDTO, VehicleMakeCreateVM>().ReverseMap();
            CreateMap<IVehicleMakeDTO, VehicleMakeVM>().ReverseMap();

            //--------

            CreateMap<VehicleMake, VehicleMakeDTO>();
            CreateMap<VehicleMakeCreateDTO, VehicleMake>();
            CreateMap<VehicleMakeUpdateDTO, VehicleMake>();

            CreateMap<VehicleMake, VehicleMakeVM>();
            CreateMap<VehicleMakeDTO, VehicleMakeVM>();
            CreateMap<VehicleMakeCreateVM, VehicleMakeCreateDTO>();
            CreateMap<VehicleMakeUpdateVM, VehicleMakeUpdateDTO>();

            CreateMap<VehicleMake, VehicleMakeCreateVM>();
            CreateMap<VehicleMake, VehicleMakeUpdateVM>();
            #endregion

            #region Model
            CreateMap<IVehicleModelDTO, VehicleModel>().ReverseMap();
            CreateMap<IVehicleModelUpdateDTO, VehicleModel>().ReverseMap();
            CreateMap<IVehicleModelCreateDTO, VehicleModel>().ReverseMap();

            CreateMap<IVehicleModelUpdateDTO, VehicleModelUpdateVM>().ReverseMap();
            CreateMap<IVehicleModelDTO, VehicleModelUpdateVM>();

            CreateMap<IVehicleModelCreateDTO, VehicleModelCreateVM>().ReverseMap();
            CreateMap<IVehicleModelDTO, VehicleModelVM>().ReverseMap();

            //--------

            CreateMap<VehicleModel, VehicleModelDTO>();
            CreateMap<VehicleModelCreateDTO, VehicleModel>();
            CreateMap<VehicleModelUpdateDTO, VehicleModel>();

            CreateMap<VehicleModel, VehicleModelVM>();
            CreateMap<VehicleModelDTO, VehicleModelVM>();
            CreateMap<VehicleModelCreateVM, VehicleModelCreateDTO>();
            CreateMap<VehicleModelUpdateVM, VehicleModelUpdateDTO>();

            CreateMap<VehicleModel, VehicleModelCreateVM>();
            CreateMap<VehicleModel, VehicleModelUpdateVM>();
            #endregion

            //CreateMap<VehicleModelUpdateVM, VehicleModel>();

            //unbound generic types

            CreateMap(typeof(PagedList<>), typeof(PagedList<>)).ConvertUsing(typeof(PagedListConverter<,>));

            #region NonGenericTest
            //CreateMap<PagedList<VehicleMake>, PagedList<IVehicleMakeDTO>>().ConvertUsing<NonGeneric>();
            #endregion

        }

        #region NonGenericTest
        //public class NonGeneric : ITypeConverter<PagedList<VehicleMake>, PagedList<IVehicleMakeDTO>>
        //{
        //    public PagedList<IVehicleMakeDTO> Convert(PagedList<VehicleMake> source, PagedList<IVehicleMakeDTO> destination, ResolutionContext context)
        //    {
        //        var collection = source.Select(m => context.Mapper.Map<VehicleMake, IVehicleMakeDTO>(m)).ToList();

        //        var collection_v2 = context.Mapper.Map<IEnumerable<VehicleMake>, IEnumerable<IVehicleMakeDTO>>(source).ToList();

        //        return new PagedList<IVehicleMakeDTO>(collection, source.TotalCount, source.CurrentPage, source.PageSize);
        //    }
        //}
        #endregion

    }
}
