using AutoMapper;
using Project.DAL;
using Project.Common;
using Project.Model.OtherModels.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static Project.WebAPI.AutoMapperPagedListConverter;
using Project.Model.Common;
using System.Linq.Expressions;

namespace Project.WebAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            #region Make
            CreateMap<IVehicleMakeDTO, VehicleMake>().ReverseMap();
            CreateMap<IVehicleMakeUpdateDTO, VehicleMake>().ReverseMap();
            CreateMap<IVehicleMakeCreateDTO, VehicleMake>().ReverseMap();

            CreateMap<IVehicleMakeDTO, VehicleMakeDTO>();

            //--------

            CreateMap<VehicleMake, VehicleMakeDTO>();
            CreateMap<VehicleMakeCreateDTO, VehicleMake>();
            CreateMap<VehicleMakeUpdateDTO, VehicleMake>();
            #endregion

            #region Model
            CreateMap<IVehicleModelDTO, VehicleModel>().ReverseMap();
                                                CreateMap<IVehicleModelDTO, IVehicleModelUpdateDTO>().ReverseMap();
            CreateMap<IVehicleModelUpdateDTO, VehicleModel>().ReverseMap();
            CreateMap<IVehicleModelCreateDTO, VehicleModel>().ReverseMap();

            CreateMap<IVehicleModelDTO, VehicleModelDTO>();

            //--------

            CreateMap<VehicleModel, VehicleModelDTO>();
            CreateMap<VehicleModelCreateDTO, VehicleModel>();
            CreateMap<VehicleModelUpdateDTO, VehicleModel>();
            #endregion

            //unbound generic types
            CreateMap(typeof(PagedList<>), typeof(PagedList<>)).ConvertUsing(typeof(PagedListConverter<,>));
        }


    }
}
