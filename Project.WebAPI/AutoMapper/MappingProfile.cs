using AutoMapper;
using Project.DAL;
using Project.Common;
using Project.WebAPI.ReadModels;
using Project.Model.DTOs;
using Project.Model.DTOs.Common;
using static Project.AutoMapper.WebAPI.AutoMapperPagedListConverter;

namespace Project.AutoMapper.WebAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Make
            CreateMap<IVehicleMakeDTO, VehicleMake>().ReverseMap();
            CreateMap<IVehicleMakeUpdateDTO, VehicleMake>().ReverseMap();
            CreateMap<IVehicleMakeCreateDTO, VehicleMake>().ReverseMap();

            CreateMap<IVehicleMakeDTO, VehicleMake_Read>();
            CreateMap<IVehicleMakeUpdateDTO, IVehicleMakeDTO>()
                .ForMember(x => x.VehicleMakeId, y => y.Ignore());

            //--------

            CreateMap<VehicleMake, VehicleMakeDTO>();
            CreateMap<VehicleMakeCreateDTO, VehicleMake>();
            CreateMap<VehicleMakeUpdateDTO, VehicleMake>();
            #endregion

            #region Model
            CreateMap<IVehicleModelDTO, VehicleModel>().ReverseMap();
            CreateMap<IVehicleModelUpdateDTO, VehicleModel>().ReverseMap();
            CreateMap<IVehicleModelCreateDTO, VehicleModel>().ReverseMap();

            CreateMap<IVehicleModelDTO, VehicleModel_Read>();
            CreateMap<IVehicleModelUpdateDTO, IVehicleModelDTO>()
                .ForMember(x => x.VehicleModelId, y => y.Ignore());

            //CreateMap<x, x>()
            //    .ForSourceMember(x => x.something, y => y.DoNotValidate());

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
