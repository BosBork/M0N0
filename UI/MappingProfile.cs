using AutoMapper;
using EntitiesCL.EFModels;
using EntitiesCL.OtherModels.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.ViewModels;

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

            CreateMap<VehicleMake, VehicleMakeVM>();
            CreateMap<VehicleMakeDTO, VehicleMakeVM>();

        }
    }
}
