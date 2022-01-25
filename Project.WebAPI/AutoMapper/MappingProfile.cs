using AutoMapper;
using Project.DAL;
using Project.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static Project.WebAPI.AutoMapperPagedListConverter;
using Project.Model.Common;
using System.Linq.Expressions;
using Project.WebAPI.ReadModels;
using Project.Model.DTOs;

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

            //CreateMap<IVehicleMakeDTO, VehicleMakeDTO>();
            CreateMap<IVehicleMakeDTO, VehicleMake_Read>();

            //--------

            CreateMap<VehicleMake, VehicleMakeDTO>();
            CreateMap<VehicleMakeCreateDTO, VehicleMake>();
            CreateMap<VehicleMakeUpdateDTO, VehicleMake>();
            #endregion

            #region Model
            CreateMap<IVehicleModelDTO, VehicleModel>().ReverseMap();
            CreateMap<IVehicleModelUpdateDTO, VehicleModel>().ReverseMap();
            CreateMap<IVehicleModelCreateDTO, VehicleModel>().ReverseMap();

            //CreateMap<IVehicleModelDTO, VehicleModelDTO>();
            CreateMap<IVehicleModelDTO, VehicleModel_Read>();

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
