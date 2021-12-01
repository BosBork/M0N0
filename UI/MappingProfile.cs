using AutoMapper;
using EntitiesCL.EFModels;
using EntitiesCL.OtherModels.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<VehicleMake, VehicleMakeDTO>();
            CreateMap<VehicleMakeCreateDTO, VehicleMake>();

            
            CreateMap<VehicleModel, VehicleModelDTO>();
            CreateMap<VehicleMakeUpdateDTO, VehicleMake>();


        }
    }
}
